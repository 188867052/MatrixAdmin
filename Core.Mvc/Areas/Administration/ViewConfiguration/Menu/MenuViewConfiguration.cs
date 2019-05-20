using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using Resources = Core.Resource.Areas.Administration.ViewConfiguration.MenuIndexResource;

namespace Core.Mvc.Areas.Administration.ViewConfiguration.Menu
{
    public class MenuViewConfiguration : GridConfiguration<MenuModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewConfiguration"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MenuViewConfiguration(ResponseModel model) : base(model)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<MenuModel>> gridColumns)
        {
            Url url = new Url(nameof(Administration), typeof(MenuController), nameof(MenuController.RowContextMenu));
            gridColumns.Add(new RowContextMenuColumn<MenuModel>(o => o.Id, Resources.RowContextMenu, url));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.Name, Resources.Name));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.Url, Resources.Url));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.Alias, Resources.Alias));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.ParentName, Resources.ParentName));
            gridColumns.Add(new IntegerGridColumn<MenuModel>(o => o.Sort, Resources.Sort));
            gridColumns.Add(new EnumGridColumn<MenuModel>(o => o.Status, Resources.Status));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.Description, Resources.Description));
            gridColumns.Add(new DateTimeGridColumn<MenuModel>(o => o.CreateTime, Resources.CreateTime));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.CreateByUserName, Resources.CreateByUserName));
            gridColumns.Add(new DateTimeGridColumn<MenuModel>(o => o.UpdateTime, Resources.UpdateTime));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.UpdateByUserName, Resources.UpdateByUserName));
        }
    }
}
