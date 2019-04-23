using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter_backup
{
    public class BooleanGridFilter<TModel, TPostModel> : BaseGridFilter<TModel, TPostModel>
    {
        private readonly Expression<Func<TModel, bool>> expression;
        private readonly Expression<Func<TPostModel, bool>> expression2;

        public BooleanGridFilter(Expression<Func<TModel, bool>> expression, Expression<Func<TPostModel, bool>> expression2, string labelText) : base(labelText)
        {
            this.expression = expression;
            this.expression2 = expression2;
        }
    }
}