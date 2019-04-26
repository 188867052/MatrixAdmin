using Core.Mvc.ViewConfiguration.Home;
using Core.Web.Html;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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
            return this.RenderContent(index);
        }

        protected ContentResult Grid(IndexBase index)
        {
            return this.RenderContent(index);
        }

        private ContentResult RenderContent(IRender render)
        {
            return base.Content(render.Render(), this._contentType, this._encoding);
        }
    }
}