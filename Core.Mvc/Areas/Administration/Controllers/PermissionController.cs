using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Api.Framework;
using Core.Api.Routes;
using Core.Entity;
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
        public async Task<IActionResult> Index()
        {
            var model = await HttpClientAsync.Async<IList<Permission>>(PermissionRoute.Index);
            PermissionIndex table = new PermissionIndex(model);

            return this.SearchGridConfiguration(table);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> GridStateChange(PermissionPostModel model)
        {
            ResponseModel response = await HttpClientAsync.Async<IList<Permission>>(PermissionRoute.Search, model);
            PermissionGridConfiguration configuration = new PermissionGridConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}