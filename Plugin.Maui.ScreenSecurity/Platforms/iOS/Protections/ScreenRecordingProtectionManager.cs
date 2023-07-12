using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ScreenRecordingProtectionManager
{
    internal static void HandleScreenRecordingProtection(bool enabled, string withColor = "", UIWindow? window = null)
    {
        UIScreen.Notifications.ObserveCapturedDidChange((sender, args) =>
        {
            try
            {
                if (UIScreen.MainScreen.Captured)
                {
                    if (enabled)
                    {
                        if (!string.IsNullOrEmpty(withColor))
                            ColorProtectionManager.EnableColor(window, withColor);
                        else
                            BlurProtectionManager.EnableBlur(window, ThemeStyle.Light);
                    }
                    else
                        DisableScreenRecordingProtection();
                }
                else
                    DisableScreenRecordingProtection();
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(HandleScreenRecordingProtection), ex);
            }
        });
    }

    private static void DisableScreenRecordingProtection()
    {
        BlurProtectionManager.DisableBlur();

        ColorProtectionManager.DisableColor();
    }
}