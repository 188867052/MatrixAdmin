using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Resource.ViewConfiguration;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration
{
    public abstract class IndexBase
    {
        protected readonly IHostingEnvironment HostingEnvironment;
        protected readonly SidebarNavigation.SidebarNavigation SidebarNavigation;

        protected IndexBase(IHostingEnvironment hostingEnvironment)
        {
            this.HostingEnvironment = hostingEnvironment;
            this.SidebarNavigation=new SidebarNavigation.SidebarNavigation();
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
            string sidebarMenu = this.SidebarNavigation.Render();
            string contentHeader = this.ContentHeader();
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
            html = html.Replace("{{content-header}}", contentHeader);
            html = html.Replace("{{Footer}}", this.Footer());

            string tobHeader = File.ReadAllText(Path.Combine(this.HostingEnvironment.WebRootPath, $@"html\topHeader.html"));
            html = html.Replace("{{tobHeader}}", tobHeader);

            return html;
        }


        protected virtual string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader();
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }

        private string Footer()
        {
            return $"<div class=\"row-fluid\">" +
                   $"<div id = \"footer\" class=\"span12\"> 2019 &copy; Matrix Admin.More Templates" +
                   $"<a href=\"http://www.taobao.com/\" target=\"_blank\"> My Blog</a>" +
                   $"</div>" +
                   $"</div>";
        }
    }
}