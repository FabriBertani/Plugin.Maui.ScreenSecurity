using System.Runtime.InteropServices;

using Application = Microsoft.Maui.Controls.Application;

namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity
{
    [LibraryImport("user32.dll")]
    private static partial uint SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);

    private const uint WDA_NONE = 0;
    private const uint WDA_MONITOR = 1;

    /// <summary>
    /// Prevent screen content from being exposed when taking
    /// a <b><c>screenshot</c></b> by the system or any external app.
    /// </summary>
    public void EnableScreenshotProtection()
    {
        try
        {
            var hwnd = GetWindowHandle();

            _ = SetWindowDisplayAffinity(hwnd, WDA_MONITOR);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"EnableScreenshotProtection failed with Exception message: {ex.Message}");
            Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
            if (ex.InnerException != null)
                Console.WriteLine($"With InnerException: {ex.InnerException}");
        }
    }

    /// <summary>
    /// Re-enables content exposure when taking a screenshot.
    /// </summary>
    public void DisableScreenshotProtection()
    {
        try
        {
            var hwnd = GetWindowHandle();

            _ = SetWindowDisplayAffinity(hwnd, WDA_NONE);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DisableScreenshotProtection failed with Exception message: {ex.Message}");
            Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
            if (ex.InnerException != null)
                Console.WriteLine($"With InnerException: {ex.InnerException}");
        }
    }

    private static nint GetWindowHandle()
    {
        return ((MauiWinUIWindow)Application.Current?.Windows[0].Handler.PlatformView).WindowHandle;
    }
}