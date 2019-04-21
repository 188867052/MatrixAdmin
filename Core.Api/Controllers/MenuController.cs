using AutoMapper;
using Core.Api.Extensions;
using Core.Api.Extensions.AuthContext;
using Core.Api.Extensions.Queryable;
using Core.Api.Models.Response;
using Core.Model.Entity;
using Core.Model.Enums;
using Core.Model.Menu;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MenuJsonModel = Core.Api.Models.Menu.MenuJsonModel;

namespace Core.Api.Controllers
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class MenuController : ControllerBase
    {
        private readonly Context _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public MenuController(Context dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (this._dbContext)
            {
                IQueryable<Menu> query = this._dbContext.Menu.AsQueryable();
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
        public IActionResult List(MenuRequestPayload request)
        {
            using (this._dbContext)
            {
                IQueryable<Menu> query = this._dbContext.Menu.AsQueryable();
                query = query.AddStringContainsFilter(request.KeyWord, nameof(Menu.Name));
                query = query.AddBooleanFilter(request.IsEnable, nameof(Menu.IsEnable));
                query = query.AddBooleanFilter(request.Status, nameof(Menu.Status));
                query = query.AddGuidEqualsFilter(request.ParentGuid, nameof(Menu.ParentGuid));
                query = query.Paged();

                List<Menu> list = query.ToList();
                int totalCount = query.Count();
                IEnumerable<MenuJsonModel> data = list.Select(_mapper.Map<Menu, MenuJsonModel>);
                ResponseResultModel response = ResponseModelFactory.CreateResultInstance;
                response.SetData(data, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="model">菜单视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(MenuCreateViewModel model)
        {
            using (this._dbContext)
            {
                Menu entity = _mapper.Map<MenuCreateViewModel, Menu>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Guid = Guid.NewGuid();
                entity.CreatedByUserGuid = AuthContextService.CurrentUser.Guid;
                entity.CreatedByUserName = AuthContextService.CurrentUser.DisplayName;
                this._dbContext.Menu.Add(entity);
                this._dbContext.SaveChanges();
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="guid">菜单ID</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            using (this._dbContext)
            {
                Menu entity = this._dbContext.Menu.FirstOrDefault(x => x.Guid == guid);
                ResponseModel response = ResponseModelFactory.CreateInstance;
                MenuEditViewModel model = _mapper.Map<Menu, MenuEditViewModel>(entity);
                //if (model.ParentGuid.HasValue)
                //{
                //    var parent = this._dbContext.DncMenu.FirstOrDefault(x => x.Guid == entity.ParentGuid);
                //    if (parent != null)
                //    {
                //        model.ParentName = parent.Name;
                //    }
                //}
                List<MenuTree> tree = LoadMenuTree(model.ParentGuid.ToString());
                response.SetData(new { model, tree });
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的菜单信息
        /// </summary>
        /// <param name="model">菜单视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(MenuEditViewModel model)
        {
            using (this._dbContext)
            {
                Menu entity = this._dbContext.Menu.FirstOrDefault(x => x.Guid == model.Guid);
                entity.Name = model.Name;
                entity.Icon = model.Icon;
                entity.Level = 1;
                entity.ParentGuid = model.ParentGuid;
                entity.Sort = model.Sort;
                entity.Url = model.Url;
                entity.ModifiedByUserGuid = AuthContextService.CurrentUser.Guid;
                entity.ModifiedByUserName = AuthContextService.CurrentUser.DisplayName;
                entity.ModifiedOn = DateTime.Now;
                entity.Description = model.Description;
                entity.ParentName = model.ParentName;
                entity.Alias = model.Alias;
                entity.IsEnable = model.IsEnable.Value;
                entity.Status = model.Status;
                entity.IsDefaultRouter = model.IsDefaultRouter;

                this._dbContext.SaveChanges();
                ResponseModel response = ResponseModelFactory.CreateInstance;
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet("{selected?}")]
        public IActionResult Tree(string selected)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            List<MenuTree> tree = LoadMenuTree(selected);
            response.SetData(tree);
            return Ok(response);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="ids">菜单ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(int[] ids)
        {
            ResponseModel response = UpdateIsEnable(true, ids);
            return Ok(response);
        }

        /// <summary>
        /// 恢复菜单
        /// </summary>
        /// <param name="ids">菜单ID,多个以逗号分隔</param>
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
        /// <param name="ids">菜单ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, int[] ids)
        {
            ResponseModel response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    response = this.UpdateIsEnable(true, ids);
                    break;
                case "recover":
                    response = this.UpdateIsEnable(false, ids);
                    break;
                case "forbidden":
                    response = this.UpdateStatus(StatusEnum.Forbidden, ids);
                    break;
                case "normal":
                    response = this.UpdateStatus(StatusEnum.Normal, ids);
                    break;
                default:
                    break;
            }
            return Ok(response);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="ids">菜单ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsEnable(bool isEnable, int[] ids)
        {
            using (this._dbContext)
            {
                string sql = @"UPDATE Menu SET IsEnable = @IsEnable WHERE Id IN @Ids";
                this._dbContext.Dapper.Execute(sql, new { IsEnable = isEnable, Ids = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="status">菜单状态</param>
        /// <param name="ids">菜单ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(StatusEnum status, int[] ids)
        {
            using (this._dbContext)
            {
                string sql = @"UPDATE Menu SET Status = @Status WHERE Guid IN @Id";
                this._dbContext.Dapper.Execute(sql, new { Status = status, Id = ids });
                return ResponseModelFactory.CreateInstance;
            }
        }

        private List<MenuTree> LoadMenuTree(string selectedGuid = null)
        {
            List<MenuTree> temp = this._dbContext.Menu.Where(x => !x.IsEnable && x.Status).ToList().Select(x => new MenuTree
            {
                Guid = x.Guid.ToString(),
                ParentGuid = x.ParentGuid,
                Title = x.Name
            }).ToList();
            MenuTree root = new MenuTree
            {
                Title = "顶级菜单",
                Guid = Guid.Empty.ToString(),
                ParentGuid = null
            };
            temp.Insert(0, root);
            List<MenuTree> tree = temp.BuildTree(selectedGuid);
            return tree;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MenuTreeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="selectedGuid"></param>
        /// <returns></returns>
        public static List<MenuTree> BuildTree(this List<MenuTree> menus, string selectedGuid = null)
        {
            ILookup<Guid?, MenuTree> lookup = menus.ToLookup(x => x.ParentGuid);
            Func<Guid?, List<MenuTree>> build = null;
            build = pid =>
            {
                return lookup[pid]
                 .Select(x => new MenuTree
                 {
                     Guid = x.Guid,
                     ParentGuid = x.ParentGuid,
                     Title = x.Title,
                     Expand = (x.ParentGuid == null || x.ParentGuid == Guid.Empty),
                     Selected = selectedGuid == x.Guid,
                     Children = build(new Guid(x.Guid)),
                 })
                 .ToList();
            };
            List<MenuTree> result = build(null);
            return result;
        }
    }
}