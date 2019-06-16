using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Api.Routes;
using Core.Model;
using Core.Model.Administration.Role;
using Core.Mvc.Areas.Administration.ViewConfiguration.Role;
using Core.Mvc.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Areas.Administration.Controllers
{
    [Area(nameof(Administration))]
    public class RoleController : StandardController
    {
        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>A IActionResult.</returns>
        public async Task<IActionResult> Index()
        {
            var model = await HttpClientAsync.Async<IList<RoleModel>>(RoleRoute.Index);
            RoleIndex<RoleModel, RolePostModel> table = new RoleIndex<RoleModel, RolePostModel>(model);

            return this.SearchGridConfiguration(table);
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
        public async Task<IActionResult> RowContextMenu(int id)
        {
            ResponseModel model = await HttpClientAsync.Async<RoleModel>(RoleRoute.FindById, data: id);
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
        public async Task<IActionResult> GridStateChange(RolePostModel model)
        {
            var response = await HttpClientAsync.Async<IList<RoleModel>>(RoleRoute.Search, model);
            RoleViewConfiguration<RoleModel> configuration = new RoleViewConfiguration<RoleModel>(response);

            return this.GridConfiguration(configuration);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseModel model = await HttpClientAsync.ResponseAsync(RoleRoute.Delete, id);

            return this.Submit(model);
        }

        /// <summary>
        /// The edit dialog.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> EditDialog(int id)
        {
            ResponseModel model = await HttpClientAsync.Async<RoleModel>(RoleRoute.FindById, id);
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
        public async Task<IActionResult> SaveEdit(RoleEditPostModel model)
        {
            var response = await HttpClientAsync.ResponseAsync(RoleRoute.Edit, model);

            return this.Submit(response);
        }

        /// <summary>
        /// The recover.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> Recover(int id)
        {
            ResponseModel model = await HttpClientAsync.ResponseAsync(RoleRoute.Recover, id);

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
            ResponseModel model = await HttpClientAsync.ResponseAsync(RoleRoute.Forbidden, id);

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
            ResponseModel model = await HttpClientAsync.ResponseAsync(RoleRoute.Normal, id);

            return this.Submit(model);
        }

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> SaveCreate(RoleCreatePostModel model)
        {
            var response = await HttpClientAsync.ResponseAsync(RoleRoute.Create, model);

            return this.Submit(response);
        }
    }
}