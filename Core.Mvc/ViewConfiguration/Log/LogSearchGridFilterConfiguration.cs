using Core.Resource.ViewConfiguration.Log;
using Core.Web.GridFilter;
using System;

namespace Core.Mvc.ViewConfiguration.Log
{
    public class LogSearchGridFilterConfiguration : GridFilterConfiguration
    {
        public override string GenerateSearchFilter()
        {
            var filter = new GridSearchFilter();
            filter.AddTextFilter(new TextGridFilter(LogResource.ID));
            filter.AddTextFilter(new TextGridFilter(LogResource.Message));
            filter.AddDateTimeFilter(new DateTimeGridFilter("开始"+LogResource.CreateTime));
            filter.AddDateTimeFilter(new DateTimeGridFilter("结束" + LogResource.CreateTime));
            return filter.Render();
        }

        public override string GenerateButton()
        {
            string button = $"<button type=\"submit\" class=\"btn btn-primary\">搜索</button>" + Environment.NewLine;
            button += $"<button type=\"submit\" class=\"btn btn-primary\">添加</button>" + Environment.NewLine;
            button += $"<button type=\"submit\" class=\"btn btn-primary\">编辑</button>";
            return button;
        }
    }
}
