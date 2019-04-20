using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Core.Mvc.ViewConfiguration;

namespace Core.Mvc.ViewConfigurations.Table
{
    public class Interface : IndexBase
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
                "/css/bootstrap.min.css",
                "/css/bootstrap-responsive.min.css",
                "/css/jquery.gritter.css",
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
               "/js/jquery.ui.custom.js",
               "/js/bootstrap.min.js",
               "/js/jquery.gritter.min.js",
               "/js/jquery.peity.min.js",
               "/js/matrix.js",
               "/js/matrix.interface.js",
               "/js/matrix.popover.js",
            };
        }
    }
}