﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Providers;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator
{
    public class ModelGenerator
    {
        private readonly UniqueNamer _namer;
        private readonly ILogger _logger;
        private GeneratorOptions _options;
        private IProviderTypeMapping _typeMapper;

        public ModelGenerator(ILoggerFactory logger)
        {
            this._logger = logger.CreateLogger<ModelGenerator>();
            this._namer = new UniqueNamer();
        }

        public EntityContext Generate(GeneratorOptions options, DatabaseModel databaseModel)
        {
            if (databaseModel == null)
            {
                throw new ArgumentNullException(nameof(databaseModel));
            }

            this._logger.LogInformation($"Building code generation model from database: {databaseModel.DatabaseName}");

            this._options = options ?? throw new ArgumentNullException(nameof(options));
            this._typeMapper = this.GetTypeMapper();

            var entityContext = new EntityContext();
            entityContext.DatabaseName = databaseModel.DatabaseName;

            // update database variables
            this._options.Database.Name = this.ToLegalName(databaseModel.DatabaseName);

            string projectNamespace = this._options.Project.Namespace;
            this._options.Project.Namespace = projectNamespace;

            string contextClass = this._options.Data.Context.Name;
            contextClass = this._namer.UniqueClassName(contextClass);

            string contextNamespace = this._options.Data.Context.Namespace;
            string contextBaseClass = this._options.Data.Context.BaseClass;

            entityContext.ContextClass = contextClass;
            entityContext.ContextNamespace = contextNamespace;
            entityContext.ContextBaseClass = contextBaseClass;

            var tables = databaseModel.Tables;

            foreach (var t in tables)
            {
                this._logger.LogDebug($"  Processing Table : {t.Name}");

                var entity = this.GetEntity(entityContext, t);
                this.GetModels(entity);
            }

            return entityContext;
        }

        private static bool IsIgnored<TOption>(Property property, TOption options, SharedModelOptions sharedOptions)
      where TOption : ModelOptionsBase
        {
            var name = $"{property.Entity.EntityClass}.{property.PropertyName}";

            var includeExpressions = new HashSet<string>(sharedOptions.Include.Properties);
            var excludeExpressions = new HashSet<string>(sharedOptions.Exclude.Properties);

            var includeProperties = options?.Include?.Properties ?? Enumerable.Empty<string>();
            foreach (var expression in includeProperties)
            {
                includeExpressions.Add(expression);
            }

            var excludeProperties = options?.Exclude?.Properties ?? Enumerable.Empty<string>();
            foreach (var expression in excludeProperties)
            {
                excludeExpressions.Add(expression);
            }

            return IsIgnored(name, excludeExpressions, includeExpressions);
        }

        private static bool IsIgnored<TOption>(Entity entity, TOption options, SharedModelOptions sharedOptions)
            where TOption : ModelOptionsBase
        {
            var name = entity.EntityClass;

            var includeExpressions = new HashSet<string>(sharedOptions.Include.Entities);
            var excludeExpressions = new HashSet<string>(sharedOptions.Exclude.Entities);

            var includeEntities = options?.Include?.Entities ?? Enumerable.Empty<string>();
            foreach (var expression in includeEntities)
            {
                includeExpressions.Add(expression);
            }

            var excludeEntities = options?.Exclude?.Entities ?? Enumerable.Empty<string>();
            foreach (var expression in excludeEntities)
            {
                excludeExpressions.Add(expression);
            }

            return IsIgnored(name, excludeExpressions, includeExpressions);
        }

        private static bool IsIgnored(string name, IEnumerable<string> excludeExpressions, IEnumerable<string> includeExpressions)
        {
            foreach (var expression in includeExpressions)
            {
                if (Regex.IsMatch(name, expression))
                {
                    return false;
                }
            }

            foreach (var expression in excludeExpressions)
            {
                if (Regex.IsMatch(name, expression))
                {
                    return true;
                }
            }

            return false;
        }

        private Entity GetEntity(EntityContext entityContext, DatabaseTable tableSchema, bool processRelationships = true, bool processMethods = true)
        {
            Entity entity = entityContext.Entities.ByTable(tableSchema.Name, tableSchema.Schema)
                ?? this.CreateEntity(entityContext, tableSchema);

            if (!entity.Properties.IsProcessed)
            {
                this.CreateProperties(entity, tableSchema.Columns);
            }

            if (processRelationships && !entity.Relationships.IsProcessed)
            {
                this.CreateRelationships(entityContext, entity, tableSchema);
            }

            if (processMethods && !entity.Methods.IsProcessed)
            {
                this.CreateMethods(entity, tableSchema);
            }

            entity.IsProcessed = true;
            return entity;
        }

        private Entity CreateEntity(EntityContext entityContext, DatabaseTable tableSchema)
        {
            var entity = new Entity
            {
                Context = entityContext,
                TableName = tableSchema.Name,
                TableSchema = tableSchema.Schema
            };

            string entityClass = this.ToClassName(tableSchema.Name, tableSchema.Schema);
            entityClass = this._namer.UniqueClassName(entityClass);

            string entityNamespace = this._options.Data.Entity.Namespace;
            string entiyBaseClass = this._options.Data.Entity.BaseClass;

            string mappingName = entityClass + "Map";
            mappingName = this._namer.UniqueClassName(mappingName);

            string mappingNamespace = this._options.Data.Mapping.Namespace;

            string contextName = this.ContextName(entityClass);
            contextName = this.ToPropertyName(entityContext.ContextClass, contextName);
            contextName = this._namer.UniqueContextName(contextName);

            entity.EntityClass = entityClass;
            entity.EntityNamespace = entityNamespace;
            entity.EntityBaseClass = entiyBaseClass;

            entity.MappingClass = mappingName;
            entity.MappingNamespace = mappingNamespace;

            entity.ContextProperty = contextName;

            entityContext.Entities.Add(entity);

            return entity;
        }

        private void CreateProperties(Entity entity, IEnumerable<DatabaseColumn> columns)
        {
            foreach (var column in columns)
            {
                var table = column.Table;
                var property = entity.Properties.ByColumn(column.Name);

                if (property == null)
                {
                    property = new Property
                    {
                        Entity = entity,
                        ColumnName = column.Name
                    };
                    entity.Properties.Add(property);
                }

                string propertyName = this.ToPropertyName(entity.EntityClass, column.Name);
                propertyName = this._namer.UniqueName(entity.EntityClass, propertyName);

                property.PropertyName = propertyName;

                property.IsNullable = column.IsNullable;

                property.IsRowVersion = column.ValueGenerated == ValueGenerated.OnAddOrUpdate
                    && (bool?)column[ScaffoldingAnnotationNames.ConcurrencyToken] == true;

                property.IsPrimaryKey = table.PrimaryKey?.Columns.Contains(column) == true;
                property.IsForeignKey = table.ForeignKeys.Any(c => c.Columns.Contains(column));

                property.IsUnique = table.UniqueConstraints.Any(c => c.Columns.Contains(column))
                    || table.Indexes.Where(i => i.IsUnique).Any(c => c.Columns.Contains(column));

                property.Default = column.DefaultValueSql;
                property.ValueGenerated = column.ValueGenerated;

                var mapping = this._typeMapper.ParseType(column.StoreType);
                property.StoreType = mapping.StoreType;
                property.NativeType = mapping.NativeType;
                property.DataType = mapping.DataType;
                property.SystemType = mapping.SystemType;
                property.IsMaxLength = mapping.IsMaxLength;
                property.Size = mapping.Size;
                property.Precision = mapping.Precision;
                property.Scale = mapping.Scale;

                property.IsProcessed = true;
            }

            entity.Properties.IsProcessed = true;
        }

        private void CreateRelationships(EntityContext entityContext, Entity entity, DatabaseTable tableSchema)
        {
            foreach (var foreignKey in tableSchema.ForeignKeys)
            {
                this.CreateRelationship(entityContext, entity, foreignKey);
            }

            entity.Relationships.IsProcessed = true;
        }

        private void CreateRelationship(EntityContext entityContext, Entity foreignEntity, DatabaseForeignKey tableKeySchema)
        {
            Entity primaryEntity = this.GetEntity(entityContext, tableKeySchema.PrincipalTable, false, false);

            string primaryName = primaryEntity.EntityClass;
            string foreignName = foreignEntity.EntityClass;

            string relationshipName = tableKeySchema.Name;
            relationshipName = this._namer.UniqueRelationshipName(relationshipName);

            var foreignMembers = this.GetKeyMembers(foreignEntity, tableKeySchema.Columns, tableKeySchema.Name);
            bool foreignMembersRequired = foreignMembers.Any(c => c.IsRequired);

            var primaryMembers = this.GetKeyMembers(primaryEntity, tableKeySchema.PrincipalColumns, tableKeySchema.Name);
            bool primaryMembersRequired = primaryMembers.Any(c => c.IsRequired);

            // skip invalid fkeys
            if (foreignMembers.Count == 0 || primaryMembers.Count == 0)
            {
                return;
            }

            Relationship foreignRelationship = foreignEntity.Relationships
                .FirstOrDefault(r => r.RelationshipName == relationshipName && r.IsForeignKey);

            if (foreignRelationship == null)
            {
                foreignRelationship = new Relationship
                {
                    RelationshipName = relationshipName
                };
                foreignEntity.Relationships.Add(foreignRelationship);
            }

            foreignRelationship.IsMapped = true;
            foreignRelationship.IsForeignKey = true;
            foreignRelationship.Cardinality = foreignMembersRequired ? Cardinality.One : Cardinality.ZeroOrOne;

            foreignRelationship.PrimaryEntity = primaryEntity;
            foreignRelationship.PrimaryProperties = new PropertyCollection(primaryMembers);

            foreignRelationship.Entity = foreignEntity;
            foreignRelationship.Properties = new PropertyCollection(foreignMembers);

            string prefix = this.GetMemberPrefix(foreignRelationship, primaryName, foreignName);

            string foreignPropertyName = this.ToPropertyName(foreignEntity.EntityClass, prefix + primaryName);
            foreignPropertyName = this._namer.UniqueName(foreignEntity.EntityClass, foreignPropertyName);
            foreignRelationship.PropertyName = foreignPropertyName;

            // add reverse
            Relationship primaryRelationship = primaryEntity.Relationships
                .FirstOrDefault(r => r.RelationshipName == relationshipName && r.IsForeignKey == false);

            if (primaryRelationship == null)
            {
                primaryRelationship = new Relationship { RelationshipName = relationshipName };
                primaryEntity.Relationships.Add(primaryRelationship);
            }

            primaryRelationship.IsMapped = false;
            primaryRelationship.IsForeignKey = false;

            primaryRelationship.PrimaryEntity = foreignEntity;
            primaryRelationship.PrimaryProperties = new PropertyCollection(foreignMembers);

            primaryRelationship.Entity = primaryEntity;
            primaryRelationship.Properties = new PropertyCollection(primaryMembers);

            bool isOneToOne = this.IsOneToOne(tableKeySchema, foreignRelationship);
            if (isOneToOne)
            {
                primaryRelationship.Cardinality = primaryMembersRequired ? Cardinality.One : Cardinality.ZeroOrOne;
            }
            else
            {
                primaryRelationship.Cardinality = Cardinality.Many;
            }

            string primaryPropertyName = prefix + foreignName;
            if (!isOneToOne)
            {
                primaryPropertyName = this.RelationshipName(primaryPropertyName);
            }

            primaryPropertyName = this.ToPropertyName(primaryEntity.EntityClass, primaryPropertyName);
            primaryPropertyName = this._namer.UniqueName(primaryEntity.EntityClass, primaryPropertyName);

            primaryRelationship.PropertyName = primaryPropertyName;

            foreignRelationship.PrimaryPropertyName = primaryRelationship.PropertyName;
            foreignRelationship.PrimaryCardinality = primaryRelationship.Cardinality;

            primaryRelationship.PrimaryPropertyName = foreignRelationship.PropertyName;
            primaryRelationship.PrimaryCardinality = foreignRelationship.Cardinality;

            foreignRelationship.IsProcessed = true;
            primaryRelationship.IsProcessed = true;
        }

        private void CreateMethods(Entity entity, DatabaseTable tableSchema)
        {
            if (tableSchema.PrimaryKey != null)
            {
                var method = this.GetMethodFromColumns(entity, tableSchema.PrimaryKey.Columns);
                if (method != null)
                {
                    method.IsKey = true;
                    method.SourceName = tableSchema.PrimaryKey.Name;

                    if (entity.Methods.All(m => m.NameSuffix != method.NameSuffix))
                    {
                        entity.Methods.Add(method);
                    }
                }
            }

            this.GetIndexMethods(entity, tableSchema);
            this.GetForeignKeyMethods(entity, tableSchema);

            entity.Methods.IsProcessed = true;
        }

        private void GetForeignKeyMethods(Entity entity, DatabaseTable table)
        {
            var columns = new List<DatabaseColumn>();

            foreach (var column in table.ForeignKeys.SelectMany(c => c.Columns))
            {
                columns.Add(column);

                var method = this.GetMethodFromColumns(entity, columns);
                if (method != null && entity.Methods.All(m => m.NameSuffix != method.NameSuffix))
                {
                    entity.Methods.Add(method);
                }

                columns.Clear();
            }
        }

        private void GetIndexMethods(Entity entity, DatabaseTable table)
        {
            foreach (var index in table.Indexes)
            {
                var method = this.GetMethodFromColumns(entity, index.Columns);
                if (method == null)
                {
                    continue;
                }

                method.SourceName = index.Name;
                method.IsUnique = index.IsUnique;
                method.IsIndex = true;

                if (entity.Methods.All(m => m.NameSuffix != method.NameSuffix))
                {
                    entity.Methods.Add(method);
                }
            }
        }

        private Method GetMethodFromColumns(Entity entity, IEnumerable<DatabaseColumn> columns)
        {
            var method = new Method { Entity = entity };
            var methodName = new StringBuilder();

            foreach (var column in columns)
            {
                var property = entity.Properties.ByColumn(column.Name);
                if (property == null)
                {
                    continue;
                }

                method.Properties.Add(property);
                methodName.Append(property.PropertyName);
            }

            if (method.Properties.Count == 0)
            {
                return null;
            }

            method.NameSuffix = methodName.ToString();
            return method;
        }

        private void GetModels(Entity entity)
        {
            if (entity == null || entity.Models.IsProcessed)
            {
                return;
            }

            this._options.Variables.Set("Entity.Name", entity.EntityClass);

            if (this._options.Model.Read.Generate)
            {
                this.CreateModel(entity, this._options.Model.Read, ModelType.Read);
            }

            if (this._options.Model.Create.Generate)
            {
                this.CreateModel(entity, this._options.Model.Create, ModelType.Create);
            }

            if (this._options.Model.Update.Generate)
            {
                this.CreateModel(entity, this._options.Model.Update, ModelType.Update);
            }

            if (entity.Models.Count > 0)
            {
                var mapperNamespace = this._options.Model.Mapper.Namespace;

                var mapperClass = this.ToLegalName(this._options.Model.Mapper.Name);
                mapperClass = this._namer.UniqueModelName(mapperNamespace, mapperClass);

                entity.MapperClass = mapperClass;
                entity.MapperNamespace = mapperNamespace;
                entity.MapperBaseClass = this._options.Model.Mapper.BaseClass;
            }

            this._options.Variables.Remove("Entity.Name");

            entity.Models.IsProcessed = true;
        }

        private void CreateModel<TOption>(Entity entity, TOption options, ModelType modelType)
            where TOption : ModelOptionsBase
        {
            if (IsIgnored(entity, options, this._options.Model.Shared))
            {
                return;
            }

            var modelNamespace = options.Namespace.HasValue()
                ? options.Namespace
                : this._options.Model.Shared.Namespace;

            var modelClass = this.ToLegalName(options.Name);
            modelClass = this._namer.UniqueModelName(modelNamespace, modelClass);

            var model = new Model
            {
                Entity = entity,
                ModelType = modelType,
                ModelBaseClass = options.BaseClass,
                ModelNamespace = modelNamespace,
                ModelClass = modelClass
            };

            foreach (var property in entity.Properties)
            {
                if (IsIgnored(property, options, this._options.Model.Shared))
                {
                    continue;
                }

                model.Properties.Add(property);
            }

            this._options.Variables.Set("Model.Name", model.ModelClass);

            var validatorNamespace = this._options.Model.Validator.Namespace;
            var validatorClass = this.ToLegalName(this._options.Model.Validator.Name);
            validatorClass = this._namer.UniqueModelName(validatorNamespace, validatorClass);

            model.ValidatorBaseClass = this._options.Model.Validator.BaseClass;
            model.ValidatorClass = validatorClass;
            model.ValidatorNamespace = validatorNamespace;

            entity.Models.Add(model);

            this._options.Variables.Remove("Model.Name");
        }

        private List<Property> GetKeyMembers(Entity entity, IEnumerable<DatabaseColumn> members, string relationshipName)
        {
            var keyMembers = new List<Property>();

            foreach (var member in members)
            {
                var property = entity.Properties.ByColumn(member.Name);

                if (property == null)
                {
                    this._logger.LogWarning("Could not find column {0} for relationship {1}.", member.Name, relationshipName);
                }
                else
                {
                    keyMembers.Add(property);
                }
            }

            return keyMembers;
        }

        private string GetMemberPrefix(Relationship relationship, string primaryClass, string foreignClass)
        {
            string thisKey = relationship.Properties
                .Select(p => p.PropertyName)
                .FirstOrDefault() ?? string.Empty;

            string otherKey = relationship.PrimaryProperties
                .Select(p => p.PropertyName)
                .FirstOrDefault() ?? string.Empty;

            bool isSameName = thisKey.Equals(otherKey, StringComparison.OrdinalIgnoreCase);
            isSameName = isSameName || thisKey.Equals(primaryClass + otherKey, StringComparison.OrdinalIgnoreCase);

            string prefix = string.Empty;
            if (isSameName)
            {
                return prefix;
            }

            prefix = thisKey.Replace(otherKey, string.Empty);
            prefix = prefix.Replace(primaryClass, string.Empty);
            prefix = prefix.Replace(foreignClass, string.Empty);
            prefix = Regex.Replace(prefix, @"(_ID|_id|_Id|\.ID|\.id|\.Id|ID|Id)$", string.Empty);
            prefix = Regex.Replace(prefix, @"^\d", string.Empty);

            return prefix;
        }

        private bool IsOneToOne(DatabaseForeignKey tableKeySchema, Relationship foreignRelationship)
        {
            var foreignColumn = foreignRelationship.Properties
                .Select(p => p.ColumnName)
                .FirstOrDefault();

            bool isFkeyPkey = tableKeySchema.PrincipalTable.PrimaryKey != null
                              && tableKeySchema.Table.PrimaryKey != null
                              && tableKeySchema.Table.PrimaryKey.Columns.Count == 1
                              && tableKeySchema.Table.PrimaryKey.Columns.Any(c => c.Name == foreignColumn);

            if (isFkeyPkey)
            {
                return true;
            }

            return false;
        }

        private string RelationshipName(string name)
        {
            var naming = this._options.Data.Entity.RelationshipNaming;
            if (naming == RelationshipNaming.Preserve)
            {
                return name;
            }

            if (naming == RelationshipNaming.Suffix)
            {
                return name + "List";
            }

            return name.Pluralize();
        }

        private string ContextName(string name)
        {
            var naming = this._options.Data.Context.PropertyNaming;
            if (naming == ContextNaming.Preserve)
            {
                return name;
            }

            if (naming == ContextNaming.Suffix)
            {
                return name + "DataSet";
            }

            return name.Pluralize();
        }

        private string EntityName(string name)
        {
            var tableNaming = this._options.Database.TableNaming;
            var entityNaming = this._options.Data.Entity.EntityNaming;

            if (tableNaming != TableNaming.Plural && entityNaming == EntityNaming.Plural)
            {
                name = name.Pluralize();
            }
            else if (tableNaming != TableNaming.Singular && entityNaming == EntityNaming.Singular)
            {
                name = name.Singularize();
            }

            return name;
        }

        private string ToClassName(string tableName, string tableSchema)
        {
            tableName = this.EntityName(tableName);
            var className = tableName;

            if (this._options.Data.Entity.PrefixWithSchemaName && tableSchema != null)
            {
                className = $"{tableSchema}{tableName}";
            }

            string legalName = this.ToLegalName(className);

            return legalName;
        }

        private string ToPropertyName(string className, string name)
        {
            string propertyName = this.ToLegalName(name);
            if (className.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
            {
                propertyName += "Member";
            }

            return propertyName;
        }

        private string ToLegalName(string name)
        {
            string legalName = name;

            // remove invalid leading identifiers
            if (Regex.IsMatch(name, @"^[^a-zA-Z_]+"))
            {
                legalName = Regex.Replace(legalName, @"^[^a-zA-Z_]+", string.Empty);
            }

            // prefix with column when all characters removed
            if (legalName.IsNullOrWhiteSpace())
            {
                legalName = "Number" + name;
            }

            legalName = legalName.ToPascalCase();

            return legalName;
        }

        private IProviderTypeMapping GetTypeMapper()
        {
            var provider = this._options.Database.Provider;

            this._logger.LogTrace($"Creating database model factory for: {provider}");
            if (provider == DatabaseProviders.SqlServer)
            {
                return new SqlServerTypeMapping();
            }

            throw new NotSupportedException($"The specified provider '{provider}' is not supported.");
        }
    }
}