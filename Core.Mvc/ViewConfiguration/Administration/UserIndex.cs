using Core.Extension;
using Core.Mvc.Controllers;
using Core.Mvc.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Core.Model;
using Core.Mvc.Controllers.Administration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class UserIndex : SearchGridPage
    {

        private ResponseModel response;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="response"></param>
        public UserIndex(IHostingEnvironment hostingEnvironment, ResponseModel response) : base(hostingEnvironment)
        {
            this.response = response;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
            };
        }

        private string RenderJavaScript()
        {
            JavaScript js = new JavaScript("index", "Index");
            Url url = new Url(nameof(Administration), typeof(UserController), nameof(UserController.GridStateChange));
            js.AddUrlInstance("searchUrl", url);

            return $"<script>{js.Render()}</script>";
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
               "/js/User/user.js",
            };
        }

        public override string Render()
        {
            UserViewConfiguration configuration = new UserViewConfiguration(response);
            string table = configuration.Render();

            var html = base.Render().Replace("{{Table}}", table);
            UserSearchGridFilterConfiguration filter = new UserSearchGridFilterConfiguration();
            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());
            return html + RenderJavaScript();
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("用户管理");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }
    }
}