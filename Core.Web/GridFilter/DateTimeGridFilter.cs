using System;
using System.Linq.Expressions;
using Core.Extension.Expression;
using Core.Web.JavaScript;

namespace Core.Web.GridFilter
{
    public class DateTimeGridFilter<TPostModel> : BaseGridFilter
    {
        public DateTimeGridFilter(Expression<Func<TPostModel, DateTime>> expression, string label) : base(label, expression.GetPropertyInfo(), "form_datetime")
        {
            this.Delegate = "alert(this.value)";
            this.Event = new JavaScriptEvent(Delegate);
        }

        public JavaScriptEvent Event { get; set; }

        public string Delegate { get; set; }
    }
}