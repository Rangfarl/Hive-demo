using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HiveDemo.Core
{
    public static class Canvas
    {
        //Inventory bounds
        public static Rectangle INVENTORY = new Rectangle(551, 232, 190, 261);
        public static Rectangle GAMEVIEW = new Rectangle(8, 30, 512, 334);
        public static Rectangle XPDROP = new Rectangle(476, 72, 45, 117);
        public static Rectangle Chatbox = new Rectangle(11, 372, 504, 110);
        public static Rectangle MAP = new Rectangle(569, 50, 117, 110);
        public static Point PLAYER = new Point(274, 274);
        public static Rectangle[] INVENTORY_SLOT = new Rectangle[]
        {
            new Rectangle(567, 239, 36, 32),
            new Rectangle(609, 239, 36, 32),
            new Rectangle(651, 239, 36, 32),
            new Rectangle(693, 239, 36, 32),
            new Rectangle(567, 275, 36, 32),
            new Rectangle(609, 275, 36, 32),
            new Rectangle(651, 275, 36, 32),
            new Rectangle(693, 275, 36, 32),
            new Rectangle(567, 311, 36, 32),
            new Rectangle(609, 311, 36, 32),
            new Rectangle(651, 311, 36, 32),
            new Rectangle(693, 311, 36, 32),
            new Rectangle(567, 348, 36, 32),
            new Rectangle(609, 348, 36, 32),
            new Rectangle(651, 348, 36, 32),
            new Rectangle(693, 348, 36, 32),
            new Rectangle(567, 385, 36, 32),
            new Rectangle(609, 385, 36, 32),
            new Rectangle(651, 385, 36, 32),
            new Rectangle(693, 385, 36, 32),
            new Rectangle(567, 422, 36, 32),
            new Rectangle(609, 422, 36, 32),
            new Rectangle(651, 422, 36, 32),
            new Rectangle(693, 422, 36, 32),
            new Rectangle(567, 459, 36, 32),
            new Rectangle(609, 459, 36, 32),
            new Rectangle(651, 459, 36, 32),
            new Rectangle(693, 459, 36, 32),
        };
        public static Rectangle[] INVENTORY_SLOT_DROP = new Rectangle[]
{
            new Rectangle(563, 213, 36, 32),
            new Rectangle(605, 213, 36, 32),
            new Rectangle(647, 213, 36, 32),
            new Rectangle(689, 213, 36, 32),

            new Rectangle(563, 249, 36, 32),
            new Rectangle(605, 249, 36, 32),
            new Rectangle(647, 249, 36, 32),
            new Rectangle(689, 249, 36, 32),

            new Rectangle(563, 285, 36, 32),
            new Rectangle(605, 285, 36, 32),
            new Rectangle(647, 285, 36, 32),
            new Rectangle(689, 285, 36, 32),

            new Rectangle(563, 321, 36, 32),
            new Rectangle(605, 321, 36, 32),
            new Rectangle(647, 321, 36, 32),
            new Rectangle(689, 321, 36, 32),

            new Rectangle(563, 357, 36, 32),
            new Rectangle(605, 357, 36, 32),
            new Rectangle(647, 357, 36, 32),
            new Rectangle(689, 357, 36, 32),

            new Rectangle(563, 393, 36, 32),
            new Rectangle(605, 393, 36, 32),
            new Rectangle(647, 393, 36, 32),
            new Rectangle(689, 393, 36, 32),

            new Rectangle(563, 429, 36, 32),
            new Rectangle(605, 429, 36, 32),
            new Rectangle(647, 429, 36, 32),
            new Rectangle(689, 429, 36, 32),
};

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        static extern IntPtr DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hObject, int x, int y, int width, int height, IntPtr hDC, int srcX, int srcY, int op);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int width, int height);
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        public static Bitmap GetScreenshot(IntPtr hWnd)
        {
            Rect rect = new Rect();
            GetWindowRect(hWnd, ref rect);
            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;
            IntPtr hDC = GetWindowDC(hWnd);
            IntPtr hMemDC = CreateCompatibleDC(hDC);
            IntPtr hBitmap = CreateCompatibleBitmap(hDC, width, height);
            if (hBitmap != IntPtr.Zero)
            {
                IntPtr hOld = SelectObject(hMemDC, hBitmap);
                BitBlt(hMemDC, 0, 0, width, height, hDC, 0, 0, 13369376);
                SelectObject(hMemDC, hOld);
                DeleteDC(hMemDC);
                ReleaseDC(hWnd, hDC);
                Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
                DeleteObject(hBitmap);
                return bmp;
            }
            return null;
        }
    }
}
