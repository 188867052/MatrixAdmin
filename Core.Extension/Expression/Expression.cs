using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Extension.Expression
{
    public static class Expression
    {
        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, int>> expression)
        {
            return ((MemberExpression)expression.Body).PropertyInfo<T>();
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, decimal>> expression)
        {
            return ((MemberExpression)expression.Body).PropertyInfo<T>();
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, DateTime>> expression)
        {
            return ((MemberExpression)expression.Body).PropertyInfo<T>();
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, bool>> expression)
        {
            return ((MemberExpression)expression.Body).PropertyInfo<T>();
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, string>> expression)
        {
            return ((MemberExpression)expression.Body).PropertyInfo<T>();
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, Enum>> expression)
        {
            return ((UnaryExpression)expression.Body).PropertyInfo<T>();
        }

        private static PropertyInfo PropertyInfo<T>(this MemberExpression expression)
        {
            string propertyName = expression.PropertyName();
            return typeof(T).GetProperties().First(o => o.Name == propertyName);
        }

        private static PropertyInfo PropertyInfo<T>(this UnaryExpression expression)
        {
            string propertyName = expression.PropertyName();
            return typeof(T).GetProperties().First(o => o.Name == propertyName);
        }

        private static string PropertyName(this UnaryExpression expr)
        {
            return ((MemberExpression)expr.Operand).Member.Name;
        }

        private static string PropertyName(this MemberExpression expr)
        {
            return expr.Member.Name;
        }

        private static string PropertyName(this ParameterExpression expr)
        {
            return expr.Type.Name;
        }

        public static string GetPropertyName<T>(this Expression<Func<T, Enum>> expr)
        {
            var rtn = "";
            switch (expr.Body)
            {
                case UnaryExpression expression:
                    rtn = ((MemberExpression)expression.Operand).Member.Name;
                    break;
                case MemberExpression memberExpression:
                    rtn = memberExpression.Member.Name;
                    break;
                case ParameterExpression parameterExpression:
                    rtn = parameterExpression.Type.Name;
                    break;
            }
            return rtn;
        }
    }
}
