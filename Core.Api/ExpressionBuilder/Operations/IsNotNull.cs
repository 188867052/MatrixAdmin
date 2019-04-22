using System.Linq.Expressions;
using Core.Api.ExpressionBuilder.Common;

namespace Core.Api.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a "not-null" check.
    /// </summary>
    public class IsNotNull : OperationBase
    {
        /// <inheritdoc />
        public IsNotNull()
            : base("IsNotNull", 0, TypeGroup.Text | TypeGroup.Nullable) { }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.NotEqual(member, Expression.Constant(null));
        }
    }
}