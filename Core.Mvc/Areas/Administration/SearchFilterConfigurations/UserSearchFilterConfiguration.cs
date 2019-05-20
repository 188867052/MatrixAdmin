using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.JavaScript;
using Core.Web.SearchFilterConfiguration;
using Resources = Core.Resource.Areas.Administration.ViewConfiguration.User.UserSearchFilterConfigurationResource;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class UserSearchFilterConfiguration : SearchFilterConfiguration<UserPostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            var dropDown = new DropDownGridFilter<UserPostModel, ForbiddenStatusEnum>(o => (ForbiddenStatusEnum)o.ForbiddenStatus, Resources.ForbiddenStatus);
            dropDown.AddOption(ForbiddenStatusEnum.Normal, Resources.Normal);
            dropDown.AddOption(ForbiddenStatusEnum.Forbidden, Resources.Forbidden);

            var roleDropDown = new AdvancedDropDownGridFilter<UserPostModel>(o => o.RoleId, Resources.Role, new MethodCall("core.addOption"));

            searchFilter.Add(new TextGridFilter<UserPostModel>(o => o.DisplayName, Resources.DisplayName));
            searchFilter.Add(new TextGridFilter<UserPostModel>(o => o.LoginName, Resources.LoginName));
            searchFilter.Add(new DateTimeGridFilter<UserPostModel>(o => o.StartCreateTime, Resources.StartCreateTime));
            searchFilter.Add(new DateTimeGridFilter<UserPostModel>(o => o.EndCreateTime, Resources.EndCreateTime));
            searchFilter.Add(dropDown);
            searchFilter.Add(roleDropDown);
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            Url searchUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.GridStateChange));
            Url addDialogUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.AddDialog));


            buttons.Add(new StandardButton(Resources.SearchButtonLabel, "index.search", searchUrl));
            buttons.Add(new StandardButton(Resources.AddButtonLabel, "core.dialog", addDialogUrl));
            buttons.Add(new StandardButton(Resources.ClearButtonLabel, "core.clear"));
        }
    }
}
