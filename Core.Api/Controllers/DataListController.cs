using System.Linq;
using AutoMapper;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Model.Administration.Role;
using Core.Model.Administration.User;
using Core.Api.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Api.Controllers
{
    /// <summary>
    /// DataList.
    /// </summary>
    public class DataListController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataListController"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext.</param>
        /// <param name="mapper">The mapper.</param>
        public DataListController(CoreContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        [HttpGet]
        public IActionResult GetUserDataList(string name)
        {
            using (this.DbContext)
            {
                IQueryable<User> query = this.DbContext.User.AsNoTracking();
                query = query.OrderByDescending(o => o.CreateTime);
                query = query.AddBooleanFilter(o => o.IsDeleted, false);
                query = query.AddBooleanFilter(o => o.IsEnable, true);
                query = query.AddStringContainsFilter(o => o.LoginName, name);
                Pager pager = Pager.CreateDefaultInstance();

                return this.StandardSearchResponse(query, pager, UserModel.Convert);
            }
        }

        [HttpGet]
        public IActionResult GetRoleDataList(string name)
        {
            using (this.DbContext)
            {
                IQueryable<Role> query = this.DbContext.Role.AsNoTracking();
                query = query.OrderByDescending(o => o.CreateTime);
                query = query.AddBooleanFilter(o => o.IsForbidden, false);
                query = query.AddBooleanFilter(o => o.IsEnable, true);
                query = query.AddStringContainsFilter(o => o.Name, name);
                Pager pager = Pager.CreateDefaultInstance();

                return this.StandardSearchResponse(query, pager, RoleModel.Convert);
            }
        }

        [HttpGet]
        public IActionResult GetMenuDataList(string name)
        {
            using (this.DbContext)
            {
                IQueryable<Menu> query = this.DbContext.Menu.AsNoTracking();
                query = query.OrderByDescending(o => o.CreateTime);
                query = query.AddBooleanFilter(o => o.IsEnable, true);
                query = query.AddStringContainsFilter(o => o.Name, name);
                Pager pager = Pager.CreateDefaultInstance();

                return this.StandardSearchResponse(query, pager, MenuModel.Convert);
            }
        }
    }
}