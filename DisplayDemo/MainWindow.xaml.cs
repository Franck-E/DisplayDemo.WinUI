// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using DisplayExtensions;
using Microsoft.UI.Xaml;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DisplayDemo;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public List<DisplayModel> Displays { get; set; }

    public MainWindow()
    {
        this.InitializeComponent();
        Displays = new List<DisplayModel>();
        Displays = Gets.GetDisplays();

        foreach (var item in Displays)
        {
            systemTBox.Text += $"Display ->" +
                $"\n  Device Name: {item.DeviceName}" +
                $"\n  Friendly Name: {item.FriendlyName}" +
                $"\n  Device UID: {item.UID}" +
                $"\n  Height: {item.Height}" +
                $"\n  Width:{item.Width}" +
                $"\n  Effective Pixels: width({item.EffectiveWidth}), height({item.EffectiveHeight})" +
                $"\n  Primary: {item.Primary}" +
                $"\n  Orientation: {item.Orientation}" +
                $"\n  Frequency: {item.Frequency}" +
                $"\n  IsAttached: {item.IsAttached}" +
                $"\n  Work Area: ({item.WorkArea.top},{item.WorkArea.left},{item.WorkArea.bottom},{item.WorkArea.right})" +
                $"\n  HW {item.HMonitor}\n\n";
        }
    }

}
