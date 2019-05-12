using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Core.Api
{
    /// <summary>
    /// Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 应用程序启动入口方法(Main).
        /// </summary>
        /// <param name="args">args.</param>
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();
            host.Run();
        }

        /// <summary>
        /// CreateWebHostBuilder.
        /// </summary>
        /// <param name="args">args.</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(c => c.AddServerHeader = false)
                .UseStartup<Startup>();
    }
}
