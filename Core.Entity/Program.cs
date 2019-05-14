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
            query = query.Where(o => o.Id == 1);
            var bbb = query.Expression.ToString();
            //http://geekswithblogs.net/SoftwareDoneRight/archive/2014/02/01/invoking-idbsetlttgt.firstordefaultpredicate-using-reflection.aspx
            ParameterExpression parameter = Expression.Parameter(typeof(User), "o");
            MemberExpression property = Expression.Property(parameter, "Id");
            ConstantExpression rightSide = Expression.Constant(3);
            BinaryExpression operation = Expression.Equal(property, rightSide);
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(User), typeof(bool));
            LambdaExpression predicate = Expression.Lambda(delegateType, operation, parameter);


            MethodInfo mi = typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static).First(x => x.Name == "FirstOrDefault" && x.GetParameters().Length == 2);
            MethodInfo genericMethod = mi.MakeGenericMethod(typeof(User));
            MethodCallExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.Call(a, mi, b);

            query = context.User;
            Expression<Func<User, bool>> Lambda(MemberExpression a, ConstantExpression b, ParameterExpression c) => Expression.Lambda<Func<User, bool>>(Predicate(a, b), c);
            var user = query.Provider.CreateQuery<User>(Expression.Call(null, CachedReflectionInfo.FirstOrDefault_TSource_2(typeof(User)), query.Expression, predicate));

            var aaa = query.Expression.ToString();

            object retVal = genericMethod.Invoke(null, new object[] { context.User, predicate });


            var q1 = query.AddStringEqualsFilter(",,", o => o.RoleName);

            query = query.Where(o => o.UserRoleMapping.FirstOrDefault(x => x.RoleId == 1) != null);
            //List<User> aa = query.ToList();

            string[] starts = "a,b,c".Split(',');
        }
    }
}