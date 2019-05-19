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
        private readonly MethodInfo endsWithMethod = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });

        public EndsWith()
            : base("EndsWith", 1, TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression constant = constant1.TrimToLower();

            return Expression.Call(member.TrimToLower(), this.endsWithMethod, constant)
                   .AddNullCheck(member);
        }
    }
}