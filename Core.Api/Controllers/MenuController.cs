﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Api.ControllerHelpers;
using Core.Api.Framework;
using Core.Entity;
using Core.Entity.Enums;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Menu;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 菜单.
    /// </summary>
    // [CustomAuthorize]
    public class MenuController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        public MenuController(CoreContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this.DbContext)
            {
                IQueryable<Menu> query = this.DbContext.Menu.AsNoTracking();
                query = query.OrderByDescending(o => o.CreateTime);
                Pager pager = Pager.CreateDefaultInstance();

                return this.StandardSearchResponse(query, pager, MenuModel.Convert);
            }
        }

        /// <summary>
        /// Search.
        /// </summary>
        /// <param name="model">model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public IActionResult Search(MenuPostModel model)
        {
            using (this.DbContext)
            {
                IQueryable<Menu> query = this.DbContext.Menu.AsNoTracking();
                query = query.AddStringContainsFilter(o => o.Name, model.Name);
                query = query.AddStringContainsFilter(o => o.Description, model.Description);
                query = query.AddFilter(o => o.Status, model.Status);
                query = query.AddDateTimeBetweenFilter(model.StartCreateTime, model.EndCreateTime, o => o.CreateTime);
                query = query.OrderByDescending(o => o.CreateTime);

                return this.StandardSearchResponse(query, model, MenuModel.Convert);
            }
        }

        /// <summary>
        /// 根据id查询.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            using (this.DbContext)
            {
                Menu entity = this.DbContext.Menu.Find(id);
                MenuModel model = new MenuModel(entity);
                HttpResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(model);

                return this.Ok(response);
            }
        }

        /// <summary>
        /// 创建菜单.
        /// </summary>
        /// <param name="model">菜单视图实体.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(MenuCreatePostModel model)
        {
            using (this.DbContext)
            {
                Menu entity = this.Mapper.Map<MenuCreatePostModel, Menu>(model);
                entity.CreateByUserId = 1;
                entity.CreateByUserName = "管理员";
                this.DbContext.Menu.Add(entity);
                this.DbContext.SaveChanges();
                HttpResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetSuccess();

                return this.Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的菜单信息.
        /// </summary>
        /// <param name="model">菜单视图实体.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(MenuEditPostModel model)
        {
            using (this.DbContext)
            {
                Menu entity = this.DbContext.Menu.Find(model.Id);
                model.MapTo(entity);

                this.DbContext.SaveChanges();
                HttpResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetSuccess();
                return this.Ok(response);
            }
        }

        /// <summary>
        /// 菜单树.
        /// </summary>
        /// <param name="selected">selected.</param>
        /// <returns></returns>
        [HttpGet("{selected?}")]
        public IActionResult Tree(string selected)
        {
            HttpResponseModel response = ResponseModelFactory.CreateInstance;
            List<MenuTree> tree = this.LoadMenuTree(selected);
            response.SetData(tree);
            return this.Ok(response);
        }

        /// <summary>
        /// 删除菜单.
        /// </summary>
        /// <param name="ids">菜单ID,多个以逗号分隔.</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(int[] ids)
        {
            HttpResponseModel response = this.UpdateIsEnable(true, ids);
            return this.Ok(response);
        }

        /// <summary>
        /// 恢复菜单.
        /// </summary>
        /// <param name="ids">菜单ID,多个以逗号分隔.</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Recover(int[] ids)
        {
            HttpResponseModel response = this.UpdateIsEnable(false, ids);
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
            HttpResponseModel response = MenuControllerHelper.UpdateStatus(true, ids);
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
            HttpResponseModel response = MenuControllerHelper.UpdateStatus(false, ids);
            return this.Ok(response);
        }

        private List<MenuTree> LoadMenuTree(string selectedGuid = null)
        {
            List<MenuTree> temp = this.DbContext.Menu.AsNoTracking().Where(x => !x.IsEnable && x.Status).ToList().Select(x => new MenuTree
            {
                ParentId = x.ParentId,
                Title = x.Name
            }).ToList();
            MenuTree root = new MenuTree
            {
                Title = "顶级菜单",
                Id = 1,
                ParentId = null
            };
            temp.Insert(0, root);
            List<MenuTree> tree = temp.BuildTree(selectedGuid);
            return tree;
        }

        /// <summary>
        /// 删除菜单.
        /// </summary>
        /// <param name="isEnable">isEnable.</param>
        /// <param name="ids">菜单ID字符串,多个以逗号隔开.</param>
        /// <returns></returns>
        private HttpResponseModel UpdateIsEnable(bool isEnable, int[] ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE Menu SET IsEnable = @IsEnable WHERE Id IN @Ids";
                CoreContext.Dapper.Execute(sql, new { IsEnable = isEnable, Ids = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }

        /// <summary>
        /// 删除菜单.
        /// </summary>
        /// <param name="status">菜单状态.</param>
        /// <param name="ids">菜单ID字符串,多个以逗号隔开.</param>
        /// <returns></returns>
        private HttpResponseModel UpdateStatus(StatusEnum status, int[] ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE Menu SET Status = @Status WHERE Guid IN @Id";
                CoreContext.Dapper.Execute(sql, new { Status = status, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }
    }
}