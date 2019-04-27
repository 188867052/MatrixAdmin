using System.Collections.Generic;
using Core.Extension;
using Core.Model.ResponseModels;
using Core.Mvc.Controllers;
using Core.Mvc.ViewConfiguration.Home;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class MenuIndex : IndexBase
    {
        private readonly ResponseModel response;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public MenuIndex(IHostingEnvironment hostingEnvironment, ResponseModel response) : base(hostingEnvironment)
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
               "/js/jquery.uniform.js",
               "/js/matrix.js",
               "/js/matrix.tables.js"
            };
        }

        public override string Render()
        {
            MenuViewConfiguration configuration = new MenuViewConfiguration(this.response);
            string table = configuration.Render();
            var html = base.Render().Replace("{{Table}}", table);
            return html;
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("菜单管理");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }
    }
}