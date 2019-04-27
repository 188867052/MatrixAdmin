using Core.Model.Log;
using Core.Resource.ViewConfiguration.Log;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;

namespace Core.Mvc.ViewConfiguration.Log
{
    public class LogSearchGridFilterConfiguration : GridFilterConfiguration<LogPostModel>
    {
        public override string GenerateSearchFilter()
        {
            GridSearchFilter.AddIntegerFilter(new IntegerGridFilter<LogPostModel>(o => o.Id, LogResource.ID));
            GridSearchFilter.AddTextFilter(new TextGridFilter<LogPostModel>(o => o.Message, LogResource.Message));
            GridSearchFilter.AddDateTimeFilter(new DateTimeGridFilter<LogPostModel>(o => o.StartTime, "开始" + LogResource.CreateTime));
            GridSearchFilter.AddDateTimeFilter(new DateTimeGridFilter<LogPostModel>(o => o.EndTime, "结束" + LogResource.CreateTime));

            var dropDown = new DropDownGridFilter<LogPostModel>(o => o.Type, "类型", true);
            dropDown.AddOption(LogType.Error, "错误");
            dropDown.AddOption(LogType.Alert, "警告");
            dropDown.AddOption(LogType.Info, "日志");
            dropDown.AddOption(LogType.Debug, "调试");
            GridSearchFilter.AddDropDownGridFilter(dropDown);
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
