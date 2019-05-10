using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing an "less than or equal" comparison.
    /// </summary>
    public class LessThanOrEqualTo : OperationBase
    {
        /// <inheritdoc />
        public LessThanOrEqualTo()
            : base("LessThanOrEqualTo", 1, TypeGroup.Number | TypeGroup.Date)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return System.Linq.Expressions.Expression.LessThanOrEqual(member, constant1);
        }
    }
}