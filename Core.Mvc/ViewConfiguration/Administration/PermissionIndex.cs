using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Controllers;
using Core.Mvc.Controllers.Administration;
using Core.Mvc.ViewConfiguration.Home;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.JavaScript;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class PermissionIndex : IndexBase
    {
        private readonly ResponseModel response;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public PermissionIndex(IHostingEnvironment hostingEnvironment,ResponseModel response) : base(hostingEnvironment)
        {
            this.response = response;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/uniform.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
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
               "/js/Permission/index.js",
            };
        }

        public override string Render()
        {
            PermissionGridConfiguration configuration = new PermissionGridConfiguration(response);
            string table = configuration.Render();
            var html = base.Render().Replace("{{Table}}", table);

            PermissionFilterConfiguration filter = new PermissionFilterConfiguration();

            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());
            return html + RenderJavaScript();
        }

        private string RenderJavaScript()
        {
            JavaScript js = new JavaScript("index", "Index");
            Url url = new Url(typeof(PermissionController), nameof(PermissionController.GridStateChange));
            js.AddUrlInstance("searchUrl", url);

            return $"<script>{js.Render()}</script>";
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader(PermissionIndexResource.WidgetTitle);
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController),nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }
    }
}