using Foundation;
using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ScreenshotProtectionManager
{
    private static UITextField? _secureTextField = null;
    private static UIView? _view = null;

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
        NSRunLoop.Main.BeginInvokeOnMainThread(() =>
        {
            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(17, 0))
                {
                    if (preventScreenshot)
                    {
                        if (window is not null)
                        {
                            _secureTextField = new();

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
                    }
                    else
                    {
                        if (_secureTextField is not null)
                            _secureTextField.SecureTextEntry = false;

                        if (_view is not null)
                        {
                            _view.Layer.RemoveFromSuperLayer();
                            _view.RemoveFromSuperview();
                        }
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

                    if (_secureTextField is not null)
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

        while (viewController?.PresentedViewController is not null)
        {
            viewController = viewController.PresentedViewController;
        }

        return viewController;
    }
}