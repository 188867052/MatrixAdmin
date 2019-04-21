using System.Collections.Generic;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Form
{
    public class FormValidation : IndexBase
    {
        public FormValidation(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        protected override string FileName
        {
            get
            {
                return "form-validation";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
               "/css/bootstrap.min.css",
               "/css/bootstrap-responsive.min.css",
               "/css/uniform.css" ,
               "/css/select2.css" ,
               "/css/matrix-style.css" ,
               "/css/matrix-media.css",
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
               "/js/jquery.uniform.js",
               "/js/select2.min.js",
               "/js/matrix.js",
               "/js/matrix.form_common.js",
               "/js/jquery.validate.js",
            };
        }
        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Form with Validation");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}