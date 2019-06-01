using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkCore.Generator.Options;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests
{
    public class ModelGeneratorTests
    {
        [Fact]
        public void GenerateCheckNames()
        {
            var generatorOptions = new GeneratorOptions();
            var databaseModel = new DatabaseModel
            {
                DatabaseName = "TestDatabase",
                DefaultSchema = "dbo"
            };
            var configuration = new DatabaseTable
            {
                Database = databaseModel,
                Name = "Configuration",
                Schema = "dbo"
            };
            databaseModel.Tables.Add(configuration);

            var identifierColumn = new DatabaseColumn
            {
                Table = configuration,
                Name = "Id",
                IsNullable = false,
                StoreType = "int"
            };
            configuration.Columns.Add(identifierColumn);

            var nameColumn = new DatabaseColumn
            {
                Table = configuration,
                Name = "Name",
                IsNullable = true,
                StoreType = "varchar(50)"
            };
            configuration.Columns.Add(nameColumn);
            var generator = new ModelGenerator(NullLoggerFactory.Instance);

            var result = generator.Generate(generatorOptions, databaseModel);
            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(1);

            var firstEntity = result.Entities[0];
            firstEntity.TableName.Should().Be("Configuration");
            firstEntity.TableSchema.Should().Be("dbo");
            firstEntity.EntityClass.Should().Be("Configuration");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.MappingClass.Should().Be("ConfigurationMap");
            firstEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");

            firstEntity.Properties.Count.Should().Be(2);

            var identifierProperty = firstEntity.Properties.ByColumn("Id");
            identifierProperty.Should().NotBeNull();
            identifierProperty.PropertyName.Should().Be("Id");

            var nameProperty = firstEntity.Properties.ByColumn("Name");
            nameProperty.Should().NotBeNull();
            nameProperty.PropertyName.Should().Be("Name");
        }

        [Fact]
        public void GenerateModelsCheckNames()
        {
            var generatorOptions = new GeneratorOptions();
            generatorOptions.Model.Read.Generate = true;
            generatorOptions.Model.Create.Generate = true;
            generatorOptions.Model.Update.Generate = true;
            generatorOptions.Model.Validator.Generate = true;
            generatorOptions.Model.Mapper.Generate = true;

            var databaseModel = new DatabaseModel
            {
                DatabaseName = "TestDatabase",
                DefaultSchema = "dbo"
            };
            var configuration = new DatabaseTable
            {
                Database = databaseModel,
                Name = "Configuration",
                Schema = "dbo"
            };
            databaseModel.Tables.Add(configuration);

            var identifierColumn = new DatabaseColumn
            {
                Table = configuration,
                Name = "Id",
                IsNullable = false,
                StoreType = "int"
            };
            configuration.Columns.Add(identifierColumn);

            var nameColumn = new DatabaseColumn
            {
                Table = configuration,
                Name = "Name",
                IsNullable = true,
                StoreType = "varchar(50)"
            };
            configuration.Columns.Add(nameColumn);
            var generator = new ModelGenerator(NullLoggerFactory.Instance);

            var result = generator.Generate(generatorOptions, databaseModel);
            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(1);

            var firstEntity = result.Entities[0];
            firstEntity.TableName.Should().Be("Configuration");
            firstEntity.TableSchema.Should().Be("dbo");
            firstEntity.EntityClass.Should().Be("Configuration");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.MappingClass.Should().Be("ConfigurationMap");
            firstEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");
            firstEntity.MapperClass.Should().Be("ConfigurationProfile");
            firstEntity.MapperNamespace.Should().Be("TestDatabase.Domain.Mapping");

            firstEntity.Properties.Count.Should().Be(2);
            firstEntity.Models.Count.Should().Be(3);

            var firstModel = firstEntity.Models[0];
            firstModel.ModelClass.Should().StartWith("Configuration");
            firstModel.ModelClass.Should().EndWith("Model");
            firstModel.ModelNamespace.Should().Be("TestDatabase.Domain.Models");
            firstModel.ValidatorClass.Should().StartWith("Configuration");
            firstModel.ValidatorClass.Should().EndWith("Validator");
            firstModel.ValidatorNamespace.Should().Be("TestDatabase.Domain.Validation");
        }

        [Fact]
        public void GenerateWithSymbolInDatabaseName()
        {
            var generatorOptions = new GeneratorOptions();
            var databaseModel = new DatabaseModel
            {
                DatabaseName = "Test+Symbol",
                DefaultSchema = "dbo"
            };
            var databaseTable = new DatabaseTable
            {
                Database = databaseModel,
                Name = "Test+Error"
            };
            databaseModel.Tables.Add(databaseTable);

            var databaseColumn = new DatabaseColumn
            {
                Table = databaseTable,
                Name = "Id",
                IsNullable = false,
                StoreType = "int"
            };
            databaseTable.Columns.Add(databaseColumn);

            var generator = new ModelGenerator(NullLoggerFactory.Instance);
            var result = generator.Generate(generatorOptions, databaseModel);
            result.ContextClass.Should().Be("TestSymbolContext");
            result.ContextNamespace.Should().Be("TestSymbol.Data");

            result.Entities.Count.Should().Be(1);
            result.Entities[0].EntityClass.Should().Be("TestError");
            result.Entities[0].EntityNamespace.Should().Be("TestSymbol.Data.Entities");
        }

        [Fact]
        public void GenerateWithAllNumberColumnName()
        {
            var generatorOptions = new GeneratorOptions();
            var databaseModel = new DatabaseModel
            {
                DatabaseName = "TestDatabase",
                DefaultSchema = "dbo"
            };
            var configuration = new DatabaseTable
            {
                Database = databaseModel,
                Name = "Configuration"
            };
            databaseModel.Tables.Add(configuration);

            var identifierColumn = new DatabaseColumn
            {
                Table = configuration,
                Name = "Id",
                IsNullable = false,
                StoreType = "int"
            };
            configuration.Columns.Add(identifierColumn);

            var numberColumn = new DatabaseColumn
            {
                Table = configuration,
                Name = "404",
                IsNullable = true,
                StoreType = "int"
            };
            configuration.Columns.Add(numberColumn);
            var generator = new ModelGenerator(NullLoggerFactory.Instance);

            var result = generator.Generate(generatorOptions, databaseModel);
            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(1);

            var firstEntity = result.Entities[0];
            firstEntity.EntityClass.Should().Be("Configuration");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.Properties.Count.Should().Be(2);

            var identifierProperty = firstEntity.Properties.ByColumn("Id");
            identifierProperty.Should().NotBeNull();
            identifierProperty.PropertyName.Should().Be("Id");

            var numberProperty = firstEntity.Properties.ByColumn("404");
            numberProperty.Should().NotBeNull();
            numberProperty.PropertyName.Should().Be("Number404");
        }

        [Fact]
        public void GenerateWithComplexDefaultValue()
        {
            var generatorOptions = new GeneratorOptions();
            var databaseModel = new DatabaseModel
            {
                DatabaseName = "TestDatabase",
                DefaultSchema = "dbo"
            };
            var configuration = new DatabaseTable
            {
                Database = databaseModel,
                Name = "Configuration",
                Schema = "dbo"
            };
            databaseModel.Tables.Add(configuration);

            var identifierColumn = new DatabaseColumn
            {
                Table = configuration,
                Name = "Id",
                IsNullable = false,
                StoreType = "int"
            };
            configuration.Columns.Add(identifierColumn);

            var nameColumn = new DatabaseColumn
            {
                Table = configuration,
                Name = "Name",
                IsNullable = true,
                StoreType = "varchar(50)",
                DefaultValueSql = @"/****** Object:  Default dbo.abc0    Script Date: 4/11/99 12:35:41 PM ******/
create default abc0 as 0
"
            };
            configuration.Columns.Add(nameColumn);
            var generator = new ModelGenerator(NullLoggerFactory.Instance);

            var result = generator.Generate(generatorOptions, databaseModel);
            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(1);

            var firstEntity = result.Entities[0];
            firstEntity.TableName.Should().Be("Configuration");
            firstEntity.TableSchema.Should().Be("dbo");
            firstEntity.EntityClass.Should().Be("Configuration");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.MappingClass.Should().Be("ConfigurationMap");
            firstEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");

            firstEntity.Properties.Count.Should().Be(2);

            var identifierProperty = firstEntity.Properties.ByColumn("Id");
            identifierProperty.Should().NotBeNull();
            identifierProperty.PropertyName.Should().Be("Id");

            var nameProperty = firstEntity.Properties.ByColumn("Name");
            nameProperty.Should().NotBeNull();
            nameProperty.PropertyName.Should().Be("Name");
        }

        [Fact]
        public void GenerateCheckNameCase()
        {
            var generatorOptions = new GeneratorOptions();
            var databaseModel = new DatabaseModel
            {
                DatabaseName = "TestDatabase",
                DefaultSchema = "dbo"
            };
            var configuration = new DatabaseTable
            {
                Database = databaseModel,
                Name = "aammstest",
                Schema = "dbo"
            };
            databaseModel.Tables.Add(configuration);

            var identifierColumn = new DatabaseColumn
            {
                Table = configuration,
                Name = "Id",
                IsNullable = false,
                StoreType = "int"
            };
            configuration.Columns.Add(identifierColumn);

            var nameColumn = new DatabaseColumn
            {
                Table = configuration,
                Name = "Name",
                IsNullable = true,
                StoreType = "varchar(50)"
            };
            configuration.Columns.Add(nameColumn);
            var generator = new ModelGenerator(NullLoggerFactory.Instance);

            var result = generator.Generate(generatorOptions, databaseModel);
            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(1);

            var firstEntity = result.Entities[0];
            firstEntity.TableName.Should().Be("aammstest");
            firstEntity.TableSchema.Should().Be("dbo");
            firstEntity.EntityClass.Should().Be("Aammstest");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.MappingClass.Should().Be("AammstestMap");
            firstEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");

            firstEntity.Properties.Count.Should().Be(2);

            var identifierProperty = firstEntity.Properties.ByColumn("Id");
            identifierProperty.Should().NotBeNull();
            identifierProperty.PropertyName.Should().Be("Id");

            var nameProperty = firstEntity.Properties.ByColumn("Name");
            nameProperty.Should().NotBeNull();
            nameProperty.PropertyName.Should().Be("Name");
        }

        [Fact]
        public void GenerateWithPrefixedSchemaName()
        {
            var generatorOptions = new GeneratorOptions();
            generatorOptions.Data.Entity.PrefixWithSchemaName = true;
            var databaseModel = new DatabaseModel
            {
                DatabaseName = "TestDatabase",
                DefaultSchema = "dbo"
            };
            var configurationDbo = new DatabaseTable
            {
                Database = databaseModel,
                Name = "Configuration",
                Schema = "dbo"
            };
            var configurationTst = new DatabaseTable
            {
                Database = databaseModel,
                Name = "Configuration",
                Schema = "tst"
            };
            databaseModel.Tables.Add(configurationDbo);
            databaseModel.Tables.Add(configurationTst);

            var identifierColumnDbo = new DatabaseColumn
            {
                Table = configurationDbo,
                Name = "Id",
                IsNullable = false,
                StoreType = "int"
            };
            var identifierColumnTst = new DatabaseColumn
            {
                Table = configurationTst,
                Name = "Id",
                IsNullable = false,
                StoreType = "int"
            };
            configurationDbo.Columns.Add(identifierColumnDbo);
            configurationDbo.Columns.Add(identifierColumnTst);

            var nameColumnDbo = new DatabaseColumn
            {
                Table = configurationDbo,
                Name = "Name",
                IsNullable = true,
                StoreType = "varchar(50)"
            };
            var nameColumnTst = new DatabaseColumn
            {
                Table = configurationTst,
                Name = "Name",
                IsNullable = true,
                StoreType = "varchar(50)"
            };
            configurationDbo.Columns.Add(nameColumnDbo);
            configurationDbo.Columns.Add(nameColumnTst);

            var generator = new ModelGenerator(NullLoggerFactory.Instance);

            var result = generator.Generate(generatorOptions, databaseModel);

            result.ContextClass.Should().Be("TestDatabaseContext");
            result.ContextNamespace.Should().Be("TestDatabase.Data");
            result.Entities.Count.Should().Be(2);

            var firstEntity = result.Entities[0];
            firstEntity.TableName.Should().Be("Configuration");
            firstEntity.TableSchema.Should().Be("dbo");
            firstEntity.EntityClass.Should().Be("DboConfiguration");
            firstEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            firstEntity.MappingClass.Should().Be("DboConfigurationMap");
            firstEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");

            var secondEntity = result.Entities[1];
            secondEntity.TableName.Should().Be("Configuration");
            secondEntity.TableSchema.Should().Be("tst");
            secondEntity.EntityClass.Should().Be("TstConfiguration");
            secondEntity.EntityNamespace.Should().Be("TestDatabase.Data.Entities");
            secondEntity.MappingClass.Should().Be("TstConfigurationMap");
            secondEntity.MappingNamespace.Should().Be("TestDatabase.Data.Mapping");
        }
    }
}
