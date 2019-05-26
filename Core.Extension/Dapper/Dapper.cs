using System.Data.Common;
using System.Data.SqlClient;
using Core.Entity;
using Dapper;

namespace Core.Extension.Dapper
{
    public static class Dapper
    {
        public static readonly DbConnection Connection = new SqlConnection("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        static Dapper()
        {
            SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
            SqlMapper.SetTypeMap(typeof(Role), new ColumnAttributeTypeMapper<Role>());
            SqlMapper.SetTypeMap(typeof(Menu), new ColumnAttributeTypeMapper<Menu>());
            SqlMapper.SetTypeMap(typeof(Permission), new ColumnAttributeTypeMapper<Permission>());
            SqlMapper.SetTypeMap(typeof(Icon), new ColumnAttributeTypeMapper<Icon>());
            SqlMapper.SetTypeMap(typeof(Log), new ColumnAttributeTypeMapper<Log>());
        }
    }
}