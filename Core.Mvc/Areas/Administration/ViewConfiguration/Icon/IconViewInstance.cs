using Core.Extension;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Icon
{
    public class IconViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName => "Index";

        public override void InitializeViewInstance(JavaScriptInitialize initialize)
        {
            Url searchUrl = new Url(nameof(Administration), typeof(IconController), nameof(IconController.GridStateChange));
            Url addUrl = new Url(nameof(Administration), typeof(IconController), nameof(IconController.AddDialog));
            initialize.AddUrlInstance("searchUrl", searchUrl);
            initialize.AddUrlInstance("addUrl", addUrl);
        }
    }
}
