using Core.Model;
using Core.Web.GridColumn;
using Core.Web.Html;
using Core.Web.Identifiers;
using System.Collections.Generic;

namespace Core.Web.ViewConfiguration
{
    public abstract class DialogConfiguration<T> : IRender
    {
        public abstract string Title { get; }
        public abstract string Footer { get; }

        public abstract string Body { get; }
        public static Identifier Identifier { get; } = new Identifier();

        /// <summary>
        /// 构造函数
        /// </summary>
        protected DialogConfiguration(ResponseModel model)
        {
            //this.GridColumn = new GridColumn<T>((List<T>)model.Data);
            //this.Count = model.TotalCount;
            //this.PageSize = model.PageSize;
            //this.CurrentPage = model.PageIndex;
        }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public GridColumn<T> GridColumn { get; }

        public virtual string Render()
        {
            string html = System.IO.File.ReadAllText("C:\\Users\\54215\\Desktop\\Study\\Asp.Net\\Core.Mvc\\wwwroot\\html\\LargeDialog.html");
            html = html.Replace("{{id}}", Identifier.Value);
            html = html.Replace("{{modal-title}}", Title);
            html = html.Replace("{{modal-body}}", Body);
            html = html.Replace("{{modal-footer}}", Footer);
            string script = $"<script>$(\"#{Identifier.Value}\").modal(\"show\");</script>";
            return html + script;
        }
    }
}
