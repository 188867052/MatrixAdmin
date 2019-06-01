using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates
{
    public class ModelClassTemplate : CodeTemplateBase
    {
        private readonly Model _model;

        public ModelClassTemplate(Model model, GeneratorOptions options) : base(options)
        {
            this._model = model;
        }

        public override string WriteCode()
        {
            this.CodeBuilder.Clear();
            this.CodeBuilder.AppendLine("using System;");
            this.CodeBuilder.AppendLine("using System.Collections.Generic;");
            this.CodeBuilder.AppendLine();
            this.CodeBuilder.AppendLine($"namespace {this._model.ModelNamespace}");
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
            var modelClass = this._model.ModelClass.ToSafeName();

            if (this.ShouldDocument())
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine("/// View Model class");
                this.CodeBuilder.AppendLine("/// </summary>");
            }

            this.CodeBuilder.AppendLine($"public partial class {modelClass}");

            if (this._model.ModelBaseClass.HasValue())
            {
                var modelBase = this._model.ModelBaseClass.ToSafeName();
                using (this.CodeBuilder.Indent())
                {
                    this.CodeBuilder.AppendLine($": {modelBase}");
                }
            }

            this.CodeBuilder.AppendLine("{");

            using (this.CodeBuilder.Indent())
            {
                this.GenerateProperties();
            }

            this.CodeBuilder.AppendLine("}");
        }

        private void GenerateProperties()
        {
            foreach (var property in this._model.Properties)
            {
                var propertyType = property.SystemType.ToNullableType(property.IsNullable == true);
                var propertyName = property.PropertyName.ToSafeName();

                if (this.ShouldDocument())
                {
                    this.CodeBuilder.AppendLine("/// <summary>");
                    this.CodeBuilder.AppendLine($"/// Gets or sets the property value for '{property.PropertyName}'.");
                    this.CodeBuilder.AppendLine("/// </summary>");
                    this.CodeBuilder.AppendLine("/// <value>");
                    this.CodeBuilder.AppendLine($"/// The property value for '{property.PropertyName}'.");
                    this.CodeBuilder.AppendLine("/// </value>");
                }

                this.CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }}");
                this.CodeBuilder.AppendLine();
            }

            this.CodeBuilder.AppendLine();
        }

        private bool ShouldDocument()
        {
            if (this._model.ModelType == ModelType.Create)
            {
                return this.Options.Model.Create.Document;
            }

            if (this._model.ModelType == ModelType.Update)
            {
                return this.Options.Model.Update.Document;
            }

            return this.Options.Model.Read.Document;
        }
    }
}
