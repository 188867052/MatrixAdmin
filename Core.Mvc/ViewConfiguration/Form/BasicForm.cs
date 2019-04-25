using System.Collections.Generic;
using Core.Mvc.ViewConfiguration.Landing;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Form
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
        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Basic Form");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}