using System.Collections.Generic;
using Core.Mvc.ViewConfiguration.Landing;
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
                "/css/select2.css",
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
               "/js/jquery.ui.custom.js",
               "/js/jquery.uniform.js",
               "/js/select2.min.js",
               "/js/jquery.dataTables.min.js",
               "/js/matrix.js",
               "/js/matrix.tables.js"
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Tables");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}