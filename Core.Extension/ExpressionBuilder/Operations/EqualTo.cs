using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing an equality comparison.
    /// </summary>
    public class EqualTo : OperationBase
    {
        public EqualTo()
            : base("EqualTo", 1, TypeGroup.Default | TypeGroup.Boolean | TypeGroup.Date | TypeGroup.Number | TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.Equal(member, constant1);
        }
    }
}