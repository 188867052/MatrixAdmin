using System.Collections.Generic;
using System.Text;
using Core.Extension;
using Core.Model;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.ViewConfiguration.Role;
using Microsoft.AspNetCore.Mvc;
using ApiController = Core.Api.Controllers.RoleController;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    public class RoleController : StandardController
    {
        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        public IActionResult Index()
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Index));
            var model = HttpClientAsync.GetAsync<IList<RoleModel>>(url).Result;
            RoleIndex table = new RoleIndex(model);

            return this.ViewConfiguration(table);
        }

        /// <summary>
        /// The add dialog.
        /// </summary>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult AddDialog()
        {
            AddRoleDialogConfiguration<RoleCreatePostModel, RoleModel> dialog = new AddRoleDialogConfiguration<RoleCreatePostModel, RoleModel>();
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
            ResponseModel model = HttpClientAsync.GetAsync<RoleModel>(url, id).Result;
            RoleModel user = (RoleModel)model.Data;
            RoleRowContextMenu menu = new RoleRowContextMenu(user);
            return this.Content(menu.Render(), "text/html", Encoding.UTF8);
        }

        /// <summary>
        /// Grid state change.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A IActionResult.</returns>
        [HttpPost]
        public IActionResult GridStateChange(RolePostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Search));
            var response = HttpClientAsync.PostAsync<IList<RoleModel>, RolePostModel>(url, model).Result;
            RoleViewConfiguration configuration = new RoleViewConfiguration(response);

            return this.GridConfiguration(configuration);
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
        /// The edit dialog.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public IActionResult EditDialog(int id)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.FindById));
            ResponseModel model = HttpClientAsync.GetAsync<RoleModel>(url, id).Result;
            RoleModel user = (RoleModel)model.Data;
            EditRoleDialogConfiguration<RoleEditPostModel, RoleModel> dialog = new EditRoleDialogConfiguration<RoleEditPostModel, RoleModel>(user);

            return this.Dialog(dialog);
        }

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The IActionResult.</returns>
        [HttpPost]
        public IActionResult SaveEdit(RoleEditPostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Edit));
            var response = HttpClientAsync.SubmitAsync(url, model).Result;

            return this.Submit(response);
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
        /// Save.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The IActionResult.</returns>
        [HttpPost]
        public IActionResult SaveCreate(RoleCreatePostModel model)
        {
            var url = new Url(typeof(ApiController), nameof(ApiController.Create));
            var response = HttpClientAsync.SubmitAsync(url, model).Result;

            return this.Submit(response);
        }
    }
}