using Core.Model;
using Core.Model.Administration.User;
using Core.Web.Button;
using Core.Web.Identifiers;
using Core.Web.TextBox;
using Core.Web.ViewConfiguration;
using System.Collections.Generic;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class AddUserDialogConfiguration : DialogConfiguration<UserCreatePostModel>
    {
        public static Identifier Identifier { get; } = new Identifier();
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        public AddUserDialogConfiguration(ResponseModel entity) : base(entity, Identifier)
        {
        }

        public override string Title => "添加用户";

        protected override void CreateBody(IList<LabeledTextBox<UserCreatePostModel>> textBoxes)
        {
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel>(o => o.LoginName, "登录名"));
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel>(o => o.DisplayName, "显示名"));
            textBoxes.Add(new LabeledTextBox<UserCreatePostModel>(o => o.Password, "密码", TextBoxTypeEnum.password));
        }

        protected override void CreateButtons(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("提交", new Identifier(), "index.submit"));
        }
    }
}