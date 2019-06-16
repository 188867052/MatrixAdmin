using System.Collections.Generic;
using Core.Mvc.Areas.Redirect.Routes;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Interface
{
    public class Interface : SearchGridPage<object>
    {
        protected override string FileName
        {
            get
            {
                return "interface";
            }
        }

        public override IList<string> CssFiles()
        {
            return new List<string>
            {
                "/css/jquery.gritter.css",

                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> JavaScriptFiles()
        {
            return new List<string>
            {
               "/js/jquery.gritter.min.js",
               "/js/jquery.peity.min.js",
               "/js/matrix.js",
               "/js/matrix.interface.js",
               "/js/matrix.popover.js",
            };
        }

        protected override GridConfiguration<object> GridConfiguration()
        {
            return null;
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Elements");
            contentHeader.AddAnchor(new Anchor(RedirectRoute.Index, "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}