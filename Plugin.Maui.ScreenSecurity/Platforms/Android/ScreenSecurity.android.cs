using Android.Views;
using Plugin.Maui.ScreenSecurity.Handlers;

namespace Plugin.Maui.ScreenSecurity;

internal partial class ScreenSecurityImplementation : IScreenSecurity, IDisposable
{
    private bool _disposed;

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
        SetScreenSecurityProtection(true);
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
        SetScreenSecurityProtection(false);
    }

    private void SetScreenSecurityProtection(bool enabled)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                var activity = Platform.CurrentActivity;

                if (activity is null)
                    return;

                if (OperatingSystem.IsAndroidVersionAtLeast(33))
                    activity.SetRecentsScreenshotEnabled(!enabled);

                if (enabled)
                    activity.Window?.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);
                else
                    activity.Window?.ClearFlags(WindowManagerFlags.Secure);

                IsProtectionEnabled = enabled;
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(SetScreenSecurityProtection), ThrowErrors, ex);
            }
        });
    }

    private void OnScreenCaptured(object? sender, EventArgs e)
    {
        ScreenCaptured?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Indicates whether screen protection is currently enabled.
    /// </summary>
    public bool IsProtectionEnabled { get; private set; }

    /// <summary>
    /// If set to true, exceptions will be thrown when an error occurs.
    /// </summary>
    public bool ThrowErrors { get; set; }

    /// <summary>
    /// Triggered when the screen is captured, either via screenshot or recording.
    /// </summary>
    public event EventHandler<EventArgs>? ScreenCaptured;

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