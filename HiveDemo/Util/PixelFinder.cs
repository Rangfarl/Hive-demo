using HiveDemo.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiveDemo.Util
{
    public static class PixelFinder
    {

        public static List<Point> PixelsearchALL(IntPtr rlHandle, Rectangle rect, Color Pixel_Color, int Shade_Variation)
        {
            using (Bitmap ScreenShot = Canvas.GetScreenshot(rlHandle))
            {
                List<Point> locations = new List<Point>();
                //Bitmap ScreenShot = Canvas.GetScreenshot(rlHandle);
                BitmapData RegionIn_BitmapData = ScreenShot.LockBits(new Rectangle(0, 0, rect.Width, rect.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int[] Formatted_Color = new int[3] { Pixel_Color.B, Pixel_Color.G, Pixel_Color.R };

                unsafe
                {
                    for (int y = 0; y < RegionIn_BitmapData.Height; y++)
                    {
                        byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);
                        for (int x = 0; x < RegionIn_BitmapData.Width; x++)
                        {
                            if (row[x * 3] >= (Formatted_Color[0] - Shade_Variation) & row[x * 3] <= (Formatted_Color[0] + Shade_Variation)) //blue
                                if (row[(x * 3) + 1] >= (Formatted_Color[1] - Shade_Variation) & row[(x * 3) + 1] <= (Formatted_Color[1] + Shade_Variation)) //green
                                    if (row[(x * 3) + 2] >= (Formatted_Color[2] - Shade_Variation) & row[(x * 3) + 2] <= (Formatted_Color[2] + Shade_Variation)) //red
                                        locations.Add(new Point(x, y));
                        }
                    }
                }
                ScreenShot.UnlockBits(RegionIn_BitmapData);
                ScreenShot.Dispose();
                return locations;
            }
        }
        public static Point Pixelsearch(IntPtr rlHandle, Rectangle rect, Color Pixel_Color, int Shade_Variation)
        {
            using (Bitmap ScreenShot = Canvas.GetScreenshot(rlHandle))
            {

                Point locations = new Point();
                // Bitmap ScreenShot = Canvas.GetScreenshot(rlHandle);
                BitmapData RegionIn_BitmapData = ScreenShot.LockBits(new Rectangle(0, 0, ScreenShot.Width, ScreenShot.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int[] Formatted_Color = new int[3] { Pixel_Color.B, Pixel_Color.G, Pixel_Color.R };

                unsafe
                {
                    for (int y = rect.Y; y < rect.Y + rect.Height; y++)
                    {
                        byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);
                        for (int x = rect.X; x < rect.X + rect.Width; x++)
                        {
                            if (row[x * 3] >= (Formatted_Color[0] - Shade_Variation) & row[x * 3] <= (Formatted_Color[0] + Shade_Variation)) //blue
                                if (row[(x * 3) + 1] >= (Formatted_Color[1] - Shade_Variation) & row[(x * 3) + 1] <= (Formatted_Color[1] + Shade_Variation)) //green
                                    if (row[(x * 3) + 2] >= (Formatted_Color[2] - Shade_Variation) & row[(x * 3) + 2] <= (Formatted_Color[2] + Shade_Variation)) //red
                                        locations = new Point(x, y);
                        }
                    }
                }
                ScreenShot.UnlockBits(RegionIn_BitmapData);
                ScreenShot.Dispose();
                return locations;
            }
        }
    }
}
