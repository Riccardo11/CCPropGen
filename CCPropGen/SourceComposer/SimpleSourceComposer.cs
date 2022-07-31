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
        public CCPropGenAttributeValues AttributeValues { get; set; }

        public string BuildSource()
        {
            if (string.IsNullOrEmpty(Namespace)
                || string.IsNullOrEmpty(ClassName)
                || AttributeValues == null)
            {
                throw new ArgumentNullException($"{nameof(Namespace)}, {nameof(ClassName)} and {nameof(AttributeValues)} must not be null");
            }

            var source =
            $@"namespace {Namespace}
{{
    public partial class {ClassName}
    {{
        protected void InitializeCCPropGen()
        {{
            {AttributeValues.ControlName}.BindingContext = this;
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

            for (int i = 0; i < AttributeValues.PropertyNames.Length; i++)
            {
                var propertyName = AttributeValues.PropertyNames[i];
                var bindablePropertyName = $"{propertyName}Property";

                sb.Append($@"
            {AttributeValues.ControlName}.SetBinding({AttributeValues.ControlType}.{bindablePropertyName}, ""{propertyName}"");");
            }

            return sb.ToString();
        }

        private string GetPropertiesString()
        {
            var propertyNames = AttributeValues.PropertyNames;
            var propertyTypes = AttributeValues.PropertyTypes;

            if (propertyNames.Length != propertyTypes.Length)
            {
                throw new ArgumentException("Property names and property types must have the same length.");
            }

            var sb = new StringBuilder();

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
            {SourceGeneratorUtils.GetBindablePropertyArguments(AttributeValues.ControlType, bindablePropertyName, ClassName)}
            );
        
        ");   
            }

            return sb.ToString();
        }
    }
}
