using System.Collections.Generic;
using System.Text;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.ViewConfiguration.Menu;
using Core.Mvc.Framework;
using Microsoft.AspNetCore.Mvc;
using ApiController = Core.Api.Controllers.MenuController;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]

    public class MenuController : StandardController
    {
        /// <summary>
        /// The index page.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        public IActionResult Index()
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Index));
            var response = HttpClientAsync.GetAsync<IList<MenuModel>>(url).Result;
            MenuIndex<MenuModel, MenuPostModel> index = new MenuIndex<MenuModel, MenuPostModel>(response);

            return this.SearchGridConfiguration(index);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">A model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public IActionResult GridStateChange(MenuPostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Search));
            var response = HttpClientAsync.PostAsync<IList<MenuModel>, MenuPostModel>(url, model).Result;
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
        public IActionResult RowContextMenu(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.FindById));
            ResponseModel model = HttpClientAsync.GetAsync<MenuModel>(url, id).Result;
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
        public IActionResult SaveCreate(MenuCreatePostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Create));
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
            ResponseModel model = HttpClientAsync.GetAsync<MenuModel>(url, id).Result;
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
        public IActionResult SaveEdit(MenuEditPostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Edit));
            var response = HttpClientAsync.SubmitAsync(url, model).Result;

            return this.Submit(response);
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
    }
}