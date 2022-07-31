using CCPropGen.Constants;
using CCPropGen.Core.SyntaxReceiver;
using CCPropGen.SourceComposer;
using CCPropGen.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CCPropGen.Core.Generator
{
    [Generator]
    internal class PropGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var syntaxReceiver = (CCPropSyntaxReceiver)context.SyntaxReceiver;

            var userClass = syntaxReceiver.ControlClassSyntax;
            if (userClass is null)
            {
                return;
            }

            var @namespace = context
                .Compilation
                .GetSemanticModel(userClass.SyntaxTree)
                .GetDeclaredSymbol(userClass)
                .ContainingNamespace;

            if (@namespace is null)
            {
                return;
            }

            var attributeValues = SourceGeneratorUtils.GetAttributeValues(context.Compilation, syntaxReceiver.AttributeSyntax);

            var generatedClass = new SimpleSourceComposer
            {
                Namespace = @namespace.ToString(),
                ClassName = userClass.Identifier.Text.ToString(),
                AttributeValues = attributeValues
            }.BuildSource();

            context.AddSource($"{userClass.Identifier.Text}", generatedClass);

            //context.AddSource($"{userClass.Identifier.Text}.g.cs", generatedClass);
            Debug.WriteLine(generatedClass);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForPostInitialization(postInitializationContext => postInitializationContext.AddSource(
                AttributeConstants.ATTRIBUTE_FULL_NAME + ".g.cs",
                AttributeConstants.ATTRIBUTE_TEXT));

            context.RegisterForSyntaxNotifications(() => new CCPropSyntaxReceiver());
        }
    }
}
