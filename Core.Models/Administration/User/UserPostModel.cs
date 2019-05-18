using System;
using System.Linq;
using Core.Entity;
using Core.Entity.Enums;
using Core.Extension.ExpressionBuilder.Generics;

namespace Core.Model.Administration.User
{
    /// <summary>
    /// User post model.
    /// </summary>
    public class UserPostModel : Pager
    {
        /// <summary>
        /// 是否已被删除.
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 登录名.
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 展示名.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 状态.
        /// </summary>
        public IsForbiddenEnum? Status { get; set; }

        /// <summary>
        /// 角色.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// 开始创建时间.
        /// </summary>
        public DateTime? StartCreateTime { get; set; }

        /// <summary>
        /// 结束创建时间.
        /// </summary>
        public DateTime? EndCreateTime { get; set; }

        public IQueryable<Entity.User> GenerateQuery(IQueryable<Entity.User> query)
        {
            Filter<Entity.User> filter = new Filter<Entity.User>();
            filter.AddExistFilter(new IntegerExistsInFilter<Entity.User, UserRoleMapping>(o => o.UserRoleMapping, new IntegarEqualFilter<UserRoleMapping>(o => o.RoleId, this.RoleId)));
            filter.AddSimpleFilter(new IntegarEqualFilter<Entity.User>(UserField.Status, (int?)this.Status));
            filter.AddSimpleFilter(new DateTimeBetweenFilter<Entity.User>(UserField.CreateTime, this.StartCreateTime, this.EndCreateTime));
            filter.AddSimpleFilter(new BooleanEqualFilter<Entity.User>(UserField.IsEnable, this.IsEnable));
            filter.AddSimpleFilter(new StringContainsFilter<Entity.User>(UserField.DisplayName, this.DisplayName));
            filter.AddSimpleFilter(new StringContainsFilter<Entity.User>(UserField.LoginName, this.LoginName));

            query = query.OrderByDescending(o => o.CreateTime);
            return query.Where(filter);
        }
    }
}
