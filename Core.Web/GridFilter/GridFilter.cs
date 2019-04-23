using System.Collections.Generic;
using System.Text;
using Core.Web.GridFilter;

namespace Core.Web.GridFilter
{
    public class GridFilter<T>
    {
        public List<BaseGridFilter<T>> GridFilters;
        public IList<T> EntityList;
        public GridFilter(IList<T> list)
        {
            this.GridFilters = new List<BaseGridFilter<T>>();
            this.EntityList = list;
        }

        public void AddBooleanFilter(BooleanGridFilter<T> filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddIntegerFilter(IntegerGridFilter<T> filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddTextFilter(TextGridFilter<T> filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddIconFilter(IconGridFilter<T> filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddDateTimeFilter(DateTimeGridFilter<T> filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddEnumFilter(EnumGridFilter<T> filter)
        {
            this.GridFilters.Add(filter);
        }

        public string Render()
        {
            StringBuilder thead = new StringBuilder();
            foreach (var item in GridFilters)
            {
                thead.Append(item.Render());
            }

            return thead.ToString();
        }
    }
}