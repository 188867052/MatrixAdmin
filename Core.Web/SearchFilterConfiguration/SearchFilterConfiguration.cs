using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Web.Button;
using Core.Web.GridFilter;

namespace Core.Web.SearchFilterConfiguration
{
    public abstract class SearchFilterConfiguration
    {
        /// <summary>
        /// Generate search filter.
        /// </summary>
        /// <returns>A string.</returns>
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

        /// <summary>
        /// Generate button.
        /// </summary>
        /// <returns>A string.</returns>
        public string GenerateButton()
        {
            IList<StandardButton> buttons = new List<StandardButton>();
            this.CreateButton(buttons);
            return buttons.Aggregate<StandardButton, string>(default, (current, button) => current + button.Render());
        }

        /// <summary>
        /// Create search filter.
        /// </summary>
        /// <param name="searchFilter">The searchFilter.</param>
        protected abstract void CreateSearchFilter(IList<BaseGridFilter> searchFilter);

        /// <summary>
        /// Create button.
        /// </summary>
        /// <param name="buttons">The buttons.</param>
        protected abstract void CreateButton(IList<StandardButton> buttons);
    }
}