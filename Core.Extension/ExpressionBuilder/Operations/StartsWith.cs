using System.Linq.Expressions;
using System.Reflection;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a string "StartsWith" method call.
    /// </summary>
    public class StartsWith : OperationBase
    {
        private readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

        /// <inheritdoc />
        public StartsWith()
            : base("StartsWith", 1, TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            System.Linq.Expressions.Expression constant = constant1.TrimToLower();

            return System.Linq.Expressions.Expression.Call(member.TrimToLower(), this.startsWithMethod, constant)
                   .AddNullCheck(member);
        }
    }
}