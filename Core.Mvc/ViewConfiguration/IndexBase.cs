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
            sidebarMenu = this.GetSidebarMenu();
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
            sidebar.AddLinkButton(new Submenu("icon icon-inbox", "/Redirect/widgets", "Widgets"));
            sidebar.AddLinkButton(new Submenu("icon icon-th", "/Redirect/tables", "Tables"));
            sidebar.AddLinkButton(new Submenu("icon icon-fullscreen", "/Redirect/grid", "Full width"));

            Submenu forms = new Submenu("icon icon-th-list", "", "Forms");
            forms.AddLinkButton(new LinkedAnchor("/Redirect/formcommon", "Basic Form")) ;
            forms.AddLinkButton(new LinkedAnchor("/Redirect/formvalidation", "Form with Validation"));
            forms.AddLinkButton(new LinkedAnchor("/Redirect/formwizard", "Form with Wizard"));
            sidebar.AddLinkButton(forms);

            sidebar.AddLinkButton(new Submenu("icon icon-tint", "/Redirect/buttons", "Buttons &amp; icons"));
            sidebar.AddLinkButton(new Submenu("icon icon-pencil", "/Redirect/interface", "Eelements"));

            Submenu addons = new Submenu("icon icon-file", "", "Addons");
            addons.AddLinkButton(new LinkedAnchor("/Redirect/index2", "Dashboard2"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/gallery", "Gallery"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/calendar", "Calendar"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/invoice", "Invoice"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/chat", "Chat option"));
            sidebar.AddLinkButton(addons);

            Submenu error = new Submenu("icon icon-sign", "", "Error");
            error.AddLinkButton(new LinkedAnchor("/Redirect/error403", "Error 403"));
            error.AddLinkButton(new LinkedAnchor("/Redirect/error404", "Error 404"));
            error.AddLinkButton(new LinkedAnchor("/Redirect/error405", "Error 405"));
            error.AddLinkButton(new LinkedAnchor("/Redirect/error500", "Error 500"));
            sidebar.AddLinkButton(error);

            return sidebar.Render();
        }
    }
}