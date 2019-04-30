using System.Collections.Generic;
using Core.Model.Administration.User;
using Core.Model.Enums;
using Core.Model.Log;
using Core.Mvc.ViewConfiguration;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class UserSearchGridFilterConfiguration : GridFilterConfiguration<UserPostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            var dropDown = new DropDownGridFilter<UserPostModel, UserStatusEnum>(o => o.Status, "状态");
            dropDown.AddOption(UserStatusEnum.All, "错误");
            dropDown.AddOption(UserStatusEnum.Forbidden, "警告");
            dropDown.AddOption(UserStatusEnum.Normal, "日志");

            searchFilter.Add(new TextGridFilter<UserPostModel>(o => o.DisplayName, LogResource.Message));
            searchFilter.Add(new DateTimeGridFilter<UserPostModel>(o => o.CreatedOn, "开始" + LogResource.CreateTime));
            searchFilter.Add(new DateTimeGridFilter<UserPostModel>(o => o.CreatedOn, "结束" + LogResource.CreateTime));
            searchFilter.Add(dropDown);
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            buttons.Add(new StandardButton("添加", new Identifier(), "index.add"));
            buttons.Add(new StandardButton("编辑", new Identifier(), "index.edit"));
        }
    }
}
