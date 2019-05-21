using System.Collections.Generic;
using Core.Extension;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Sidebar;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Form
{
    public class FormWizard : SearchGridPage<object>
    {
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
               "/font-awesome/css/font-awesome.css"
            };
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
               "/js/jquery.validate.js",
               "/js/jquery.wizard.js",
               "/js/matrix.js",
               "/js/matrix.wizard.js",
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Form with Wizard");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}