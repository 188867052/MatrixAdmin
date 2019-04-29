using Core.Extension;
using Core.Mvc.Controllers.Administration;
using Core.Web.JavaScript;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class IconViewInstance : IViewInstanceConstruction
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
