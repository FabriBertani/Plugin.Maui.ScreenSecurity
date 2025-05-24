#if IOS
using Plugin.Maui.ScreenSecurity.Platforms.iOS;
#endif

namespace Plugin.Maui.ScreenSecurity;

public interface IScreenSecurity
{
    /// <summary>
    /// Activates screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents <b>screenshots</b> or <b>screen recordings</b> from being taken.
    /// </summary>
    void ActivateScreenSecurityProtection();

    /// <summary>
    /// Activates screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents <b>screenshots</b> or <b>screen recordings</b> from being taken.
    /// The specified parameters apply to <b>iOS</b> only.
    /// </summary>
    /// <param name="blurScreenProtection">Indicates whether to blur the screen.</param>
    /// <param name="preventScreenshot">Indicates whether to prevent screenshots.</param>
    /// <param name="preventScreenRecording">Indicates whether to prevent screen recording.</param>
    /// <remarks>
    /// These parameters have <i><b>no effect</b></i> on <b>Android</b> and <b>Windows</b> platforms.
    /// </remarks>
    void ActivateScreenSecurityProtection(bool blurScreenProtection, bool preventScreenshot, bool preventScreenRecording);

    /// <summary>
    /// Activates screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents <b>screenshots</b> or <b>screen recordings</b> from being taken.
    /// The specified parameters are for using a <i>Color</i> or an <i>Image</i> as protection on iOS only.
    /// </summary>
    /// <param name="screenProtectionOptions">
    /// Provides additional settings for screen security on iOS,
    /// allowing customization using either a <b>Color</b> or an <b>Image</b>.
    /// </param>
    /// <remarks>
    /// These parameters have <i><b>no effect</b></i> on <b>Android</b> and <b>Windows</b> platforms.
    /// </remarks>
    void ActivateScreenSecurityProtection(ScreenProtectionOptions screenProtectionOptions);

    /// <summary>
    /// Deactivates all screen security protection.
    /// </summary>
    void DeactivateScreenSecurityProtection();

    /// <summary>
    /// Indicates whether screen protection is currently enabled.
    /// </summary>
    bool IsProtectionEnabled { get; }

    /// <summary>
    /// Triggered when the screen is captured, either via screenshot or recording.
    /// </summary>
    event EventHandler<EventArgs>? ScreenCaptured;
}