using System;
using System.Collections.Generic;
using System.Text;
using Core.Model;
using Core.Web.GridColumn;

namespace Core.Web.ViewConfiguration
{
    public abstract class GridConfiguration<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected GridConfiguration(ResponseModel model)
        {
            this.EntityList = (List<T>)model.Data;
            this.Count = model.TotalCount;
            this.PageSize = model.PageSize;
            this.CurrentPage = model.PageIndex;
        }

        private List<T> EntityList { get; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        private int Count { get; }

        public string GenerateGridColumn()
        {
            IList<BaseGridColumn<T>> gridColumns = new List<BaseGridColumn<T>>();
            this.CreateGridColumn(gridColumns);

            StringBuilder thead = new StringBuilder();
            thead.Append("<th>序号</th>");
            foreach (var item in gridColumns)
            {
                thead.Append(item.RenderTh());
            }

            StringBuilder tbody = new StringBuilder();
            foreach (var entity in this.EntityList)
            {
                StringBuilder tr = new StringBuilder();
                int row = (this.CurrentPage - 1) * this.PageSize + this.EntityList.IndexOf(entity) + 1;
                tr.Append($"<td>{row}</td>");
                foreach (var item in gridColumns)
                {
                    tr.Append(item.RenderTd(entity));
                }

                tbody.Append($"<tr>{tr}</tr>");
            }

            return $"<table class=\"table table-bordered data-table\"><thead><tr>{thead}</tr></thead><tbody>{tbody}</tbody></table>";
        }

        public abstract void CreateGridColumn(IList<BaseGridColumn<T>> gridColumns);

        public string Pager()
        {
            int pageCount = (int)Math.Ceiling((decimal)this.Count / this.PageSize);
            string pageHtml = default;
            int sideCount = 3;
            for (int i = 1; i <= pageCount; i++)
            {
                string style = this.CurrentPage == i ? "style=\"color:red\"" : default;
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

                    if (i >= this.CurrentPage - 1 && i <= this.CurrentPage + 1 && i > sideCount && i <= pageCount - sideCount)
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
                   $"<ul class=\"pagination pagination-md\">" +
                   $"<p>共Count:{this.Count}条,pageSize:{this.PageSize},CurrentPage:{this.CurrentPage},pageCount:{pageCount}</p>" +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">&laquo;</a></li>" +
                   pageHtml +
                   $"<li class=\"page-item\"><a class=\"page-link\" href=\"#\">&raquo;</a></li>" +
                   $"</ul>";
        }
    }
}
