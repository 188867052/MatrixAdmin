using System;
using System.Linq.Expressions;
using Core.Extension;
using Core.Web.Enums;
using Core.Web.Html;
using Core.Web.Identifiers;
using Core.Web.JavaScript;

namespace Core.Web.TextBox
{
    /// <summary>
    /// 构造函数.
    /// </summary>
    /// <typeparam name="TPostModel">The post model.</typeparam>
    /// <typeparam name="TModel">TModel.</typeparam>
    public class AdvancedDropDown<TPostModel, TModel> : ITextRender<TPostModel, TModel>
    {
        private readonly Expression<Func<TPostModel, Enum>> _expression;
        private readonly string _labelText;
        private readonly string _script;
        private readonly Identifier _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDropDown{TPostModel, TModel}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">The labelText.</param>
        /// <param name="methodCall">The method call.</param>
        public AdvancedDropDown(Expression<Func<TPostModel, Enum>> expression, string labelText, MethodCall methodCall)
        {
            this._expression = expression;
            this._labelText = labelText;
            this._script = new JavaScriptEvent(func: methodCall.Method, id: methodCall.Id, eventType: JavaScriptEventEnum.MouseDown).Render();
            this._id = methodCall.Id;
        }

        public string Render(TModel model)
        {
            string listId = new Identifier().Value;
            string property = this._expression.GetPropertyName();

            return $"<div class=\"form-group\">" +
                   $"<label>{this._labelText}</label>" +
                   $"<input class=\"form-control\" id=\"{this._id.Value}\" name=\"{property}\" list=\"{listId}\">" +
                   $"</select>" +
                   $"</div>{this._script}" + $"<datalist id=\"{listId}\"></datalist>";
        }
    }
}