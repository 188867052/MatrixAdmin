using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entity;
using Core.Model;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.Sidebar;
using Core.Web.ViewConfiguration;
using Core.Api.Routes;
using Core.Mvc.Areas.Redirect.Routes;
using Core.Api.Framework;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Button
{
    public class Button : SearchGridPage<object>
    {
        protected override string FileName { get; } = "buttons";

        public override string Render()
        {
            Task<HttpResponseModel> a = HttpClientAsync.Async<IList<Icon>>(IconRoute.Index);
#pragma warning disable VSTHRD002 // 避免有问题的同步等待
            List<Icon> icons = (List<Icon>)a.Result.Data;
#pragma warning restore VSTHRD002 // 避免有问题的同步等待
            string iconHtml = default;
            foreach (var icon in icons)
            {
                iconHtml += $"<li><i class=\"{icon.Code}\"></i> {icon.Code}</li>";
            }

            return base.Render().Replace("{{icon-png}}", iconHtml);
        }

        public override IList<string> CssFiles()
        {
            return new List<string>
            {
                Css.Fullcalendar,
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> JavaScriptFiles()
        {
            return new List<string>
            {
               Javascript.Matrix
            };
        }

        protected override GridConfiguration<object> GridConfiguration()
        {
            return null;
        }

        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Buttons & Icons");
            contentHeader.AddAnchor(new Anchor(RedirectRoute.Index, "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }
    }
}