using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.ViewConfiguration.User;
using Core.Mvc.Framework;
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
        public async Task<IActionResult> Index()
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Index));
            var model = await HttpClientAsync.GetAsync<IList<UserModel>>(url, this.Authentication);
            UserIndex<UserModel, UserPostModel> table = new UserIndex<UserModel, UserPostModel>(model);

            return this.SearchGridConfiguration(table);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> GridStateChange(UserPostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Search));
            ResponseModel response = await HttpClientAsync.PostAsync<IList<UserModel>, UserPostModel>(url, model);
            UserViewConfiguration<UserModel> configuration = new UserViewConfiguration<UserModel>(response);

            return this.GridConfiguration(configuration);
        }

        /// <summary>
        /// RowContextMenu.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> RowContextMenu(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.FindById));
            ResponseModel model = await HttpClientAsync.GetAsync<UserModel>(url, id);
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
        public async Task<IActionResult> SaveCreate(UserCreatePostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Create));
            var response = await HttpClientAsync.SubmitAsync(url, model);

            return this.Submit(response);
        }

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> SaveEdit(UserEditPostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Edit));
            var response = await HttpClientAsync.SubmitAsync(url, model);

            return this.Submit(response);
        }

        /// <summary>
        /// The edit dialog.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> EditDialog(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.FindById));
            ResponseModel model = await HttpClientAsync.GetAsync<UserModel>(url, id);
            EditUserDialogConfiguration<UserEditPostModel, UserModel> dialog = new EditUserDialogConfiguration<UserEditPostModel, UserModel>((UserModel)model.Data);

            return this.Dialog(dialog);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Delete));
            ResponseModel model = await HttpClientAsync.DeleteAsync(url, id);

            return this.Submit(model);
        }

        /// <summary>
        /// The recover.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Recover(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Recover));
            ResponseModel model = await HttpClientAsync.DeleteAsync(url, id);

            return this.Submit(model);
        }

        /// <summary>
        /// The forbidden.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Forbidden(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Disable));
            ResponseModel model = await HttpClientAsync.DeleteAsync(url, id);

            return this.Submit(model);
        }

        /// <summary>
        /// The normal.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Normal(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Enable));
            ResponseModel model = await HttpClientAsync.DeleteAsync(url, id);

            return this.Submit(model);
        }
    }
}