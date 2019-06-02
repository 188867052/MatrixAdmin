using System.Collections.Generic;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates
{
    public class MapperClassTemplate : CodeTemplateBase
    {
        private readonly Entity _entity;

        public MapperClassTemplate(Entity entity, GeneratorOptions options) : base(options)
        {
            this._entity = entity;
        }

        public override string WriteCode()
        {
            this.CodeBuilder.Clear();
            this.CodeBuilder.AppendLine("using System;");
            this.CodeBuilder.AppendLine("using AutoMapper;");

            var imports = new SortedSet<string>();
            imports.Add(this._entity.EntityNamespace);

            foreach (var model in this._entity.Models)
            {
                imports.Add(model.ModelNamespace);
            }

            foreach (var import in imports)
            {
                if (this._entity.MapperNamespace != import)
                {
                    this.CodeBuilder.AppendLine($"using {import};");
                }
            }

            this.CodeBuilder.AppendLine();

            this.CodeBuilder.AppendLine($"namespace {this._entity.MapperNamespace}");
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
            var entityClass = this._entity.EntityClass.ToSafeName();
            var mapperClass = this._entity.MapperClass.ToSafeName();
            if (this.Options.Model.Mapper.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Mapper class for entity <see cref=\"{entityClass}\"/> .");
                this.CodeBuilder.AppendLine("/// </summary>");
            }

            this.CodeBuilder.AppendLine($"public partial class {mapperClass}");
            if (this._entity.MapperBaseClass.HasValue())
            {
                var mapperBaseClass = this._entity.MapperBaseClass.ToSafeName();
                using (this.CodeBuilder.Indent())
                {
                    this.CodeBuilder.AppendLine($": {mapperBaseClass}");
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
            var mapperClass = this._entity.MapperClass.ToSafeName();

            var entityClass = this._entity.EntityClass.ToSafeName();
            var entityFullName = $"{this._entity.EntityNamespace}.{entityClass}";

            if (this.Options.Model.Mapper.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{mapperClass}\"/> class.");
                this.CodeBuilder.AppendLine("/// </summary>");
            }

            this.CodeBuilder.AppendLine($"public {mapperClass}()");
            this.CodeBuilder.AppendLine("{");
            using (this.CodeBuilder.Indent())
            {
                foreach (var model in this._entity.Models)
                {
                    var modelClass = model.ModelClass.ToSafeName();
                    var modelFullName = $"{model.ModelNamespace}.{modelClass}";

                    switch (model.ModelType)
                    {
                        case ModelType.Read:
                            this.CodeBuilder.AppendLine($"CreateMap<{entityFullName}, {modelFullName}>();");
                            break;
                        case ModelType.Create:
                            this.CodeBuilder.AppendLine($"CreateMap<{modelFullName}, {entityFullName}>();");
                            break;
                        case ModelType.Update:
                            this.CodeBuilder.AppendLine($"CreateMap<{entityFullName}, {modelFullName}>();");
                            this.CodeBuilder.AppendLine($"CreateMap<{modelFullName}, {entityFullName}>();");
                            break;
                    }
                }
            }

            this.CodeBuilder.AppendLine("}");
            this.CodeBuilder.AppendLine();
        }
    }
}
