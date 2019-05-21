using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.SearchFilterConfigurations;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.SearchFilterConfiguration;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class RoleIndex<TModel, TPostModel> : SearchGridPage<TModel>
        where TModel : RoleModel
        where TPostModel : RolePostModel
    {
        private readonly ResponseModel _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleIndex{TModel, TPostModel}"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public RoleIndex(ResponseModel response)
        {
            this._response = response;
        }

        public override IList<string> Css()
        {
            return new List<string>();
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
            return new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new RoleViewInstance()
            };
        }

        protected override SearchFilterConfiguration SearchFilterConfiguration()
        {
            return new RoleSearchFilterConfiguration<TPostModel>();
        }

        protected override GridConfiguration<TModel> GridConfiguration()
        {
            return new RoleViewConfiguration<TModel>(this._response);
        }
    }
}