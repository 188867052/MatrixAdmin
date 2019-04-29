using System.Collections.Generic;
using Core.Model.Administration.Icon;
using Core.Mvc.ViewConfiguration;
using Core.Resource;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class IconSearchGridFilterConfiguration : GridFilterConfiguration<IconPostModel>
    {
        public override string GenerateSearchFilter()
        {
            GridSearchFilter.AddTextFilter(new TextGridFilter<IconPostModel>(o => o.Code, IconResource.Code));
            var filter = new BooleanGridFilter<IconPostModel>(o => o.IsEnable, IconResource.Status);
            filter.AddOption(true, "可用");
            filter.AddOption(false, "不可用");
            GridSearchFilter.AddBooleanFilter(filter);
            return GridSearchFilter.Render();
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            buttons.Add(new StandardButton("添加"));
            buttons.Add(new StandardButton("编辑"));
        }
    }
}
