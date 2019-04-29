using Core.Extension;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
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
            Url searchUrl = new Url(nameof(Administration), typeof(IconController), nameof(IconController.GridStateChange));
            Url addUrl = new Url(nameof(Administration), typeof(IconController), nameof(IconController.AddDialog));
            js.AddUrlInstance("searchUrl", searchUrl);
            js.AddUrlInstance("addUrl", addUrl);
            return js;
        }
    }
}
