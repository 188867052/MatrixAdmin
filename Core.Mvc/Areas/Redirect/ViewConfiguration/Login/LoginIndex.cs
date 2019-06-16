using System.Collections.Generic;
using Core.Mvc;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Login
{
    public class LoginIndex : SearchGridPage<object>
    {
        protected override string FileName => "login";

        public override IList<string> CssFiles()
        {
            return new List<string>
            {
                "/font-awesome/css/font-awesome.css",
                Css.MatrixLogin
            };
        }

        protected override IList<string> JavaScriptFiles()
        {
            return new List<string>
            {
               Javascript.MatrixLogin,
               Mvc.Js.Login.Index
            };
        }

        protected override GridConfiguration<object> GridConfiguration()
        {
            return null;
        }

        /// <summary>
        /// Create view instance constructions.
        /// </summary>
        /// <returns>The string.</returns>
        protected override IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            return new List<ViewInstanceConstruction>
            {
                new IndexViewInstance(),
                new LoginViewInstance()
            };
        }
    }
}