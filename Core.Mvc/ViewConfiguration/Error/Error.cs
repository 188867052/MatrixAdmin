using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace Core.Mvc.ViewConfigurations.Table
{
    public class Error : IndexBase
    {
        public Error(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        protected override string Title
        {
            get
            {
                return "Matrix Admin";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/bootstrap.min.css",
                "/css/bootstrap-responsive.min.css",
                "/font-awesome/css/font-awesome.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
            };
        }

        protected override string FileName
        {
            get
            {
                return "Error403";
            }
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/jquery.min.js",
               "/js/jquery.ui.custom.js",
               "/js/bootstrap.min.js",
            };
        }
    }
}