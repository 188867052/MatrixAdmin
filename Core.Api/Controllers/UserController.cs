using System.Linq;
using AutoMapper;
using Core.Api.ControllerHelpers;
using Core.Entity;
using Core.Model;
using Core.Model.Administration.User;
using Core.Api.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resources = Core.Api.Resource.Controllers.UserControllerResource;
using Core.Api.Extensions.CustomException;

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
        public UserController(CoreContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        [CustomAuthorize]
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
                response.SetFailed(Resources.PleaseInputLoginName);
                return this.Ok(response);
            }

            using (this.DbContext)
            {
                if (this.DbContext.User.Count(x => x.LoginName == model.LoginName) > 0)
                {
                    response.SetFailed(Resources.UserNameHasExist);
                    return this.Ok(response);
                }

                User entity = model.MapTo(this.Mapper);
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
                    response.SetFailed(Resources.UserNotExist);
                    return this.Ok(response);
                }

                model.MapTo(entity);

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
        public IActionResult Enable(int[] ids)
        {
            ResponseModel response = UserControllerHelper.UpdateIsEnable(true, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 禁止用户.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Disable(int[] ids)
        {
            ResponseModel response = UserControllerHelper.UpdateIsEnable(false, ids);
            return this.Ok(response);
        }
    }
}