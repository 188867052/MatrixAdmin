using System.Collections.Generic;

namespace Core.Model
{
    /// <summary>
    /// 用于前端的路由对象.
    /// </summary>
    public class Router
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Router"/> class.
        /// </summary>
        public Router()
        {
            this.Meta = new RouterMeta();
            this.Children = new List<Router>();
        }

        public string Path { get; set; }

        public string Name { get; set; }

        public string Component { get; set; }

        public RouterMeta Meta { get; set; }

        public List<Router> Children { get; set; }
    }
}
