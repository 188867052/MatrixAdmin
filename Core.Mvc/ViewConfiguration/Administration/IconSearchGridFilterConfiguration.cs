using Core.Model.PostModel;
using Core.Mvc.ViewConfiguration.Log;
using Core.Resource.ViewConfiguration.Log;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class IconSearchGridFilterConfiguration : GridFilterConfiguration<IconPostModel>
    {
        public override string GenerateSearchFilter()
        {
            GridSearchFilter.AddIntegerFilter(new IntegerGridFilter<IconPostModel>(o => o.Id, LogResource.ID));
            GridSearchFilter.AddTextFilter(new TextGridFilter<IconPostModel>(o => o.Code, LogResource.Message));
            GridSearchFilter.AddDateTimeFilter(new DateTimeGridFilter<IconPostModel>(o => o.StartTime, "开始" + LogResource.CreateTime));
            GridSearchFilter.AddDateTimeFilter(new DateTimeGridFilter<IconPostModel>(o => o.EndTime, "结束" + LogResource.CreateTime));
            return GridSearchFilter.Render();
        }

        public override string GenerateButton()
        {
            this.Buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            this.Buttons.Add(new StandardButton("添加"));
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
