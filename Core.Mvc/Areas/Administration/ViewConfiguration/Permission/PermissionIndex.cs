using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Areas.Administration.SearchFilterConfigurations;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.SearchFilterConfiguration;
using Core.Web.Sidebar;
using Resources = Core.Resource.Areas.Administration.ViewConfiguration.Permission.PermissionIndexResource;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Permission
{
    public class PermissionIndex : SearchGridPage
    {
        private readonly ResponseModel _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionIndex"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public PermissionIndex(ResponseModel response)
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
            PermissionGridConfiguration configuration = new PermissionGridConfiguration(this._response);
            string table = configuration.GenerateGridColumn();
            var html = base.Render().Replace("{{Table}}", table);

            PermissionSearchFilterConfiguration filter = new PermissionSearchFilterConfiguration();

            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());
            return html;
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
               "/js/Permission/index.js",
            };
        }

        protected override SearchFilterConfiguration SearchFilterConfiguration()
        {
            return new PermissionSearchFilterConfiguration();
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader(Resources.WidgetTitle);
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "首页", "返回首页", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        protected override IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            IList<ViewInstanceConstruction> constructions = new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new PermissionViewInstance()
            };
            return constructions;
        }
    }
}