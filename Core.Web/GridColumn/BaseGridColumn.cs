using Core.Shared.Utilities;

namespace Core.Web.GridColumn
{
    public abstract class BaseGridColumn<T>
    {
        protected BaseGridColumn(string thead)
        {
            Check.NotEmpty(thead, nameof(thead));

            this.Thead = thead;
        }

        private string Thead { get; }

        public virtual string RenderTh()
        {
            return $"<th>{this.Thead}</th>";
        }

        public abstract string RenderTd(T entity);

        protected virtual string RenderTd(object value)
        {
            return $"<td>{value}</td>";
        }
    }
}