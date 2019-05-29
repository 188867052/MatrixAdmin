using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Extension.Dapper;

namespace Dapper
{
    /// <summary>
    /// Main class for Dapper.SimpleCRUD extensions.
    /// </summary>
    public static partial class SimpleCRUD
    {
        public static async Task<T> GetAsync<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();

            if (!idProps.Any())
            {
                throw new ArgumentException("Get<T> only supports an entity with a [Key] or Id property");
            }

            var name = GetTableName(currenttype);
            var sb = new StringBuilder();
            sb.Append("Select ");

            // create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties<T>().ToArray());
            sb.AppendFormat(" from {0} where ", name);

            for (var i = 0; i < idProps.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" and ");
                }

                sb.AppendFormat("{0} = @{1}", GetColumnName(idProps[i]), idProps[i].Name);
            }

            var dynParms = new DynamicParameters();
            if (idProps.Count == 1)
            {
                dynParms.Add("@" + idProps.First().Name, id);
            }
            else
            {
                foreach (var prop in idProps)
                {
                    dynParms.Add("@" + prop.Name, id.GetType().GetProperty(prop.Name).GetValue(id, null));
                }
            }

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("Get<{0}>: {1} with Id: {2}", currenttype, sb, id));
            }

            var query = await connection.QueryAsync<T>(sb.ToString(), dynParms, transaction, commandTimeout);
            return query.FirstOrDefault();
        }

        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();

            if (!idProps.Any())
            {
                throw new ArgumentException("Entity must have at least one [Key] property");
            }

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");

            // create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties<T>().ToArray());
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere<T>(sb, whereprops, whereConditions);
            }

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("GetList<{0}>: {1}", currenttype, sb));
            }

            return connection.QueryAsync<T>(sb.ToString(), whereConditions, transaction, commandTimeout);
        }

        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
            {
                throw new ArgumentException("Entity must have at least one [Key] property");
            }

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            sb.Append("Select ");

            // create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties<T>().ToArray());
            sb.AppendFormat(" from {0}", name);

            sb.Append(" " + conditions);

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("GetList<{0}>: {1}", currenttype, sb));
            }

            return connection.QueryAsync<T>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection)
        {
            return connection.GetListAsync<T>(new { });
        }

        public static Task<IEnumerable<T>> GetListPagedAsync<T>(this IDbConnection connection, int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (string.IsNullOrEmpty(_pagedListSql))
            {
                throw new Exception("GetListPage is not supported with the current SQL Dialect");
            }

            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
            {
                throw new ArgumentException("Entity must have at least one [Key] property");
            }

            var name = GetTableName(currenttype);
            var sb = new StringBuilder();
            var query = _pagedListSql;
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = GetColumnName(idProps.First());
            }

            // create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties<T>().ToArray());
            query = query.Replace("{SelectColumns}", sb.ToString());
            query = query.Replace("{TableName}", name);
            query = query.Replace("{PageNumber}", pageNumber.ToString());
            query = query.Replace("{RowsPerPage}", rowsPerPage.ToString());
            query = query.Replace("{OrderBy}", orderby);
            query = query.Replace("{WhereClause}", conditions);
            query = query.Replace("{Offset}", ((pageNumber - 1) * rowsPerPage).ToString());

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("GetListPaged<{0}>: {1}", currenttype, query));
            }

            return connection.QueryAsync<T>(query, parameters, transaction, commandTimeout);
        }

        public static dynamic Default(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static async Task<dynamic> InsertAsync<TEntity>(this IDbConnection connection, TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var tableName = DapperExtension.GetTableName<TEntity>();
            var columns = DapperExtension.GetColumns<TEntity>().ToList();
            var newColumns = new List<string>();
            var newProperties = new List<string>();
            string key = DapperExtension.GetKey<TEntity>();

            foreach (var columnName in columns)
            {
                var property = typeof(TEntity).GetProperty(DapperExtension.ToProperty(columnName));
                dynamic value = property.GetValue(entity, null);
                dynamic defaultValue = Default(property.PropertyType);
                if (value != defaultValue && (DapperExtension.HasMultipleKey<TEntity>() || columnName != key))
                {
                    newColumns.Add(columnName);
                    newProperties.Add(property.Name);
                }
            }

            StringBuilder sb = new StringBuilder($"insert into {Encapsulate(tableName)} (");
            sb.Append($"[{string.Join("], [", newColumns)}]");
            sb.Append(") values (");
            sb.Append($"@{string.Join(", @", newProperties)}");
            sb.Append($");{_getIdentitySql}");

            var result = await connection.QueryAsync(sb.ToString(), entity, transaction, commandTimeout);
            if (DapperExtension.HasMultipleKey<TEntity>())
            {
                return typeof(TEntity).GetProperty(DapperExtension.ToProperty(DapperExtension.ToProperty(key))).GetValue(entity, null);
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

            var name = GetTableName(entityToUpdate);

            var sb = new StringBuilder();
            sb.AppendFormat("update {0}", name);

            sb.AppendFormat(" set ");
            BuildUpdateSet<TEntity>(sb);
            sb.Append(" where ");
            BuildWhere<TEntity>(sb, idProps, entityToUpdate);

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("Update: {0}", sb));
            }

            System.Threading.CancellationToken cancelToken = token ?? default(System.Threading.CancellationToken);
            return connection.ExecuteAsync(new CommandDefinition(sb.ToString(), entityToUpdate, transaction, commandTimeout, cancellationToken: cancelToken));
        }

        public static Task<int> DeleteAsync<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var idProps = GetIdProperties(entityToDelete).ToList();

            if (!idProps.Any())
            {
                throw new ArgumentException("Entity must have at least one [Key] or Id property");
            }

            var name = GetTableName(entityToDelete);

            var sb = new StringBuilder();
            sb.AppendFormat("delete from {0}", name);

            sb.Append(" where ");
            BuildWhere<T>(sb, idProps, entityToDelete);

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("Delete: {0}", sb));
            }

            return connection.ExecuteAsync(sb.ToString(), entityToDelete, transaction, commandTimeout);
        }

        public static Task<int> DeleteAsync<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProp = GetIdProperties(currenttype).ToList().FirstOrDefault();

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            sb.AppendFormat("Delete from {0} where ", name);

            sb.AppendFormat("{0} = @{1}", GetColumnName(idProp, idProp.Name), idProp.Name);

            var dynParms = new DynamicParameters();
            dynParms.Add("@" + idProp.Name, id);

            return connection.ExecuteAsync(sb.ToString(), dynParms, transaction, commandTimeout);
        }

        public static Task<int> DeleteListAsync<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.AppendFormat("Delete from {0}", name);
            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere<T>(sb, whereprops);
            }

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("DeleteList<{0}> {1}", currenttype, sb));
            }

            return connection.ExecuteAsync(sb.ToString(), whereConditions, transaction, commandTimeout);
        }

        public static Task<int> DeleteListAsync<T>(this IDbConnection connection, string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (string.IsNullOrEmpty(conditions))
            {
                throw new ArgumentException("DeleteList<T> requires a where clause");
            }

            if (!conditions.ToLower().Contains("where"))
            {
                throw new ArgumentException("DeleteList<T> requires a where clause and must contain the WHERE keyword");
            }

            var currenttype = typeof(T);
            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            sb.AppendFormat("Delete from {0}", name);
            sb.Append(" " + conditions);

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("DeleteList<{0}> {1}", currenttype, sb));
            }

            return connection.ExecuteAsync(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public static Task<int> RecordCountAsync<T>(this IDbConnection connection, string conditions = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var name = GetTableName(currenttype);
            var sb = new StringBuilder();
            sb.Append("Select count(1)");
            sb.AppendFormat(" from {0}", name);
            sb.Append(" " + conditions);

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("RecordCount<{0}>: {1}", currenttype, sb));
            }

            return connection.ExecuteScalarAsync<int>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public static Task<int> RecordCountAsync<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select count(1)");
            sb.AppendFormat(" from {0}", name);
            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere<T>(sb, whereprops);
            }

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("RecordCount<{0}>: {1}", currenttype, sb));
            }

            return connection.ExecuteScalarAsync<int>(sb.ToString(), whereConditions, transaction, commandTimeout);
        }
    }
}
