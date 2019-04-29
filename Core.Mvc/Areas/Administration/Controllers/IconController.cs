using System.Collections.Generic;
using Core.Extension;
using Core.Model.Administration.Icon;
using Core.Mvc.Areas.Administration.ViewConfiguration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
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
            var responseModel = AsyncRequest.GetAsync<IList<Icon>>(url).Result;
            IconIndex index = new IconIndex(this.HostingEnvironment, responseModel);

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
            var response = AsyncRequest.PostAsync<IList<Icon>, IconPostModel>(url, model).Result;
            IconGridConfiguration configuration = new IconGridConfiguration(response);

            return this.GridConfiguration(configuration);
        }

        /// <summary>
        /// The add dialog.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddDialog()
        {
            AddUserDialogConfiguration user = new AddUserDialogConfiguration(null);

            return this.Dialog(user);
        }
    }
}