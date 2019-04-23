namespace Core.Web.GridFilter
{
    public abstract class BaseGridFilter<T>
    {
        protected BaseGridFilter(string thead)
        {
            this.Thead = thead;
        }

        private string Thead { get; }

        public virtual string RenderTh()
        {
            return $"<th>{this.Thead}</th>"; ;
        }

        public abstract string RenderTd(T entity);

        protected virtual string RenderTd(object value)
        {
            return $"<td>{value}</td>";
        }
    }
}