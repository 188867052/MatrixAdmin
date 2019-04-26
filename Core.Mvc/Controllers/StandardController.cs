using System.Text;
using Core.Mvc.ViewConfiguration.Home;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers
{
    public class StandardController : Controller
    {
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _contentType = "text/html";
        protected readonly IHostingEnvironment HostingEnvironment;

        /// <summary>
        /// 
        /// </summary>
        protected StandardController(IHostingEnvironment hostingEnvironment)
        {
            this.HostingEnvironment = hostingEnvironment;
        }

        protected ContentResult Index(IndexBase index)
        {
            return base.Content(index.Render(), _contentType, _encoding);
        }

        protected ContentResult Grid(IndexBase index)
        {
            return base.Content(index.Render(), _contentType, _encoding);
        }
    }
}