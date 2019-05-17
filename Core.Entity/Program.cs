using System;
using System.Linq;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Generics;
using Core.Extension.ExpressionBuilder.Interfaces;
using Core.Extension.ExpressionBuilder.Operations;

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

            IFilterInfo filterInfo1 = new IntegarEqualFilter<User>(o => o.Id, 3);
            //IFilterInfo filterInfo2 = new IntegerInArrayFilte<User>(o => o.Id, new[] { 4, 5 });
            //IFilter a = new Filter<User>(filterInfo1, filterInfo2, Connector.Or);
            //filter.AddComplexFilter(a);

            //var filterInfo4 = new DateTimeExistsInFilter<User, UserRoleMapping>(o => o.UserRoleMapping, o => o.CreateTime, Operation.EqualTo, DateTime.Now);
            //filter.AddExistFilter(filterInfo4);
            filter.AddSimpleFilter(filterInfo1);
            query = query.Where(filter);
            var ab = query.ToList();
        }
    }
}