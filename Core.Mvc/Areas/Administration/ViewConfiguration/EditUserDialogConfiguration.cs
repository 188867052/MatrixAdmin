using System.Collections.Generic;
using Core.Model.Administration.User;
using Core.Model.Enums;
using Core.Web.Button;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class EditUserDialogConfiguration : DialogConfiguration<UserEditPostModel, User>
    {
        public static Identifier Identifier { get; } = new Identifier();

        /// <summary>
        /// Initializes a new instance of the <see cref="EditUserDialogConfiguration"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="user"></param>
        public EditUserDialogConfiguration(User user) : base(user, Identifier)
        {
        }

        public override string Title => "编辑用户";

        protected override void CreateBody(IList<ITextRender<UserEditPostModel, User>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, User>("登录名", o => o.LoginName, o => o.LoginName));
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, User>("显示名", o => o.DisplayName, o => o.DisplayName));

            var dropDown = new DropDownTextBox<UserEditPostModel, User>("状态");
            dropDown.AddOption((int)UserStatusEnum.All, "所有");
            dropDown.AddOption((int)UserStatusEnum.Forbidden, "禁止");
            textBoxes.Add(dropDown);
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, User>("密码", o => o.Password, o => o.Password, TextBoxTypeEnum.password));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("提交", new Identifier(), "index.submit"));
        }
    }
}