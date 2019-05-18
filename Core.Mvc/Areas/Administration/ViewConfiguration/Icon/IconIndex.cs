using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Areas.Administration.SearchFilterConfigurations;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.Sidebar;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Icon
{
    public class IconIndex : SearchGridPage
    {
        private readonly ResponseModel response;

        /// <summary>
        /// Initializes a new instance of the <see cref="IconIndex"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public IconIndex(ResponseModel response)
        {
            this.response = response;
        }

        /// <inheritdoc/>
        protected override string FileName
        {
            get
            {
                return "SearchGridPage";
            }
        }

        /// <inheritdoc/>
        public override IList<string> Css()
        {
            return new List<string>
            {
                "/font-awesome/css/font-awesome.css",
            };
        }

        public override string Render()
        {
            IconGridConfiguration configuration = new IconGridConfiguration(this.response);
            string table = configuration.GenerateGridColumn();
            var html = base.Render().Replace("{{Table}}", table);

            IconSearchFilterConfiguration filter = new IconSearchFilterConfiguration();
            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());
            html = html.Replace("{{Pager}}", this.Pager());

            return html;
        }

        /// <inheritdoc/>
        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
                "/js/icon/index.js",
            };
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
            IList<ViewInstanceConstruction> constructions = new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new IconViewInstance()
            };
            return constructions;
        }
    }
}