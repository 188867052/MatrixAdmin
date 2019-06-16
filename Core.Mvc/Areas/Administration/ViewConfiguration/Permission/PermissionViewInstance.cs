using Core.Mvc.Areas.Administration.Routes;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Permission
{
    public class PermissionViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName => "Index";

        public override void InitializeViewInstance(JavaScriptInitialize initialize)
        {
            initialize.AddUrlInstance("searchUrl", PermissionRoute.GridStateChange);
        }
    }
}
