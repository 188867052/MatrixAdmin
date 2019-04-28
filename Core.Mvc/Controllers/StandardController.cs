using Core.Mvc.ViewConfiguration.Home;
using Core.Web.Html;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Core.Model.Administration.User;
using Core.Web.ViewConfiguration;

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

        protected ContentResult ViewConfiguration(SearchGridPage index)
        {
            return this.RenderContent(index);
        }

        protected IActionResult Dialog<T>(DialogConfiguration<T> index)
        {
            return new JsonResult(new { data = index.Render(), id = DialogConfiguration<User>.Identifier.Value });
        }

        protected JsonResult GridConfiguration<T>(GridConfiguration<T> index)
        {
            return new JsonResult(new
            {
                data = index.Render(),
                pageSize = index.PageSize,
                currentPage = index.CurrentPage,
                pager = index.Pager()
            });
        }

        private ContentResult RenderContent(IRender render)
        {
            return base.Content(render.Render(), this._contentType, this._encoding);
        }
    }
}