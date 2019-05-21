using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;
using Resources = Core.Resource.Areas.Administration.SearchFilterConfigurations.RoleSearchFilterConfigurationResource;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class RoleSearchFilterConfiguration<T> : SearchFilterConfiguration
        where T : RolePostModel
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            searchFilter.Add(new TextGridFilter<T>(o => o.RoleName, Resources.RoleName));
            searchFilter.Add(new TextGridFilter<T>(o => o.Description, Resources.Description));
            searchFilter.Add(new DateTimeGridFilter<T>(o => o.StartCreateTime, Resources.StartCreateTime));
            searchFilter.Add(new DateTimeGridFilter<T>(o => o.EndCreateTime, Resources.EndCreateTime));
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            Url searchUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.GridStateChange));
            Url addDialogUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.AddDialog));

            buttons.Add(new StandardButton(Resources.SearchButtonLabel, "index.search", searchUrl));
            buttons.Add(new StandardButton(Resources.AddButtonLabel, "core.dialog", addDialogUrl));
            buttons.Add(new StandardButton(Resources.ClearButtonLabel, "core.clear"));
        }
    }
}
