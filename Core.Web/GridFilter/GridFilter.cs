using System.Collections.Generic;
using System.Text;

namespace Core.Web.GridFilter
{
    public class GridSearchFilter<T>
    {
        public List<BaseGridFilter> GridFilters;

        /// <summary>
        /// 构造函数
        /// </summary>
        public GridSearchFilter()
        {
            this.GridFilters = new List<BaseGridFilter>();
        }

        public void AddBooleanFilter(BooleanGridFilter filter)
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

        public void AddDropDownGridFilter(DropDownGridFilter<T> filter)
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

        public void AddEnumFilter(EnumGridFilter filter)
        {
            this.GridFilters.Add(filter);
        }

        public string Render()
        {
            StringBuilder filterText = new StringBuilder();
            foreach (var item in GridFilters)
            {
                filterText.Append(item.Render());
            }

            return filterText.ToString();
        }
    }
}