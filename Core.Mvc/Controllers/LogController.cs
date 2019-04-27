using Core.Extension;
using Core.Model.Entity;
using Core.Model.PostModel;
using Core.Model.ResponseModels;
using Core.Mvc.ViewConfiguration.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Mvc.Controllers
{
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
            Task<ResponseModel> model = AsyncRequest.GetAsync<IList<Log>>(url);
            var errors = (List<Log>)model.Result.Data;
            int total = model.Result.TotalCount;
            LogIndex table = new LogIndex(HostingEnvironment, errors, total, 0, 0);

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
            Task<ResponseModel> response = AsyncRequest.PostAsync<IList<Log>, LogPostModel>(url, model);
            int count = response.Result.TotalCount;
            List<Log> logs = (List<Log>)response.Result.Data;
            LogGridConfiguration configuration = new LogGridConfiguration(logs, count, model.PageSize, model.CurrentPage);

            return this.GridConfiguration(configuration);
        }
    }
}