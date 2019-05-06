using System.Collections.Generic;
using System.Linq;

namespace Core.Web.RowContextMenu
{
    public abstract class RowContextMenu<T>
    {
        private readonly IList<RowContextMenuLink> _links;

        protected RowContextMenu(T model)
        {
            this._links = new List<RowContextMenuLink>();
            this.Model = model;
        }

        protected T Model { get; set; }

        public string Render()
        {
            this.CreateMenu(this._links);
            return this._links.Aggregate<RowContextMenuLink, string>(default, (current, link) => current + link.Render());
        }

        protected abstract void CreateMenu(IList<RowContextMenuLink> links);
    }
}