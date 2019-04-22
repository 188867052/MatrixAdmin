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
            string propertyName = ((MemberExpression)expression.Body).Member.Name;
            return typeof(T).GetProperties().First(o => o.Name == propertyName);
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, decimal>> expression)
        {
            string propertyName = ((MemberExpression)expression.Body).Member.Name;
            return typeof(T).GetProperties().First(o => o.Name == propertyName);
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, DateTime>> expression)
        {
            string propertyName = ((MemberExpression)expression.Body).Member.Name;
            return typeof(T).GetProperties().First(o => o.Name == propertyName);
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, bool>> expression)
        {
            string propertyName = ((MemberExpression)expression.Body).Member.Name;
            return typeof(T).GetProperties().First(o => o.Name == propertyName);
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, string>> expression)
        {
            string propertyName = ((MemberExpression)expression.Body).Member.Name;
            return typeof(T).GetProperties().First(o => o.Name == propertyName);
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, Enum>> expression)
        {
            string propertyName = expression.Body.ToString().Split('.', ',')[1];
            return typeof(T).GetProperties().First(l => l.Name == propertyName);
        }
    }
}
