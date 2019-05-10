using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CoreApiContext coreApiContext = new CoreApiContext();

            // coreApiContext.User.Include(o => o.UserStatus);
            // var query = coreApiContext.User.AsQueryable();
            // coreApiContext.Set<User>().Include(o => o.UserStatus);
            coreApiContext.Set<UserStatus>().Load();

            // query = query.Where(o => o.Id == 1);
            // coreApiContext.User.Include(o => o.UserRoleMapping);
            coreApiContext.Set<UserRoleMapping>().Load();
            User user = coreApiContext.User.Find(1);
            var userRoleMapping = user.UserRoleMapping.FirstOrDefault(o => o.UserId == 1);

            // var list = query.ToList();
            userRoleMapping.RoleId = 2;
            userRoleMapping.CreateTime = DateTime.Now;
            coreApiContext.SaveChanges();
        }
    }
}

// Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir .