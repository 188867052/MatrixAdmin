using System.Collections.Generic;
using Core.Model;
using Core.Model.Log;
using Core.Mvc.Areas.Log.SearchFilterConfigurations;
using Core.Mvc.Areas.Redirect.Routes;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Mvc.Resource.Areas.Log.ViewConfiguration;
using Core.Web.JavaScript;
using Core.Web.SearchFilterConfiguration;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Log.ViewConfiguration
{
    public class LogIndex<TModel, TPostModel> : SearchGridPage<TModel>
        where TModel : LogModel
        where TPostModel : LogPostModel
    {
        private readonly HttpResponseModel _response;

        public LogIndex(HttpResponseModel response)
        {
            this._response = response;
        }

        public override IList<string> CssFiles()
        {
            return new List<string>();
        }

        protected override IList<string> JavaScriptFiles()
        {
            return new List<string>
            {
                Js.Log.Index
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader(ErrorResource.Header);
            contentHeader.AddAnchor(new Anchor(RedirectRoute.Index, "Home", "Go to Home", "Error-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        protected override IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            return new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new LogViewInstance()
            };
        }

        protected override SearchFilterConfiguration SearchFilterConfiguration()
        {
            return new LogSearchFilterConfiguration<TPostModel>();
        }

        protected override GridConfiguration<TModel> GridConfiguration()
        {
            return new LogGridConfiguration<TModel>(this._response);
        }
    }
}