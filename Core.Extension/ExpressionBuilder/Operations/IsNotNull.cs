using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a "not-null" check.
    /// </summary>
    public class IsNotNull : OperationBase
    {
        /// <inheritdoc />
        public IsNotNull()
            : base("IsNotNull", 0, TypeGroup.Text | TypeGroup.Nullable)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return System.Linq.Expressions.Expression.NotEqual(member, System.Linq.Expressions.Expression.Constant(null));
        }
    }
}