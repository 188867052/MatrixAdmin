using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Core.Extension.Dapper
{
    /// <summary>
    /// Main class for Dapper extensions.
    /// </summary>
    public static partial class DapperExtension
    {
        public static async Task<T> GetAsync<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var type = typeof(T);
            var idProps = GetIdProperties(type).ToList();

            if (!idProps.Any())
            {
                throw new ArgumentException("Get<T> only supports an entity with a [Key] or Id property");
            }

            var name = GetTableName<T>();
            var sb = new StringBuilder();
            sb.Append("SELECT ");
            BuildSelectColumns(sb);
            sb.AppendFormat(" FROM {0} WHERE ", Encapsulate(name));

            for (var i = 0; i < idProps.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" AND ");
                }

                sb.AppendFormat("{0} = @{1}", ToColumn<T>(idProps[i].Name), idProps[i].Name);
            }

            var parameter = new DynamicParameters();
            if (idProps.Count == 1)
            {
                parameter.Add("@" + idProps.First().Name, id);
            }
            else
            {
                foreach (var prop in idProps)
                {
                    parameter.Add("@" + prop.Name, id.GetType().GetProperty(prop.Name).GetValue(id, null));
                }
            }

            var query = await connection.QueryAsync<T>(sb.ToString(), parameter, transaction, commandTimeout);
            return query.FirstOrDefault();
        }

        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListByEntity<T>(whereConditions, out string sql);
            return connection.QueryAsync<T>(sql, whereConditions, transaction, commandTimeout);
        }

        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, string whereSql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListByWhereSql<T>(whereSql, out var sql);
            return connection.QueryAsync<T>(sql, parameters, transaction, commandTimeout);
        }

        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection)
        {
            return connection.GetListAsync<T>(new { });
        }

        public static Task<IEnumerable<T>> GetListPagedAsync<T>(this IDbConnection connection, int pageNumber, int rowsPerPage, string conditions, string orderBy, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListPagedByWhereSql<T>(pageNumber, rowsPerPage, orderBy, conditions, out string sql);
            return connection.QueryAsync<T>(sql, parameters, transaction, commandTimeout);
        }

        public static dynamic Default(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static async Task<dynamic> InsertAsync<TEntity>(this IDbConnection connection, TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var tableName = GetTableName<TEntity>();
            var columns = GetColumns<TEntity>().ToList();
            var newColumns = new List<string>();
            var newProperties = new List<string>();
            string key = GetKey<TEntity>();

            foreach (var columnName in columns)
            {
                var property = typeof(TEntity).GetProperty(ToProperty(columnName));
                dynamic value = property.GetValue(entity, null);
                dynamic defaultValue = Default(property.PropertyType);
                if (value != defaultValue && (HasMultipleKey<TEntity>() || columnName != key))
                {
                    newColumns.Add(columnName);
                    newProperties.Add(property.Name);
                }
            }

            StringBuilder sb = new StringBuilder($"INSERT INTO {Encapsulate(tableName)} (");
            sb.Append($"[{string.Join("], [", newColumns)}]");
            sb.Append(") VALUES (");
            sb.Append($"@{string.Join(", @", newProperties)}");
            sb.Append($");{_getIdentitySql}");

            var result = await connection.QueryAsync(sb.ToString(), entity, transaction, commandTimeout);
            if (HasMultipleKey<TEntity>())
            {
                return typeof(TEntity).GetProperty(ToProperty(ToProperty(key))).GetValue(entity, null);
            }

            return result.First().id;
        }

        public static Task<int> UpdateAsync<TEntity>(this IDbConnection connection, TEntity entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null, System.Threading.CancellationToken? token = null)
        {
            var idProps = GetIdProperties(entityToUpdate).ToList();

            if (!idProps.Any())
            {
                throw new ArgumentException("Entity must have at least one [Key] or Id property");
            }

            var name = GetTableName<TEntity>();
            var sb = new StringBuilder($"UPDATE {Encapsulate(name)} SET ");
            BuildUpdateSet<TEntity>(sb);
            sb.Append(" WHERE ");
            BuildWhere<TEntity>(sb, idProps, entityToUpdate);

            CancellationToken cancelToken = token ?? default;
            return connection.ExecuteAsync(new CommandDefinition(sb.ToString(), entityToUpdate, transaction, commandTimeout, cancellationToken: cancelToken));
        }

        public static Task<int> DeleteAsync<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareDeleteByEntity<T>(out var sql);
            return connection.ExecuteAsync(sql, entityToDelete, transaction, commandTimeout);
        }

        public static Task<int> DeleteAsync<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareDelete<T>(id, out var sql, out var parameter);
            return connection.ExecuteAsync(sql, parameter, transaction, commandTimeout);
        }

        public static Task<int> DeleteListAsync<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var name = GetTableName<T>();
            var sb = new StringBuilder($"DELETE FROM {Encapsulate(name)}");
            var where = GetAllProperties(whereConditions).ToArray();
            if (where.Any())
            {
                sb.Append(" WHERE ");
                BuildWhere<T>(sb, where);
            }

            return connection.ExecuteAsync(sb.ToString(), whereConditions, transaction, commandTimeout);
        }

        public static Task<int> DeleteListAsync<T>(this IDbConnection connection, string whereSql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareDeleteListByWhereSql<T>(whereSql, out var sql);
            return connection.ExecuteAsync(sql, parameters, transaction, commandTimeout);
        }

        public static Task<int> RecordCountAsync<T>(this IDbConnection connection, string whereSql = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareRecordCountByWhereSql<T>(whereSql, out var sql);
            return connection.ExecuteScalarAsync<int>(sql, parameters, transaction, commandTimeout);
        }

        public static Task<int> RecordCountAsync<T>(this IDbConnection connection, object whereEntity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareRecordCountByWhereEntity<T>(whereEntity, out var sql);
            return connection.ExecuteScalarAsync<int>(sql, whereEntity, transaction, commandTimeout);
        }
    }
}
