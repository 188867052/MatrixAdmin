﻿using Core.Mvc.Models;
using Core.Web.Dialog;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Core.Web.File;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.Controllers
{
    public class DialogController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public DialogController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            LargeDialog largeDialog = new LargeDialog();
            return Content(largeDialog.Render(), "text/html");
        }

        public IActionResult SmallDialog()
        {
            SmallDialog smallDialog = new SmallDialog();
            return Content(smallDialog.Render(), "text/html");
        }

        public IActionResult Carousel()
        {
            Carousel carousel = new Carousel();
            return Content(carousel.Render(), "text/html");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //https://localhost:44317/Dialog/FormInLine
        public IActionResult FormInLine()
        {
            File file = new File(_hostingEnvironment, "form_custom_inline");
            return Content(file.Render(), "text/html");
        }
    }
}
