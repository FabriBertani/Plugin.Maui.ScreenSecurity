using Plugin.Maui.ScreenSecurity.Handlers;
using Plugin.Maui.ScreenSecurity.Platforms.Windows;

using Application = Microsoft.Maui.Controls.Application;

namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity
{
    private const uint WDA_NONE = 0;
    private const uint WDA_MONITOR = 1;

    public void ActivateScreenSecurityProtection()
    {
        SetScreenshotProtection(true);
    }

    public void ActivateScreenSecurityProtection(bool blurScreenProtection, bool preventScreenshot, bool preventScreenRecording)
    {
        ActivateScreenSecurityProtection();
    }

    public void ActivateScreenSecurityProtection(ScreenProtectionOptions screenProtectionOptions)
    {
        ActivateScreenSecurityProtection();
    }

    public void DeactivateScreenSecurityProtection()
    {
        SetScreenshotProtection(false);
    }

    /// <summary>
    /// Prevent screen content from being exposed when taking
    /// a <b><c>screenshot</c></b> by the system or any external app.
    /// </summary>
    public void EnableScreenshotProtection()
    {
        SetScreenshotProtection(true);
    }

    /// <summary>
    /// Re-enables content exposure when taking a screenshot.
    /// </summary>
    public void DisableScreenshotProtection()
    {
        SetScreenshotProtection(false);
    }

    private void SetScreenshotProtection(bool enabled)
    {
        try
        {
            var hwnd = GetWindowHandle();

            if (hwnd != IntPtr.Zero)
                _ = NativeMethods.SetWindowDisplayAffinity(hwnd, enabled ? WDA_MONITOR : WDA_NONE);
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(SetScreenshotProtection), ex);
        }
    }

    private static nint GetWindowHandle() => ((MauiWinUIWindow)Application.Current?.Windows[0].Handler.PlatformView!).WindowHandle;
}