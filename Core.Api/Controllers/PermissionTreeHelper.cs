using System;
using System.Collections.Generic;
using System.Linq;
using Core.Api.Extensions;
using Core.Model.Administration.Permission;

namespace Core.Api.Controllers
{
    /// <summary>
    /// PermissionTreeHelper.
    /// </summary>
    public static class PermissionTreeHelper
    {
        /// <summary>
        /// Fill recursive.
        /// </summary>
        /// <param name="menus">菜单集合.</param>
        /// <param name="permissions">权限集合.</param>
        /// <param name="parentGuid">父级菜单GUID.</param>
        /// <param name="isSuperAdministrator">是否为超级管理员角色.</param>
        /// <returns></returns>
        public static List<PermissionMenuTree> FillRecursive(this List<PermissionMenuTree> menus, List<PermissionWithAssignProperty> permissions, Guid? parentGuid, bool isSuperAdministrator = false)
        {
            List<PermissionMenuTree> recursiveObjects = new List<PermissionMenuTree>();
            foreach (PermissionMenuTree item in menus.Where(x => x.ParentGuid == parentGuid))
            {
                PermissionMenuTree children = new PermissionMenuTree
                {
                    AllAssigned = isSuperAdministrator || permissions.Where(x => x.MenuGuid == item.Guid).Count(x => x.IsAssigned == 0) == 0,
                    Expand = true,
                    Guid = item.Guid,
                    ParentGuid = item.ParentGuid,
                    Permissions = permissions.Where(x => x.MenuGuid == item.Guid).Select(x => new PermissionElement
                    {
                        Name = x.Name,
                        Code = x.Code,
                        IsAssignedToRole = IsAssigned(x.IsAssigned, isSuperAdministrator)
                    }).ToList(),

                    Title = item.Title,
                    Children = FillRecursive(menus, permissions, item.Guid)
                };
                recursiveObjects.Add(children);
            }

            return recursiveObjects;
        }

        private static bool IsAssigned(int isAssigned, bool isSuperAdministrator)
        {
            if (isSuperAdministrator)
            {
                return true;
            }

            return isAssigned == 1;
        }

        // public static List<PermissionMenuTree> FillRecursive(this List<PermissionMenuTree> menus, List<DncPermissionWithAssignProperty> permissions, Guid? parentGuid)
        // {
        //    List<PermissionMenuTree> recursiveObjects = new List<PermissionMenuTree>();

        // return recursiveObjects;
        // }
    }
}