using System.Linq.Expressions;
using System.Reflection;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation that checks for the non-existence of a substring within another string.
    /// </summary>
    public class DoesNotContain : OperationBase
    {
        private readonly MethodInfo stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        /// <inheritdoc />
        public DoesNotContain()
            : base("DoesNotContain", 1, TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            System.Linq.Expressions.Expression constant = constant1.TrimToLower();

            return System.Linq.Expressions.Expression.Not(System.Linq.Expressions.Expression.Call(member.TrimToLower(), this.stringContainsMethod, constant))
                   .AddNullCheck(member);
        }
    }
}