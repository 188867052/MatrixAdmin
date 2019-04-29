using System.Diagnostics;
using Core.Model.Log;
using Core.Web.Dialog;
using Core.Web.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas
{
    public class FileController : StandardController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public FileController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        [HttpGet]
        public IActionResult Index(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "index";
            }
            File file = new File(HostingEnvironment, fileName);
            return Content(file.Render(), "text/html");
        }

        public IActionResult Carousel()
        {
            Carousel dialog = new Carousel();
            return Content(dialog.Render(), "text/html");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
