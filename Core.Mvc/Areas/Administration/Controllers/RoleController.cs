using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.ViewConfiguration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    public class RoleController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// 构造函数.
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
            RoleIndex table = new RoleIndex(this.HostingEnvironment, model);

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