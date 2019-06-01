using System;
using System.Collections.Generic;

namespace Core.Data.Entities
{
    public partial class Icon
    {
        public Icon()
        {
        }

        public int Id { get; set; }

        public string Code { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public string Custom { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateByUserName { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateByUserName { get; set; }

        public bool IsEnable { get; set; }

        public bool Status { get; set; }

        public int CreateByUserId { get; set; }

        public int UpdateByUserId { get; set; }
    }
}
