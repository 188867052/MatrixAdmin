using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Api.Extensions;
using Core.Api.Extensions.AuthContext;
using Core.Model.Entity;
using Core.Model.PostModel;
using Core.Model.ResponseModels;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    public class IconController : StandardController
    {
        public IconController(Context dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this.DbContext)
            {
                IQueryable<Icon> query = this.DbContext.Icon.AsQueryable();
                query = query.Paged();
                var list = query.ToList();
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(list);
                return Ok(response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(IconPostedModel model)
        {
            using (this.DbContext)
            {
                IQueryable<Icon> query = this.DbContext.Icon.AsQueryable();
                //Filter<Icon> filter = new Filter<Icon>(new FilterInfo<DateTime>(nameof(Icon.CreatedOn), Operation.Between, DateTime.Now, DateTime.Now));
                //query = query.AddFilter(filter);
                //query = query.ExpressionBuilder(model.Status, nameof(Icon.Status));
                query = query.AddBooleanFilter(model.IsEnable, nameof(Icon.IsEnable));
                query = query.AddStringContainsFilter(model.KeyWord, nameof(Icon.Code));
                query = query.AddBooleanFilter(model.Status, nameof(Icon.Status));
                int totalCount = query.Count();

                query = query.Paged();

                List<Icon> list = query.ToList();
                IEnumerable<IconJsonModel> data = list.Select(this.Mapper.Map<Icon, IconJsonModel>);
                ResponseResultModel response = ResponseModelFactory.CreateResultInstance;
                response.SetData(data, totalCount);

                return Ok(response);
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        public IActionResult Search(IconPostModel model)
        {
            ResponseResultModel response = ResponseModelFactory.CreateResultInstance;
            using (this.DbContext)
            {
                IQueryable<Icon> query = this.DbContext.Icon.AsQueryable();
                if (!string.IsNullOrEmpty(model.Code))
                {
                    query = query.Where(x => x.Code.Contains(model.Code));
                }
                //if (postModel.Status)
                {
                    query = query.Where(x => x.IsEnable == model.IsEnable);
                }
                int count = query.Count();
                query = query.Paged();
                List<Icon> list = query.ToList();
                response.SetData(list, count);

                return Ok(response);
            }
        }

        /// <summary>
        /// 创建图标
        /// </summary>
        /// <param name="model">图标视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(IconCreateViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            if (model.Code.Trim().Length <= 0)
            {
                response.SetFailed("请输入图标名称");
                return Ok(response);
            }
            using (this.DbContext)
            {
                if (this.DbContext.Icon.Count(x => x.Code == model.Code) > 0)
                {
                    response.SetFailed("图标已存在");
                    return Ok(response);
                }
                Icon entity = this.Mapper.Map<IconCreateViewModel, Icon>(model);
                entity.CreatedOn = DateTime.Now;
                entity.CreatedByUserGuid = AuthContextService.CurrentUser.Guid;
                entity.CreatedByUserName = AuthContextService.CurrentUser.DisplayName;
                this.DbContext.Icon.Add(entity);
                this.DbContext.SaveChanges();
                response.SetSuccess();

                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑图标
        /// </summary>
        /// <param name="id">图标ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int id)
        {
            using (this.DbContext)
            {
                Icon entity = this.DbContext.Icon.FirstOrDefault(x => x.Id == id);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(Mapper.Map<Icon, IconCreateViewModel>(entity));
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的图标信息
        /// </summary>
        /// <param name="model">图标视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(IconCreateViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            if (model.Code.Trim().Length <= 0)
            {
                response.SetFailed("请输入图标名称");
                return Ok(response);
            }
            using (this.DbContext)
            {
                if (this.DbContext.Icon.Count(x => x.Code == model.Code && x.Id != model.Id) > 0)
                {
                    response.SetFailed("图标已存在");
                    return Ok(response);
                }
                Icon entity = this.DbContext.Icon.FirstOrDefault(x => x.Id == model.Id);
                entity.Code = model.Code;
                entity.Color = model.Color;
                entity.Custom = model.Custom;
                entity.Size = model.Size;
                entity.IsEnable = model.IsDeleted;
                entity.ModifiedByUserGuid = AuthContextService.CurrentUser.Guid;
                entity.ModifiedByUserName = AuthContextService.CurrentUser.DisplayName;
                entity.ModifiedOn = DateTime.Now;
                entity.Status = model.Status.Value;
                entity.Description = model.Description;
                this.DbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除图标
        /// </summary>
        /// <param name="ids">图标ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(int[] ids)
        {
            ResponseModel response = UpdateIsEnable(false, ids);
            return Ok(response);
        }

        /// <summary>
        /// 恢复图标
        /// </summary>
        /// <param name="ids">图标ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Recover(int[] ids)
        {
            ResponseModel response = UpdateIsEnable(true, ids);
            return Ok(response);
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">图标ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, int[] ids)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    response = this.UpdateIsEnable(false, ids);
                    break;

                case "recover":
                    response = this.UpdateIsEnable(true, ids);
                    break;

                case "forbidden":
                    response = this.UpdateStatus(false, ids);
                    break;

                case "normal":
                    response = this.UpdateStatus(true, ids);
                    break;
            }
            return Ok(response);
        }

        /// <summary>
        /// 创建图标
        /// </summary>
        /// <param name="model">多行图标视图</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Import(IconImportViewModel model)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            if (model.Icons.Trim().Length <= 0)
            {
                response.SetFailed("没有可用的图标");
                return Ok(response);
            }
            IEnumerable<Icon> models = model.Icons.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Icon
            {
                Code = x.Trim(),
                CreatedByUserGuid = AuthContextService.CurrentUser.Guid,
                CreatedOn = DateTime.Now,
                CreatedByUserName = "超级管理员"
            });
            using (this.DbContext)
            {
                this.DbContext.Icon.AddRange(models);
                this.DbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 修改状态IsEnable
        /// </summary>
        /// <param name="isEnable">is enable</param>
        /// <param name="ids">图标ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsEnable(bool isEnable, int[] ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE Icon SET IsEnable = @IsEnable WHERE Id IN @Id";
                this.DbContext.Dapper.Execute(sql, new { IsEnable = isEnable, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }

        /// <summary>
        /// 删除图标
        /// </summary>
        /// <param name="status">图标状态</param>
        /// <param name="ids">图标ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(bool status, int[] ids)
        {
            using (this.DbContext)
            {
                string sql = @"UPDATE Icon SET Status = @Status WHERE Id IN @Id";
                this.DbContext.Dapper.Execute(sql, new { Status = status, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }
    }
}