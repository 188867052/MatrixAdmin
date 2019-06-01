using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class MultiplePrimaryKeyTable
    {
        public MultiplePrimaryKeyTable()
        {
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
