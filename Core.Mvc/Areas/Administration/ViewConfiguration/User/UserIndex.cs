using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.SearchFilterConfigurations;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.SearchFilterConfiguration;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class UserIndex<TModel, TPostModel> : SearchGridPage<TModel>
         where TModel : UserModel
         where TPostModel : UserPostModel
    {
        private readonly ResponseModel _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIndex{TModel, TPostModel}"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public UserIndex(ResponseModel response)
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
               Js.User.Index,
            };
        }

        /// <summary>
        /// Content header.
        /// </summary>
        /// <returns>The string.</returns>
        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("用户管理");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "首页", "返回首页", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        /// <summary>
        /// Create view instance constructions.
        /// </summary>
        /// <returns>The string.</returns>
        protected override IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            return new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new UserViewInstance()
            };
        }

        protected override SearchFilterConfiguration SearchFilterConfiguration()
        {
            return new UserSearchFilterConfiguration<TPostModel>();
        }

        protected override GridConfiguration<TModel> GridConfiguration()
        {
            return new UserViewConfiguration<TModel>(this._response);
        }
    }
}