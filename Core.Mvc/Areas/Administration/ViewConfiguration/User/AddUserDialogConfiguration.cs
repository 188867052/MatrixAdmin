using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.JavaScript;
using Core.Web.TextBox;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class AddUserDialogConfiguration : DialogConfiguration<UserCreatePostModel, UserModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserDialogConfiguration"/> class.
        /// </summary>
        public AddUserDialogConfiguration() : base(null, Identifier)
        {
        }

        public new static Identifier Identifier { get; } = new Identifier();

        public override string Title => "添加用户";

        protected override void CreateBody(IList<ITextRender<UserCreatePostModel, UserModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel, UserModel>("登录名", o => o.LoginName));
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel, UserModel>("显示名", o => o.DisplayName));
            var roleDropDown = new AdvancedDropDown<UserCreatePostModel, UserModel>(o => o.UserRole, "角色", new MethodCall("core.addOption"));
            textBoxes.Add(roleDropDown);
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel, UserModel>("密码", o => o.Password, null, TextBoxTypeEnum.Password));
        }

        protected override void CreateHiddenValues(IList<ITextRender<UserCreatePostModel, UserModel>> textBoxes)
        {
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            Url saveCreateUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.SaveCreate));

            buttons.Add(new StandardButton("提交", "index.submit", saveCreateUrl));
            buttons.Add(new StandardButton("取消", "core.cancel"));
        }
    }
}