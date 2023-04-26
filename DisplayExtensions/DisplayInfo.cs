using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace DisplayExtensions;

public partial class DisplayInfo : ObservableObject
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
    private PInvoke.RECT workArea;

    [ObservableProperty]
    private IntPtr hMonitor;
}
