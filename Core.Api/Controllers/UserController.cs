using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Api.ControllerHelpers;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Role;
using Core.Model.Administration.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 用户控制器.
    /// </summary>
    public class UserController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        public UserController(CoreApiContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this.DbContext)
            {
                this.DbContext.Set<UserRoleMapping>().Load();
                this.DbContext.Set<UserStatus>().Load();
                this.DbContext.Set<Role>().Load();
                DbSet<User> query = this.DbContext.User;
                Pager pager = Pager.CreateDefaultInstance();
                pager.TotalCount = query.Count();
                List<User> list = query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();

                IList<UserModel> models = new List<UserModel>();
                foreach (User item in list)
                {
                    models.Add(new UserModel(item));
                }

                ResponseModel response = new ResponseModel(models, pager);

                return this.Ok(response);
            }
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="model">model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public IActionResult Search(UserPostModel model)
        {
            using (this.DbContext)
            {
                this.DbContext.Set<UserStatus>().Load();
                this.DbContext.Set<UserRoleMapping>().Load();
                this.DbContext.Set<Role>().Load();
                IQueryable<User> query = this.DbContext.User.AsQueryable();
                if (model.Status.HasValue)
                {
                    query = query.Where(o => o.Status == (int)model.Status);
                }

                if (model.RoleId.HasValue)
                {
                    query = query.Where(o => o.UserRoleMapping.Any(u => u.Role.Id == model.RoleId));
                }

                if (model.EndCreateTime.HasValue)
                {
                    query = query.Where(o => o.CreateTime <= model.EndCreateTime);
                }

                if (model.StartCreateTime.HasValue)
                {
                    query = query.Where(o => o.CreateTime >= model.StartCreateTime);
                }

                query = query.AddBooleanFilter(model.IsEnable, nameof(Entity.User.IsEnable));
                query = query.AddStringContainsFilter(model.DisplayName, nameof(Entity.User.DisplayName));
                query = query.AddStringContainsFilter(model.LoginName, nameof(Entity.User.LoginName));

                model.TotalCount = query.Count();
                if (model.PageIndex < 1)
                {
                    model.PageIndex = 1;
                }

                var list = query.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).ToList();

                IList<UserModel> models = new List<UserModel>();
                foreach (User item in list)
                {
                    models.Add(new UserModel(item));
                }

                ResponseModel response = new ResponseModel(models, model);
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 创建用户.
        /// </summary>
        /// <param name="model">用户视图实体.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public IActionResult Create(UserCreatePostModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            if (model.LoginName.Trim().Length <= 0)
            {
                response.SetFailed("请输入登录名称");
                return this.Ok(response);
            }

            using (this.DbContext)
            {
                if (this.DbContext.User.Count(x => x.LoginName == model.LoginName) > 0)
                {
                    response.SetFailed("登录名已存在");
                    return this.Ok(response);
                }

                User entity = this.Mapper.Map<UserCreatePostModel, User>(model);
                entity.CreateTime = DateTime.Now;

                // entity.Id = Guid.NewGuid();
                entity.Status = (int)model.Status;
                this.DbContext.User.Add(entity);
                this.DbContext.SaveChanges();

                return null;

                // return this.SubmitResponse(response);
            }
        }

        /// <summary>
        /// 编辑用户.
        /// </summary>
        /// <param name="id">用户GUID.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult FindById(int id)
        {
            using (this.DbContext)
            {
                this.DbContext.Set<UserRoleMapping>().Load();
                this.DbContext.Set<Role>().Load();
                User entity = this.DbContext.User.Find(id);
                UserModel model = new UserModel(entity);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(model);

                return this.Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的用户信息.
        /// </summary>
        /// <param name="model">用户视图实体.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(UserEditPostModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this.DbContext)
            {
                this.DbContext.Set<UserRoleMapping>().Load();
                this.DbContext.Set<UserStatus>().Load();
                this.DbContext.Set<Role>().Load();
                User entity = this.DbContext.User.Find(model.Id);
                if (entity == null)
                {
                    response.SetFailed("用户不存在");
                    return this.Ok(response);
                }

                entity.DisplayName = model.DisplayName;
                entity.LoginName = model.LoginName;
                entity.Password = model.Password;
                entity.UpdateTime = DateTime.Now;

                if (model.UserRole.HasValue)
                {
                    var userRoleMapping = this.DbContext.UserRoleMapping.FirstOrDefault(x => x.UserId == model.Id);
                    if (userRoleMapping != null)
                    {
                        userRoleMapping.RoleId = (int)model.UserRole.Value;
                    }
                    else
                    {
                        this.DbContext.UserRoleMapping.Add(new UserRoleMapping
                        {
                            UserId = model.Id,
                            RoleId = (int)model.UserRole
                        });
                    }
                }

                this.DbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
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
            ResponseModel response = UserControllerHelper.UpdateIsDeleted(true, ids);
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
            ResponseModel response = UserControllerHelper.UpdateIsDeleted(false, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 恢复用户.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Normal(int[] ids)
        {
            ResponseModel response = UserControllerHelper.UpdateStatus(true, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 恢复用户.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Forbidden(int[] ids)
        {
            ResponseModel response = UserControllerHelper.UpdateStatus(false, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 保存用户-角色的关系映射数据.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveRoles(SaveUserRolesViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            List<UserRoleMapping> roles = model.AssignedRoles.Select(x => new UserRoleMapping
            {
                CreateTime = DateTime.Now,

                // RoleId = x.Trim()
            }).ToList();
            this.DbContext.Database.ExecuteSqlInterpolated($"DELETE FROM DncUserRoleMapping WHERE UserGuid={model.UserGuid}");
            bool success = true;
            if (roles.Count > 0)
            {
                this.DbContext.UserRoleMapping.AddRange(roles);
                success = this.DbContext.SaveChanges() > 0;
            }

            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                response.SetFailed("保存用户角色数据失败");
            }

            return this.Ok(response);
        }
    }
}