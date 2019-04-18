/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/

using System;
using Core.Api.Entities.Enums;

namespace Core.Api.ViewModels.Rbac.User
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
