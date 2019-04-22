using System.Linq.Expressions;
using Core.Api.ExpressionBuilder.Common;

namespace Core.Api.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a null check.
    /// </summary>
    public class IsNull : OperationBase
    {
        /// <inheritdoc />
        public IsNull()
            : base("IsNull", 0, TypeGroup.Text | TypeGroup.Nullable, expectNullValues: true) { }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.Equal(member, Expression.Constant(null));
        }
    }
}