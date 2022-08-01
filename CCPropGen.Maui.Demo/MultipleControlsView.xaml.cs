using Maui.CCPropGen;

namespace CCPropGen.Maui.Demo;

[CCPropGen("ControlEntry", typeof(Entry), "Text", typeof(string))]
[CCPropGen("ControlLabel", typeof(Label), "FontSize", typeof(double))]
public partial class MultipleControlsView : ContentView
{
	public MultipleControlsView()
	{
		InitializeComponent();
		InitializeCCPropGen();
	}
}