using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Areas.Administration.SearchFilterConfigurations;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.Sidebar;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class RoleIndex : SearchGridPage
    {
        private readonly ResponseModel _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleIndex"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public RoleIndex(ResponseModel response)
        {
            this._response = response;
        }

        protected override string FileName { get; } = "SearchGridPage";

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/font-awesome/css/font-awesome.css",
            };
        }

        public override string Render()
        {
            RoleViewConfiguration configuration = new RoleViewConfiguration(this._response);
            string table = configuration.GenerateGridColumn();
            var html = base.Render().Replace("{{Table}}", table);

            RoleSearchFilterConfiguration filter = new RoleSearchFilterConfiguration();
            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());
            return html;
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
                "/js/role/role.js",
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("角色管理");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        protected override IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            IList<ViewInstanceConstruction> constructions = new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new RoleViewInstance()
            };
            return constructions;
        }
    }
}