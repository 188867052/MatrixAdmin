using Core.Mvc.Models;
using Core.Web.Dialog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Core.Mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public LoginController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            Login Table = new Login(this._hostingEnvironment);
            return Content(Table.Render(), "text/html");
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
