using System.Collections.Generic;

namespace Core.Api.Models.Menu
{
    public class MenuJsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuJsonModel"/> class.
        /// </summary>
        public MenuJsonModel()
        {
            this.Children = new List<Child>();
        }

        public string Path { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Component { get; set; }

        public List<Child> Children { get; set; }
    }

    public class Child
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Child"/> class.
        /// </summary>
        public Child()
        {
            this.Permission = new List<string>();
        }

        public string Path { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Component { get; set; }

        public List<string> Permission { get; set; }
    }
}
