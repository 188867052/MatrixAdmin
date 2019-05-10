using System.Linq;
using System.Linq.Expressions;

namespace Core.Extension.DataAccess
{
    /// <summary>
    /// 排序静态扩展类.
    /// </summary>
    public static class OrderByExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, false);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool descending)
        {
            return OrderingHelper(source, propertyName, descending, false);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, true);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName, bool descending)
        {
            return OrderingHelper(source, propertyName, descending, true);
        }

        private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        {
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(T), "p");
            MemberExpression property = System.Linq.Expressions.Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = System.Linq.Expressions.Expression.Lambda(property, param);

            MethodCallExpression call = System.Linq.Expressions.Expression.Call(
                typeof(System.Linq.Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                System.Linq.Expressions.Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
    }
}
