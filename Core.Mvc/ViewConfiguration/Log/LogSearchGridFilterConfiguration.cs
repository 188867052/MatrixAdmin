using System.Collections.Generic;
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

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            buttons.Add(new StandardButton("添加"));
            buttons.Add(new StandardButton("编辑"));
        }
    }
}
