// using ConsoleApp.DataModels;
// using Core.Model.Administration.Permission;
// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Data.Common;

// namespace Core.Model
// {
//    /// <summary>
//    ///
//    /// </summary>
//    public class Context : CoreApiContext
//    {
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="options"></param>
//        public Context(DbContextOptions<CoreApiContext> options) : base(options)
//        {
//            this.Guid = Guid.NewGuid();
//        }

// /// <summary>
//        /// Dapper
//        /// </summary>
//        public DbConnection Dapper => this.Database.GetDbConnection();

// public Guid Guid { get; set; }

// ///// <summary>
//        ///// 用户
//        ///// </summary>
//        //public DbSet<User> User { get; set; }

// ///// <summary>
//        ///// 角色
//        ///// </summary>
//        //public DbSet<Role> Role { get; set; }

// ///// <summary>
//        ///// 菜单
//        ///// </summary>
//        //public DbSet<Menu> Menu { get; set; }

// ///// <summary>
//        ///// 图标
//        ///// </summary>
//        //public DbSet<Icon> Icon { get; set; }

// ///// <summary>
//        ///// 日志
//        ///// </summary>
//        //public DbSet<Log.Log> Log { get; set; }

// ///// <summary>
//        ///// 用户-角色多对多映射
//        ///// </summary>
//        //public DbSet<UserRoleMapping> UserRoleMapping { get; set; }

// ///// <summary>
//        ///// 权限
//        ///// </summary>
//        //public DbSet<Entity.Permission> Permission { get; set; }

// ///// <summary>
//        ///// 角色-权限多对多映射
//        ///// </summary>
//        //public DbSet<RolePermissionMapping> RolePermissionMapping { get; set; }

// /// <summary>
//        ///
//        /// </summary>
//        public DbSet<PermissionWithAssignProperty> PermissionWithAssignProperty { get; set; }

// //public virtual DbSet<UserStatus> UserStatus { get; set; }

// /// <summary>
//        ///
//        /// </summary>
//        public DbSet<PermissionWithMenu> PermissionWithMenu { get; set; }
//    }
// }
