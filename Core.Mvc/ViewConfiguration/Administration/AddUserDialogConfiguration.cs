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
            get
            {
                return "添加用户";

            }
        }


        public override string Footer
        {
            get
            {
                return "<button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">关闭</button>";

            }
        }


        public override string Body
        {
            get
            {
                return "添加用户";

            }
        }
    }
}
