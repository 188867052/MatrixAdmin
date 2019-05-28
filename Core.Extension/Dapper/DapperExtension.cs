using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Core.Entity;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Core.Extension.Dapper
{
    public static class DapperExtension
    {
        public static Func<TableInfo, string, bool> Predicate = (o, entity) => o.TableName.Replace("_", default).Equals(entity, StringComparison.InvariantCultureIgnoreCase);
        private static IEnumerable<TableInfo> _tables;
        private static IDbConnection _connection;

        static DapperExtension()
        {
            var properties = typeof(CoreApiContext).GetProperties();
            foreach (var property in properties)
            {
                if (property.ToString().Contains(typeof(DbSet<>).FullName))
                {
                    var type = property.PropertyType.GenericTypeArguments[default];
                    SqlMapper.SetTypeMap(type, new ColumnAttributeTypeMapper(type));
                }
            }
        }


        public static IEnumerable<TableInfo> Tables
        {
            get
            {
                if (_tables == null)
                {
                    using (Connection)
                    {
                        string sql = $"SELECT a.name AS [TableName],case e.name when f.column_NAME then 'True'else 'false' end AS IsPrimaryKey,F.COLUMN_NAME AS ColumnName FROM sysobjects AS a LEFT JOIN sysobjects AS b ON a.id=b.parent_obj LEFT JOIN sysindexes AS c ON a.id=c.id AND b.name=c.name LEFT JOIN sysindexkeys AS d ON a.id=d.id AND c.indid=d.indid LEFT JOIN syscolumns AS e ON a.id=e.id AND d.colid=e.colid left join  INFORMATION_SCHEMA.COLUMNS f on a.name=f.TABLE_NAME WHERE a.xtype='U' AND b.xtype='PK'";
                        _tables = DapperExtension.Connection.Query<TableInfo>(sql);
                    }
                }

                return _tables;
            }
        }

        public static IDbConnection Connection
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed)
                {
                    _connection = new SqlConnection("Data Source=.;App=Dapper;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                }

                return _connection;
            }
        }

        public static string ToProperty(string field)
        {
            var array = field.Split('_');
            return array.Aggregate<string, string>(default, (current, item) => current + char.ToUpper(item[0]) + item.Substring(1));
        }

        public static IEnumerable<string> GetColumns<T>()
        {
            return GetTableInfo<T>().Select(x => x.ColumnName).Distinct();
        }

        public static string GetColumn<T>(string propertyName)
        {
            return GetColumns<T>().FirstOrDefault(o => o.Replace("_", string.Empty).Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<TableInfo> GetTableInfo<T>()
        {
            return Tables.Where(o => Predicate(o, typeof(T).Name));
        }

        public static string GetKey<T>()
        {
            return GetTableInfo<T>().First(o => o.IsPrimaryKey).ColumnName;
        }

        public static bool HasMultipleKey<T>()
        {
            return GetTableInfo<T>().Count(o => o.IsPrimaryKey) > 1;
        }

        public static string GetTableName<T>()
        {
            return GetTableInfo<T>().First(o => o.IsPrimaryKey).TableName;
        }

        public class TableInfo
        {
            public string TableName { get; set; }

            public string ColumnName { get; set; }

            public bool IsPrimaryKey { get; set; }
        }
    }
}