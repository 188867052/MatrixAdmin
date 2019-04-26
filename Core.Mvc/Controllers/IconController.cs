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

        public IActionResult Index()
        {
            IconIndex index = new IconIndex(this.HostingEnvironment);
            return this.ViewConfiguration(index);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GridStateChange(IconPostModel postModel)
        {
            var url = new Url(typeof(Api.Controllers.IconController), nameof(Api.Controllers.IconController.Search));
            Task<ResponseModel> model = AsyncRequest.PostAsync<IList<Icon>, IconPostModel>(url, postModel);
            List<Icon> icons = (List<Icon>)model.Result.Data;
            IconGridConfiguration configuration = new IconGridConfiguration(icons);

            return this.GridConfiguration(configuration);
        }
    }
}