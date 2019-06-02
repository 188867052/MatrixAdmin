using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

// Scaffold-DbContext -Force "Data Source=.;App=EntityFrameworkCore;Initial Catalog=Core;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer
namespace Core.Entity
{
    public partial class CoreContext
    {
        public static readonly DbConnection Dapper = new SqlConnection("Data Source=.;Initial Catalog=Core;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}
