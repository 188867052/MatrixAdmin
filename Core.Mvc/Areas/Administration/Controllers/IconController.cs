using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Api.Framework;
using Core.Api.Routes;
using Core.Entity;
using Core.Model.Administration.Icon;
using Core.Mvc.Areas.Administration.ViewConfiguration.Icon;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    public class IconController : StandardController
    {
        /// <summary>
        /// The index page.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        public async Task<IActionResult> Index()
        {
            var responseModel = await HttpClientAsync.Async<IList<Icon>>(IconRoute.Index);
            IconIndex<IconPostModel> index = new IconIndex<IconPostModel>(responseModel);

            return SearchGridConfiguration(index);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> GridStateChange(IconPostModel model)
        {
            var response = await HttpClientAsync.Async<IList<Icon>>(IconRoute.Search, model);
            IconGridConfiguration configuration = new IconGridConfiguration(response);

            return GridConfiguration(configuration);
        }

        /// <summary>
        /// The add dialog.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        [HttpGet]
        public IActionResult AddDialog()
        {
            return null;
        }
    }
}