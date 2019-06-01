using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates
{
    public class ValidatorClassTemplate : CodeTemplateBase
    {
        private readonly Model _model;

        public ValidatorClassTemplate(Model model, GeneratorOptions options) : base(options)
        {
            this._model = model;
        }

        public override string WriteCode()
        {
            this.CodeBuilder.Clear();

            this.CodeBuilder.AppendLine("using System;");
            this.CodeBuilder.AppendLine("using FluentValidation;");

            if (this._model.ModelNamespace != this._model.ValidatorNamespace)
            {
                this.CodeBuilder.AppendLine($"using {this._model.ModelNamespace};");
            }

            this.CodeBuilder.AppendLine();

            this.CodeBuilder.AppendLine($"namespace {this._model.ValidatorNamespace}");
            this.CodeBuilder.AppendLine("{");

            using (this.CodeBuilder.Indent())
            {
                this.GenerateClass();
            }

            this.CodeBuilder.AppendLine("}");

            return this.CodeBuilder.ToString();
        }

        private void GenerateClass()
        {
            var validatorClass = this._model.ValidatorClass.ToSafeName();
            var modelClass = this._model.ModelClass.ToSafeName();

            if (this.Options.Model.Validator.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Validator class for <see cref=\"{modelClass}\"/> .");
                this.CodeBuilder.AppendLine("/// </summary>");
            }

            this.CodeBuilder.AppendLine($"public partial class {validatorClass}");

            if (this._model.ValidatorBaseClass.HasValue())
            {
                var validatorBase = this._model.ValidatorBaseClass.ToSafeName();
                using (this.CodeBuilder.Indent())
                {
                    this.CodeBuilder.AppendLine($": {validatorBase}");
                }
            }

            this.CodeBuilder.AppendLine("{");

            using (this.CodeBuilder.Indent())
            {
                this.GenerateConstructor();
            }

            this.CodeBuilder.AppendLine("}");
        }

        private void GenerateConstructor()
        {
            var validatorClass = this._model.ValidatorClass.ToSafeName();

            if (this.Options.Model.Validator.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{validatorClass}\"/> class.");
                this.CodeBuilder.AppendLine("/// </summary>");
            }

            this.CodeBuilder.AppendLine($"public {validatorClass}()");
            this.CodeBuilder.AppendLine("{");

            using (this.CodeBuilder.Indent())
            {
                foreach (var property in this._model.Properties)
                {
                    if (property.ValueGenerated.HasValue)
                    {
                        continue;
                    }

                    var propertyName = property.PropertyName.ToSafeName();

                    if (property.IsRequired && property.SystemType == typeof(string))
                    {
                        this.CodeBuilder.AppendLine($"RuleFor(p => p.{propertyName}).NotEmpty();");
                    }

                    if (property.Size.HasValue && property.SystemType == typeof(string))
                    {
                        this.CodeBuilder.AppendLine($"RuleFor(p => p.{propertyName}).MaximumLength({property.Size});");
                    }
                }
            }

            this.CodeBuilder.AppendLine("}");
            this.CodeBuilder.AppendLine();
        }
    }
}
