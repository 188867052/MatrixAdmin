using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Permission;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;
using Resources = Core.Resource.Areas.Administration.ViewConfiguration.Permission.PermissionSearchFilterConfigurationResource;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class PermissionSearchFilterConfiguration : SearchFilterConfiguration
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            searchFilter.Add(new BooleanGridFilter<PermissionPostModel>(o => o.IsEnable, Resources.IsEnable));
            searchFilter.Add(new TextGridFilter<PermissionPostModel>(o => o.ActionCode, Resources.ActionCode));
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            Url searchUrl = new Url(nameof(Administration), typeof(PermissionController), nameof(PermissionController.GridStateChange));

            buttons.Add(new StandardButton(Resources.SearchButtonLabel, "index.search", searchUrl));
            buttons.Add(new StandardButton(Resources.AddButtonLabel, "index.search"));
            buttons.Add(new StandardButton(Resources.ClearButtonLabel, "core.clear"));
        }
    }
}
