using System.Collections.Generic;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Login
{
    public class Login : SearchGridPage
    {
        public Login(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/font-awesome/css/font-awesome.css",
                "/css/matrix-login.css",
            };
        }

        protected override string FileName
        {
            get
            {
                return "login";
            }
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