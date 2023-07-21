#if IOS
using Plugin.Maui.ScreenSecurity.Platforms.iOS;
#endif

namespace Plugin.Maui.ScreenSecurity;

public interface IScreenSecurity
{
    /// <summary>
    /// Activates the screen security protection when the app is sent
    /// to <b>Recents screen</b> or the <b>App Switcher</b>.
    /// Also prevents app <b>screenshots</b> or <b>recording</b> to be taken.
    /// </summary>
    void ActivateScreenSecurityProtection();

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
    void ActivateScreenSecurityProtection(bool blurScreenProtection, bool preventScreenshot, bool preventScreenRecording);

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
    void ActivateScreenSecurityProtection(ScreenProtectionOptions screenProtectionOptions);

    /// <summary>
    /// Deactivates all screen security protection.
    /// </summary>
    void DeactivateScreenSecurityProtection();

#if ANDROID
    /// <summary>
    /// Prevent screen content from being exposed when the app
    /// is sent to <b>Background</b> or the <b>Recents screen</b>.
    /// Also prevents app <b>screenshots</b> or <b>recording</b> to be taken.
    /// </summary>
    /// <remarks>Supported for <b><c>Android</c></b> only.</remarks>
    void EnableScreenSecurityProtection();

    /// <summary>
    /// Re-enables screen content exposure.
    /// </summary>
    /// <remarks>Supported for <b><c>Android</c></b> only.</remarks>
    void DisableScreenSecurityProtection();
#elif IOS
    /// <summary>
    /// Prevent screen content from being exposed by 
    /// using a <b>Blur layer</b> when the app is sent to 
    /// <b>Background</b> or the <b>App Switcher</b>.
    /// </summary>
    /// <param name="style">
    /// Choose between a <b><c>Light</c></b> or <b><c>Dark</c></b> theme for the Blur layer.
    /// <b><c>Light</c></b> by default.
    /// </param>
    /// <remarks>Supported for <b><c>iOS</c></b> only.</remarks>
    void EnableBlurScreenProtection(ThemeStyle style);

    /// <summary>
    /// Removes the <b>Blur layer</b> and re-enables screen content exposure.
    /// </summary>
    /// <remarks>Supported for <b><c>iOS</c></b> only.</remarks>
    void DisableBlurScreenProtection();

    /// <summary>
    /// Prevent screen content from being exposed by
    /// using a <b>Color layer</b> when the app is sent to
    /// <b>Background</b> or the <b>App Switcher</b>.
    /// </summary>
    /// <param name="hexColor">Hexadecimal color as <b><c>string</c></b> in the form of 
    /// <b><c>#RGB</c></b>, <b><c>#RGBA</c></b>, <b><c>#RRGGBB</c></b> or <b><c>#RRGGBBAA</c></b>.
    /// <b>#FFFFFF</b> by default.</param>
    /// <remarks>Supported for <b><c>iOS</c></b> only.</remarks>
    void EnableColorScreenProtection(string hexColor);

    /// <summary>
    /// Removes the <b>Color layer</b> and re-enables screen content exposure.
    /// </summary>
    /// <remarks>Supported for <b><c>iOS</c></b> only.</remarks>
    void DisableColorScreenProtection();

    /// <summary>
    /// Prevent screen content from being exposed by
    /// using an <b>Image</b> when the app is sent to
    /// <b>Background</b> or the <b>App Switcher</b>.
    /// </summary>
    /// <param name="image">Name with extension of the image to use.</param>
    /// <remarks>Supported for <b><c>iOS</c></b> only.</remarks>
    void EnableImageScreenProtection(string image);

    /// <summary>
    /// Removes the <b>Image</b> and re-enables screen content exposure.
    /// </summary>
    /// <remarks>Supported for <b><c>iOS</c></b> only.</remarks>
    void DisableImageScreenProtection();

    /// <summary>
    /// Prevent screen content from <b>being recorded</b> 
    /// by the system or external app. <b>It uses the 
    /// Blur layer by default.</b>
    /// </summary>
    /// <param name="withColor">Hexadecimal color as <b><c>string</c></b> in the form of 
    /// <b><c>#RGB</c></b>, <b><c>#RGBA</c></b>, <b><c>#RRGGBB</c></b> or <b><c>#RRGGBBAA</c></b>. 
    /// It can be mixed with <paramref name="withBlur"/>.</param>
    /// <param name="withBlur">Set it to <b><c>false</c></b> to deactivate screen protection with Blur. 
    /// It can be mixed with <paramref name="withColor"/>. <b><c>True</c> by default.</b></param>
    /// <remarks>Supported for <b><c>iOS</c></b> only.</remarks>
    void EnableScreenRecordingProtection(string withColor = "", bool withBlur = true);

    /// <summary>
    /// Turn off the screen recording protection.
    /// </summary>
    /// <remarks>Supported for <b><c>iOS</c></b> only.</remarks>
    void DisableScreenRecordingProtection();

    /// <summary>
    /// Prevent screen content from being exposed when taking a <b><c>screenshot</c></b>
    /// by placing a black screen.
    /// </summary>
    /// <remarks>Supported for <b><c>iOS</c></b> only.</remarks>
    void EnableScreenshotProtection();

    /// <summary>
    /// Turn off the screenshot protection.
    /// </summary>
    /// <remarks>Supported for <b><c>iOS</c></b> only.</remarks>
    void DisableScreenshotProtection();
#elif WINDOWS
    /// <summary>
    /// Prevent screen content from being exposed when taking
    /// a <b><c>screenshot</c></b> by the system or any external app.
    /// </summary>
    /// <remarks>Supported for <b><c>Windows</c></b> only.</remarks>
    void EnableScreenshotProtection();

    /// <summary>
    /// Re-enables content exposure when taking a screenshot.
    /// </summary>
    /// <remarks>Supported for <b><c>Windows</c></b> only.</remarks>
    void DisableScreenshotProtection();
#endif
}