using Core.Model.PostModel;
using Core.Resource.ViewConfiguration.Administration;
using Core.Web.GridFilter;
using Core.Web.ViewConfiguration;
using System.Collections.Generic;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class PermissionFilterConfiguration : ViewConfiguration<PermissionPostModel>
    {
        public PermissionFilterConfiguration(IList<PermissionPostModel> entity) : base(entity)
        {
        }

        public override void GenerateGridColumn()
        {
        }

        public override void GenerateGridFilter()
        {
            GridFilter.AddBooleanFilter(new BooleanGridFilter<PermissionPostModel>(o => o.Status.Value, PermissionIndexResource.Name));
        }
    }
}
