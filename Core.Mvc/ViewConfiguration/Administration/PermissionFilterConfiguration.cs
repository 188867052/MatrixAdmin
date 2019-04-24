using Core.Model.Entity;
using Core.Model.PostModel;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridFilter;
using System.Collections.Generic;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class PermissionFilterConfiguration
    {
        public PermissionFilterConfiguration(IList<PermissionPostModel> entity)
        {
        }

        public void GenerateGridFilter()
        {
            var filter = new GridSearchFilter<PermissionPostModel>();
            filter.AddBooleanFilter(new BooleanGridFilter(PermissionIndexResource.Name));
            filter.AddBooleanFilter(new BooleanGridFilter(PermissionIndexResource.Status));
            filter.Render();
        }
    }
}
