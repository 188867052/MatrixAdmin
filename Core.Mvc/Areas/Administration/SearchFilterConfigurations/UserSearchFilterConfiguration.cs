using System.Collections.Generic;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model.Administration.User;
using Core.Mvc.AdvancedGridFilters;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;
using Resources = Core.Resource.Areas.Administration.SearchFilterConfigurations.UserSearchFilterConfigurationResource;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class UserSearchFilterConfiguration<T> : SearchFilterConfiguration
        where T : UserPostModel
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            var dropDown = new DropDownGridFilter<T, ForbiddenStatusEnum>(o => (ForbiddenStatusEnum)o.ForbiddenStatus, Resources.ForbiddenStatus);
            dropDown.AddOption(ForbiddenStatusEnum.Normal, Resources.Normal);
            dropDown.AddOption(ForbiddenStatusEnum.Forbidden, Resources.Forbidden);

            searchFilter.Add(new TextGridFilter<T>(o => o.DisplayName, Resources.DisplayName));
            searchFilter.Add(new TextGridFilter<T>(o => o.LoginName, Resources.LoginName));
            searchFilter.Add(new DateTimeGridFilter<T>(o => o.StartCreateTime, Resources.StartCreateTime));
            searchFilter.Add(new DateTimeGridFilter<T>(o => o.EndCreateTime, Resources.EndCreateTime));
            searchFilter.Add(dropDown);
            searchFilter.Add(AdvancedDropDown<T>.RoleAdvancedDropDown(o => o.RoleId));
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
