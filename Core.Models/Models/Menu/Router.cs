using System.Collections.Generic;

namespace Core.Models.Models.Menu
{
    /// <summary>
    /// 用于前端的路由对象
    /// </summary>
    public class Router
    {
        public Router()
        {
            Meta = new RouterMeta();
            Children = new List<Router>();
        }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public RouterMeta Meta { get; set; }
        public List<Router> Children { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RouterMeta
    {
        public string Title { get; set; }

        public bool HideInMenu { get; set; }

        public bool NotCache { get; set; }
    }
}
