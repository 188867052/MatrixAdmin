using System.Linq;
using Core.Extension;
using Core.Extension.ExpressionBuilder.Generics;

// Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer
namespace Core.Entity
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Filter<User> filter = new Filter<User>();
            CoreApiContext context = new CoreApiContext();
            IQueryable<User> query = context.User;
            query = query.AddFilter(1, o => o.UserStatus.Id == 1);

            var ab = query.ToList();
        }
    }
}