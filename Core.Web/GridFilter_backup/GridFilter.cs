using System.Collections.Generic;
using System.Text;

namespace Core.Web.GridFilter_backup
{
    public class GridFilter<TModel,TPostModel>
    {
        public List<BaseGridFilter<TModel, TPostModel>> GridFilters;
        public GridFilter()
        {
            this.GridFilters = new List<BaseGridFilter<TModel, TPostModel>>();
        }

        public void AddBooleanFilter(BooleanGridFilter<TModel, TPostModel> filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddIntegerFilter(IntegerGridFilter<TModel, TPostModel> filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddTextFilter(TextGridFilter<TModel, TPostModel> filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddIconFilter(IconGridFilter<TModel, TPostModel> filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddDateTimeFilter(DateTimeGridFilter<TModel, TPostModel> filter)
        {
            this.GridFilters.Add(filter);
        }

        public void AddEnumFilter(EnumGridFilter<TModel, TPostModel> filter)
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