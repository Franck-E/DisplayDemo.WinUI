using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;

namespace DisplayExtensions;

public class ScreenshotCapture
{
    #region Methods

    public static async Task<ImageSource> GetScreenshot(DisplayModel item)
    {
        var bitmap = CaptureRegion(item.WorkArea);
        var bit = BitmapToByteArray(bitmap);
        return await ByteToImageSource(bit);
    }


    private static Bitmap CaptureRegion(Rectangle region)
    {
        IntPtr desktophWnd;
        IntPtr desktopDc;
        IntPtr memoryDc;
        IntPtr bitmap;
        IntPtr oldBitmap;
        bool success;
        Bitmap result;

        desktophWnd = NativeMethods.GetDesktopWindow();
        desktopDc = NativeMethods.GetWindowDC(desktophWnd);
        memoryDc = NativeMethods.CreateCompatibleDC(desktopDc);
        bitmap = NativeMethods.CreateCompatibleBitmap(desktopDc, region.Width, region.Height);
        oldBitmap = NativeMethods.SelectObject(memoryDc, bitmap);

        success = NativeMethods.BitBlt(memoryDc, 0, 0, region.Width, region.Height, desktopDc, region.Left, region.Top, NativeMethods.RasterOperations.SRCCOPY | NativeMethods.RasterOperations.CAPTUREBLT);

        try
        {
            if (!success)
            {
                throw new Win32Exception();
            }

            result = Image.FromHbitmap(bitmap);
        }
        finally
        {
            NativeMethods.SelectObject(memoryDc, oldBitmap);
            NativeMethods.DeleteObject(bitmap);
            NativeMethods.DeleteDC(memoryDc);
            NativeMethods.ReleaseDC(desktophWnd, desktopDc);
        }
        return result;
    }


    private static byte[] BitmapToByteArray(Bitmap bitmap)
    {
        var imageStream = new MemoryStream();
        using (imageStream)
        {
            bitmap.Save(imageStream, ImageFormat.Jpeg);
            imageStream.Position = 0;
            return imageStream.ToArray();
        }
    }


    private static async Task<ImageSource> ByteToImageSource(byte[] bytes)
    {

        var image = bytes.AsBuffer().AsStream().AsRandomAccessStream();

        // decode image
        var decoder = await BitmapDecoder.CreateAsync(image);
        image.Seek(0);

        // create bitmap
        var output = new WriteableBitmap((int)decoder.PixelHeight, (int)decoder.PixelWidth);
        await output.SetSourceAsync(image);
        return output;
    }


    #endregion
}
