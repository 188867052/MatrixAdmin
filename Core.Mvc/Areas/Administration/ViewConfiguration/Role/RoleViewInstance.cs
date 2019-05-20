using Core.Extension;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Role
{
    public class RoleViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName { get; } = "Index";

        public override void InitializeViewInstance(JavaScriptInitialize initialize)
        {
            Url url = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.GridStateChange));
            initialize.AddUrlInstance("searchUrl", url);
        }
    }
}
