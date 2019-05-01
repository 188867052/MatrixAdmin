using System.Collections.Generic;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Microsoft.AspNetCore.Hosting;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Error
{
    public class Error : SearchGridPage
    {
        private readonly int errorNumber;

        public Error(IHostingEnvironment hostingEnvironment, int errorNumber) : base(hostingEnvironment)
        {
            this.errorNumber = errorNumber;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/font-awesome/css/font-awesome.css",
                "/css/matrix-style.css",
                "/css/matrix-media.css",
            };
        }


        protected override string FileName
        {
            get
            {
                return "Error";
            }
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>();
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <returns></returns>
        public override string Render()
        {
            string html = base.Render().Replace("{{number}}", this.errorNumber.ToString());
            return html;
        }
    }
}