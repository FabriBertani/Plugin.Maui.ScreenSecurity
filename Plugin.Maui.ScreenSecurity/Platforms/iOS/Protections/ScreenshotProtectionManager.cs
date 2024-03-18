using CoreFoundation;
using CoreGraphics;
using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ScreenshotProtectionManager
{
    private static UITextField? _secureTextField = null;
    private static UIView? _view = null;
    private static UIImageView? _imageView = null;

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
                if (preventScreenshot)
                {
                    if (window is not null)
                    {
                        _secureTextField = new();

                        _view = new(new CGRect(x: 0, y: 0, width: _secureTextField.Frame.Width, height: _secureTextField.Frame.Height));

                        _imageView = new(new UIImage(IOSHelpers.GetCurrentTheme() == ThemeStyle.Light ? "white_bg.png" : "black_bg.png"))
                        {
                            Frame = new CGRect(x: 0, y: 0, width: UIScreen.MainScreen.Bounds.Width, height: UIScreen.MainScreen.Bounds.Height)
                        };

                        _secureTextField.SecureTextEntry = true;

                        window.AddSubview(_secureTextField);
                        _view.AddSubview(_imageView);

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
                    
                    if (_imageView is not null)
                    {
                        _imageView.Layer.RemoveFromSuperLayer();
                        _imageView.RemoveFromSuperview();
                    }

                    if (_view is not null)
                    {
                        _view.Layer.RemoveFromSuperLayer();
                        _view.RemoveFromSuperview();
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