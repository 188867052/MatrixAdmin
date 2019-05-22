using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Icon;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;
using Resources = Core.Resource.Areas.Administration.SearchFilterConfigurations.IconSearchFilterConfigurationResource;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class IconSearchFilterConfiguration<T> : SearchFilterConfiguration
            where T : IconPostModel
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            searchFilter.Add(new TextGridFilter<T>(o => o.Code, Resources.Code, Resources.Code));
            var filter = new BooleanGridFilter<T>(o => o.IsEnable, Resources.Status, tooltip: Resources.Status);
            filter.AddOption(true, Resources.Enable);
            filter.AddOption(false, Resources.Disable);
            searchFilter.Add(filter);
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            Url searchUrl = new Url(nameof(Administration), typeof(IconController), nameof(IconController.GridStateChange));
            buttons.Add(new StandardButton(Resources.SearchButtonLabel, "index.search", searchUrl, Resources.SearchButtonLabel));
            buttons.Add(new StandardButton(Resources.ClearButtonLabel, "core.clear", tooltip: Resources.ClearButtonToolTip));
        }
    }
}
