using Core.Extension;
using Core.Model.Entity;
using Core.Model.PostModel;
using Core.Mvc.ViewConfiguration.Administration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Core.Mvc.Controllers
{
    public class IconController : StandardController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public IconController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        /// <summary>
        /// The index page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var url = new Url(typeof(Api.Controllers.IconController), nameof(Api.Controllers.IconController.Index));
            var  a = AsyncRequest.GetAsync<IList<Icon>>(url).Result;
            IconIndex index = new IconIndex(this.HostingEnvironment, a);

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
            var url = new Url(typeof(Api.Controllers.IconController), nameof(Api.Controllers.IconController.Search));
            var response = AsyncRequest.PostAsync<IList<Icon>, IconPostModel>(url, model).Result;
            IconGridConfiguration configuration = new IconGridConfiguration(response);

            return this.GridConfiguration(configuration);
        }
    }
}