using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Core.Entity;
using Dapper;
using Microsoft.EntityFrameworkCore;

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
                        _informationSchema = DapperExtension.Connection.Query<InformationSchema>("SELECT * FROM INFORMATION_SCHEMA.COLUMNS");
                    }
                }

                return _informationSchema;
            }
        }

        public static IDbConnection Connection
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed)
                {
                    _connection = new SqlConnection("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                }

                return _connection;
            }
        }

        private static void SetTypeMap()
        {
            var pro = typeof(CoreApiContext).GetProperties();
            foreach (var type in pro)
            {
                if (type.ToString().Contains(typeof(DbSet<>).FullName))
                {
                    var type2 = type.PropertyType.GenericTypeArguments[default];
                    SqlMapper.SetTypeMap(type2, new ColumnAttributeTypeMapper(type2));
                }
            }

            SqlMapper.SetTypeMap(typeof(InformationSchema), new ColumnAttributeTypeMapper(typeof(InformationSchema)));
        }

        public class InformationSchema
        {
            public string TableName { get; set; }

            public string ColumnName { get; set; }
        }
    }
}