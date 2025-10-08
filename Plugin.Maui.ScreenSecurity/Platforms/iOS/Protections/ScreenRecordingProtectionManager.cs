using Plugin.Maui.ScreenSecurity.Handlers;
using UIKit;

namespace Plugin.Maui.ScreenSecurity.Platforms.iOS;

internal class ScreenRecordingProtectionManager
{
    internal static void HandleScreenRecordingProtection(bool enabled, bool throwErrors, string withColor = "", UIWindow? window = null)
    {
        UIScreen.Notifications.ObserveCapturedDidChange((sender, args) =>
        {
            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(17, 0))
                {
                    if (UITraitCollection.CurrentTraitCollection.SceneCaptureState != UISceneCaptureState.Active)
                        return;

                    if (enabled)
                        EnableScreenRecordingProtection(throwErrors, withColor, window);
                    else
                        DisableScreenRecordingProtection(window, throwErrors);
                }
                else
                    DisableScreenRecordingProtection(window, throwErrors);
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(HandleScreenRecordingProtection), throwErrors, ex);
            }
        });
    }

    private static void EnableScreenRecordingProtection(bool throwErrors, string withColor = "", UIWindow? window = null)
    {
        if (!string.IsNullOrEmpty(withColor))
            ColorProtectionManager.EnableColor(window, withColor, throwErrors);
        else
            BlurProtectionManager.EnableBlur(window, ThemeStyle.Light, throwErrors);
    }

    private static void DisableScreenRecordingProtection(UIWindow? window, bool throwErrors)
    {
        BlurProtectionManager.DisableBlur(window, throwErrors);

        ColorProtectionManager.DisableColor(throwErrors);
    }
}