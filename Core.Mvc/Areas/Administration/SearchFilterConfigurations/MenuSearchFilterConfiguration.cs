using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class MenuSearchFilterConfiguration : SearchFilterConfiguration<MenuPostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            searchFilter.Add(new TextGridFilter<MenuPostModel>(o => o.MenuName, "菜单名称"));
            searchFilter.Add(new DateTimeGridFilter<MenuPostModel>(o => o.StartCreateTime, "开始" + LogResource.CreateTime));
            searchFilter.Add(new DateTimeGridFilter<MenuPostModel>(o => o.EndCreateTime, "结束" + LogResource.CreateTime));
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            Url searchUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.GridStateChange));
            Url addDialogUrl = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.AddDialog));

            buttons.Add(new StandardButton("搜索", "index.search", searchUrl));
            buttons.Add(new StandardButton("添加", "core.dialog", addDialogUrl));
            buttons.Add(new StandardButton("清理", "core.clear"));
        }
    }
}
