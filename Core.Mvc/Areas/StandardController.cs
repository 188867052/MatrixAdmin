using System.Text;
using Core.Model;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Dialog;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardController"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The hostingEnvironment.</param>
        protected StandardController(IHostingEnvironment hostingEnvironment)
        {
            this.HostingEnvironment = hostingEnvironment;
        }

        protected IHostingEnvironment HostingEnvironment { get; set; }

        protected ContentResult ViewConfiguration(SearchGridPage index)
        {
            return this.RenderContent(index);
        }

        protected IActionResult Dialog<TPostModel, T>(DialogConfiguration<TPostModel, T> index)
        {
            return new JsonResult(new { data = index.Render(default), id = index.Identifier.Value });
        }

        protected IActionResult Submit(ResponseModel model)
        {
            return new JsonResult(model);
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
            return this.Content(render.Render(), this._contentType, this._encoding);
        }
    }
}