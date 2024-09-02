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
#if NET8_0
                if (UIDevice.CurrentDevice.CheckSystemVersion(17, 0))
                {
                    if (UITraitCollection.CurrentTraitCollection.SceneCaptureState == UISceneCaptureState.Active)
                    {
                        if (enabled)
                            EnableScreenRecordingProtection(withColor, window);
                        else
                            DisableScreenRecordingProtection(window);
                    }
                }
                else
                    DisableScreenRecordingProtection(window);
#elif NET7_0
                if (UIScreen.MainScreen.Captured)
                {
                    if (enabled)
                        EnableScreenRecordingProtection(withColor, window);
                    else
                        DisableScreenRecordingProtection(window);
                }
                else
                    DisableScreenRecordingProtection(window);
#endif
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(HandleScreenRecordingProtection), ex);
            }
        });
    }

    private static void EnableScreenRecordingProtection(string withColor = "", UIWindow? window = null)
    {
        if (!string.IsNullOrEmpty(withColor))
            ColorProtectionManager.EnableColor(window, withColor);
        else
            BlurProtectionManager.EnableBlur(window, ThemeStyle.Light);
    }

    private static void DisableScreenRecordingProtection(UIWindow? window)
    {
        BlurProtectionManager.DisableBlur(window);

        ColorProtectionManager.DisableColor();
    }
}