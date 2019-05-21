using System;
using System.Linq.Expressions;
using Core.Resource.Areas.AdvancedDropDown;
using Core.Web.GridFilter;
using Core.Web.JavaScript;

namespace Core.Mvc.Framework.AdvancedGridFilters
{
    public static class AdvancedDropDown<T>
    {
        public static AdvancedDropDownGridFilter<T> RoleAdvancedDropDown(Expression<Func<T, int?>> expression)
        {
            return new AdvancedDropDownGridFilter<T>(expression, AdvancedDropDownIndexResource.RoleName, new MethodCall("core.addOption"));
        }
    }
}
