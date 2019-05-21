using System.Collections.Generic;
using Core.Extension;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Widget
{
    public class Widget : SearchGridPage<object>
    {
        protected override string FileName
        {
            get
            {
                return "widgets";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/fullcalendar.css",

                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
               "/js/matrix.js",
            };
        }

        protected override GridConfiguration<object> GridConfiguration()
        {
            return null;
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Widget");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}