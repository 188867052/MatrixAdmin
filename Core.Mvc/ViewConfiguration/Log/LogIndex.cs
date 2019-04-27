using Core.Mvc.Controllers;
using Core.Mvc.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Controllers.Log;
using Core.Resource.ViewConfiguration.Log;

namespace Core.Mvc.ViewConfiguration.Log
{
    public class LogIndex : SearchGridPage
    {
        private readonly ResponseModel response;

        public LogIndex(IHostingEnvironment hostingEnvironment, ResponseModel response) : base(hostingEnvironment)
        {
            this.response = response;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                
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
            LogGridConfiguration configuration = new LogGridConfiguration(response);
            string table = configuration.Render();
            var html = base.Render().Replace("{{Table}}", table);

            LogSearchGridFilterConfiguration filter = new LogSearchGridFilterConfiguration();
            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());

            return html + RenderJavaScript();
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader(ErrorResource.Header);
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "Error-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        private string RenderJavaScript()
        {
            JavaScript js = new JavaScript("index", "Index");
            Url url = new Url(nameof(Log), typeof(LogController), nameof(LogController.GridStateChange));
            js.AddUrlInstance("searchUrl", url);

            return $"<script>{js.Render()}</script>";
        }
    }
}