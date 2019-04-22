using System.Linq.Expressions;
using Core.Api.ExpressionBuilder.Common;

namespace Core.Api.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing an "greater than" comparison.
    /// </summary>
    public class GreaterThan : OperationBase
    {
        /// <inheritdoc />
        public GreaterThan()
            : base("GreaterThan", 1, TypeGroup.Number | TypeGroup.Date) { }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.GreaterThan(member, constant1);
        }
    }
}