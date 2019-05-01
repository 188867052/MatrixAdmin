using Core.Model.Administration.User;
using Core.Model.Enums;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;
using System.Collections.Generic;
using Core.Web.SearchFilterConfiguration;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class UserSearchGridFilterConfiguration : SearchFilterConfiguration<UserPostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            var dropDown = new DropDownGridFilter<UserPostModel, UserStatusEnum>(o => (UserStatusEnum)o.Status, "状态");
            dropDown.AddOption(UserStatusEnum.Normal, "正常");
            dropDown.AddOption(UserStatusEnum.Forbidden, "已禁用");
            dropDown.AddOption(UserStatusEnum.All, "未指定");

            searchFilter.Add(new TextGridFilter<UserPostModel>(o => o.DisplayName, "显示名"));
            searchFilter.Add(new TextGridFilter<UserPostModel>(o => o.LoginName, "登录名"));
            searchFilter.Add(new DateTimeGridFilter<UserPostModel>(o => o.CreatedOn, "开始" + LogResource.CreateTime));
            searchFilter.Add(new DateTimeGridFilter<UserPostModel>(o => o.CreatedOn, "结束" + LogResource.CreateTime));
            searchFilter.Add(dropDown);
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            buttons.Add(new StandardButton("添加", new Identifier(), "index.add"));
        }
    }
}
