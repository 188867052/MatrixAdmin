using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Core.Web.File
{
    public class File
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _fileName;

        public File(IHostingEnvironment hostingEnvironment, string fileName)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._fileName = fileName;
        }

        public string Render()
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, $@"Html\{this._fileName}.html");
            string html = System.IO.File.ReadAllText(path);
            return html;
        }
    }
}
