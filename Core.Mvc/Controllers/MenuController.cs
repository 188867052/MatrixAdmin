using Core.Extension;
using Core.Model.Entity;
using Core.Model.PostModel;
using Core.Mvc.ViewConfiguration.Administration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Mvc.ViewConfiguration.Log;

namespace Core.Mvc.Controllers
{
    public class MenuController : StandardController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public MenuController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        /// <summary>
        /// The index page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Index));
            var  a = AsyncRequest.GetAsync<IList<Menu>>(url).Result;
            MenuIndex index = new MenuIndex(this.HostingEnvironment, a);

            return this.ViewConfiguration(index);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GridStateChange(IconPostModel model)
        {
            if (model.CurrentPage == 0)
            {
                model.CurrentPage = 1;
            }
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Search));
            var response = AsyncRequest.PostAsync<IList<Menu>, IconPostModel>(url, model).Result;
            LogGridConfiguration configuration = new LogGridConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}