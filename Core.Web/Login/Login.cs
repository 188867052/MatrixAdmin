using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Core.Web.Login
{
    public class Login
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Login(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        public string Render()
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, $@"Html\index.html");
            string html = System.IO.File.ReadAllText(path);
            return html;
        }
    }
}
