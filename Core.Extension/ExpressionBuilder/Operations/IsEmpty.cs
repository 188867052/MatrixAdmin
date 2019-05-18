using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a check for an empty string.
    /// </summary>
    public class IsEmpty : OperationBase
    {
        /// <inheritdoc />
        public IsEmpty()
            : base("IsEmpty", 0, TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.Equal(member, Expression.Constant(string.Empty));
        }
    }
}