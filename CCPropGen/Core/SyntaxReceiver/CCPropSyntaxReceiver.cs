using CCPropGen.Constants;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Diagnostics;

namespace CCPropGen.Core.SyntaxReceiver
{
    internal class CCPropSyntaxReceiver : ISyntaxReceiver
    {
        public Dictionary<ClassDeclarationSyntax, List<AttributeSyntax>> ControlClassSyntaxesWithAttributes { get; }

        public CCPropSyntaxReceiver()
        {
            ControlClassSyntaxesWithAttributes = new();
        }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not ClassDeclarationSyntax classDeclarationSyntax)
            {
                return;
            }

            foreach (var attributes in classDeclarationSyntax.AttributeLists)
            {
                if (attributes == null
                    || attributes.Attributes == null)
                {
                    continue;
                }

                foreach (var attribute in attributes.Attributes)
                {
                    if (attribute.Name.GetText().ToString() != AttributeConstants.ATTRIBUTE_NAME)
                    {
                        continue;
                    }

                    if (ControlClassSyntaxesWithAttributes.ContainsKey(classDeclarationSyntax))
                    {
                        ControlClassSyntaxesWithAttributes[classDeclarationSyntax].Add(attribute);
                    }
                    else
                    {
                        ControlClassSyntaxesWithAttributes[classDeclarationSyntax] = new() { attribute };
                    }
                }
            }
        }
    }
}
