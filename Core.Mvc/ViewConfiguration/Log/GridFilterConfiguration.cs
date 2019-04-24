using System.Collections.Generic;

namespace Core.Mvc.ViewConfiguration.Log
{
    public abstract class GridFilterConfiguration
    {
        public IList<Button.Button> Type { get; set; }

        public abstract string GenerateSearchFilter();

        public abstract string GenerateButton();
    }
}