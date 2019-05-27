using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Core.Entity;
using Dapper;

namespace Core.Extension.Dapper
{
    public static class DapperExtension
    {
        private static IEnumerable<InformationSchema> _informationSchema;
        private static IDbConnection _connection;

        static DapperExtension()
        {
            DapperExtension.SetTypeMap();
        }

        public static IEnumerable<InformationSchema> InformationSchemas
        {
            get
            {
                if (_informationSchema == null)
                {
                    using (Connection)
                    {
                        _informationSchema = Connection.Query<InformationSchema>("SELECT * FROM INFORMATION_SCHEMA.COLUMNS");
                    }
                }

                return _informationSchema;
            }
        }

        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        public static void SetTypeMap()
        {
            SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
            SqlMapper.SetTypeMap(typeof(Role), new ColumnAttributeTypeMapper<Role>());
            SqlMapper.SetTypeMap(typeof(Menu), new ColumnAttributeTypeMapper<Menu>());
            SqlMapper.SetTypeMap(typeof(Permission), new ColumnAttributeTypeMapper<Permission>());
            SqlMapper.SetTypeMap(typeof(Icon), new ColumnAttributeTypeMapper<Icon>());
            SqlMapper.SetTypeMap(typeof(Log), new ColumnAttributeTypeMapper<Log>());
            SqlMapper.SetTypeMap(typeof(InformationSchema), new ColumnAttributeTypeMapper<InformationSchema>());
        }

        public class InformationSchema
        {
            public string TableName { get; set; }

            public string ColumnName { get; set; }
        }
    }
}