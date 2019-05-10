using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing an equality comparison.
    /// </summary>
    public class EqualTo : OperationBase
    {
        /// <inheritdoc />
        public EqualTo()
            : base("EqualTo", 1, TypeGroup.Default | TypeGroup.Boolean | TypeGroup.Date | TypeGroup.Number | TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            System.Linq.Expressions.Expression constant = constant1;

            if (member.Type == typeof(string))
            {
                constant = constant1.TrimToLower();

                return System.Linq.Expressions.Expression.Equal(member.TrimToLower(), constant)
                       .AddNullCheck(member);
            }

            return System.Linq.Expressions.Expression.Equal(member, constant);
        }
    }
}