using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Api.Extensions;
using Core.Api.Extensions.AuthContext;
using Core.Api.Extensions.Queryable;
using Core.Api.Models.Response;
using Core.Model.Entity;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    //[WebApiExceptionFilter]
    //[CustomAuthorize]
    public class IconController : ControllerBase
    {
        private readonly Context _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        public IconController(Context dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this._dbContext)
            {
                IQueryable<Icon> query = this._dbContext.Icon.AsQueryable();
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
            using (this._dbContext)
            {
                IQueryable<Icon> query = this._dbContext.Icon.AsQueryable();
                //Filter<Icon> filter = new Filter<Icon>(new FilterInfo<DateTime>(nameof(Icon.CreatedOn), Operation.Between, DateTime.Now, DateTime.Now));
                //query = query.AddFilter(filter);
                //query = query.ExpressionBuilder(model.Status, nameof(Icon.Status));
                query = query.AddBooleanFilter(model.IsEnable, nameof(Icon.IsEnable));
                query = query.AddStringContainsFilter(model.KeyWord, nameof(Icon.Code));
                query = query.AddBooleanFilter(model.Status, nameof(Icon.Status));
                query = query.Paged();

                List<Icon> list = query.ToList();
                int totalCount = list.Count;
                IEnumerable<IconJsonModel> data = list.Select(this._mapper.Map<Icon, IconJsonModel>);
                ResponseResultModel response = ResponseModelFactory.CreateResultInstance;
                response.SetData(data, totalCount);

                return Ok(response);
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/v1/rbac/icon/find_list_by_kw/{kw}")]
        public IActionResult Search(string kw)
        {
            ResponseResultModel response = ResponseModelFactory.CreateResultInstance;
            if (string.IsNullOrEmpty(kw))
            {
                response.SetFailed("没有查询到数据");
                return Ok(response);
            }
            using (this._dbContext)
            {
                IQueryable<Icon> query = this._dbContext.Icon.Where(x => x.Code.Contains(kw));
                List<Icon> list = query.ToList();
                var data = list.Select(x => new { x.Code, x.Color, x.Size });
                response.SetData(data);

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
            using (this._dbContext)
            {
                if (this._dbContext.Icon.Count(x => x.Code == model.Code) > 0)
                {
                    response.SetFailed("图标已存在");
                    return Ok(response);
                }
                Icon entity = this._mapper.Map<IconCreateViewModel, Icon>(model);
                entity.CreatedOn = DateTime.Now;
                entity.CreatedByUserGuid = AuthContextService.CurrentUser.Guid;
                entity.CreatedByUserName = AuthContextService.CurrentUser.DisplayName;
                this._dbContext.Icon.Add(entity);
                this._dbContext.SaveChanges();
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
            using (this._dbContext)
            {
                Icon entity = this._dbContext.Icon.FirstOrDefault(x => x.Id == id);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<Icon, IconCreateViewModel>(entity));
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
            using (this._dbContext)
            {
                if (this._dbContext.Icon.Count(x => x.Code == model.Code && x.Id != model.Id) > 0)
                {
                    response.SetFailed("图标已存在");
                    return Ok(response);
                }
                Icon entity = this._dbContext.Icon.FirstOrDefault(x => x.Id == model.Id);
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
                this._dbContext.SaveChanges();
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
            using (this._dbContext)
            {
                this._dbContext.Icon.AddRange(models);
                this._dbContext.SaveChanges();
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
            using (this._dbContext)
            {
                string sql = @"UPDATE Icon SET IsEnable = @IsEnable WHERE Id IN @Id";
                this._dbContext.Dapper.Execute(sql, new { IsEnable = isEnable, Id = ids });
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
            using (this._dbContext)
            {
                string sql = @"UPDATE Icon SET Status = @Status WHERE Id IN @Id";
                this._dbContext.Dapper.Execute(sql, new { Status = status, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }
    }
}