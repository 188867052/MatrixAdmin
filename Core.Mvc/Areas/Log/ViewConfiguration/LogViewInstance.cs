using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Log.ViewConfiguration
{
    public class LogViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName { get; } = "Index";

        public override void InitializeViewInstance(JavaScriptInitialize javaScriptInitialize)
        {
        }
    }
}
