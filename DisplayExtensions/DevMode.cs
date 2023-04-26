using System.Runtime.InteropServices;
using System;

namespace DisplayExtensions;

public class DevMode
{
    [DllImport("User32.dll", SetLastError = true)]
    public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAY_DEVICE
    {
        public int cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;
        public int StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;
    }

    public const int EDD_GET_DEVICE_INTERFACE_NAME = 0x00000001;

    public const int DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = 0x00000001;
    public const int DISPLAY_DEVICE_MULTI_DRIVER = 0x00000002;
    public const int DISPLAY_DEVICE_PRIMARY_DEVICE = 0x00000004;
    public const int DISPLAY_DEVICE_MIRRORING_DRIVER = 0x00000008;
    public const int DISPLAY_DEVICE_VGA_COMPATIBLE = 0x00000010;
    public const int DISPLAY_DEVICE_REMOVABLE = 0x00000020;
    public const int DISPLAY_DEVICE_ACC_DRIVER = 0x00000040;
    public const int DISPLAY_DEVICE_MODESPRUNED = 0x08000000;
    public const int DISPLAY_DEVICE_RDPUDD = 0x01000000;
    public const int DISPLAY_DEVICE_REMOTE = 0x04000000;
    public const int DISPLAY_DEVICE_DISCONNECT = 0x02000000;
    public const int DISPLAY_DEVICE_TS_COMPATIBLE = 0x00200000;
    public const int DISPLAY_DEVICE_UNSAFE_MODES_ON = 0x00080000;
    public const int DISPLAY_DEVICE_ACTIVE = 0x00000001;
    public const int DISPLAY_DEVICE_ATTACHED = 0x00000002;


    [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool EnumDisplaySettingsEx(IntPtr lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode, int dwFlags);

    public const int ENUM_CURRENT_SETTINGS = -1;
    public const int ENUM_REGISTRY_SETTINGS = -2;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DEVMODE
    {
        private const int CCHDEVICENAME = 32;
        private const int CCHFORMNAME = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public ScreenOrientation dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }
}
