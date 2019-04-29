using System.Collections.Generic;
using Core.Extension;
using Core.Mvc.Controllers;
using Core.Mvc.ViewConfiguration.Home;
using Core.Web.JavaScript;
using Core.Web.Sidebar;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.ViewConfiguration.Form
{
    public class BasicForm : SearchGridPage
    {
        public BasicForm(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        protected override string FileName
        {
            get
            {
                return "form-common";
            }
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
               "/css/colorpicker.css" ,
               "/css/datepicker.css" ,
               "/css/uniform.css" ,
               "/css/matrix-style.css" ,
               "/css/matrix-media.css",
               "/css/bootstrap-wysihtml5.css" ,
               "/font-awesome/css/font-awesome.css" ,
            };
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>
            {
               "/js/bootstrap-colorpicker.js",
               "/js/bootstrap-datepicker.js",
               "/js/jquery.toggle.buttons.html",
               "/js/masked.js",
               "/js/jquery.uniform.js",
               "/js/matrix.js",
               "/js/matrix.form_common.js",
               "/js/wysihtml5-0.3.0.js",
               "/js/jquery.peity.min.js",
               "/js/bootstrap-wysihtml5.js",
            };
        }
        protected override string ContentHeader()
        {
            ContentHeader contentHeader = new ContentHeader("Basic Form");
            contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController),nameof(RedirectController.Index)), "Home", "Go to Home", "icon-home", "tip-bottom"));
            return contentHeader.Render();
        }

        protected override IList<IViewInstanceConstruction> CreateViewInstanceConstructions()
        {
            throw new System.NotImplementedException();
        }
    }
}