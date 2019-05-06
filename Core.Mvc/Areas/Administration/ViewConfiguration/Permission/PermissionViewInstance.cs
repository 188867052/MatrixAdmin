using Core.Extension;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Permission
{
    public class PermissionViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName
        {
            get
            {
                return "Index";
            }
        }

        public override void InitializeViewInstance(JavaScriptInitialize javaScriptInitialize)
        {
            Url url = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.GridStateChange));
            javaScriptInitialize.AddUrlInstance("searchUrl", url);
        }
    }
}
