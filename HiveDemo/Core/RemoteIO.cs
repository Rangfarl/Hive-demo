using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiveDemo.Core
{
    class RemoteIO
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        static extern bool FreeLibrary(IntPtr hModule);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate bool KInput_Create(UInt32 PID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate bool KInput_Delete(UInt32 PID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate bool KInput_FocusEvent(UInt32 PID, int ID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate bool KInput_KeyEvent(UInt32 PID, int ID, ulong When, int Modifiers, int KeyCode, ushort KeyChar, int KeyLocation);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate bool KInput_MouseEvent(UInt32 PID, int ID, ulong When, int Modifiers, int X, int Y, int ClickCount, bool PopupTrigger, int Button);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate bool KInput_MouseWheelEvent(UInt32 PID, int ID, ulong When, int Modifiers, int X, int Y, int ClickCount, bool PopupTrigger, int ScrollType, int ScrollAmount, int WheelRotation);

        public static ulong CurrentTimeMillis()
        {
            return (ulong)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        private UInt32 _PID;
        private IntPtr _hModule;
        private KInput_Create _Create;
        private KInput_Delete _Delete;
        private KInput_FocusEvent _FocusEvent;
        private KInput_KeyEvent _KeyEvent;
        private KInput_MouseEvent _MouseEvent;
        private KInput_MouseWheelEvent _MouseWheelEvent;
        private string folderName = "Plugins";
        

        public RemoteIO(UInt32 PID)
        {
            _PID = PID;
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
            _hModule = LoadLibrary(@"C:\Users\ihost\source\repos\HiveDemo\Plugins\KInputCtrl.dll");
            _Create = (KInput_Create)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_hModule, "KInput_Create"), typeof(KInput_Create));
            _Delete = (KInput_Delete)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_hModule, "KInput_Delete"), typeof(KInput_Delete));
            _FocusEvent = (KInput_FocusEvent)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_hModule, "KInput_FocusEvent"), typeof(KInput_FocusEvent));
            _KeyEvent = (KInput_KeyEvent)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_hModule, "KInput_KeyEvent"), typeof(KInput_KeyEvent));
            _MouseEvent = (KInput_MouseEvent)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_hModule, "KInput_MouseEvent"), typeof(KInput_MouseEvent));
            _MouseWheelEvent = (KInput_MouseWheelEvent)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_hModule, "KInput_MouseWheelEvent"), typeof(KInput_MouseWheelEvent));
            _Create(_PID);
        }

        public void Click(int X, int Y)
        {
            _FocusEvent(_PID, 1004);
            Thread.Sleep(300);
            _MouseEvent(_PID, 504, CurrentTimeMillis(), 0, X, Y, 0, false, 0);
            _MouseEvent(_PID, 505, CurrentTimeMillis(), 0, X, Y, 0, false, 0);
            _MouseEvent(_PID, 503, CurrentTimeMillis(), 0, X, Y, 0, false, 0);
            Thread.Sleep(300);
            _MouseEvent(_PID, 501, CurrentTimeMillis(), 0, X, Y, 1, false, 1);
            _MouseEvent(_PID, 502, CurrentTimeMillis(), 0, X, Y, 1, false, 1);
            _MouseEvent(_PID, 500, CurrentTimeMillis(), 0, X, Y, 1, false, 1);

        }
        public void keyevent(int keyaction, int id, ushort keycode)
        {
            _KeyEvent(_PID, keyaction, CurrentTimeMillis(), 0, id, keycode, 0);
        }
    }
}
