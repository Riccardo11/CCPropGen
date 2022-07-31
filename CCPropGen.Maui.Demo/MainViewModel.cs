using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCPropGen.Maui.Demo
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        string entryText;

        [ObservableProperty]
        int maxLength;

        partial void OnEntryTextChanged(string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
        }
    }
}
