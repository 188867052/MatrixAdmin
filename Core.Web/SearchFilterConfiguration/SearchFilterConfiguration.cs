using System.Collections.Generic;
using Core.Web.Button;
using Core.Web.GridFilter;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.SearchFilterConfiguration
{
    public abstract class SearchFilterConfiguration
    {
        /// <summary>
        /// Generate search filter.
        /// </summary>
        /// <returns>A string.</returns>
        public TagHelperOutput GenerateSearchFilter()
        {
            var searchFilter = new List<BaseGridFilter>();
            this.CreateSearchFilter(searchFilter);
            TagHelperOutput content = default;
            foreach (var item in searchFilter)
            {
                if (content == default)
                {
                    content = item.Render();
                }
                else
                {
                    content.PostElement.AppendHtml(item.Render());
                }
            }

            return content;
        }

        /// <summary>
        /// Generate button.
        /// </summary>
        /// <returns>A string.</returns>
        public TagHelperOutput GenerateButton()
        {
            IList<StandardButton> buttons = new List<StandardButton>();
            this.CreateButton(buttons);
            TagHelperOutput content = default;
            foreach (var item in buttons)
            {
                if (content == default)
                {
                    content = item.Render();
                }
                else
                {
                    content.PostElement.AppendHtml(item.Render());
                }
            }

            return content;
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