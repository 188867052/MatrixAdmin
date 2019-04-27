using Core.Extension;
using Core.Model.Entity;
using Core.Model.ResponseModels;
using Core.Mvc.ViewConfiguration.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;
using Core.Mvc.ViewConfiguration.Administration;

namespace Core.Mvc.Controllers
{
    public class RoleController : StandardController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public RoleController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var url = new Url(typeof(Api.Controllers.RoleController), nameof(Api.Controllers.RoleController.Index));
            var model = AsyncRequest.GetAsync<IList<Role>>(url).Result;
            RoleIndex table = new RoleIndex(HostingEnvironment, model);

            return this.ViewConfiguration(table);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GridStateChange(RolePostModel model)
        {
            var url = new Url(typeof(Api.Controllers.RoleController), nameof(Api.Controllers.RoleController.Search));
            var response = AsyncRequest.PostAsync<IList<Role>, RolePostModel>(url, model).Result;
            RoleViewConfiguration configuration = new RoleViewConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}