﻿using CoreFoundation;
using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ScreenshotProtectionManager
{
    private static readonly UITextField _secureTextField = new()
    {
        UserInteractionEnabled = false
    };

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
                    UIViewController? rootViewController = GetRootPresentedViewController(window);

                    rootViewController?.View?.AddSubview(_secureTextField);
                    window.MakeKeyAndVisible();

                    window.Layer.SuperLayer?.AddSublayer(_secureTextField.Layer);

                    _secureTextField.Layer.Sublayers?[0].AddSublayer(window.Layer);
                }

                _secureTextField.SecureTextEntry = preventScreenshot;
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