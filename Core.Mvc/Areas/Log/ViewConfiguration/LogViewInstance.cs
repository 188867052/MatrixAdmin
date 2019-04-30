using Core.Extension;
using Core.Mvc.Areas.Log.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Log.ViewConfiguration
{
    public class LogViewInstance : IViewInstanceConstruction
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
            Url url = new Url(nameof(Log), typeof(LogController), nameof(LogController.GridStateChange));
            js.AddUrlInstance("searchUrl", url);
            return js;
        }
    }
}
