using CCPropGen.Constants;
using CCPropGen.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCPropGen.Utils
{
    internal static class SourceGeneratorUtils
    {
        public static CCPropGenAttributeValues GetAttributeValues(Compilation compilation, AttributeSyntax attributeSyntax)
        {
            var attributeValues = new CCPropGenAttributeValues();

            var semanticModel = compilation.GetSemanticModel(attributeSyntax.SyntaxTree);

            for (int i = 0; i < attributeSyntax.ArgumentList.Arguments.Count; i++)
            {
                var argument = attributeSyntax.ArgumentList.Arguments[i];

                if (i == 0)
                {
                    attributeValues.ControlName = semanticModel.GetConstantValue(argument.Expression).ToString();
                }

                if (i == 1)
                {
                    var typeofOperation = semanticModel.GetOperation(argument.Expression) as ITypeOfOperation;
                    attributeValues.ControlType = GetFullyQualifiedTypeAsString(typeofOperation.TypeOperand);
                }

                if (i == 2)
                {
                    attributeValues.PropertyName = semanticModel.GetConstantValue(argument.Expression).ToString();
                }

                if (i == 3)
                {
                    var typeofOperation = semanticModel.GetOperation(argument.Expression) as ITypeOfOperation;
                    attributeValues.PropertyType = GetFullyQualifiedTypeAsString(typeofOperation.TypeOperand);
                }

                if (i > 3)
                {
                    var assignment = semanticModel.GetOperation(argument) as ISimpleAssignmentOperation;
                    var field = assignment.Target as IFieldReferenceOperation;
                    var propertyName = field.Field.Name.ToString();

                    var value = assignment.Value.ConstantValue.Value;

                    attributeValues.SetPropertyValue(propertyName, value);
                }
            }

            return attributeValues;
        }

        // Needed for those types like string, int, etc.
        public static string GetFullyQualifiedTypeAsString(ITypeSymbol typeSymbol)
        {
            return $"{typeSymbol.ContainingNamespace}.{typeSymbol.Name}";
        }

        public static string GetBindablePropertyArguments(CCPropGenAttributeValues attributeValues, string className)
        {
            return GeneratedClassConstants.BINDABLE_PROPERTY_PARAMETERS
                .Select(par =>
                {
                    if (par == "DeclaringType")
                    {
                        return $"typeof({className})";
                    }
                    else
                    {
                        return attributeValues.ControlType + "." + attributeValues.BindablePropertyName + "." + par;
                    }
                })
                .Aggregate((s1, s2) => $"{s1},\n            {s2}");
        }

    }
}
