using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Mvc.Areas.Administration.Controllers;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;

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
            gridColumns.Add(new RowContextMenuColumn<MenuModel>(o => o.Id, "操作", url));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.Name, MenuIndexResource.Name));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.Url, MenuIndexResource.Url));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.Alias, MenuIndexResource.Alias));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.ParentName, MenuIndexResource.ParentName));
            gridColumns.Add(new IntegerGridColumn<MenuModel>(o => o.Sort, MenuIndexResource.Sort));
            gridColumns.Add(new EnumGridColumn<MenuModel>(o => o.Status, MenuIndexResource.Status));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.Description, MenuIndexResource.Description));

            // gridColumns.Add(new EnumGridColumn<ConsoleApp.Menu>(o => o.IsDefaultRouter, MenuIndexResource.IsDefaultRouter));
            gridColumns.Add(new DateTimeGridColumn<MenuModel>(o => o.CreateTime, MenuIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.CreateByUserName, MenuIndexResource.CreatedByUserName));
            gridColumns.Add(new TextGridColumn<MenuModel>(o => o.UpdateByUserName, MenuIndexResource.UpdateByUserName));
        }
    }
}
