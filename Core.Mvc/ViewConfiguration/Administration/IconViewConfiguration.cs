using System.Collections.Generic;
using Core.Model.Entity;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.Grid;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class IconViewConfiguration
    {
        private readonly IList<Icon> _icons;

        public IconViewConfiguration(IList<Icon> icons)
        {
            this._icons = icons;
        }

        public string Render()
        {
            GridColumn<Icon> column = new GridColumn<Icon>(_icons);
            column.AddIconColumn(new IconColumn<Icon>(o => o.Code, IconResource.Icon));
            column.AddTextColumn(new TextColumn<Icon>(o => o.Code, IconResource.Code));
            column.AddTextColumn(new TextColumn<Icon>(o => o.Custom, IconResource.Custom));
            column.AddTextColumn(new TextColumn<Icon>(o => o.Size, IconResource.Size));
            column.AddTextColumn(new TextColumn<Icon>(o => o.Color, IconResource.Color));
            column.AddBooleanColumn(new BooleanColumn<Icon>(o => o.Status, IconResource.Status));
            column.AddDateTimeColumn(new DateTimeColumn<Icon>(o => o.CreatedOn, IconResource.CreatedOn));
            column.AddTextColumn(new TextColumn<Icon>(o => o.CreatedByUserName, IconResource.CreatedByUserName));
            return column.Render();
        }
    }
}
