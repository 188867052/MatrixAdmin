using Core.Extension;
using Core.Model.ResponseModels;
using Core.Mvc.ViewConfiguration.Home;
using Core.Resource.ViewConfiguration.Error;
using Core.Web.JavaScript;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Mvc.Controllers;

namespace Core.Mvc.ViewConfiguration.Log
{
    public class LogIndex : IndexBase
    {
        private readonly List<Model.Entity.Log> _errors;

        public LogIndex(IHostingEnvironment hostingEnvironment, List<Model.Entity.Log> errors) : base(hostingEnvironment)
        {
            this._errors = errors;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/uniform.css",
                "/css/select2.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",

                "/css/Log/Log.css",
            };
        }

        protected override string FileName
        {
            get
            {
                return "Manage";
            }
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/select2.min.js",
               "/js/log/index.js",
            };
        }

        public override string Render()
        {
            LogViewConfiguration configuration = new LogViewConfiguration(this._errors);
            string table = configuration.Render();
            LogSearchGridFilterConfiguration filter = new LogSearchGridFilterConfiguration();

            var html = base.Render().Replace("{{Table}}", table);
            html = html.Replace("{{widget-title}}", ErrorResource.Header);
            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());

            return html + RenderJavaScript();
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader(ErrorResource.Header);
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "Error-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        private string RenderJavaScript()
        {
            JavaScript js = new JavaScript("index", "Index");
            var url = new Url(typeof(LogController), nameof(LogController.Search));
            js.AddUrlInstance("searchUrl", url);

            return $"<script>{js.Render()}</script>";
        }
    }
}