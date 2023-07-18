using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ImageProtectionManager
{
    private static UIImageView? _screenImage = null;

    internal static void HandleImageProtection(bool enabled, string image = "", UIWindow? window = null)
    {
        UIApplication.Notifications.ObserveWillResignActive((sender, args) =>
        {
            try
            {
                if (enabled)
                    EnableImageScreenProtection(image, window);
                else
                    DisableImageScreenProtection();
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(HandleImageProtection), ex);
            }
        });

        UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
        {
            try
            {
                DisableImageScreenProtection();
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(HandleImageProtection), ex);
            }
        });
    }

    private static void EnableImageScreenProtection(string image, UIWindow? window)
    {
        if (window != null)
        {
            DisableImageScreenProtection();

            if (!string.IsNullOrEmpty(image))
            {
                if (File.Exists(image))
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
                }
            }
        }
    }

    private static void DisableImageScreenProtection()
    {
        _screenImage?.RemoveFromSuperview();

        _screenImage?.Dispose();

        _screenImage = null;
    }
}