using System.Collections.Generic;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Login
{
    public class Login : SearchGridPage<object>
    {
        protected override string FileName
        {
            get
            {
                return "login";
            }
        }

        public override IList<string> CssFiles()
        {
            return new List<string>
            {
                "/font-awesome/css/font-awesome.css",
                "/css/matrix-login.css",
            };
        }

        protected override IList<string> JavaScriptFiles()
        {
            return new List<string>
            {
               "/js/matrix.login.js"
            };
        }

        protected override GridConfiguration<object> GridConfiguration()
        {
            return null;
        }
    }
}