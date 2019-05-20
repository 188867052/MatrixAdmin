using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
{
    public class MenuViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName => "Index";

        public override void InitializeViewInstance(JavaScriptInitialize javaScriptInitialize)
        {
        }
    }
}
