// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using DisplayExtensions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage.Streams;

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
        InitializeComponent();
        Displays = new List<DisplayModel>();
        Displays = Gets.GetDisplays();
        Start();
    }

    public async void Start()
    {
        int i = 0;
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
                $"\n  Work Area: ({item.WorkArea.X},{item.WorkArea.Y},{item.WorkArea.Width},{item.WorkArea.Height})" +
                $"\n  HW {item.HMonitor}\n\n";

            if (i == 0)
                one.Source = await ScreenshotCapture.GetScreenshot(item);

            else if (i == 1)
                two.Source = await ScreenshotCapture.GetScreenshot(item);

            else continue;

            i++;
        }
    }




}
