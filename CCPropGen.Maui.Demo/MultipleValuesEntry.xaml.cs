using Maui.CCPropGen;

namespace CCPropGen.Maui.Demo;

//[CCPropGen(
//    "EntryControl",
//    typeof(Entry),
//    new string[] { "Text", "MaxLength" },
//    new Type[] { typeof(string), typeof(int) })]
public partial class MultipleValuesEntry : ContentView
{
    public MultipleValuesEntry()
    {
        InitializeComponent();
        // InitializeCCPropGen();
    }
}