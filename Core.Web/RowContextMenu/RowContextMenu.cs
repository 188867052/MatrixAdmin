using System.Collections.Generic;
using System.Linq;

namespace Core.Web.RowContextMenu
{
    public abstract class RowContextMenu<T>
    {
        protected readonly T Model;
        private readonly IList<RowContextMenuLink> links;

        protected RowContextMenu(T model)
        {
            this.Model = model;
            this.links = new List<RowContextMenuLink>();
        }

        public string Render()
        {
            this.CreateMenu(this.links);
            return this.links.Aggregate<RowContextMenuLink, string>(default, (current, link) => current + link.Render());
        }

        protected abstract void CreateMenu(IList<RowContextMenuLink> links);
    }
}