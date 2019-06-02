using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore.Internal;

namespace Core.Extension.Dapper
{
    /// <summary>
    /// Main class for Dapper extensions.
    /// </summary>
    public static partial class DapperExtension
    {
        private static readonly string _getIdentitySql;
        private static readonly string _pagedListSql;
        private static readonly ConcurrentDictionary<string, string> StringBuilderCacheDictionary = new ConcurrentDictionary<string, string>();
        private static bool stringBuilderCacheEnabled = true;

        public static T Get<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currentType = typeof(T);
            var idProps = GetIdProperties(currentType).ToList();

            if (!idProps.Any())
            {
                throw new ArgumentException("Get<T> only supports an entity with a [Key] or Id property");
            }

            var name = GetTableName<T>();
            var sb = new StringBuilder();
            sb.Append("Select ");

            BuildSelectColumns(sb);
            sb.AppendFormat(" from {0} where ", name);

            for (var i = 0; i < idProps.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" and ");
                }

                sb.AppendFormat("{0} = @{1}", ToColumn<T>(idProps[i].Name), idProps[i].Name);
            }

            var parameters = new DynamicParameters();
            if (idProps.Count == 1)
            {
                parameters.Add("@" + idProps.First().Name, id);
            }
            else
            {
                foreach (var prop in idProps)
                {
                    parameters.Add("@" + prop.Name, id.GetType().GetProperty(prop.Name).GetValue(id, null));
                }
            }

            return connection.Query<T>(sb.ToString(), parameters, transaction, true, commandTimeout).FirstOrDefault();
        }

        public static T QueryFirst<T>(this IDbConnection connection)
        {
            string tableName = GetTableName<T>();
            string sql = $"SELECT  TOP 1 * FROM [{tableName}]";
            return Connection.QueryFirst<T>(sql);
        }

        public static IEnumerable<T> GetList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListByEntity<T>(whereConditions, out string sql);
            return connection.Query<T>(sql, whereConditions, transaction, true, commandTimeout);
        }

        public static IList<T> FindAll<T>(this IDbConnection connection, Expression<Func<T, int>> expression, int value)
        {
            string propertyName = expression.GetPropertyName();
            string columnName = ToColumn<T>(propertyName);
            string where = $"where {columnName} = @{propertyName}";
            var parameters = new DynamicParameters();
            parameters.Add("@" + propertyName, value);

            return connection.GetList<T>(where, parameters).ToList();
        }

        public static IList<T> FindAll<T>(this IDbConnection connection, Expression<Func<T, string>> expression, string value)
        {
            string propertyName = expression.GetPropertyName();
            string where = $"where {propertyName} = @{propertyName}";
            var parameters = new DynamicParameters();
            parameters.Add("@" + propertyName, value);

            return connection.GetList<T>(where, parameters).ToList();
        }

        public static IList<T> FindAll<T>(this IDbConnection connection)
        {
            return connection.GetList<T>().ToList();
        }

        public static T Find<T>(this IDbConnection connection, int value)
        {
            var key = GetKey<T>();
            var parameters = new DynamicParameters();
            string column = ToColumn<T>(key);
            parameters.Add($"@{key}", value);

            return connection.GetList<T>($"where {column} = @{key}", parameters).FirstOrDefault();
        }

        public static IList<T> FindAll<T>(this IDbConnection connection, int value)
        {
            var key = GetKey<T>();
            var parameters = new DynamicParameters();
            string column = ToColumn<T>(key);
            parameters.Add($"@{key}", value);

            return connection.GetList<T>($"where {column} = @{key}", parameters).ToList();
        }

        public static T QueryFirst<T>(this IDbConnection connection, Expression<Func<T, bool>> expression, bool value)
        {
            return connection.QueryTopOne(expression, value);
        }

        public static T QueryFirst<T>(this IDbConnection connection, Expression<Func<T, int>> expression, int value)
        {
            return connection.QueryTopOne(expression, value);
        }

        public static T QueryFirst<T>(this IDbConnection connection, Expression<Func<T, string>> expression, string value)
        {
            return connection.QueryTopOne(expression, value);
        }

        public static IEnumerable<T> GetList<T>(this IDbConnection connection, string whereSql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListByWhereSql<T>(whereSql, out var sql);
            return connection.Query<T>(sql, parameters, transaction, true, commandTimeout);
        }

        public static IEnumerable<T> GetList<T>(this IDbConnection connection)
        {
            return connection.GetList<T>(new { });
        }

        public static IEnumerable<T> GetListPaged<T>(this IDbConnection connection, int pageNumber, int rowsPerPage, string conditions, string orderBy, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListPagedByWhereSql<T>(pageNumber, rowsPerPage, orderBy, conditions, out string sql);
            return connection.Query<T>(sql, parameters, transaction, true, commandTimeout);
        }

        public static dynamic InsertReturnKey<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var tableName = GetTableName<T>();
            var columns = GetColumns<T>().ToList();
            var newColumns = new List<string>();
            var newProperties = new List<string>();
            string key = GetKey<T>();

            foreach (var columnName in columns)
            {
                var property = typeof(T).GetProperty(ToProperty(columnName));
                dynamic value = property.GetValue(entity, null);
                dynamic defaultValue = Default(property.PropertyType);
                if (value != defaultValue && (HasMultipleKey<T>() || columnName != key))
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
            var result = connection.Query(sb.ToString(), entity, transaction, true, commandTimeout);

            if (HasMultipleKey<T>())
            {
                return typeof(T).GetProperty(ToProperty(ToProperty(key))).GetValue(entity, null);
            }

            return result.First().id;
        }

        public static int Insert<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var tableName = GetTableName<T>();
            var columns = GetColumns<T>().ToList();
            var newColumns = new List<string>();
            var newProperties = new List<string>();
            string key = GetKey<T>();

            foreach (var columnName in columns)
            {
                var property = typeof(T).GetProperty(ToProperty(columnName));
                dynamic value = property.GetValue(entity, null);
                dynamic defaultValue = Default(property.PropertyType);
                if (value != defaultValue && (HasMultipleKey<T>() || columnName != key))
                {
                    newColumns.Add(columnName);
                    newProperties.Add(property.Name);
                }
            }

            StringBuilder sb = new StringBuilder($"insert into {Encapsulate(tableName)} (");
            sb.Append($"[{string.Join("], [", newColumns)}]");
            sb.Append(") values (");
            sb.Append($"@{string.Join(", @", newProperties)}");
            sb.Append(")");

            return connection.Execute(sb.ToString(), entity, transaction, commandTimeout);
        }

        public static int Update<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var stringBuilder = new StringBuilder();
            StringBuilderCache(stringBuilder, $"{typeof(T).FullName}_Update", sb =>
            {
                var idProps = GetIdProperties(entity).ToList();
                var tableName = GetTableName<T>();
                sb.AppendFormat("update {0} set ", Encapsulate(tableName));
                BuildUpdateSet<T>(sb);
                sb.Append(" where ");
                BuildWhere<T>(sb, idProps, entity);
            });

            return connection.Execute(stringBuilder.ToString(), entity, transaction, commandTimeout);
        }

        public static int Delete<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareDeleteByEntity<T>(out var sql);
            return connection.Execute(sql, entity, transaction, commandTimeout);
        }

        public static int Delete<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareDelete<T>(id, out var sql, out var parameter);
            return connection.Execute(sql, parameter, transaction, commandTimeout);
        }

        public static int DeleteList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_DeleteWhere{whereConditions?.GetType()?.FullName}", sb =>
            {
                var tableName = GetTableName<T>();
                var where = GetAllProperties(whereConditions).ToArray();
                sb.AppendFormat("Delete from {0}", Encapsulate(tableName));
                if (where.Any())
                {
                    sb.Append(" where ");
                    BuildWhere<T>(sb, where, whereConditions);
                }
            });

            return connection.Execute(masterSb.ToString(), whereConditions, transaction, commandTimeout);
        }

        public static int DeleteList<T>(this IDbConnection connection, string whereSql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareDeleteListByWhereSql<T>(whereSql, out var sql);
            return connection.Execute(sql, parameters, transaction, commandTimeout);
        }

        public static int RecordCount<T>(this IDbConnection connection, string conditions = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareRecordCountByWhereSql<T>(conditions, out var sql);
            return connection.ExecuteScalar<int>(sql, parameters, transaction, commandTimeout);
        }

        public static string Encapsulate(string databaseWord)
        {
            return $"[{databaseWord}]";
        }

        public static int RecordCount<T>(this IDbConnection connection, object whereEntity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareRecordCountByWhereEntity<T>(whereEntity, out var sql);
            return connection.ExecuteScalar<int>(sql, whereEntity, transaction, commandTimeout);
        }

        // build update statement based on list on an entity
        private static void BuildUpdateSet<T>(StringBuilder stringBuilder)
        {
            var key = GetKey<T>();
            var nonIdColumns = GetColumns<T>().Where(o => o != key);
            foreach (var item in nonIdColumns)
            {
                int index = nonIdColumns.IndexOf(item);
                stringBuilder.AppendFormat("{0}{1} = @{2}", index == 0 ? string.Empty : ",", item, ToProperty(item));
            }
        }

        private static void BuildSelectColumns(StringBuilder stringBuilder, IList<string> columns = null)
        {
            stringBuilder.AppendJoin(",", columns);
        }

        private static void BuildSelectColumns<T>(StringBuilder stringBuilder)
        {
            var columns = GetColumns<T>();
            stringBuilder.AppendJoin(",", columns);
        }

        private static void BuildWhere<T>(StringBuilder sb, IEnumerable<PropertyInfo> idProps, object whereConditions = null)
        {
            var propertyInfos = idProps.ToArray();
            foreach (var property in propertyInfos)
            {
                var value = property.GetValue(whereConditions, null);
                sb.AppendFormat(
                    value == DBNull.Value ? "{0} is null" : "{0} = @{1}",
                    ToColumn<T>(property.Name) ?? property.Name,
                    property.Name);
                if (propertyInfos.IndexOf(property) != propertyInfos.Length - 1)
                {
                    sb.AppendFormat(" and ");
                }
            }
        }

        private static void BuildWhere<TEntity>(StringBuilder sb)
        {
            var keys = GetKeys<TEntity>();
            foreach (var key in keys)
            {
                int index = keys.IndexOf(key);
                string propertyToUse = ToProperty(key);
                sb.AppendFormat("{0}{1} = @{2}", index == 0 ? string.Empty : " and ", key, propertyToUse);
            }
        }

        // Get all properties in an entity
        private static IEnumerable<PropertyInfo> GetAllProperties<T>(T entity) where T : class
        {
            if (entity == null)
            {
                return Array.Empty<PropertyInfo>();
            }

            return entity.GetType().GetProperties();
        }

        // Determine if the Attribute has an IsReadOnly key and return its boolean state
        // fake the funk and try to mimic ReadOnlyAttribute in System.ComponentModel
        // This allows use of the DataAnnotations property in the model and have the SimpleCRUD engine just figure it out without a reference
        private static IEnumerable<PropertyInfo> GetIdProperties(object entity)
        {
            var type = entity.GetType();
            return GetIdProperties(type);
        }

        // Get all properties that are named Id or have the Key attribute
        // For Get(id) and Delete(id) we don't have an entity, just the type so this method is used
        private static IEnumerable<PropertyInfo> GetIdProperties(Type type)
        {
            var tp = type.GetProperties().ToList();
            return tp.Where(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        }

        private static void StringBuilderCache(StringBuilder sb, string cacheKey, Action<StringBuilder> stringBuilderAction)
        {
            if (stringBuilderCacheEnabled && StringBuilderCacheDictionary.TryGetValue(cacheKey, out string value))
            {
                sb.Append(value);
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilderAction(stringBuilder);
            value = stringBuilder.ToString();
            StringBuilderCacheDictionary.AddOrUpdate(cacheKey, value, (t, v) => value);
            sb.Append(value);
        }

        private static T QueryTopOne<T, TProperty>(this IDbConnection connection, Expression<Func<T, TProperty>> expression, TProperty value = default)
        {
            var table = GetTableName<T>();
            string propertyName = expression.Body.GetName();
            string column = ToColumn<T>(propertyName);
            var parameters = new DynamicParameters();
            parameters.Add($"@{propertyName}", value);

            return connection.QueryFirst<T>($"SELECT TOP 1 * FROM [{table}] WHERE {column} = @{propertyName}", parameters);
        }

        private static void PrepareDelete<T>(object id, out string sql, out DynamicParameters parameter)
        {
            var key = GetKey<T>();
            var name = GetTableName<T>();
            sql = $"Delete from {name} where {key} = @{key}";

            parameter = new DynamicParameters();
            parameter.Add("@" + key, id);
        }

        private static void PrepareRecordCountByWhereSql<T>(string whereSql, out string sql)
        {
            var tableName = GetTableName<T>();
            sql = $"Select count(1) from {Encapsulate(tableName)} {whereSql}";
        }

        private static void PrepareRecordCountByWhereEntity<T>(object whereEntity, out string sql)
        {
            var name = GetTableName<T>();
            StringBuilder sb = new StringBuilder($"Select count(1) from {Encapsulate(name)}");
            var where = GetAllProperties(whereEntity).ToArray();
            if (where.Any())
            {
                sb.Append(" where ");
                BuildWhere<T>(sb, where, whereEntity);
            }

            sql = sb.ToString();
        }

        private static void PrepareDeleteListByWhereSql<T>(string whereSql, out string sql)
        {
            var name = GetTableName<T>();
            sql = $"Delete from {Encapsulate(name)} {whereSql}";
        }

        private static void PrepareDeleteByEntity<T>(out string sql)
        {
            var tableName = GetTableName<T>();
            var sb = new StringBuilder($"delete from {Encapsulate(tableName)} where ");
            BuildWhere<T>(sb);
            sql = sb.ToString();
        }

        private static void PrepareGetListByWhereSql<T>(string whereSql, out string sql)
        {
            var sb = new StringBuilder("Select ");
            BuildSelectColumns<T>(sb);
            sb.Append($" from {Encapsulate(GetTableName<T>())} {whereSql}");
            sql = sb.ToString();
        }

        private static void PrepareGetListByEntity<T>(object entity, out string sql)
        {
            var sb = new StringBuilder("Select ");
            BuildSelectColumns<T>(sb);
            var tableName = GetTableName<T>();
            sb.Append($" from {Encapsulate(tableName)}");

            var properties = GetAllProperties(entity).ToArray();
            if (properties.Any())
            {
                sb.Append(" where ");
                BuildWhere<T>(sb, properties, entity);
            }

            sql = sb.ToString();
        }

        private static void PrepareGetListPagedByWhereSql<T>(int pageNumber, int rowsPerPage, string orderBy, string conditions, out string sql)
        {
            var tableName = GetTableName<T>();
            sql = _pagedListSql;
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = GetKey<T>();
            }

            var sb = new StringBuilder();
            BuildSelectColumns<T>(sb);
            sql = sql.Replace("{SelectColumns}", sb.ToString());
            sql = sql.Replace("{TableName}", Encapsulate(tableName));
            sql = sql.Replace("{PageNumber}", pageNumber.ToString());
            sql = sql.Replace("{RowsPerPage}", rowsPerPage.ToString());
            sql = sql.Replace("{OrderBy}", orderBy);
            sql = sql.Replace("{WhereClause}", conditions);
            sql = sql.Replace("{Offset}", ((pageNumber - 1) * rowsPerPage).ToString());
        }
    }
}
