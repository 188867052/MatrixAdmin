using Core.Mvc.Models;
using Core.Web.Dialog;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Core.Mvc.Controllers
{
    public class DialogController : Controller
    {
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
    }
}
