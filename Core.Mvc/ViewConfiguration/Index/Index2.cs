using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace Core.Mvc.ViewConfiguration.Index
{
    public class Index2 : IndexBase
    {
        public Index2(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }


        public override IList<string> Css()
        {
            return new List<string>
            {
                
                "/css/bootstrap-responsive.min.css",
                "/css/fullcalendar.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override string FileName
        {
            get
            {
                return "Index2";
            }
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/excanvas.min.js",
               
               "/js/jquery.ui.custom.js",
               
               "/js/jquery.flot.min.js",
               "/js/jquery.flot.resize.min.js",
               "/js/jquery.peity.min.js",
               "/js/matrix.js",
               "/js/fullcalendar.min.js",
               "/js/matrix.calendar.js",
               "/js/jquery.validate.js",
               "/js/matrix.form_validation.js",
               "/js/jquery.wizard.js",
               "/js/jquery.uniform.js",
               "/js/select2.min.js",
               "/js/matrix.popover.js",
               "/js/jquery.dataTables.min.js",
               "/js/matrix.tables.js",
               "/js/matrix.interface.js",
            };
        }
    }
}