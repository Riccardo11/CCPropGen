using CCPropGen.Constants;
using CCPropGen.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace CCPropGen.Core.SyntaxReceiver
{
    internal class CCPropSyntaxReceiver : ISyntaxReceiver
    {
        public ClassDeclarationSyntax ControlClassSyntax { get; private set; }
        public AttributeSyntax AttributeSyntax { get; private set; }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax)
            {
                foreach (var attributes in classDeclarationSyntax.AttributeLists)
                {
                    if (attributes != null
                        && attributes.Attributes != null)
                    {
                        foreach (var attribute in attributes.Attributes)
                        {

                            var name = attribute.Name.GetText().ToString();
                            Debug.WriteLine(name);
                            if (attribute.Name.GetText().ToString() == AttributeConstants.ATTRIBUTE_NAME)
                            {
                                ControlClassSyntax = classDeclarationSyntax;
                                return;
                            }
                        }
                    }
                }
            }
            else if (syntaxNode is AttributeSyntax attributeSyntax)
            {
                if (attributeSyntax.Name.GetText().ToString() == AttributeConstants.ATTRIBUTE_NAME)
                {
                    AttributeSyntax = attributeSyntax;
                }
            }
        }
    }
}
