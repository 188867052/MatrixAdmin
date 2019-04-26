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
            LogIndex table = new LogIndex(HostingEnvironment, errors);

            return this.ViewConfiguration(table);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GridStateChange(LogPostModel postModel)
        {
            var url = new Url(typeof(Api.Controllers.LogController), nameof(Api.Controllers.LogController.Search));
            Task<ResponseModel> model = AsyncRequest.PostAsync<IList<Log>, LogPostModel>(url, postModel);
            List<Log> logs = (List<Log>)model.Result.Data;
            LogViewConfiguration configuration = new LogViewConfiguration(logs);

            return this.GridConfiguration(configuration);
        }
    }
}