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
            if (expression.Body is MemberExpression memberExpression)
            {
                string propertyName = memberExpression.Member.Name;
                return typeof(T).GetProperties().First(o => o.Name == propertyName);
            }
            return null;
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, decimal>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                string propertyName = memberExpression.Member.Name;
                return typeof(T).GetProperties().First(o => o.Name == propertyName);
            }
            return null;
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, DateTime>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                string propertyName = memberExpression.Member.Name;
                return typeof(T).GetProperties().First(o => o.Name == propertyName);
            }
            return null;
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, bool>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                string propertyName = memberExpression.Member.Name;
                return typeof(T).GetProperties().First(o => o.Name == propertyName);
            }
            return null;
        }


        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, string>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                string propertyName = memberExpression.Member.Name;
                return typeof(T).GetProperties().First(o => o.Name == propertyName);
            }
            return null;
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, Enum>> expression)
        {
            string propertyName = expression.Body.ToString().Split('.', ',')[1];
            return typeof(T).GetProperties().First(l => l.Name == propertyName);
        }
    }
}
