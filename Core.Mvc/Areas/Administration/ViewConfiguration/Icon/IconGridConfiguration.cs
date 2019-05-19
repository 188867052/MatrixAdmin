using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Icon
{
    public class IconGridConfiguration : GridConfiguration<Entity.Icon>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IconGridConfiguration"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public IconGridConfiguration(ResponseModel model) : base(model)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Entity.Icon>> gridColumns)
        {
            gridColumns.Add(new IconGridColumn<Entity.Icon>(o => o.Code, IconResource.Icon));
            gridColumns.Add(new TextGridColumn<Entity.Icon>(o => o.Code, IconResource.Code));
            gridColumns.Add(new TextGridColumn<Entity.Icon>(o => o.Custom, IconResource.Custom));
            gridColumns.Add(new TextGridColumn<Entity.Icon>(o => o.Size, IconResource.Size));
            gridColumns.Add(new TextGridColumn<Entity.Icon>(o => o.Color, IconResource.Color));
            gridColumns.Add(new BooleanGridColumn<Entity.Icon>(o => o.IsEnable, IconResource.Status));
            gridColumns.Add(new DateTimeGridColumn<Entity.Icon>(o => o.CreateTime, IconResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Entity.Icon>(o => o.CreateByUserName, IconResource.CreatedByUserName));
        }
    }
}
