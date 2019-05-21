using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Mvc.Framework;
using Microsoft.AspNetCore.Mvc;
using ApiController = Core.Api.Controllers.RoleController;

namespace Core.Mvc.Areas.AdvancedDropDown
{
    [Area(nameof(AdvancedDropDown))]
    public class AdvancedDropDownController : StandardController
    {
        /// <summary>
        /// Gets role data list.
        /// </summary>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult RoleDataList()
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.GetRoleDataList));
            ResponseModel model = HttpClientAsync.GetAsync<IList<Role>>(url).Result;
            IList<Role> roles = (IList<Role>)model.Data;
            string options = roles.Aggregate(string.Empty, (current, role) => current + $"<option key=\"{role.Id}\" value=\"{role.Name}\"></option>");

            return this.Content(options, "text/html", Encoding.UTF8);
        }
    }
}