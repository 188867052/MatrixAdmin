using System;
using System.Diagnostics;
using System.IO;
using EntityFrameworkCore.Generator.Core.Extensions;
using EntityFrameworkCore.Generator.Core.Metadata.Generation;
using EntityFrameworkCore.Generator.Core.Options;
using EntityFrameworkCore.Generator.Core.Parsing;
using EntityFrameworkCore.Generator.Core.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Core
{
    public class CodeGenerator : ICodeGenerator
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private readonly IDiagnosticsLogger<DbLoggerCategory.Scaffolding> _diagnosticsLogger;
        private readonly ModelGenerator _modelGenerator;
        private readonly SourceSynchronizer _synchronizer;

        public CodeGenerator(ILoggerFactory loggerFactory)
        {
            this._loggerFactory = loggerFactory;
            this._logger = loggerFactory.CreateLogger<CodeGenerator>();
            this._diagnosticsLogger = new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(loggerFactory, new LoggingOptions(), new DiagnosticListener(string.Empty));
            this._modelGenerator = new ModelGenerator(loggerFactory);
            this._synchronizer = new SourceSynchronizer(loggerFactory);
        }

        public GeneratorOptions Options { get; set; }

        public bool Generate(GeneratorOptions options)
        {
            this.Options = options ?? throw new ArgumentNullException(nameof(options));

            var factory = this.GetDatabaseModelFactory();
            var databaseModel = this.GetDatabaseModel(factory);

            if (databaseModel == null)
            {
                throw new InvalidOperationException("Failed to create database model");
            }

            this._logger.LogInformation($"Loaded database model for: {databaseModel.DatabaseName}");

            var context = this._modelGenerator.Generate(this.Options, databaseModel);

            this._synchronizer.UpdateFromSource(context, options);

            this.GenerateFiles(context);

            return true;
        }

        private void GenerateFiles(EntityContext entityContext)
        {
            this.GenerateDataContext(entityContext);
            this.GenerateEntityClasses(entityContext);
            this.GenerateMappingClasses(entityContext);

            if (this.Options.Data.Query.Generate)
            {
                this.GenerateQueryExtensions(entityContext);
            }

            this.GenerateModelClasses(entityContext);
        }

        private void GenerateQueryExtensions(EntityContext entityContext)
        {
            foreach (var entity in entityContext.Entities)
            {
                var directory = this.Options.Data.Query.Directory;
                var file = entity.EntityClass + "Extensions.cs";
                var path = Path.Combine(directory, file);

                this._logger.LogInformation(File.Exists(path)
                    ? $"Updating query extensions class: {file}"
                    : $"Creating query extensions class: {file}");

                var template = new QueryExtensionTemplate(entity, this.Options);
                template.WriteCode(path);
            }
        }

        private void GenerateMappingClasses(EntityContext entityContext)
        {
            foreach (var entity in entityContext.Entities)
            {
                var directory = this.Options.Data.Mapping.Directory;
                var file = entity.MappingClass + ".cs";
                var path = Path.Combine(directory, file);

                this._logger.LogInformation(File.Exists(path)
                    ? $"Updating mapping class: {file}"
                    : $"Creating mapping class: {file}");

                var template = new MappingClassTemplate(entity, this.Options);
                template.WriteCode(path);
            }
        }

        private void GenerateEntityClasses(EntityContext entityContext)
        {
            foreach (var entity in entityContext.Entities)
            {
                this.Options.Variables.Set("Entity.Name", entity.EntityClass);

                var directory = this.Options.Data.Entity.Directory;
                var file = entity.EntityClass + ".cs";
                var path = Path.Combine(directory, file);

                this._logger.LogInformation(File.Exists(path)
                    ? $"Updating entity class: {file}"
                    : $"Creating entity class: {file}");

                var template = new EntityClassTemplate(entity, this.Options);

                template.WriteCode(path);
            }

            this.Options.Variables.Remove("Entity.Name");
        }

        private void GenerateDataContext(EntityContext entityContext)
        {
            var directory = this.Options.Data.Context.Directory;
            var file = entityContext.ContextClass + ".cs";
            var path = Path.Combine(directory, file);

            this._logger.LogInformation(File.Exists(path)
                ? $"Updating data context class: {file}"
                : $"Creating data context class: {file}");

            var template = new DbContextTemplate(entityContext, this.Options);
            template.WriteCode(path);
        }

        private void GenerateModelClasses(EntityContext entityContext)
        {
            foreach (var entity in entityContext.Entities)
            {
                this.Options.Variables.Set("Entity.Name", entity.EntityClass);
                if (entity.Models.Count <= 0)
                {
                    continue;
                }

                this.GenerateModelClasses(entity);
                this.GenerateValidatorClasses(entity);
                this.GenerateMapperClass(entity);
            }

            this.Options.Variables.Remove("Entity.Name");
        }

        private void GenerateModelClasses(Entity entity)
        {
            foreach (var model in entity.Models)
            {
                this.Options.Variables.Set("Model.Name", entity.EntityClass);

                var directory = this.GetModelDirectory(model);
                var file = model.ModelClass + ".cs";
                var path = Path.Combine(directory, file);

                this._logger.LogInformation(File.Exists(path)
                    ? $"Updating model class: {file}"
                    : $"Creating model class: {file}");

                var template = new ModelClassTemplate(model, this.Options);
                template.WriteCode(path);
            }

            this.Options.Variables.Remove("Model.Name");
        }

        private string GetModelDirectory(Model model)
        {
            if (model.ModelType == ModelType.Create)
            {
                return this.Options.Model.Create.Directory.HasValue()
                    ? this.Options.Model.Create.Directory
                    : this.Options.Model.Shared.Directory;
            }

            if (model.ModelType == ModelType.Update)
            {
                return this.Options.Model.Update.Directory.HasValue()
                    ? this.Options.Model.Update.Directory
                    : this.Options.Model.Shared.Directory;
            }

            return this.Options.Model.Read.Directory.HasValue()
                ? this.Options.Model.Read.Directory
                : this.Options.Model.Shared.Directory;
        }

        private void GenerateValidatorClasses(Entity entity)
        {
            if (!this.Options.Model.Validator.Generate)
            {
                return;
            }

            foreach (var model in entity.Models)
            {
                this.Options.Variables.Set("Model.Name", entity.EntityClass);

                // don't validate read models
                if (model.ModelType == ModelType.Read)
                {
                    continue;
                }

                var directory = this.Options.Model.Validator.Directory;
                var file = model.ValidatorClass + ".cs";
                var path = Path.Combine(directory, file);

                this._logger.LogInformation(File.Exists(path)
                    ? $"Updating validation class: {file}"
                    : $"Creating validation class: {file}");

                var template = new ValidatorClassTemplate(model, this.Options);
                template.WriteCode(path);
            }

            this.Options.Variables.Remove("Model.Name");
        }

        private void GenerateMapperClass(Entity entity)
        {
            if (!this.Options.Model.Mapper.Generate)
            {
                return;
            }

            var directory = this.Options.Model.Mapper.Directory;
            var file = entity.MapperClass + ".cs";
            var path = Path.Combine(directory, file);

            this._logger.LogInformation(File.Exists(path)
                ? $"Updating object mapper class: {file}"
                : $"Creating object mapper class: {file}");

            var template = new MapperClassTemplate(entity, this.Options);
            template.WriteCode(path);
        }

        private DatabaseModel GetDatabaseModel(IDatabaseModelFactory factory)
        {
            this._logger.LogInformation("Loading database model ...");

            var database = this.Options.Database;
            var connectionString = this.ResolveConnectionString(database);

            return factory.Create(connectionString, database.Tables, database.Schemas);
        }

        private string ResolveConnectionString(DatabaseOptions database)
        {
            if (database.ConnectionString.HasValue())
            {
                return database.ConnectionString;
            }

            if (database.UserSecretsId.HasValue())
            {
                var secretsStore = new SecretsStore(database.UserSecretsId);
                if (secretsStore.ContainsKey(database.ConnectionName))
                {
                    return secretsStore[database.ConnectionName];
                }
            }

            throw new InvalidOperationException("Could not find connection string.");
        }

        private IDatabaseModelFactory GetDatabaseModelFactory()
        {
            var provider = this.Options.Database.Provider;

            this._logger.LogDebug($"Creating database model factory for: {provider}");
            if (provider == DatabaseProviders.SqlServer)
            {
                return new Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal.SqlServerDatabaseModelFactory(this._diagnosticsLogger);
            }

            if (provider == DatabaseProviders.PostgreSQL)
            {
                return new Npgsql.EntityFrameworkCore.PostgreSQL.Scaffolding.Internal.NpgsqlDatabaseModelFactory(this._diagnosticsLogger);
            }

            if (this.Options.Database.Provider == DatabaseProviders.Sqlite)
            {
                return new Microsoft.EntityFrameworkCore.Sqlite.Scaffolding.Internal.SqliteDatabaseModelFactory(this._diagnosticsLogger, null);
            }

            throw new NotSupportedException($"The specified provider '{provider}' is not supported.");
        }
    }
}
