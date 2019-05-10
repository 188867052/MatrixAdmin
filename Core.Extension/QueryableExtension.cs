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
        private const string Key = "o";

        /// <summary>
        /// IQueryable分页.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="query">query.</param>
        /// <param name="count">count.</param>
        /// <param name="pager">pager.</param>
        /// <returns></returns>
        public static IList<T> Paged<T>(this IQueryable<T> query, out int count, Pager pager = default)
        {
            if (pager == default)
            {
                pager = new Pager { PageSize = 10, PageIndex = 1 };
            }

            if (pager.PageIndex < 1)
            {
                throw new Exception("pageIndex must lager than 0");
            }

            if (pager.PageSize < 1)
            {
                throw new Exception("pageSize must lager than 0");
            }

            pager.TotalCount = query.Count();

            count = query.Count();
            return query.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
        }

        public static IQueryable<T> AddBooleanFilter<T>(this IQueryable<T> query, bool? value, string name)
        {
            if (value.HasValue)
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), Key);
                var left = System.Linq.Expressions.Expression.Property(parameter, typeof(T).GetProperty(name));
                var right = System.Linq.Expressions.Expression.Constant(value);
                var predicate = System.Linq.Expressions.Expression.Equal(left, right);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
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

        public static IQueryable<T> AddStringContainsFilter<T>(this IQueryable<T> query, string value, string name)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), Key);
                var left = System.Linq.Expressions.Expression.Property(parameter, typeof(T).GetProperty(name));
                var right = System.Linq.Expressions.Expression.Constant(value.Trim());
                MethodInfo method = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
                var predicate = System.Linq.Expressions.Expression.Call(left, method, right);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        public static IQueryable<T> AddGuidEqualsFilter<T>(this IQueryable<T> query, Guid? guid, string name)
        {
            if (guid.HasValue)
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), Key);
                var left = System.Linq.Expressions.Expression.Property(parameter, typeof(T).GetProperty(name));
                var right = System.Linq.Expressions.Expression.Constant(guid.Value);
                var predicate = System.Linq.Expressions.Expression.Equal(left, right);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }

            return query;
        }
    }
}
