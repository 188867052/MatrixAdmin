using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Log.ViewConfiguration;
using Core.Web.Enums;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using Microsoft.Extensions.Logging;

namespace Core.Mvc.Areas.Log.ViewConfiguration
{
    public class LogGridConfiguration : GridConfiguration<Entity.Log>
    {
        public LogGridConfiguration(ResponseModel response) : base(response)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Entity.Log>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<Entity.Log>(o => "<span class=\"icon-copy\"  onclick=\"index.copy()\" ></span>", "复制"));
            gridColumns.Add(new TextGridColumn<Entity.Log>(o => o.Message, LogResource.Message));
            gridColumns.Add(new EnumGridColumn<Entity.Log>(o => (LogLevel)o.LogLevel, "级别"));
            gridColumns.Add(new EnumGridColumn<Entity.Log>(o => (SqlTypeEnum)o.SqlOperateType, "操作类型"));
            gridColumns.Add(new DateTimeGridColumn<Entity.Log>(o => o.CreateTime, LogResource.CreateTime));
        }
    }
}
