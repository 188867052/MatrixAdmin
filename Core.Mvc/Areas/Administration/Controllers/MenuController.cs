using System.Collections.Generic;
using System.Text;
using Core.Entity;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Menu;
using Core.Mvc.Areas.Administration.ViewConfiguration.Menu;
using Core.Mvc.Areas.Administration.ViewConfiguration.User;
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
        public IActionResult Index()
        {
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Index));
            var response = HttpClientAsync.GetAsync<IList<MenuModel>>(url).Result;
            MenuIndex index = new MenuIndex(response);

            return this.ViewConfiguration(index);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">A model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public IActionResult GridStateChange(MenuPostModel model)
        {
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Search));
            var response = HttpClientAsync.PostAsync<IList<MenuModel>, MenuPostModel>(url, model).Result;
            MenuViewConfiguration configuration = new MenuViewConfiguration(response);

            return this.GridConfiguration(configuration);
        }

        /// <summary>
        /// The add dialog.
        /// </summary>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult AddDialog()
        {
            AddMenuDialogConfiguration dialog = new AddMenuDialogConfiguration();
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
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.FindById));
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
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Create));
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
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.FindById));
            ResponseModel model = HttpClientAsync.GetAsync<MenuModel>(url, id).Result;
            MenuModel menuModel = (MenuModel)model.Data;
            EditMenuDialogConfiguration dialog = new EditMenuDialogConfiguration(menuModel);

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
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Edit));
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
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Delete));
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
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Recover));
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
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Forbidden));
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
            var url = new Url(typeof(Api.Controllers.MenuController), nameof(Api.Controllers.MenuController.Normal));
            ResponseModel model = HttpClientAsync.DeleteAsync(url, id).Result;

            return this.Submit(model);
        }
    }
}