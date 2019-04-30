using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Core.Model.Administration.User;
using Core.Web.Button;
using Core.Web.Identifiers;
using Core.Web.TextBox;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class AddUserDialogConfiguration : DialogConfiguration<User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        public AddUserDialogConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override string Title
        {
            get { return "添加用户"; }
        }


        public override string Footer
        {
            get
            {
                IList<StandardButton> buttons = new List<StandardButton>();
                buttons.Add(new StandardButton("提交", new Identifier(), "index.submit"));
                return buttons.Aggregate<StandardButton, string>(default, (current, button) => current + button.Render()) +
                  "<button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">关闭</button>";
            }
        }


        public override string Body
        {
            get
            {
                List<LabeledTextBox<UserPostModel>> list = new List<LabeledTextBox<UserPostModel>>
                {
                    new LabeledTextBox<UserPostModel>(o => o.LoginName, "登录名"),
                    new LabeledTextBox<UserPostModel>(o => o.DisplayName, "显示名"),
                    new LabeledTextBox<UserPostModel>(o => o.Password, "密码")
                };

                string html = default;
                foreach (var item in list)
                {
                    html += item.Render();
                }
                return html;
            }
        }
    }
}