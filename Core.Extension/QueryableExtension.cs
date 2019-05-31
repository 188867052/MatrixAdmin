using System;
using System.Collections.Generic;
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
        private static readonly MethodInfo stringContainsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
        private static readonly MethodInfo stringEqualsMethod = typeof(string).GetMethod(nameof(string.Equals), new[] { typeof(string) });
        private static readonly MethodInfo stringEndsWithMethod = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });
        private static readonly MethodInfo stringStartsWithMethod = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
        private static MethodInfo whereTSource;

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

        public static IQueryable<T> AddBooleanFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression, bool? value)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                return query.CreateEqualFilter(value, name);
            }

            return query;
        }

        public static IQueryable<T> AddIntegerInArrayFilter<T>(this IQueryable<T> query, Expression<Func<T, int>> expression, int[] value)
        {
            return query.AddInArrayFilter(expression, value);
        }

        public static IQueryable<T> AddStringInArrayFilter<T>(this IQueryable<T> query, Expression<Func<T, string>> expression, string[] value)
        {
            return query.AddInArrayFilter(expression, value);
        }

        public static IQueryable<T> AddDateTimeBetweenFilter<T>(this IQueryable<T> query, DateTime? starTime, DateTime? endTime, Expression<Func<T, DateTime?>> expression)
        {
            query = query.AddDateTimeGreaterThanOrEqualFilter(starTime, expression);
            query = query.AddDateTimeLessThanOrEqualFilter(endTime, expression);
            return query;
        }

        public static IQueryable<T> AddIntegerBetweenFilter<T>(this IQueryable<T> query, int? starTime, int? endTime, Expression<Func<T, int?>> expression)
        {
            query = query.AddIntegerGreaterThanOrEqualFilter(starTime, expression);
            query = query.AddIntegerLessThanOrEqualFilter(endTime, expression);
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

        public static IQueryable<T> AddIntegerGreaterThanOrEqualFilter<T>(this IQueryable<T> query, int? value, Expression<Func<T, int?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.GreaterThanOrEqual(a, b);
                return query.CreateQuery(value, name, Predicate);
            }

            return query;
        }

        public static IQueryable<T> AddIntegerLessThanOrEqualFilter<T>(this IQueryable<T> query, int? value, Expression<Func<T, int?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.LessThanOrEqual(a, b);
                return query.CreateQuery(value, name, Predicate);
            }

            return query;
        }

        public static IQueryable<T> AddIntegerEqualFilter<T>(this IQueryable<T> query, int? value, Expression<Func<T, int>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                return query.CreateEqualFilter(value, name);
            }

            return query;
        }

        public static IQueryable<T> AddFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression, object value)
        {
            if (value != null)
            {
                return query.Where(expression);
            }

            return query;
        }

        public static IQueryable<T> AddStringContainsFilter<T>(this IQueryable<T> query, Expression<Func<T, string>> expression, string value)
        {
            return query.AddStringFilter(value, expression, stringContainsMethod);
        }

        public static IQueryable<T> AddStringEqualFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression)
        {
            return query.AddStringFilter(value, expression, stringEqualsMethod);
        }

        public static IQueryable<T> AddStringEndsWithFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression)
        {
            return query.AddStringFilter(value, expression, stringEndsWithMethod);
        }

        public static IQueryable<T> AddStringStartsWithFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression)
        {
            return query.AddStringFilter(value, expression, stringStartsWithMethod);
        }

        public static IQueryable<T> AddStringIsNullFilter<T>(this IQueryable<T> query, Expression<Func<T, string>> expression)
        {
            return query.AddIsNullFilter(expression.GetPropertyName());
        }

        public static IQueryable<T> AddStringIsEmptyFilter<T>(this IQueryable<T> query, Expression<Func<T, string>> expression)
        {
            return query.AddIsEmptyFilter(expression.GetPropertyName());
        }

        public static IQueryable<T> AddStringNotNullFilter<T>(this IQueryable<T> query, Expression<Func<T, string>> expression)
        {
            BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.NotEqual(a, b);
            return query.CreateQuery(null, expression.GetPropertyName(), Predicate);
        }

        private static IQueryable<T> AddStringFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression, MethodInfo method)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                MethodCallExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.Call(a, method, b);
                return query.CreateQuery(value, expression.GetPropertyName(), Predicate);
            }

            return query;
        }

        private static IQueryable<T> CreateQuery<T>(this IQueryable<T> query, object value, string name, Func<MemberExpression, ConstantExpression, BinaryExpression> predicate)
        {
            Expression<Func<T, bool>> Lambda(MemberExpression a, ConstantExpression b, ParameterExpression c) => Expression.Lambda<Func<T, bool>>(predicate(a, b), c);
            return query.CreateQuery(value, name, Lambda);
        }

        private static IQueryable<T> CreateQuery<T>(this IQueryable<T> query, object value, string propertyName, Func<MemberExpression, ConstantExpression, MethodCallExpression> predicate)
        {
            Expression<Func<T, bool>> Lambda(MemberExpression a, ConstantExpression b, ParameterExpression c) => Expression.Lambda<Func<T, bool>>(predicate(a, b), c);
            return query.CreateQuery(value, propertyName, Lambda);
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

        private static IQueryable<T> AddIsNullFilter<T>(this IQueryable<T> query, string propertyName)
        {
            BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.Equal(a, b);
            return query.CreateQuery(null, propertyName, Predicate);
        }

        private static IQueryable<T> AddIsEmptyFilter<T>(this IQueryable<T> query, string propertyName)
        {
            BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.Equal(a, b);
            return query.CreateQuery(string.Empty, propertyName, Predicate);
        }

        private static IQueryable<T> CreateQuery<T>(this IQueryable<T> query, object value, string propertyName, Func<MemberExpression, ConstantExpression, ParameterExpression, Expression<Func<T, bool>>> lambda)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "o");
            MemberExpression left = Expression.Property(parameter, typeof(T).GetProperty(propertyName));
            ConstantExpression right = Expression.Constant(value);

            return query.Provider.CreateQuery<T>(Expression.Call(null, QueryableExtension.WhereTSource(typeof(T)), query.Expression, Expression.Quote(lambda(left, right, parameter))));
        }

        private static MethodInfo WhereTSource(Type source)
        {
            if (QueryableExtension.whereTSource is null)
            {
                QueryableExtension.whereTSource = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.Where).GetMethodInfo().GetGenericMethodDefinition();
            }

            return QueryableExtension.whereTSource.MakeGenericMethod(source);
        }

        private static IQueryable<TEntity> AddInArrayFilter<TEntity, TValue>(this IQueryable<TEntity> query, Expression<Func<TEntity, TValue>> expression, TValue[] value)
        {
            if (value.Length > 0)
            {
                string name = expression.Body.GetName();
                Type constructedListType = typeof(List<>).MakeGenericType(typeof(TValue[]).GetElementType());
                MethodInfo method = constructedListType.GetMethod(nameof(List<TValue>.Contains), new[] { typeof(TValue) });
                var value1 = Activator.CreateInstance(constructedListType, (object)value);
                var constant = Expression.Constant(value1);
                ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "o");
                MemberExpression member = Expression.Property(parameter, typeof(TEntity).GetProperty(name));
                Expression<Func<TEntity, bool>> Lambda(ParameterExpression c) => Expression.Lambda<Func<TEntity, bool>>(Expression.Call(constant, method, member), c);

                return query.Provider.CreateQuery<TEntity>(Expression.Call(
                    null,
                    WhereTSource(typeof(TEntity)),
                    query.Expression,
                    Expression.Quote(Lambda(parameter))));
            }

            return query;
        }
    }
}
