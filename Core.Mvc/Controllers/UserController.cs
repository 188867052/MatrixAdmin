using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Mvc.ViewConfiguration.UserManage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

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
            UserManage index = new UserManage(this._hostingEnvironment);
            return Content(index.Render(), "text/html", Encoding.UTF8);
        }
    }
}