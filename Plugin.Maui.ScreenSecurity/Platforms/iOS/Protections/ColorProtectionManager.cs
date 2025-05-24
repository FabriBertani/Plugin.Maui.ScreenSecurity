using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ColorProtectionManager
{
    private static UIView? _screenColor = null;

    internal static void HandleColorProtection(bool enabled, string hexColor = "", UIWindow? window = null)
    {
        UIApplication.Notifications.ObserveWillResignActive((sender, args) =>
        {
            try
            {
                if (enabled)
                    EnableColorScreenProtection(window, hexColor);
                else
                    DisableColorScreenProtection();
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(HandleColorProtection), ex);
            }
        });

        UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
        {
            try
            {
                DisableColorScreenProtection();
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(HandleColorProtection), ex);
            }
        });
    }

    internal static void EnableColor(UIWindow? window, string hexColor)
    {
        try
        {
            EnableColorScreenProtection(window, hexColor);
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(EnableColor), ex);
        }
    }

    internal static void DisableColor()
    {
        try
        {
            DisableColorScreenProtection();
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(DisableColor), ex);
        }
    }

    private static void EnableColorScreenProtection(UIWindow? window, string hexColor)
    {
        if (window is null)
            return;
        
        _screenColor = new UIView(window.Bounds)
        {
            BackgroundColor = UIColor.Clear.FromHex(hexColor)
        };

        window.AddSubview(_screenColor);
    }

    private static void DisableColorScreenProtection()
    {
        _screenColor?.RemoveFromSuperview();

        _screenColor = null;
    }
}