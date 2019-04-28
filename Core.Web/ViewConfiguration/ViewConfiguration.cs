using System;
using Core.Web.GridColumn;
using System.Collections.Generic;
using Core.Model;
using Core.Web.Html;

namespace Core.Web.ViewConfiguration
{
    public abstract class GridConfiguration<T> : IRender
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected GridConfiguration(ResponseModel model)
        {
            this.GridColumn = new GridColumn<T>((List<T>)model.Data);
            this.Count = model.TotalCount;
            this.PageSize = model.PageSize;
            this.CurrentPage = model.PageIndex;
        }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public GridColumn<T> GridColumn { get; }

        public abstract void GenerateGridColumn();

        public virtual string Render()
        {
            this.GenerateGridColumn();
            return GridColumn.Render(Count, PageSize, CurrentPage);
        }

        public string Pager()
        {
            int pageCount = (int)Math.Ceiling((decimal)Count / PageSize);
            string pageHtml = default;
            int sideCount = 3;
            for (int i = 1; i <= pageCount; i++)
            {
                string style = (CurrentPage == i ? "style=\"color:red\"" : default);
                if (pageCount <= 10)
                {
                    pageHtml += $"<li class=\"page-item\"><a {style} class=\"page-link\" href=\"#\">{i}</a></li>";
                }
                else if (pageCount > 10)
                {
                    if (i >= 1 && i <= sideCount)
                    {
                        pageHtml += $"<li class=\"page-item\"><a {style} class=\"page-link\" href=\"#\">{i}</a></li>";
                    }
                    if (i >= CurrentPage - 1 && i <= CurrentPage + 1 && i > sideCount && i <= pageCount - sideCount)
                    {
                        pageHtml += $"<li class=\"page-item\"><a {style} class=\"page-link\" href=\"#\">{i}</a></li>";
                    }
                    if (i >= pageCount - 2 && i <= pageCount && i > pageCount - sideCount)
                    {
                        pageHtml += $"<li class=\"page-item\"><a {style} class=\"page-link\" href=\"#\">{i}</a></li>";
                    }
                }
            }
            return
                   $"<ul class=\"pagination pagination-lg\">" +
                   $"<p>共Count:{Count}条,pageSize:{PageSize},CurrentPage:{CurrentPage},pageCount:{pageCount}</p>" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">&laquo;</a></li>" +
                   pageHtml +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">&raquo;</a></li>" +
                   $"</ul>";
        }
    }
}
