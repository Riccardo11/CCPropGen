using Maui.CCPropGen;

namespace CCPropGen.Maui.Demo;

[CCPropGen(
    "EntryControl",
    typeof(Entry),
    new string[] { "Text", "MaxLength", "Placeholder" },
    new Type[] { typeof(string), typeof(int), typeof(string) })]
public partial class MultipleValuesEntry : ContentView
{
    public MultipleValuesEntry()
    {
        InitializeComponent();
        InitializeCCPropGen();
    }
}