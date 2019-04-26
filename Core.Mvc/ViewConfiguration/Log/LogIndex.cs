using Core.Mvc.Controllers;
using Core.Mvc.ViewConfiguration.Home;
using Core.Resource.ViewConfiguration.Error;
using Core.Web.JavaScript;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Core.Extension;

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
                
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css"
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
               "/js/log/index.js",
            };
        }

        public override string Render()
        {
            LogGridConfiguration configuration = new LogGridConfiguration(this._errors);
            string table = configuration.Render();

            var html = base.Render().Replace("{{Table}}", table);
            html = html.Replace("{{widget-title}}", ErrorResource.Header);

            LogSearchGridFilterConfiguration filter = new LogSearchGridFilterConfiguration();
            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());

            return html + RenderJavaScript();
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader(ErrorResource.Header);
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController),nameof(RedirectController.Index)), "Home", "Go to Home", "Error-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        private string RenderJavaScript()
        {
            JavaScript js = new JavaScript("index", "Index");
            Url url = new Url(typeof(LogController), nameof(LogController.GridStateChange));
            js.AddUrlInstance("searchUrl", url);

            return $"<script>{js.Render()}</script>";
        }
    }
}