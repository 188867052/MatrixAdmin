using System.Globalization;
using System.Linq;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.Generator.Templates
{
    public class MappingClassTemplate : CodeTemplateBase
    {
        private readonly Entity _entity;

        public MappingClassTemplate(Entity entity, GeneratorOptions options) : base(options)
        {
            this._entity = entity;
        }

        public override string WriteCode()
        {
            this.CodeBuilder.Clear();

            this.CodeBuilder.AppendLine("using System;");
            this.CodeBuilder.AppendLine("using System.Collections.Generic;");
            this.CodeBuilder.AppendLine("using Microsoft.EntityFrameworkCore;");
            this.CodeBuilder.AppendLine();

            this.CodeBuilder.AppendLine($"namespace {this._entity.MappingNamespace}");
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
            var mappingClass = this._entity.MappingClass.ToSafeName();
            var entityClass = this._entity.EntityClass.ToSafeName();
            var safeName = $"{this._entity.EntityNamespace}.{entityClass}";

            if (this.Options.Data.Mapping.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Allows configuration for an entity type <see cref=\"{safeName}\" />");
                this.CodeBuilder.AppendLine("/// </summary>");
            }

            this.CodeBuilder.AppendLine($"public partial class {mappingClass}");

            using (this.CodeBuilder.Indent())
            {
                this.CodeBuilder.AppendLine($": IEntityTypeConfiguration<{safeName}>");
            }

            this.CodeBuilder.AppendLine("{");

            using (this.CodeBuilder.Indent())
            {
                this.GenerateConfigure();
            }

            this.CodeBuilder.AppendLine("}");
        }

        private void GenerateConfigure()
        {
            var entityClass = this._entity.EntityClass.ToSafeName();
            var entityFullName = $"{this._entity.EntityNamespace}.{entityClass}";

            if (this.Options.Data.Mapping.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Configures the entity of type <see cref=\"{entityFullName}\" />");
                this.CodeBuilder.AppendLine("/// </summary>");
                this.CodeBuilder.AppendLine("/// <param name=\"builder\">The builder to be used to configure the entity type.</param>");
            }

            this.CodeBuilder.AppendLine($"public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<{entityFullName}> builder)");
            this.CodeBuilder.AppendLine("{");
            using (this.CodeBuilder.Indent())
            {
                this.GenerateTableMapping();
                this.GenerateKeyMapping();
                this.GeneratePropertyMapping();
                this.GenerateRelationshipMapping();
            }

            this.CodeBuilder.AppendLine("}");
            this.CodeBuilder.AppendLine();
        }

        private void GenerateRelationshipMapping()
        {
            this.CodeBuilder.AppendLine("// relationships");
            foreach (var relationship in this._entity.Relationships.Where(e => e.IsMapped))
            {
                this.GenerateRelationshipMapping(relationship);
                this.CodeBuilder.AppendLine();
            }
        }

        private void GenerateRelationshipMapping(Relationship relationship)
        {
            this.CodeBuilder.Append("builder.HasOne(t => t.");
            this.CodeBuilder.Append(relationship.PropertyName);
            this.CodeBuilder.Append(")");
            this.CodeBuilder.AppendLine();
            this.CodeBuilder.IncrementIndent();
            this.CodeBuilder.Append(relationship.PrimaryCardinality == Cardinality.Many
                ? ".WithMany(t => t."
                : ".WithOne(t => t.");
            this.CodeBuilder.Append(relationship.PrimaryPropertyName);
            this.CodeBuilder.Append(")");
            this.CodeBuilder.AppendLine();
            this.CodeBuilder.Append(".HasForeignKey");
            if (relationship.IsOneToOne)
            {
                this.CodeBuilder.Append("<");
                this.CodeBuilder.Append(this._entity.EntityNamespace);
                this.CodeBuilder.Append(".");
                this.CodeBuilder.Append(this._entity.EntityClass.ToSafeName());
                this.CodeBuilder.Append(">");
            }

            this.CodeBuilder.Append("(d => ");
            var keys = relationship.Properties;
            bool wroteLine = false;
            if (keys.Count == 1)
            {
                var propertyName = keys.First().PropertyName.ToSafeName();
                this.CodeBuilder.Append($"d.{propertyName}");
            }
            else
            {
                this.CodeBuilder.Append("new { ");
                foreach (var p in keys)
                {
                    if (wroteLine)
                    {
                        this.CodeBuilder.Append(", ");
                    }

                    this.CodeBuilder.Append($"d.{p.PropertyName}");
                    wroteLine = true;
                }

                this.CodeBuilder.Append("}");
            }

            this.CodeBuilder.Append(")");
            if (!string.IsNullOrEmpty(relationship.RelationshipName))
            {
                this.CodeBuilder.AppendLine();
                this.CodeBuilder.Append(".HasConstraintName(\"");
                this.CodeBuilder.Append(relationship.RelationshipName);
                this.CodeBuilder.Append("\")");
            }

            this.CodeBuilder.DecrementIndent();
            this.CodeBuilder.AppendLine(";");
        }

        private void GeneratePropertyMapping()
        {
            this.CodeBuilder.AppendLine("// properties");
            foreach (var property in this._entity.Properties)
            {
                this.GeneratePropertyMapping(property);
                this.CodeBuilder.AppendLine();
            }
        }

        private void GeneratePropertyMapping(Property property)
        {
            bool isString = property.SystemType == typeof(string);
            bool isByteArray = property.SystemType == typeof(byte[]);
            this.CodeBuilder.Append($"builder.Property(t => t.{property.PropertyName})");
            this.CodeBuilder.IncrementIndent();
            if (property.IsRequired)
            {
                this.CodeBuilder.AppendLine();
                this.CodeBuilder.Append(".IsRequired()");
            }

            if (property.IsRowVersion == true)
            {
                this.CodeBuilder.AppendLine();
                this.CodeBuilder.Append(".IsRowVersion()");
            }

            this.CodeBuilder.AppendLine();
            this.CodeBuilder.Append($".HasColumnName({property.ColumnName.ToLiteral()})");
            if (!string.IsNullOrEmpty(property.StoreType))
            {
                this.CodeBuilder.AppendLine();
                this.CodeBuilder.Append($".HasColumnType({property.StoreType.ToLiteral()})");
            }

            if ((isString || isByteArray) && property.Size > 0 && property.IsMaxLength != true)
            {
                this.CodeBuilder.AppendLine();
                this.CodeBuilder.Append($".HasMaxLength({property.Size.Value.ToString(CultureInfo.InvariantCulture)})");
            }

            if (!string.IsNullOrEmpty(property.Default))
            {
                this.CodeBuilder.AppendLine();
                this.CodeBuilder.Append($".HasDefaultValueSql({property.Default.ToLiteral()})");
            }

            switch (property.ValueGenerated)
            {
                case ValueGenerated.OnAdd:
                    this.CodeBuilder.AppendLine();
                    this.CodeBuilder.Append(".ValueGeneratedOnAdd()");
                    break;
                case ValueGenerated.OnAddOrUpdate:
                    this.CodeBuilder.AppendLine();
                    this.CodeBuilder.Append(".ValueGeneratedOnAddOrUpdate()");
                    break;
                case ValueGenerated.OnUpdate:
                    this.CodeBuilder.AppendLine();
                    this.CodeBuilder.Append(".ValueGeneratedOnUpdate()");
                    break;
            }

            this.CodeBuilder.DecrementIndent();
            this.CodeBuilder.AppendLine(";");
        }

        private void GenerateKeyMapping()
        {
            var keys = this._entity.Properties.Where(p => p.IsPrimaryKey == true).ToList();
            if (keys.Count == 0)
            {
                return;
            }

            this.CodeBuilder.AppendLine("// key");
            this.CodeBuilder.Append("builder.HasKey(t => ");

            if (keys.Count == 1)
            {
                var propertyName = keys.First().PropertyName.ToSafeName();
                this.CodeBuilder.AppendLine($"t.{propertyName});");
                this.CodeBuilder.AppendLine();

                return;
            }

            bool wroteLine = false;
            this.CodeBuilder.Append("new { ");
            foreach (var p in keys)
            {
                if (wroteLine)
                {
                    this.CodeBuilder.Append(", ");
                }

                this.CodeBuilder.Append("t.");
                this.CodeBuilder.Append(p.PropertyName);
                wroteLine = true;
            }

            this.CodeBuilder.AppendLine(" });");
            this.CodeBuilder.AppendLine();
        }

        private void GenerateTableMapping()
        {
            this.CodeBuilder.AppendLine("// table");

            this.CodeBuilder.AppendLine(this._entity.TableSchema.HasValue()
                ? $"builder.ToTable(\"{this._entity.TableName}\", \"{this._entity.TableSchema}\");"
                : $"builder.ToTable(\"{this._entity.TableName}\");");

            this.CodeBuilder.AppendLine();
        }
    }
}
