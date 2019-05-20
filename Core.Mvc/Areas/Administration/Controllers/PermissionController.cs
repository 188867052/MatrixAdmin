using System.Collections.Generic;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Permission;
using Core.Mvc.Areas.Administration.ViewConfiguration.Permission;
using Microsoft.AspNetCore.Mvc;
using ApiController = Core.Api.Controllers.PermissionController;

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
            var url = new Url(typeof(ApiController), nameof(ApiController.Index));
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
            var url = new Url(typeof(ApiController), nameof(ApiController.Search));
            ResponseModel response = HttpClientAsync.PostAsync<IList<Permission>, PermissionPostModel>(url, model).Result;
            PermissionGridConfiguration configuration = new PermissionGridConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}