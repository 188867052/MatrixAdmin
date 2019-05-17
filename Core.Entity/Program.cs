using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Generics;
using Core.Extension.ExpressionBuilder.Interfaces;
using Core.Extension.ExpressionBuilder.Operations;
using System.Linq;

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

            filter.AddIntegerBetweenFilter(o => o.Id, 2, 6);
            filter.AddIntegerInArrayFilter(o => o.Id, new[] { 4, 5 });
            filter.AddExistsFilter(o => o.UserRoleMapping, o => o.Id, Operation.EqualTo, 1);

            IFilterInfo filterInfo1 = new IntegerBetweenFilter<User>(o => o.Id, 2, 3);
            IFilterInfo filterInfo2 = new IntegerInArrayFilte<User>(o => o.Id, new[] { 4, 5 });
            IFilter a = new Filter<User>(filterInfo1, filterInfo2, Connector.Or);
            filter.AddComplexFilter(a);

            var filterInfo4 = new CollectionExistsInFilter<User, UserRoleMapping>(o => o.UserRoleMapping, o => o.Id, Operation.EqualTo, 11);
            filter.AddComplexFilter(filterInfo4);
            query = query.Where(filter);
            var ab = query.ToList();
        }
    }
}