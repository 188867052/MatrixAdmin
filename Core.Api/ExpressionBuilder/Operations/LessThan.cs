using System.Linq.Expressions;
using Core.Api.ExpressionBuilder.Common;

namespace Core.Api.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing an "less than" comparison.
    /// </summary>
    public class LessThan : OperationBase
    {
        /// <inheritdoc />
        public LessThan()
            : base("LessThan", 1, TypeGroup.Number | TypeGroup.Date) { }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.LessThan(member, constant1);
        }
    }
}