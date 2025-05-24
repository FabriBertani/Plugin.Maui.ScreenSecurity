using Plugin.Maui.ScreenSecurity.Handlers;
using Plugin.Maui.ScreenSecurity.Platforms.Windows;

using Application = Microsoft.Maui.Controls.Application;

namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity, IDisposable
{
    private bool _disposed;

    private const uint WDA_NONE = 0;
    private const uint WDA_MONITOR = 1;

    public ScreenSecurityImplementation()
    {
        ScreenCaptureEventHandler.ScreenCaptured += OnScreenCaptured;
    }

    ~ScreenSecurityImplementation()
    {
        Dispose(false);
    }

    /// <summary>
    /// Activates screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents <b>screenshots</b> or <b>screen recordings</b> from being taken.
    /// </summary>
    public void ActivateScreenSecurityProtection()
    {
        SetScreenshotProtection(true);
    }

    /// <summary>
    /// Activates screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents <b>screenshots</b> or <b>screen recordings</b> from being taken.
    /// The specified parameters apply to <b>iOS</b> only.
    /// </summary>
    /// <param name="blurScreenProtection">Indicates whether to blur the screen.</param>
    /// <param name="preventScreenshot">Indicates whether to prevent screenshots.</param>
    /// <param name="preventScreenRecording">Indicates whether to prevent screen recording.</param>
    /// <remarks>
    /// These parameters have <i><b>no effect</b></i> on <b>Android</b> and <b>Windows</b> platforms.
    /// </remarks>
    public void ActivateScreenSecurityProtection(bool blurScreenProtection, bool preventScreenshot, bool preventScreenRecording)
    {
        ActivateScreenSecurityProtection();
    }

    /// <summary>
    /// Activates screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents <b>screenshots</b> or <b>screen recordings</b> from being taken.
    /// The specified parameters are for using a <i>Color</i> or an <i>Image</i> as protection on iOS only.
    /// </summary>
    /// <param name="screenProtectionOptions">
    /// Provides additional settings for screen security on iOS,
    /// allowing customization using either a <b>Color</b> or an <b>Image</b>.
    /// </param>
    /// <remarks>
    /// These parameters have <i><b>no effect</b></i> on <b>Android</b> and <b>Windows</b> platforms.
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
    /// Indicates whether screen protection is currently enabled.
    /// </summary>
    public bool IsProtectionEnabled { get; private set; }

    /// <summary>
    /// Triggered when the screen is captured, either via screenshot or recording.
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

    private void OnScreenCaptured(object sender, EventArgs e)
    {
        ScreenCaptured?.Invoke(this, EventArgs.Empty);
    }

    private static nint GetWindowHandle() => ((MauiWinUIWindow)Application.Current?.Windows[0].Handler.PlatformView!).WindowHandle;

    #region  Disposables

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
            ScreenCaptureEventHandler.ScreenCaptured -= OnScreenCaptured;

        _disposed = true;
    }

    #endregion
}