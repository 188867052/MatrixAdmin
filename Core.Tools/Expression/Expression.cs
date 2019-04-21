using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Extension.Expression
{
    public static class Expression
    {
        public static string GetValue<T>(this Expression<Func<T, string>> expression, object instance)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            string propertyName = memberExpression.Member.Name;
            var property = typeof(T).GetProperties().First(l => l.Name == propertyName);
            var obj = property.GetValue(instance);
            return obj is null ? default : property.GetValue(instance).ToString();
        }

        public static bool GetValue<T>(this Expression<Func<T, bool>> expression, object instance)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            string propertyName = memberExpression.Member.Name;
            var property = typeof(T).GetProperties().First(l => l.Name == propertyName);
            return (bool)property.GetValue(instance);
        }

        public static DateTime GetValue<T>(this Expression<Func<T, DateTime>> expression, object instance)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            string propertyName = memberExpression.Member.Name;
            var property = typeof(T).GetProperties().First(o => o.Name == propertyName);
            return (DateTime)property.GetValue(instance);
        }

        public static Enum GetValue<T>(this Expression<Func<T, Enum>> expression, object instance)
        {
            string memberExpression = expression.Body.ToString();
            string propertyName = memberExpression.Split('.', ',')[1];
            var property = typeof(T).GetProperties().First(l => l.Name == propertyName);
            return (Enum)property.GetValue(instance);
        }
    }
}
