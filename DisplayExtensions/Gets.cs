using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using static DisplayExtensions.DevMode;

namespace DisplayExtensions;

public class Gets
{
    [DllImport("user32.dll")]
    static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, EnumMonitorsDelegate lpfnEnum, IntPtr dwData);
    delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref PInvoke.RECT lprcMonitor, IntPtr dwData);

    public static List<DisplayModel> GetDisplays()
    {
        List<DisplayModel> col = new();

        _ = EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
            delegate (IntPtr hMonitor, IntPtr hdcMonitor, ref PInvoke.RECT lprcMonitor, IntPtr dwData)
            {
                PInvoke.User32.MONITORINFOEX mi = new PInvoke.User32.MONITORINFOEX();
                mi.cbSize = Marshal.SizeOf(mi);
                bool success = PInvoke.User32.GetMonitorInfo(hMonitor, out mi);
                if (success)
                {
                    DisplayModel di = ConvertMonitorInfoToDisplayInfo(hMonitor, mi);
                    col.Add(di);
                }
                return true;
            }, IntPtr.Zero);
        return col;
    }

    public static DisplayModel GetDisplay(IntPtr hwnd)
    {
        DisplayModel di = null;
        IntPtr hMonitor;
        PInvoke.RECT rc;
        PInvoke.User32.GetWindowRect(hwnd, out rc);
        hMonitor = PInvoke.User32.MonitorFromRect(ref rc, PInvoke.User32.MonitorOptions.MONITOR_DEFAULTTONEAREST);

        PInvoke.User32.MONITORINFOEX mi = new PInvoke.User32.MONITORINFOEX();
        mi.cbSize = Marshal.SizeOf(mi);
        bool success = PInvoke.User32.GetMonitorInfo(hMonitor, out mi);
        if (success)
        {
            di = ConvertMonitorInfoToDisplayInfo(hMonitor, mi);
        }
        return di;
    }

    private unsafe static DisplayModel ConvertMonitorInfoToDisplayInfo(IntPtr hMonitor, PInvoke.User32.MONITORINFOEX mi)
    {
        return new DisplayModel
        {            
            Height = mi.Monitor.bottom - mi.Monitor.top,
            Width = mi.Monitor.right - mi.Monitor.left,
            DeviceName = new string(mi.DeviceName),
            FriendlyName = Regex.Replace(new string(mi.DeviceName), @"[^A-Za+Z0-9 ]", ""),
            UID = GetUID(new string(mi.DeviceName)),
            Primary = mi.Flags.ToString().Contains("PRIMARY"),
            EffectiveHeight = GetEffectiveHeight(hMonitor, mi.Monitor.bottom - mi.Monitor.top),
            EffectiveWidth = GetEffectiveWidth(hMonitor, mi.Monitor.right - mi.Monitor.left),
            Orientation = GetOrientation(new string(mi.DeviceName)),
            Frequency = GetFrequency(new string(mi.DeviceName)),
            IsAttached = IsAttached(new string(mi.DeviceName)),
            WorkArea = mi.WorkArea,            
            HMonitor = hMonitor
        };
    }

    private static bool IsAttached(string devName)
    {
        DISPLAY_DEVICE ddAdapter = new DISPLAY_DEVICE
        {
            cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE))
        };

        try
        {
            for (uint nAdapter = 0; EnumDisplayDevices(null, nAdapter, ref ddAdapter, 0); nAdapter++)
            {
                if (ddAdapter.DeviceName == devName)
                {
                    return (ddAdapter.StateFlags & DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) == DISPLAY_DEVICE_ATTACHED_TO_DESKTOP; ;
                }
                else continue;
            }
        }
        catch (Exception) { }

        return false;
    }

    private static string GetOrientation(string devName)
    {
        DISPLAY_DEVICE ddAdapter = new DISPLAY_DEVICE
        {
            cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE))
        };

        try
        {
            for (uint nAdapter = 0; EnumDisplayDevices(null, nAdapter, ref ddAdapter, 0); nAdapter++)
            {
                if (!ddAdapter.DeviceName.Equals(devName)) continue;

                IntPtr pDeviceName = Marshal.StringToHGlobalUni(ddAdapter.DeviceName);
                DEVMODE devmode = new DEVMODE();
                devmode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                bool bRet = EnumDisplaySettingsEx(pDeviceName, ENUM_CURRENT_SETTINGS, ref devmode, 0);
                if (!bRet)
                {
                    devmode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                    bRet = EnumDisplaySettingsEx(pDeviceName, ENUM_REGISTRY_SETTINGS, ref devmode, 0);
                }
                string _orientation = devmode.dmDisplayOrientation.ToString();

                _orientation = _orientation switch
                {
                    "Angle0" => "Landscape",
                    "Angle90" => "Portrait",
                    "Angle180" => "L Flipped",
                    "Angle270" => "P Flipped",
                    _ => "Landscape",
                };
                return _orientation;
            }
        }
        catch (Exception) { }

        return string.Empty;
    }

    private static string GetFrequency(string devName)
    {
        DISPLAY_DEVICE ddAdapter = new DISPLAY_DEVICE
        {
            cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE))
        };

        for (uint nAdapter = 0; EnumDisplayDevices(null, nAdapter, ref ddAdapter, 0); nAdapter++)
        {
            if (ddAdapter.DeviceName.Equals(devName))
            {
                IntPtr pDeviceName = Marshal.StringToHGlobalUni(ddAdapter.DeviceName);
                DEVMODE devmode = new DEVMODE();
                devmode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                bool bRet = EnumDisplaySettingsEx(pDeviceName, ENUM_CURRENT_SETTINGS, ref devmode, 0);
                if (!bRet)
                {
                    devmode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                    bRet = EnumDisplaySettingsEx(pDeviceName, ENUM_REGISTRY_SETTINGS, ref devmode, 0);
                }
                return string.Format("{0} Hz", devmode.dmDisplayFrequency);
            }
            else continue;
        }
        return String.Empty;
    }

    public static string GetUID(string devName)
    {
        DISPLAY_DEVICE ddAdapter = new DISPLAY_DEVICE
        {
            cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE))
        };

        try
        {
            for (uint nAdapter = 0; EnumDisplayDevices(null, nAdapter, ref ddAdapter, 0); nAdapter++)
            {
                DISPLAY_DEVICE ddMonitor = new DISPLAY_DEVICE();
                ddMonitor.cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE));
                for (uint nMonitor = 0; EnumDisplayDevices(ddAdapter.DeviceName, nMonitor, ref ddMonitor, EDD_GET_DEVICE_INTERFACE_NAME); nMonitor++)
                {
                    if (ddAdapter.DeviceName.Equals(devName))
                    {
                        return Regex.Match(ddMonitor.DeviceID, @"(UID)\d+").ToString();
                    }
                    else continue;
                }
            }
        }
        catch (Exception) { }
        return String.Empty;
    }

    private static string GetDeviceNameByUID(string uid)
    {
        DISPLAY_DEVICE ddAdapter = new DISPLAY_DEVICE
        {
            cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE))
        };

        try
        {
            for (uint nAdapter = 0; EnumDisplayDevices(null, nAdapter, ref ddAdapter, 0); nAdapter++)
            {
                DISPLAY_DEVICE ddMonitor = new DISPLAY_DEVICE();
                ddMonitor.cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE));
                for (uint nMonitor = 0; EnumDisplayDevices(ddAdapter.DeviceName, nMonitor, ref ddMonitor, EDD_GET_DEVICE_INTERFACE_NAME); nMonitor++)
                {
                    string tmp = Regex.Match(ddMonitor.DeviceID, @"(UID)\d+").ToString();
                    if (tmp == uid)
                    {
                        return ddAdapter.DeviceName.ToString();
                    }
                    else continue;
                }
            }
        }
        catch (Exception) { }
        return null;
    }

    private static int GetEffectiveHeight(IntPtr hMonitor, int height)
    {
        try
        {
            int heightDPI;
            _ = PInvoke.SHCore.GetDpiForMonitor(hMonitor,
                PInvoke.MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI, out heightDPI, out _);
            float scalingFactor = (float)heightDPI / 96;
            return (int)(height / scalingFactor);
        }
        catch (Exception) { return 0; }
    }

    private static int GetEffectiveWidth(IntPtr hMonitor, int width)
    {
        try
        {
            int widthDPI;
            _ = PInvoke.SHCore.GetDpiForMonitor(hMonitor,
                PInvoke.MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI, out widthDPI, out _);
            float scalingFactor = (float)widthDPI / 96;
            return (int)(width / scalingFactor);
        }
        catch (Exception) { return 0; }
    }
}
