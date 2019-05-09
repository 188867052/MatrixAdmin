using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Icon
{
    public class IconGridConfiguration : GridConfiguration<Entity.DataModels.Icon>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IconGridConfiguration"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public IconGridConfiguration(ResponseModel model) : base(model)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Entity.DataModels.Icon>> gridColumns)
        {
            gridColumns.Add(new IconGridColumn<Entity.DataModels.Icon>(o => o.Code, IconResource.Icon));
            gridColumns.Add(new TextGridColumn<Entity.DataModels.Icon>(o => o.Code, IconResource.Code));
            gridColumns.Add(new TextGridColumn<Entity.DataModels.Icon>(o => o.Custom, IconResource.Custom));
            gridColumns.Add(new TextGridColumn<Entity.DataModels.Icon>(o => o.Size, IconResource.Size));
            gridColumns.Add(new TextGridColumn<Entity.DataModels.Icon>(o => o.Color, IconResource.Color));
            gridColumns.Add(new BooleanGridColumn<Entity.DataModels.Icon>(o => o.IsEnable, IconResource.Status));
            gridColumns.Add(new DateTimeGridColumn<Entity.DataModels.Icon>(o => o.CreatedOn, IconResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Entity.DataModels.Icon>(o => o.CreatedByUserName, IconResource.CreatedByUserName));
        }
    }
}
