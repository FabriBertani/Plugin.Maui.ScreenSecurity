using Foundation;
using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ColorProtectionManager
{
#if NET9_0_OR_GREATER
    private static readonly Lock _lock = new();
#else
    private static readonly object _lock = new();
#endif

    private static bool _enabled;

    private static NSObject? _willResignActiveObserver;
    private static NSObject? _didBecomeActiveObserver;

    private static UIView? _screenColor = null;

    internal static void HandleColorProtection(bool enabled, bool throwErrors, string hexColor = "", UIWindow? window = null)
    {
#if NET9_0_OR_GREATER
        lock (_lock)
#else
        lock (_lock)
#endif
        {
            // If state hasn't changed and observers are already set, skip re-subscribing
            if (_enabled == enabled
                    && _willResignActiveObserver is not null
                    && _didBecomeActiveObserver is not null)
            {
                return;
            }

            _enabled = enabled;

            // Remove existing observers before re-adding
            DisposeObservers();

            _willResignActiveObserver = UIApplication.Notifications.ObserveWillResignActive((sender, args) =>
            {
                try
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (enabled)
                            EnableColorScreenProtection(window, hexColor);
                        else
                            DisableColorScreenProtection();
                    });
                }
                catch (Exception ex)
                {
                    ErrorsHandler.HandleException(nameof(HandleColorProtection), throwErrors, ex);
                }
            });

            _didBecomeActiveObserver = UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
            {
                try
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        DisableColorScreenProtection();
                    });
                }
                catch (Exception ex)
                {
                    ErrorsHandler.HandleException(nameof(HandleColorProtection), throwErrors, ex);
                }
            });
        }
    }

    internal static void EnableColor(UIWindow? window, string hexColor, bool throwErrors)
    {
        try
        {
            EnableColorScreenProtection(window, hexColor);
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(EnableColor), throwErrors, ex);
        }
    }

    internal static void DisableColor(bool throwErrors)
    {
        try
        {
            DisableColorScreenProtection();
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(DisableColor), throwErrors, ex);
        }
    }

    private static void EnableColorScreenProtection(UIWindow? window, string hexColor)
    {
        if (window is null)
            return;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            _screenColor = new UIView(window.Bounds)
            {
                BackgroundColor = UIColor.Clear.FromHex(hexColor)
            };

            window.AddSubview(_screenColor);
        });
    }

    private static void DisableColorScreenProtection()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            _screenColor?.RemoveFromSuperview();

            _screenColor = null;
        });
    }

    private static void DisposeObservers()
    {
        _willResignActiveObserver?.Dispose();
        _willResignActiveObserver = null;

        _didBecomeActiveObserver?.Dispose();
        _didBecomeActiveObserver = null;
    }
}