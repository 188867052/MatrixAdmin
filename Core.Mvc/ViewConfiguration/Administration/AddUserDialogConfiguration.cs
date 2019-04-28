using Core.Model;
using Core.Model.Administration.User;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Administration
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
                return "<button type=\"submit\" class=\"btn btn-primary\">提交</button>" +
              "<button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">关闭</button>";
            }
        }


        public override string Body
        {
            get
            {
                string html = "<div class=\"form-group\">" +
                              "<label for=\"pwd\">登录名:</label>" +
                              "<input type=\"password\" class=\"form-control\" id=\"pwd\">" +
                              "</div>";
                html += "<div class=\"form-group\">" +
                       "<label for=\"pwd1\">密码:</label>" +
                       "<input type=\"password\" class=\"form-control\" id=\"pwd1\">" +
                       "</div>";
                return html;
            }
        }
    }
}