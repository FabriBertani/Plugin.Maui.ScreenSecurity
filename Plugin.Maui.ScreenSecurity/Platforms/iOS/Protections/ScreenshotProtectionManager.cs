using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ScreenshotProtectionManager
{
    private static UITextField? _secureTextField = null;
    private static UIView? _view = null;

    internal static void HandleScreenshotProtection(bool enabled, bool throwErrors, UIWindow? window = null)
    {
        try
        {
            SetScreenshotProtection(enabled, throwErrors, window);
        }
        catch (Exception ex)
        {
            ErrorsHandler.HandleException(nameof(HandleScreenshotProtection), throwErrors, ex);
        }
    }

    private static void SetScreenshotProtection(bool preventScreenshot, bool throwErrors, UIWindow? window)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(17, 0))
                {
                    if (preventScreenshot)
                    {
                        if (window is null)
                            return;

                        _secureTextField = new UITextField();

                        var color = IOSHelpers.GetCurrentTheme() == ThemeStyle.Light ? UIColor.White : UIColor.Black;

                        _view = new UIView(window.Bounds)
                        {
                            BackgroundColor = color
                        };

                        _secureTextField.SecureTextEntry = true;

                        window.AddSubview(_secureTextField);
                        window.AddSubview(_view);

                        window.Layer.SuperLayer?.AddSublayer(_secureTextField.Layer);
                        _secureTextField.Layer.Sublayers?.Last().AddSublayer(window.Layer);

                        _secureTextField.LeftView = _view;
                        _secureTextField.LeftViewMode = UITextFieldViewMode.Always;
                    }
                    else
                    {
                        if (_secureTextField is not null)
                            _secureTextField.SecureTextEntry = false;

                        if (_view is null)
                            return;

                        _view.Layer.RemoveFromSuperLayer();
                        _view.RemoveFromSuperview();
                    }
                }
                else
                {
                    if (window is not null)
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

                    if (_secureTextField is null)
                        return;

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
                ErrorsHandler.HandleException(nameof(SetScreenshotProtection), throwErrors, ex);
            }
        });
    }

    private static UIViewController? GetRootPresentedViewController(UIWindow window)
    {
        UIViewController? viewController = window.RootViewController;

        while (viewController?.PresentedViewController is not null)
        {
            viewController = viewController.PresentedViewController;
        }

        return viewController;
    }
}