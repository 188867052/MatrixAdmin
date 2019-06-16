using System.Collections.Generic;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.Routes;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;
using Resources = Core.Mvc.Resource.Areas.Administration.SearchFilterConfigurations.MenuSearchFilterConfigurationResource;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class MenuSearchFilterConfiguration<T> : SearchFilterConfiguration
        where T : MenuPostModel
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            searchFilter.Add(new TextGridFilter<T>(o => o.Name, Resources.MenuName));
            searchFilter.Add(new DateTimeGridFilter<T>(o => o.StartCreateTime, Resources.StartCreateTime));
            searchFilter.Add(new DateTimeGridFilter<T>(o => o.EndCreateTime, Resources.EndCreateTime));
            searchFilter.Add(new TextGridFilter<T>(o => o.Description, Resources.Description));
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton(Resources.SearchButtonLabel, "index.search", MenuRoute.GridStateChange));
            buttons.Add(new StandardButton(Resources.AddButtonLabel, "core.dialog", MenuRoute.AddDialog));
            buttons.Add(new StandardButton(Resources.ClearButtonLabel, "core.clear"));
        }
    }
}
