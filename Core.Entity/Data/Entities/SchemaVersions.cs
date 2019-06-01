using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class SchemaVersions
    {
        public SchemaVersions()
        {
        }

        public int Id { get; set; }

        public string ScriptName { get; set; }

        public DateTime Applied { get; set; }
    }
}
