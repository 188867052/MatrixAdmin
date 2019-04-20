using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Grid
{
    public class Grid : IndexBase
    {
        public Grid(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        protected override string FileName
        {
            get
            {
                return "grid";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/bootstrap.min.css",
                "/css/bootstrap-responsive.min.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/jquery.min.js",
               "/js/bootstrap.min.js",
               "/js/jquery.ui.custom.js",
               "/js/matrix.js",
            };
        }
    }
}