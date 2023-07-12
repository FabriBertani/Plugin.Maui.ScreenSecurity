using System.Runtime.InteropServices;

namespace Plugin.Maui.ScreenSecurity.Platforms.Windows;

internal partial class NativeMethods
{
    [LibraryImport("user32.dll", SetLastError = true)]
    public static partial uint SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);
}