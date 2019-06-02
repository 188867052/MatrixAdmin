using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Entity;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Core.Extension.Dapper
{
    public static partial class DapperExtension
    {
        private static readonly string _getIdentitySql;
        private static readonly string _pagedListSql;
        private static readonly ConcurrentDictionary<string, string> StringBuilderCacheDictionary = new ConcurrentDictionary<string, string>();
        private static readonly Func<TableInfo, string, bool> predicate = (o, entity) => o.TableName.Replace("_", string.Empty).Equals(entity, StringComparison.InvariantCultureIgnoreCase);
        private static IDbConnection _connection;
        private static bool stringBuilderCacheEnabled = true;

        static DapperExtension()
        {
            var properties = typeof(CoreContext).GetProperties();
            foreach (var property in properties)
            {
                if (property.ToString().Contains(typeof(DbSet<>).FullName))
                {
                    var type = property.PropertyType.GenericTypeArguments[0];
                    SqlMapper.SetTypeMap(type, new ColumnAttributeTypeMapper(type));
                }
            }

            using (var connection = Connection)
            {
                string sql = $"SELECT a.name AS [TableName],case e.name when f.column_NAME then 'True' else 'false' end AS IsPrimaryKey,F.COLUMN_NAME AS ColumnName FROM sysobjects AS a LEFT JOIN sysobjects AS b ON a.id=b.parent_obj LEFT JOIN sysindexes AS c ON a.id=c.id AND b.name=c.name LEFT JOIN sysindexkeys AS d ON a.id=d.id AND c.indid=d.indid LEFT JOIN syscolumns AS e ON a.id=e.id AND d.colid=e.colid left join  INFORMATION_SCHEMA.COLUMNS f on a.name=f.TABLE_NAME WHERE a.xtype='U' AND b.xtype='PK'";
                Tables = connection.Query<TableInfo>(sql);
            }

            _getIdentitySql = "SELECT CAST(SCOPE_IDENTITY()  AS BIGINT) AS [id]";
            _pagedListSql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {OrderBy}) AS PagedNumber, {SelectColumns} FROM {TableName} {WhereClause}) AS u WHERE PagedNumber BETWEEN (({PageNumber}-1) * {RowsPerPage} + 1) AND ({PageNumber} * {RowsPerPage})";
        }

        public static IDbConnection Connection
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed)
                {
                    _connection = new SqlConnection("Data Source=.;App=Dapper;Initial Catalog=Core;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                }

                OpenConnection();
                return _connection;
            }
        }

        private static IEnumerable<TableInfo> Tables { get; }

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
            string where = $"WHERE {columnName} = @{propertyName}";
            var parameters = new DynamicParameters();
            parameters.Add("@" + propertyName, value);

            return connection.GetList<T>(where, parameters).ToList();
        }

        public static IList<T> FindAll<T>(this IDbConnection connection, Expression<Func<T, string>> expression, string value)
        {
            string propertyName = expression.GetPropertyName();
            string where = $"WHERE {propertyName} = @{propertyName}";
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

            return connection.GetList<T>($"WHERE {column} = @{key}", parameters).FirstOrDefault();
        }

        public static IList<T> FindAll<T>(this IDbConnection connection, int value)
        {
            var key = GetKey<T>();
            var parameters = new DynamicParameters();
            string column = ToColumn<T>(key);
            parameters.Add($"@{key}", value);

            return connection.GetList<T>($"WHERE {column} = @{key}", parameters).ToList();
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

            StringBuilder sb = new StringBuilder($"INSERT INTO {Encapsulate(tableName)} (");
            sb.Append($"[{string.Join("], [", newColumns)}]");
            sb.Append(") VALUES (");
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

            StringBuilder sb = new StringBuilder($"INSERT INTO {Encapsulate(tableName)} (");
            sb.Append($"[{string.Join("], [", newColumns)}]");
            sb.Append(") VALUES (");
            sb.Append($"@{string.Join(", @", newProperties)}");
            sb.Append(")");

            return connection.Execute(sb.ToString(), entity, transaction, commandTimeout);
        }

        public static int Update<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var sb = new StringBuilder();
            var idProps = GetIdProperties(entity).ToList();
            var tableName = GetTableName<T>();
            sb.AppendFormat("UPDATE {0} SET ", Encapsulate(tableName));
            BuildUpdateSet<T>(sb);
            sb.Append(" WHERE ");
            BuildWhere<T>(sb, idProps, entity);

            return connection.Execute(sb.ToString(), entity, transaction, commandTimeout);
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
            var sb = new StringBuilder();
            var tableName = GetTableName<T>();
            var where = GetAllProperties(whereConditions).ToArray();
            sb.AppendFormat("DELETE FROM {0}", Encapsulate(tableName));
            if (where.Any())
            {
                sb.Append(" WHERE ");
                BuildWhere<T>(sb, where, whereConditions);
            }

            return connection.Execute(sb.ToString(), whereConditions, transaction, commandTimeout);
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

        public static void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public static void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public static IDbTransaction BeginTransaction()
        {
            return Connection.BeginTransaction();
        }

        public static string ToProperty(string field)
        {
            var array = field.Split('_');
            return array.Aggregate<string, string>(default, (current, item) => current + char.ToUpper(item[0]) + item.Substring(1));
        }

        public static string ToColumn<T>(string propertyName)
        {
            return GetColumn<T>(propertyName);
        }

        public static IEnumerable<string> GetColumns<T>()
        {
            return GetTableInfo<T>().Select(x => x.ColumnName).Distinct();
        }

        public static string GetColumn<T>(string propertyName)
        {
            return GetColumns<T>().FirstOrDefault(o => o.Replace("_", string.Empty).Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
        }

        public static string GetKey<T>()
        {
            return GetTableInfo<T>().First(o => o.IsPrimaryKey).ColumnName;
        }

        public static IList<string> GetKeys<T>()
        {
            return GetTableInfo<T>().Where(o => o.IsPrimaryKey).Select(o => o.ColumnName).ToList();
        }

        public static bool HasMultipleKey<T>()
        {
            return GetTableInfo<T>().Count(o => o.IsPrimaryKey) > 1;
        }

        public static string GetTableName<T>()
        {
            return GetTableInfo<T>().First(o => o.IsPrimaryKey).TableName;
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
                    value == DBNull.Value ? "{0} IS NULL" : "{0} = @{1}",
                    ToColumn<T>(property.Name) ?? property.Name,
                    property.Name);
                if (propertyInfos.IndexOf(property) != propertyInfos.Length - 1)
                {
                    sb.AppendFormat(" AND ");
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
                sb.AppendFormat("{0}{1} = @{2}", index == 0 ? string.Empty : " AND ", key, propertyToUse);
            }
        }

        private static IEnumerable<TableInfo> GetTableInfo<T>()
        {
            return Tables.Where(o => predicate(o, typeof(T).Name));
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
            sql = $"SELECT COUNT(*) FROM {Encapsulate(tableName)} {whereSql}";
        }

        private static void PrepareRecordCountByWhereEntity<T>(object whereEntity, out string sql)
        {
            var name = GetTableName<T>();
            StringBuilder sb = new StringBuilder($"SELECT COUNT(*) FROM {Encapsulate(name)}");
            var where = GetAllProperties(whereEntity).ToArray();
            if (where.Any())
            {
                sb.Append(" WHERE ");
                BuildWhere<T>(sb, where, whereEntity);
            }

            sql = sb.ToString();
        }

        private static void PrepareDeleteListByWhereSql<T>(string whereSql, out string sql)
        {
            var name = GetTableName<T>();
            sql = $"DELETE FROM {Encapsulate(name)} {whereSql}";
        }

        private static void PrepareDeleteByEntity<T>(out string sql)
        {
            var tableName = GetTableName<T>();
            var sb = new StringBuilder($"DELETE FROM {Encapsulate(tableName)} WHERE ");
            BuildWhere<T>(sb);
            sql = sb.ToString();
        }

        private static void PrepareGetListByWhereSql<T>(string whereSql, out string sql)
        {
            var sb = new StringBuilder("SELECT ");
            BuildSelectColumns<T>(sb);
            sb.Append($" FROM {Encapsulate(GetTableName<T>())} {whereSql}");
            sql = sb.ToString();
        }

        private static void PrepareGetListByEntity<T>(object entity, out string sql)
        {
            var sb = new StringBuilder("SELECT ");
            BuildSelectColumns<T>(sb);
            var tableName = GetTableName<T>();
            sb.Append($" FROM {Encapsulate(tableName)}");

            var properties = GetAllProperties(entity).ToArray();
            if (properties.Any())
            {
                sb.Append(" WHERE ");
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

        private static void PrepareGet<T>(object id, out string sql, out DynamicParameters parameters)
        {
            var keys = GetKeys<T>();
            var tableName = GetTableName<T>();
            var sb = new StringBuilder("SELECT ");
            BuildSelectColumns<T>(sb);
            sb.AppendFormat(" FROM {0} WHERE ", Encapsulate(tableName));
            sb.AppendJoin("AND", keys.Select(o => $"{o} = @{ToProperty(o)}"));
            sql = sb.ToString();

            parameters = new DynamicParameters();
            foreach (var key in keys)
            {
                parameters.Add("@" + key, id.GetType().Namespace == nameof(System) ? id : id.GetType().GetProperty(key).GetValue(id, null));
            }
        }

        internal class TableInfo
        {
            public string TableName { get; set; }

            public string ColumnName { get; set; }

            public bool IsPrimaryKey { get; set; }
        }
    }
}
