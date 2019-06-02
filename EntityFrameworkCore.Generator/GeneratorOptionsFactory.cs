using System;
using EntityFrameworkCore.Generator.Options;
using YamlDotNet.Serialization;

namespace EntityFrameworkCore.Generator
{
    public class GeneratorOptionsFactory : IObjectFactory
    {
        private readonly GeneratorOptions _generatorOptions;

        public GeneratorOptionsFactory()
        {
            this._generatorOptions = new GeneratorOptions();
            this._generatorOptions.Variables.ShouldEvaluate = false;
        }

        public object Create(Type type)
        {
            // work around YamlDotNet requiring parameterless constructor
            if (type == typeof(GeneratorOptions))
            {
                return this._generatorOptions;
            }

            if (type == typeof(ProjectOptions))
            {
                return this._generatorOptions.Project;
            }

            if (type == typeof(DatabaseOptions))
            {
                return this._generatorOptions.Database;
            }

            if (type == typeof(DataOptions))
            {
                return this._generatorOptions.Data;
            }

            if (type == typeof(ModelOptions))
            {
                return this._generatorOptions.Model;
            }

            if (type == typeof(ContextClassOptions))
            {
                return this._generatorOptions.Data.Context;
            }

            if (type == typeof(EntityClassOptions))
            {
                return this._generatorOptions.Data.Entity;
            }

            if (type == typeof(MappingClassOptions))
            {
                return this._generatorOptions.Data.Mapping;
            }

            if (type == typeof(QueryExtensionOptions))
            {
                return this._generatorOptions.Data.Query;
            }

            if (type == typeof(SharedModelOptions))
            {
                return this._generatorOptions.Model.Shared;
            }

            if (type == typeof(ReadModelOptions))
            {
                return this._generatorOptions.Model.Read;
            }

            if (type == typeof(CreateModelOptions))
            {
                return this._generatorOptions.Model.Create;
            }

            if (type == typeof(UpdateModelOptions))
            {
                return this._generatorOptions.Model.Update;
            }

            if (type == typeof(MapperClassOptions))
            {
                return this._generatorOptions.Model.Mapper;
            }

            if (type == typeof(ValidatorClassOptions))
            {
                return this._generatorOptions.Model.Validator;
            }

            return Activator.CreateInstance(type);
        }
    }
}