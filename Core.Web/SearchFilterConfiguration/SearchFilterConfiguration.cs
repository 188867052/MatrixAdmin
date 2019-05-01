using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Web.Button;
using Core.Web.GridFilter;

namespace Core.Web.SearchFilterConfiguration
{
    public abstract class SearchFilterConfiguration<T>
    {
        public string GenerateSearchFilter()
        {
            var searchFilter = new List<BaseGridFilter>();
            this.CreateSearchFilter(searchFilter);
            StringBuilder filterText = new StringBuilder();
            foreach (var item in searchFilter)
            {
                filterText.Append(item.Render());
            }

            return filterText.ToString();
        }

        protected abstract void CreateSearchFilter(IList<BaseGridFilter> searchFilter);

        protected abstract void CreateButton(IList<StandardButton> buttons);

        public string GenerateButton()
        {
            IList<StandardButton> buttons = new List<StandardButton>();
            this.CreateButton(buttons);
            return buttons.Aggregate<StandardButton, string>(default, (current, button) => current + button.Render());
        }
    }
}