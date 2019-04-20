using Core.Web.Dialog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers
{
    public class RedirectController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public RedirectController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            File File = new File(_hostingEnvironment, "index");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Dashboard()
        {
            File File = new File(_hostingEnvironment, "dashboard");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Tables()
        {
            File File = new File(_hostingEnvironment, "tables");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Login()
        {
            File File = new File(_hostingEnvironment, "Login");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Widgets()
        {
            File File = new File(_hostingEnvironment, "widgets");
            return Content(File.Render(), "text/html");
        }
    }
}
