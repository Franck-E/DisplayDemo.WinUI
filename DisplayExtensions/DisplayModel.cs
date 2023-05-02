using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Drawing;

namespace DisplayExtensions;

public partial class DisplayModel : ObservableObject
{
    [ObservableProperty]
    private int? id;

    [ObservableProperty]
    private string deviceName;

    [ObservableProperty]
    private string friendlyName;

    [ObservableProperty]
    private string uID;

    [ObservableProperty]
    private int height;

    [ObservableProperty]
    private int width;

    [ObservableProperty]
    private int effectiveHeight;

    [ObservableProperty]
    private int effectiveWidth;

    [ObservableProperty]
    private bool primary;

    [ObservableProperty]
    private string orientation;

    [ObservableProperty]
    private string frequency;

    [ObservableProperty]
    private bool isAttached;

    [ObservableProperty]
    private Rectangle workArea;

    [ObservableProperty]
    private IntPtr hMonitor;

    [ObservableProperty]
    private BitmapImage image;
}
