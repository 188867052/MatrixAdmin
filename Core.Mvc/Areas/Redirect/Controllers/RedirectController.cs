using System.Text;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Addons;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Button;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Chart;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Error;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Form;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Grid;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Index;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Interface;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Login;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Widget;
using Core.Web.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Redirect.Controllers
{
    public class RedirectController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectController"/> class.
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public RedirectController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }
        public IActionResult Test()
        {
            File file = new File(this.HostingEnvironment, "a_test");
            return this.Content(file.Render(), "text/html");
        }

        public IActionResult Index()
        {
            Index index = new Index(this.HostingEnvironment);
            return this.Content(index.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Index2()
        {
            Index2 file = new Index2(this.HostingEnvironment);
            return this.Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Login()
        {
            Login login = new Login(this.HostingEnvironment);
            return this.Content(login.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Widgets()
        {
            Widget widget = new Widget(this.HostingEnvironment);
            return this.Content(widget.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Buttons()
        {
            Button button = new Button(this.HostingEnvironment);
            return this.Content(button.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Calendar()
        {
            Calendar file = new Calendar(this.HostingEnvironment);
            return this.Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Charts()
        {
            Chart button = new Chart(this.HostingEnvironment);
            return this.Content(button.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Chat()
        {
            Chat button = new Chat(this.HostingEnvironment);
            return this.Content(button.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Dashboard()
        {
            File file = new File(this.HostingEnvironment, "dashboard");
            return this.Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Error(int number)
        {
            Error error = new Error(this.HostingEnvironment, number);
            return this.Content(error.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult FormCommon()
        {
            BasicForm basicForm = new BasicForm(this.HostingEnvironment);
            return this.Content(basicForm.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult FormValidation()
        {
            FormValidation basicForm = new FormValidation(this.HostingEnvironment);
            return this.Content(basicForm.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult FormWizard()
        {
            FormWizard basicForm = new FormWizard(this.HostingEnvironment);
            return this.Content(basicForm.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Gallery()
        {
            Gallery file = new Gallery(this.HostingEnvironment);
            return this.Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Grid()
        {
            Grid file = new Grid(this.HostingEnvironment);
            return this.Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Interface()
        {
            Interface @interface = new Interface(this.HostingEnvironment);
            return this.Content(@interface.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Invoice()
        {
            Invoice @interface = new Invoice(this.HostingEnvironment);
            return this.Content(@interface.Render(), "text/html", Encoding.UTF8);
        }
    }
}
