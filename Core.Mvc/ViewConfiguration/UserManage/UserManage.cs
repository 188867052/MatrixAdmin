using Core.Models.Entities;
using Core.Models.Models.Response;
using Core.Tools;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Web.Grid;

namespace Core.Mvc.ViewConfiguration.UserManage
{
    public class UserManage : IndexBase
    {

        private readonly List<User> users;

        public UserManage(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            Task<ResponseModel> a = AsyncRequest.GetAsync<IList<User>>("/user");
            this.users = (List<User>)a.Result.Data;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/css/bootstrap.min.css",
                "/css/bootstrap-responsive.min.css",
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
                return "UserManage";
            }
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               "/js/jquery.min.js",
               "/js/jquery.ui.custom.js",
               "/js/bootstrap.min.js",
               "/js/jquery.uniform.js",
               "/js/select2.min.js",
               "/js/jquery.dataTables.min.js",
               "/js/matrix.js",
               "/js/matrix.tables.js"
            };
        }

        public override string Render()
        {
            ViewConfiguration configuration=new ViewConfiguration(this.users);
            string table = configuration.Render();
            return base.Render().Replace("{{Table}}", table);
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("用户管理");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }
    }
}