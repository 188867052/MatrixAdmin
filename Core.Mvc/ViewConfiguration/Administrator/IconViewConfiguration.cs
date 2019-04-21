using System.Collections.Generic;
using Core.Models.Entities;
using Core.Web.Grid;

namespace Core.Mvc.ViewConfiguration.Administrator
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
            ColumnConfiguration<Icon> column = new ColumnConfiguration<Icon>(_icons);
            //< i class="icon icon-user"></i>
            column.AddIconColumn(new IconColumn<Icon>(o => o.Code, "图标"));
            column.AddTextColumn(new TextColumn<Icon>(o =>o.Code, "图标名称"));
            column.AddTextColumn(new TextColumn<Icon>(o => o.Custom, "自定义"));
            column.AddTextColumn(new TextColumn<Icon>(o => o.Size, "大小"));
            column.AddTextColumn(new TextColumn<Icon>(o => o.Color, "颜色"));
            column.AddBooleanColumn(new BooleanColumn<Icon>(o => o.Status, "状态"));
            column.AddDateTimeColumn(new DateTimeColumn<Icon>(o => o.CreatedOn, "创建时间"));
            column.AddTextColumn(new TextColumn<Icon>(o => o.CreatedByUserName, "创建者"));
            return column.Render();
        }
    }
}
