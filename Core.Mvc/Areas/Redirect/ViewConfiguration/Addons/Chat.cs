using System.Collections.Generic;
using Core.Extension;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Addons
{
    public class Chat : SearchGridPage
    {
        public Chat(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        protected override string FileName
        {
            get
            {
                return "Chat";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
               "/js/matrix.js",
               "/js/matrix.chat.js"
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Gallery");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}