using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class Configuration
    {
        public Configuration()
        {
        }

        public int Id { get; set; }

        public string Value { get; set; }

        public string Key { get; set; }

        public string Description { get; set; }
    }
}
