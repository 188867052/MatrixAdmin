using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a "null or whitespace" check.
    /// </summary>
    public class IsNullOrWhiteSpace : OperationBase
    {
        /// <inheritdoc />
        public IsNullOrWhiteSpace()
            : base("IsNullOrWhiteSpace", 0, TypeGroup.Text, expectNullValues: true)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            System.Linq.Expressions.Expression exprNull = System.Linq.Expressions.Expression.Constant(null);
            System.Linq.Expressions.Expression exprEmpty = System.Linq.Expressions.Expression.Constant(string.Empty);
            return System.Linq.Expressions.Expression.OrElse(
                System.Linq.Expressions.Expression.Equal(member, exprNull),
                System.Linq.Expressions.Expression.AndAlso(
                    System.Linq.Expressions.Expression.NotEqual(member, exprNull),
                    System.Linq.Expressions.Expression.Equal(member.TrimToLower(), exprEmpty)));
        }
    }
}