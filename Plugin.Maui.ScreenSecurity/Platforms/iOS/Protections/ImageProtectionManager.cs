using Foundation;
using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ImageProtectionManager
{
#if NET9_0_OR_GREATER
    private static readonly Lock _lock = new();
#else
    private static readonly object _lock = new();
#endif

    private static bool _enabled;

    private static NSObject? _willResignActiveObserver;
    private static NSObject? _didBecomeActiveObserver;

    private static UIImageView? _screenImage = null;

    internal static void HandleImageProtection(bool enabled, bool throwErrors, string image = "", UIWindow? window = null)
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
                            EnableImageScreenProtection(image, window);
                        else
                            DisableImageScreenProtection();
                    });
                }
                catch (Exception ex)
                {
                    ErrorsHandler.HandleException(nameof(HandleImageProtection), throwErrors, ex);
                }
            });

            _didBecomeActiveObserver = UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
            {
                try
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        DisableImageScreenProtection();
                    });
                }
                catch (Exception ex)
                {
                    ErrorsHandler.HandleException(nameof(HandleImageProtection), throwErrors, ex);
                }
            });
        }
    }

    private static void EnableImageScreenProtection(string image, UIWindow? window)
    {
        if (window is null)
            return;

        DisableImageScreenProtection();

        if (string.IsNullOrEmpty(image))
            return;

        if (!File.Exists(image))
            return;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            using var uiImage = new UIImage(image);
            _screenImage = new UIImageView(UIScreen.MainScreen.Bounds)
            {
                Image = uiImage,
                UserInteractionEnabled = false,
                ContentMode = UIViewContentMode.ScaleAspectFill,
                ClipsToBounds = true
            };

            window.AddSubview(_screenImage);
        });
    }

    private static void DisableImageScreenProtection()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            _screenImage?.RemoveFromSuperview();

            _screenImage?.Dispose();

            _screenImage = null;
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