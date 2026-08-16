using Foundation;
using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class BlurProtectionManager
{
    private static readonly Lock _lock = new();

    private static bool _enabled;

    private static NSObject? _willResignActiveObserver;
    private static NSObject? _didBecomeActiveObserver;

    private static UIVisualEffectView? _blurBackground = null;

    internal static void HandleBlurProtection(bool enabled, bool throwErrors, ThemeStyle? style = null, UIWindow? window = null)
    {
        lock (_lock)
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
                        if (_enabled)
                            EnableBlurScreenProtection(window, style);
                        else
                            DisableBlurScreenProtection(window);
                    });
                }
                catch (Exception ex)
                {
                    ErrorsHandler.HandleException(nameof(HandleBlurProtection), throwErrors, ex);
                }
            });

            _didBecomeActiveObserver = UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
            {
                try
                {
                    MainThread.BeginInvokeOnMainThread(() => { DisableBlurScreenProtection(window); });
                }
                catch (Exception ex)
                {
                    ErrorsHandler.HandleException(nameof(HandleBlurProtection), throwErrors, ex);
                }
            });
        }
    }

    internal static void EnableBlur(UIWindow? window, ThemeStyle style, bool throwErrors)
    {
        try
        {
            EnableBlurScreenProtection(window, style);
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(EnableBlur), throwErrors, ex);
        }
    }

    internal static void DisableBlur(UIWindow? window, bool throwErrors)
    {
        try
        {
            DisableBlurScreenProtection(window);
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(DisableBlur), throwErrors, ex);
        }
    }

    private static void EnableBlurScreenProtection(UIWindow? window = null, ThemeStyle? style = null)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            AddBlurScreenProtection(window, style);
        });
    }

    private static void AddBlurScreenProtection(UIWindow? window = null, ThemeStyle? style = null)
    {
        var target = window ?? IOSHelpers.GetWindow();
        if (target is null)
            return;

        RemoveBlurScreenProtection(target);
        var blurEffectStyle = style switch
        {
            ThemeStyle.Light => UIBlurEffectStyle.Light,
            _ => UIBlurEffectStyle.Dark
        };

        using var blurEffect = UIBlurEffect.FromStyle(blurEffectStyle);

        _blurBackground = new UIVisualEffectView(blurEffect)
        {
            Frame = target.Bounds,
            AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight
        };

        target.AddSubview(_blurBackground);
    }

    private static void DisableBlurScreenProtection(UIWindow? window)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            RemoveBlurScreenProtection(window);
        });
    }

    private static void RemoveBlurScreenProtection(UIWindow? window)
    {
        _blurBackground?.RemoveFromSuperview();
        _blurBackground?.Dispose();
        _blurBackground = null;

        var target = window ?? IOSHelpers.GetWindow();

        if (target is null)
            return;

        foreach (var subview in target.Subviews)
        {
            if (subview is UIVisualEffectView)
            {
                subview.RemoveFromSuperview();
            }
        }
    }

    private static void DisposeObservers()
    {
        _willResignActiveObserver?.Dispose();
        _willResignActiveObserver = null;

        _didBecomeActiveObserver?.Dispose();
        _didBecomeActiveObserver = null;
    }
}
