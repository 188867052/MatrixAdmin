using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter_backup
{
    public class IntegerGridFilter<TModel, TPostModel> : BaseGridFilter<TModel, TPostModel>
    {
        private readonly Expression<Func<TModel, int>> expression;

        public IntegerGridFilter(Expression<Func<TModel, int>> expression, string thead) : base(thead)
        {
            this.expression = expression;
        }
    }
}