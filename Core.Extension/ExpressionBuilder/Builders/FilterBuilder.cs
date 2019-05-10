using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Core.Extension.ExpressionBuilder.Common;
using Core.Extension.ExpressionBuilder.Exceptions;
using Core.Extension.ExpressionBuilder.Interfaces;

namespace Core.Extension.ExpressionBuilder.Builders
{
    public class FilterBuilder
    {
        public System.Linq.Expressions.Expression GetSafePropertyMember(ParameterExpression param, string memberName, System.Linq.Expressions.Expression expr)
        {
            if (!memberName.Contains("."))
            {
                return expr;
            }

            var index = memberName.LastIndexOf(".", StringComparison.InvariantCulture);
            var parentName = memberName.Substring(0, index);
            var subParam = param.GetMemberExpression(parentName);
            var resultExpr = System.Linq.Expressions.Expression.AndAlso(System.Linq.Expressions.Expression.NotEqual(subParam, System.Linq.Expressions.Expression.Constant(null)), expr);
            return this.GetSafePropertyMember(param, parentName, resultExpr);
        }

        public Expression<Func<T, bool>> GetExpression<T>(IFilter filter) where T : class
        {
            var param = System.Linq.Expressions.Expression.Parameter(typeof(T), "x");
            System.Linq.Expressions.Expression expression = null;
            var connector = Connector.And;
            foreach (var statementGroup in filter.Statements)
            {
                var statementGroupConnector = Connector.And;
                System.Linq.Expressions.Expression partialExpr = this.GetPartialExpression(param, ref statementGroupConnector, statementGroup);

                expression = expression == null ? partialExpr : this.CombineExpressions(expression, partialExpr, connector);
                connector = statementGroupConnector;
            }

            expression = expression ?? System.Linq.Expressions.Expression.Constant(true);

            return System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(expression, param);
        }

        protected System.Linq.Expressions.Expression CheckIfParentIsNull(ParameterExpression param, string memberName)
        {
            var parentMember = this.GetParentMember(param, memberName);
            return System.Linq.Expressions.Expression.Equal(parentMember, System.Linq.Expressions.Expression.Constant(null));
        }

        private System.Linq.Expressions.Expression GetPartialExpression(ParameterExpression param, ref Connector connector, IEnumerable<IFilterInfo> statementGroup)
        {
            System.Linq.Expressions.Expression expression = null;
            foreach (var statement in statementGroup)
            {
                System.Linq.Expressions.Expression expr = null;
                if (this.IsList(statement))
                {
                    expr = this.ProcessListStatement(param, statement);
                }
                else
                {
                    expr = this.GetExpression(param, statement);
                }

                expression = expression == null ? expr : this.CombineExpressions(expression, expr, connector);
                connector = statement.Connector;
            }

            return expression;
        }

        private bool IsList(IFilterInfo statement)
        {
            return statement.PropertyName.Contains("[") && statement.PropertyName.Contains("]");
        }

        private System.Linq.Expressions.Expression CombineExpressions(System.Linq.Expressions.Expression expr1, System.Linq.Expressions.Expression expr2, Connector connector)
        {
            return connector == Connector.And ? System.Linq.Expressions.Expression.AndAlso(expr1, expr2) : System.Linq.Expressions.Expression.OrElse(expr1, expr2);
        }

        private System.Linq.Expressions.Expression ProcessListStatement(ParameterExpression param, IFilterInfo statement)
        {
            var basePropertyName = statement.PropertyName.Substring(0, statement.PropertyName.IndexOf("["));
            var propertyName = statement.PropertyName.Substring(statement.PropertyName.IndexOf("[") + 1).Replace("]", string.Empty);

            var type = param.Type.GetProperty(basePropertyName).PropertyType.GetGenericArguments()[0];
            ParameterExpression listItemParam = System.Linq.Expressions.Expression.Parameter(type, "i");
            var lambda = System.Linq.Expressions.Expression.Lambda(this.GetExpression(listItemParam, statement, propertyName), listItemParam);
            var member = param.GetMemberExpression(basePropertyName);
            var enumerableType = typeof(Enumerable);
            var anyInfo = enumerableType.GetMethods(BindingFlags.Static | BindingFlags.Public).First(m => m.Name == "Any" && m.GetParameters().Count() == 2);
            anyInfo = anyInfo.MakeGenericMethod(type);
            return System.Linq.Expressions.Expression.Call(anyInfo, member, lambda);
        }

        private System.Linq.Expressions.Expression GetExpression(ParameterExpression param, IFilterInfo statement, string propertyName = null)
        {
            System.Linq.Expressions.Expression resultExpr = null;
            var memberName = propertyName ?? statement.PropertyName;
            MemberExpression member = param.GetMemberExpression(memberName);

            if (Nullable.GetUnderlyingType(member.Type) != null && statement.Value != null)
            {
                resultExpr = System.Linq.Expressions.Expression.Property(member, "HasValue");
                member = System.Linq.Expressions.Expression.Property(member, "Value");
            }

            var constant1 = System.Linq.Expressions.Expression.Constant(statement.Value);
            var constant2 = System.Linq.Expressions.Expression.Constant(statement.Value2);

            this.CheckPropertyValueMismatch(member, constant1);

            var safeStringExpression = statement.Operation.GetExpression(member, constant1, constant2);
            resultExpr = resultExpr != null ? System.Linq.Expressions.Expression.AndAlso(resultExpr, safeStringExpression) : safeStringExpression;
            resultExpr = this.GetSafePropertyMember(param, memberName, resultExpr);

            if (statement.Operation.ExpectNullValues && memberName.Contains("."))
            {
                resultExpr = System.Linq.Expressions.Expression.OrElse(this.CheckIfParentIsNull(param, memberName), resultExpr);
            }

            return resultExpr;
        }

        private void CheckPropertyValueMismatch(MemberExpression member, ConstantExpression constant1)
        {
            var memberType = member.Member.MemberType == MemberTypes.Property ? (member.Member as PropertyInfo).PropertyType : (member.Member as FieldInfo).FieldType;

            var constant1Type = this.GetConstantType(constant1);
            var nullableType = constant1Type != null ? Nullable.GetUnderlyingType(constant1Type) : null;

            var constantValueIsNotNull = constant1.Value != null;
            var memberAndConstantTypeDoNotMatch = nullableType == null && memberType != constant1Type;
            var memberAndNullableUnderlyingTypeDoNotMatch = nullableType != null && memberType != nullableType;

            if (constantValueIsNotNull && (memberAndConstantTypeDoNotMatch || memberAndNullableUnderlyingTypeDoNotMatch))
            {
                throw new PropertyValueTypeMismatchException(member.Member.Name, memberType.Name, constant1.Type.Name);
            }
        }

        private Type GetConstantType(ConstantExpression constant)
        {
            if (constant != null && constant.Value != null && constant.Value.IsGenericList())
            {
                return constant.Value.GetType().GenericTypeArguments[0];
            }

            return constant != null && constant.Value != null ? constant.Value.GetType() : null;
        }

        private MemberExpression GetParentMember(ParameterExpression param, string memberName)
        {
            var parentName = memberName.Substring(0, memberName.IndexOf("."));
            return param.GetMemberExpression(parentName);
        }
    }
}