using System.Collections.Generic;
using System.Linq;
using EntityFrameworkCore.Generator.Core.Metadata.Parsing;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EntityFrameworkCore.Generator.Core.Parsing
{
    public class ContextVisitor : CSharpSyntaxWalker
    {
        private string _currentClass;

        public ContextVisitor()
        {
            this.DataSetTypes = new HashSet<string> { "DbSet", "IDbSet" };
        }

        public HashSet<string> DataSetTypes { get; set; }

        public ParsedContext ParsedContext { get; set; }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var hasBaseType = node.BaseList != null && node.BaseList
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .Any();

            if (hasBaseType)
            {
                this._currentClass = node.Identifier.Text;
            }

            base.VisitClassDeclaration(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            this.ParseProperty(node);
            base.VisitPropertyDeclaration(node);
        }

        private void ParseProperty(PropertyDeclarationSyntax node)
        {
            var returnType = node.Type
                .DescendantNodesAndSelf()
                .OfType<GenericNameSyntax>()
                .FirstOrDefault();

            // expecting generic return type with 1 argument
            if (returnType == null || returnType.TypeArgumentList.Arguments.Count != 1)
            {
                return;
            }

            var returnName = returnType.Identifier.ValueText;
            if (!this.DataSetTypes.Contains(returnName))
            {
                return;
            }

            var firstArgument = returnType
                .TypeArgumentList
                .Arguments
                .FirstOrDefault();

            // last identifier is class name
            var className = firstArgument
                .DescendantNodesAndSelf()
                .OfType<IdentifierNameSyntax>()
                .Select(s => s.Identifier.ValueText)
                .LastOrDefault();

            var propertyName = node.Identifier.ValueText;

            if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            if (this.ParsedContext == null)
            {
                this.ParsedContext = new ParsedContext { ContextClass = this._currentClass };
            }

            var entitySet = new ParsedEntitySet
            {
                EntityClass = className,
                ContextProperty = propertyName
            };

            this.ParsedContext.Properties.Add(entitySet);
        }
    }
}