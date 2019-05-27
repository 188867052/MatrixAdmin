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

namespace Dapper
{
    /// <summary>
    /// Main class for Dapper.SimpleCRUD extensions.
    /// </summary>
    public static partial class SimpleCRUD
    {
        static SimpleCRUD()
        {
            SetDialect(_dialect);
            DapperExtension.SetTypeMap();
        }

        private static Dialect _dialect = Dialect.SQLServer;
        private static string _encapsulation;
        private static string _getIdentitySql;
        private static string _getPagedListSql;
        private static readonly ConcurrentDictionary<Type, string> TableNames = new ConcurrentDictionary<Type, string>();
        private static readonly ConcurrentDictionary<string, string> ColumnNames = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, string> StringBuilderCacheDict = new ConcurrentDictionary<string, string>();
        private static bool stringBuilderCacheEnabled = true;
        private static ITableNameResolver _tableNameResolver = new TableNameResolver();
        private static IColumnNameResolver _columnNameResolver = new ColumnNameResolver();

        /// <summary>
        /// Append a Cached version of a strinbBuilderAction result based on a cacheKey.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="cacheKey"></param>
        /// <param name="stringBuilderAction"></param>
        private static void StringBuilderCache(StringBuilder sb, string cacheKey, Action<StringBuilder> stringBuilderAction)
        {
            if (stringBuilderCacheEnabled && StringBuilderCacheDict.TryGetValue(cacheKey, out string value))
            {
                sb.Append(value);
                return;
            }

            StringBuilder newSb = new StringBuilder();
            stringBuilderAction(newSb);
            value = newSb.ToString();
            StringBuilderCacheDict.AddOrUpdate(cacheKey, value, (t, v) => value);
            sb.Append(value);
        }

        /// <summary>
        /// Returns the current dialect name.
        /// </summary>
        /// <returns></returns>
        public static string GetDialect()
        {
            return _dialect.ToString();
        }

        /// <summary>
        /// Sets the database dialect.
        /// </summary>
        /// <param name="dialect"></param>
        public static void SetDialect(Dialect dialect)
        {
            switch (dialect)
            {
                case Dialect.PostgreSQL:
                    _dialect = Dialect.PostgreSQL;
                    _encapsulation = "\"{0}\"";
                    _getIdentitySql = string.Format("SELECT LASTVAL() AS id");
                    _getPagedListSql = "Select {SelectColumns} from {TableName} {WhereClause} Order By {OrderBy} LIMIT {RowsPerPage} OFFSET (({PageNumber}-1) * {RowsPerPage})";
                    break;
                case Dialect.SQLite:
                    _dialect = Dialect.SQLite;
                    _encapsulation = "\"{0}\"";
                    _getIdentitySql = string.Format("SELECT LAST_INSERT_ROWID() AS id");
                    _getPagedListSql = "Select {SelectColumns} from {TableName} {WhereClause} Order By {OrderBy} LIMIT {RowsPerPage} OFFSET (({PageNumber}-1) * {RowsPerPage})";
                    break;
                case Dialect.MySQL:
                    _dialect = Dialect.MySQL;
                    _encapsulation = "`{0}`";
                    _getIdentitySql = string.Format("SELECT LAST_INSERT_ID() AS id");
                    _getPagedListSql = "Select {SelectColumns} from {TableName} {WhereClause} Order By {OrderBy} LIMIT {Offset},{RowsPerPage}";
                    break;
                default:
                    _dialect = Dialect.SQLServer;
                    _encapsulation = "[{0}]";
                    _getIdentitySql = string.Format("SELECT CAST(SCOPE_IDENTITY()  AS BIGINT) AS [id]");
                    _getPagedListSql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {OrderBy}) AS PagedNumber, {SelectColumns} FROM {TableName} {WhereClause}) AS u WHERE PagedNumber BETWEEN (({PageNumber}-1) * {RowsPerPage} + 1) AND ({PageNumber} * {RowsPerPage})";
                    break;
            }
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

