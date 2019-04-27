using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Extension;
using Core.Model.Entity;
using Core.Model.ResponseModels;
using Core.Mvc.Controllers;
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
            var url = new Url(typeof(Api.Controllers.RoleController), nameof(Api.Controllers.RoleController.Index));
            Task<ResponseModel> a = AsyncRequest.GetAsync<IList<Role>>(url);
            this.roles = (List<Role>)a.Result.Data;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/uniform.css",
                
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
               "/js/matrix.js",
               "/js/matrix.tables.js"
            };
        }

        public override string Render()
        {
            RoleViewConfiguration configuration=new RoleViewConfiguration(new ResponseModel());
            string table = configuration.Render();
            var html = base.Render().Replace("{{Table}}", table);

            RoleSearchGridFilterConfiguration filter = new RoleSearchGridFilterConfiguration();
            html = html.Replace("{{grid-search-filter}}", filter.GenerateSearchFilter());
            html = html.Replace("{{button-group}}", filter.GenerateButton());

            return html;
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("角色管理");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController),nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }
    }
}