using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class BlurProtectionManager
{
    private static UIVisualEffectView? _blurBackground = null;

    internal static void HandleBlurProtection(bool enabled, ThemeStyle? style = null, UIWindow? window = null)
    {
        UIApplication.Notifications.ObserveWillResignActive((sender, args) =>
        {
            try
            {
                if (enabled)
                    EnableBlurScreenProtection(window, style);
                else
                    DisableBlurScreenProtection();
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(HandleBlurProtection), ex);
            }
        });

        UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
        {
            try
            {
                DisableBlurScreenProtection();
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(HandleBlurProtection), ex);
            }
        });
    }

    internal static void EnableBlur(UIWindow? window, ThemeStyle style)
    {
        try
        {
            EnableBlurScreenProtection(window, style);
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(EnableBlur), ex);
        }
    }

    internal static void DisableBlur()
    {
        try
        {
            DisableBlurScreenProtection();
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(DisableBlur), ex);
        }
    }

    private static void EnableBlurScreenProtection(UIWindow? window = null, ThemeStyle? style = null)
    {
        if (window != null)
        {
            var blurEffectStyle = style switch
            {
                ThemeStyle.Light => UIBlurEffectStyle.Light,
                _ => UIBlurEffectStyle.Dark
            };

            using var blurEffect = UIBlurEffect.FromStyle(blurEffectStyle);

            _blurBackground = new UIVisualEffectView(blurEffect)
            {
                Frame = window.Frame
            };
        
            window.AddSubview(_blurBackground);
        }
    }

    private static void DisableBlurScreenProtection()
    {
        _blurBackground?.RemoveFromSuperview();

        _blurBackground = null;
    }
}