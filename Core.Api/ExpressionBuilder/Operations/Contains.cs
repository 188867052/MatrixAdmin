using System.Linq.Expressions;
using System.Reflection;
using Core.Api.ExpressionBuilder.Common;

namespace Core.Api.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a string "Contains" method call.
    /// </summary>
    public class Contains : OperationBase
    {
        private readonly MethodInfo stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        /// <summary>
        /// Initializes a new instance of the <see cref="Contains"/> class.
        /// </summary>
        public Contains()
            : base("Contains", 1, TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            Expression constant = constant1.TrimToLower();

            return Expression.Call(member.TrimToLower(), this.stringContainsMethod, constant)
                   .AddNullCheck(member);
        }
    }
}