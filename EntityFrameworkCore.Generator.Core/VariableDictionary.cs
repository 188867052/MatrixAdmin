using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EntityFrameworkCore.Generator
{
    /// <summary>
    /// Variable substitution dictionary.
    /// </summary>
    public class VariableDictionary
    {
        private readonly Dictionary<string, string> _variables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private enum StateEnum
        {
            OutsideExpression,
            OnOpenBracket,
            InsideExpression,
            OnCloseBracket,
            End
        }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="Get"/> should evaluate variable expressions.
        /// </summary>
        /// <value>
        ///   <c>true</c> if <see cref="Get"/> should evaluate; otherwise, <c>false</c>.
        /// </value>
        public bool ShouldEvaluate { get; set; } = true;

        /// <summary>
        /// Gets or sets a variable by name.
        /// </summary>
        /// <param name="name">The name of the variable to set.</param>
        /// <returns>The current (evaluated) value of the variable.</returns>
        public string this[string name]
        {
            get => this.Get(name);
            set => this.Set(name, value);
        }

        /// <summary>
        /// Sets a variable value.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        public void Set(string name, string value)
        {
            if (name == null)
            {
                return;
            }

            this._variables[name] = value;
        }

        /// <summary>
        /// Gets the value of a variable, or returns <c>null</c> if the variable is not defined. If the variable contains an expression, it will be evaluated first.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns>
        /// The value of the variable, or <c>null</c> if the variable is not defined.
        /// </returns>
        public string Get(string name)
        {
            if (!this._variables.TryGetValue(name, out var variable))
            {
                return null;
            }

            return this.ShouldEvaluate ? this.Evaluate(variable) : variable;
        }

        /// <summary>
        /// Removes the variable with specified name.
        /// </summary>
        /// <param name="name">The variable name.</param>
        public void Remove(string name)
        {
            if (name == null)
            {
                return;
            }

            if (this._variables.ContainsKey(name))
            {
                this._variables.Remove(name);
            }
        }

        /// <summary>
        /// Evaluates the specified variable or text.
        /// </summary>
        /// <param name="variableOrText">The variable or text.</param>
        /// <returns>The result of the variable.</returns>
        /// <exception cref="System.FormatException">Invalid variable format.</exception>
        public string Evaluate(string variableOrText)
        {
            if (variableOrText == null)
            {
                return null;
            }

            var loop = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            return this.Eval(variableOrText, loop);
        }

        private string Eval(string variableOrText, ISet<string> loop)
        {
            var result = new StringBuilder(variableOrText.Length * 2);
            var variable = new StringBuilder();
            var state = StateEnum.OutsideExpression;

            using (var reader = new StringReader(variableOrText))
            {
                do
                {
                    int c = -1;
                    switch (state)
                    {
                        case StateEnum.OutsideExpression:
                            c = reader.Read();
                            switch (c)
                            {
                                case -1:
                                    state = StateEnum.End;
                                    break;
                                case '{':
                                    state = StateEnum.OnOpenBracket;
                                    break;
                                case '}':
                                    state = StateEnum.OnCloseBracket;
                                    break;
                                default:
                                    result.Append((char)c);
                                    break;
                            }

                            break;
                        case StateEnum.OnOpenBracket:
                            c = reader.Read();
                            switch (c)
                            {
                                case -1:
                                    throw new FormatException();
                                case '{':
                                    result.Append('{');
                                    state = StateEnum.OutsideExpression;
                                    break;
                                default:
                                    variable.Append((char)c);
                                    state = StateEnum.InsideExpression;
                                    break;
                            }

                            break;
                        case StateEnum.InsideExpression:
                            c = reader.Read();
                            switch (c)
                            {
                                case -1:
                                    throw new FormatException();
                                case '}':

                                    var v = variable.ToString();
                                    if (loop.Add(v) && this._variables.TryGetValue(v, out string value))
                                    {
                                        value = this.Eval(value, loop);
                                        result.Append(value);
                                    }

                                    variable.Length = 0;
                                    state = StateEnum.OutsideExpression;
                                    break;
                                default:
                                    variable.Append((char)c);
                                    break;
                            }

                            break;
                        case StateEnum.OnCloseBracket:
                            c = reader.Read();
                            switch (c)
                            {
                                case '}':
                                    result.Append('}');
                                    state = StateEnum.OutsideExpression;
                                    break;
                                default:
                                    throw new FormatException();
                            }

                            break;
                        default:
                            throw new FormatException("Invalid parse state.");
                    }
                }
                while (state != StateEnum.End);
            }

            return result.ToString();
        }
    }
}
