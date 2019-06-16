using System.Collections.Generic;
using Core.Mvc;
using Core.Extension;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Chart
{
    public class Chart : SearchGridPage<object>
    {
        protected override string FileName
        {
            get
            {
                return "charts";
            }
        }

        public override IList<string> CssFiles()
        {
            return new List<string>
            {
                Css.Fullcalendar,
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> JavaScriptFiles()
        {
            return new List<string>
            {
               "/js/jquery.flot.min.js",
               "/js/jquery.flot.pie.min.js",
               Javascript.MatrixCharts,
               "/js/jquery.flot.resize.min.js",
               Javascript.Matrix,
               "/js/jquery.peity.min.js",
            };
        }

        protected override GridConfiguration<object> GridConfiguration()
        {
            return null;
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Charts");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}