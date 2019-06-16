using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Enums;
using Core.Web.Identifiers;
using Core.Web.JavaScript;

namespace Core.Web.GridFilter
{
    /// <summary>
    /// 构造函数.
    /// </summary>
    /// <typeparam name="T">The post model.</typeparam>
    public class AdvancedDropDownGridFilter<T> : BaseGridFilter
    {
        private readonly string _url;
        private readonly string _script;
        private readonly Identifier _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDropDownGridFilter{T}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">The labelText.</param>
        /// <param name="methodCall">The method call.</param>
        /// <param name="url">url.</param>
        public AdvancedDropDownGridFilter(Expression<Func<T, int?>> expression, string labelText, MethodCall methodCall, string url) : base(labelText, expression.GetPropertyName())
        {
            this._url = url;
            this._script = new JavaScriptEvent(func: methodCall.Method, methodCall.Id, eventType: JavaScriptEventEnum.MouseDown).Render();
            this._id = methodCall.Id;
        }

        public override string Render()
        {
            string listId = new Identifier().Value;
            return $"<div class=\"{this.ContainerClass}\">" +
                   $"<div class=\"form-group\">" +
                   $"<label>{this.LabelText}</label>" +
                   $"<input class=\"form-control\" id=\"{this._id.Value}\" name=\"{this.InputName}\" data-url=\"{this._url}\" list=\"{listId}\">" +
                   $"</select>" +
                   $"</div>" +
                   $"</div>{this._script}" + $"<datalist id=\"{listId}\"></datalist>";
        }
    }
}