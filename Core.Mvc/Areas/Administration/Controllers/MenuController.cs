using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.ViewConfiguration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]

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
        public IActionResult GridStateChange(MenuPostModel model)
        {
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Search));
            var response = AsyncRequest.PostAsync<IList<Menu>, MenuPostModel>(url, model).Result;
            MenuViewConfiguration configuration = new MenuViewConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}