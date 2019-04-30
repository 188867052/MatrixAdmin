using Core.Extension;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class IconViewInstance : ViewInstanceConstruction
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
            Url searchUrl = new Url(nameof(Administration), typeof(IconController), nameof(IconController.GridStateChange));
            Url addUrl = new Url(nameof(Administration), typeof(IconController), nameof(IconController.AddDialog));
            javaScriptInitialize.AddUrlInstance("searchUrl", searchUrl);
            javaScriptInitialize.AddUrlInstance("addUrl", addUrl);
        }
    }
}
