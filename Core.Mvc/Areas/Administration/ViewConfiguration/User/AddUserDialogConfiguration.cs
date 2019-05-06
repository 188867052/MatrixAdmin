using System.Collections.Generic;
using Core.Model.Administration.User;
using Core.Web.Button;
using Core.Web.Dialog;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.TextBox;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.User
{
    public class AddUserDialogConfiguration : DialogConfiguration<UserCreatePostModel, Model.Administration.User.User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserDialogConfiguration"/> class.
        /// </summary>
        public AddUserDialogConfiguration() : base(null, Identifier)
        {
        }

        public new static Identifier Identifier { get; } = new Identifier();

        public override string Title => "添加用户";

        protected override void CreateBody(IList<ITextRender<UserCreatePostModel, Model.Administration.User.User>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel, Model.Administration.User.User>("登录名", o => o.LoginName));
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel, Model.Administration.User.User>("显示名", o => o.DisplayName));
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel, Model.Administration.User.User>("密码", o => o.Password, null, TextBoxTypeEnum.Password));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("提交",  "index.submit"));
        }
    }
}