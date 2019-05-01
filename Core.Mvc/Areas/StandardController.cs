using System.Text;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Html;
using Core.Web.ViewConfiguration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas
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

        protected IActionResult Dialog<TPostModel, T>(DialogConfiguration<TPostModel, T> index)
        {
            return new JsonResult(new { data = index.Render(), id = index.Identifier.Value });
        }

        protected IActionResult Submit<T>()
        {
            return new JsonResult(new { success = true });
        }

        protected JsonResult GridConfiguration<T>(GridConfiguration<T> index)
        {
            return new JsonResult(new
            {
                data = index.GenerateGridColumn(),
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