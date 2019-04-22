namespace Core.Web.Grid
{
    public abstract class BaseGridColumn<T>
    {
        protected BaseGridColumn(string thead)
        {
            this.Thead = thead;
        }

        private string Thead { get; }

        public virtual string RenderTh()
        {
            return $"<th>{this.Thead}</th>"; ;
        }

        public abstract string RenderTd(T entity);
    }
}