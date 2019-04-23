using System.Collections.Generic;

namespace Core.Web.ViewConfiguration
{
    public abstract class ViewConfiguration<T>
    {
        protected ViewConfiguration(IList<T> entity)
        {
            this.Entity = entity;
        }

        public IList<T> Entity { get; }


        public abstract string Render();
    }
}
