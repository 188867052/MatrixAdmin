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
    /// <typeparam name="TEnumType">The enum.</typeparam>
    public class AdvancedDropDownGridFilter<TPostModel, TEnumType> : BaseGridFilter where TEnumType : Enum
    {
        private readonly bool _isContainsEmpty;
        private readonly string _script;
        private readonly Identifier _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedDropDownGridFilter{TPostModel, TEnumType}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">The labelText.</param>
        /// <param name="isContainsEmpty">The isContainsEmpty.</param>
        /// <param name="script"></param>
        public AdvancedDropDownGridFilter(Expression<Func<TPostModel, TEnumType>> expression, string labelText, bool isContainsEmpty, string script, Identifier id) : base(labelText, expression.GetPropertyName())
        {
            this._isContainsEmpty = isContainsEmpty;
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