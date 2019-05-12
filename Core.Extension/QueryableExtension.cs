using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Core.Extension.ExpressionBuilder.Builders;
using Core.Extension.ExpressionBuilder.Generics;
using Core.Model;

namespace Core.Extension
{
    /// <summary>
    /// QueryableExtension.
    /// </summary>
    public static class QueryableExtension
    {
        private static readonly string key = "o";

        /// <summary>
        /// IQueryable分页.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="query">query.</param>
        /// <param name="count">count.</param>
        /// <param name="pager">pager.</param>
        /// <returns></returns>
        public static IList<T> ToPagedList<T>(this IQueryable<T> query, out int count, Pager pager)
        {
            pager.TotalCount = query.Count();
            count = query.Count();
            return query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
        }

        public static IQueryable<T> AddDateTimeLessThanOrEqualFilter<T>(this IQueryable<T> query, DateTime? value, Expression<Func<T, DateTime?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), key);
                var left = System.Linq.Expressions.Expression.Property(parameter, typeof(T).GetProperty(name));
                var right = System.Linq.Expressions.Expression.Constant(value);
                var predicate = System.Linq.Expressions.Expression.LessThanOrEqual(left, right);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        public static IQueryable<T> AddDateTimeGreaterThanOrEqualFilter<T>(this IQueryable<T> query, DateTime? value, Expression<Func<T, DateTime?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), key);
                var left = System.Linq.Expressions.Expression.Property(parameter, typeof(T).GetProperty(name));
                var right = System.Linq.Expressions.Expression.Constant(value);
                var predicate = System.Linq.Expressions.Expression.GreaterThanOrEqual(left, right);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
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
                var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), key);
                var left = System.Linq.Expressions.Expression.Property(parameter, typeof(T).GetProperty(name));
                var right = System.Linq.Expressions.Expression.Constant(value.Trim());
                MethodInfo method = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
                var predicate = System.Linq.Expressions.Expression.Call(left, method, right);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        private static IQueryable<T> AddEqualFilter<T>(this IQueryable<T> query, object value, string name)
        {
            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), key);
            var left = System.Linq.Expressions.Expression.Property(parameter, typeof(T).GetProperty(name));
            var right = System.Linq.Expressions.Expression.Constant(value);
            var predicate = System.Linq.Expressions.Expression.Equal(left, right);
            var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(predicate, parameter);
            query = query.Where(lambda);

            return query;
        }
    }
}
