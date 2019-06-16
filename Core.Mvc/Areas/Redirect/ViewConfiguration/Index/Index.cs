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

        public override IList<string> CssFiles()
        {
            return new List<string>
            {
                Css.Fullcalendar,
                "/font-awesome/css/font-awesome.css",
                Css.JqueryGritter
            };
        }

        protected override IList<string> JavaScriptFiles()
        {
            return new List<string>
            {
               "/js/excanvas.min.js",
               "/js/jquery.flot.min.js",
               "/js/jquery.flot.resize.min.js",
               "/js/jquery.peity.min.js",
               "/js/fullcalendar.min.js",
               Javascript.Matrix,
               Javascript.MatrixDashboard,
               "/js/jquery.gritter.min.js",
               Javascript.MatrixInterface,
               Javascript.MatrixChat,
               Javascript.JqueryValidate
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