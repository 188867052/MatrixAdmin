using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Core.Entity;
using Dapper;

namespace Core.Extension.Dapper
{
    public static class DapperExtension
    {
        private static IEnumerable<InformationSchema> _myProperty;
        private static DbConnection _connection;

        static DapperExtension()
        {
            SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
            SqlMapper.SetTypeMap(typeof(Role), new ColumnAttributeTypeMapper<Role>());
            SqlMapper.SetTypeMap(typeof(Menu), new ColumnAttributeTypeMapper<Menu>());
            SqlMapper.SetTypeMap(typeof(Permission), new ColumnAttributeTypeMapper<Permission>());
            SqlMapper.SetTypeMap(typeof(Icon), new ColumnAttributeTypeMapper<Icon>());
            SqlMapper.SetTypeMap(typeof(Log), new ColumnAttributeTypeMapper<Log>());
            SqlMapper.SetTypeMap(typeof(InformationSchema), new ColumnAttributeTypeMapper<InformationSchema>());
        }

        public static IEnumerable<InformationSchema> MyProperty
        {
            get
            {
                if (_myProperty == null)
                {
                    _myProperty = Connection.Query<InformationSchema>("SELECT * FROM INFORMATION_SCHEMA.COLUMNS");
                }

                return _myProperty;
            }
        }

        public static DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                }

                return _connection;
            }
        }

        public class InformationSchema
        {
            public string TableName { get; set; }

            public string ColumnName { get; set; }
        }
    }
}