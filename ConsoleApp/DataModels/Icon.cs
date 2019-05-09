using System;

namespace ConsoleApp.DataModels
{
    public partial class Icon
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Custom { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedByUserName { get; set; }
        public Guid CreatedByUserGuid { get; set; }
        public Guid? ModifiedByUserGuid { get; set; }
        public bool IsEnable { get; set; }
        public bool Status { get; set; }
    }
}
