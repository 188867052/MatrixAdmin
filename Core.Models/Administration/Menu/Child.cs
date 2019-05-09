using System.Collections.Generic;

namespace Core.Model.Administration.Menu
{
    public class Child
    {
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