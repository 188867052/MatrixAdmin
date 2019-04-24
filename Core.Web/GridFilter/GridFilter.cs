using System.Collections.Generic;
using System.Text;

namespace Core.Web.GridFilter
{
    public class GridSearchFilter
    {
        public List<BaseGridFilter> GridFilters;
        public GridSearchFilter()
        {
            this.GridFilters = new List<BaseGridFilter>();
        }

        public void AddBooleanFilter(BooleanGridFilter filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddIntegerFilter(IntegerGridFilter filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddTextFilter(TextGridFilter filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddIconFilter(IconGridFilter filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddDateTimeFilter(DateTimeGridFilter filter)
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