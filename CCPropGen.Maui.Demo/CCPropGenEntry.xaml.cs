using Maui.CCPropGen;

namespace CCPropGen.Maui.Demo;

[CCPropGen(
	"EntryControl",
	typeof(Entry),
    "Text",
	typeof(string))] 
public partial class CCPropGenEntry : ContentView
{
	public CCPropGenEntry()
	{
		InitializeComponent();
		InitializeCCPropGen();
	}
}
