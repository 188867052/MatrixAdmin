using Core.Mvc.Areas.Administration.Routes;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class RoleViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName { get; } = "Index";

        public override void InitializeViewInstance(JavaScriptInitialize initialize)
        {
            initialize.AddUrlInstance("searchUrl", RoleRoute.GridStateChange);
        }
    }
}
