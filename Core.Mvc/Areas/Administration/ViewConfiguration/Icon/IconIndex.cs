using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Areas.Administration.SearchFilterConfigurations;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.SearchFilterConfiguration;
using Core.Web.Sidebar;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Icon
{
    public class IconIndex : SearchGridPage<object>
    {
        private readonly ResponseModel _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="IconIndex"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public IconIndex(ResponseModel response)
        {
            this._response = response;
        }

        /// <inheritdoc/>
        public override IList<string> Css()
        {
            return new List<string>();
        }

        /// <inheritdoc/>
        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
                "/js/icon/index.js",
            };
        }

        protected override SearchFilterConfiguration SearchFilterConfiguration()
        {
            return new IconSearchFilterConfiguration();
        }

        /// <inheritdoc/>
        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("图标管理");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }

        /// <inheritdoc/>
        protected override IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            return new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new IconViewInstance()
            };
        }
    }
}