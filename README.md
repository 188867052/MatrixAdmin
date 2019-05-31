# Asp.Net
.Net Core 2.1,MVC, WebApi,EntityFrameWork Core,bootstrap4,jquery,Restful,前后端分离，javascript原型链，Dapper，自定义UI框架,SwaggerUI,Linq表达式树，T4模板,NUnit单元测试
后端：WebApi+EfCore+Dapper兼具开发速度和性能，批处理可以使用dapper 
前端：基于bootstrap4封装的一套组件，GridSearchFilter，Dialog，RowContextMenu，
一些表单提交，做BS开发不用重复造轮子，js代码量少，统一了UI，开发人员只需要写业务

下面是对IQueryable的一些扩展
```C#
   public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var modelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            var queryModel = modelGenerator.ParseQuery(query.Expression);
            var database = (IDatabase)DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);

            string sql = modelVisitor.Queries.First().ToString();
            return sql;
        }

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
```
