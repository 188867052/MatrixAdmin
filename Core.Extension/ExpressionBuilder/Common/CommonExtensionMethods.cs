using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Extension.ExpressionBuilder.Common
{
    public static class CommonExtensionMethods
    {
        private static readonly MethodInfo trimMethod = typeof(string).GetMethod("Trim", new Type[0]);
        private static readonly MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);

        /// <summary>
        /// Gets a member expression for an specific property.
        /// </summary>
        /// <param name="param">param.</param>
        /// <param name="propertyName">propertyName.</param>
        /// <returns></returns>
        public static MemberExpression GetMemberExpression(this ParameterExpression param, string propertyName)
        {
            return GetMemberExpression((System.Linq.Expressions.Expression)param, propertyName);
        }

        /// <summary>
        /// Applies the string Trim and ToLower methods to an ExpressionMember.
        /// </summary>
        /// <param name="member">Member to which to methods will be applied.</param>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression TrimToLower(this MemberExpression member)
        {
            var trimMemberCall = System.Linq.Expressions.Expression.Call(member, trimMethod);
            return System.Linq.Expressions.Expression.Call(trimMemberCall, toLowerMethod);
        }

        /// <summary>
        /// Applies the string Trim and ToLower methods to an ExpressionMember.
        /// </summary>
        /// <param name="constant">Constant to which to methods will be applied.</param>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression TrimToLower(this ConstantExpression constant)
        {
            var trimMemberCall = System.Linq.Expressions.Expression.Call(constant, trimMethod);
            return System.Linq.Expressions.Expression.Call(trimMemberCall, toLowerMethod);
        }

        /// <summary>
        /// Adds a "null check" to the expression (before the original one).
        /// </summary>
        /// <param name="expression">Expression to which the null check will be pre-pended.</param>
        /// <param name="member">Member that will be checked.</param>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression AddNullCheck(this System.Linq.Expressions.Expression expression, MemberExpression member)
        {
            System.Linq.Expressions.Expression memberIsNotNull = System.Linq.Expressions.Expression.NotEqual(member, System.Linq.Expressions.Expression.Constant(null));
            return System.Linq.Expressions.Expression.AndAlso(memberIsNotNull, expression);
        }

        /// <summary>
        /// Checks if an object is a generic list.
        /// </summary>
        /// <param name="o">Object to be tested.</param>
        /// <returns>TRUE if the object is a generic list.</returns>
        public static bool IsGenericList(this object o)
        {
            var oType = o.GetType();
            return oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.List<>));
        }

        private static MemberExpression GetMemberExpression(System.Linq.Expressions.Expression param, string propertyName)
        {
            if (!propertyName.Contains("."))
            {
                return System.Linq.Expressions.Expression.PropertyOrField(param, propertyName);
            }

            var index = propertyName.IndexOf(".");
            var subParam = System.Linq.Expressions.Expression.PropertyOrField(param, propertyName.Substring(0, index));
            return GetMemberExpression(subParam, propertyName.Substring(index + 1));
        }
    }
}