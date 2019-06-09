using System.Collections.Generic;
using Core.Extension;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Addons
{
    public class Calendar : SearchGridPage<object>
    {
        protected override string FileName
        {
            get
            {
                return "Calendar";
            }
        }

        public override IList<string> CssFiles()
        {
            return new List<string>
            {
                "/font-awesome/css/font-awesome.css",
                "/css/fullcalendar.css"
            };
        }

        protected override IList<string> JavaScriptFiles()
        {
            return new List<string>
            {
               "/js/matrix.js",
               "/js/fullcalendar.min.js",
               "/js/matrix.calendar.js"
            };
        }

        protected override GridConfiguration<object> GridConfiguration()
        {
            return null;
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Calendar");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}