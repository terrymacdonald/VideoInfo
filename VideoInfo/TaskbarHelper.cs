using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace DisplayMagicianShared.Windows
{
    public static class TaskbarHelper
    {
        private const int HWND_BROADCAST = 0xffff;
        private const int WM_DISPLAYCHANGE = 0x007E;
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("shell32.dll")]
        private static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        public static void ForceTaskbarRedraw()
        {
            // 1. Send WM_DISPLAYCHANGE broadcast (simulates a display event)
            int width = GetSystemMetrics(SM_CXSCREEN);
            int height = GetSystemMetrics(SM_CYSCREEN);
            int lParam = (height << 16) | width;

            PostMessage((IntPtr)HWND_BROADCAST, WM_DISPLAYCHANGE, IntPtr.Zero, (IntPtr)lParam);

            Thread.Sleep(200); // 2. Nudge the shell (explorer) to redraw UI

            // 2. Nudge the shell (explorer) to redraw UI
            const uint SHCNE_ASSOCCHANGED = 0x8000000;
            const uint SHCNF_IDLIST = 0;

            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
