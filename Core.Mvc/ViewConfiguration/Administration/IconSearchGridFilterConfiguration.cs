using Core.Model.PostModel;
using Core.Mvc.ViewConfiguration.Log;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class IconSearchGridFilterConfiguration : GridFilterConfiguration<IconPostModel>
    {
        public override string GenerateSearchFilter()
        {
            GridSearchFilter.AddTextFilter(new TextGridFilter<IconPostModel>(o => o.Code,IconResource.Code));
            var filter = new BooleanGridFilter<IconPostModel>(o => o.IsEnable, IconResource.Status);
            filter.AddOption(1, "可用");
            filter.AddOption(0, "不可用");
            GridSearchFilter.AddBooleanFilter(filter);
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
