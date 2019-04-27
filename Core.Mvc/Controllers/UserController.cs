using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Core.Mvc.ViewConfiguration.Administration;

namespace Core.Mvc.Controllers
{
    public class UserController : StandardController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public UserController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public IActionResult UserManage()
        {
            UserIndex index = new UserIndex(this.HostingEnvironment);
            return Content(index.Render(), "text/html", Encoding.UTF8);
        }

        public IActionResult RoleManage()
        {
            RoleIndex index = new RoleIndex(this.HostingEnvironment);
            return Content(index.Render(), "text/html", Encoding.UTF8);
        }
        public IActionResult PermissionManage()
        {
            PermissionIndex index = new PermissionIndex(this.HostingEnvironment);
            return Content(index.Render(), "text/html", Encoding.UTF8);
        }
    }
}