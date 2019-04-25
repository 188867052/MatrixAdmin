using System.Text;
using Core.Mvc.ViewConfiguration.Addons;
using Core.Mvc.ViewConfiguration.Button;
using Core.Mvc.ViewConfiguration.Chart;
using Core.Mvc.ViewConfiguration.Error;
using Core.Mvc.ViewConfiguration.Form;
using Core.Mvc.ViewConfiguration.Grid;
using Core.Mvc.ViewConfiguration.Index;
using Core.Mvc.ViewConfiguration.Interface;
using Core.Mvc.ViewConfiguration.Login;
using Core.Mvc.ViewConfiguration.Table;
using Core.Mvc.ViewConfiguration.Widget;
using Core.Web.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers
{
    public class RedirectController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public RedirectController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            Index index = new Index(this._hostingEnvironment);
            return Content(index.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Index2()
        {
            Index2 file = new Index2(_hostingEnvironment);
            return Content(file.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Tables()
        {
            Table table = new Table(_hostingEnvironment);
            return Content(table.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Login()
        {
            Login login = new Login(_hostingEnvironment);
            return Content(login.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Widgets()
        {
            Widget widget = new Widget(_hostingEnvironment);
            return Content(widget.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Buttons()
        {
            Button button = new Button(_hostingEnvironment);
            return Content(button.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Calendar()
        {
            Calendar file = new Calendar(_hostingEnvironment);
            return Content(file.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Charts()
        {
            Chart button = new Chart(_hostingEnvironment);
            return Content(button.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Chat()
        {
            Chat button = new Chat(_hostingEnvironment);
            return Content(button.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Dashboard()
        {
            File file = new File(_hostingEnvironment, "dashboard");
            return Content(file.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Error403()
        {
            Error error = new Error(_hostingEnvironment, 403);
            return Content(error.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Error404()
        {
            Error error = new Error(_hostingEnvironment, 404);
            return Content(error.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Error405()
        {
            Error error = new Error(_hostingEnvironment, 405);
            return Content(error.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Error500()
        {
            Error error = new Error(_hostingEnvironment, 500);
            return Content(error.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult FormCommon()
        {
            BasicForm basicForm = new BasicForm(_hostingEnvironment);
            return Content(basicForm.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult FormValidation()
        {
            FormValidation basicForm = new FormValidation(_hostingEnvironment);
            return Content(basicForm.Render(), "text/html",Encoding.UTF8);
        }


        public IActionResult FormWizard()
        {
            FormWizard basicForm = new FormWizard(_hostingEnvironment);
            return Content(basicForm.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Gallery()
        {
            Gallery file = new Gallery(_hostingEnvironment);
            return Content(file.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Grid()
        {
            Grid file = new Grid(_hostingEnvironment);
            return Content(file.Render(), "text/html",Encoding.UTF8);
        }


        public IActionResult Interface()
        {
            Interface @interface = new Interface(_hostingEnvironment);
            return Content(@interface.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Invoice()
        {
            Invoice @interface = new Invoice(_hostingEnvironment);
            return Content(@interface.Render(), "text/html",Encoding.UTF8);
        }
    }
}
