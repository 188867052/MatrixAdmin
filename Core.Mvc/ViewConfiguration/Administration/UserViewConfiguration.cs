﻿using System.Collections.Generic;
using Core.Model.Entity;
using Core.Web.GridColumn;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class UserViewConfiguration
    {
        private readonly IList<User> users;

        public UserViewConfiguration(IList<User> users)
        {
            this.users = users;
        }

        public string Render()
        {
            GridColumn<User> column = new GridColumn<User>(users);
            column.AddTextColumn(new TextGridColumn<User>(o => o.LoginName, "登录名"));
            column.AddTextColumn(new TextGridColumn<User>(o => o.DisplayName, "显示名"));
            column.AddEnumColumn(new EnumGridColumn<User>(o => o.UserType, "用户类型"));
            column.AddEnumColumn(new EnumGridColumn<User>(o => o.Status, "状态"));
            column.AddDateTimeColumn(new DateTimeGridColumn<User>(o => o.CreatedOn, "创建时间"));
            column.AddTextColumn(new TextGridColumn<User>(o => o.CreatedByUserName, "创建者"));
            return column.Render();
        }
    }
}
