using Core.Extension;
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
            initialize.AddUrlInstance("indexUrl", url);
        }
    }
}
