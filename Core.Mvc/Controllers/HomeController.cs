using Core.Mvc.Models;
using Core.Web.Dialog;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Core.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //LargeDialog Dialog = new LargeDialog();
            //return Content(Dialog.Render(), "text/html");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
