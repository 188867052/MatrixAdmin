using System.Collections.Generic;
using Core.Model;
using Core.Model.Log;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using Microsoft.Extensions.Logging;
using Resources = Core.Mvc.Resource.Areas.Log.ViewConfiguration.LogGridConfigurationResource;

namespace Core.Mvc.Areas.Log.ViewConfiguration
{
    public class LogGridConfiguration<T> : GridConfiguration<T>
    where T : LogModel
    {
        public LogGridConfiguration(HttpResponseModel response) : base(response)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<T>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<T>(o => "<span class=\"icon-copy\"  onclick=\"index.copy()\" ></span>", Resources.Copy));
            gridColumns.Add(new TextGridColumn<T>(o => o.Message, Resources.Message));
            gridColumns.Add(new EnumGridColumn<T>(o => (LogLevel)o.LogLevel, Resources.LogLevel));
            gridColumns.Add(new EnumGridColumn<T>(o => o.SqlType, Resources.SqlType));
            gridColumns.Add(new DateTimeGridColumn<T>(o => o.CreateTime, Resources.CreateTime));
        }
    }
}
