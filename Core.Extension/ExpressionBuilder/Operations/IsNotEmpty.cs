using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a check for a non-empty string.
    /// </summary>
    public class IsNotEmpty : OperationBase
    {
        /// <inheritdoc />
        public IsNotEmpty()
            : base("IsNotEmpty", 0, TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return System.Linq.Expressions.Expression.NotEqual(member.TrimToLower(), System.Linq.Expressions.Expression.Constant(string.Empty))
                   .AddNullCheck(member);
        }
    }
}