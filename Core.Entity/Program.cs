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
            for (int i = 0; i < 100000; i++)
            {
                var q1 = context.User.AddStringEqualsFilter("aa", o => o.LoginName);
            }

            sw.Stop();
            long times = sw.ElapsedMilliseconds;

            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            for (int i = 0; i < 100000; i++)
            {
                var q2 = context.User.Where(o => o.LoginName == "aa");
            }
            sw2.Stop();
            long times2 = sw2.ElapsedMilliseconds;

            var e = times - times2;
        }
    }
}