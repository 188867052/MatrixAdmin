using Core.Extension;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Mvc.Areas.Redirect.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Login
{
    public class LoginViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName => "Index";

        public override void InitializeViewInstance(JavaScriptInitialize initialize)
        {
            var url = new Url(typeof(RedirectController), nameof(RedirectController.Index));
            var authUrl = new Url(nameof(Administration), typeof(AuthenticationController), nameof(AuthenticationController.Auth));
            var roleUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.Index));

            initialize.AddUrlInstance("indexUrl", url);
            initialize.AddUrlInstance("authUrl", authUrl);
            initialize.AddUrlInstance("roleUrl", roleUrl);
        }
    }
}
