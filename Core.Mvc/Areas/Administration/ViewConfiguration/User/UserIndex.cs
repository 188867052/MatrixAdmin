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

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class UserIndex : SearchGridPage
    {
        private readonly ResponseModel _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIndex"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public UserIndex(ResponseModel response)
        {
            this._response = response;
        }

        public override IList<string> Css()
        {
            return new List<string>();
        }

        public override string Render()
        {
            UserViewConfiguration configuration = new UserViewConfiguration(this._response);
            string table = configuration.GenerateGridColumn();

            var html = base.Render().Replace("{{Table}}", table);
            return html;
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
                "/js/User/user.js",
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
            IList<ViewInstanceConstruction> constructions = new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new UserViewInstance()
            };
            return constructions;
        }

        protected override SearchFilterConfiguration SearchFilterConfiguration()
        {
            return new UserSearchFilterConfiguration<UserPostModel>();
        }
    }
}