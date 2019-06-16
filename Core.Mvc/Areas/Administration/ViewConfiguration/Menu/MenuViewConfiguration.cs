using System.Collections.Generic;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.Routes;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using Resources = Core.Mvc.Resource.Areas.Administration.ViewConfiguration.Menu.MenuIndexResource;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
{
    public class MenuViewConfiguration<T> : GridConfiguration<T>
    where T : MenuModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewConfiguration{T}"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MenuViewConfiguration(ResponseModel model) : base(model)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<T>> gridColumns)
        {
            gridColumns.Add(new RowContextMenuColumn<T>(o => o.Id, Resources.RowContextMenu, MenuRoute.RowContextMenu));
            gridColumns.Add(new TextGridColumn<T>(o => o.Name, Resources.Name));
            gridColumns.Add(new TextGridColumn<T>(o => o.Url, Resources.Url));
            gridColumns.Add(new TextGridColumn<T>(o => o.Alias, Resources.Alias));
            gridColumns.Add(new TextGridColumn<T>(o => o.ParentName, Resources.ParentName));
            gridColumns.Add(new IntegerGridColumn<T>(o => o.Sort, Resources.Sort));
            gridColumns.Add(new EnumGridColumn<T>(o => o.Status, Resources.Status));
            gridColumns.Add(new TextGridColumn<T>(o => o.Description, Resources.Description));
            gridColumns.Add(new DateTimeGridColumn<T>(o => o.CreateTime, Resources.CreateTime));
            gridColumns.Add(new TextGridColumn<T>(o => o.CreateByUserName, Resources.CreateByUserName));
            gridColumns.Add(new DateTimeGridColumn<T>(o => o.UpdateTime, Resources.UpdateTime));
            gridColumns.Add(new TextGridColumn<T>(o => o.UpdateByUserName, Resources.UpdateByUserName));
        }
    }
}
