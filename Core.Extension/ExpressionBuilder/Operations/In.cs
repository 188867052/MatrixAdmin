using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a list "Contains" method call.
    /// </summary>
    public class In : OperationBase
    {
        public In() : base("In", 1, TypeGroup.Default | TypeGroup.Boolean | TypeGroup.Date | TypeGroup.Number | TypeGroup.Text, true, true)
        {
        }

        /// <inheritdoc />
        public override System.Linq.Expressions.Expression GetExpression(MemberExpression member, ConstantExpression constant1, ConstantExpression constant2)
        {
            if (!(constant1.Value is IList) || !constant1.Value.GetType().IsGenericType)
            {
                throw new ArgumentException("The 'In' operation only supports lists as parameters.");
            }

            var type = constant1.Value.GetType();
            var inInfo = type.GetMethod("Contains", new[] { type.GetGenericArguments()[0] });

            return this.GetExpressionHandlingNullables(member, constant1, type, inInfo) ?? System.Linq.Expressions.Expression.Call(constant1, inInfo, member);
        }

        private System.Linq.Expressions.Expression GetExpressionHandlingNullables(MemberExpression member, ConstantExpression constant1, Type type, MethodInfo inInfo)
        {
            var listUnderlyingType = Nullable.GetUnderlyingType(type.GetGenericArguments()[0]);
            var memberUnderlingType = Nullable.GetUnderlyingType(member.Type);
            if (listUnderlyingType != null && memberUnderlingType == null)
            {
                return System.Linq.Expressions.Expression.Call(constant1, inInfo, member.Expression);
            }

            return null;
        }
    }
}