using Core.Model.Administration.User;
using Core.Resource.ViewConfiguration.Log;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.ViewConfiguration.Administration
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

        public override string GenerateButton()
        {
            this.Buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            this.Buttons.Add(new StandardButton("添加", new Identifier(), "index.add"));
            this.Buttons.Add(new StandardButton("编辑"));
            string html = default;
            string script = default;

            foreach (var button in Buttons)
            {
                html += button.Render();
                script += button.Event.Render();
            }

            script = $"<script>{script}</script>";
            return html + script;
        }
    }
}
