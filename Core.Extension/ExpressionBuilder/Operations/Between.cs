using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a range comparison.
    /// </summary>
    public class Between : OperationBase
    {
        /// <inheritdoc />
        public Between()
            : base("Between", 2, TypeGroup.Number | TypeGroup.Date)
        {
        }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            var left = Expression.GreaterThanOrEqual(member, constant1);
            var right = Expression.LessThanOrEqual(member, constant2);

            return Expression.AndAlso(left, right);
        }
    }
}