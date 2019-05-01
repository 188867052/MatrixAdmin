using Core.Model;
using Core.Model.Administration.Menu;
using Core.Resource.Areas.Administration.ViewConfiguration;
using Core.Web.GridColumn;
using Core.Web.ViewConfiguration;
using System.Collections.Generic;

namespace Core.Mvc.Areas.Administration.ViewConfiguration
{
    public class MenuViewConfiguration : GridConfiguration<Menu>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuViewConfiguration(ResponseModel response) : base(response)
        {
        }

        public override void CreateGridColumn(IList<BaseGridColumn<Menu>> gridColumns)
        {
            gridColumns.Add(new TextGridColumn<Menu>(o => o.Name, MenuIndexResource.Name));
            gridColumns.Add(new TextGridColumn<Menu>(o => o.Url, MenuIndexResource.Url));
            gridColumns.Add(new TextGridColumn<Menu>(o => o.Alias, MenuIndexResource.Alias));
            gridColumns.Add(new TextGridColumn<Menu>(o => o.ParentName, MenuIndexResource.ParentName));
            gridColumns.Add(new IntegerGridColumn<Menu>(o => o.Sort, MenuIndexResource.Sort));
            gridColumns.Add(new BooleanGridColumn<Menu>(o => o.Status, MenuIndexResource.Status));
            gridColumns.Add(new EnumGridColumn<Menu>(o => o.IsDefaultRouter, MenuIndexResource.IsDefaultRouter));
            gridColumns.Add(new DateTimeGridColumn<Menu>(o => o.CreatedOn, MenuIndexResource.CreatedOn));
            gridColumns.Add(new TextGridColumn<Menu>(o => o.CreatedByUserName, MenuIndexResource.CreatedByUserName));
        }
    }
}
