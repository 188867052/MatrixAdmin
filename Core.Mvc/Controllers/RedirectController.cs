using Core.Mvc.ViewConfigurations.Table;
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
            Index index = new Index(this._hostingEnvironment);
            return Content(index.Render(), "text/html");
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
            ViewConfigurations.Table.Login login = new ViewConfigurations.Table.Login(_hostingEnvironment);
            return Content(login.Render(), "text/html");
        }

        public IActionResult Widgets()
        {
            Widget widget = new Widget(_hostingEnvironment);
            return Content(widget.Render(), "text/html");
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
            Error Error = new Error(_hostingEnvironment, 403);
            return Content(Error.Render(), "text/html");
        }

        public IActionResult Error404()
        {
            Error Error = new Error(_hostingEnvironment, 404);
            return Content(Error.Render(), "text/html");
        }

        public IActionResult Error405()
        {
            Error Error = new Error(_hostingEnvironment, 405);
            return Content(Error.Render(), "text/html");
        }

        public IActionResult Error500()
        {
            Error Error = new Error(_hostingEnvironment, 500);
            return Content(Error.Render(), "text/html");
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
