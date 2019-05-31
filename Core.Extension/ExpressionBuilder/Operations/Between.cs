using System;
using System.Linq.Expressions;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Operations
{
    /// <summary>
    /// Operation representing a range comparison.
    /// </summary>
    public class Between : OperationBase
    {
        public Between() : base("Between", 2, TypeGroup.Number | TypeGroup.Date)
        {
        }

        /// <inheritdoc />
        public override Expression GetExpression(MemberExpression member, ConstantExpression leftConstant, ConstantExpression rightConstant)
        {
            if (leftConstant.Value != null && rightConstant.Value == null)
            {
                return Expression.GreaterThanOrEqual(member, leftConstant);
            }
            else if (leftConstant.Value == null && rightConstant.Value != null)
            {
                return Expression.LessThanOrEqual(member, rightConstant);
            }
            else if (leftConstant.Value != null && rightConstant.Value != null)
            {
                var left = Expression.GreaterThanOrEqual(member, leftConstant);
                var right = Expression.LessThanOrEqual(member, rightConstant);
                return Expression.AndAlso(left, right);
            }
            else
            {
                throw new ArgumentException("参数错误");
            }
        }
    }
}