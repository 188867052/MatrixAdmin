using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Entity
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CoreApiContext context = new CoreApiContext();
            var a = context.User.Where2(o => o.Id == 1).ToList();
        }
    }

    public static class aa
    {
        public static IQueryable<TSource> Where2<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Provider.CreateQuery<TSource>(Expression.Call(null, Where_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
        }

        public static MethodInfo Where_TSource_2(Type tSource)
        {
            var methodInfo = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.Where).GetMethodInfo().GetGenericMethodDefinition();
            return methodInfo.MakeGenericMethod(tSource);
        }
    }
}

// Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer