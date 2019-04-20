using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration
{
    public abstract class IndexBase
    {
        protected readonly IHostingEnvironment HostingEnvironment;

        protected IndexBase(IHostingEnvironment hostingEnvironment)
        {
            this.HostingEnvironment = hostingEnvironment;
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
            string sidebarMenu = this.GenerateSidebarMenu();
            string breadCrumb = BreadCrumb();
            string htmlFormat = File.ReadAllText(Path.Combine(this.HostingEnvironment.WebRootPath, $@"html\{this.FileName}.html"));
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
            html = html.Replace("{{breadCrumb}}", breadCrumb);

            return html;
        }

        /// <summary>
        /// Generate sidebar menu.
        /// </summary>
        /// <returns></returns>
        protected string GenerateSidebarMenu()
        {
            SubMenu forms = new SubMenu("icon icon-th-list", "", "Forms", 3);
            forms.AddLinkButton(new LinkedAnchor("/Redirect/formcommon", "Basic Form"));
            forms.AddLinkButton(new LinkedAnchor("/Redirect/formvalidation", "Form with Validation"));
            forms.AddLinkButton(new LinkedAnchor("/Redirect/formwizard", "Form with Wizard"));


            SubMenu addons = new SubMenu("icon icon-file", "", "Addons", 5);
            addons.AddLinkButton(new LinkedAnchor("/Redirect/index2", "Dashboard2"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/gallery", "Gallery"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/calendar", "Calendar"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/invoice", "Invoice"));
            addons.AddLinkButton(new LinkedAnchor("/Redirect/chat", "Chat option"));

            SubMenu error = new SubMenu("icon icon-info-sign", "", "Error", 4);
            error.AddLinkButton(new LinkedAnchor("/Redirect/error403", "Error 403"));
            error.AddLinkButton(new LinkedAnchor("/Redirect/error404", "Error 404"));
            error.AddLinkButton(new LinkedAnchor("/Redirect/error405", "Error 405"));
            error.AddLinkButton(new LinkedAnchor("/Redirect/error500", "Error 500"));

            Sidebar sidebar = new Sidebar();
            sidebar.AddSubMenu(new SubMenu("icon icon-home", "/Redirect/index", "Dashboard", 0, true));
            sidebar.AddSubMenu(new SubMenu("icon icon-signal", "/Redirect/charts", "Charts &amp; Graphs"));
            sidebar.AddSubMenu(new SubMenu("icon icon-inbox", "/Redirect/widgets", "Widgets"));
            sidebar.AddSubMenu(new SubMenu("icon icon-th", "/Redirect/tables", "Tables"));
            sidebar.AddSubMenu(new SubMenu("icon icon-fullscreen", "/Redirect/grid", "Full width"));
            sidebar.AddSubMenu(forms);
            sidebar.AddSubMenu(new SubMenu("icon icon-tint", "/Redirect/buttons", "Buttons &amp; icons"));
            sidebar.AddSubMenu(new SubMenu("icon icon-pencil", "/Redirect/interface", "Elements"));
            sidebar.AddSubMenu(addons);
            sidebar.AddSubMenu(error);

            sidebar.AddSubContent(new SidebarContent("Monthly Bandwidth Transfer", 0.77, "21419.94 / 14000 MB", "progress progress-mini progress-danger active progress-striped"));
            sidebar.AddSubContent(new SidebarContent("Disk Space Usage", 0.87, "604.44 / 4000 MB", "progress progress-mini active progress-striped"));
            sidebar.AddSubContent(new SidebarContent("Disk Space Usage", 0.27, "614.44 / 4000 MB", "progress progress-mini active progress-striped"));

            return sidebar.Render();
        }


        protected virtual string BreadCrumb()
        {
            BreadCrumb breadCrumb = new BreadCrumb();
            breadCrumb.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            return breadCrumb.Render();
        }
    }
}