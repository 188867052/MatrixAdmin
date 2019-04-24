using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Extension;
using Core.Model.Entity;
using Core.Model.ResponseModels;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class IconIndex : IndexBase
    {
        private readonly List<Icon> _icons;

        public IconIndex(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            Task<ResponseModel> a = AsyncRequest.GetAsync<IList<Icon>>("/Icon/Index");
            this._icons = (List<Icon>)a.Result.Data;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                
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
                return "Manage";
            }
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               
               "/js/jquery.ui.custom.js",
               
               "/js/jquery.uniform.js",
               "/js/select2.min.js",
               "/js/jquery.dataTables.min.js",
               "/js/matrix.js",
               "/js/matrix.tables.js"
            };
        }

        public override string Render()
        {
            IconViewConfiguration configuration=new IconViewConfiguration(this._icons);
            string table = configuration.Render();
            var html = base.Render().Replace("{{Table}}", table);
            html = html.Replace("{{widget-title}}", "图标管理");
            return html;
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("图标管理");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            string html = contentHeader.Render();
            return html;
        }
    }
}