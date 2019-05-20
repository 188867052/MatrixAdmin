using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Areas.Administration.SearchFilterConfigurations;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.JavaScript;
using Core.Web.Sidebar;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
{
    public class MenuIndex : SearchGridPage
    {
        private readonly ResponseModel _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuIndex"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public MenuIndex(ResponseModel response)
        {
            this._response = response;
        }

        protected override string FileName => "SearchGridPage";

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/font-awesome/css/font-awesome.css",
            };
        }

        public override string Render()
        {
            MenuViewConfiguration configuration = new MenuViewConfiguration(this._response);
            string table = configuration.GenerateGridColumn();
            var html = base.Render().Replace("{{Table}}", table);

            MenuSearchFilterConfiguration filter = new MenuSearchFilterConfiguration();

            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());
            return html;
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
                "/js/menu/index.js",
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader(MenuIndexResource.MenuManageTitle);
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        protected override IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            IList<ViewInstanceConstruction> constructions = new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new MenuViewInstance()
            };
            return constructions;
        }
    }
}