using AutoMapper;
using Core.Api.Extensions;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entity;
using Core.Model.Administration.Role;
using Core.Model.Administration.User;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class UserController : StandardController
    {
        public UserController(CoreApiContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this.DbContext)
            {
                DbContext.Set<UserRoleMapping>().Load();
                DbContext.Set<UserStatus>().Load();
                DbContext.Set<Role>().Load();
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

                return Ok(response);
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
                DbContext.Set<UserStatus>().Load();
                DbContext.Set<UserRoleMapping>().Load();
                DbContext.Set<Role>().Load();
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
                //ResponseModel response = new ResponseModel(list, model);
                IList<UserModel> models = new List<UserModel>();
                foreach (User item in list)
                {
                    models.Add(new UserModel(item));
                }
                ResponseModel response = new ResponseModel(models, model);
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(UserCreatePostModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            if (model.LoginName.Trim().Length <= 0)
            {
                response.SetFailed("请输入登录名称");
                return Ok(response);
            }
            using (this.DbContext)
            {
                if (this.DbContext.User.Count(x => x.LoginName == model.LoginName) > 0)
                {
                    response.SetFailed("登录名已存在");
                    return Ok(response);
                }
                User entity = Mapper.Map<UserCreatePostModel, User>(model);
                entity.CreateTime = DateTime.Now;
                //entity.Id = Guid.NewGuid();
                entity.Status = (int)model.Status;
                this.DbContext.User.Add(entity);
                this.DbContext.SaveChanges();

                return null;
                //return this.SubmitResponse(response);
            }
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="id">用户GUID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FindById(int id)
        {
            using (this.DbContext)
            {
                User entity = this.DbContext.User.Find(id);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(entity);
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的用户信息
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(UserEditPostModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this.DbContext)
            {
                DbContext.Set<UserRoleMapping>().Load();
                DbContext.Set<UserStatus>().Load();
                DbContext.Set<Role>().Load();
                User entity = this.DbContext.User.FirstOrDefault(x => x.Id == model.Id);
                if (entity == null)
                {
                    response.SetFailed("用户不存在");
                    return Ok(response);
                }
                entity.DisplayName = model.DisplayName;
                entity.LoginName = model.LoginName;
                entity.Password = model.Password;
                //if (model.UserRole.HasValue)
                //{
                //    var userRoleMapping = DbContext.UserRoleMapping.FirstOrDefault(x => x.UserId == model.Id);
                //    if (userRoleMapping!=null)
                //    {
                //        userRoleMapping.RoleId = (int)model.UserRole.Value;
                //    }
                //}
                entity.UpdateTime = DateTime.Now;
                this.DbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int[] ids)
        {
            ResponseModel response = this.UpdateIsDeleted(true, ids);
            return Ok(response);
        }

        /// <summary>
        /// 恢复用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Recover(int[] ids)
        {
            ResponseModel response = UpdateIsDeleted(false, ids);
            return Ok(response);
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">用户ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, int[] ids)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    response = UpdateIsDeleted(true, ids);
                    break;
                case "recover":
                    response = UpdateIsDeleted(false, ids);
                    break;
                case "forbidden":
                    response = UpdateStatus(false, ids);
                    break;
                case "normal":
                    response = UpdateStatus(true, ids);
                    break;
                default:
                    break;
            }
            return Ok(response);
        }

        #region 用户-角色
        /// <summary>
        /// 保存用户-角色的关系映射数据
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
                //RoleId = x.Trim()
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
            return Ok(response);
        }
        #endregion

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids">用户ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDeleted(bool isDeleted, int[] ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE [User] SET IsDeleted = @IsDeleted WHERE Id IN @Id";
                //this.DbContext.Dapper.Execute(sql, new { IsDeleted = isDeleted, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="status">用户状态</param>
        /// <param name="ids">用户ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(bool status, int[] ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE [User] SET IsEnable = @IsEnable WHERE Id IN @Id";
                //this.DbContext.Dapper.Execute(sql, new { Status = status, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }
    }
}