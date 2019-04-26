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
    public class RedirectController : StandardController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public RedirectController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public IActionResult Index()
        {
            Index index = new Index(this.HostingEnvironment);
            return Content(index.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Index2()
        {
            Index2 file = new Index2(HostingEnvironment);
            return Content(file.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Tables()
        {
            Table table = new Table(HostingEnvironment);
            return Content(table.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Login()
        {
            Login login = new Login(HostingEnvironment);
            return Content(login.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Widgets()
        {
            Widget widget = new Widget(HostingEnvironment);
            return Content(widget.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Buttons()
        {
            Button button = new Button(HostingEnvironment);
            return Content(button.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Calendar()
        {
            Calendar file = new Calendar(HostingEnvironment);
            return Content(file.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Charts()
        {
            Chart button = new Chart(HostingEnvironment);
            return Content(button.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Chat()
        {
            Chat button = new Chat(HostingEnvironment);
            return Content(button.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Dashboard()
        {
            File file = new File(HostingEnvironment, "dashboard");
            return Content(file.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Error403()
        {
            Error error = new Error(HostingEnvironment, 403);
            return Content(error.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Error404()
        {
            Error error = new Error(HostingEnvironment, 404);
            return Content(error.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Error405()
        {
            Error error = new Error(HostingEnvironment, 405);
            return Content(error.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Error500()
        {
            Error error = new Error(HostingEnvironment, 500);
            return Content(error.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult FormCommon()
        {
            BasicForm basicForm = new BasicForm(HostingEnvironment);
            return Content(basicForm.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult FormValidation()
        {
            FormValidation basicForm = new FormValidation(HostingEnvironment);
            return Content(basicForm.Render(), "text/html",Encoding.UTF8);
        }


        public IActionResult FormWizard()
        {
            FormWizard basicForm = new FormWizard(HostingEnvironment);
            return Content(basicForm.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Gallery()
        {
            Gallery file = new Gallery(HostingEnvironment);
            return Content(file.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Grid()
        {
            Grid file = new Grid(HostingEnvironment);
            return Content(file.Render(), "text/html",Encoding.UTF8);
        }


        public IActionResult Interface()
        {
            Interface @interface = new Interface(HostingEnvironment);
            return Content(@interface.Render(), "text/html",Encoding.UTF8);
        }

        public IActionResult Invoice()
        {
            Invoice @interface = new Invoice(HostingEnvironment);
            return Content(@interface.Render(), "text/html",Encoding.UTF8);
        }
    }
}
