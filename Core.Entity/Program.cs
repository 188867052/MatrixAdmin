using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Generics;
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

            filter.AddIntegerBetweenFilter(o => o.Id, 2, 6).And.AddIntegerInArrayFilter<User>(o => o.Id, new[] { 4, 5 });
            filter.AddExistsFilter(o => o.UserRoleMapping, o => o.Id, Operation.EqualTo, 1);
            query = query.Where(filter);
            var a = query.ToList();
        }
    }
}