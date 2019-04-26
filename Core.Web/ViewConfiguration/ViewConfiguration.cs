using Core.Web.GridColumn;
using System.Collections.Generic;
using Core.Web.Html;

namespace Core.Web.ViewConfiguration
{
    public abstract class GridConfiguration<T> : IRender
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="count"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        protected GridConfiguration(IList<T> entity, int count = default, int pageSize = default, int currentPage = default)
        {
            this.GridColumn = new GridColumn<T>(entity);
            this.Count = count;
            this.PageSize = pageSize;
            this.CurrentPage = currentPage;
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
    }
}
