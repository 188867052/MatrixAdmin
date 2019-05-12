using System;
using Core.Entity.Enums;

namespace Core.Model.Administration.Role
{
    /// <summary>
    ///
    /// </summary>
    public class RoleModel
    {
        public RoleModel()
        {
        }

        public RoleModel(Entity.Role role)
        {
            this.Name = role.Name;
            this.Id = role.Id;
            this.IsForbidden = role.IsForbidden ? IsForbiddenEnum.Forbidden : IsForbiddenEnum.Normal;
            this.CreateTime = role.CreateTime;
            this.UpdateTime = role.UpdateTime;
            this.CreatedByUserName = role.CreatedByUserName;
            this.Description = role.Description;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IsForbiddenEnum IsForbidden { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateTime { get; set; }

        public Guid CreatedByUserGuid { get; set; }

        public string CreatedByUserName { get; set; }

        public DateTime UpdateTime { get; set; }

        public Guid? ModifiedByUserGuid { get; set; }

        public string ModifiedByUserName { get; set; }

        /// <summary>
        /// 是否是超级管理员(超级管理员拥有系统的所有权限).
        /// </summary>
        public bool IsSuperAdministrator { get; set; }
    }
}
