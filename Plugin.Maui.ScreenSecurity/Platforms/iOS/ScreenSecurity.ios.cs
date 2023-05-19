using Plugin.Maui.ScreenSecurity.Platforms.iOS;
using UIKit;

namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity
{
    private UIWindow? _window = null;

    private UIImageView? _screenImage = null;

    private UIView? _screenBlur = null;

    private UIView? _screenColor = null;

    /// <summary>
    /// Prevent screen content from being exposed by using a <b>Blur layer</b> when the app 
    /// is sent to <b>Background</b> or the <b>App Switcher</b>.
    /// </summary>
    /// <param name="style">
    /// Choose between a <b><c>Light</c></b> or <b><c>Dark</c></b> theme for the Blur layer.
    /// <b><c>Light</c></b> by default.
    /// </param>
    public void EnableBlurScreenProtection(ThemeStyle style = ThemeStyle.Light)
    {
        UIApplication.Notifications.ObserveWillResignActive((sender, args) =>
        {
            try
            {
                EnableBlurScreen(style);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EnableBlurScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });

        UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
        {
            try
            {
                DisableBlurScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EnableBlurScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });
    }

    /// <summary>
    /// Removes the <b>Blur layer</b> and re-enables screen content exposure.
    /// </summary>
    public void DisableBlurScreenProtection()
    {
        UIApplication.Notifications.ObserveWillResignActive((sender, args) =>
        {
            try
            {
                DisableBlurScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DisableBlurScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });

        UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
        {
            try
            {
                DisableBlurScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DisableBlurScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });
    }

    /// <summary>
    /// Prevent screen content from being exposed by using a <b>Color layer</b> when the app is sent to
    /// <b>Background</b> or the <b>App Switcher</b>.
    /// </summary>
    /// <param name="hexColor">Hexadecimal color as <b><c>string</c></b> in the form of 
    /// <b><c>#RGB</c></b>, <b><c>#RGBA</c></b>, <b><c>#RRGGBB</c></b> or <b><c>#RRGGBBAA</c></b>.
    /// <b>#FFFFFF</b> by default.</param>
    public void EnableColorScreenProtection(string hexColor = "#FFFFFF")
    {
        UIApplication.Notifications.ObserveWillResignActive((sender, args) =>
        {
            try
            {
                EnableColorScreen(hexColor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EnableColorScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });

        UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
        {
            try
            {
                DisableColorScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EnableColorScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });
    }

    /// <summary>
    /// Removes the <b>Color layer</b> and re-enables screen content exposure.
    /// </summary>
    public void DisableColorScreenProtection()
    {
        UIApplication.Notifications.ObserveWillResignActive((sender, args) =>
        {
            try
            {
                DisableColorScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DisableColorScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });

        UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
        {
            try
            {
                DisableColorScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DisableColorScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });
    }

    /// <summary>
    /// Prevent screen content from being exposed by using an <b>Image</b> when the app is sent to
    /// <b>Background</b> or the <b>App Switcher</b>.
    /// </summary>
    /// <param name="image">Name with extension of the image to use.</param>
    public void EnableImageScreenProtection(string image)
    {
        UIApplication.Notifications.ObserveWillResignActive((sender, args) =>
        {
            try
            {
                EnableImageScreen(image);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EnableImageScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });

        UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
        {
            try
            {
                DisableImageScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EnableImageScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });
    }

    /// <summary>
    /// Removes the <b>Image</b> and re-enables screen content exposure.
    /// </summary>
    public void DisableImageScreenProtection()
    {
        UIApplication.Notifications.ObserveWillResignActive((sender, args) =>
        {
            try
            {
                DisableImageScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DisableImageScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });

        UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
        {
            try
            {
                DisableImageScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DisableImageScreenProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });
    }

    /// <summary>
    /// Prevent screen content from <b>being recorded</b> by the system or external app.
    /// <b>It uses the Blur layer by default.</b>
    /// </summary>
    /// <param name="withColor">Hexadecimal color as <b><c>string</c></b> in the form of 
    /// <b><c>#RGB</c></b>, <b><c>#RGBA</c></b>, <b><c>#RRGGBB</c></b> or <b><c>#RRGGBBAA</c></b>. 
    /// It can be mixed with <paramref name="withBlur"/>.</param>
    /// <param name="withBlur">Set it to <b><c>false</c></b> to deactivate screen protection with Blur. 
    /// It can be mixed with <paramref name="withColor"/>. <b><c>True</c> by default.</b></param>
    public void EnableScreenRecordingProtection(string withColor = "", bool withBlur = true)
    {
        UIScreen.Notifications.ObserveCapturedDidChange((sender, args) =>
        {
            try
            {
                if (UIScreen.MainScreen.Captured)
                {
                    if (withBlur)
                        EnableBlurScreen(ThemeStyle.Light);

                    if (!string.IsNullOrEmpty(withColor))
                        EnableColorScreen(withColor);
                }
                else
                {
                    DisableBlurScreen();
                    DisableColorScreen();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EnableScreenRecordingProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });
    }

    /// <summary>
    /// Turn off the screen recording protection.
    /// </summary>
    public void DisableScreenRecordingProtection()
    {
        UIScreen.Notifications.ObserveCapturedDidChange((sender, args) =>
        {
            try
            {
                if (UIScreen.MainScreen.Captured)
                {
                    DisableBlurScreen();
                    DisableColorScreen();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DisableScreenRecordingProtection failed with Exception message: {ex.Message}");
                Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"With InnerException: {ex.InnerException}");
            }
        });
    }

    private void GetWindow()
    {
        if (_window == null)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
            {
                _window = UIApplication.SharedApplication.ConnectedScenes
                    .OfType<UIWindowScene>()
                    .SelectMany(s => s.Windows)
                    .FirstOrDefault(w => w.IsKeyWindow);
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(14, 2))
                _window = UIApplication.SharedApplication.Windows.FirstOrDefault(o => o.IsKeyWindow);
            else
                _window = UIApplication.SharedApplication.KeyWindow;
        }
    }

    private void EnableBlurScreen(ThemeStyle style)
    {
        GetWindow();

        _screenBlur = UIScreen.MainScreen.SnapshotView(false);

        UIBlurEffectStyle blurEffectStyle =
            style == ThemeStyle.Light
                ? UIBlurEffectStyle.Light
                : UIBlurEffectStyle.Dark;

        var blurEffect = UIBlurEffect.FromStyle(blurEffectStyle);

        var blurBackground = new UIVisualEffectView(blurEffect);

        _screenBlur?.AddSubview(blurBackground);

        blurBackground.Frame = (CoreGraphics.CGRect)_screenBlur?.Frame;

        _window?.AddSubview(_screenBlur);
    }

    private void DisableBlurScreen()
    {
        _screenBlur?.RemoveFromSuperview();
        _screenBlur = null;
    }

    private void EnableColorScreen(string hexColor)
    {
        GetWindow();

        if (_window != null)
        {
            _screenColor = new UIView(_window.Bounds)
            {
                BackgroundColor = UIColor.Clear.FromHex(hexColor)
            };

            _window.AddSubview(_screenColor);
        }
    }

    private void DisableColorScreen()
    {
        _screenColor?.RemoveFromSuperview();
        _screenColor = null;
    }

    private void EnableImageScreen(string image)
    {
        GetWindow();

        _screenImage = new UIImageView(UIScreen.MainScreen.Bounds)
        {
            Image = new UIImage(image),
            UserInteractionEnabled = false,
            ContentMode = UIViewContentMode.ScaleAspectFill,
            ClipsToBounds = true
        };

        _window?.AddSubview(_screenImage);
    }

    private void DisableImageScreen()
    {
        _screenImage?.RemoveFromSuperview();
        _screenImage = null;
    }
}