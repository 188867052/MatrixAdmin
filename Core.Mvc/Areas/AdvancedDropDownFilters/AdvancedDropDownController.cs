using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entity;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Model.Administration.User;
using Microsoft.AspNetCore.Mvc;
using Core.Api.Routes;
using Core.Api.Framework;

namespace Core.Mvc.Areas.AdvancedDropDownFilters
{
    [Area(nameof(AdvancedDropDownFilters))]
    public class AdvancedDropDownController : StandardController
    {
        /// <summary>
        /// Gets role data list.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> RoleDataList(string name)
        {
            ResponseModel model = await HttpClientAsync.Async<IList<Role>>(DataListRoute.GetRoleDataList, name);
            IList<Role> roles = (IList<Role>)model.Data;

            return this.DataListContent(roles, o => o.Id, o => o.Name);
        }

        [HttpGet]
        public async Task<IActionResult> UserDataList(string name)
        {
            ResponseModel model = await HttpClientAsync.Async<IList<UserModel>>(DataListRoute.GetUserDataList, name);
            IList<UserModel> users = (IList<UserModel>)model.Data;

            return this.DataListContent(users, o => o.Id, o => o.LoginName);
        }

        [HttpGet]
        public async Task<IActionResult> MenuDataList(string name)
        {
            ResponseModel model = await HttpClientAsync.Async<IList<MenuModel>>(DataListRoute.GetMenuDataList, name);
            IList<MenuModel> users = (IList<MenuModel>)model.Data;

            return this.DataListContent(users, o => o.Id, o => o.Name);
        }
    }
}