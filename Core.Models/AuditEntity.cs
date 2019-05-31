using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model
{
    public abstract class AuditEntity
    {
        public DateTime CreatedOn { get; set; }

        public Guid? CreatedByUserGuid { get; set; }

        public string CreatedByUserName { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Guid? ModifiedByUserGuid { get; set; }

        public string ModifiedByUserName { get; set; }
    }
}
