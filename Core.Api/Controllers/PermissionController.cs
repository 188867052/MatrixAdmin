using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Api.Extensions;
using Core.Api.Extensions.AuthContext;
using Core.Api.Utils;
using Core.Entity;
using Core.Extension.Dapper;
using Core.Model;
using Core.Model.Administration.Permission;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 权限控制器.
    /// </summary>
    // [CustomAuthorize]
    public class PermissionController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        public PermissionController(CoreApiContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this.DbContext)
            {
                return this.StandardResponse(this.DbContext.Menu);
            }
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Search(PermissionPostModel model)
        {
            using (this.DbContext)
            {
                IQueryable<Permission> query = this.DbContext.Permission.AsQueryable();

                // Filter<Permission> filter1 = new Filter<Permission>(nameof(Permission.Name), Operation.EqualTo, model.Status);
                // Filter<Permission> filter2 = new Filter<Permission>(nameof(Permission.Id), Operation.EqualTo, model.IsEnable);
                // Filter<Permission> filter = new Filter<Permission>(filter1, filter2, Connector.Or);
                // query = query.AddFilter(filter);
                query = query.AddBooleanFilter(model.IsEnable, nameof(Permission.IsEnable));

                // query = query.AddBooleanFilter(model.Status, nameof(Permission.Status));
                // query = query.AddGuidEqualsFilter(model.MenuGuid, nameof(Permission.MenuGuid));

                // var data = list.Select(this.Mapper.Map<Permission, PermissionJsonModel>);
                /*
                 * .Select(x => new PermissionJsonModel {
                    MenuName = x.Menu.Name,
                    x.
                });
                 */

                return this.StandardResponse(query, model);
            }
        }

        /// <summary>
        /// 创建权限.
        /// </summary>
        /// <param name="model">权限视图实体.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(PermissionCreateViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            if (model.Name.Trim().Length <= 0)
            {
                response.SetFailed("请输入权限名称");
                return this.Ok(response);
            }

            using (this.DbContext)
            {
                if (this.DbContext.Permission.Count(x => x.ActionCode == model.ActionCode && x.MenuGuid == model.MenuGuid) > 0)
                {
                    response.SetFailed("权限操作码已存在");
                    return this.Ok(response);
                }

                Permission entity = this.Mapper.Map<PermissionCreateViewModel, Permission>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Id = RandomHelper.GetRandom(8, true, false, true, true);
                entity.CreatedByUserGuid = AuthContextService.CurrentUser.Guid;
                entity.CreatedByUserName = AuthContextService.CurrentUser.DisplayName;
                this.DbContext.Permission.Add(entity);
                this.DbContext.SaveChanges();

                response.SetSuccess();
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 编辑权限.
        /// </summary>
        /// <param name="code">权限惟一编码.</param>
        /// <returns></returns>
        [HttpGet("{code}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(string code)
        {
            using (this.DbContext)
            {
                Permission entity = this.DbContext.Permission.FirstOrDefault(x => x.Id == code);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                PermissionEditViewModel model = this.Mapper.Map<Permission, PermissionEditViewModel>(entity);
                Menu menu = this.DbContext.Menu.FirstOrDefault(x => x.Guid == entity.MenuGuid);
                model.MenuName = menu.Name;
                response.SetData(model);
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的权限信息.
        /// </summary>
        /// <param name="model">权限视图实体.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(PermissionEditViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this.DbContext)
            {
                if (this.DbContext.Permission.Count(x => x.ActionCode == model.ActionCode && x.Id != model.Code) > 0)
                {
                    response.SetFailed("权限操作码已存在");
                    return this.Ok(response);
                }

                Permission entity = this.DbContext.Permission.FirstOrDefault(x => x.Id == model.Code);
                if (entity == null)
                {
                    response.SetFailed("权限不存在");
                    return this.Ok(response);
                }

                entity.Name = model.Name;
                entity.ActionCode = model.ActionCode;
                entity.MenuGuid = model.MenuGuid;
                entity.IsEnable = model.IsEnable.Value;
                entity.ModifiedByUserGuid = AuthContextService.CurrentUser.Guid;
                entity.ModifiedByUserName = AuthContextService.CurrentUser.DisplayName;
                entity.ModifiedOn = DateTime.Now;
                entity.Status = model.Status;
                entity.Description = model.Description;
                this.DbContext.SaveChanges();
                response.SetSuccess();
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 删除权限.
        /// </summary>
        /// <param name="ids">权限ID,多个以逗号分隔.</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            ResponseModel response = this.UpdateIsEnable(true, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 恢复权限.
        /// </summary>
        /// <param name="ids">权限ID,多个以逗号分隔.</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Recover(string ids)
        {
            ResponseModel response = this.UpdateIsEnable(false, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 批量操作.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">权限ID,多个以逗号分隔.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    response = this.UpdateIsEnable(true, ids);
                    break;
                case "recover":
                    response = this.UpdateIsEnable(false, ids);
                    break;
                case "forbidden":
                    response = this.UpdateStatus(false, ids);
                    break;
                case "normal":
                    response = this.UpdateStatus(true, ids);
                    break;
                default:
                    break;
            }

            return this.Ok(response);
        }

        /// <summary>
        /// 角色-权限菜单树.
        /// </summary>
        /// <param name="code">角色编码.</param>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/permission/permission_tree/{code}")]
        public IActionResult PermissionTree(int code)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this.DbContext)
            {
                Role role = this.DbContext.Role.FirstOrDefault(x => x.Id == code);
                if (role == null)
                {
                    response.SetFailed("角色不存在");
                    return this.Ok(response);
                }

                List<PermissionMenuTree> menu = this.DbContext.Menu.Where(x => !x.IsEnable && x.Status).OrderBy(x => x.CreatedOn).ThenBy(x => x.Sort)
                    .Select(x => new PermissionMenuTree
                    {
                        Guid = x.Guid,
                        ParentGuid = x.ParentGuid,
                        Title = x.Name
                    }).ToList();

                // DncPermissionWithAssignProperty
                string sql = @"SELECT P.Code,P.MenuGuid,P.Name,P.ActionCode,ISNULL(S.RoleCode,'') AS RoleCode,(CASE WHEN S.PermissionCode IS NOT NULL THEN 1 ELSE 0 END) AS IsAssigned FROM DncPermission AS P 
LEFT JOIN (SELECT * FROM RolePermissionMapping AS RPM WHERE RPM.RoleCode={0}) AS S 
ON S.PermissionCode= P.Code
WHERE P.IsDeleted=0 AND P.Status=1";
                if (role.IsSuperAdministrator)
                {
                    sql = @"SELECT P.Code,P.MenuGuid,P.Name,P.ActionCode,'SUPERADM' AS RoleCode,(CASE WHEN P.Code IS NOT NULL THEN 1 ELSE 0 END) AS IsAssigned FROM DncPermission AS P 
WHERE P.IsDeleted=0 AND P.Status=1";
                }

                // List<PermissionWithAssignProperty> permissionList = this.DbContext.PermissionWithAssignProperty.FromSql(sql, code).ToList();
                // List<PermissionMenuTree> tree = menu.FillRecursive(permissionList, Guid.Empty, role.IsSuperAdministrator);
                // response.SetData(new { tree, selectedPermissions = permissionList.Where(x => x.IsAssigned == 1).Select(x => x.Code) });
            }

            return this.Ok(response);
        }

        /// <summary>
        /// 删除权限.
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="ids">权限ID字符串,多个以逗号隔开.</param>
        /// <returns></returns>
        private ResponseModel UpdateIsEnable(bool isEnable, string ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE Permission SET IsEnable = @IsEnable WHERE Id IN @Id";
                this.DbContext.Dapper.Execute(sql, new { IsEnable = isEnable, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }

        /// <summary>
        /// 删除权限.
        /// </summary>
        /// <param name="status">权限状态.</param>
        /// <param name="ids">权限ID字符串,多个以逗号隔开.</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(bool status, string ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE Permission SET Status = @Status WHERE Id IN @Id";
                this.DbContext.Dapper.Execute(sql, new { Status = status, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }
    }

    /// <summary>
    ///
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