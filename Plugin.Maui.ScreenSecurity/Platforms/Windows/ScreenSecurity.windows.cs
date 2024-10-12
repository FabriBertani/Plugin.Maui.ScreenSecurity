using Plugin.Maui.ScreenSecurity.Handlers;
using Plugin.Maui.ScreenSecurity.Platforms.Windows;

using Application = Microsoft.Maui.Controls.Application;

namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity
{
    private const uint WDA_NONE = 0;
    private const uint WDA_MONITOR = 1;

    /// <summary>
    /// Activates the screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents app <b>screenshots</b> or <b>recording</b> to be taken.
    /// </summary>
    public void ActivateScreenSecurityProtection()
    {
        SetScreenshotProtection(true);
    }

    /// <summary>
    /// Activates the screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents app <b>screenshots</b> or <b>recording</b> to be taken.
    /// The specified parameters are for <b>iOS</b> only.
    /// </summary>
    /// <param name="blurScreenProtection">A boolean value indicates whether to blur the screen.</param>
    /// <param name="preventScreenshot">A boolean value that indicates whether to prevent screenshots.</param>
    /// <param name="preventScreenRecording">A boolean value that indicates whether to prevent screen recording.</param>
    /// <remarks>
    /// These parameters have <u><b>no effect</b></u> on <b>Android</b> and <b>Windows</b> platforms.
    /// </remarks>
    public void ActivateScreenSecurityProtection(bool blurScreenProtection, bool preventScreenshot, bool preventScreenRecording)
    {
        ActivateScreenSecurityProtection();
    }

    /// <summary>
    /// Activates the screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents app <b>screenshots</b> or <b>recording</b> to be taken.
    /// The specified parameters are for using a <u>Color</u> or an <u>Image</u> as protection on iOS only.
    /// </summary>
    /// <param name="screenProtectionOptions">
    /// ScreenProtectionOptions contains extra options for screen security protection,
    /// in order to customize the screen protection by specifying either a <b>Color</b> or an <b>Image</b> for iOS devices.
    /// </param>
    /// <remarks>
    /// These parameters have <u><b>no effect</b></u> on <b>Android</b> and <b>Windows</b> platforms.
    /// </remarks>
    public void ActivateScreenSecurityProtection(ScreenProtectionOptions screenProtectionOptions)
    {
        ActivateScreenSecurityProtection();
    }

    /// <summary>
    /// Deactivates all screen security protection.
    /// </summary>
    public void DeactivateScreenSecurityProtection()
    {
        SetScreenshotProtection(false);
    }

    /// <summary>
    /// Checks if screen protection is enabled.
    /// </summary>
    public bool IsProtectionEnabled { get; private set; }

    /// <summary>
    /// Triggered when the screen is captured by a screenshot.
    /// </summary>
    public event EventHandler<EventArgs>? ScreenCaptured;

    private void SetScreenshotProtection(bool enabled)
    {
        try
        {
            var hwnd = GetWindowHandle();

            if (hwnd != IntPtr.Zero)
                _ = NativeMethods.SetWindowDisplayAffinity(hwnd, enabled ? WDA_MONITOR : WDA_NONE);

            IsProtectionEnabled = enabled;
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(SetScreenshotProtection), ex);
        }
    }

    private static nint GetWindowHandle() => ((MauiWinUIWindow)Application.Current?.Windows[0].Handler.PlatformView!).WindowHandle;
}