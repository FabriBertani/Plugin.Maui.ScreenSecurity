using Foundation;
using Plugin.Maui.ScreenSecurity.Handlers;
using Plugin.Maui.ScreenSecurity.Platforms.iOS;
using UIKit;

namespace Plugin.Maui.ScreenSecurity;

internal partial class ScreenSecurityImplementation : IScreenSecurity, IDisposable
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

        BlurProtectionManager.HandleBlurProtection(true, ThrowErrors, IOSHelpers.GetCurrentTheme(), _window);

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
            BlurProtectionManager.HandleBlurProtection(true, ThrowErrors, IOSHelpers.GetCurrentTheme(), _window);

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
                ColorProtectionManager.HandleColorProtection(true, ThrowErrors, screenProtectionOptions.HexColor, _window);
            else
            {
                var invalidHexadecimalColorMessage = $"{screenProtectionOptions.HexColor} is not a valid hexadecimal color.";

                System.Diagnostics.Trace.TraceError(invalidHexadecimalColorMessage);

                if (ThrowErrors)
                    throw new ArgumentException(invalidHexadecimalColorMessage);
            }
        }
        else if (!string.IsNullOrEmpty(screenProtectionOptions.Image)
            && string.IsNullOrEmpty(screenProtectionOptions.HexColor))
        {
            if (screenProtectionOptions.Image.IsValidImage())
                ImageProtectionManager.HandleImageProtection(true, ThrowErrors, screenProtectionOptions.Image, _window);
            else
            {
                var invalidImageFormatMessage = $"{screenProtectionOptions.Image} is not a valid image format.";

                System.Diagnostics.Trace.TraceError(invalidImageFormatMessage);

                if (ThrowErrors)
                    throw new ArgumentException(invalidImageFormatMessage);
            }
        }

        HandleScreenCaptureProtection(screenProtectionOptions.PreventScreenshot, screenProtectionOptions.PreventScreenRecording);

        IsProtectionEnabled = true;
    }

    /// <summary>
    /// Deactivates all screen security protection.
    /// </summary>
    public void DeactivateScreenSecurityProtection()
    {
        BlurProtectionManager.HandleBlurProtection(false, ThrowErrors);

        ColorProtectionManager.HandleColorProtection(false, ThrowErrors);

        ImageProtectionManager.HandleImageProtection(false, ThrowErrors);

        ScreenRecordingProtectionManager.HandleScreenRecordingProtection(false, ThrowErrors);

        ScreenshotProtectionManager.HandleScreenshotProtection(false, ThrowErrors);

        IsProtectionEnabled = false;
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

    private void HandleScreenCaptureProtection(bool preventScreenshot, bool preventScreenRecording)
    {
        ScreenshotProtectionManager.HandleScreenshotProtection(preventScreenshot, ThrowErrors, _window);

        ScreenRecordingProtectionManager.HandleScreenRecordingProtection(preventScreenRecording, ThrowErrors, string.Empty, _window);
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

    /// <summary>
    /// Ensures that <c>_window</c> is initialized with the current UIWindow instance.
    /// If called from the main thread, the window is set synchronously.
    /// If called from a background thread, the assignment is dispatched asynchronously to the main thread.
    /// <para><b>Note:</b> If you call this method from a background thread, <c>_window</c> may not be immediately available
    /// after the call returns, as the assignment happens asynchronously. Any code that depends on <c>_window</c>
    /// being set should either ensure it runs on the main thread or handle the possibility that <c>_window</c> is still null.</para>
    /// </summary>
    private void GetWindow()
    {
        if (_window is not null)
            return;

        if (MainThread.IsMainThread)
        {
            _window = IOSHelpers.GetWindow();
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _window ??= IOSHelpers.GetWindow();
            });
        }
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

            _screenshotObserver?.Dispose();
            _screenshotObserver = null;

            _screenCapturedObserver?.Dispose();
            _screenCapturedObserver = null;
        }

        _disposed = true;
    }

    #endregion
}