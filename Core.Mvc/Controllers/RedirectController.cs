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

        public IActionResult Index2()
        {
            File File = new File(_hostingEnvironment, "index2");
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

        public IActionResult Buttons()
        {
            File File = new File(_hostingEnvironment, "buttons");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Calendar()
        {
            File File = new File(_hostingEnvironment, "calendar");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Charts()
        {
            File File = new File(_hostingEnvironment, "charts");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Chat()
        {
            File File = new File(_hostingEnvironment, "Chat");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Dashboard()
        {
            File File = new File(_hostingEnvironment, "dashboard");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Error403()
        {
            File File = new File(_hostingEnvironment, "Error403");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Error404()
        {
            File File = new File(_hostingEnvironment, "Error404");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Error405()
        {
            File File = new File(_hostingEnvironment, "Error405");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Error500()
        {
            File File = new File(_hostingEnvironment, "Error500");
            return Content(File.Render(), "text/html");
        }

        public IActionResult FormCommon()
        {
            File File = new File(_hostingEnvironment, "Form-Common");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Formvalidation()
        {
            File File = new File(_hostingEnvironment, "form-validation");
            return Content(File.Render(), "text/html");
        }


        public IActionResult FormWizard()
        {
            File File = new File(_hostingEnvironment, "form-wizard");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Gallery()
        {
            File File = new File(_hostingEnvironment, "gallery");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Grid()
        {
            File File = new File(_hostingEnvironment, "Grid");
            return Content(File.Render(), "text/html");
        }

   
        public IActionResult Interface()
        {
            File File = new File(_hostingEnvironment, "interface");
            return Content(File.Render(), "text/html");
        }

        public IActionResult Invoice()
        {
            File File = new File(_hostingEnvironment, "invoice");
            return Content(File.Render(), "text/html");
        }
    }
}
