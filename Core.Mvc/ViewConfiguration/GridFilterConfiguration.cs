using System.Collections.Generic;
using Core.Web.Button;
using Core.Web.GridFilter;

namespace Core.Mvc.ViewConfiguration
{
    public abstract class GridFilterConfiguration<T>
    {
        protected GridFilterConfiguration()
        {
            this.Buttons = new List<StandardButton>();
            this.GridSearchFilter = new GridSearchFilter<T>();
        }
        public IList<StandardButton> Buttons { get; set; }

        public GridSearchFilter<T> GridSearchFilter{ get; set; }

        public abstract string GenerateSearchFilter();

        public abstract string GenerateButton();

    }
}