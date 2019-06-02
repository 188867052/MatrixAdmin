using System.Linq;
using EntityFrameworkCore.Generator.Extensions;
using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;

namespace EntityFrameworkCore.Generator.Templates
{
    public class QueryExtensionTemplate : CodeTemplateBase
    {
        private readonly Entity _entity;

        public QueryExtensionTemplate(Entity entity, GeneratorOptions options) : base(options)
        {
            this._entity = entity;
        }

        public override string WriteCode()
        {
            this.CodeBuilder.Clear();

            this.CodeBuilder.AppendLine("using System;");
            this.CodeBuilder.AppendLine("using System.Collections.Generic;");
            this.CodeBuilder.AppendLine("using System.Linq;");
            this.CodeBuilder.AppendLine("using System.Threading.Tasks;");
            this.CodeBuilder.AppendLine("using Microsoft.EntityFrameworkCore;");
            this.CodeBuilder.AppendLine();

            var extensionNamespace = this.Options.Data.Query.Namespace;

            this.CodeBuilder.AppendLine($"namespace {extensionNamespace}");
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
            string safeName = this._entity.EntityNamespace + "." + entityClass;

            if (this.Options.Data.Query.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Query extensions for entity <see cref=\"{safeName}\" />.");
                this.CodeBuilder.AppendLine("/// </summary>");
            }

            this.CodeBuilder.AppendLine($"public static partial class {entityClass}Extensions");
            this.CodeBuilder.AppendLine("{");

            using (this.CodeBuilder.Indent())
            {
                this.GenerateMethods();
            }

            this.CodeBuilder.AppendLine("}");
        }

        private void GenerateMethods()
        {
            foreach (var method in this._entity.Methods.OrderBy(m => m.NameSuffix))
            {
                if (method.IsKey)
                {
                    this.GenerateKeyMethod(method);
                    this.GenerateKeyMethod(method, true);
                }
                else if (method.IsUnique)
                {
                    this.GenerateUniqueMethod(method);
                    this.GenerateUniqueMethod(method, true);
                }
                else
                {
                    this.GenerateMethod(method);
                }
            }

            this.CodeBuilder.AppendLine();
        }

        private void GenerateMethod(Method method)
        {
            string safeName = this._entity.EntityNamespace + "." + this._entity.EntityClass.ToSafeName();
            string prefix = this.Options.Data.Query.IndexPrefix;
            string suffix = method.NameSuffix;

            if (this.Options.Data.Query.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine("/// Filters a sequence of values based on a predicate.");
                this.CodeBuilder.AppendLine("/// </summary>");
                this.CodeBuilder.AppendLine("/// <param name=\"queryable\">An <see cref=\"T:System.Linq.IQueryable`1\" /> to filter.</param>");
                this.AppendDocumentation(method);
                this.CodeBuilder.AppendLine("/// <returns>An <see cref=\"T: System.Linq.IQueryable`1\" /> that contains elements from the input sequence that satisfy the condition specified.</returns>");
            }

            this.CodeBuilder.Append($"public static IQueryable<{safeName}> {prefix}{suffix}(this IQueryable<{safeName}> queryable, ");
            this.AppendParameters(method);
            this.CodeBuilder.AppendLine(")");
            this.CodeBuilder.AppendLine("{");

            using (this.CodeBuilder.Indent())
            {
                this.CodeBuilder.Append($"return queryable.Where(");
                this.AppendLamba(method);
                this.CodeBuilder.AppendLine(");");
            }

            this.CodeBuilder.AppendLine("}");
            this.CodeBuilder.AppendLine();
        }

        private void GenerateUniqueMethod(Method method, bool async = false)
        {
            string safeName = this._entity.EntityNamespace + "." + this._entity.EntityClass.ToSafeName();
            string uniquePrefix = this.Options.Data.Query.UniquePrefix;
            string suffix = method.NameSuffix;

            string asyncSuffix = async ? "Async" : string.Empty;
            string returnType = async ? $"Task<{safeName}>" : safeName;

            if (this.Options.Data.Query.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine($"/// Gets an instance of <see cref=\"T:{safeName}\"/> by using a unique index.");
                this.CodeBuilder.AppendLine("/// </summary>");
                this.CodeBuilder.AppendLine("/// <param name=\"queryable\">An <see cref=\"T:System.Linq.IQueryable`1\" /> to filter.</param>");
                this.AppendDocumentation(method);
                this.CodeBuilder.AppendLine($"/// <returns>An instance of <see cref=\"T:{safeName}\"/> or null if not found.</returns>");
            }

            this.CodeBuilder.Append($"public static {returnType} {uniquePrefix}{suffix}{asyncSuffix}(this IQueryable<{safeName}> queryable, ");
            this.AppendParameters(method);
            this.CodeBuilder.AppendLine(")");
            this.CodeBuilder.AppendLine("{");

            using (this.CodeBuilder.Indent())
            {
                this.CodeBuilder.Append($"return queryable.FirstOrDefault{asyncSuffix}(");
                this.AppendLamba(method);
                this.CodeBuilder.AppendLine(");");
            }

            this.CodeBuilder.AppendLine("}");
            this.CodeBuilder.AppendLine();
        }

