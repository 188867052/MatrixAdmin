using System.Collections.Generic;
using System.Text;

namespace Core.Web.Grid
{
    public class GridColumn<T>
    {
        public List<BaseGridColumn<T>> GridColumns;
        public IList<T> EntityList;
        public GridColumn(IList<T> list)
        {
            this.GridColumns = new List<BaseGridColumn<T>>();
            this.EntityList = list;
        }

        public void AddBooleanColumn(BooleanColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddIntegerColumn(IntegerColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddTextColumn(TextColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddIconColumn(IconColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddDateTimeColumn(DateTimeColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddEnumColumn(EnumColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public string Render()
        {
            StringBuilder thead = new StringBuilder();
            foreach (var item in GridColumns)
            {
                thead.Append(item.RenderTh());
            }
            StringBuilder tbody = new StringBuilder();
            foreach (var entity in EntityList)
            {
                StringBuilder tr = new StringBuilder(); ;
                foreach (var item in GridColumns)
                {
                    tr.Append(item.RenderTd(entity));
                }
                tbody.Append($"<tr>{tr}</tr>");
            }
            return $"<table class=\"table table-bordered data-table\"><thead><tr>{thead}</tr></thead><tbody>{tbody}</tbody></table>";
        }
    }
}