using System.Collections.Generic;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Index
{
    public class Index : SearchGridPage<object>
    {
        protected override string FileName
        {
            get
            {
                return "index";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/fullcalendar.css",

                "/font-awesome/css/font-awesome.css",
                "/css/jquery.gritter.css",
            };
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

        protected override GridConfiguration<object> GridConfiguration()
        {
            return null;
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