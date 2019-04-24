using Core.Extension;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model.Entity;
using Core.Model.ResponseModels;

namespace Core.Mvc.ViewConfiguration.Button
{
    public class Button : IndexBase
    {
        public Button(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        protected override string FileName
        {
            get
            {
                return "buttons";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                
                "/css/bootstrap-responsive.min.css",
                "/css/fullcalendar.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> Javascript()
        {
            return new List<string>
            {
               
               "/js/bootstrap.min.js",
               "/js/jquery.ui.custom.js",
               "/js/matrix.js",
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Buttons & Icons");
            contentHeader.AddAnchor(new Anchor("/Redirect/index", "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }

        public override string Render()
        {
            Task<ResponseModel> a = AsyncRequest.GetAsync<IList<Icon>>("/Icon/Index");
            List<Icon> icons = (List<Icon>)a.Result.Data;
            string iconHtml = default;
            foreach (var icon in icons)
            {
                iconHtml += $"<li><i class=\"{icon.Code}\"></i> {icon.Code}</li>";
            }
            return base.Render().Replace("{{icon-png}}", iconHtml);
        }
    }
}