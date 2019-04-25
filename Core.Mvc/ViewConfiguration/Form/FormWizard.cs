using System.Collections.Generic;
using Core.Mvc.ViewConfiguration.Landing;
using Core.Web.Sidebar;
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
               "/css/matrix-style.css" ,
               "/css/matrix-media.css",
               "/font-awesome/css/font-awesome.css" ,
            };
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               
               "/js/jquery.ui.custom.js",
               
               "/js/jquery.validate.js",
               "/js/jquery.wizard.js",
               "/js/matrix.js",
               "/js/matrix.wizard.js",
            };
        }
        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Form with Wizard");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}