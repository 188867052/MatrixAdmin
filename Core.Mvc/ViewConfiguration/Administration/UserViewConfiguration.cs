using System.Collections.Generic;
using Core.Model.Entity;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class UserViewConfiguration : ViewConfiguration<User>
    {
        public UserViewConfiguration(IList<User> entity) : base(entity)
        {
        }

        public override void GenerateGridColumn()
        {
            GridColumn.AddTextColumn(new TextGridColumn<User>(o => o.LoginName, "登录名"));
            GridColumn.AddTextColumn(new TextGridColumn<User>(o => o.DisplayName, "显示名"));
            GridColumn.AddEnumColumn(new EnumGridColumn<User>(o => o.UserType, "用户类型"));
            GridColumn.AddEnumColumn(new EnumGridColumn<User>(o => o.Status, "状态"));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<User>(o => o.CreatedOn, "创建时间"));
            GridColumn.AddTextColumn(new TextGridColumn<User>(o => o.CreatedByUserName, "创建者"));
        }
    }
}
