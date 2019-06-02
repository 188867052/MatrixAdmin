using System.Linq;
using EntityFrameworkCore.Generator.Metadata.Parsing;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EntityFrameworkCore.Generator.Parsing
{
    public class MappingVisitor : CSharpSyntaxWalker
    {
        private ParsedProperty _currentProperty;
        private ParsedRelationship _currentRelationship;

        public MappingVisitor()
        {
            this.MappingBaseType = "IEntityTypeConfiguration";
        }

        public string MappingBaseType { get; set; }

        public ParsedEntity ParsedEntity { get; set; }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            this.ParseClassNames(node);
            base.VisitClassDeclaration(node);
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var methodName = this.ParseMethodName(node);

            switch (methodName)
            {
                case "HasForeignKey":
                    this.ParseForeignKey(node);
                    break;
                case "WithMany":
                case "WithOne":
                    this.ParseWithMany(node);
                    break;
                case "HasMany":
                case "HasOne":
                    this.ParseHasOne(node);
                    break;
                case "HasColumnName":
                    this.ParseColumnName(node);
                    break;
                case "Property":
                    this.ParseProperty(node);
                    break;
                case "ToTable":
                    this.ParseTable(node);
                    break;
            }

            base.VisitInvocationExpression(node);
        }

        private string ParseMethodName(InvocationExpressionSyntax node)
        {
            var memberAccess = node
                .ChildNodes()
                .OfType<MemberAccessExpressionSyntax>()
                .FirstOrDefault();

            if (memberAccess == null)
            {
                return string.Empty;
            }

            var methodName = memberAccess
                .ChildNodes()
                .OfType<IdentifierNameSyntax>()
                .Select(s => s.Identifier.ValueText)
                .LastOrDefault();

            return methodName ?? string.Empty;
        }

        private string ParseLambaExpression(InvocationExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }

            var lambaExpression = node
                .ArgumentList
                .DescendantNodes()
                .OfType<LambdaExpressionSyntax>()
                .FirstOrDefault();

            if (lambaExpression == null)
            {
                return null;
            }

            var simpleExpression = lambaExpression
                .ChildNodes()
                .OfType<MemberAccessExpressionSyntax>()
                .FirstOrDefault();

            if (simpleExpression == null)
            {
                return null;
            }

            var propertyName = simpleExpression
                .ChildNodes()
                .OfType<IdentifierNameSyntax>()
                .Select(s => s.Identifier.ValueText)
                .LastOrDefault();

            return propertyName;
        }

        private void ParseHasOne(InvocationExpressionSyntax node)
        {
            if (node == null || this.ParsedEntity == null || this._currentRelationship == null)
            {
                return;
            }

            var propertyName = this.ParseLambaExpression(node);
            if (!string.IsNullOrEmpty(propertyName))
            {
                this._currentRelationship.ThisPropertyName = propertyName;
            }

            // add and reset current relationship
            if (this._currentRelationship.IsValid())
            {
                this.ParsedEntity.Relationships.Add(this._currentRelationship);
            }

            this._currentRelationship = null;
        }

        private void ParseWithMany(InvocationExpressionSyntax node)
        {
            if (node == null || this.ParsedEntity == null || this._currentRelationship == null)
            {
                return;
            }

            var propertyName = this.ParseLambaExpression(node);
            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            this._currentRelationship.OtherPropertyName = propertyName;
        }

        private void ParseForeignKey(InvocationExpressionSyntax node)
        {
            if (node == null || this.ParsedEntity == null)
            {
                return;
            }

            var propertyName = this.ParseLambaExpression(node);

            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            // start new relationship
            this._currentRelationship = new ParsedRelationship();
            this._currentRelationship.ThisProperties.Add(propertyName);
        }

        private void ParseProperty(InvocationExpressionSyntax node)
        {
            if (node == null || this._currentProperty == null)
            {
                return;
            }

            var propertyName = this.ParseLambaExpression(node);
            if (!string.IsNullOrEmpty(propertyName))
            {
                this._currentProperty.PropertyName = propertyName;
            }

            // add and reset current property
            if (this._currentProperty.IsValid())
            {
                this.ParsedEntity.Properties.Add(this._currentProperty);
            }

            this._currentProperty = null;
        }

        private void ParseColumnName(InvocationExpressionSyntax node)
        {
            if (node == null || this.ParsedEntity == null)
            {
                return;
            }

            var columnName = node
                .ArgumentList
                .DescendantNodes()
                .OfType<LiteralExpressionSyntax>()
                .Select(t => t.Token.ValueText)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(columnName))
            {
                return;
            }

            this._currentProperty = new ParsedProperty { ColumnName = columnName };
        }

        private void ParseTable(InvocationExpressionSyntax node)
        {
            if (node == null || this.ParsedEntity == null)
            {
                return;
            }

            var arguments = node
                .ArgumentList
                .DescendantNodes()
                .OfType<LiteralExpressionSyntax>()
                .Select(t => t.Token.ValueText)
                .ToList();

            if (arguments.Count == 0)
            {
                return;
            }

            if (arguments.Count >= 1)
            {
                this.ParsedEntity.TableName = arguments[0];
            }

            if (arguments.Count >= 2)
            {
                this.ParsedEntity.TableSchema = arguments[1];
            }
        }

        private void ParseClassNames(ClassDeclarationSyntax node)
        {
            if (node == null)
            {
                return;
            }

            var baseType = node.BaseList
                .DescendantNodes()
                .OfType<GenericNameSyntax>()
                .FirstOrDefault(t => t.Identifier.ValueText == this.MappingBaseType);

            if (baseType == null)
            {
                return;
            }

            var firstArgument = baseType
                .TypeArgumentList
                .Arguments
                .FirstOrDefault();

            // last identifier is class name
            var entityClass = firstArgument
                .DescendantNodesAndSelf()
                .OfType<IdentifierNameSyntax>()
                .Select(s => s.Identifier.ValueText)
                .LastOrDefault();

            var mappingClass = node.Identifier.Text;

            if (string.IsNullOrEmpty(entityClass) || string.IsNullOrEmpty(mappingClass))
            {
                return;
            }

            if (this.ParsedEntity == null)
            {
                this.ParsedEntity = new ParsedEntity();
            }

            this.ParsedEntity.MappingClass = mappingClass;
            this.ParsedEntity.EntityClass = entityClass;
        }
    }
}