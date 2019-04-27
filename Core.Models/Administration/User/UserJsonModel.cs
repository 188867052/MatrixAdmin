using System;
using Core.Model.Enums;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserJsonModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserTypeEnum UserType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IsLockedEnum IsLocked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserStatusEnum Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IsDeletedEnum IsDeleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatedOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid CreatedByUserGuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatedByUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? ModifiedByUserGuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedByUserName { get; set; }
    }
}
