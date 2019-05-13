using System.Linq;
using Core.Extension;

namespace Core.Entity
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CoreApiContext context = new CoreApiContext();
            IQueryable<User> q = context.User;
            q = q.AddBooleanFilter(true, o => o.IsEnable);
            var list = q.ToList();
        }
    }
}

// Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer