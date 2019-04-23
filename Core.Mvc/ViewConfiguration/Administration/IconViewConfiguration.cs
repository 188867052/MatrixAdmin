using System.Collections.Generic;
using Core.Model.Entity;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class IconViewConfiguration : ViewConfiguration<Icon>
    {
        public IconViewConfiguration(IList<Icon> entity) : base(entity)
        {
        }

        public override string Render()
        {
            GridColumn<Icon> column = new GridColumn<Icon>(base.Entity);
            column.AddIconColumn(new IconGridColumn<Icon>(o => o.Code, IconResource.Icon));
            column.AddTextColumn(new TextGridColumn<Icon>(o => o.Code, IconResource.Code));
            column.AddTextColumn(new TextGridColumn<Icon>(o => o.Custom, IconResource.Custom));
            column.AddTextColumn(new TextGridColumn<Icon>(o => o.Size, IconResource.Size));
            column.AddTextColumn(new TextGridColumn<Icon>(o => o.Color, IconResource.Color));
            column.AddBooleanColumn(new BooleanGridColumn<Icon>(o => o.Status, IconResource.Status));
            column.AddDateTimeColumn(new DateTimeGridColumn<Icon>(o => o.CreatedOn, IconResource.CreatedOn));
            column.AddTextColumn(new TextGridColumn<Icon>(o => o.CreatedByUserName, IconResource.CreatedByUserName));
            return column.Render();
        }


    }
}
