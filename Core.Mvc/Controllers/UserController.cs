using Core.Mvc.ViewConfiguration.Administrator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Core.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManage()
        {
            UserIndex index = new UserIndex(this._hostingEnvironment);
            return Content(index.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult RoleManage()
        {
            RoleIndex index = new RoleIndex(this._hostingEnvironment);
            return Content(index.Render(), "text/html", Encoding.UTF8);
        }
        public IActionResult PermissionManage()
        {
            PermissionIndex index = new PermissionIndex(this._hostingEnvironment);
            return Content(index.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult MenuManage()
        {
            MenuIndex index = new MenuIndex(this._hostingEnvironment);
            return Content(index.Render(), "text/html", Encoding.UTF8);
        }
    }
}