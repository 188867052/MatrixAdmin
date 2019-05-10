using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Identifiers;

namespace Core.Web.GridFilter
{
    public class DateTimeGridFilter<TPostModel> : BaseGridFilter
    {
        public DateTimeGridFilter(Expression<Func<TPostModel, DateTime?>> expression, string label) : base(label, expression.GetPropertyName())
        {
        }

        public override string Render()
        {
            string id = new Identifier().Value;
            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label for=\"{id}\">{this.LabelText}</label>" +
                   $"<input class=\"form_datetime form-control\" name=\"{this.InputName}\" type=\"text\" id=\"{id}\">" +
                   $"</div>" +
                   $"</div>";
        }
    }
}