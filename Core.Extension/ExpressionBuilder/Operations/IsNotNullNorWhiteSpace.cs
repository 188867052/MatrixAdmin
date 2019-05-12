using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a "not null nor whitespace" check.
    /// </summary>
    public class IsNotNullNorWhiteSpace : OperationBase
    {
        /// <inheritdoc />
        public IsNotNullNorWhiteSpace()
            : base("IsNotNullNorWhiteSpace", 0, TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression exprNull = Expression.Constant(null);
            Expression exprEmpty = Expression.Constant(string.Empty);
            return Expression.AndAlso(
                Expression.NotEqual(member, exprNull),
                Expression.NotEqual(member.TrimToLower(), exprEmpty));
        }
    }
}