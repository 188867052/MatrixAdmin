using Core.Extension;
using Core.Mvc.Areas.Log.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Log.ViewConfiguration
{
    public class LogViewInstance : ViewInstanceConstruction
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
            Url url = new Url(nameof(Log), typeof(LogController), nameof(LogController.GridStateChange));
            javaScriptInitialize.AddUrlInstance("searchUrl", url);
        }
    }
}
