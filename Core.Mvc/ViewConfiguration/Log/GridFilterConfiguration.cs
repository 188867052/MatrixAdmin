using System.Collections.Generic;
using Core.Web.Button;

namespace Core.Mvc.ViewConfiguration.Log
{
    public abstract class GridFilterConfiguration
    {
        public IList<StandardButton> Buttons { get; set; }

        public abstract string GenerateSearchFilter();

        public abstract string GenerateButton();
    }
}