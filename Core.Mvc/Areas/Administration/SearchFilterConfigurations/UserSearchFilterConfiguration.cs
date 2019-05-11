using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.JavaScript;
using Core.Web.SearchFilterConfiguration;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class UserSearchFilterConfiguration : SearchFilterConfiguration<UserPostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            var dropDown = new DropDownGridFilter<UserPostModel, IsForbiddenEnum>(o => (IsForbiddenEnum)o.Status, "禁用状态");
            dropDown.AddOption(IsForbiddenEnum.Normal, "启用");
            dropDown.AddOption(IsForbiddenEnum.Forbidden, "禁用");

            var roleDropDown = new AdvancedDropDownGridFilter<UserPostModel>(o => o.RoleId, "角色", new MethodCall("core.addOption"));

            searchFilter.Add(new TextGridFilter<UserPostModel>(o => o.DisplayName, "显示名"));
            searchFilter.Add(new TextGridFilter<UserPostModel>(o => o.LoginName, "登录名"));
            searchFilter.Add(new DateTimeGridFilter<UserPostModel>(o => o.StartCreateTime, "开始" + LogResource.CreateTime));
            searchFilter.Add(new DateTimeGridFilter<UserPostModel>(o => o.EndCreateTime, "结束" + LogResource.CreateTime));
            searchFilter.Add(dropDown);
            searchFilter.Add(roleDropDown);
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            Url searchUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.GridStateChange));
            Url addDialogUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.AddDialog));

            buttons.Add(new StandardButton("搜索", "index.search", searchUrl));
            buttons.Add(new StandardButton("添加", "index.add", addDialogUrl));
        }
    }
}
