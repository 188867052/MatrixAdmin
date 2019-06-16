using System.Collections.Generic;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.Routes;
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
            var dropDown = new BooleanGridFilter<T>(o => o.IsLocked, Resources.ForbiddenStatus, tooltip: Resources.ForbiddenStatus);
            dropDown.AddOption(true, Resources.Normal);
            dropDown.AddOption(false, Resources.Forbidden);

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
            buttons.Add(new StandardButton(Resources.SearchButtonLabel, "index.search", UserRoute.GridStateChange, Resources.SearchButtonLabel));
            buttons.Add(new StandardButton(Resources.AddButtonLabel, "core.dialog", UserRoute.AddDialog, Resources.AddButtonLabel));
            buttons.Add(new StandardButton(Resources.ClearButtonLabel, "core.clear", tooltip: Resources.ClearButtonLabel));
        }
    }
}
