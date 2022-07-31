using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCPropGen.Helpers
{
    internal class CCPropGenAttributeValues
    {
        public string ControlName { get; set; }
        public string ControlType { get; set; }
        public string[] PropertyNames { get; set; }
        public string[] PropertyTypes { get; set; }
        public bool IsBindable { get; set; }
        public string[] BindablePropertyNames
        {
            get
            {
                return PropertyNames
                    .Select(propName => $"{propName}Property")
                    .ToArray();
            }
        }

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
