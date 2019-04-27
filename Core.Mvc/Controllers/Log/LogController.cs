using System.Collections.Generic;
using Core.Extension;
using Core.Model;
using Core.Model.Log;
using Core.Mvc.ViewConfiguration.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers.Log
{
    [Area(nameof(Log))]
    public class LogController : StandardController
    {
        /// <summary>
        /// 构造函数
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
            LogIndex table = new LogIndex(HostingEnvironment, model);

            return this.ViewConfiguration(table);
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