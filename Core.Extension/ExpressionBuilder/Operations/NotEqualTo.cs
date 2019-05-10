using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing an inequality comparison.
    /// </summary>
    public class NotEqualTo : OperationBase
    {
        /// <inheritdoc />
        public NotEqualTo()
            : base("NotEqualTo", 1, TypeGroup.Default | TypeGroup.Boolean | TypeGroup.Date | TypeGroup.Number | TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            System.Linq.Expressions.Expression constant = constant1;

            if (member.Type == typeof(string))
            {
                constant = constant1.TrimToLower();

                return System.Linq.Expressions.Expression.NotEqual(member.TrimToLower(), constant)
                       .AddNullCheck(member);
            }

            return System.Linq.Expressions.Expression.NotEqual(member, constant);
        }
    }
}