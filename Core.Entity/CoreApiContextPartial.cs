using System.Data.Common;
using System.Data.SqlClient;

namespace Core.Entity
{
    public partial class CoreApiContext
    {
        public static readonly DbConnection Dapper = new SqlConnection("Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}
