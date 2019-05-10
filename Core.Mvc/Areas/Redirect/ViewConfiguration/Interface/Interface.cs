using System.Collections.Generic;
using Core.Extension;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Interface
{
    public class Interface : SearchGridPage
    {
        public Interface(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        protected override string FileName
        {
            get
            {
                return "interface";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/jquery.gritter.css",

                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> JavaScript()
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

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Elements");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}