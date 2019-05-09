using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Api.ControllerHelpers;
using Core.Api.Extensions;
using Core.Entity;
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
    public partial class UserController : StandardController
    {
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
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Search(UserPostModel model)
        {
            using (this.DbContext)
            {
                this.DbContext.Set<UserStatus>().Load();
                this.DbContext.Set<UserRoleMapping>().Load();
                this.DbContext.Set<Role>().Load();
                IQueryable<User> query = this.DbContext.User.AsQueryable();
                query = query.AddBooleanFilter(model.IsEnable, nameof(Entity.User.IsEnable));
                if (model.Status.HasValue)
                {
                    query = query.Where(o => o.Status == (int)model.Status);
                }

                if (model.RoleId.HasValue)
                {
                    query = query.Where(o => o.UserRoleMapping.Any(u => u.Role.Id == model.RoleId));
                }

                query = query.AddStringContainsFilter(model.DisplayName, nameof(Entity.User.DisplayName));
                query = query.AddStringContainsFilter(model.LoginName, nameof(Entity.User.LoginName));

                model.TotalCount = query.Count();
                if (model.PageIndex < 1)
                {
                    model.PageIndex = 1;
                }

                var list = query.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).ToList();

                // ResponseModel response = new ResponseModel(list, model);
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
        /// <returns></returns>
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
        /// <returns></returns>
        [HttpGet]
        public IActionResult FindById(int id)
        {
            using (this.DbContext)
            {
                User entity = this.DbContext.User.Find(id);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(entity);
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的用户信息.
        /// </summary>
        /// <param name="model">用户视图实体.</param>
        /// <returns></returns>
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
                User entity = this.DbContext.User.FirstOrDefault(x => x.Id == model.Id);
                if (entity == null)
                {
                    response.SetFailed("用户不存在");
                    return this.Ok(response);
                }

                entity.DisplayName = model.DisplayName;
                entity.LoginName = model.LoginName;
                entity.Password = model.Password;

                // if (model.UserRole.HasValue)
                // {
                //    var userRoleMapping = DbContext.UserRoleMapping.FirstOrDefault(x => x.UserId == model.Id);
                //    if (userRoleMapping!=null)
                //    {
                //        userRoleMapping.RoleId = (int)model.UserRole.Value;
                //    }
                // }
                entity.UpdateTime = DateTime.Now;
                this.DbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int[] ids)
        {
            ResponseModel response = UserControllerHelper.UpdateIsDeleted(true, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 恢复用户.
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Recover(int[] ids)
        {
            ResponseModel response = UserControllerHelper.UpdateIsDeleted(false, ids);

            return this.Ok(response);
        }

        /// <summary>
        /// 批量操作.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">用户ID,多个以逗号分隔.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, int[] ids)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    response = UserControllerHelper.UpdateIsDeleted(true, ids);
                    break;
                case "recover":
                    response = UserControllerHelper.UpdateIsDeleted(false, ids);
                    break;
                case "forbidden":
                    response = UserControllerHelper.UpdateStatus(false, ids);
                    break;
                case "normal":
                    response = UserControllerHelper.UpdateStatus(true, ids);
                    break;
                default:
                    break;
            }

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
                CreatedTime = DateTime.Now,

                // RoleId = x.Trim()
            }).ToList();
            this.DbContext.Database.ExecuteSqlCommand("DELETE FROM DncUserRoleMapping WHERE UserGuid={0}", model.UserGuid);
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