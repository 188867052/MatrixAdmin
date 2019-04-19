using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Core.Web.Dialog
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
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, @"Html\Index.html");
            string html= System.IO.File.ReadAllText(path);
            return html;
        }
    }
}
