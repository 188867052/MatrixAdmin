using System.Collections.Generic;
using Core.Extension;
using Core.Model.Log;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.GridFilter;
using Core.Web.SearchFilterConfiguration;
using Microsoft.Extensions.Logging;
using LogController = Core.Mvc.Areas.Log.Controllers.LogController;

namespace Core.Mvc.Areas.Log.SearchFilterConfigurations
{
    public class LogSearchFilterConfiguration : SearchFilterConfiguration<LogPostModel>
    {
        protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
        {
            var dropDown = new DropDownGridFilter<LogPostModel, LogLevel>(o => (LogLevel)o.LogLevel, "级别");
            dropDown.AddOption(LogLevel.Error, "错误");
            dropDown.AddOption(LogLevel.Information, "信息");
            dropDown.AddOption(LogLevel.Debug, "调试");

            searchFilter.Add(new IntegerGridFilter<LogPostModel>(o => o.Id, LogResource.ID));
            searchFilter.Add(new TextGridFilter<LogPostModel>(o => o.Message, LogResource.Message));
            searchFilter.Add(new DateTimeGridFilter<LogPostModel>(o => o.StartTime, "开始" + LogResource.CreateTime));
            searchFilter.Add(new DateTimeGridFilter<LogPostModel>(o => o.EndTime, "结束" + LogResource.CreateTime));
            searchFilter.Add(dropDown);
        }

        protected override void CreateButton(IList<StandardButton> buttons)
        {
            Url url = new Url(nameof(Log), typeof(LogController), nameof(LogController.Clear));
            Url searchUrl = new Url(nameof(Log), typeof(LogController), nameof(LogController.GridStateChange));
            buttons.Add(new StandardButton("搜索", "index.search", searchUrl));
            buttons.Add(new StandardButton("清理", "index.clear", url));
        }
    }
}
