using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Core.Mvc.Areas.Administration.ViewConfiguration;
using Core.Mvc.ViewConfiguration.Home;
using Core.Web.JavaScript;

namespace Core.Mvc.ViewConfiguration.Index
{
    public class Index : SearchGridPage
    {
        public Index(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }


        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/fullcalendar.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
                "/css/jquery.gritter.css",
            };
        }

        protected override string FileName
        {
            get
            {
                return "index";
            }
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
               "/js/excanvas.min.js",
               "/js/jquery.flot.min.js",
               "/js/jquery.flot.resize.min.js",
               "/js/jquery.peity.min.js",
               "/js/fullcalendar.min.js",
               "/js/matrix.js",
               "/js/matrix.dashboard.js",
               "/js/jquery.gritter.min.js",
               "/js/matrix.interface.js",
               "/js/matrix.chat.js",
               "/js/jquery.validate.js",
            };
        }

        protected override IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            IList<ViewInstanceConstruction> constructions = new List<ViewInstanceConstruction>
            {
                new IndexViewInstance()
            };
            return constructions;
        }
    }
}