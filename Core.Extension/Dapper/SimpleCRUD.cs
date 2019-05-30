using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Extension;
using Core.Extension.Dapper;
using Microsoft.EntityFrameworkCore.Internal;

namespace Dapper
{
    /// <summary>
    /// Main class for Dapper.SimpleCRUD extensions.
    /// </summary>
    public static partial class SimpleCRUD
    {
        private static readonly string _getIdentitySql;
        private static readonly string _pagedListSql;
        private static readonly ConcurrentDictionary<Type, string> TableNames = new ConcurrentDictionary<Type, string>();
        private static readonly ConcurrentDictionary<string, string> ColumnNames = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, string> StringBuilderCacheDictionary = new ConcurrentDictionary<string, string>();
        private static bool stringBuilderCacheEnabled = true;
        private static ITableNameResolver _tableNameResolver = new TableNameResolver();
        private static IColumnNameResolver _columnNameResolver = new ColumnNameResolver();

        static SimpleCRUD()
        {
            _getIdentitySql = "SELECT CAST(SCOPE_IDENTITY()  AS BIGINT) AS [id]";
            _pagedListSql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {OrderBy}) AS PagedNumber, {SelectColumns} FROM {TableName} {WhereClause}) AS u WHERE PagedNumber BETWEEN (({PageNumber}-1) * {RowsPerPage} + 1) AND ({PageNumber} * {RowsPerPage})";
        }

        /// <summary>
        /// Sets the table name resolver.
        /// </summary>
        /// <param name="resolver">The resolver to use when requesting the format of a table name.</param>
        public static void SetTableNameResolver(ITableNameResolver resolver)
        {
            _tableNameResolver = resolver;
        }

        /// <summary>
        /// Sets the column name resolver.
        /// </summary>
        /// <param name="resolver">The resolver to use when requesting the format of a column name.</param>
        public static void SetColumnNameResolver(IColumnNameResolver resolver)
        {
            _columnNameResolver = resolver;
        }

        public static T Get<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
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

                sb.AppendFormat("{0} = @{1}", GetColumnName(idProps[i], default), idProps[i].Name);
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

            return connection.Query<T>(sb.ToString(), dynParms, transaction, true, commandTimeout).FirstOrDefault();
        }

        public static T FirstOrDefault<T>(this IDbConnection connection)
        {
            string tableName = DapperExtension.GetTableName<T>();
            string sql = $"SELECT  TOP 1 * FROM [{tableName}]";

            return DapperExtension.Connection.QueryFirstOrDefault<T>(sql);
        }

        public static IEnumerable<T> GetList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var sb = new StringBuilder("Select ");
            BuildSelect<T>(sb);
            var tableName = DapperExtension.GetTableName<T>();
            sb.Append($" from {Encapsulate(tableName)}");

            var whereprops = GetAllProperties(whereConditions).ToArray();
            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere<T>(sb, whereprops, whereConditions);
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
            parameters.Add($"@{key}", value);

            return connection.GetList<T>($"where {key} = @{key}", parameters).FirstOrDefault();
        }

        public static IList<T> FindAll<T>(this IDbConnection connection, int value)
        {
            var key = DapperExtension.GetKey<T>();
            var parameters = new DynamicParameters();
            parameters.Add($"@{key}", value);

            return connection.GetList<T>($"where {key} = @{key}", parameters).ToList();
        }

        public static IEnumerable<T> GetList<T>(this IDbConnection connection, string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
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
            var currenttype = typeof(T);
            var idProp = GetIdProperties(currenttype).ToList().FirstOrDefault();

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            sb.AppendFormat("Delete from {0} where ", name);

            string key = DapperExtension.GetKey<T>();

            sb.AppendFormat("{0} = @{1}", GetColumnName(idProp, key), key);

            var dynParms = new DynamicParameters();
            dynParms.Add("@" + idProp.Name, id);

            return connection.Execute(sb.ToString(), dynParms, transaction, commandTimeout);
        }

        public static int DeleteList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_DeleteWhere{whereConditions?.GetType()?.FullName}", sb =>
            {
                var currenttype = typeof(T);
                var tableName = GetTableName<T>();

                var whereprops = GetAllProperties(whereConditions).ToArray();
                sb.AppendFormat("Delete from {0}", tableName);
                if (whereprops.Any())
                {
                    sb.Append(" where ");
                    BuildWhere<T>(sb, whereprops);
                }

                if (Debugger.IsAttached)
                {
                    Trace.WriteLine(string.Format("DeleteList<{0}> {1}", currenttype, sb));
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

                var currenttype = typeof(T);
                var name = GetTableName(currenttype);

                sb.AppendFormat("Delete from {0}", name);
                sb.Append(" " + conditions);

                if (Debugger.IsAttached)
                {
                    Trace.WriteLine(string.Format("DeleteList<{0}> {1}", currenttype, sb));
                }
            });
            return connection.Execute(masterSb.ToString(), parameters, transaction, commandTimeout);
        }

        public static int RecordCount<T>(this IDbConnection connection, string conditions = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var tableName = DapperExtension.GetTableName<T>();
            var sb = new StringBuilder();
            sb.AppendFormat("Select count(1) from {0}", Encapsulate(tableName));
            sb.Append(" " + conditions);

            return connection.ExecuteScalar<int>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        public static int RecordCount<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var tableName = DapperExtension.GetTableName<T>();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            var sb = new StringBuilder();
            sb.AppendFormat("Select count(1) from {0}", Encapsulate(tableName));
            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere<T>(sb, whereprops);
            }

            return connection.ExecuteScalar<int>(sb.ToString(), whereConditions, transaction, commandTimeout);
        }

        // build update statement based on list on an entity
        private static void BuildUpdateSet<T>(StringBuilder stringBuilder)
        {
            var key = DapperExtension.GetKey<T>();
            var nonIdProps = DapperExtension.GetColumns<T>().Where(o => o != key);
            foreach (var item in nonIdProps)
            {
                int index = nonIdProps.IndexOf(item);
                stringBuilder.AppendFormat("{0}{1} = @{2}", index == 0 ? string.Empty : ",", item, DapperExtension.ToProperty(item));
            }
        }

        // build select clause based on list of properties skipping ones with the IgnoreSelect and NotMapped attribute
        private static void BuildSelect(StringBuilder masterSb, IEnumerable<PropertyInfo> props, IList<string> columns = null)
        {
            StringBuilderCache(masterSb, $"{props.CacheKey()}_BuildSelect", sb =>
            {
                var propertyInfos = props as IList<PropertyInfo> ?? props.ToList();
                var addedAny = false;
                for (var i = 0; i < propertyInfos.Count(); i++)
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

                    // if there is a custom column name add an "as customcolumnname" to the item so it maps properly
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
                for (var x = 0; x < sourceProperties.Count(); x++)
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

                if (i < propertyInfos.Count() - 1)
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

        private static void BuildInsertValues<T>(StringBuilder masterSb)
        {
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_BuildInsertValues", sb =>
            {
                var props = GetScaffoldableProperties<T>().ToArray();
                for (var i = 0; i < props.Count(); i++)
                {
                    var property = props.ElementAt(i);
                    var columnName = DapperExtension.GetColumn<T>(property.Name);
                    if (columnName == DapperExtension.GetKey<T>())
                    {
                        continue;
                    }

                    sb.AppendFormat("@{0}", property.Name);
                    if (i < props.Count() - 1)
                    {
                        sb.Append(", ");
                    }
                }

                if (sb.ToString().EndsWith(", "))
                {
                    sb.Remove(sb.Length - 2, 2);
                }
            });
        }

        private static void BuildInsertParameters<T>(StringBuilder stringBuilder)
        {
            StringBuilderCache(stringBuilder, $"{typeof(T).FullName}_BuildInsertParameters", sb =>
            {
                var props = GetScaffoldableProperties<T>().ToArray();

                for (var i = 0; i < props.Count(); i++)
                {
                    var property = props.ElementAt(i);
                    if (property.PropertyType != typeof(Guid) && property.PropertyType != typeof(string)
                          && property.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name)
                          && property.GetCustomAttributes(true).All(attr => attr.GetType().Name != typeof(RequiredAttribute).Name))
                    {
                        continue;
                    }

                    var columnName = DapperExtension.GetColumn<T>(property.Name);
                    if (columnName == DapperExtension.GetKey<T>())
                    {
                        continue;
                    }

                    sb.Append(Encapsulate(columnName));
                    if (i < props.Count() - 1)
                    {
                        sb.Append(", ");
                    }
                }

                if (sb.ToString().EndsWith(", "))
                {
                    sb.Remove(sb.Length - 2, 2);
                }
            });
        }

        // Get all properties in an entity
        private static IEnumerable<PropertyInfo> GetAllProperties<T>(T entity) where T : class
        {
            if (entity == null)
            {
                return new PropertyInfo[0];
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
        private static bool IsReadOnly(PropertyInfo pi)
        {
            var attributes = pi.GetCustomAttributes(false);
            if (attributes.Length > 0)
            {
                dynamic write = attributes.FirstOrDefault(x => x.GetType().Name == typeof(ReadOnlyAttribute).Name);
                if (write != null)
                {
                    return write.IsReadOnly;
                }
            }

            return false;
        }

        private static IEnumerable<PropertyInfo> GetUpdateableProperties<T>(T entity)
        {
            var updateableProperties = GetScaffoldableProperties<T>();

            // remove ones with ID
            updateableProperties = updateableProperties.Where(p => !p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));

            // remove ones with key attribute
            updateableProperties = updateableProperties.Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name) == false);

            // remove ones that are readonly
            updateableProperties = updateableProperties.Where(p => p.GetCustomAttributes(true).Any(attr => (attr.GetType().Name == typeof(ReadOnlyAttribute).Name) && IsReadOnly(p)) == false);

            // remove ones with IgnoreUpdate attribute
            updateableProperties = updateableProperties.Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(IgnoreUpdateAttribute).Name) == false);

            // remove ones that are not mapped
            updateableProperties = updateableProperties.Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(NotMappedAttribute).Name) == false);

            return updateableProperties;
        }

        // Get all properties that are named Id or have the Key attribute
        // For Inserts and updates we have a whole entity so this method is used
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

        private static string GetTableName(object entity)
        {
            var type = entity.GetType();
            return GetTableName(type);
        }

        private static string GetTableName<T>()
        {
            if (TableNames.TryGetValue(typeof(T), out string tableName))
            {
                return tableName;
            }

            tableName = _tableNameResolver.ResolveTableName(typeof(T));
            TableNames.AddOrUpdate(typeof(T), tableName, (t, v) => tableName);

            return tableName;
        }

        private static string GetTableName(Type type)
        {
            if (TableNames.TryGetValue(type, out string tableName))
            {
                return tableName;
            }

            tableName = _tableNameResolver.ResolveTableName(type);

            TableNames.AddOrUpdate(type, tableName, (t, v) => tableName);

            return tableName;
        }

        private static string GetColumnName(PropertyInfo propertyInfo, string name = default)
        {
            string key = string.Format("{0}.{1}", propertyInfo.DeclaringType, propertyInfo.Name);
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
            string key = string.Format("{0}.{1}", typeof(T).DeclaringType, typeof(T).Name);
            if (ColumnNames.TryGetValue(key, out string columnName))
            {
                return columnName;
            }

            columnName = _columnNameResolver.ResolveColumnName<T>(name);
            ColumnNames.AddOrUpdate(key, columnName, (t, v) => columnName);

            return columnName;
        }

        private static string Encapsulate(string databaseword)
        {
            return string.Format("[{0}]", databaseword);
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
    }
}
