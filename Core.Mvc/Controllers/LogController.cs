using System.Text;
using Core.Mvc.ViewConfiguration.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers
{
    public class LogController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public LogController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Error()
        {
            LogIndex table = new LogIndex(_hostingEnvironment);
            return Content(table.Render(), "text/html", Encoding.UTF8);
        }

        [HttpPost]
        public IActionResult Search(LogPostModel model)
        {
            LogIndex table = new LogIndex(_hostingEnvironment);
            return Content(table.Render(), "text/html", Encoding.UTF8);
        }
    }
}