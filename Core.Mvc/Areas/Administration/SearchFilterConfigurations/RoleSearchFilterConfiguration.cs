using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;

namespace Core.Mvc.Areas.Administration.SearchFilterConfigurations
{
    public class RoleSearchFilterConfiguration : SearchFilterConfiguration<RolePostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            searchFilter.Add(new TextGridFilter<RolePostModel>(o => o.RoleName, "角色名称"));
            searchFilter.Add(new TextGridFilter<RolePostModel>(o => o.Description, "描述"));
            searchFilter.Add(new DateTimeGridFilter<RolePostModel>(o => o.StartCreateTime, "开始" + LogResource.CreateTime));
            searchFilter.Add(new DateTimeGridFilter<RolePostModel>(o => o.EndCreateTime, "结束" + LogResource.CreateTime));
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            Url searchUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.GridStateChange));
            Url addDialogUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.AddDialog));

            buttons.Add(new StandardButton("搜索", "index.search", searchUrl));
            buttons.Add(new StandardButton("添加", "core.dialog", addDialogUrl));
        }
    }
}
