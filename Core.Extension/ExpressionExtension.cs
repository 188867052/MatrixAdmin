using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Extension
{
    public static class ExpressionExtension
    {
        public static string GetPropertyName<T>(this Expression<Func<T, int>> expression)
        {
            return ((MemberExpression)expression.Body).PropertyInfo<T>().Name;
        }

        public static PropertyInfo GetPropertyName<T>(this Expression<Func<T, decimal>> expression)
        {
            return ((MemberExpression)expression.Body).PropertyInfo<T>();
        }

        public static PropertyInfo GetPropertyName<T>(this Expression<Func<T, decimal?>> expression)
        {
            return ((MemberExpression)expression.Body).PropertyInfo<T>();
        }


        public static string GetPropertyName<T>(this Expression<Func<T, bool?>> expression)
        {
            return ((MemberExpression)expression.Body).PropertyInfo<T>().Name;
        }

        public static string GetPropertyName<T>(this Expression<Func<T, string>> expression)
        {
            return ((MemberExpression)expression.Body).PropertyInfo<T>().Name;
        }

        public static string GetPropertyName<T, TEnumType>(this Expression<Func<T, TEnumType>> expression) where TEnumType : Enum
        {
            string name;
            switch (expression.Body)
            {
                case UnaryExpression unaryExpression:
                    name = ((MemberExpression)unaryExpression.Operand).Member.Name;
                    break;
                case MemberExpression memberExpression:
                    name = memberExpression.Member.Name;
                    break;
                case ParameterExpression parameterExpression:
                    name = parameterExpression.Type.Name;
                    break;
                default:
                    throw new ArgumentException("不支持的参数");
            }

            return name;
        }

        public static string GetPropertyName<T>(this Expression<Func<T, int?>> expression)
        {
            return expression.Body.GetName<T>();
        }

        public static string GetPropertyName<T>(this Expression<Func<T, DateTime?>> expression)
        {
            return expression.Body.GetName<T>();
        }

        private static PropertyInfo PropertyInfo<T>(this MemberExpression expression)
        {
            string propertyName = expression.PropertyName();
            return typeof(T).GetProperties().First(o => o.Name == propertyName);
        }

        private static string PropertyName(this MemberExpression expr)
        {
            return expr.Member.Name;
        }

        private static string GetName<T>(this Expression expression)
        {
            string name;
            switch (expression)
            {
                case UnaryExpression unaryExpression:
                    name = unaryExpression.GetValue();
                    break;
                case MemberExpression memberExpression:
                    name = memberExpression.GetValue();
                    break;
                case ParameterExpression parameterExpression:
                    name = parameterExpression.GetValue();
                    break;
                default:
                    throw new ArgumentException("不支持的参数");
            }

            return name;
        }

        private static string GetValue(this UnaryExpression unaryExpression)
        {
            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }

        private static string GetValue(this MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        private static string GetValue(this ParameterExpression parameterExpression)
        {
            return parameterExpression.Type.Name;
        }
    }
}
