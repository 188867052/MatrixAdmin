using System.Linq;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates
{
    public class EntityClassTemplate : CodeTemplateBase
    {
        private readonly Entity _entity;

        public EntityClassTemplate(Entity entity, GeneratorOptions options) : base(options)
        {
            this._entity = entity;
        }

        public override string WriteCode()
        {
            this.CodeBuilder.Clear();
            this.CodeBuilder.AppendLine("using System;");
            this.CodeBuilder.AppendLine("using System.Collections.Generic;");
            this.CodeBuilder.AppendLine();
            this.CodeBuilder.AppendLine($"namespace {this._entity.EntityNamespace}");
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
            if (this.Options.Data.Entity.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Entity class representing data for table '{this._entity.TableName}'.");
                this.CodeBuilder.AppendLine("/// </summary>");
            }

            this.CodeBuilder.AppendLine($"public partial class {entityClass}");
            if (this._entity.EntityBaseClass.HasValue())
            {
                var entityBaseClass = this._entity.EntityBaseClass.ToSafeName();
                using (this.CodeBuilder.Indent())
                {
                    this.CodeBuilder.AppendLine($": {entityBaseClass}");
                }
            }

            this.CodeBuilder.AppendLine("{");
            using (this.CodeBuilder.Indent())
            {
                this.GenerateConstructor();
                this.GenerateProperties();
                this.GenerateRelationshipProperties();
            }

            this.CodeBuilder.AppendLine("}");
        }

        private void GenerateConstructor()
        {
            var relationships = this._entity.Relationships
                .Where(r => r.Cardinality == Cardinality.Many)
                .OrderBy(r => r.PropertyName)
                .ToList();

            var entityClass = this._entity.EntityClass.ToSafeName();

            if (this.Options.Data.Entity.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Initializes a new instance of the <see cref=\"{entityClass}\"/> class.");
                this.CodeBuilder.AppendLine("/// </summary>");
            }

            this.CodeBuilder.AppendLine($"public {entityClass}()");
            this.CodeBuilder.AppendLine("{");

            using (this.CodeBuilder.Indent())
            {
                foreach (var relationship in relationships)
                {
                    var propertyName = relationship.PropertyName.ToSafeName();
                    var primaryName = relationship.PrimaryEntity.EntityClass.ToSafeName();

                    this.CodeBuilder.AppendLine($"{propertyName} = new HashSet<{primaryName}>();");
                }
            }

            this.CodeBuilder.AppendLine("}");
            this.CodeBuilder.AppendLine();
        }

        private void GenerateProperties()
        {
            foreach (var property in this._entity.Properties)
            {
                var propertyType = property.SystemType.ToNullableType(property.IsNullable == true);
                var propertyName = property.PropertyName.ToSafeName();

                if (this.Options.Data.Entity.Document)
                {
                    this.CodeBuilder.AppendLine("/// <summary>");
                    this.CodeBuilder.AppendLine($"/// Gets or sets the property value representing column '{property.ColumnName}'.");
                    this.CodeBuilder.AppendLine("/// </summary>");
                    this.CodeBuilder.AppendLine("/// <value>");
                    this.CodeBuilder.AppendLine($"/// The property value representing column '{property.ColumnName}'.");
                    this.CodeBuilder.AppendLine("/// </value>");
                }

                this.CodeBuilder.AppendLine($"public {propertyType} {propertyName} {{ get; set; }}");
                if (!IsLastIndex(this._entity.Properties, property))
                {
                    this.CodeBuilder.AppendLine();
                }
            }

            if (this._entity.Relationships.Any())
            {
                this.CodeBuilder.AppendLine();
            }
        }

        private void GenerateRelationshipProperties()
        {
            var relationships = this._entity.Relationships.OrderBy(o => o.Cardinality).ToList();
            foreach (var relationship in relationships)
            {
                var propertyName = relationship.PropertyName.ToSafeName();
                var primaryName = relationship.PrimaryEntity.EntityClass.ToSafeName();
                switch (relationship.Cardinality)
                {
                    case Cardinality.Many:
                        if (this.Options.Data.Entity.Document)
                        {
                            this.CodeBuilder.AppendLine("/// <summary>");
                            this.CodeBuilder.AppendLine($"/// Gets or sets the navigation collection for entity <see cref=\"{primaryName}\" />.");
                            this.CodeBuilder.AppendLine("/// </summary>");
                            this.CodeBuilder.AppendLine("/// <value>");
                            this.CodeBuilder.AppendLine($"/// The the navigation collection for entity <see cref=\"{primaryName}\" />.");
                            this.CodeBuilder.AppendLine("/// </value>");
                        }

                        this.CodeBuilder.AppendLine($"public virtual ICollection<{primaryName}> {propertyName} {{ get; set; }}");
                        break;
                    case Cardinality.One:
                        {
                            if (this.Options.Data.Entity.Document)
                            {
                                this.CodeBuilder.AppendLine("/// <summary>");
                                this.CodeBuilder.AppendLine($"/// Gets or sets the navigation property for entity <see cref=\"{primaryName}\" />.");
                                this.CodeBuilder.AppendLine("/// </summary>");
                                this.CodeBuilder.AppendLine("/// <value>");
                                this.CodeBuilder.AppendLine($"/// The the navigation property for entity <see cref=\"{primaryName}\" />.");
                                this.CodeBuilder.AppendLine("/// </value>");

                                foreach (var property in relationship.Properties)
                                {
                                    this.CodeBuilder.AppendLine($"/// <seealso cref=\"{property.PropertyName}\" />");
                                }
                            }

                            this.CodeBuilder.AppendLine($"public virtual {primaryName} {propertyName} {{ get; set; }}");
                            break;
                        }
                }

                if (!IsLastIndex(relationships, relationship))
                {
                    this.CodeBuilder.AppendLine();
                }
            }
        }
    }
}
