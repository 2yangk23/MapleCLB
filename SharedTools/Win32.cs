using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MapleCLB.Tools {
    /// <summary>
    /// Win32 support code.
    /// (C) 2003 Bob Bradley / ZBobb@hotmail.com
    /// </summary>
    public class Win32 {
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_LBUTTONDBLCLK = 0x0203;

        public const int WM_MOUSELEAVE = 0x02A3;

        public const int WM_PAINT = 0x000F;
        public const int WM_ERASEBKGND = 0x0014;

        public const int WM_PRINT = 0x0317;

        //const int EN_HSCROLL                  = 0x0601;
        //const int EN_VSCROLL                  = 0x0602;

        public const int WM_HSCROLL = 0x0114;
        public const int WM_VSCROLL = 0x0115;

        public const int EM_GETSEL = 0x00B0;
        public const int EM_LINEINDEX = 0x00BB;
        public const int EM_LINEFROMCHAR = 0x00C9;

        public const int EM_POSFROMCHAR = 0x00D6;

        public const int EM_SETCUEBANNER = 0x1501;
        public const int EM_GETCUEBANNER = 0x1502;

        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("USER32.DLL", EntryPoint = "PostMessage")]
        public static extern bool PostMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("USER32.DLL", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("USER32.DLL", EntryPoint = "GetCaretBlinkTime")]
        public static extern uint GetCaretBlinkTime();

        public const long PRF_CLIENT = 0x00000004L;
        public const long PRF_ERASEBKGND = 0x00000008L;

        public static bool CaptureWindow(System.Windows.Forms.Control control, ref Bitmap bitmap) {
            //This function captures the contents of a window or control
            var g2 = Graphics.FromImage(bitmap);

            //PRF_CHILDREN // PRF_NONCLIENT
            const int meint = (int)(PRF_CLIENT | PRF_ERASEBKGND); //| PRF_OWNED ); //  );
            var meptr = new IntPtr(meint);

            var hdc = g2.GetHdc();
            SendMessage(control.Handle, WM_PRINT, hdc, meptr);

            g2.ReleaseHdc(hdc);
            g2.Dispose();

            return true;
        }
    }
}