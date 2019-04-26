using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers
{
    public class StandardController : Controller
    {
        public readonly IHostingEnvironment HostingEnvironment;

        /// <summary>
        /// 
        /// </summary>
        public StandardController(IHostingEnvironment hostingEnvironment)
        {
            this.HostingEnvironment = hostingEnvironment;
        }
    }
}