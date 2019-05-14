using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Core.Extension;
using Microsoft.EntityFrameworkCore;

// Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer
namespace Core.Entity
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CoreApiContext context = new CoreApiContext();
            context.Set<UserRoleMapping>().Load();
            IQueryable<User> query = context.User;

            var user = query.Provider.Execute(Expression.Call(null, CachedReflectionInfo.FirstOrDefault_TSource_2(typeof(User)), query.Expression, GetMethodCallExpression(query)));

            query = query.Where(o => o.UserRoleMapping.FirstOrDefault(x => x.RoleId == 1) != null);
        }

        private static LambdaExpression GetMethodCallExpression(IQueryable<User> query)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(User), "o");
            MemberExpression property = Expression.Property(parameter, "Id");
            ConstantExpression rightSide = Expression.Constant(3);
            BinaryExpression operation = Expression.Equal(property, rightSide);
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(User), typeof(bool));
            LambdaExpression predicate = Expression.Lambda(delegateType, operation, parameter);
            return predicate;
        }
    }
}