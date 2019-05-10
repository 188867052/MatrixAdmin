using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Model.Administration.User;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class EditUserDialogConfiguration : DialogConfiguration<UserEditPostModel, UserModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditUserDialogConfiguration"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public EditUserDialogConfiguration(UserModel user) : base(user, Identifier)
        {
        }

        public new static Identifier Identifier { get; } = new Identifier();

        public override string Title => "编辑用户";

        protected override void CreateHiddenValues(IList<ITextRender<UserEditPostModel, UserModel>> textBoxes)
        {
            textBoxes.Add(new HiddenTextBox<UserEditPostModel, UserModel>(o => o.Id, this.Model.Id));
        }

        protected override void CreateBody(IList<ITextRender<UserEditPostModel, UserModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, UserModel>("登录名", o => o.LoginName, o => o.LoginName));
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, UserModel>("显示名", o => o.DisplayName, o => o.DisplayName));

            var dropDown = new DropDownTextBox<UserEditPostModel, UserModel>("角色", o => o.UserRole, false);
            dropDown.AddOption((int)UserRoleEnum.GeneralUser, "普通用户", this.Model.UserRole == UserRoleEnum.GeneralUser);
            dropDown.AddOption((int)UserRoleEnum.Admin, "管理员", this.Model.UserRole == UserRoleEnum.Admin);
            dropDown.AddOption((int)UserRoleEnum.SuperAdministrator, "超级管理员", this.Model.UserRole == UserRoleEnum.SuperAdministrator);
            textBoxes.Add(dropDown);
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, UserModel>("密码", o => o.Password, o => o.Password, TextBoxTypeEnum.Password));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("提交", "index.submit"));
            buttons.Add(new StandardButton("取消", "core.cancel"));
        }
    }
}