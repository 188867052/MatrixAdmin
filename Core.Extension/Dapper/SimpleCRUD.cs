using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Extension.Dapper.Attributes;
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
        private static readonly ConcurrentDictionary<Type, string> TableNames = new ConcurrentDictionary<Type, string>();
        private static readonly ConcurrentDictionary<string, string> ColumnNames = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, string> StringBuilderCacheDictionary = new ConcurrentDictionary<string, string>();
        private static bool stringBuilderCacheEnabled = true;
        private static ITableNameResolver _tableNameResolver = new TableNameResolver();
        private static IColumnNameResolver _columnNameResolver = new ColumnNameResolver();

        public static T Get<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currentType = typeof(T);
            var idProps = GetIdProperties(currentType).ToList();

            if (!idProps.Any())
            {
                throw new ArgumentException("Get<T> only supports an entity with a [Key] or Id property");
            }

            var name = DapperExtension.GetTableName<T>();
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
            string tableName = DapperExtension.GetTableName<T>();
            string sql = $"SELECT  TOP 1 * FROM [{tableName}]";

            return DapperExtension.Connection.QueryFirst<T>(sql);
        }

        public static IEnumerable<T> GetList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var sb = new StringBuilder("Select ");
            BuildSelect<T>(sb);
            var tableName = DapperExtension.GetTableName<T>();
            sb.Append($" from {Encapsulate(tableName)}");

            var properties = GetAllProperties(whereConditions).ToArray();
            if (properties.Any())
            {
                sb.Append(" where ");
                BuildWhere<T>(sb, properties, whereConditions);
            }

            return connection.Query<T>(sb.ToString(), whereConditions, transaction, true, commandTimeout);
        }

        public static IList<T> FindAll<T>(this IDbConnection connection, Expression<Func<T, int>> expression, int value)
        {
            string propertyName = expression.GetPropertyName();
            string columnName = DapperExtension.ToColumn<T>(propertyName);
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
            var key = DapperExtension.GetKey<T>();
            var parameters = new DynamicParameters();
            string column = DapperExtension.ToColumn<T>(key);
            parameters.Add($"@{key}", value);

            return connection.GetList<T>($"where {column} = @{key}", parameters).FirstOrDefault();
        }

        public static IList<T> FindAll<T>(this IDbConnection connection, int value)
        {
            var key = DapperExtension.GetKey<T>();
            var parameters = new DynamicParameters();
            string column = DapperExtension.ToColumn<T>(key);
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

        public static IEnumerable<T> GetList<T>(this IDbConnection connection, string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currentType = typeof(T);
            var sb = new StringBuilder("Select ");

            string tableName = Encapsulate(DapperExtension.GetTableName<T>());
            BuildSelect<T>(sb);
            sb.Append($" from {tableName}");
            sb.Append(" " + conditions);

            return connection.Query<T>(sb.ToString(), parameters, transaction, true, commandTimeout);
        }

        public static IEnumerable<T> GetList<T>(this IDbConnection connection)
        {
            return connection.GetList<T>(new { });
        }

        public static IEnumerable<T> GetListPaged<T>(this IDbConnection connection, int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentException("Page must be greater than 0");
            }

            var tableName = DapperExtension.GetTableName<T>();
            var sb = new StringBuilder();
            var query = _pagedListSql;
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = DapperExtension.GetKey<T>();
            }

            BuildSelect<T>(sb);
            query = query.Replace("{SelectColumns}", sb.ToString());
            query = query.Replace("{TableName}", Encapsulate(tableName));
            query = query.Replace("{PageNumber}", pageNumber.ToString());
            query = query.Replace("{RowsPerPage}", rowsPerPage.ToString());
            query = query.Replace("{OrderBy}", orderby);
            query = query.Replace("{WhereClause}", conditions);
            query = query.Replace("{Offset}", ((pageNumber - 1) * rowsPerPage).ToString());

            return connection.Query<T>(query, parameters, transaction, true, commandTimeout);
        }

        public static dynamic InsertReturnKey<TEntity>(this IDbConnection connection, TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
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
            var result = connection.Query(sb.ToString(), entity, transaction, true, commandTimeout);

            if (DapperExtension.HasMultipleKey<TEntity>())
            {
                return typeof(TEntity).GetProperty(DapperExtension.ToProperty(DapperExtension.ToProperty(key))).GetValue(entity, null);
            }

            return result.First().id;
        }

        public static int Insert<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var tableName = DapperExtension.GetTableName<T>();
            var columns = DapperExtension.GetColumns<T>().ToList();
            var newColumns = new List<string>();
            var newProperties = new List<string>();
            string key = DapperExtension.GetKey<T>();

            foreach (var columnName in columns)
            {
                var property = typeof(T).GetProperty(DapperExtension.ToProperty(columnName));
                dynamic value = property.GetValue(entity, null);
                dynamic defaultValue = Default(property.PropertyType);
                if (value != defaultValue && (DapperExtension.HasMultipleKey<T>() || columnName != key))
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

                if (!idProps.Any())
                {
                    throw new ArgumentException("Entity must have at least one [Key] or Id property");
                }

                var tableName = DapperExtension.GetTableName<T>();
                sb.AppendFormat("update {0} set ", tableName);
                BuildUpdateSet<T>(sb);
                sb.Append(" where ");
                BuildWhere<T>(sb, idProps, entity);
            });

            return connection.Execute(stringBuilder.ToString(), entity, transaction, commandTimeout);
        }

        public static int Delete<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var stringBuilder = new StringBuilder();
            StringBuilderCache(stringBuilder, $"{typeof(T).FullName}_Delete", sb =>
            {
                var tableName = DapperExtension.GetTableName<T>();
                sb.AppendFormat("delete from {0} where ", tableName);
                BuildWhere<T>(sb);
            });

            return connection.Execute(stringBuilder.ToString(), entity, transaction, commandTimeout);
        }

        public static int Delete<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var name = GetTableName<T>();
            var sb = new StringBuilder();
            sb.AppendFormat("Delete from {0} where ", name);
            string key = DapperExtension.GetKey<T>();
            sb.AppendFormat("{0} = @{1}", key, DapperExtension.ToProperty(key));
            var parameters = new DynamicParameters();
            parameters.Add("@" + key, id);

            return connection.Execute(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public static int DeleteList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_DeleteWhere{whereConditions?.GetType()?.FullName}", sb =>
            {
                var tableName = GetTableName<T>();
                var where = GetAllProperties(whereConditions).ToArray();
                sb.AppendFormat("Delete from {0}", tableName);
                if (where.Any())
                {
                    sb.Append(" where ");
                    BuildWhere<T>(sb, where);
                }
            });

            return connection.Execute(masterSb.ToString(), whereConditions, transaction, commandTimeout);
        }

        public static int DeleteList<T>(this IDbConnection connection, string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_DeleteWhere{conditions}", sb =>
            {
                if (string.IsNullOrEmpty(conditions))
                {
                    throw new ArgumentException("DeleteList<T> requires a where clause");
                }

                if (!conditions.ToLower().Contains("where"))
                {
                    throw new ArgumentException("DeleteList<T> requires a where clause and must contain the WHERE keyword");
                }

                sb.AppendFormat("Delete from {0}", DapperExtension.GetTableName<T>());
                sb.Append(" " + conditions);
            });

            return connection.Execute(masterSb.ToString(), parameters, transaction, commandTimeout);
        }

        public static int RecordCount<T>(this IDbConnection connection, string conditions = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var tableName = DapperExtension.GetTableName<T>();
            var sb = new StringBuilder();
            sb.AppendFormat("Select count(1) from {0}", Encapsulate(tableName));
            sb.Append(" " + conditions);

            return connection.ExecuteScalar<int>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public static string Encapsulate(string databaseWord)
        {
            return $"[{databaseWord}]";
        }

        public static int RecordCount<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var tableName = DapperExtension.GetTableName<T>();
            var where = GetAllProperties(whereConditions).ToArray();
            var sb = new StringBuilder();
            sb.AppendFormat("Select count(1) from {0}", Encapsulate(tableName));
            if (where.Any())
            {
                sb.Append(" where ");
                BuildWhere<T>(sb, where);
            }

            return connection.ExecuteScalar<int>(sb.ToString(), whereConditions, transaction, commandTimeout);
        }

        // build update statement based on list on an entity
        private static void BuildUpdateSet<T>(StringBuilder stringBuilder)
        {
            var key = DapperExtension.GetKey<T>();
            var nonIdColumns = DapperExtension.GetColumns<T>().Where(o => o != key);
            foreach (var item in nonIdColumns)
            {
                int index = nonIdColumns.IndexOf(item);
                stringBuilder.AppendFormat("{0}{1} = @{2}", index == 0 ? string.Empty : ",", item, DapperExtension.ToProperty(item));
            }
        }

        // build select clause based on list of properties skipping ones with the IgnoreSelect and NotMapped attribute
        private static void BuildSelect(StringBuilder masterSb, IEnumerable<PropertyInfo> properties, IList<string> columns = null)
        {
            StringBuilderCache(masterSb, $"{properties.CacheKey()}_BuildSelect", sb =>
            {
                var propertyInfos = properties as IList<PropertyInfo> ?? properties.ToList();
                var addedAny = false;
                for (var i = 0; i < propertyInfos.Count; i++)
                {
                    if (i == columns.Count)
                    {
                        break;
                    }

                    var property = propertyInfos.ElementAt(i);

                    if (property.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(IgnoreSelectAttribute).Name || attr.GetType().Name == typeof(NotMappedAttribute).Name))
                    {
                        continue;
                    }

                    if (addedAny)
                    {
                        sb.Append(",");
                    }

                    string name = columns.FirstOrDefault(o => o.Replace("_", string.Empty).Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));
                    sb.Append(GetColumnName(property, name));

                    if (property.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name) != null)
                    {
                        sb.Append(" as " + Encapsulate(property.Name));
                    }

                    addedAny = true;
                }
            });
        }

        private static void BuildSelect<T>(StringBuilder stringBuilder)
        {
            StringBuilderCache(stringBuilder, $"{typeof(T).Name}_BuildSelect", sb =>
            {
                IEnumerable<string> columns = DapperExtension.GetColumns<T>();
                columns.ToList().ForEach(o => GetColumnName<T>(o));
                sb.Append(string.Join(",", columns));
            });
        }

        private static void BuildWhere<TEntity>(StringBuilder sb, IEnumerable<PropertyInfo> idProps, object whereConditions = null)
        {
            var propertyInfos = idProps.ToArray();
            for (var i = 0; i < propertyInfos.Length; i++)
            {
                var useIsNull = false;

                // match up generic properties to source entity properties to allow fetching of the column attribute
                // the anonymous object used for search doesn't have the custom attributes attached to them so this allows us to build the correct where clause
                // by converting the model type to the database column name via the column attribute
                var propertyToUse = propertyInfos.ElementAt(i);
                var sourceProperties = GetScaffoldableProperties<TEntity>().ToArray();
                for (var x = 0; x < sourceProperties.Length; x++)
                {
                    if (sourceProperties.ElementAt(x).Name.Equals(propertyToUse.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (whereConditions != null && propertyToUse.CanRead && (propertyToUse.GetValue(whereConditions, null) == null || propertyToUse.GetValue(whereConditions, null) == DBNull.Value))
                        {
                            useIsNull = true;
                        }

                        propertyToUse = sourceProperties.ElementAt(x);
                        break;
                    }
                }

                sb.AppendFormat(
                    useIsNull ? "{0} is null" : "{0} = @{1}",
                    GetColumnName(propertyToUse, propertyToUse.Name),
                    propertyToUse.Name);

                if (i < propertyInfos.Length - 1)
                {
                    sb.AppendFormat(" and ");
                }
            }
        }

        private static void BuildWhere<TEntity>(StringBuilder sb)
        {
            var keys = DapperExtension.GetKeys<TEntity>();
            foreach (var key in keys)
            {
                int index = keys.IndexOf(key);
                string propertyToUse = DapperExtension.ToProperty(key);
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

        // Get all properties that are not decorated with the Editable(false) attribute
        private static IEnumerable<PropertyInfo> GetScaffoldableProperties<T>()
        {
            IEnumerable<PropertyInfo> props = typeof(T).GetProperties();

            props = props.Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(EditableAttribute).Name && !IsEditable(p)) == false);

            return props.Where(p => p.PropertyType.IsSimpleType() || IsEditable(p));
        }

        // Determine if the Attribute has an AllowEdit key and return its boolean state
        // fake the funk and try to mimic EditableAttribute in System.ComponentModel.DataAnnotations
        // This allows use of the DataAnnotations property in the model and have the SimpleCRUD engine just figure it out without a reference
        private static bool IsEditable(PropertyInfo pi)
        {
            var attributes = pi.GetCustomAttributes(false);
            if (attributes.Length > 0)
            {
                dynamic write = attributes.FirstOrDefault(x => x.GetType().Name == typeof(EditableAttribute).Name);
                if (write != null)
                {
                    return write.AllowEdit;
                }
            }

            return false;
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
            var tp = type.GetProperties().Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name)).ToList();
            return tp.Any() ? tp : type.GetProperties().Where(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        }

        private static string GetColumnName(PropertyInfo propertyInfo, string name = default)
        {
            string key = $"{propertyInfo.DeclaringType}.{propertyInfo.Name}";
            if (ColumnNames.TryGetValue(key, out string columnName))
            {
                return columnName;
            }

            columnName = _columnNameResolver.ResolveColumnName(propertyInfo, name);
            ColumnNames.AddOrUpdate(key, columnName, (t, v) => columnName);

            return columnName;
        }

        private static string GetColumnName<T>(string name)
        {
            string key = $"{typeof(T).DeclaringType}.{typeof(T).Name}";
            if (ColumnNames.TryGetValue(key, out string columnName))
            {
                return columnName;
            }

            columnName = _columnNameResolver.ResolveColumnName<T>(name);
            ColumnNames.AddOrUpdate(key, columnName, (t, v) => columnName);

            return columnName;
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
            var table = DapperExtension.GetTableName<T>();
            string propertyName = expression.Body.GetName();
            string column = DapperExtension.ToColumn<T>(propertyName);
            var parameters = new DynamicParameters();
            parameters.Add($"@{propertyName}", value);

            return connection.QueryFirst<T>($"SELECT TOP 1 * FROM [{table}] WHERE {column} = @{propertyName}", parameters);
        }
    }
}
