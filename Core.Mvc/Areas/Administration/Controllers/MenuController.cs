using System.Collections.Generic;
using ConsoleApp.DataModels;
using Core.Extension;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.ViewConfiguration.Menu;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]

    public class MenuController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuController"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The hostingEnvironment.</param>
        public MenuController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        /// <summary>
        /// The index page.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        public IActionResult Index()
        {
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Index));
            var response = AsyncRequest.GetAsync<IList<Menu>>(url).Result;
            MenuIndex index = new MenuIndex(this.HostingEnvironment, response);

            return this.ViewConfiguration(index);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">A model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public IActionResult GridStateChange(MenuPostModel model)
        {
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Search));
            var response = AsyncRequest.PostAsync<IList<Menu>, MenuPostModel>(url, model).Result;
            MenuViewConfiguration configuration = new MenuViewConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}