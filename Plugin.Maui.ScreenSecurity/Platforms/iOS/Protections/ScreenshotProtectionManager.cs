using CoreFoundation;
using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ScreenshotProtectionManager
{
    private static UITextField? _secureTextField = null;

    internal static void HandleScreenshotProtection(bool enabled, UIWindow? window = null)
    {
        try
        {
            SetScreenshotProtection(enabled, window);
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(HandleScreenshotProtection), ex);
        }
    }

    private static void SetScreenshotProtection(bool preventScreenshot, UIWindow? window)
    {
        DispatchQueue.MainQueue.DispatchAsync(() =>
        {
            try
            {
                if (window != null)
                {
                    _secureTextField ??= new()
                    {
                        UserInteractionEnabled = false
                    };

                    UIViewController? rootViewController = GetRootPresentedViewController(window);

                    rootViewController?.View?.AddSubview(_secureTextField);
                    window.MakeKeyAndVisible();

                    window.Layer.SuperLayer?.AddSublayer(_secureTextField.Layer);

                    _secureTextField.Layer.Sublayers?[0].AddSublayer(window.Layer);
                }

                if (_secureTextField != null)
                {
                    if (preventScreenshot)
                        _secureTextField.SecureTextEntry = preventScreenshot;
                    else
                    {
                        _secureTextField.SecureTextEntry = false;
                        _secureTextField = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(SetScreenshotProtection), ex);
            }
        });
    }

    private static UIViewController? GetRootPresentedViewController(UIWindow window)
    {
        UIViewController? viewController = window.RootViewController;

        while (viewController?.PresentedViewController != null)
        {
            viewController = viewController.PresentedViewController;
        }

        return viewController;
    }
}