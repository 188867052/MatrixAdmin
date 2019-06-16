using System;
using System.Linq.Expressions;
using Core.Mvc.Areas.AdvancedDropDownFilters;
using Core.Mvc.Resource.Areas.AdvancedDropDown;
using Core.Web.GridFilter;
using Core.Web.JavaScript;

namespace Core.Mvc.Framework.AdvancedGridFilters
{
    public static class AdvancedDropDown
    {
        public static AdvancedDropDownGridFilter<T> RoleAdvancedDropDown<T>(Expression<Func<T, int?>> expression)
        {
            return new AdvancedDropDownGridFilter<T>(expression, AdvancedDropDownIndexResource.RoleName, new MethodCall("core.addOption"), AdvancedDropDownRoute.RoleDataList);
        }

        public static AdvancedDropDownGridFilter<T> UserAdvancedDropDown<T>(Expression<Func<T, int?>> expression)
        {
            return new AdvancedDropDownGridFilter<T>(expression, AdvancedDropDownIndexResource.UserName, new MethodCall("core.addOption"), AdvancedDropDownRoute.UserDataList);
        }

        public static AdvancedDropDownGridFilter<T> MenuAdvancedDropDown<T>(Expression<Func<T, int?>> expression)
        {
            return new AdvancedDropDownGridFilter<T>(expression, AdvancedDropDownIndexResource.MenuName, new MethodCall("core.addOption"), AdvancedDropDownRoute.MenuDataList);
        }
    }
}
