using Core.Entity.Enums;
using Core.Model.Administration.User;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;
using System.Collections.Generic;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class EditUserDialogConfiguration : DialogConfiguration<UserEditPostModel, ConsoleApp.DataModels.User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditUserDialogConfiguration"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public EditUserDialogConfiguration(ConsoleApp.DataModels.User user) : base(user, Identifier)
        {
        }

        public new static Identifier Identifier { get; } = new Identifier();

        public override string Title => "编辑用户";

        protected override void CreateHiddenValues(IList<ITextRender<UserEditPostModel, ConsoleApp.DataModels.User>> textBoxes)
        {
            textBoxes.Add(new HiddenTextBox<UserEditPostModel, ConsoleApp.DataModels.User>(o => o.Id, this.Model.Id));
        }

        protected override void CreateBody(IList<ITextRender<UserEditPostModel, ConsoleApp.DataModels.User>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, ConsoleApp.DataModels.User>("登录名", o => o.LoginName, o => o.LoginName));
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, ConsoleApp.DataModels.User>("显示名", o => o.DisplayName, o => o.DisplayName));

            var dropDown = new DropDownTextBox<UserEditPostModel, ConsoleApp.DataModels.User>("角色", o => o.UserRole, false);
            dropDown.AddOption((int)UserRoleEnum.GeneralUser, "一般用户");
            dropDown.AddOption((int)UserRoleEnum.Admin, "管理员");
            dropDown.AddOption((int)UserRoleEnum.SuperAdministrator, "超级管理员");
            textBoxes.Add(dropDown);
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, ConsoleApp.DataModels.User>("密码", o => o.Password, o => o.Password, TextBoxTypeEnum.Password));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("提交", "index.submit"));
            buttons.Add(new StandardButton("取消", "core.cancel"));
        }
    }
}