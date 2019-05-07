using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Extension.Expression;
using Core.Web.Identifiers;

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
        /// <param name="script">The script.</param>
        public AdvancedDropDownGridFilter(Expression<Func<TPostModel, string>> expression, string labelText, string script, Identifier id) : base(labelText, expression.GetPropertyName())
        {
            this._script = script;
            this._id = id;
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