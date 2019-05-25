using System.Data.Common;
using System.Data.SqlClient;

namespace Core.Entity
{
    public partial class CoreApiContext
    {
        public static readonly DbConnection Dapper = new SqlConnection("Data Source=HCHENG;Initial Catalog=CoreApi;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True");
    }
}
