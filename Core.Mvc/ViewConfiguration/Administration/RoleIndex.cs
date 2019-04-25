using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Extension;
using Core.Model.Entity;
using Core.Model.ResponseModels;
using Core.Mvc.ViewConfiguration.Home;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class RoleIndex : IndexBase
    {
        private readonly List<Role> roles;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public RoleIndex(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            Task<ResponseModel> a = AsyncRequest.GetAsync<IList<Role>>("/Role/Index");
            this.roles = (List<Role>)a.Result.Data;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/uniform.css",
                "/css/select2.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override string FileName
        {
            get
            {
                return "Manage";
            }
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/jquery.uniform.js",
               "/js/select2.min.js",
               "/js/matrix.js",
               "/js/matrix.tables.js"
            };
        }

        public override string Render()
        {
            RoleViewConfiguration configuration=new RoleViewConfiguration(this.roles);
            string table = configuration.Render();
            var html = base.Render().Replace("{{Table}}", table);
            html = html.Replace("{{widget-title}}", "角色管理");
            return html;
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("角色管理");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }
    }
}