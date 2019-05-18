using System;
using System.Linq;
using Core.Extension.ExpressionBuilder.Generics;

// Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer
namespace Core.Entity
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Filter<Role> filter = new Filter<Role>();
            CoreApiContext context = new CoreApiContext();
            IQueryable<Role> query = context.Role;

            filter.AddSimpleFilter(new BooleanEqualFilter<Role>(RoleField.CreateByUserField.IsEnable, true));
            filter.AddSimpleFilter(new BooleanEqualFilter<Role>(RoleField.IsEnable, true));

            query = query.Where(filter);
            var ab = query.ToList();
        }
    }
}