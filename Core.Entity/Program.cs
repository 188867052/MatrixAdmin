using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.Entity
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CoreApiContext coreApiContext = new CoreApiContext();

            // coreApiContext.User.Include(o => o.UserStatus);
            var query = coreApiContext.User;
            query.AddIntegerEqualFilte2r(1, o => o.Id);
            var a = query.ToList();
            // coreApiContext.Set<User>().Include(o => o.UserStatus);
            //coreApiContext.Set<UserStatus>().Load();
            //// query = query.Where(o => o.Id == 1);
            //// coreApiContext.User.Include(o => o.UserRoleMapping);
            //coreApiContext.Set<UserRoleMapping>().Load();
            //User user = coreApiContext.User.Find(1);
            //var userRoleMapping = user.UserRoleMapping.FirstOrDefault(o => o.UserId == 1);

            //// var list = query.ToList();
            //userRoleMapping.RoleId = 2;
            //userRoleMapping.CreateTime = DateTime.Now;
            //coreApiContext.SaveChanges();
        }
    }

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
            ParameterExpression param = Expression.Parameter(typeof(T), "p");
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));
            source = (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
    }


    public static class AA
    {
        public static IQueryable<T> AddIntegerEqualFilte2r<T>(this IQueryable<T> query, int? value, Expression<Func<T, int?>> expression)
        {
            if (value.HasValue)
            {
                string name = expression.GetPropertyName();
                var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), "o");
                var left = System.Linq.Expressions.Expression.Property(parameter, typeof(T).GetProperty(name));
                var right = System.Linq.Expressions.Expression.Constant(value);
                var predicate = System.Linq.Expressions.Expression.Equal(left, right);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        public static string GetPropertyName<T>(this Expression<Func<T, int?>> expression)
        {
            string name;
            switch (expression.Body)
            {
                case UnaryExpression unaryExpression:
                    name = ((MemberExpression)unaryExpression.Operand).Member.Name;
                    break;
                case MemberExpression memberExpression:
                    name = memberExpression.Member.Name;
                    break;
                case ParameterExpression parameterExpression:
                    name = parameterExpression.Type.Name;
                    break;
                default:
                    throw new ArgumentException("不支持的参数");
            }

            return name;
        }
    }
}

// Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer