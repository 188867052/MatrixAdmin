using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Form
{
    public class FormWizard : IndexBase
    {
        public FormWizard(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        protected override string FileName
        {
            get
            {
                return "form-wizard";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
               "/css/bootstrap.min.css",
               "/css/bootstrap-responsive.min.css",
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
               "/js/jquery.validate.js",
               "/js/jquery.wizard.js",
               "/js/matrix.js",
               "/js/matrix.wizard.js",
            };
        }
    }
}