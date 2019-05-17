using System;
using System.Linq;
using Core.Extension.ExpressionBuilder.Generics;
using Core.Extension.ExpressionBuilder.Interfaces;

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

            filter.AddExistFilter(new IntegerExistsInFilter<Entity.User, UserRoleMapping>(o => o.UserRoleMapping, new IntegarEqualFilter<UserRoleMapping>(o => o.RoleId, null)));

            query = query.Where(filter);
            var ab = query.ToList();
        }
    }
}