using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Permission;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class PermissionSearchFilterConfiguration : SearchFilterConfiguration<PermissionPostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            searchFilter.Add(new BooleanGridFilter<PermissionPostModel>(o => o.IsEnable, "是否已删除"));
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            Url searchUrl = new Url(nameof(Administration), typeof(PermissionController), nameof(PermissionController.GridStateChange));

            buttons.Add(new StandardButton("搜索", "index.search", searchUrl));
            buttons.Add(new StandardButton("添加", "index.search"));
            buttons.Add(new StandardButton("编辑", "index.search"));
        }
    }
}