        public static IEnumerable<T> GetList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");

            IList<string> columns = DapperExtension.MyProperty.Where(o => o.TableName.Replace("_", default).Equals(currenttype.Name, StringComparison.InvariantCultureIgnoreCase)).Select(o => o.ColumnName).ToList();

            // create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties<T>().ToArray(), columns);
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())
            {
                sb.Append(" where ");
                BuildWhere<T>(sb, whereprops, whereConditions);
            }

            return connection.Query<T>(sb.ToString(), whereConditions, transaction, true, commandTimeout);
        }

        public static IList<T> FindAll<T>(this IDbConnection connection, Expression<Func<T, int>> expression, int value)
        {
            string name = expression.GetPropertyName();
            string where = $"where {name} = @{name}";
            var dynParms = new DynamicParameters();
            dynParms.Add("@" + name, value);

            return connection.GetList<T>(where, dynParms).ToList();
        }

        public static IEnumerable<T> GetList<T>(this IDbConnection connection, string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
            {
                throw new ArgumentException("Entity must have at least one [Key] property");
            }

            IList<string> columns = DapperExtension.MyProperty.Where(o => o.TableName.Replace("_", default).Equals(currenttype.Name, StringComparison.InvariantCultureIgnoreCase)).Select(o => o.ColumnName).ToList();

            var name = GetTableName(currenttype);
            var sb = new StringBuilder();
            sb.Append("Select ");

            // create a new empty instance of the type to get the base properties
            SimpleCRUD.BuildSelect(sb, GetScaffoldableProperties<T>().ToArray(), columns);
            sb.AppendFormat(" from {0}", name);

            sb.Append(" " + conditions);

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("GetList<{0}>: {1}", currenttype, sb));
            }

            return connection.Query<T>(sb.ToString(), parameters, transaction, true, commandTimeout);
        }

        /// <summary>
        /// <para>By default queries the table matching the class name.</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")].</para>
        /// <para>Returns a list of all entities.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <returns>Gets a list of all entities.</returns>
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

            var currenttype = typeof(T);
            IList<string> columns = DapperExtension.MyProperty.Where(o => o.TableName.Replace("_", default).Equals(currenttype.Name, StringComparison.InvariantCultureIgnoreCase)).Select(o => o.ColumnName).ToList();

            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
            {
                throw new ArgumentException("Entity must have at least one [Key] property");
            }

            var name = GetTableName(currenttype);
            var sb = new StringBuilder();
            var query = _getPagedListSql;
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = columns.First();
            }

            // create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties<T>().ToArray(), columns);
            query = query.Replace("{SelectColumns}", sb.ToString());
            query = query.Replace("{TableName}", name);
            query = query.Replace("{PageNumber}", pageNumber.ToString());
            query = query.Replace("{RowsPerPage}", rowsPerPage.ToString());
            query = query.Replace("{OrderBy}", orderby);
            query = query.Replace("{WhereClause}", conditions);
            query = query.Replace("{Offset}", ((pageNumber - 1) * rowsPerPage).ToString());

            return connection.Query<T>(query, parameters, transaction, true, commandTimeout);
        }

        /// <summary>
        /// <para>Inserts a row into the database.</para>
        /// <para>By default inserts into the table matching the class name.</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")].</para>
        /// <para>Insert filters out Id column and any columns with the [Key] attribute.</para>
        /// <para>Properties marked with attribute [Editable(false)] and complex types are ignored.</para>
        /// <para>Supports transaction and command timeout.</para>
        /// <para>Returns the ID (primary key) of the newly inserted record if it is identity using the int? type, otherwise null.</para>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entityToInsert"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The ID (primary key) of the newly inserted record if it is identity using the int? type, otherwise null.</returns>
        public static int? Insert<TEntity>(this IDbConnection connection, TEntity entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Insert<int?, TEntity>(connection, entityToInsert, transaction, commandTimeout);
        }

        public static TKey Insert<TKey, TEntity>(this IDbConnection connection, TEntity entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var idProps = GetIdProperties(entityToInsert).ToList();

            if (!idProps.Any())
            {
                throw new ArgumentException("Insert<T> only supports an entity with a [Key] or Id property");
            }

            var keyHasPredefinedValue = false;
            var baseType = typeof(TKey);
            var underlyingType = Nullable.GetUnderlyingType(baseType);
            var keytype = underlyingType ?? baseType;
            if (keytype != typeof(int) && keytype != typeof(uint) && keytype != typeof(long) && keytype != typeof(ulong) && keytype != typeof(short) && keytype != typeof(ushort) && keytype != typeof(Guid) && keytype != typeof(string))
            {
                throw new Exception("Invalid return type");
            }

            var name = GetTableName(entityToInsert);
            var sb = new StringBuilder();
            sb.AppendFormat("insert into {0}", name);
            sb.Append(" (");
            BuildInsertParameters<TEntity>(sb);
            sb.Append(") ");
            sb.Append("values");
            sb.Append(" (");
            BuildInsertValues<TEntity>(sb);
            sb.Append(")");

            if (keytype == typeof(Guid))
            {
                var guidvalue = (Guid)idProps.First().GetValue(entityToInsert, null);
                if (guidvalue == Guid.Empty)
                {
                    var newguid = SequentialGuid();
                    idProps.First().SetValue(entityToInsert, newguid, null);
                }
                else
                {
                    keyHasPredefinedValue = true;
                }

                sb.Append(";select '" + idProps.First().GetValue(entityToInsert, null) + "' as id");
            }

            if ((keytype == typeof(int) || keytype == typeof(long)) && Convert.ToInt64(idProps.First().GetValue(entityToInsert, null)) == 0)
            {
                sb.Append(";" + _getIdentitySql);
            }
            else
            {
                keyHasPredefinedValue = true;
            }

            if (Debugger.IsAttached)
            {
                Trace.WriteLine(string.Format("Insert: {0}", sb));
            }

            var r = connection.Query(sb.ToString(), entityToInsert, transaction, true, commandTimeout);

            if (keytype == typeof(Guid) || keyHasPredefinedValue)
            {
                return (TKey)idProps.First().GetValue(entityToInsert, null);
            }

            return (TKey)r.First().id;
        }

        public static int Update<T>(this IDbConnection connection, T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_Update", sb =>
            {
                var idProps = GetIdProperties(entityToUpdate).ToList();

                if (!idProps.Any())
                {
                    throw new ArgumentException("Entity must have at least one [Key] or Id property");
                }

                var name = GetTableName(entityToUpdate);

                sb.AppendFormat("update {0}", name);

                sb.AppendFormat(" set ");
                BuildUpdateSet(entityToUpdate, sb);
                sb.Append(" where ");
                BuildWhere<T>(sb, idProps, entityToUpdate);

                if (Debugger.IsAttached)
                {
                    Trace.WriteLine(string.Format("Update: {0}", sb));
                }
            });
            return connection.Execute(masterSb.ToString(), entityToUpdate, transaction, commandTimeout);
        }

        public static int Delete<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_Delete", sb =>
            {
                var idProps = GetIdProperties(entityToDelete).ToList();

                if (!idProps.Any())
                {
                    throw new ArgumentException("Entity must have at least one [Key] or Id property");
                }

                var name = GetTableName(entityToDelete);

                sb.AppendFormat("delete from {0}", name);

                sb.Append(" where ");
                BuildWhere<T>(sb, idProps, entityToDelete);

                if (Debugger.IsAttached)
                {
                    Trace.WriteLine(string.Format("Delete: {0}", sb));
                }
            });
            return connection.Execute(masterSb.ToString(), entityToDelete, transaction, commandTimeout);
        }

        public static int Delete<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProp = GetIdProperties(currenttype).ToList().FirstOrDefault();

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            sb.AppendFormat("Delete from {0} where ", name);

            IList<string> columns = DapperExtension.MyProperty.Where(o => o.TableName.Replace("_", default).Equals(currenttype.Name, StringComparison.InvariantCultureIgnoreCase)).Select(o => o.ColumnName).ToList();

            sb.AppendFormat("{0} = @{1}", GetColumnName(idProp, columns[0]), columns[0]);

            var dynParms = new DynamicParameters();
            dynParms.Add("@" + idProp.Name, id);

            return connection.Execute(sb.ToString(), dynParms, transaction, commandTimeout);
        }

        /// <summary>
        /// <para>Deletes a list of records in the database.</para>
        /// <para>By default deletes records in the table matching the class name.</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")].</para>
        /// <para>Deletes records where that match the where clause.</para>
        /// <para>whereConditions is an anonymous type to filter the results ex: new {Category = 1, SubCategory=2}.</para>
        /// <para>The number of records affected.</para>
        /// <para>Supports transaction and command timeout.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="whereConditions"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected.</returns>
        public static int DeleteList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_DeleteWhere{whereConditions?.GetType()?.FullName}", sb =>
            {
                var currenttype = typeof(T);
                var name = GetTableName(currenttype);

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
            });
            return connection.Execute(masterSb.ToString(), whereConditions, transaction, commandTimeout);
        }

        /// <summary>
        /// <para>Deletes a list of records in the database.</para>
        /// <para>By default deletes records in the table matching the class name.</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")].</para>
        /// <para>Deletes records where that match the where clause.</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age".</para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }.</para>
        /// <para>Supports transaction and command timeout.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected.</returns>
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

        /// <summary>
        /// <para>By default queries the table matching the class name.</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")].</para>
        /// <para>Returns a number of records entity by a single id from table T.</para>
        /// <para>Supports transaction and command timeout.</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age" - not required. </para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Returns a count of records.</returns>
        public static int RecordCount<T>(this IDbConnection connection, string conditions = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
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

            return connection.ExecuteScalar<int>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// <para>By default queries the table matching the class name.</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")].</para>
        /// <para>Returns a number of records entity by a single id from table T.</para>
        /// <para>Supports transaction and command timeout.</para>
        /// <para>whereConditions is an anonymous type to filter the results ex: new {Category = 1, SubCategory=2}.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="whereConditions"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Returns a count of records.</returns>
        public static int RecordCount<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
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

            return connection.ExecuteScalar<int>(sb.ToString(), whereConditions, transaction, commandTimeout);
        }

        // build update statement based on list on an entity
        private static void BuildUpdateSet<T>(T entityToUpdate, StringBuilder masterSb)
        {
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_BuildUpdateSet", sb =>
            {
                var nonIdProps = GetUpdateableProperties(entityToUpdate).ToArray();

                for (var i = 0; i < nonIdProps.Length; i++)
                {
                    var property = nonIdProps[i];

                    sb.AppendFormat("{0} = @{1}", GetColumnName(property, default), property.Name);
                    if (i < nonIdProps.Length - 1)
                    {
                        sb.AppendFormat(", ");
                    }
                }
            });
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

        private static void BuildWhere<TEntity>(StringBuilder sb, IEnumerable<PropertyInfo> idProps, object whereConditions = null)
        {
            var propertyInfos = idProps.ToArray();
            for (var i = 0; i < propertyInfos.Count(); i++)
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

        // build insert values which include all properties in the class that are:
        // Not named Id
        // Not marked with the Editable(false) attribute
        // Not marked with the [Key] attribute (without required attribute)
        // Not marked with [IgnoreInsert]
        // Not marked with [NotMapped]
        private static void BuildInsertValues<T>(StringBuilder masterSb)
        {
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_BuildInsertValues", sb =>
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

                    if (property.GetCustomAttributes(true).Any(attr =>
                        attr.GetType().Name == typeof(IgnoreInsertAttribute).Name ||
                        attr.GetType().Name == typeof(NotMappedAttribute).Name ||
                        attr.GetType().Name == typeof(ReadOnlyAttribute).Name && IsReadOnly(property))
                    )
                    {
                        continue;
                    }

                    if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) && property.GetCustomAttributes(true).All(attr => attr.GetType().Name != typeof(RequiredAttribute).Name) && property.PropertyType != typeof(Guid))
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

        // build insert parameters which include all properties in the class that are not:
        // marked with the Editable(false) attribute
        // marked with the [Key] attribute
        // marked with [IgnoreInsert]
        // named Id
        // marked with [NotMapped]
        private static void BuildInsertParameters<T>(StringBuilder masterSb)
        {
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_BuildInsertParameters", sb =>
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

                    if (property.GetCustomAttributes(true).Any(attr =>
                        attr.GetType().Name == typeof(IgnoreInsertAttribute).Name ||
                        attr.GetType().Name == typeof(NotMappedAttribute).Name ||
                        attr.GetType().Name == typeof(ReadOnlyAttribute).Name && IsReadOnly(property)))
                    {
                        continue;
                    }

                    if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) && property.GetCustomAttributes(true).All(attr => attr.GetType().Name != typeof(RequiredAttribute).Name) && property.PropertyType != typeof(Guid))
                    {
                        continue;
                    }

                    sb.Append(GetColumnName(property));
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

        // Get all properties that are:
        // Not named Id
        // Not marked with the Key attribute
        // Not marked ReadOnly
        // Not marked IgnoreInsert
        // Not marked NotMapped
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

        // Gets the table name for this entity
        // For Inserts and updates we have a whole entity so this method is used
        // Uses class name by default and overrides if the class has a Table attribute
        private static string GetTableName(object entity)
        {
            var type = entity.GetType();
            return GetTableName(type);
        }

        // Gets the table name for this type
        // For Get(id) and Delete(id) we don't have an entity, just the type so this method is used
        // Use dynamic type to be able to handle both our Table-attribute and the DataAnnotation
        // Uses class name by default and overrides if the class has a Table attribute
        private static string GetTableName(Type type)
        {
            string tableName;

            if (TableNames.TryGetValue(type, out tableName))
            {
                return tableName;
            }

            tableName = _tableNameResolver.ResolveTableName(type);

            TableNames.AddOrUpdate(type, tableName, (t, v) => tableName);

            return tableName;
        }

        private static string GetColumnName(PropertyInfo propertyInfo, string name = default)
        {
            string columnName;
            string key = string.Format("{0}.{1}", propertyInfo.DeclaringType, propertyInfo.Name);

            if (ColumnNames.TryGetValue(key, out columnName))
            {
                return columnName;
            }

            columnName = _columnNameResolver.ResolveColumnName(propertyInfo, name);

            ColumnNames.AddOrUpdate(key, columnName, (t, v) => columnName);

            return columnName;
        }

        private static string Encapsulate(string databaseword)
        {
            return string.Format(_encapsulation, databaseword);
        }

        /// <summary>
        /// Generates a GUID based on the current date/time
        /// http://stackoverflow.com/questions/1752004/sequential-guid-generator-c-sharp.
        /// </summary>
        /// <returns></returns>
        public static Guid SequentialGuid()
        {
            var tempGuid = Guid.NewGuid();
            var bytes = tempGuid.ToByteArray();
            var time = DateTime.Now;
            bytes[3] = (byte)time.Year;
            bytes[2] = (byte)time.Month;
            bytes[1] = (byte)time.Day;
            bytes[0] = (byte)time.Hour;
            bytes[5] = (byte)time.Minute;
            bytes[4] = (byte)time.Second;
            return new Guid(bytes);
        }

        public interface ITableNameResolver
        {
            string ResolveTableName(Type type);
        }

        public interface IColumnNameResolver
        {
            string ResolveColumnName(PropertyInfo propertyInfo, string name = default);
        }
    }
}
