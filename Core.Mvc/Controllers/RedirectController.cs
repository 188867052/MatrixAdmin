using Core.Mvc.ViewConfiguration.Button;
using Core.Mvc.ViewConfiguration.Chart;
using Core.Mvc.ViewConfiguration.Error;
using Core.Mvc.ViewConfiguration.Form;
using Core.Mvc.ViewConfiguration.Grid;
using Core.Mvc.ViewConfiguration.Index;
using Core.Mvc.ViewConfiguration.Table;
using Core.Mvc.ViewConfiguration.Widget;
using Core.Mvc.ViewConfigurations.Table;
using Core.Web.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Login = Core.Mvc.ViewConfiguration.Login.Login;

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
            File file = new File(_hostingEnvironment, "index2");
            return Content(file.Render(), "text/html");
        }


        public IActionResult Tables()
        {
            Table table = new Table(_hostingEnvironment);
            return Content(table.Render(), "text/html");
        }

        public IActionResult Login()
        {
            Login login = new Login(_hostingEnvironment);
            return Content(login.Render(), "text/html");
        }

        public IActionResult Widgets()
        {
            Widget widget = new Widget(_hostingEnvironment);
            return Content(widget.Render(), "text/html");
        }

        public IActionResult Buttons()
        {
            Button button = new Button(_hostingEnvironment);
            return Content(button.Render(), "text/html");
        }

        public IActionResult Calendar()
        {
            File file = new File(_hostingEnvironment, "calendar");
            return Content(file.Render(), "text/html");
        }

        public IActionResult Charts()
        {
            Chart button = new Chart(_hostingEnvironment);
            return Content(button.Render(), "text/html");
        }

        public IActionResult Chat()
        {
            File file = new File(_hostingEnvironment, "Chat");
            return Content(file.Render(), "text/html");
        }

        public IActionResult Dashboard()
        {
            File file = new File(_hostingEnvironment, "dashboard");
            return Content(file.Render(), "text/html");
        }

        public IActionResult Error403()
        {
            Error error = new Error(_hostingEnvironment, 403);
            return Content(error.Render(), "text/html");
        }

        public IActionResult Error404()
        {
            Error error = new Error(_hostingEnvironment, 404);
            return Content(error.Render(), "text/html");
        }

        public IActionResult Error405()
        {
            Error error = new Error(_hostingEnvironment, 405);
            return Content(error.Render(), "text/html");
        }

        public IActionResult Error500()
        {
            Error error = new Error(_hostingEnvironment, 500);
            return Content(error.Render(), "text/html");
        }

        public IActionResult FormCommon()
        {
            BasicForm basicForm = new BasicForm(_hostingEnvironment);
            return Content(basicForm.Render(), "text/html");
        }

        public IActionResult FormValidation()
        {
            FormValidation basicForm = new FormValidation(_hostingEnvironment);
            return Content(basicForm.Render(), "text/html");
        }


        public IActionResult FormWizard()
        {
            FormWizard basicForm = new FormWizard(_hostingEnvironment);
            return Content(basicForm.Render(), "text/html");
        }

        public IActionResult Gallery()
        {
            File file = new File(_hostingEnvironment, "gallery");
            return Content(file.Render(), "text/html");
        }

        public IActionResult Grid()
        {
            Grid file = new Grid(_hostingEnvironment);
            return Content(file.Render(), "text/html");
        }


        public IActionResult Interface()
        {
            Interface @interface = new Interface(_hostingEnvironment);
            return Content(@interface.Render(), "text/html");
        }

        public IActionResult Invoice()
        {
            File file = new File(_hostingEnvironment, "invoice");
            return Content(file.Render(), "text/html");
        }
    }
}
