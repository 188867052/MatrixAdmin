using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing an "greater than" comparison.
    /// </summary>
    public class GreaterThan : OperationBase
    {
        /// <inheritdoc />
        public GreaterThan()
            : base("GreaterThan", 1, TypeGroup.Number | TypeGroup.Date)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return System.Linq.Expressions.Expression.GreaterThan(member, constant1);
        }
    }
}