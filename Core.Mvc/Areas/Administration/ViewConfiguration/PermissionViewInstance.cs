using Core.Extension;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class PermissionViewInstance : IViewInstanceConstruction
    {
        public string InstanceClassName
        {
            get
            {
                return "Index";
            }
        }

        public JavaScript InitializeViewInstance()
        {
            JavaScript js = new JavaScript("index", "Index");
            Url url = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.GridStateChange));
            js.AddUrlInstance("searchUrl", url);
            return js;
        }
    }
}
