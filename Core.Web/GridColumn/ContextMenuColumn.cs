namespace Core.Web.GridColumn
{
    public class ContextMenuColumn<T> : BaseGridColumn<T>
    {
        private readonly string iconClass;

        public ContextMenuColumn(string iconClass, string thead) : base(thead)
        {
            this.iconClass = iconClass;
        }

        public override string RenderTd(T entity)
        {
            //var innerHtml = $"<a href=\"#\"><span class=\"{iconClass}\"></span></a>";
            string innerHtml = $"<span class=\"dropdown-toggle\" data-toggle=\"dropdown\"></span><div class=\"dropdown-menu\"></div>";
            return base.RenderTd(innerHtml);
        }
    }
}