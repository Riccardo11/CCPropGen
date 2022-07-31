namespace CCPropGen.Constants
{
    internal static class AttributeConstants
    {
        internal const string ATTRIBUTE_NAME = "CCPropGen";
        internal const string ATTRIBUTE_FULL_NAME = "CCPropGenAttribute";

        internal const string ATTRIBUTE_TEXT = @"using System;

namespace Maui.CCPropGen;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class CCPropGenAttribute : Attribute
{
    private string _control;
    private Type _type;
    private string[] _properties;
    private Type[] _propertyTypes;
    public bool IsBindable;
                
    public CCPropGenAttribute(string control, Type type, string property, Type propertyType)
    {
        _control = control;
        _type = type;
        _properties = new string[] { property };
        _propertyTypes = new Type[] { propertyType };
    }

    public CCPropGenAttribute(string control, Type type, string[] properties, Type[] propertyTypes)
    {
        _control = control;
        _type = type;
        _properties = properties;
        _propertyTypes = propertyTypes;
    }
}
";
    }
}
