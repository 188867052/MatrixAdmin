using System;
using System.Linq;
using ConsoleApp.DataModels2;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CoreApiContext coreApiContext = new CoreApiContext();
            //coreApiContext.User.Include(o => o.UserStatus);
            var query = coreApiContext.User.AsQueryable();
            //coreApiContext.Set<User>().Include(o => o.UserStatus);
            coreApiContext.Set<UserStatus>().Load();

            query = query.Where(o => o.UserStatus.Name == "正常");
            var list = query.ToList();

            string name = list.First().UserStatus.Name;
            Console.WriteLine("Hello World!");
        }
    }
}
//Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataModels