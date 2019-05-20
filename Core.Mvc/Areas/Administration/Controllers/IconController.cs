using System.Collections.Generic;
using Core.Entity;
using Core.Extension;
using Core.Model.Administration.Icon;
using Core.Mvc.Areas.Administration.ViewConfiguration.Icon;
using Core.Mvc.Areas.Administration.ViewConfiguration.User;
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
        public IActionResult Index()
        {
            var url = new Url(typeof(Api.Controllers.IconController), nameof(Api.Controllers.IconController.Index));
            var responseModel = HttpClientAsync.GetAsync<IList<Icon>>(url).Result;
            IconIndex index = new IconIndex(responseModel);

            return this.ViewConfiguration(index);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public IActionResult GridStateChange(IconPostModel model)
        {
            var url = new Url(typeof(Api.Controllers.IconController), nameof(Api.Controllers.IconController.Search));
            var response = HttpClientAsync.PostAsync<IList<Icon>, IconPostModel>(url, model).Result;
            IconGridConfiguration configuration = new IconGridConfiguration(response);

            return this.GridConfiguration(configuration);
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