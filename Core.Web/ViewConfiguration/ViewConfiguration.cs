using Core.Web.GridColumn;
using System.Collections.Generic;

namespace Core.Web.ViewConfiguration
{
    public abstract class ViewConfiguration<T>
    {
        protected ViewConfiguration(IList<T> entity)
        {
            this.Entity = entity;
            this.GridColumn = new GridColumn<T>(entity);
        }

        public GridColumn<T> GridColumn { get; }

        public IList<T> Entity { get; }

        public abstract void GenerateGridColumn();

        public virtual string Render()
        {
            return GridColumn.Render();
        }
    }
}
