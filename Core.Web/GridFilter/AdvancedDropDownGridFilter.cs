using System;
using System.Linq.Expressions;
using Core.Extension.Expression;
using Core.Web.Identifiers;
using Core.Web.JavaScript;

namespace Core.Web.GridFilter
{
    /// <summary>
    /// 构造函数.
    /// </summary>
    /// <typeparam name="TPostModel">The post model.</typeparam>
    public class AdvancedDropDownGridFilter<TPostModel> : BaseGridFilter
    {
        private readonly string _script;
        private readonly Identifier _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDropDownGridFilter{TPostModel}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">The labelText.</param>
        /// <param name="event">The event.</param>
        public AdvancedDropDownGridFilter(Expression<Func<TPostModel, int>> expression, string labelText, JavaScriptEvent @event) : base(labelText, expression.GetPropertyName())
        {
            this._script = @event.Render();
            this._id = @event.Id;
        }

        public override string Render()
        {
            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label>{this.LabelText}</label>" +
                   $"<input class=\"form-control\" id=\"{this._id.Value}\" name=\"{this.InputName}\" list=\"browsers\">" +
                   $"</select>" +
                   $"</div>" +
                   $"</div>{this._script}" + "<datalist id=\"browsers\"></datalist>";
        }
    }
}