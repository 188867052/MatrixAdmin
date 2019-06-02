using System.Runtime.CompilerServices;
using EntityFrameworkCore.Generator.Extensions;

namespace EntityFrameworkCore.Generator.Options
{
    public class OptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsBase" /> class.
        /// </summary>
        /// <param name="variables">The shared variables dictionary.</param>
        /// <param name="prefix">The variable key prefix.</param>
        public OptionsBase(VariableDictionary variables, string prefix)
        {
            this.Variables = variables;
            this.Prefix = prefix;
        }

        protected VariableDictionary Variables { get; }

        protected string Prefix { get; }

        protected static string AppendPrefix(string root, string prefix)
        {
            if (prefix.IsNullOrWhiteSpace())
            {
                return root;
            }

            return root.HasValue()
                ? $"{root}.{prefix}"
                : prefix;
        }

        protected string GetProperty([CallerMemberName] string propertyName = null)
        {
            var name = AppendPrefix(this.Prefix, propertyName);
            return this.Variables.Get(name);
        }

        protected void SetProperty(string value, [CallerMemberName] string propertyName = null)
        {
            var name = AppendPrefix(this.Prefix, propertyName);
            this.Variables.Set(name, value);
        }
    }
}