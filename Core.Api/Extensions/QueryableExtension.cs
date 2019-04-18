using ExpressionBuilder.Builders;
using ExpressionBuilder.Generics;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Api.Extensions.Queryable
{
    /// <summary>
    /// 
    /// </summary>
    public static class QueryableExtension
    {
        private const string key = "o";

        /// <summary>
        /// IQueryable分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> Paged<T>(this IQueryable<T> query, int pageIndex = 1, int pageSize = 20)
        {
            if (pageIndex < 1)
            {
                throw new Exception("pageIndex must lager than 0");
            }
            if (pageSize < 1)
            {
                throw new Exception("pageSize must lager than 0");
            }

            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }


        public static IQueryable<T> AddBooleanFilter<T>(this IQueryable<T> query, bool? value, string name)
        {
            if (value.HasValue)
            {
                var parameter = Expression.Parameter(typeof(T), key);
                var left = Expression.Property(parameter, typeof(T).GetProperty(name));
                var right = Expression.Constant(value);
                var predicate = Expression.Equal(left, right);
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
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
                var parameter = Expression.Parameter(typeof(T), key);
                var left = Expression.Property(parameter, typeof(T).GetProperty(name));
                var right = Expression.Constant(value.Trim());
                MethodInfo method = typeof(string).GetMethod(nameof(System.String.Contains), new[] { typeof(string) });
                var predicate = Expression.Call(left, method, right);
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }
            return query;
        }

        public static IQueryable<T> AddGuidEqualsFilter<T>(this IQueryable<T> query, Guid? value, string name)
        {
            if (value.HasValue)
            {
                var parameter = Expression.Parameter(typeof(T), key);
                var left = Expression.Property(parameter, typeof(T).GetProperty(name));
                var right = Expression.Constant(value);
                var predicate = Expression.Equal(left, right);
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }

            return query;
        }
    }
}
