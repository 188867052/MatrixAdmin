using System.Diagnostics;
using Core.Model.Log;
using Core.Web.File;
using Core.Web.Login;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    public class LoginController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The hostingEnvironment.</param>
        public LoginController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public IActionResult Index()
        {
            Login login = new Login(this.HostingEnvironment);
            return this.Content(login.Render(), "text/html");
        }

        public IActionResult File(string fileName)
        {
            File file = new File(this.HostingEnvironment, fileName);
            return this.Content(file.Render(), "text/html");
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
