using CCPropGen.Constants;
using CCPropGen.Helpers;
using CCPropGen.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCPropGen.SourceComposer
{
    internal class SimpleSourceComposer
    {
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public List<CCPropGenAttributeValues> AttributeValuesList { get; set; }

        public string BuildSource()
        {
            if (string.IsNullOrEmpty(Namespace)
                || string.IsNullOrEmpty(ClassName)
                || AttributeValuesList == null
                || !AttributeValuesList.Any())
            {
                throw new ArgumentNullException($"{nameof(Namespace)}, {nameof(ClassName)} and {nameof(AttributeValuesList)} must not be null");
            }

            var source =
            $@"namespace {Namespace}
{{
    public partial class {ClassName}
    {{
        protected void InitializeCCPropGen()
        {{
            {GetInitializePropertiesBody()}
        }}

        {GetPropertiesString()}
    }}
}}";
            return source;

            //public {AttributeValues.PropertyType} {AttributeValues.PropertyName}
            //{{
            //    get => ({AttributeValues.PropertyType})GetValue({AttributeValues.BindablePropertyNames});
            //    set => SetValue({AttributeValues.BindablePropertyNames}, value);
            //}}

            //public static readonly BindableProperty {AttributeValues.BindablePropertyNames} = BindableProperty.Create(
            //    {SourceGeneratorUtils.GetBindablePropertyArguments(AttributeValues, ClassName)}
            //    );
        }

        private string GetInitializePropertiesBody()
        {
            var sb = new StringBuilder();

            foreach (var attributeValues in AttributeValuesList)
            {
                sb.Append($@"{attributeValues.ControlName}.BindingContext = this;");

                for (int i = 0; i < attributeValues.PropertyNames.Length; i++)
                {
                    var propertyName = attributeValues.PropertyNames[i];
                    var bindablePropertyName = $"{propertyName}Property";

                    sb.Append($@"
            {attributeValues.ControlName}.SetBinding({attributeValues.ControlType}.{bindablePropertyName}, ""{propertyName}"");");
                }

                sb.Append(@"
            ");
            }

            return sb.ToString();
        }

        private string GetPropertiesString()
        {
            var sb = new StringBuilder();

            foreach (var attributeValues in AttributeValuesList)
            {
                var propertyNames = attributeValues.PropertyNames;
                var propertyTypes = attributeValues.PropertyTypes;

                if (propertyNames.Length != propertyTypes.Length)
                {
                    throw new ArgumentException("Property names and property types must have the same length.");
                }


                for (int i = 0; i < propertyNames.Length; i++)
                {
                    var propertyName = propertyNames[i];
                    var propertyType = propertyTypes[i];
                    var bindablePropertyName = $"{propertyName}Property";

                    sb.Append($@"public {propertyType} {propertyName}
        {{
            get => ({propertyType})GetValue({bindablePropertyName});
            set => SetValue({bindablePropertyName}, value);
        }}

        public static readonly BindableProperty {bindablePropertyName} = BindableProperty.Create(
            {SourceGeneratorUtils.GetBindablePropertyArguments(attributeValues.ControlType, bindablePropertyName, ClassName)}
            );
        
        ");
                }
            }

            return sb.ToString();
        }
    }
}
