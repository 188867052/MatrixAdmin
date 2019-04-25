using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Extension;
using Core.Model.Entity;
using Core.Model.PostModel;
using Core.Model.ResponseModels;
using Core.Mvc.ViewConfiguration.Error;
using Core.Mvc.ViewConfiguration.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers
{
    public class LogController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public LogController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            Task<ResponseModel> model = AsyncRequest.GetAsync<IList<Log>>("/error");
            var errors = (List<Log>)model.Result.Data;
            LogIndex table = new LogIndex(_hostingEnvironment, errors);
            return Content(table.Render(), "text/html", Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GridStateChange(LogPostModel postModel)
        {
            Task<ResponseModel> model = AsyncRequest.PostAsync<IList<Log>, LogPostModel>("/error", postModel);
            List<Log> logs = (List<Log>)model.Result.Data;
            LogIndex table = new LogIndex(_hostingEnvironment, logs);

            return Content(table.Render(), "text/html", Encoding.UTF8);
        }
    }
}