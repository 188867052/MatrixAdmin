using System.Collections.Generic;
using Core.Model.Administration.Menu;
using Core.Mvc.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class MenuSearchFilterConfiguration : SearchFilterConfiguration<MenuPostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            searchFilter.Add(new TextGridFilter<MenuPostModel>(o => o.MenuName, "菜单名称"));
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            buttons.Add(new StandardButton("添加"));
            buttons.Add(new StandardButton("编辑"));
        }
    }
}
