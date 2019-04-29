using System.Collections.Generic;
using Core.Web.Button;
using Core.Web.GridFilter;

namespace Core.Mvc.ViewConfiguration
{
    public abstract class GridFilterConfiguration<T>
    {
        protected GridFilterConfiguration()
        {
            this.GridSearchFilter = new GridSearchFilter<T>();
        }

        public GridSearchFilter<T> GridSearchFilter { get; set; }

        public abstract string GenerateSearchFilter();

        protected abstract void CreateButton(IList<StandardButton> buttons);

        public string GenerateButton()
        {
            IList<StandardButton> buttons = new List<StandardButton>();
            this.CreateButton(buttons);
            ;
            string html = default;
            string script = default;
            foreach (var button in buttons)
            {
                html += button.Render();
                script += button.Event.Render();
            }

            script = $"<script>{script}</script>";
            return html + script;
        }
    }
}