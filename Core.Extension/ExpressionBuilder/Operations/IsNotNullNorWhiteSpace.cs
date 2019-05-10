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
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            System.Linq.Expressions.Expression exprNull = System.Linq.Expressions.Expression.Constant(null);
            System.Linq.Expressions.Expression exprEmpty = System.Linq.Expressions.Expression.Constant(string.Empty);
            return System.Linq.Expressions.Expression.AndAlso(
                System.Linq.Expressions.Expression.NotEqual(member, exprNull),
                System.Linq.Expressions.Expression.NotEqual(member.TrimToLower(), exprEmpty));
        }
    }
}