using System.Diagnostics;
using Core.Mvc.Models;
using Core.Web.Dialog;
using Core.Web.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers.Administration
{
    [Area(nameof(Administration))]
    public class LoginController : StandardController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>

        public LoginController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public IActionResult Index()
        {
            Login login = new Login(this.HostingEnvironment);
            return Content(login.Render(), "text/html");
        }

        public IActionResult File(string fileName)
        {
            File file = new File(HostingEnvironment, fileName);
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
