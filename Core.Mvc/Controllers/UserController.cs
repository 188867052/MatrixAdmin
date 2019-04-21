using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Entities;
using Core.Models.Models.Response;
using Core.Mvc.ViewConfiguration.Administrator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    }
}