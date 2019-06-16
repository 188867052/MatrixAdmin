using Core.Mvc.Areas.Administration.Routes;
using Core.Mvc.Areas.Redirect.Routes;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Login
{
    public class LoginViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName => "Index";

        public override void InitializeViewInstance(JavaScriptInitialize initialize)
        {
            initialize.AddUrlInstance("indexUrl", RedirectRoute.Index);
            initialize.AddUrlInstance("authUrl", AuthenticationRoute.Auth);
            initialize.AddUrlInstance("roleUrl", RoleRoute.Index);
        }
    }
}
