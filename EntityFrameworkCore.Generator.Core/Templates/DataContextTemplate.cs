using System.Linq;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates
{
    public class DataContextTemplate : CodeTemplateBase
    {
        private readonly EntityContext _entityContext;

        public DataContextTemplate(EntityContext entityContext, GeneratorOptions options) : base(options)
        {
            this._entityContext = entityContext;
        }

        public override string WriteCode()
        {
            this.CodeBuilder.Clear();

            this.CodeBuilder.AppendLine("using System;");
            this.CodeBuilder.AppendLine("using Microsoft.EntityFrameworkCore;");
            this.CodeBuilder.AppendLine("using Microsoft.EntityFrameworkCore.Metadata;");
            this.CodeBuilder.AppendLine();
            this.CodeBuilder.AppendLine($"namespace {this._entityContext.ContextNamespace}");
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
            var contextClass = this._entityContext.ContextClass.ToSafeName();
            var baseClass = this._entityContext.ContextBaseClass.ToSafeName();
            if (this.Options.Data.Context.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine("/// A <see cref=\"DbContext\" /> instance represents a session with the database and can be used to query and save instances of entities. ");
                this.CodeBuilder.AppendLine("/// </summary>");
            }

            this.CodeBuilder.AppendLine($"public partial class {contextClass} : {baseClass}");
            this.CodeBuilder.AppendLine("{");
            using (this.CodeBuilder.Indent())
            {
                this.GenerateConstructors();
                this.GenerateDbSets();
                this.GenerateOnConfiguring();
            }

            this.CodeBuilder.AppendLine("}");
        }

        private void GenerateConstructors()
        {
            var contextName = this._entityContext.ContextClass.ToSafeName();
            if (this.Options.Data.Context.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{contextName}\"/> class.");
                this.CodeBuilder.AppendLine("/// </summary>");
                this.CodeBuilder.AppendLine("/// <param name=\"options\">The options to be used by this <see cref=\"DbContext\" />.</param>");
            }

            this.CodeBuilder.AppendLine($"public {contextName}(DbContextOptions<{contextName}> options)")
                .IncrementIndent()
                .AppendLine(": base(options)")
                .DecrementIndent()
                .AppendLine("{")
                .AppendLine("}")
                .AppendLine();
        }

        private void GenerateDbSets()
        {
            foreach (var entityType in this._entityContext.Entities)
            {
                var entityClass = entityType.EntityClass.ToSafeName();
                var propertyName = entityType.ContextProperty.ToSafeName();
                var fullName = $"{entityType.EntityNamespace}.{entityClass}";

                if (this.Options.Data.Context.Document)
                {
                    this.CodeBuilder.AppendLine("/// <summary>");
                    this.CodeBuilder.AppendLine($"/// Gets or sets the <see cref=\"T:Microsoft.EntityFrameworkCore.DbSet`1\" /> that can be used to query and save instances of <see cref=\"{fullName}\"/>.");
                    this.CodeBuilder.AppendLine("/// </summary>");
                    this.CodeBuilder.AppendLine("/// <value>");
                    this.CodeBuilder.AppendLine($"/// The <see cref=\"T:Microsoft.EntityFrameworkCore.DbSet`1\" /> that can be used to query and save instances of <see cref=\"{fullName}\"/>.");
                    this.CodeBuilder.AppendLine("/// </value>");
                }

                this.CodeBuilder.AppendLine($"public virtual DbSet<{fullName}> {propertyName} {{ get; set; }}");
                this.CodeBuilder.AppendLine();
            }
        }

        private void GenerateOnConfiguring()
        {
            if (this.Options.Data.Context.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine("/// Configure the model that was discovered from the entity types exposed in <see cref=\"T:Microsoft.EntityFrameworkCore.DbSet`1\" /> properties on this context.");
                this.CodeBuilder.AppendLine("/// </summary>");
                this.CodeBuilder.AppendLine("/// <param name=\"modelBuilder\">The builder being used to construct the model for this context.</param>");
            }

            this.CodeBuilder.AppendLine("protected override void OnModelCreating(ModelBuilder modelBuilder)");
            this.CodeBuilder.AppendLine("{");
            using (this.CodeBuilder.Indent())
            {
                foreach (var entityType in this._entityContext.Entities.OrderBy(e => e.MappingClass))
                {
                    var mappingClass = entityType.MappingClass.ToSafeName();
                    this.CodeBuilder.AppendLine($"modelBuilder.ApplyConfiguration(new {entityType.MappingNamespace}.{mappingClass}());");
                }
            }

            this.CodeBuilder.AppendLine("}");
        }
    }
}
