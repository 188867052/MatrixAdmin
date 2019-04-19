using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Core.Web.Dialog
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
