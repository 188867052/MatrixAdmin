using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a null check.
    /// </summary>
    public class IsNull : OperationBase
    {
        /// <inheritdoc />
        public IsNull()
            : base("IsNull", 0, TypeGroup.Text | TypeGroup.Nullable, expectNullValues: true)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return System.Linq.Expressions.Expression.Equal(member, System.Linq.Expressions.Expression.Constant(null));
        }
    }
}