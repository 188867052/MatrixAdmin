using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.User;
using Core.Mvc.Framework;
using Microsoft.AspNetCore.Mvc;
using ApiRole = Core.Api.Controllers.RoleController;
using ApiUser = Core.Api.Controllers.UserController;

namespace Core.Mvc.Areas.AdvancedDropDownFilters
{
    [Area(nameof(AdvancedDropDownFilters))]
    public class AdvancedDropDownController : StandardController
    {
        /// <summary>
        /// Gets role data list.
        /// </summary>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult RoleDataList()
        {
            var url = new Url(typeof(ApiRole), nameof(ApiRole.GetRoleDataList));
            ResponseModel model = HttpClientAsync.GetAsync<IList<Role>>(url).Result;
            IList<Role> roles = (IList<Role>)model.Data;
            string options = roles.Aggregate(string.Empty, (current, role) => current + $"<option key=\"{role.Id}\" value=\"{role.Name}\"></option>");

            return this.Content(options, "text/html", Encoding.UTF8);
        }

        [HttpGet]
        public IActionResult UserDataList()
        {
            var url = new Url(typeof(ApiUser), nameof(ApiUser.GetUserDataList));
            ResponseModel model = HttpClientAsync.GetAsync<IList<UserModel>>(url).Result;
            IList<UserModel> users = (IList<UserModel>)model.Data;
            string options = users.Aggregate(string.Empty, (current, user) => current + $"<option key=\"{user.Id}\" value=\"{user.LoginName}\"></option>");

            return this.Content(options, "text/html", Encoding.UTF8);
        }
    }
}