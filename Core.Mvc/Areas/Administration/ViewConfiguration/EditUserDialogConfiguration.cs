using Core.Model.Administration.User;
using Core.Web.Button;
using Core.Web.Identifiers;
using Core.Web.TextBox;
using Core.Web.ViewConfiguration;
using System.Collections.Generic;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class EditUserDialogConfiguration : DialogConfiguration<UserEditPostModel, User>
    {
        public static Identifier Identifier { get; } = new Identifier();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="user"></param>
        public EditUserDialogConfiguration(User user) : base(user, Identifier)
        {
        }

        public override string Title => "编辑用户";

        protected override void CreateBody(IList<LabeledTextBox<UserEditPostModel, User>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, User>("登录名", o => o.LoginName, o => o.LoginName));
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, User>("显示名", o => o.DisplayName, o => o.DisplayName));
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, User>("显示名", o => o.DisplayName, o => o.DisplayName));
            textBoxes.Add(new LabeledTextBox<UserEditPostModel, User>("密码", o => o.Password, o => o.Password, TextBoxTypeEnum.password));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("提交", new Identifier(), "index.submit"));
        }
    }
}