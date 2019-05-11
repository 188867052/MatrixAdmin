using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class UserViewInstance : ViewInstanceConstruction
    {
        protected override string InstanceClassName => "Index";

        public override void InitializeViewInstance(JavaScriptInitialize javaScriptInitialize)
        {
            javaScriptInitialize.AddFrameWorkInstance("dialogInstance", AddUserDialogConfiguration.Identifier);
        }
    }
}
