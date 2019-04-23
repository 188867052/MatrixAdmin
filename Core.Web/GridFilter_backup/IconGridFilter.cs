using System;
using System.Linq.Expressions;

namespace Core.Web.GridFilter_backup
{
    public class IconGridFilter<TModel, TPostModel> : TextGridFilter<TModel, TPostModel>
    {
        public IconGridFilter(Expression<Func<TModel, string>> expression, string thead) : base(expression, thead)
        {
        }
    }
}