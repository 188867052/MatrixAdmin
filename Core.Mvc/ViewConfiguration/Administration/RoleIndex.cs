using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Extension;
using Core.Model.Entity;
using Core.Model.ResponseModels;
using Core.Mvc.Controllers;
using Core.Mvc.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class RoleIndex : IndexBase
    {
        private readonly ResponseModel response;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public RoleIndex(IHostingEnvironment hostingEnvironment,ResponseModel response) : base(hostingEnvironment)
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
               "/js/role/role.js",
            };
        }

        public override string Render()
        {
            RoleViewConfiguration configuration=new RoleViewConfiguration(response);
            string table = configuration.Render();
            var html = base.Render().Replace("{{Table}}", table);

            RoleSearchGridFilterConfiguration filter = new RoleSearchGridFilterConfiguration();
            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());
            return html + RenderJavaScript();
        }

        private string RenderJavaScript()
        {
            JavaScript js = new JavaScript("index", "Index");
            Url url = new Url(typeof(RoleController), nameof(RoleController.GridStateChange));
            js.AddUrlInstance("searchUrl", url);

            return $"<script>{js.Render()}</script>";
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("角色管理");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController),nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }
    }
}