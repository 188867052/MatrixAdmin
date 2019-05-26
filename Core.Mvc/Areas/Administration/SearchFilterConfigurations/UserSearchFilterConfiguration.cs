using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Mvc.Framework.AdvancedGridFilters;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;
using Resources = Core.Mvc.Resource.Areas.Administration.SearchFilterConfigurations.UserSearchFilterConfigurationResource;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class UserSearchFilterConfiguration<T> : SearchFilterConfiguration
        where T : UserPostModel
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            var dropDown = new DropDownGridFilter<T, ForbiddenStatusEnum>(o => (ForbiddenStatusEnum)o.ForbiddenStatus, Resources.ForbiddenStatus, tooltip: Resources.ForbiddenStatus);
            dropDown.AddOption(ForbiddenStatusEnum.Normal, Resources.Normal);
            dropDown.AddOption(ForbiddenStatusEnum.Forbidden, Resources.Forbidden);

            searchFilter.Add(new TextGridFilter<T>(o => o.DisplayName, Resources.DisplayName, Resources.DisplayName));
            searchFilter.Add(new TextGridFilter<T>(o => o.LoginName, Resources.LoginName, Resources.LoginName));
            searchFilter.Add(new DateTimeGridFilter<T>(o => o.StartCreateTime, Resources.StartCreateTime, Resources.StartCreateTime));
            searchFilter.Add(new DateTimeGridFilter<T>(o => o.EndCreateTime, Resources.EndCreateTime, Resources.EndCreateTime));
            searchFilter.Add(dropDown);
            searchFilter.Add(AdvancedDropDown.RoleAdvancedDropDown<T>(o => o.RoleId));
            searchFilter.Add(AdvancedDropDown.UserAdvancedDropDown<T>(o => o.Id));
            searchFilter.Add(AdvancedDropDown.MenuAdvancedDropDown<T>(o => o.Id));
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            Url searchUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.GridStateChange));
            Url addDialogUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.AddDialog));

            buttons.Add(new StandardButton(Resources.SearchButtonLabel, "index.search", searchUrl, Resources.SearchButtonLabel));
            buttons.Add(new StandardButton(Resources.AddButtonLabel, "core.dialog", addDialogUrl, Resources.AddButtonLabel));
            buttons.Add(new StandardButton(Resources.ClearButtonLabel, "core.clear", tooltip: Resources.ClearButtonLabel));
        }
    }
}
