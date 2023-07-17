using Android.OS;
using Android.Views;
using Plugin.Maui.ScreenSecurity.Handlers;

namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity
{
    public void ActivateScreenSecurityProtection()
    {
        SetScreenSecurityProtection(true);
    }

    public void ActivateScreenSecurityProtection(bool preventScreenshot, bool preventScreenRecording)
    {
        ActivateScreenSecurityProtection();
    }

    public void ActivateScreenSecurityProtection(ScreenProtectionOptions screenProtectionOptions)
    {
        ActivateScreenSecurityProtection();
    }

    public void DeactivateScreenSecurityProtection()
    {
        SetScreenSecurityProtection(false);
    }

    /// <summary>
    /// Prevent screen content from being exposed when the app
    /// is sent to <b>Background</b> or the <b>Recents screen</b>.
    /// Also prevents app <b>screenshots</b> or <b>recording</b> to be taken.
    /// </summary>
    public void EnableScreenSecurityProtection()
    {
        SetScreenSecurityProtection(true);
    }

    /// <summary>
    /// Re-enables screen content exposure.
    /// </summary>
    public void DisableScreenSecurityProtection()
    {
        SetScreenSecurityProtection(false);
    }

    private void SetScreenSecurityProtection(bool enabled)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                var activity = Platform.CurrentActivity;

                if (activity != null)
                {
#pragma warning disable CA1416
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
                        activity.SetRecentsScreenshotEnabled(!enabled);
#pragma warning restore CA1416

                    if (enabled)
                        activity.Window?.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);
                    else
                        activity.Window?.ClearFlags(WindowManagerFlags.Secure);
                }
            }
            catch (Exception ex)
            {
                ErrorsHandler.HandleException(nameof(SetScreenSecurityProtection), ex);
            }
        });
    }
}