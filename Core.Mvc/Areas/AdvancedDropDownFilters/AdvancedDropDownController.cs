using System.Collections.Generic;
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
        public IActionResult RoleDataList(string name)
        {
            var url = new Url(typeof(DataListController), nameof(DataListController.GetRoleDataList));
            ResponseModel model = HttpClientAsync.GetAsync<IList<Role>>(url, name).Result;
            IList<Role> roles = (IList<Role>)model.Data;

            return this.DataListContent(roles, o => o.Id, o => o.Name);
        }

        [HttpGet]
        public IActionResult UserDataList(string name)
        {
            var url = new Url(typeof(DataListController), nameof(DataListController.GetUserDataList));
            ResponseModel model = HttpClientAsync.GetAsync<IList<UserModel>>(url, name).Result;
            IList<UserModel> users = (IList<UserModel>)model.Data;

            return this.DataListContent(users, o => o.Id, o => o.LoginName);
        }

        [HttpGet]
        public IActionResult MenuDataList(string name)
        {
            var url = new Url(typeof(DataListController), nameof(DataListController.GetMenuDataList));
            ResponseModel model = HttpClientAsync.GetAsync<IList<MenuModel>>(url, name).Result;
            IList<MenuModel> users = (IList<MenuModel>)model.Data;

            return this.DataListContent(users, o => o.Id, o => o.Name);
        }
    }
}