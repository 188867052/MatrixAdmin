﻿using System;
using Core.Api.Entities.Enums;

namespace Core.Api.Models.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserEditViewModel
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
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 用户描述信息
        /// </summary>
        public string Description { get; set; }
    }
}
