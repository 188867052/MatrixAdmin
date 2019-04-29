using System.Diagnostics;
using Core.Model.Log;
using Core.Web.Dialog;
using Core.Web.File;
using Core.Web.Login;
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
