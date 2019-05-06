using Core.Extension;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.JavaScript;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
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
            Url searchUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.GridStateChange));
            Url addUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.AddDialog));
            Url editUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.EditDialog));
            Url saveUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.Save));
            Url deleteUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.Delete));
            Url recoverUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.Recover));
            Url rowContextMenuUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.RowContextMenu));

            javaScriptInitialize.AddUrlInstance("searchUrl", searchUrl);
            javaScriptInitialize.AddUrlInstance("addUrl", addUrl);
            javaScriptInitialize.AddUrlInstance("editUrl", editUrl);
            javaScriptInitialize.AddUrlInstance("saveUrl", saveUrl);
            javaScriptInitialize.AddUrlInstance("deleteUrl", deleteUrl);
            javaScriptInitialize.AddUrlInstance("recoverUrl", recoverUrl);
            javaScriptInitialize.AddUrlInstance("rowContextMenuUrl", rowContextMenuUrl);
            javaScriptInitialize.AddFrameWorkInstance("dialogInstance", AddUserDialogConfiguration.Identifier);
        }
    }
}
