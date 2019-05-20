using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.ViewConfiguration.User;
using Microsoft.AspNetCore.Mvc;
using ApiController = Core.Api.Controllers.UserController;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    public class UserController : StandardController
    {
        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Index));
            var model = HttpClientAsync.GetAsync<IList<UserModel>>(url).Result;
            UserIndex table = new UserIndex(this.HostingEnvironment, model);

            return this.ViewConfiguration(table);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public IActionResult GridStateChange(UserPostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Search));
            ResponseModel response = HttpClientAsync.PostAsync<IList<UserModel>, UserPostModel>(url, model).Result;
            UserViewConfiguration configuration = new UserViewConfiguration(response);

            return this.GridConfiguration(configuration);
        }

        /// <summary>
        /// RowContextMenu.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult RowContextMenu(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.FindById));
            ResponseModel model = HttpClientAsync.GetAsync<UserModel>(url, id).Result;
            UserModel user = (UserModel)model.Data;
            UserRowContextMenu menu = new UserRowContextMenu(user);
            return this.Content(menu.Render(), "text/html", Encoding.UTF8);
        }

        /// <summary>
        /// The add dialog.
        /// </summary>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult AddDialog()
        {
            AddUserDialogConfiguration<UserCreatePostModel, UserModel> dialog = new AddUserDialogConfiguration<UserCreatePostModel, UserModel>();
            return this.Dialog(dialog);
        }

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The IActionResult.</returns>
        [HttpPost]
        public IActionResult SaveCreate(UserCreatePostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Create));
            var response = HttpClientAsync.SubmitAsync(url, model).Result;

            return this.Submit(response);
        }

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The IActionResult.</returns>
        [HttpPost]
        public IActionResult SaveEdit(UserEditPostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Edit));
            var response = HttpClientAsync.SubmitAsync(url, model).Result;

            return this.Submit(response);
        }

        /// <summary>
        /// The edit dialog.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult EditDialog(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.FindById));
            ResponseModel model = HttpClientAsync.GetAsync<UserModel>(url, id).Result;
            EditUserDialogConfiguration<UserEditPostModel, UserModel> dialog = new EditUserDialogConfiguration<UserEditPostModel, UserModel>((UserModel)model.Data);

            return this.Dialog(dialog);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Delete));
            ResponseModel model = HttpClientAsync.DeleteAsync(url, id).Result;

            return this.Submit(model);
        }

        /// <summary>
        /// The recover.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult Recover(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Recover));
            ResponseModel model = HttpClientAsync.DeleteAsync(url, id).Result;

            return this.Submit(model);
        }

        /// <summary>
        /// The forbidden.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult Forbidden(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Forbidden));
            ResponseModel model = HttpClientAsync.DeleteAsync(url, id).Result;

            return this.Submit(model);
        }

        /// <summary>
        /// The normal.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult Normal(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Normal));
            ResponseModel model = HttpClientAsync.DeleteAsync(url, id).Result;

            return this.Submit(model);
        }

        /// <summary>
        /// Gets role data list.
        /// </summary>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult GetRoleDataList()
        {
            var url = new Url(typeof(Api.Controllers.RoleController), nameof(Api.Controllers.RoleController.GetRoleDataList));
            ResponseModel model = HttpClientAsync.GetAsync<IList<Role>>(url).Result;
            IList<Role> roles = (IList<Role>)model.Data;
            string options = roles.Aggregate(string.Empty, (current, role) => current + $"<option key=\"{role.Id}\" value=\"{role.Name}\"></option>");

            return this.Content(options, "text/html", Encoding.UTF8);
        }
    }
}