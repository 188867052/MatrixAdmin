using System.Collections.Generic;
using Core.Model.Log;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.Identifiers;
using Core.Web.SearchFilterConfiguration;

namespace Core.Mvc.Areas.Log.SearchFilterConfigurations
{
    public class LogSearchFilterConfiguration : SearchFilterConfiguration<LogPostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            var dropDown = new DropDownGridFilter<LogPostModel, LogType>(o => (LogType)o.Type, "类型");
            dropDown.AddOption(LogType.Error, "错误");
            dropDown.AddOption(LogType.Alert, "警告");
            dropDown.AddOption(LogType.Info, "日志");
            dropDown.AddOption(LogType.Debug, "调试");

            searchFilter.Add(new IntegerGridFilter<LogPostModel>(o => o.Id, LogResource.ID));
            searchFilter.Add(new TextGridFilter<LogPostModel>(o => o.Message, LogResource.Message));
            searchFilter.Add(new DateTimeGridFilter<LogPostModel>(o => o.StartTime, "开始" + LogResource.CreateTime));
            searchFilter.Add(new DateTimeGridFilter<LogPostModel>(o => o.EndTime, "结束" + LogResource.CreateTime));
            searchFilter.Add(dropDown);
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            buttons.Add(new StandardButton("搜索", new Identifier(), "index.search"));
            buttons.Add(new StandardButton("添加"));
            buttons.Add(new StandardButton("编辑"));
        }
    }
}
