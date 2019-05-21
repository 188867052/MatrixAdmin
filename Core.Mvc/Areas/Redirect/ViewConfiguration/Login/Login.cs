using System.Collections.Generic;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;

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

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/font-awesome/css/font-awesome.css",
                "/css/matrix-login.css",
            };
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
               "/js/matrix.login.js"
            };
        }
    }
}