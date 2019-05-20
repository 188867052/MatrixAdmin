using System.Collections.Generic;
using Core.Extension;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Sidebar;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Chart
{
    public class Chart : SearchGridPage
    {
        protected override string FileName
        {
            get
            {
                return "charts";
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
               "/js/jquery.flot.min.js",
               "/js/jquery.flot.pie.min.js",
               "/js/matrix.charts.js",
               "/js/jquery.flot.resize.min.js",
               "/js/matrix.js",
               "/js/jquery.peity.min.js",
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Charts");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}