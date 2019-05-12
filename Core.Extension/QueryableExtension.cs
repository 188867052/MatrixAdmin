using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Core.Extension.ExpressionBuilder.Builders;
using Core.Extension.ExpressionBuilder.Generics;

namespace Core.Extension
{
    /// <summary>
    /// QueryableExtension.
    /// </summary>
    public static class QueryableExtension
    {
        private static readonly string key = "o";
        private static readonly MethodInfo StringContainsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });

       

        public static IQueryable<T> AddDateTimeLessThanOrEqualFilter<T>(this IQueryable<T> query, DateTime? value, Expression<Func<T, DateTime?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.LessThanOrEqual(a, b);
                query = query.Build(value, name, Predicate);
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
                query = query.Build(value, name, Predicate);
            }

            return query;
        }

        public static IQueryable<T> AddIntegerEqualFilter<T>(this IQueryable<T> query, int? value, Expression<Func<T, int?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                query = query.AddEqualFilter(value.Value, name);
            }

            return query;
        }

        public static IQueryable<T> AddBooleanFilter<T>(this IQueryable<T> query, bool? value, Expression<Func<T, bool?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                query = query.AddEqualFilter(value.Value, name);
            }

            return query;
        }

        public static IQueryable<T> AddFilter<T>(this IQueryable<T> query, Filter<T> filter) where T : class
        {
            var builder = new FilterBuilder();
            Expression<Func<T, bool>> expression = builder.GetExpression<T>(filter);
            query = query.Where(expression);
            return query;
        }

        public static IQueryable<T> AddStringContainsFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                string name = expression.GetPropertyName();
                MethodCallExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.Call(a, StringContainsMethod, b);
                query = query.Build(value, name, Predicate);
            }

            return query;
        }

        private static IQueryable<T> Build<T>(this IQueryable<T> query, object value, string name, Func<MemberExpression, ConstantExpression, BinaryExpression> predicate)
        {
            Expression<Func<T, bool>> Lambda(MemberExpression a, ConstantExpression b, ParameterExpression c) => Expression.Lambda<Func<T, bool>>(predicate(a, b), c);
            query = query.BuildQuery(value, name, Lambda);

            return query;
        }

        private static IQueryable<T> Build<T>(this IQueryable<T> query, object value, string name, Func<MemberExpression, ConstantExpression, MethodCallExpression> predicate)
        {
            Expression<Func<T, bool>> Lambda(MemberExpression a, ConstantExpression b, ParameterExpression c) => Expression.Lambda<Func<T, bool>>(predicate(a, b), c);
            query = query.BuildQuery(value, name, Lambda);

            return query;
        }

        private static IQueryable<T> AddEqualFilter<T>(this IQueryable<T> query, object value, string name)
        {
            BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.Equal(a, b);
            query = query.Build(value, name, Predicate);

            return query;
        }

        private static IQueryable<T> AddNotEqualFilter<T>(this IQueryable<T> query, object value, string name)
        {
            BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.NotEqual(a, b);
            query = query.Build(value, name, Predicate);

            return query;
        }

        private static IQueryable<T> AddNotNullFilter<T>(this IQueryable<T> query, string name)
        {
            BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.NotEqual(a, b);
            query = query.Build(null, name, Predicate);

            return query;
        }

        private static IQueryable<T> AddIsNullFilter<T>(this IQueryable<T> query, string name)
        {
            BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.Equal(a, b);
            query = query.Build(null, name, Predicate);

            return query;
        }

        private static IQueryable<T> BuildQuery<T>(this IQueryable<T> query, object value, string name, Func<MemberExpression, ConstantExpression, ParameterExpression, Expression<Func<T, bool>>> lambda)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), key);
            MemberExpression left = Expression.Property(parameter, typeof(T).GetProperty(name));
            ConstantExpression right = Expression.Constant(value);
            query = query.Where(lambda(left, right, parameter));

            return query;
        }
    }
}
