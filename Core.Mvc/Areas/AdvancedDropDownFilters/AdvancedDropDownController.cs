using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Api.Controllers;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Model.Administration.User;
using Core.Mvc.Framework;
using Microsoft.AspNetCore.Mvc;

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
            var url = new Url(typeof(DataListController), nameof(DataListController.GetRoleDataList));
            ResponseModel model = await HttpClientAsync.GetAsync<IList<Role>>(url, name);
            IList<Role> roles = (IList<Role>)model.Data;

            return this.DataListContent(roles, o => o.Id, o => o.Name);
        }

        [HttpGet]
        public async Task<IActionResult> UserDataList(string name)
        {
            var url = new Url(typeof(DataListController), nameof(DataListController.GetUserDataList));
            ResponseModel model = await HttpClientAsync.GetAsync<IList<UserModel>>(url, name);
            IList<UserModel> users = (IList<UserModel>)model.Data;

            return this.DataListContent(users, o => o.Id, o => o.LoginName);
        }

        [HttpGet]
        public async Task<IActionResult> MenuDataList(string name)
        {
            var url = new Url(typeof(DataListController), nameof(DataListController.GetMenuDataList));
            ResponseModel model = await HttpClientAsync.GetAsync<IList<MenuModel>>(url, name);
            IList<MenuModel> users = (IList<MenuModel>)model.Data;

            return this.DataListContent(users, o => o.Id, o => o.Name);
        }
    }
}