using System;
using System.Collections.Generic;

namespace Core.Entity
{
    public partial class Configuration
    {
        public string Value { get; set; }
        public int Id { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
    }
}
