using System.Collections.Generic;
using Core.Extension;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Sidebar;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Form
{
    public class FormValidation : SearchGridPage<object>
    {
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
               "/css/uniform.css",

               "/font-awesome/css/font-awesome.css"
            };
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
               "/js/jquery.uniform.js",
               "/js/matrix.js",
               "/js/matrix.form_common.js",
               "/js/jquery.validate.js"
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Form with Validation");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}