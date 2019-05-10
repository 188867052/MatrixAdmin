using System.Linq.Expressions;
using System.Reflection;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a string "EndsWith" method call.
    /// </summary>
    public class EndsWith : OperationBase
    {
        private readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        /// <inheritdoc />
        public EndsWith()
            : base("EndsWith", 1, TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            System.Linq.Expressions.Expression constant = constant1.TrimToLower();

            return System.Linq.Expressions.Expression.Call(member.TrimToLower(), this.endsWithMethod, constant)
                   .AddNullCheck(member);
        }
    }
}