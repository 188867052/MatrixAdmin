using Core.Mvc.Models;
using Core.Web.Dialog;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Core.Web.Table;

namespace Core.Mvc.Controllers
{
    public class TableController : Controller
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            Table table = new Table();
            return Content(table.Render(), "text/html");
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
