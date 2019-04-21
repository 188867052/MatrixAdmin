using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.UserManage
{
    public class UserManage : IndexBase
    {
        public UserManage(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

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

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("用户管理");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }

        private void GenerateTable()
        {
            Column column = new Column();

        }
    }

    internal class Column
    {
        private List<string> Heads;

        public Column()
        {
                
        }



    }
}