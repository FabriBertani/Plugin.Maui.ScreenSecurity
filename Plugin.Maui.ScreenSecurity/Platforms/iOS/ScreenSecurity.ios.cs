using Foundation;
using Plugin.Maui.ScreenSecurity.Handlers;
using Plugin.Maui.ScreenSecurity.Platforms.iOS;
using UIKit;

namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity, IDisposable
{
    private UIWindow? _window;

    private NSObject? _screenshotObserver;
    private NSObject? _screenCapturedObserver;

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
        GetWindow();

        EnableScreenCapturedEvent();

        BlurProtectionManager.HandleBlurProtection(true, IOSHelpers.GetCurrentTheme(), _window);

        HandleScreenCaptureProtection(true, true);

        IsProtectionEnabled = true;
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
    public void ActivateScreenSecurityProtection(bool blurScreenProtection = true, bool preventScreenshot = true, bool preventScreenRecording = true)
    {
        GetWindow();

        EnableScreenCapturedEvent();

        if (blurScreenProtection)
            BlurProtectionManager.HandleBlurProtection(true, IOSHelpers.GetCurrentTheme(), _window);

        HandleScreenCaptureProtection(preventScreenshot, preventScreenRecording);

        IsProtectionEnabled = true;
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
        GetWindow();

        EnableScreenCapturedEvent();

        if (!string.IsNullOrEmpty(screenProtectionOptions.HexColor)
            && string.IsNullOrEmpty(screenProtectionOptions.Image))
        {
            if (screenProtectionOptions.HexColor.IsHexColor())
                ColorProtectionManager.HandleColorProtection(true, screenProtectionOptions.HexColor, _window);
            else
                throw new ArgumentException($"{screenProtectionOptions.HexColor} is not a valid hexadecimal color.");
        }
        else if (!string.IsNullOrEmpty(screenProtectionOptions.Image)
            && string.IsNullOrEmpty(screenProtectionOptions.HexColor))
        {
            if (screenProtectionOptions.Image.IsValidImage())
                ImageProtectionManager.HandleImageProtection(true, screenProtectionOptions.Image, _window);
            else
                throw new ArgumentException($"{screenProtectionOptions.Image} is not a valid image format.");
        }

        HandleScreenCaptureProtection(screenProtectionOptions.PreventScreenshot, screenProtectionOptions.PreventScreenRecording);

        IsProtectionEnabled = true;
    }

    /// <summary>
    /// Deactivates all screen security protection.
    /// </summary>
    public void DeactivateScreenSecurityProtection()
    {
        BlurProtectionManager.HandleBlurProtection(false);

        ColorProtectionManager.HandleColorProtection(false);

        ImageProtectionManager.HandleImageProtection(false);

        ScreenRecordingProtectionManager.HandleScreenRecordingProtection(false);

        ScreenshotProtectionManager.HandleScreenshotProtection(false);

        IsProtectionEnabled = false;
    }

    /// <summary>
    /// Indicates whether screen protection is currently enabled.
    /// </summary>
    public bool IsProtectionEnabled { get; private set; }

    /// <summary>
    /// Triggered when the screen is captured, either via screenshot or recording.
    /// </summary>
    public event EventHandler<EventArgs>? ScreenCaptured;

    private void HandleScreenCaptureProtection(bool preventScreenshot, bool preventScreenRecording)
    {
        ScreenshotProtectionManager.HandleScreenshotProtection(preventScreenshot, _window);

        ScreenRecordingProtectionManager.HandleScreenRecordingProtection(preventScreenRecording, string.Empty, _window);
    }

    private void EnableScreenCapturedEvent()
    {
        _screenshotObserver = UIApplication.Notifications.ObserveUserDidTakeScreenshot((sender, args) =>
        {
            ScreenCaptureEventHandler.RaiseScreenCaptured();
        });

        _screenCapturedObserver = UIScreen.Notifications.ObserveCapturedDidChange((sender, args) =>
        {
            ScreenCaptureEventHandler.RaiseScreenCaptured();
        });
    }

    private void GetWindow()
    {
        _window ??= IOSHelpers.GetWindow();
    }

    private void OnScreenCaptured(object? sender, EventArgs e)
    {
        ScreenCaptured?.Invoke(this, EventArgs.Empty);
    }

    #region Disposables

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
        {
            ScreenCaptureEventHandler.ScreenCaptured -= OnScreenCaptured;

            if (_screenshotObserver is not null)
            {
                _screenshotObserver.Dispose();
                _screenshotObserver = null;
            }

            if (_screenCapturedObserver is not null)
            {
                _screenCapturedObserver.Dispose();
                _screenCapturedObserver = null;
            }
        }

        _disposed = true;
    }

    #endregion
}