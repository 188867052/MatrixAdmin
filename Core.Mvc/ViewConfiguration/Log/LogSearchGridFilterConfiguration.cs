using Core.Resource.ViewConfiguration.Log;
using Core.Web.GridFilter;
using System;
using System.Collections.Generic;
using Core.Mvc.Controllers;
using Core.Web.Button;
using Core.Web.GridFilter_backup;
using Core.Web.Identifiers;

namespace Core.Mvc.ViewConfiguration.Log
{
    public class LogSearchGridFilterConfiguration : GridFilterConfiguration
    {
        public LogSearchGridFilterConfiguration()
        {
            this.Buttons = new List<StandardButton>();
        }
        public override string GenerateSearchFilter()
        {
            var filter = new GridSearchFilter<LogPostModel>();
            filter.AddIntegerFilter(new IntegerGridFilter<LogPostModel>(o => o.Id, LogResource.ID));
            filter.AddTextFilter(new TextGridFilter<LogPostModel>(o => o.Message, LogResource.Message));
            filter.AddDateTimeFilter(new DateTimeGridFilter("开始" + LogResource.CreateTime));
            filter.AddDateTimeFilter(new DateTimeGridFilter("结束" + LogResource.CreateTime));
            filter.AddDropDownGridFilter(new DropDownGridFilter("天数"));
            filter.AddDropDownGridFilter(new DropDownGridFilter("价格"));
            return filter.Render();
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