        private void GenerateKeyMethod(Method method, bool async = false)
        {
            string safeName = this._entity.EntityNamespace + "." + this._entity.EntityClass.ToSafeName();
            string uniquePrefix = this.Options.Data.Query.UniquePrefix;

            string asyncSuffix = async ? "Async" : string.Empty;
            string returnType = async ? $"Task<{safeName}>" : safeName;

            if (this.Options.Data.Query.Document)
            {
                this.CodeBuilder.AppendLine("/// <summary>");
                this.CodeBuilder.AppendLine("/// Gets an instance by the primary key.");
                this.CodeBuilder.AppendLine("/// </summary>");
                this.CodeBuilder.AppendLine("/// <param name=\"queryable\">An <see cref=\"T:System.Linq.IQueryable`1\" /> to filter.</param>");
                this.AppendDocumentation(method);
                this.CodeBuilder.AppendLine($"/// <returns>An instance of <see cref=\"T:{safeName}\"/> or null if not found.</returns>");
            }

            this.CodeBuilder.Append($"public static {returnType} {uniquePrefix}Key{asyncSuffix}(this IQueryable<{safeName}> queryable, ");
            this.AppendParameters(method);
            this.CodeBuilder.AppendLine(")");
            this.CodeBuilder.AppendLine("{");

            using (this.CodeBuilder.Indent())
            {
                this.CodeBuilder.AppendLine($"if (queryable is DbSet<{safeName}> dbSet)");
                using (this.CodeBuilder.Indent())
                {
                    this.CodeBuilder.Append($"return dbSet.Find{asyncSuffix}(");
                    this.AppendNames(method);
                    this.CodeBuilder.AppendLine(");");
                }

                this.CodeBuilder.AppendLine(string.Empty);
                this.CodeBuilder.Append($"return queryable.FirstOrDefault{asyncSuffix}(");
                this.AppendLamba(method);
                this.CodeBuilder.AppendLine(");");
            }

            this.CodeBuilder.AppendLine("}");
            this.CodeBuilder.AppendLine();
        }

        private void AppendDocumentation(Method method)
        {
            foreach (var property in method.Properties)
            {
                string paramName = property.PropertyName
                    .ToCamelCase()
                    .ToSafeName();

                this.CodeBuilder.AppendLine($"/// <param name=\"{paramName}\">The value to filter by.</param>");
            }
        }

        private void AppendParameters(Method method)
        {
            bool wrote = false;

            foreach (var property in method.Properties)
            {
                if (wrote)
                {
                    this.CodeBuilder.Append(", ");
                }

                string paramName = property.PropertyName
                    .ToCamelCase()
                    .ToSafeName();

                string paramType = property.SystemType
                    .ToNullableType(property.IsNullable == true);

                this.CodeBuilder.Append($"{paramType} {paramName}");

                wrote = true;
            }
        }

        private void AppendNames(Method method)
        {
            bool wrote = false;
            foreach (var property in method.Properties)
            {
                if (wrote)
                {
                    this.CodeBuilder.Append(", ");
                }

                string paramName = property.PropertyName
                    .ToCamelCase()
                    .ToSafeName();

                this.CodeBuilder.Append(paramName);
                wrote = true;
            }
        }

        private void AppendLamba(Method method)
        {
            bool wrote = false;
            bool indented = false;

            foreach (var property in method.Properties)
            {
                string paramName = property.PropertyName
                    .ToCamelCase()
                    .ToSafeName();

                if (!wrote)
                {
                    this.CodeBuilder.Append("q => ");
                }
                else
                {
                    this.CodeBuilder.AppendLine();
                    this.CodeBuilder.IncrementIndent();
                    this.CodeBuilder.Append("&& ");

                    indented = true;
                }

                if (property.IsNullable == true)
                {
                    this.CodeBuilder.Append($"(q.{property.PropertyName} == {paramName} || ({paramName} == null && q.{property.PropertyName} == null))");
                }
                else
                {
                    this.CodeBuilder.Append($"q.{property.PropertyName} == {paramName}");
                }

                wrote = true;
            }

            if (indented)
            {
                this.CodeBuilder.DecrementIndent();
            }
        }
    }
}
