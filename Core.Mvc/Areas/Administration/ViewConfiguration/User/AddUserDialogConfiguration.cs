using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.JavaScript;
using Core.Web.TextBox;
using Resources = Core.Resource.Areas.Administration.ViewConfiguration.User.AddUserDialogConfigurationResource;

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

        protected override void CreateBody(IList<ITextRender<TPostModel, TModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.LoginName, o => o.LoginName));
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.DisplayName, o => o.DisplayName));
            var roleDropDown = new AdvancedDropDown<TPostModel, TModel>(o => o.UserRole, Resources.UserRole, new MethodCall("core.addOption"));
            textBoxes.Add(roleDropDown);
            textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.Password, o => o.Password, null, TextBoxTypeEnum.Password));
        }

        protected override void CreateHiddenValues(IList<ITextRender<TPostModel, TModel>> textBoxes)
        {
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            Url saveCreateUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.SaveCreate));

            buttons.Add(new StandardButton(Resources.Submit, "index.submit", saveCreateUrl));
            buttons.Add(new StandardButton(Resources.Cancel, "core.cancel"));
        }
    }
}