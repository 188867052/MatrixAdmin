using System.Collections.Generic;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Permission;
using Core.Mvc.Areas.Administration.ViewConfiguration.Permission;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    public class PermissionController : StandardController
    {
        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        public IActionResult Index()
        {
            var url = new Url(typeof(Api.Controllers.PermissionController), nameof(Api.Controllers.PermissionController.Index));
            var model = HttpClientAsync.GetAsync<IList<Permission>>(url).Result;
            PermissionIndex table = new PermissionIndex(model);

            return this.ViewConfiguration(table);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public IActionResult GridStateChange(PermissionPostModel model)
        {
            var url = new Url(typeof(Api.Controllers.PermissionController), nameof(Api.Controllers.PermissionController.Search));
            ResponseModel response = HttpClientAsync.PostAsync<IList<Permission>, PermissionPostModel>(url, model).Result;
            PermissionGridConfiguration configuration = new PermissionGridConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}