using System.Collections.Generic;
using System.Text;

namespace Core.Web.GridColumn
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

        public void AddBooleanColumn(BooleanGridColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddIntegerColumn(IntegerGridColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddTextColumn(TextGridColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddIconColumn(IconGridColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddDateTimeColumn(DateTimeGridColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddEnumColumn(EnumGridColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public string Render(int count, int pageSize, int currentPage)
        {
            StringBuilder thead = new StringBuilder();
            thead.Append("<th>序号</th>");
            foreach (var item in GridColumns)
            {
                thead.Append(item.RenderTh());
            }
            StringBuilder tbody = new StringBuilder();
            foreach (var entity in EntityList)
            {
                StringBuilder tr = new StringBuilder();
                tr.Append($"<td>{(currentPage - 1) * pageSize + EntityList.IndexOf(entity) + 1}</td>");
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