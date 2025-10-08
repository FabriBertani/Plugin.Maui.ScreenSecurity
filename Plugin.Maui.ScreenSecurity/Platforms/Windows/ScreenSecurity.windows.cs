using Plugin.Maui.ScreenSecurity.Handlers;
using Plugin.Maui.ScreenSecurity.Platforms.Windows;
using System.Runtime.InteropServices;

using Application = Microsoft.Maui.Controls.Application;

namespace Plugin.Maui.ScreenSecurity;

internal partial class ScreenSecurityImplementation : IScreenSecurity, IDisposable
{
    private bool _disposed;

    private const uint WDA_NONE = 0x00000000;
    private const uint WDA_MONITOR = 0x00000001;

    // Windows 10, version 2004 and later
    private const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;

#if NET9_0_OR_GREATER
    private static readonly Lock _stateLock = new();
#else
    private static readonly object _stateLock = new();
#endif

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
#if NET9_0_OR_GREATER
        lock (_stateLock)
#else
        lock (_stateLock)
#endif
        {
            if (IsProtectionEnabled)
                return;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                SetScreenshotProtection(true);

                NativeMethods.SetHook(NativeMethods.Proc);
            });
        }
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
#if NET9_0_OR_GREATER
        lock (_stateLock)
#else
        lock (_stateLock)
#endif
        {
            if (!IsProtectionEnabled)
                return;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                SetScreenshotProtection(false);

                NativeMethods.Unhook();
            });
        }
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

    private void SetScreenshotProtection(bool enabled)
    {
        try
        {
            var hwnd = GetWindowHandle();

            if (hwnd == IntPtr.Zero)
            {
                System.Diagnostics.Trace.TraceWarning("SetScreenshotProtection: Window handle not available.");

                IsProtectionEnabled = enabled;

                return;
            }

            if (enabled)
            {
                if (!ApplyAffinity(hwnd, WDA_EXCLUDEFROMCAPTURE))
                {
                    System.Diagnostics.Trace.TraceInformation("WDA_EXCLUDEFROMCAPTURE not supported; falling back to WDA_MONITOR.");

                    ApplyAffinity(hwnd, WDA_MONITOR);
                }
            }
            else
                ApplyAffinity(hwnd, WDA_NONE);

            IsProtectionEnabled = enabled;
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(SetScreenshotProtection), ThrowErrors, ex);
        }
    }

    private static bool ApplyAffinity(IntPtr hwnd, uint affinity)
    {
        uint result = NativeMethods.SetWindowDisplayAffinity(hwnd, affinity);

        if (result == 0)
        {
            int err = Marshal.GetLastWin32Error();

            System.Diagnostics.Trace.TraceError($"SetWindowDisplayAffinity failed (Affinity=0x{affinity:X}). Win32Error={err}");

            return false;
        }

        return true;
    }

    private void OnScreenCaptured(object? sender, EventArgs e)
    {
        ScreenCaptured?.Invoke(this, EventArgs.Empty);
    }

    private static nint GetWindowHandle()
    {
        if (!MainThread.IsMainThread)
            throw new InvalidOperationException("GetWindowHandle must be called on the main (UI) thread.");

        var window = Application.Current?.Windows.Count > 0
            ? Application.Current?.Windows[0]
            : null;

        var platformView = window?.Handler.PlatformView as MauiWinUIWindow;

        return platformView?.WindowHandle ?? IntPtr.Zero;
    }

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