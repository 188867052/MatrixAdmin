using System;
using System.Linq;
using AutoMapper;
using Core.Api.ControllerHelpers;
using Core.Entity;
using Core.Model;
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
                IQueryable<User> query = this.DbContext.User;
                query = query.OrderByDescending(o => o.CreateTime);
                Pager pager = Pager.CreateDefaultInstance();

                return this.StandardSearchResponse(query, pager, UserModel.Convert);
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
                IQueryable<User> query = this.DbContext.User;
                query = model.GenerateQuery(query);

                return this.StandardSearchResponse(query, model, UserModel.Convert);
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
            if (model.LoginName.Length <= 0)
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
                if (model.UserRole.HasValue)
                {
                    entity.UserRoleMapping.Add(new UserRoleMapping
                    {
                        UserId = entity.Id,
                        RoleId = (int)model.UserRole
                    });
                }

                this.DbContext.User.Add(entity);
                this.DbContext.SaveChanges();

                return this.SubmitResponse(response);
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
                    if (entity.RoleMapping != null)
                    {
                        entity.RoleMapping.RoleId = (int)model.UserRole.Value;
                    }
                    else
                    {
                        entity.UserRoleMapping.Add(new UserRoleMapping
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
        /// 启用用户.
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
        /// 禁止用户.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Forbidden(int[] ids)
        {
            ResponseModel response = UserControllerHelper.UpdateStatus(false, ids);
            return this.Ok(response);
        }
    }
}