using System;
using System.Diagnostics;
using System.Linq;
using Core.Extension;

// Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer
namespace Core.Entity
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CoreApiContext context = new CoreApiContext();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var a = Stopwatch.GetTimestamp();
            var q1 = context.User.AddStringEndsWithFilter("aa", o => o.LoginName);

            sw.Stop();
            long times = sw.ElapsedMilliseconds;
            var aa = q1.ToList();
        }
    }
}