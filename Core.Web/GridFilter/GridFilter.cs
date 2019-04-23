using System.Collections.Generic;
using System.Text;

namespace Core.Web.GridFilter
{
    public class GridFilter<T>
    {
        public List<BaseGridFilter<T>> GridColumns;
        public IList<T> EntityList;
        public GridFilter(IList<T> list)
        {
            this.GridColumns = new List<BaseGridFilter<T>>();
            this.EntityList = list;
        }

        public void AddBooleanColumn(BooleanGridFilter<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddIntegerColumn(IntegerGridFilter<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddTextColumn(TextGridFilter<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddIconColumn(IconGridColumn<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddDateTimeColumn(DateTimeGridFilter<T> column)
        {
            this.GridColumns.Add(column);
        }

        public void AddEnumColumn(EnumGridFilter<T> column)
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