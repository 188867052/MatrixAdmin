using System.Text;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Addons;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Button;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Chart;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Error;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Index;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Interface;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Login;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Widget;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Redirect.Controllers
{
    public class RedirectController : StandardController
    {
        public IActionResult Index()
        {
            Index index = new Index();
            return this.Content(index.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Index2()
        {
            Index2 file = new Index2();
            return this.Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Login()
        {
            LoginIndex login = new LoginIndex();
            return this.Content(login.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Widgets()
        {
            Widget widget = new Widget();
            return this.Content(widget.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Buttons()
        {
            Button button = new Button();
            return this.Content(button.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Calendar()
        {
            Calendar file = new Calendar();
            return this.Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Charts()
        {
            Chart button = new Chart();
            return this.Content(button.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Chat()
        {
            Chat button = new Chat();
            return this.Content(button.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Error(int number)
        {
            Error error = new Error(number);
            return this.Content(error.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Gallery()
        {
            Gallery file = new Gallery();
            return this.Content(file.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Interface()
        {
            Interface @interface = new Interface();
            return this.Content(@interface.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult Invoice()
        {
            Invoice @interface = new Invoice();
            return this.Content(@interface.Render(), "text/html", Encoding.UTF8);
        }
    }
}
