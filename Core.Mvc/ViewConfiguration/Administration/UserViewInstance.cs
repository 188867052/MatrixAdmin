using Core.Extension;
using Core.Mvc.Controllers.Administration;
using Core.Web.JavaScript;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class UserViewInstance : IViewInstanceConstruction
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
            Url url = new Url(nameof(Administration), typeof(UserController), nameof(UserController.GridStateChange));
            Url addUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.AddDialog));
            js.AddUrlInstance("searchUrl", url);
            js.AddUrlInstance("addUrl", addUrl);
            return js;
        }
    }
}
