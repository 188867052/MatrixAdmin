using Core.Mvc.Areas.Administration.Routes;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Icon
{
    public class IconViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName => "Index";

        public override void InitializeViewInstance(JavaScriptInitialize initialize)
        {
            initialize.AddUrlInstance("searchUrl", IconRoute.GridStateChange);
            initialize.AddUrlInstance("addUrl", IconRoute.AddDialog);
        }
    }
}
