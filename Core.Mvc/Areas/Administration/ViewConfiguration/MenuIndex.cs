using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class MenuIndex : SearchGridPage
    {
        private readonly ResponseModel response;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="response"></param>
        public MenuIndex(IHostingEnvironment hostingEnvironment, ResponseModel response) : base(hostingEnvironment)
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

        protected override string FileName
        {
            get
            {
                return "Manage";
            }
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
               "/js/menu/index.js",
            };
        }

        public override string Render()
        {
            MenuViewConfiguration configuration = new MenuViewConfiguration(this.response);
            string table = configuration.Render();
            var html = base.Render().Replace("{{Table}}", table);

            MenuFilterConfiguration filter = new MenuFilterConfiguration();

            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());
            return html;
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("菜单管理");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        protected override IList<IViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            IList<IViewInstanceConstruction> constructions = new List<IViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new MenuViewInstance()
            };
            return constructions;
        }
    }
}