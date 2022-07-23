using System;
using System.Collections.Generic;
using System.Text;

namespace CCPropGen.Helpers
{
    internal class CCPropGenAttributeValues
    {
        public string ControlName { get; set; }
        public string PropertyName { get; set; }
        public string ControlType { get; set; }
        public string PropertyType { get; set; }
        public bool IsBindable { get; set; }
        public string BindablePropertyName => $"{PropertyName}Property";

        public void SetPropertyValue(string propertyName, object value)
        {
            switch (propertyName)
            {
                case nameof(IsBindable):
                    IsBindable = (bool)value;
                    break;
            }
        }
    }
}
