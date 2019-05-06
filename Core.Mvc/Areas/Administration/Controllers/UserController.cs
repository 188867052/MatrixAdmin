using System.Collections.Generic;
using System.Text;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.ViewConfiguration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    public class UserController : StandardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public UserController(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var url = new Url(typeof(Api.Controllers.UserController), nameof(Api.Controllers.UserController.Index));
            var model = AsyncRequest.GetAsync<IList<User>>(url).Result;
            UserIndex table = new UserIndex(this.HostingEnvironment, model);

            return this.ViewConfiguration(table);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GridStateChange(UserPostModel model)
        {
            var url = new Url(typeof(Api.Controllers.UserController), nameof(Api.Controllers.UserController.Search));
            ResponseModel response = AsyncRequest.PostAsync<IList<User>, UserPostModel>(url, model).Result;
            UserViewConfiguration configuration = new UserViewConfiguration(response);

            return this.GridConfiguration(configuration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RowContextMenu(int id)
        {
            var url = new Url(typeof(Api.Controllers.UserController), nameof(Api.Controllers.UserController.FindById));
            ResponseModel model = AsyncRequest.GetAsync<User>(url.Render() + "?id=" + id).Result;
            User user = (User)model.Data;
            UserRowContextMenu menu = new UserRowContextMenu(user);
            return this.Content(menu.Render(), "text/html", Encoding.UTF8);
        }

        /// <summary>
        /// The add dialog.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddDialog()
        {
            AddUserDialogConfiguration user = new AddUserDialogConfiguration();
            return this.Dialog(user);
        }

        /// <summary>
        /// Save.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Save(UserCreatePostModel model)
        {
            var url = new Url(typeof(Api.Controllers.UserController), nameof(Api.Controllers.UserController.Create));
            var model2 = AsyncRequest.SubmitAsync(url, model).Result;

            return this.Submit(model2);
        }

        /// <summary>
        /// The edit dialog.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditDialog(int id)
        {
            var url = new Url(typeof(Api.Controllers.UserController), nameof(Api.Controllers.UserController.FindById));
            ResponseModel model = AsyncRequest.GetAsync<User>(url.Render() + "?id=" + id).Result;
            User user = (User)model.Data;
            EditUserDialogConfiguration dialog = new EditUserDialogConfiguration(user);

            return this.Dialog(dialog);
        }

        /// <summary>
        /// The edit dialog.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var url = new Url(typeof(Api.Controllers.UserController), nameof(Api.Controllers.UserController.Delete));
            ResponseModel model = AsyncRequest.DeleteAsync(url.Render() + "?ids=" + id).Result;

            return this.Submit(model);
        }


        /// <summary>
        /// The edit dialog.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Recover(int id)
        {
            var url = new Url(typeof(Api.Controllers.UserController), nameof(Api.Controllers.UserController.Recover));
            ResponseModel model = AsyncRequest.DeleteAsync(url.Render() + "?ids=" + id).Result;

            return this.Submit(model);
        }
    }
}