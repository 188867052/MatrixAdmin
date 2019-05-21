using System.Collections.Generic;
using Core.Mvc.Areas.Redirect.ViewConfiguration.Home;
using Core.Web.ViewConfiguration;

namespace Core.Mvc.Areas.Redirect.ViewConfiguration.Error
{
    public class Error : SearchGridPage<object>
    {
        private readonly int _errorNumber;

        public Error(int errorNumber)
        {
            this._errorNumber = errorNumber;
        }

        protected override string FileName
        {
            get
            {
                return "Error";
            }
        }

        /// <summary>
        /// 渲染.
        /// </summary>
        /// <returns>The string.</returns>
        public override string Render()
        {
            string html = base.Render().Replace("{{number}}", this._errorNumber.ToString());
            return html;
        }

        public override IList<string> Css()
        {
            return new List<string>
            {
                "/font-awesome/css/font-awesome.css",
            };
        }

        protected override IList<string> JavaScript()
        {
            return new List<string>();
        }

        protected override GridConfiguration<object> GridConfiguration()
        {
            return null;
        }
    }
}