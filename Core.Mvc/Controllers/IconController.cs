using Core.Extension;
using Core.Model.Entity;
using Core.Model.PostModel;
using Core.Mvc.ViewConfiguration.Administration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model.ResponseModels;

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
            Task<ResponseModel> a = AsyncRequest.GetAsync<IList<Icon>>(url);
            var _icons = (List<Icon>)a.Result.Data;
            IconIndex index = new IconIndex(this.HostingEnvironment, _icons);

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
            var url = new Url(typeof(Api.Controllers.IconController), nameof(Api.Controllers.IconController.Search));
            Task<ResponseModel> response = AsyncRequest.PostAsync<IList<Icon>, IconPostModel>(url, model);
            List<Icon> icons = (List<Icon>)response.Result.Data;
            IconGridConfiguration configuration = new IconGridConfiguration(icons);

            return this.GridConfiguration(configuration);
        }
    }
}