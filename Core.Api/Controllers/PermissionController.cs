using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Api.Authentication;
using Core.Api.Framework;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Permission;
using Dapper;
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
        public PermissionController(CoreContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this.DbContext)
            {
                return this.StandardResponse(this.DbContext.Permission);
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
                IQueryable<Permission> query = this.DbContext.Permission;
                query = query.AddBooleanFilter(o => o.IsEnable, model.IsEnable);
                query = query.AddStringContainsFilter(o => o.ActionCode, model.ActionCode);

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
                if (this.DbContext.Permission.Count(x => x.ActionCode == model.ActionCode && x.Id == model.Id.ToString()) > 0)
                {
                    response.SetFailed("权限操作码已存在");
                    return this.Ok(response);
                }

                Permission entity = this.Mapper.Map<PermissionCreateViewModel, Permission>(model);
                entity.CreateTime = DateTime.Now;
                entity.Id = Guid.NewGuid().ToString("N");
                entity.CreateByUserId = AuthenticationContextService.CurrentUser.Id;
                entity.CreateByUserName = AuthenticationContextService.CurrentUser.DisplayName;
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
                Menu menu = this.DbContext.Menu.FirstOrDefault(x => x.Id == entity.MenuId);
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
        public IActionResult SaveEdit(PermissionEditViewModel model)
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
                entity.MenuId = model.MenuId;
                entity.IsEnable = model.IsEnable.Value;
                entity.UpdateByUserId = 1;
                entity.UpdateByUserName = AuthenticationContextService.CurrentUser.DisplayName;
                entity.CreateTime = DateTime.Now;
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
        /// 角色-权限菜单树.
        /// </summary>
        /// <param name="code">角色编码.</param>
        /// <returns></returns>
        [HttpGet("/api/v1/permission/permission_tree/{code}")]
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

                List<PermissionMenuTree> menu = this.DbContext.Menu.Where(x => !x.IsEnable && x.Status).OrderBy(x => x.CreateTime).ThenBy(x => x.Sort)
                    .Select(x => new PermissionMenuTree
                    {
                        Id = x.Id,
                        ParentId = x.ParentId,
                        Title = x.Name
                    }).ToList();
                if (role.IsSuperAdministrator)
                {
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
        /// <param name="isEnable">The isEnable.</param>
        /// <param name="ids">权限ID字符串,多个以逗号隔开.</param>
        /// <returns></returns>
        private ResponseModel UpdateIsEnable(bool isEnable, string ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE Permission SET IsEnable = @IsEnable WHERE Id IN @Id";
                CoreContext.Dapper.Execute(sql, new { IsEnable = isEnable, Id = ids });
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
                CoreContext.Dapper.Execute(sql, new { Status = status, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }
    }
}