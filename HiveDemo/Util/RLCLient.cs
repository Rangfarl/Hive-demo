using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HiveDemo.Util
{
    class RLClient
    {
        [DllImport("user32.dll")]
        static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsWindowVisible(IntPtr hWnd);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public static IntPtr getHandle(string windowName)
        {
            IntPtr hWnd = IntPtr.Zero;
            EnumWindows(delegate (IntPtr wnd, IntPtr param)
            {
                StringBuilder sb = new StringBuilder(GetWindowTextLength(wnd) + 1);
                GetWindowText(wnd, sb, sb.Capacity);
                if (sb.ToString() == windowName)
                {
                    hWnd = wnd;
                    return false;
                }

                return true;
            }, IntPtr.Zero);

            return hWnd;
        }
        public static IntPtr getHandlePID(UInt32 processId)
        {
            UInt32 ID = (UInt32)processId;
            IntPtr hwHandle = IntPtr.Zero;
            Process process = Process.GetProcessById((int)ID);

            // Get the main window handle for the process
            IntPtr mainWindowHandle = process.MainWindowHandle;

            // Check if the main window handle is valid
            if (mainWindowHandle != IntPtr.Zero)
            {
                return mainWindowHandle;
            }
            else
            {
                return IntPtr.Zero;
            }
            

        }
    }
}
