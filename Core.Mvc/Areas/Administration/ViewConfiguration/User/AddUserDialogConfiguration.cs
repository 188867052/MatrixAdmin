using System.Collections.Generic;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Routes;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.JavaScript;
using Core.Web.TextBox;
using Resources = Core.Mvc.Resource.Areas.Administration.ViewConfiguration.User.AddUserDialogConfigurationResource;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class AddUserDialogConfiguration<TPostModel, TModel> : DialogConfiguration<TPostModel, TModel>
        where TPostModel : UserCreatePostModel
        where TModel : UserModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserDialogConfiguration{TPostModel, TModel}"/> class.
        /// </summary>
        public AddUserDialogConfiguration() : base(UserIdentifiers.AddUserDialogIdentifier)
        {
        }

        public override string Title => Resources.Title;

        protected override void CreateBody(IList<IHtmlContent<TPostModel, TModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.LoginName, o => o.LoginName));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.DisplayName, o => o.DisplayName));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.Description, o => o.Description));
            var roleDropDown = new AdvancedDropDown<TPostModel, TModel>(o => o.UserRole, Resources.UserRole, new MethodCall("core.addOption"));
            textBoxes.Add(roleDropDown);
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.Password, o => o.Password, null, TextBoxTypeEnum.Password));
        }

        protected override void CreateHiddenValues(IList<IHtmlContent<TPostModel, TModel>> textBoxes)
        {
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton(Resources.Submit, "index.submit", UserRoute.SaveCreate));
            buttons.Add(new StandardButton(Resources.Cancel, "core.cancel"));
        }
    }
}