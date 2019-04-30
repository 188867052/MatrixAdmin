using System.Collections.Generic;
using Core.Model.Administration.Role;
using Core.Mvc.ViewConfiguration;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class RoleSearchGridFilterConfiguration : GridFilterConfiguration<RolePostModel>
    {
        public override string GenerateSearchFilter()
        {
            GridSearchFilter.AddBooleanFilter(new BooleanGridFilter<RolePostModel>(o => o.IsEnable, LogResource.Message));

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
