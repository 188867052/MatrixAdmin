using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Model.Log;
using Core.Mvc.Areas.Log.ViewConfiguration;
using Microsoft.AspNetCore.Mvc;
using ApiController = Core.Api.Controllers.LogController;

namespace Core.Mvc.Areas.Log.Controllers
{
    [Area(nameof(Log))]
    public class LogController : StandardController
    {
        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        public IActionResult Index()
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Index));
            var model = HttpClientAsync.GetAsync<IList<Entity.Log>>(url).Result;
            LogIndex table = new LogIndex(model);

            return this.ViewConfiguration(table);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult Clear()
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Clear));
            ResponseModel model = HttpClientAsync.DeleteAsync(url).Result;

            return this.Submit(model);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public IActionResult GridStateChange(LogPostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Search));
            ResponseModel response = HttpClientAsync.PostAsync<IList<Entity.Log>, LogPostModel>(url, model).Result;
            LogGridConfiguration configuration = new LogGridConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}