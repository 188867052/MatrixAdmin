using System.Text;
using Core.Models.Entities;
using Core.Mvc.ViewConfiguration.Error;
using Core.Web.Table;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers
{
    public class LogController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public LogController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Error()
        {
            ErrorIndex table = new ErrorIndex(_hostingEnvironment);
            return Content(table.Render(), "text/html",Encoding.UTF8);
        }
    }
}