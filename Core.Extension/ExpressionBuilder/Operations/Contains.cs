using System.Linq.Expressions;
using System.Reflection;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a string "Contains" method call.
    /// </summary>
    public class Contains : OperationBase
    {
        private readonly MethodInfo stringContainsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });

        /// <summary>
        /// Initializes a new instance of the <see cref="Contains"/> class.
        /// </summary>
        public Contains()
            : base(nameof(string.Contains), 1, TypeGroup.Text)
        {
        }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            return Expression.Call(member, this.stringContainsMethod, constant1);
        }
    }
}