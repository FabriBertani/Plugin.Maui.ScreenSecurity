using Android.OS;
using Android.Views;
using Plugin.Maui.ScreenSecurity.Handlers;

namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity
{
    /// <summary>
    /// Activates the screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents app <b>screenshots</b> or <b>recording</b> to be taken.
    /// </summary>   
    public void ActivateScreenSecurityProtection()
    {
        SetScreenSecurityProtection(true);
    }

    /// <summary>
    /// Activates the screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents app <b>screenshots</b> or <b>recording</b> to be taken.
    /// The specified parameters are for <b>iOS</b> only.
    /// </summary>
    /// <param name="blurScreenProtection">A boolean value indicates whether to blur the screen.</param>
    /// <param name="preventScreenshot">A boolean value that indicates whether to prevent screenshots.</param>
    /// <param name="preventScreenRecording">A boolean value that indicates whether to prevent screen recording.</param>
    /// <remarks>
    /// These parameters have <u><b>no effect</b></u> on <b>Android</b> and <b>Windows</b> platforms.
    /// </remarks>
    public void ActivateScreenSecurityProtection(bool blurScreenProtection, bool preventScreenshot, bool preventScreenRecording)
    {
        ActivateScreenSecurityProtection();
    }

    /// <summary>
    /// Activates the screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents app <b>screenshots</b> or <b>recording</b> to be taken.
    /// The specified parameters are for using a <u>Color</u> or an <u>Image</u> as protection on iOS only.
    /// </summary>
    /// <param name="screenProtectionOptions">
    /// ScreenProtectionOptions contains extra options for screen security protection,
    /// in order to customize the screen protection by specifying either a <b>Color</b> or an <b>Image</b> for iOS devices.
    /// </param>
    /// <remarks>
    /// These parameters have <u><b>no effect</b></u> on <b>Android</b> and <b>Windows</b> platforms.
    /// </remarks>
    public void ActivateScreenSecurityProtection(ScreenProtectionOptions screenProtectionOptions)
    {
        ActivateScreenSecurityProtection();
    }

    /// <summary>
    /// Deactivates all screen security protection.
    /// </summary>
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
#if NET7_0
#pragma warning disable CA1416
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
                        activity.SetRecentsScreenshotEnabled(!enabled);
#pragma warning restore CA1416
#endif

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