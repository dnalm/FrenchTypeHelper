using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FrenchTypeHelper_WPF.Utils
{
    internal class ImageUtil
    {
        public static BitmapSource BitmapToBitmapSource(Bitmap bitmap)
        {
            BitmapSource img;
            IntPtr hBitmap;

            hBitmap = bitmap.GetHbitmap();
            img = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            return img;
        }
    }
}
