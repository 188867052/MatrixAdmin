using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Icon
{
    public class IconGridConfiguration : GridConfiguration<Model.Administration.Icon.Icon>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IconGridConfiguration"/> class.
        /// </summary>
        public IconGridConfiguration(ResponseModel response) : base(response)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Model.Administration.Icon.Icon>> gridColumns)
        {
            gridColumns.Add(new IconGridColumn<Model.Administration.Icon.Icon>(o => o.Code, IconResource.Icon));
            gridColumns.Add(new TextGridColumn<Model.Administration.Icon.Icon>(o => o.Code, IconResource.Code));
            gridColumns.Add(new TextGridColumn<Model.Administration.Icon.Icon>(o => o.Custom, IconResource.Custom));
            gridColumns.Add(new TextGridColumn<Model.Administration.Icon.Icon>(o => o.Size, IconResource.Size));
            gridColumns.Add(new TextGridColumn<Model.Administration.Icon.Icon>(o => o.Color, IconResource.Color));
            gridColumns.Add(new BooleanGridColumn<Model.Administration.Icon.Icon>(o => o.IsEnable, IconResource.Status));
            gridColumns.Add(new DateTimeGridColumn<Model.Administration.Icon.Icon>(o => o.CreatedOn, IconResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Model.Administration.Icon.Icon>(o => o.CreatedByUserName, IconResource.CreatedByUserName));
        }
    }
}
