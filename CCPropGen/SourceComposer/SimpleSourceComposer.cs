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
            {AttributeValues.ControlName}.SetBinding({AttributeValues.ControlType}.{AttributeValues.BindablePropertyName}, ""{AttributeValues.PropertyName}"");
        }}

        public {AttributeValues.PropertyType} {AttributeValues.PropertyName}
        {{
            get => ({AttributeValues.PropertyType})GetValue({AttributeValues.BindablePropertyName});
            set => SetValue({AttributeValues.BindablePropertyName}, value);
        }}

        public static readonly BindableProperty {AttributeValues.BindablePropertyName} = BindableProperty.Create(
            {SourceGeneratorUtils.GetBindablePropertyArguments(AttributeValues, ClassName)}
            );
    }}
}}";
            return source;
        }
    }
}
