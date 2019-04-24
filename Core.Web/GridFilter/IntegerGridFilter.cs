using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter
{
    public class IntegerGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly Expression<Func<TPostModel, int>> expression;
        public IntegerGridFilter(Expression<Func<TPostModel, int>> expression, string label) : base(label)
        {
            this.expression = expression;
        }


    }
}