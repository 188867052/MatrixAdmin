using System;
using System.Collections.Generic;

namespace Core.Entity
{
    public partial class Menu
    {
        public Menu()
        {
            Permission = new HashSet<Permission>();
        }

        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Alias { get; set; }
        public string Icon { get; set; }
        public Guid? ParentGuid { get; set; }
        public string ParentName { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
        public int IsDefaultRouter { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateByUserName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string UpdateByUserName { get; set; }
        public bool IsEnable { get; set; }
        public bool Status { get; set; }
        public int CreateByUserId { get; set; }
        public int UpdateByUserId { get; set; }

        public virtual ICollection<Permission> Permission { get; set; }
    }
}
