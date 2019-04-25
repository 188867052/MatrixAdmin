using System;
using System.Linq.Expressions;
using Core.Extension.Expression;
using Core.Web.JavaScript;

namespace Core.Web.GridFilter
{
    public class DateTimeGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly Expression<Func<TPostModel, DateTime>> expression;
        public DateTimeGridFilter(Expression<Func<TPostModel, DateTime>> expression, string label) : base(label)
        {
            this.Delegate = "alert(this.value)";
            this.expression = expression;
            this.Event = new JavaScriptEvent(Delegate);
        }

        public JavaScriptEvent Event { get; set; }

        public string Delegate { get; set; }

        public override string Render()
        {
            string name = this.expression.GetPropertyInfo().Name;
            return $"<div class=\"custom-control-inline\">" +
                   $"<label>{this.LabelText}</label>" +
                  $"<input name= \"{name}\" size=\"16\" type=\"text\" value=\"2019-04-25 14:45\" readonly class=\"form_datetime\">" +
                   $"</div>";
        }
    }
}