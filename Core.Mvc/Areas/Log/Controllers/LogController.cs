using System.Collections.Generic;
using System.Text;
using Core.Extension;
using Core.Model;
using Core.Model.Log;
using Core.Mvc.Areas.Log.ViewConfiguration;
using Core.Web.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Log.Controllers
{
    [Area(nameof(Log))]
    public class LogController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogController"/> class.
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public LogController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var url = new Url(typeof(Api.Controllers.LogController), nameof(Api.Controllers.LogController.Index));
            var model = AsyncRequest.GetAsync<IList<Model.Log.Log>>(url).Result;
            LogIndex table = new LogIndex(this.HostingEnvironment, model);

            return this.ViewConfiguration(table);
        }

        public IActionResult Test()
        {
            File index = new File(this.HostingEnvironment, "a_test");
            return this.Content(index.Render(), "text/html", Encoding.UTF8);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GridStateChange(LogPostModel model)
        {
            var url = new Url(typeof(Api.Controllers.LogController), nameof(Api.Controllers.LogController.Search));
            ResponseModel response = AsyncRequest.PostAsync<IList<Model.Log.Log>, LogPostModel>(url, model).Result;
            LogGridConfiguration configuration = new LogGridConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}