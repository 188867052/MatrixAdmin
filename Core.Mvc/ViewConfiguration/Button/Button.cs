using Core.Extension;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;
using Core.Model.Administration.Icon;
using Core.Mvc.Controllers;
using Core.Mvc.ViewConfiguration.Home;

namespace Core.Mvc.ViewConfiguration.Button
{
    public class Button : SearchGridPage
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
               "/js/matrix.js",
            };
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Buttons & Icons");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }

        public override string Render()
        {
            var url = new Url(typeof(Api.Controllers.IconController), nameof(Api.Controllers.IconController.Index));
            Task<ResponseModel> a = AsyncRequest.GetAsync<IList<Icon>>(url);
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