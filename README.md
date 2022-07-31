# CCPropGen
A .NET MAUI plugin that helps to create bindable properties for your custom controls.

## Overview
When you create a custom control with the [`ContentView` method](https://github.com/jsuarezruiz/ways-create-netmaui-controls#3-using-contentview), you may be interested
in some bindable properties belonging to controls that are now hidden by the `ContentView` container.

CCPropGen allows you to create such properties with minimal effort.

## Example
Let's consider our simple custom control, which is an `Entry` inside a `ContentView`.

`CCPropGenEntry.xaml`

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentView xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CCPropGen.Maui.Demo.CCPropGenEntry">
    <Entry x:Name="EntryControl" />
</ContentView>
```

Let's assume we want to bind the `Text` property of `EntryControl`. Using CCPropGen we can easily create this property inside our custom control using an attribute.

`CCPropGenEntry.xaml.cs`

```csharp
using Maui.CCPropGen;

namespace CCPropGen.Maui.Demo;

// Attribute parameters:
// - Control name
// - Control type
// - Property name
// - Property type
[CCPropGen("EntryControl", typeof(Entry), "Text", typeof(string))]
public partial class CCPropGenEntry : ContentView
{
    public CCPropGenEntry()
    {
        InitializeComponent();
        
        // Currently needed to set binding contexts correctly
        InitializeCCPropGen();
    }
}
```

Now our custom control has a `Text`  property ready to be bound, that will be linked directly to the `Text` property of the `Entry`.

## Generating multiple properties

We may want to create more than one bindable property for our `Entry` control. Let's bind `MaxLength` property, too.

The attribute can accept as arguments also two arrays containing properties names and properties types.

```csharp
using Maui.CCPropGen;

namespace CCPropGen.Maui.Demo;

[CCPropGen(
    "EntryControl",
    typeof(Entry),
    new string[] { "Text", "MaxLength" },
    new Type[] { typeof(string), typeof(int) })] 
public partial class CCPropGenEntry : ContentView
{
	public CCPropGenEntry()
	{
		InitializeComponent();
		InitializeCCPropGen();
	}
}
```