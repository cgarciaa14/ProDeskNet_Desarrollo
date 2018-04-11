#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
#endregion

using System;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FPScannerX64.FPScannerClasses
{
    public static class Utils
    {
        public static readonly Brush COLOR_DERMALOG_GREEN = new SolidColorBrush(HexToMediaColor(0x11aa11));
        public static readonly Brush COLOR_DERMALOG_RED = new SolidColorBrush(HexToMediaColor(0xff0511));
        public static readonly Brush COLOR_DERMALOG_BLUE = new SolidColorBrush(HexToMediaColor(0x004289));



        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public static BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapSource img = null;
            try
            {
                img = Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             System.Windows.Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
                img.Freeze();
            }
            finally
            {
                DeleteObject(hBitmap);
            }
            return img;
        }

        public static Brush GetBrushFromNFIQ(int nfiq)
        {
            Brush fontColor = Brushes.Black;
            switch (nfiq)
            {
                case 1:
                    fontColor = COLOR_DERMALOG_GREEN;
                    break;
                case 2:
                    fontColor = Brushes.DarkOrange;
                    break;
                case 3:
                    fontColor = Brushes.DarkOrange;
                    break;
                case 4:
                    fontColor = Brushes.DarkOrange;
                    break;
                case 5:
                    fontColor = COLOR_DERMALOG_RED;
                    break;
            }
            return fontColor;
        }

        public static Color ToMediaColor(System.Drawing.Color color)
        {
            return Color.FromArgb(0xFF, color.R, color.G, color.B);
        }

        public static System.Drawing.Color HexToColor(int rgb)
        {
            return System.Drawing.Color.FromArgb(rgb);
        }

        public static Color HexToMediaColor(int rgb)
        {
            return ToMediaColor(HexToColor(rgb));
        }
    }
}