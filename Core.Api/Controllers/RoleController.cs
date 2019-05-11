using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Api.ControllerHelpers;
using Core.Entity;
using Core.Entity.Enums;
using Core.Extension;
using Core.Extension.AuthContext;
using Core.Extension.Dapper;
using Core.Model;
using Core.Model.Administration.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolePermissionMapping = Core.Entity.RolePermissionMapping;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 角色控制器.
    /// </summary>
    // [CustomAuthorize]
    public class RoleController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        public RoleController(CoreApiContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this.DbContext)
            {
                IQueryable<Role> query = this.DbContext.Role;
                query = query.OrderByDescending(o => o.CreateTime);
                Pager pager = Pager.CreateDefaultInstance();
                pager.TotalCount = query.Count();
                List<Role> list = query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();

                IList<RoleModel> models = new List<RoleModel>();
                foreach (Role item in list)
                {
                    models.Add(new RoleModel(item));
                }

                ResponseModel response = new ResponseModel(models, pager);

                return this.Ok(response);
            }
        }

        /// <summary>
        /// 根据id查询.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult FindById(int id)
        {
            using (this.DbContext)
            {
                Role entity = this.DbContext.Role.Find(id);
                RoleModel model = new RoleModel(entity);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(model);

                return this.Ok(response);
            }
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="model">model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public IActionResult Search(RolePostModel model)
        {
            using (this.DbContext)
            {
                IQueryable<Role> query = this.DbContext.Role;
                query = query.AddStringContainsFilter(model.RoleName, nameof(Role.Name));
                if (model.PageIndex < 1)
                {
                    model.PageIndex = 1;
                }

                var list = query.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).ToList();

                IList<RoleModel> models = new List<RoleModel>();
                foreach (Role item in list)
                {
                    models.Add(new RoleModel(item));
                }

                ResponseModel response = new ResponseModel(models, model);
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 创建角色.
        /// </summary>
        /// <param name="model">角色视图实体.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(RoleCreatePostModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            if (model.Name.Trim().Length <= 0)
            {
                response.SetFailed("请输入角色名称");
                return this.Ok(response);
            }

            using (this.DbContext)
            {
                if (this.DbContext.Role.Count(x => x.Name == model.Name) > 0)
                {
                    response.SetFailed("角色已存在");
                    return this.Ok(response);
                }

                Role entity = this.Mapper.Map<RoleCreatePostModel, Role>(model);
                entity.CreateTime = DateTime.Now;
                entity.IsSuperAdministrator = false;
                entity.IsBuiltin = false;
                entity.CreateByUserId = Guid.NewGuid();
                entity.CreatedByUserName = "System";

                this.DbContext.Role.Add(entity);
                this.DbContext.SaveChanges();

                response.SetSuccess();
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 编辑角色.
        /// </summary>
        /// <param name="code">角色惟一编码.</param>
        /// <returns></returns>
        [HttpGet("{code}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int code)
        {
            using (this.DbContext)
            {
                Role entity = this.DbContext.Role.FirstOrDefault(x => x.Id == code);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(this.Mapper.Map<Role, RoleCreateModel>(entity));
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的角色信息.
        /// </summary>
        /// <param name="model">角色视图实体.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(RoleCreateModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this.DbContext)
            {
                if (this.DbContext.Role.Count(x => x.Name == model.Name && x.Id != model.Code) > 0)
                {
                    response.SetFailed("角色已存在");
                    return this.Ok(response);
                }

                Role entity = this.DbContext.Role.FirstOrDefault(x => x.Id == model.Code);

                if (entity.IsSuperAdministrator && !AuthContextService.IsSupperAdministrator)
                {
                    response.SetFailed("没有足够的权限");
                    return this.Ok(response);
                }

                entity.Name = model.Name;
                if (model.IsEnable.HasValue)
                {
                    entity.IsEnable = model.IsEnable.Value;
                }

                entity.ModifiedByUserId = AuthContextService.CurrentUser.Guid;
                entity.ModifiedByUserName = AuthContextService.CurrentUser.DisplayName;
                entity.UpdateTime = DateTime.Now;
                if (model.IsForbidden.HasValue)
                {
                    entity.IsForbidden = model.IsForbidden.Value;
                }

                entity.Description = model.Description;
                this.DbContext.SaveChanges();
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Delete(int[] ids)
        {
            ResponseModel response = RoleControllerHelper.UpdateIsDeleted(true, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 恢复用户.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Recover(int[] ids)
        {
            ResponseModel response = RoleControllerHelper.UpdateIsDeleted(false, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 启用用户.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Normal(int[] ids)
        {
            ResponseModel response = RoleControllerHelper.UpdateStatus(false, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 禁止用户.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Forbidden(int[] ids)
        {
            ResponseModel response = RoleControllerHelper.UpdateStatus(true, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 为指定角色分配权限.
        /// </summary>
        /// <param name="payload">角色分配权限的请求载体类.</param>
        /// <returns></returns>
        [HttpPost("/api/v1/rbac/role/assign_permission")]
        public IActionResult AssignPermission(RoleAssignPermissionPayload payload)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this.DbContext)
            {
                Role role = this.DbContext.Role.FirstOrDefault(x => x.Id == payload.RoleCode);
                if (role == null)
                {
                    response.SetFailed("角色不存在");
                    return this.Ok(response);
                }

                // 如果是超级管理员，则不写入到角色-权限映射表(在读取时跳过角色权限映射，直接读取系统所有的权限)
                if (role.IsSuperAdministrator)
                {
                    response.SetSuccess();
                    return this.Ok(response);
                }

                // 先删除当前角色原来已分配的权限
                this.DbContext.Database.ExecuteSqlInterpolated($"DELETE FROM DncRolePermissionMapping WHERE RoleCode={payload.RoleCode}");
                if (payload.Permissions != null || payload.Permissions.Count > 0)
                {
                    IEnumerable<RolePermissionMapping> permissions = payload.Permissions.Select(x => new RolePermissionMapping
                    {
                        CreatedOn = DateTime.Now,
                        PermissionCode = x.Trim(),

                        // RoleCode = payload.RoleCode.Trim()
                    });

                    // this.DbContext.RolePermissionMapping.AddRange(permissions);
                    this.DbContext.SaveChanges();
                }
            }

            return this.Ok(response);
        }

        /// <summary>
        /// 获取指定用户的角色列表.
        /// </summary>
        /// <param name="guid">用户GUID.</param>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/role/find_list_by_user_guid/{guid}")]
        public IActionResult FindListByUserGuid(Guid guid)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this.DbContext)
            {
                // 有N+1次查询的性能问题
                // var query = this.DbContext.DncUser
                //    .Include(r => r.UserRoles)
                //    .ThenInclude(x => x.DncRole)
                //    .Where(x => x.Guid == guid);
                // var roles = query.FirstOrDefault().UserRoles.Select(x => new
                // {
                //    x.DncRole.Code,
                //    x.DncRole.Name
                // });
                string sql = @"SELECT R.* FROM DncUserRoleMapping AS URM
INNER JOIN DncRole AS R ON R.Code=URM.RoleCode
WHERE URM.UserGuid={0}";
                List<Role> query = this.DbContext.Role.FromSqlRaw(sql, guid).ToList();
                List<int> assignedRoles = query.ToList().Select(x => x.Id).ToList();
                var roles = this.DbContext.Role.Where(x => !x.IsEnable && x.IsForbidden).ToList().Select(x => new { label = x.Name, key = x.Id });
                response.SetData(new { roles, assignedRoles });
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 查询所有角色列表(只包含主要的字段信息:name,code).
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FindSimpleList()
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this.DbContext)
            {
                var roles = this.DbContext.Role.Where(x => !x.IsEnable && x.IsForbidden).Select(x => new { x.Name, x.Id }).ToList();
                response.SetData(roles);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// 删除角色.
        /// </summary>
        /// <param name="isEnable">The isEnable.</param>
        /// <param name="ids">ids.</param>
        /// <returns>ResponseModel.</returns>
        private ResponseModel UpdateIsEnable(bool isEnable, int[] ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE Role SET IsEnable = @IsEnable WHERE Id IN @Ids";
                this.DbContext.Dapper.Execute(sql, new { IsEnable = isEnable, Ids = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }

        /// <summary>
        /// 删除角色.
        /// </summary>
        /// <param name="status">角色状态.</param>
        /// <param name="ids">角色ID字符串,多个以逗号隔开.</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(StatusEnum status, int[] ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE Role SET Status = @Status WHERE Id IN @Ids";
                this.DbContext.Dapper.Execute(sql, new { Status = status, Ids = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }
    }
}