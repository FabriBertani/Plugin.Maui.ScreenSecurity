using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class BlurProtectionManager
{
    private static UIView? _screenBlur = null;

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
        _screenBlur = UIScreen.MainScreen.SnapshotView(false);

        var blurEffectStyle = style == ThemeStyle.Light
            ? UIBlurEffectStyle.Light
            : UIBlurEffectStyle.Dark;

        using var blurEffect = UIBlurEffect.FromStyle(blurEffectStyle);
        using var blurBackground = new UIVisualEffectView(blurEffect);
        if (_screenBlur != null)
        {
            _screenBlur.AddSubview(blurBackground);

            blurBackground.Frame = _screenBlur.Frame;

            window?.AddSubview(_screenBlur);
        }
    }

    private static void DisableBlurScreenProtection()
    {
        _screenBlur?.RemoveFromSuperview();

        _screenBlur = null;
    }
}