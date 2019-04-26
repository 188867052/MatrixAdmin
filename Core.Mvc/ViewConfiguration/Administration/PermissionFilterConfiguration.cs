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
            filter.AddBooleanFilter(new BooleanGridFilter<PermissionPostModel>(o => o.IsEnable.Value, PermissionIndexResource.Name));
            filter.AddTextFilter(new TextGridFilter<PermissionPostModel>(o => o.KeyWord, PermissionIndexResource.Status));
            filter.Render();
        }
    }
}
