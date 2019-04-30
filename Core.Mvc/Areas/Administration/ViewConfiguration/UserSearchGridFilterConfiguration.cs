using System.Collections.Generic;
using Core.Model.Administration.User;
using Core.Mvc.ViewConfiguration;
using Core.Resource;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class UserSearchGridFilterConfiguration : GridFilterConfiguration<UserPostModel>
    {
        public override string GenerateSearchFilter()
        {
            GridSearchFilter.AddTextFilter(new TextGridFilter<UserPostModel>(o => o.DisplayName, LogResource.Message));
            GridSearchFilter.AddDateTimeFilter(new DateTimeGridFilter<UserPostModel>(o => o.CreatedOn, "开始" + LogResource.CreateTime));
            GridSearchFilter.AddDateTimeFilter(new DateTimeGridFilter<UserPostModel>(o => o.CreatedOn, "结束" + LogResource.CreateTime));
            GridSearchFilter.AddBooleanFilter(new BooleanGridFilter<UserPostModel>(o => o.IsEnable, "是否启用"));
            return GridSearchFilter.Render();
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            buttons.Add(new StandardButton("添加", new Identifier(), "index.add"));
            buttons.Add(new StandardButton("编辑", new Identifier(), "index.edit"));
        }
    }
}
