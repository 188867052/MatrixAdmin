﻿using System.Collections.Generic;
using Core.Entity;
using Core.Extension;
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
            var response = HttpClientAsync.GetAsync<IList<Menu>>(url).Result;
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
            var response = HttpClientAsync.PostAsync<IList<Menu>, MenuPostModel>(url, model).Result;
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
    }
}