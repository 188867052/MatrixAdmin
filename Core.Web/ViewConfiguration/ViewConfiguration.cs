using Core.Web.GridColumn;
using System.Collections.Generic;
using Core.Web.Html;

namespace Core.Web.ViewConfiguration
{
    public abstract class ViewConfiguration<T> : IRender
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        protected ViewConfiguration(IList<T> entity)
        {
            this.GridColumn = new GridColumn<T>(entity);
        }

        public GridColumn<T> GridColumn { get; }

        public abstract void GenerateGridColumn();

        public virtual string Render()
        {
            this.GenerateGridColumn();
            return GridColumn.Render();
        }
    }
}
