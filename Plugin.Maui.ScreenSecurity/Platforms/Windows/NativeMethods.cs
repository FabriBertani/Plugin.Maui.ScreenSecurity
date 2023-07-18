using System.Runtime.InteropServices;

namespace Plugin.Maui.ScreenSecurity.Platforms.Windows;

internal partial class NativeMethods
{
#if NET6_0
    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);
#else
    [LibraryImport("user32.dll", SetLastError = true)]
    public static partial uint SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);
#endif
}