using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Areas.Log.SearchFilterConfigurations;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.JavaScript;
using Core.Web.Sidebar;

namespace Core.Mvc.Areas.Log.ViewConfiguration
{
    public class LogIndex : SearchGridPage
    {
        private readonly ResponseModel _response;

        public LogIndex(ResponseModel response)
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
            string table = new LogGridConfiguration(this._response).GenerateGridColumn();
            var html = base.Render().Replace("{{Table}}", table);

            LogSearchFilterConfiguration filter = new LogSearchFilterConfiguration();
            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());

            return html;
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
                "/js/log/index.js",
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader(ErrorResource.Header);
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "Error-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        protected override IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            IList<ViewInstanceConstruction> constructions = new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new LogViewInstance()
            };
            return constructions;
        }
    }
}