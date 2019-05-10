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
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            var left = System.Linq.Expressions.Expression.GreaterThanOrEqual(member, constant1);
            var right = System.Linq.Expressions.Expression.LessThanOrEqual(member, constant2);

            return System.Linq.Expressions.Expression.AndAlso(left, right);
        }
    }
}