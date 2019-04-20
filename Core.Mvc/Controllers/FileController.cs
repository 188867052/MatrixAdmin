using Core.Mvc.Models;
using Core.Web.Dialog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Core.Web.File;

namespace Core.Mvc.Controllers
{
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "index";
            }
            File file = new File(_hostingEnvironment, fileName);
            return Content(file.Render(), "text/html");
        }

        public IActionResult SmallDialog()
        {
            SmallDialog dialog = new SmallDialog();
            return Content(dialog.Render(), "text/html");
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
