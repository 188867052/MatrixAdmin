using System.Collections.Generic;
using Core.Model;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
{
    public class MenuViewConfiguration : GridConfiguration<Entity.Menu>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewConfiguration"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MenuViewConfiguration(ResponseModel model) : base(model)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Entity.Menu>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<Entity.Menu>(o => o.Name, MenuIndexResource.Name));
            gridColumns.Add(new TextGridColumn<Entity.Menu>(o => o.Url, MenuIndexResource.Url));
            gridColumns.Add(new TextGridColumn<Entity.Menu>(o => o.Alias, MenuIndexResource.Alias));
            gridColumns.Add(new TextGridColumn<Entity.Menu>(o => o.ParentName, MenuIndexResource.ParentName));
            gridColumns.Add(new IntegerGridColumn<Entity.Menu>(o => o.Sort, MenuIndexResource.Sort));
            gridColumns.Add(new BooleanGridColumn<Entity.Menu>(o => o.Status, MenuIndexResource.Status));
            //gridColumns.Add(new EnumGridColumn<ConsoleApp.Menu>(o => o.IsDefaultRouter, MenuIndexResource.IsDefaultRouter));
            gridColumns.Add(new DateTimeGridColumn<Entity.Menu>(o => o.CreatedOn, MenuIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Entity.Menu>(o => o.CreatedByUserName, MenuIndexResource.CreatedByUserName));
        }
    }
}
