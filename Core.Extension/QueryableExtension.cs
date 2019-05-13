using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Extension
{
    /// <summary>
    /// QueryableExtension.
    /// </summary>
    public static class QueryableExtension
    {
        public static IQueryable<T> AddDateTimeLessThanOrEqualFilter<T>(this IQueryable<T> query, DateTime? value, Expression<Func<T, DateTime?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.LessThanOrEqual(a, b);
                return query.CreateQuery(value, name, Predicate);
            }

            return query;
        }

        public static IQueryable<T> AddDateTimeBetweenFilter<T>(this IQueryable<T> query, DateTime? starTime, DateTime? endTime, Expression<Func<T, DateTime?>> expression)
        {
            query = query.AddDateTimeGreaterThanOrEqualFilter(starTime, expression);
            query = query.AddDateTimeLessThanOrEqualFilter(endTime, expression);
            return query;
        }

        public static IQueryable<T> AddDateTimeGreaterThanOrEqualFilter<T>(this IQueryable<T> query, DateTime? value, Expression<Func<T, DateTime?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.GreaterThanOrEqual(a, b);
                return query.CreateQuery(value, name, Predicate);
            }

            return query;
        }

        public static IQueryable<T> AddIntegerEqualFilter<T>(this IQueryable<T> query, int? value, Expression<Func<T, int?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                return query.CreateEqualFilter(value.Value, name);
            }

            return query;
        }

        public static IQueryable<T> AddBooleanFilter<T>(this IQueryable<T> query, bool? value, Expression<Func<T, bool?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                return query.CreateEqualFilter(value.Value, name);
            }

            return query;
        }

        public static IQueryable<T> AddStringContainsFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                string name = expression.GetPropertyName();
                MethodCallExpression Predicate(MemberExpression a, ConstantExpression b) => PredicateLocal<string>(a, b, nameof(string.Contains));

                return query.CreateQuery(value, name, Predicate);
            }

            return query;
        }

        public static IQueryable<T> AddStringEqualsFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                string name = expression.GetPropertyName();
                MethodCallExpression Predicate(MemberExpression a, ConstantExpression b) => PredicateLocal<string>(a, b, nameof(string.Equals));

                return query.CreateQuery(value, name, Predicate);
            }

            return query;
        }

        private static MethodCallExpression PredicateLocal<T>(MemberExpression a, ConstantExpression b, string name)
        {
            return Expression.Call(a, GetMethodInfo<T>(name), b);
        }

        private static IQueryable<T> CreateQuery<T>(this IQueryable<T> query, object value, string name, Func<MemberExpression, ConstantExpression, BinaryExpression> predicate)
        {
            Expression<Func<T, bool>> Lambda(MemberExpression a, ConstantExpression b, ParameterExpression c) => Expression.Lambda<Func<T, bool>>(predicate(a, b), c);
            return query.CreateQuery(value, name, Lambda);
        }

        private static IQueryable<T> CreateQuery<T>(this IQueryable<T> query, object value, string name, Func<MemberExpression, ConstantExpression, MethodCallExpression> predicate)
        {
            Expression<Func<T, bool>> Lambda(MemberExpression a, ConstantExpression b, ParameterExpression c) => Expression.Lambda<Func<T, bool>>(predicate(a, b), c);
            return query.CreateQuery(value, name, Lambda);
        }

        private static IQueryable<T> CreateEqualFilter<T>(this IQueryable<T> query, object value, string name)
        {
            return query.CreateQuery(value, name, Predicate);
        }

        private static BinaryExpression Predicate(MemberExpression a, ConstantExpression b)
        {
            return Expression.Equal(a, b);
        }

        private static IQueryable<T> AddNotEqualFilter<T>(this IQueryable<T> query, object value, string name)
        {
            BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.NotEqual(a, b);
            return query.CreateQuery(value, name, Predicate);
        }

        private static IQueryable<T> AddNotNullFilter<T>(this IQueryable<T> query, string name)
        {
            BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.NotEqual(a, b);
            return query.CreateQuery(null, name, Predicate);
        }

        private static IQueryable<T> AddIsNullFilter<T>(this IQueryable<T> query, string name)
        {
            BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.Equal(a, b);
            return query.CreateQuery(null, name, Predicate);
        }

        private static IQueryable<T> CreateQuery<T>(this IQueryable<T> query, object value, string name, Func<MemberExpression, ConstantExpression, ParameterExpression, Expression<Func<T, bool>>> lambda)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), CachedReflectionInfo.Key);
            MemberExpression left = Expression.Property(parameter, typeof(T).GetProperty(name));
            ConstantExpression right = Expression.Constant(value);
            return query.Provider.CreateQuery<T>(Expression.Call(null, CachedReflectionInfo.Where_TSource_2(typeof(T)), query.Expression, Expression.Quote(lambda(left, right, parameter))));
        }

        private static MethodInfo GetMethodInfo<T>(string name)
        {
            return typeof(T).GetMethod(name, new[] { typeof(T) });
        }
    }
}
