using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Api.Framework;
using Core.Api.Routes;
using Core.Model;
using Core.Model.Administration.User;
using Core.Mvc.Areas.Administration.ViewConfiguration.User;
using Core.Mvc.CustomException;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    //[CustomAuthorize]
    public class UserController : StandardController
    {
        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await HttpClientAsync.Async<IList<UserModel>>(UserRoute.Index, this.Authentication);
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
            HttpResponseModel response = await HttpClientAsync.Async<IList<UserModel>>(UserRoute.Search, model);
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
            HttpResponseModel model = await HttpClientAsync.Async<UserModel>(UserRoute.FindById, id);
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
            var response = await HttpClientAsync.Async<HttpResponseModel>(UserRoute.Create, model);

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
            var response = await HttpClientAsync.Async<HttpResponseModel>(UserRoute.Edit, model);

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
            HttpResponseModel model = await HttpClientAsync.Async<UserModel>(UserRoute.FindById, id);
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
            HttpResponseModel model = await HttpClientAsync.Async<HttpResponseModel>(UserRoute.Delete, id);

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
            HttpResponseModel model = await HttpClientAsync.Async<HttpResponseModel>(UserRoute.Recover, id);

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
            HttpResponseModel model = await HttpClientAsync.Async<HttpResponseModel>(UserRoute.Disable, id);

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
            HttpResponseModel model = await HttpClientAsync.Async<HttpResponseModel>(UserRoute.Enable, id);

            return this.Submit(model);
        }
    }
}