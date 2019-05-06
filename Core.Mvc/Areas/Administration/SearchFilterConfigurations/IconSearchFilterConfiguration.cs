using System.Collections.Generic;
using Core.Model.Administration.Icon;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;
using Core.Web.SearchFilterConfiguration;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class IconSearchFilterConfiguration : SearchFilterConfiguration<IconPostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            searchFilter.Add(new TextGridFilter<IconPostModel>(o => o.Code, IconResource.Code));
            var filter = new BooleanGridFilter<IconPostModel>(o => o.IsEnable, IconResource.Status);
            filter.AddOption(true, "可用");
            filter.AddOption(false, "不可用");
            searchFilter.Add(filter);
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            buttons.Add(new StandardButton("添加"));
            buttons.Add(new StandardButton("编辑"));
        }
    }
}
