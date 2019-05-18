using System.Collections.Generic;
using Core.Extension;
using Core.Model.Log;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Button;
using Core.Web.Enums;
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
            var logLevelDropDown = new DropDownGridFilter<LogPostModel, LogLevel>(o => (LogLevel)o.LogLevel, "级别");
            logLevelDropDown.AddOption(LogLevel.Error, "错误");
            logLevelDropDown.AddOption(LogLevel.Information, "信息");
            logLevelDropDown.AddOption(LogLevel.Debug, "调试");

            var sqlTypeDropDown = new DropDownGridFilter<LogPostModel, SqlTypeEnum>(o => (SqlTypeEnum)o.SqlType, "操作类型");
            sqlTypeDropDown.AddOption(SqlTypeEnum.Select, "查找");
            sqlTypeDropDown.AddOption(SqlTypeEnum.Create, "添加");
            sqlTypeDropDown.AddOption(SqlTypeEnum.Update, "更新");
            sqlTypeDropDown.AddOption(SqlTypeEnum.Delete, "删除");
            sqlTypeDropDown.AddOption(SqlTypeEnum.Insert, "插入");

            searchFilter.Add(new IntegerGridFilter<LogPostModel>(o => o.Id, LogResource.ID));
            searchFilter.Add(new TextGridFilter<LogPostModel>(o => o.Message, LogResource.Message));
            searchFilter.Add(new DateTimeGridFilter<LogPostModel>(o => o.StartTime, "开始" + LogResource.CreateTime));
            searchFilter.Add(new DateTimeGridFilter<LogPostModel>(o => o.EndTime, "结束" + LogResource.CreateTime));
            searchFilter.Add(logLevelDropDown);
            searchFilter.Add(sqlTypeDropDown);
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
