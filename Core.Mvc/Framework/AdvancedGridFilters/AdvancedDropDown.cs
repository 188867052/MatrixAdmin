using System;
using System.Linq.Expressions;
using Core.Extension;
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
            var url = new Url(nameof(Areas.AdvancedDropDownFilters), typeof(AdvancedDropDownController), nameof(AdvancedDropDownController.RoleDataList));

            return new AdvancedDropDownGridFilter<T>(expression, AdvancedDropDownIndexResource.RoleName, new MethodCall("core.addOption"), url);
        }

        public static AdvancedDropDownGridFilter<T> UserAdvancedDropDown<T>(Expression<Func<T, int?>> expression)
        {
            var url = new Url(nameof(Areas.AdvancedDropDownFilters), typeof(AdvancedDropDownController), nameof(AdvancedDropDownController.UserDataList));

            return new AdvancedDropDownGridFilter<T>(expression, AdvancedDropDownIndexResource.UserName, new MethodCall("core.addOption"), url);
        }

        public static AdvancedDropDownGridFilter<T> MenuAdvancedDropDown<T>(Expression<Func<T, int?>> expression)
        {
            var url = new Url(nameof(Areas.AdvancedDropDownFilters), typeof(AdvancedDropDownController), nameof(AdvancedDropDownController.MenuDataList));

            return new AdvancedDropDownGridFilter<T>(expression, AdvancedDropDownIndexResource.MenuName, new MethodCall("core.addOption"), url);
        }
    }
}
