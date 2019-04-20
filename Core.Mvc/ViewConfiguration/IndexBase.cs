using Core.Web;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Mvc.ViewConfigurations
{
    public abstract class IndexBase
    {
        protected readonly IHostingEnvironment _hostingEnvironment;

        public IndexBase(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Title
        /// </summary>
        /// <returns></returns>
        protected virtual string Title
        {
            get
            {
                return "Matrix Admin";
            }
        }

        /// <summary>
        /// Html
        /// </summary>
        /// <returns></returns>
        protected abstract string FileName { get; }

        /// <summary>
        /// Css文件
        /// </summary>
        /// <returns></returns>
        public abstract IList<string> Css();

        /// <summary>
        /// Javascript文件
        /// </summary>
        /// <returns></returns>
        protected abstract IList<string> Javascript();

        /// <summary>
        /// 渲染
        /// </summary>
        /// <returns></returns>
        public virtual string Render()
        {
            string sidebarMenu = File.ReadAllText(Path.Combine(this._hostingEnvironment.WebRootPath, @"html\sidebarMenu.html"));
            string htmlFormat = File.ReadAllText(Path.Combine(this._hostingEnvironment.WebRootPath, $@"html\{this.FileName}.html"));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in this.Css())
            {
                stringBuilder.Append($"<link href=\"{item}\" rel=\"stylesheet\">");
            }
            foreach (var item in this.Javascript())
            {
                stringBuilder.Append($"<script src=\"{item}\"></script>");
            }
            string head = $"<head>{stringBuilder}</head>";
            string html = htmlFormat.Replace("{{head}}", head);
            html = html.Replace("{{sidebarMenu}}", sidebarMenu);

            return html;
        }


        private string GetSidebarMenu()
        {
            Sidebar sidebar = new Sidebar();
            sidebar.AddLinkButton(new Submenu("icon icon-home", "/Redirect/index", "Dashboard"));
            sidebar.AddLinkButton(new Submenu("icon icon-signal", "/Redirect/charts", "Charts &amp; Graphs"));
            sidebar.AddLinkButton(new Submenu("icon icon-inbox", "/Redirect/widgets", "Charts &amp; Graphs"));
            sidebar.AddLinkButton(new Submenu("icon icon-th", "/Redirect/tables", "Charts &amp; Graphs"));
            sidebar.AddLinkButton(new Submenu("icon icon-fullscreen", "/Redirect/grid", "Charts &amp; Graphs"));
            sidebar.AddLinkButton(new Submenu("icon icon-th-list", "", "Charts &amp; Graphs"));
            sidebar.AddLinkButton(new Submenu("icon icon-tint", "/Redirect/buttons", "Charts &amp; Graphs"));
            sidebar.AddLinkButton(new Submenu("icon icon-pencil", "/Redirect/interface", "Charts &amp; Graphs"));
            sidebar.AddLinkButton(new Submenu("icon icon-file", "", "Charts &amp; Graphs"));
            sidebar.AddLinkButton(new Submenu("icon icon-info-sign", "", "Charts &amp; Graphs"));

            return sidebar.Render();
        }
    }
}