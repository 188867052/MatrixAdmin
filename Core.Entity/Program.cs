using System;
using System.Collections.Generic;
using Core.Extension;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

// Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer
namespace Core.Entity
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CoreApiContext context = new CoreApiContext();
            context.Set<UserRoleMapping>().Load();
            IQueryable<User> query = context.User;

            var q1 = query.AddStringEqualsFilter(",,", o => o.RoleName);

            query = query.Where(o => o.UserRoleMapping.FirstOrDefault(x => x.RoleId == 1) != null);
            //List<User> aa = query.ToList();

            string[] starts = "a,b,c".Split(',');
            var c = query.ToList();
        }
    }
}