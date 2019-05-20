using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Entity;
using Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 账户控制器.
    /// </summary>
    [Authorize]
    public class AccountController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        public AccountController(CoreApiContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        /// <summary>
        /// Profile.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Profile()
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;

            // using (this.DbContext)
            //            {
            //                Guid guid = AuthContextService.CurrentUser.Guid;
            //                User user = this.DbContext.User.FirstOrDefaultAsync(x => x.Id == 1).Result;

            // List<Menu> menus = this.DbContext.Menu.Where(x => !x.IsEnable && x.Status).ToList();

            // //查询当前登录用户拥有的权限集合(非超级管理员)
            //                string sqlPermission = @"SELECT P.Code AS PermissionCode,P.ActionCode AS PermissionActionCode,P.Name AS PermissionName,P.Type AS PermissionType,M.Name AS MenuName,M.Guid AS MenuGuid,M.Alias AS MenuAlias,M.IsDefaultRouter FROM RolePermissionMapping AS RPM
            // LEFT JOIN Permission AS P ON P.Code = RPM.PermissionCode
            // INNER JOIN Menu AS M ON M.Guid = P.MenuGuid
            // WHERE P.IsDeleted=0 AND P.Status=1 AND EXISTS (SELECT 1 FROM UserRoleMapping AS URM WHERE URM.UserGuid={0} AND URM.RoleCode=RPM.RoleCode)";
            //                if (user.UserType == UserRoleEnum.SuperAdministrator)
            //                {
            //                    //如果是超级管理员
            //                    sqlPermission = @"SELECT P.Code AS PermissionCode,P.ActionCode AS PermissionActionCode,P.Name AS PermissionName,P.Type AS PermissionType,M.Name AS MenuName,M.Guid AS MenuGuid,M.Alias AS MenuAlias,M.IsDefaultRouter FROM Permission AS P
            // INNER JOIN Menu AS M ON M.Guid = P.MenuGuid
            // WHERE P.IsDeleted=0 AND P.Status=1";
            //                }
            //                List<PermissionWithMenu> permissions = this.DbContext.PermissionWithMenu.FromSql(sqlPermission, user.Id).ToList();
            //                List<string> allowPages = new List<string> { };

            // if (user.UserType == UserRoleEnum.SuperAdministrator)
            //                {
            //                    allowPages.AddRange(menus.Select(x => x.Alias));
            //                }
            //                else
            //                {
            //                    allowPages.AddRange(menus.Where(x => x.IsDefaultRouter == YesOrNoEnum.Yes).Select(x => x.Alias));
            //                    foreach (PermissionWithMenu permission in permissions.Where(x => x.PermissionType == PermissionTypeEnum.Menu))
            //                    {
            //                        allowPages.AddRange(FindParentMenuAlias(menus, permission.MenuGuid));
            //                    }
            //                }

            // //var allowPages = FindParentMenuAlias(menus);
            //                List<string> pages = allowPages.Distinct().ToList();
            //                Dictionary<string, IEnumerable<string>> pagePermissions = permissions.GroupBy(x => x.MenuAlias).ToDictionary(g => g.Key, g => g.Select(x => x.PermissionActionCode));
            //                response.SetData(new
            //                {
            //                    access = new string[] { },
            //                    avator = user.Avatar,
            //                    user_guid = user.Id,
            //                    user_name = user.DisplayName,
            //                    user_type = user.UserType,
            //                    pages, // =new[] { "rbac", "rbac_user_page", "rbac_menu_page", "rbac_role_page", "rbac_permission_page", "rbac_role_permission_page" },
            //                    permissions = pagePermissions

            // });
            //            }
            return this.Ok(response);
        }

        private List<string> FindParentMenuAlias(List<Menu> menus, int? parentId)
        {
            List<string> pages = new List<string>();
            Menu parent = menus.FirstOrDefault(x => x.Id == parentId);
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

                pages.AddRange(this.FindParentMenuAlias(menus, parent.ParentId));
            }

            return pages.Distinct().ToList();
        }
    }
}