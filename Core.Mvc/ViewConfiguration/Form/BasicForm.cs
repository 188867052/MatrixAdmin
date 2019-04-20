using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace Core.Mvc.ViewConfigurations.Table
{
    public class BasicForm : IndexBase
    {
        public BasicForm(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        protected override string FileName
        {
            get
            {
                return "form-common";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
               "/css/bootstrap.min.css",
               "/css/bootstrap-responsive.min.css",
               "/css/colorpicker.css" ,
               "/css/datepicker.css" ,
               "/css/uniform.css" ,
               "/css/select2.css" ,
               "/css/matrix-style.css" ,
               "/css/matrix-media.css",
               "/css/bootstrap-wysihtml5.css" ,
               "/font-awesome/css/font-awesome.css" ,
            };
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/jquery.min.js",
               "/js/jquery.ui.custom.js",
               "/js/bootstrap.min.js",
               "/js/bootstrap-colorpicker.js",
               "/js/bootstrap-datepicker.js",
               "/js/jquery.toggle.buttons.html",
               "/js/masked.js",
               "/js/jquery.uniform.js",
               "/js/select2.min.js",
               "/js/matrix.js",
               "/js/matrix.form_common.js",
               "/js/wysihtml5-0.3.0.js",
               "/js/jquery.peity.min.js",
               "/js/bootstrap-wysihtml5.js",
            };
        }
    }
}