using System.Collections.Generic;
using Core.Model.Entity;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class IconGridConfiguration : GridConfiguration<Icon>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="count"></param>
        /// <param name="pageSize" />
        /// <param name="currentPage"></param>
        public IconGridConfiguration(IList<Icon> entity, int count = default, int pageSize = default, int currentPage = default) : base(entity, count, pageSize, currentPage)
        {
        }

        public override void GenerateGridColumn()
        {
            GridColumn.AddIconColumn(new IconGridColumn<Icon>(o => o.Code, IconResource.Icon));
            GridColumn.AddTextColumn(new TextGridColumn<Icon>(o => o.Code, IconResource.Code));
            GridColumn.AddTextColumn(new TextGridColumn<Icon>(o => o.Custom, IconResource.Custom));
            GridColumn.AddTextColumn(new TextGridColumn<Icon>(o => o.Size, IconResource.Size));
            GridColumn.AddTextColumn(new TextGridColumn<Icon>(o => o.Color, IconResource.Color));
            GridColumn.AddBooleanColumn(new BooleanGridColumn<Icon>(o => o.IsEnable, IconResource.Status));
            GridColumn.AddDateTimeColumn(new DateTimeGridColumn<Icon>(o => o.CreatedOn, IconResource.CreatedOn));
            GridColumn.AddTextColumn(new TextGridColumn<Icon>(o => o.CreatedByUserName, IconResource.CreatedByUserName));
        }
    }
}
