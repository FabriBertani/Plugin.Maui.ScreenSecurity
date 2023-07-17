using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal static class IOSHelpers
{
    internal static UIWindow? GetWindow()
    {
        if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
        {
            return UIApplication.SharedApplication.ConnectedScenes
                .OfType<UIWindowScene>()
                .SelectMany(s => s.Windows)
                .FirstOrDefault(w => w.IsKeyWindow);
        }
        else if (UIDevice.CurrentDevice.CheckSystemVersion(14, 2))
            return UIApplication.SharedApplication.Windows.FirstOrDefault(o => o.IsKeyWindow);
        else
            return UIApplication.SharedApplication.KeyWindow;
    }

    internal static ThemeStyle GetCurrentTheme()
    {
        return Application.Current?.RequestedTheme == AppTheme.Dark
            ? ThemeStyle.Dark : ThemeStyle.Light;
    }
}