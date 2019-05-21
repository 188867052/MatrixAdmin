using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;
using Resources = Core.Resource.Areas.Administration.SearchFilterConfigurations.MenuSearchFilterConfigurationResource;

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
            Url searchUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.GridStateChange));
            Url addDialogUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.AddDialog));

            buttons.Add(new StandardButton(Resources.SearchButtonLabel, "index.search", searchUrl));
            buttons.Add(new StandardButton(Resources.AddButtonLabel, "core.dialog", addDialogUrl));
            buttons.Add(new StandardButton(Resources.ClearButtonLabel, "core.clear"));
        }
    }
}
