using System;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity
{
    public partial class CoreApiContext
    {
        [Obsolete("已过时清使用 Dapper2")]
        public DbConnection Dapper => this.Database.GetDbConnection();

        public static readonly DbConnection Dapper2 = new SqlConnection("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}
