using Core.Mvc.Models;
using Core.Web.Dialog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            File File = new File(_hostingEnvironment, fileName);
            return Content(File.Render(), "text/html");
        }

        public IActionResult SmallDialog()
        {
            SmallDialog Dialog = new SmallDialog();
            return Content(Dialog.Render(), "text/html");
        }

        public IActionResult Carousel()
        {
            Carousel Dialog = new Carousel();
            return Content(Dialog.Render(), "text/html");
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
