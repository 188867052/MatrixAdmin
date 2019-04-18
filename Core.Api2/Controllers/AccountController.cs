using System;
using System.Collections.Generic;
using System.Linq;
using Core.Api.Entities;
using Core.Api.Entities.Enums;
using Core.Api.Entities.QueryModels.Permission;
using Core.Api.Extensions;
using Core.Api.Extensions.AuthContext;
using Core.Api.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly Context _dbContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public AccountController(Context dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Profile()
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this._dbContext)
            {
                Guid guid = AuthContextService.CurrentUser.Guid;
                User user = this._dbContext.User.FirstOrDefaultAsync(x => x.Guid == guid).Result;

                List<Menu> menus = this._dbContext.Menu.Where(x => !x.IsEnable && x.Status).ToList();

                //查询当前登录用户拥有的权限集合(非超级管理员)
                string sqlPermission = @"SELECT P.Code AS PermissionCode,P.ActionCode AS PermissionActionCode,P.Name AS PermissionName,P.Type AS PermissionType,M.Name AS MenuName,M.Guid AS MenuGuid,M.Alias AS MenuAlias,M.IsDefaultRouter FROM RolePermissionMapping AS RPM 
LEFT JOIN Permission AS P ON P.Code = RPM.PermissionCode
INNER JOIN Menu AS M ON M.Guid = P.MenuGuid
WHERE P.IsDeleted=0 AND P.Status=1 AND EXISTS (SELECT 1 FROM UserRoleMapping AS URM WHERE URM.UserGuid={0} AND URM.RoleCode=RPM.RoleCode)";
                if (user.UserType == UserTypeEnum.SuperAdministrator)
                {
                    //如果是超级管理员
                    sqlPermission = @"SELECT P.Code AS PermissionCode,P.ActionCode AS PermissionActionCode,P.Name AS PermissionName,P.Type AS PermissionType,M.Name AS MenuName,M.Guid AS MenuGuid,M.Alias AS MenuAlias,M.IsDefaultRouter FROM Permission AS P 
INNER JOIN Menu AS M ON M.Guid = P.MenuGuid
WHERE P.IsDeleted=0 AND P.Status=1";
                }
                List<PermissionWithMenu> permissions = this._dbContext.PermissionWithMenu.FromSql(sqlPermission, user.Guid).ToList();
                List<string> allowPages = new List<string> { };

                if (user.UserType == UserTypeEnum.SuperAdministrator)
                {
                    allowPages.AddRange(menus.Select(x => x.Alias));
                }
                else
                {
                    allowPages.AddRange(menus.Where(x => x.IsDefaultRouter == YesOrNoEnum.Yes).Select(x => x.Alias));
                    foreach (PermissionWithMenu permission in permissions.Where(x => x.PermissionType == PermissionTypeEnum.Menu))
                    {
                        allowPages.AddRange(FindParentMenuAlias(menus, permission.MenuGuid));
                    }
                }

                //var allowPages = FindParentMenuAlias(menus);
                List<string> pages = allowPages.Distinct().ToList();
                Dictionary<string, IEnumerable<string>> pagePermissions = permissions.GroupBy(x => x.MenuAlias).ToDictionary(g => g.Key, g => g.Select(x => x.PermissionActionCode));
                response.SetData(new
                {
                    access = new string[] { },
                    avator = user.Avatar,
                    user_guid = user.Guid,
                    user_name = user.DisplayName,
                    user_type = user.UserType,
                    pages, // =new[] { "rbac", "rbac_user_page", "rbac_menu_page", "rbac_role_page", "rbac_permission_page", "rbac_role_permission_page" },
                    permissions = pagePermissions

                });
            }

            return Ok(response);
        }

        private List<string> FindParentMenuAlias(List<Menu> menus, Guid? parentGuid)
        {
            List<string> pages = new List<string>();
            Menu parent = menus.FirstOrDefault(x => x.Guid == parentGuid);
            if (parent != null)
            {
                if (!pages.Contains(parent.Alias))
                {
                    pages.Add(parent.Alias);
                }
                else
                {
                    return pages;
                }
                if (parent.ParentGuid != Guid.Empty)
                {
                    pages.AddRange(FindParentMenuAlias(menus, parent.ParentGuid));
                }
            }

            return pages.Distinct().ToList();
        }
    }
}