using System.Collections.Generic;
using Core.Extension;
using Core.Mvc.Controllers;
using Core.Mvc.ViewConfiguration.Home;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Table
{
    public class Table : IndexBase
    {
        public Table(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/uniform.css",
                
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override string FileName
        {
            get
            {
                return "tables";
            }
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/jquery.uniform.js",
               "/js/matrix.js",
               "/js/matrix.tables.js"
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Tables");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController),nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}