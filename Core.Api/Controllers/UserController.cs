using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Api.Extensions;
using Core.Api.Extensions.AuthContext;
using Core.Api.Extensions.CustomException;
using Core.Api.Extensions.DataAccess;
using Core.Api.Extensions.Queryable;
using Core.Api.Models.Response;
using Core.Models;
using Core.Models.Entities;
using Core.Models.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [CustomAuthorize]
    [Route("[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class UserController : ControllerBase
    {
        private readonly Context _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public UserController(Context dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(UserRequestPayload model)
        {
            using (this._dbContext)
            {
                IQueryable<User> query = this._dbContext.User.AsQueryable();
                if (!string.IsNullOrEmpty(model.KeyWord))
                {
                    query = query.Where(x => x.LoginName.Contains(model.KeyWord.Trim()) || x.DisplayName.Contains(model.KeyWord.Trim()));
                }
                query = query.AddBooleanFilter(model.IsEnable, nameof(Core.Models.Entities.User.IsEnable));
                query = query.AddBooleanFilter(model.Status, nameof(Core.Models.Entities.User.Status));
                query = query.Paged(model.CurrentPage, model.PageSize);
                if (model.FirstSort != null)
                {
                    query = query.OrderBy(model.FirstSort.Field, model.FirstSort.Direct == "DESC");
                }
                List<User> list = query.ToList();
                int totalCount = query.Count();
                IEnumerable<UserJsonModel> data = list.Select(_mapper.Map<User, UserJsonModel>);
                ResponseResultModel response = ResponseModelFactory.CreateResultInstance;
                response.SetData(data, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(UserCreateViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            if (model.LoginName.Trim().Length <= 0)
            {
                response.SetFailed("请输入登录名称");
                return Ok(response);
            }
            using (this._dbContext)
            {
                if (this._dbContext.User.Count(x => x.LoginName == model.LoginName) > 0)
                {
                    response.SetFailed("登录名已存在");
                    return Ok(response);
                }
                User entity = _mapper.Map<UserCreateViewModel, User>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Guid = Guid.NewGuid();
                entity.Status = model.Status;
                this._dbContext.User.Add(entity);
                this._dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="guid">用户GUID</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            using (this._dbContext)
            {
                User entity = this._dbContext.User.FirstOrDefault(x => x.Guid == guid);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<User, UserEditViewModel>(entity));
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
        public IActionResult Edit(UserEditViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            using (this._dbContext)
            {
                User entity = this._dbContext.User.FirstOrDefault(x => x.Guid == model.Guid);
                if (entity == null)
                {
                    response.SetFailed("用户不存在");
                    return Ok(response);
                }
                entity.DisplayName = model.DisplayName;
                entity.IsEnable = model.IsEnable.Value;
                entity.IsLocked = model.IsLocked;
                entity.ModifiedByUserGuid = AuthContextService.CurrentUser.Guid;
                entity.ModifiedByUserName = AuthContextService.CurrentUser.DisplayName;
                entity.ModifiedOn = DateTime.Now;
                entity.Password = model.Password;
                entity.Status = model.Status;
                entity.UserType = model.UserType;
                entity.Description = model.Description;
                this._dbContext.SaveChanges();
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(int[] ids)
        {
            ResponseModel response = this.UpdateIsEnable(true, ids);
            return Ok(response);
        }

        /// <summary>
        /// 恢复用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Recover(int[] ids)
        {
            ResponseModel response = UpdateIsEnable(false, ids);
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
                    response = UpdateIsEnable(true, ids);
                    break;
                case "recover":
                    response = UpdateIsEnable(false, ids);
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveRoles(SaveUserRolesViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            List<UserRoleMapping> roles = model.AssignedRoles.Select(x => new UserRoleMapping
            {
                UserGuid = model.UserGuid,
                CreatedOn = DateTime.Now,
                RoleCode = x.Trim()
            }).ToList();
            this._dbContext.Database.ExecuteSqlCommand("DELETE FROM DncUserRoleMapping WHERE UserGuid={0}", model.UserGuid);
            bool success = true;
            if (roles.Count > 0)
            {
                this._dbContext.UserRoleMapping.AddRange(roles);
                success = this._dbContext.SaveChanges() > 0;
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
        /// <param name="isEnable"></param>
        /// <param name="ids">用户ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsEnable(bool isEnable, int[] ids)
        {
            using (this._dbContext)
            {
                string sql = @"UPDATE User SET IsEnable = @IsEnable WHERE Id IN @Id";
                this._dbContext.Dapper.Execute(sql, new { IsEnable = isEnable, Id = ids });
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
            using (this._dbContext)
            {
                string sql = @"UPDATE User SET IsEnable = @IsEnable WHERE Guid IN @Id";
                this._dbContext.Dapper.Execute(sql, new { Status = status, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }
    }
}