using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Api.Framework;
using Core.Api.Routes;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.ViewConfiguration.Menu;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]

    public class MenuController : StandardController
    {
        /// <summary>
        /// The index page.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        public async Task<IActionResult> Index()
        {
            var response = await HttpClientAsync.Async<IList<MenuModel>>(MenuRoute.Index);
            MenuIndex<MenuModel, MenuPostModel> index = new MenuIndex<MenuModel, MenuPostModel>(response);

            return this.SearchGridConfiguration(index);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">A model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> GridStateChange(MenuPostModel model)
        {
            var response = await HttpClientAsync.Async<IList<MenuModel>>(MenuRoute.Search, model);
            MenuViewConfiguration<MenuModel> configuration = new MenuViewConfiguration<MenuModel>(response);

            return this.GridConfiguration(configuration);
        }

        /// <summary>
        /// The add dialog.
        /// </summary>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult AddDialog()
        {
            AddMenuDialogConfiguration<MenuCreatePostModel, MenuModel> dialog = new AddMenuDialogConfiguration<MenuCreatePostModel, MenuModel>();
            return this.Dialog(dialog);
        }

        /// <summary>
        /// RowContextMenu.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> RowContextMenu(int id)
        {
            HttpResponseModel model = await HttpClientAsync.Async<MenuModel>(MenuRoute.FindById, id);
            MenuModel menuModel = (MenuModel)model.Data;
            MenuRowContextMenu menu = new MenuRowContextMenu(menuModel);
            return this.Content(menu.Render(), "text/html", Encoding.UTF8);
        }

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> SaveCreate(MenuCreatePostModel model)
        {
            var response = await HttpClientAsync.Async<HttpResponseModel>(MenuRoute.Create, model);

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
            HttpResponseModel model = await HttpClientAsync.Async<MenuModel>(MenuRoute.FindById, id);
            MenuModel menuModel = (MenuModel)model.Data;
            EditMenuDialogConfiguration<MenuEditPostModel, MenuModel> dialog = new EditMenuDialogConfiguration<MenuEditPostModel, MenuModel>(menuModel);

            return this.Dialog(dialog);
        }

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> SaveEdit(MenuEditPostModel model)
        {
            var response = await HttpClientAsync.Async<HttpResponseModel>(MenuRoute.Edit, model);

            return this.Submit(response);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseModel model = await HttpClientAsync.Async<HttpResponseModel>(MenuRoute.Delete, id);

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
            HttpResponseModel model = await HttpClientAsync.Async<HttpResponseModel>(MenuRoute.Recover, id);

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
            HttpResponseModel model = await HttpClientAsync.Async<HttpResponseModel>(MenuRoute.Forbidden, id);

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
            HttpResponseModel model = await HttpClientAsync.Async<HttpResponseModel>(MenuRoute.Normal, id);

            return this.Submit(model);
        }
    }
}