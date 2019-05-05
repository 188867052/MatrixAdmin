using System.Collections.Generic;
using System.Linq;

namespace Core.Web.RowContextMenu
{
    public abstract class RowContextMenu<T>
    {
        protected readonly T model;
        private readonly IList<RowContextMenuLink> links;
        protected RowContextMenu(T model)
        {
            this.model = model;
            this.links = new List<RowContextMenuLink>();
        }

        public string Render()
        {
            CreateMenu(links);
            return links.Aggregate<RowContextMenuLink, string>(default, (current, link) => current + link.Render());
        }

        protected abstract void CreateMenu(IList<RowContextMenuLink> links);
    }
}