﻿using System.Collections.Generic;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Addons
{
    public class Chat : IndexBase
    {
        public Chat(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        protected override string FileName
        {
            get
            {
                return "Chat";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/bootstrap.min.css",
                "/css/bootstrap-responsive.min.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/jquery.min.js",
               "/js/bootstrap.min.js",
               "/js/jquery.ui.custom.js",
               "/js/matrix.js",
               "/js/matrix.chat.js"
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Gallery");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}