using Core.Web.GridColumn;
using Core.Web.GridFilter;
using System.Collections.Generic;

namespace Core.Web.ViewConfiguration
{
    public abstract class ViewConfiguration<T>
    {
        protected ViewConfiguration(IList<T> entity)
        {
            this.GridColumn = new GridColumn<T>(entity);
            this.GridFilter = new GridFilter<T>(entity);
        }

        public GridColumn<T> GridColumn { get; }
        public GridFilter<T> GridFilter { get; }

        public abstract void GenerateGridColumn();

        public virtual void GenerateGridFilter()
        {
        }

        public virtual string Render()
        {
            this.GenerateGridColumn();
            return GridColumn.Render();
        }
    }
}
