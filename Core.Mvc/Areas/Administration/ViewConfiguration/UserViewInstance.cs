using Core.Extension;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class UserViewInstance : ViewInstanceConstruction
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
            Url url = new Url(nameof(Administration), typeof(UserController), nameof(UserController.GridStateChange));
            Url addUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.AddDialog));
            Url editUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.EditDialog));
            javaScriptInitialize.AddUrlInstance("searchUrl", url);
            javaScriptInitialize.AddUrlInstance("addUrl", addUrl);
            javaScriptInitialize.AddUrlInstance("editUrl", editUrl);
        }
    }
}
