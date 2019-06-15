using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.SearchFilterConfigurations;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Mvc.Resource.Areas.Administration.ViewConfiguration.Menu;
using Core.Web.JavaScript;
using Core.Web.SearchFilterConfiguration;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
{
    public class MenuIndex<TModel, TPostModel> : SearchGridPage<TModel>
        where TModel : MenuModel
        where TPostModel : MenuPostModel
    {
        private readonly ResponseModel _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuIndex{TModel, TPostModel}"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public MenuIndex(ResponseModel response)
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
                Api.Js.Menu.Index
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
            return new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new MenuViewInstance()
            };
        }

        protected override SearchFilterConfiguration SearchFilterConfiguration()
        {
            return new MenuSearchFilterConfiguration<TPostModel>();
        }

        protected override GridConfiguration<TModel> GridConfiguration()
        {
            return new MenuViewConfiguration<TModel>(this._response);
        }
    }
}