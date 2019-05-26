using System.Collections.Generic;
using Core.Model;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using Resources = Core.Mvc.Resource.Areas.Administration.ViewConfiguration.Icon.IconResource;

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
            gridColumns.Add(new IconGridColumn<Entity.Icon>(o => o.Code, Resources.Icon));
            gridColumns.Add(new TextGridColumn<Entity.Icon>(o => o.Code, Resources.Code));
            gridColumns.Add(new TextGridColumn<Entity.Icon>(o => o.Custom, Resources.Custom));
            gridColumns.Add(new TextGridColumn<Entity.Icon>(o => o.Size, Resources.Size));
            gridColumns.Add(new TextGridColumn<Entity.Icon>(o => o.Color, Resources.Color));
            gridColumns.Add(new BooleanGridColumn<Entity.Icon>(o => o.IsEnable, Resources.Status));
            gridColumns.Add(new DateTimeGridColumn<Entity.Icon>(o => o.CreateTime, Resources.CreatedOn));
            gridColumns.Add(new TextGridColumn<Entity.Icon>(o => o.CreateByUserName, Resources.CreatedByUserName));
        }
    }
}
