using Core.Web.Dialog;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Core.Model.Log;
using Core.Web.File;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.Controllers
{
    public class DialogController : StandardController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public DialogController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public IActionResult Index()
        {
            LargeDialog largeDialog = new LargeDialog();
            return Content(largeDialog.Render(), "text/html");
        }

        public IActionResult SmallDialog()
        {
            SmallDialog smallDialog = new SmallDialog();
            return Content(smallDialog.Render(), "text/html");
        }

        public IActionResult Carousel()
        {
            Carousel carousel = new Carousel();
            return Content(carousel.Render(), "text/html");
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

        //https://localhost:44317/Dialog/FormInLine
        public IActionResult Dialog()
        {
            File file = new File(HostingEnvironment, "a_test");
            return Content(file.Render(), "text/html");
        }
    }
}
