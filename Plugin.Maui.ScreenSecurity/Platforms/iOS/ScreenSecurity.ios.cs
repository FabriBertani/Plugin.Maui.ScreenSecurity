using Plugin.Maui.ScreenSecurity.Platforms.iOS;
using UIKit;

namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity
{
    private readonly Lazy<UIWindow?> _window = new(IOSHelpers.GetWindow);

    public void ActivateScreenSecurityProtection()
    {
        var style = IOSHelpers.GetCurrentTheme();

        BlurProtectionManager.HandleBlurProtection(true, style, _window.Value);

        HandleScreenCaptureProtection(true, true);
    }

    public void ActivateScreenSecurityProtection(bool preventScreenshot = true, bool preventScreenRecording = true)
    {
        BlurProtectionManager.HandleBlurProtection(true, IOSHelpers.GetCurrentTheme(), _window.Value);

        HandleScreenCaptureProtection(preventScreenshot, preventScreenRecording);
    }

    public void ActivateScreenSecurityProtection(ScreenProtectionOptions screenProtectionOptions)
    {
        if (!string.IsNullOrEmpty(screenProtectionOptions.HexColor)
            && string.IsNullOrEmpty(screenProtectionOptions.Image))
        {
            if (screenProtectionOptions.HexColor.IsHexColor())
                ColorProtectionManager.HandleColorProtection(true, screenProtectionOptions.HexColor, _window.Value);
            else
                throw new ArgumentException($"{screenProtectionOptions.HexColor} is not a valid hexadecimal color.");
        }
        else if (!string.IsNullOrEmpty(screenProtectionOptions.Image)
            && string.IsNullOrEmpty(screenProtectionOptions.HexColor))
        {
            if (screenProtectionOptions.Image.IsValidImage())
                ImageProtectionManager.HandleImageProtection(true, screenProtectionOptions.Image, _window.Value);
            else
                throw new ArgumentException($"{screenProtectionOptions.Image} is not a valid image format.");
        }

        HandleScreenCaptureProtection(screenProtectionOptions.PreventScreenshot, screenProtectionOptions.PreventScreenRecording);
    }

    public void DeactivateScreenSecurityProtection()
    {
        BlurProtectionManager.HandleBlurProtection(false);

        ColorProtectionManager.HandleColorProtection(false);

        ImageProtectionManager.HandleImageProtection(false);

        ScreenRecordingProtectionManager.HandleScreenRecordingProtection(false);

        ScreenshotProtectionManager.HandleScreenshotProtection(false);
    }

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
        BlurProtectionManager.HandleBlurProtection(true, style, _window.Value);
    }

    /// <summary>
    /// Removes the <b>Blur layer</b> and re-enables screen content exposure.
    /// </summary>
    public void DisableBlurScreenProtection()
    {
        BlurProtectionManager.HandleBlurProtection(false);
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
        ColorProtectionManager.HandleColorProtection(true, hexColor, _window.Value);
    }

    /// <summary>
    /// Removes the <b>Color layer</b> and re-enables screen content exposure.
    /// </summary>
    public void DisableColorScreenProtection()
    {
        ColorProtectionManager.HandleColorProtection(false);
    }

    /// <summary>
    /// Prevent screen content from being exposed by using an <b>Image</b> when the app is sent to
    /// <b>Background</b> or the <b>App Switcher</b>.
    /// </summary>
    /// <param name="image">Name with extension of the image to use.</param>
    public void EnableImageScreenProtection(string image)
    {
        ImageProtectionManager.HandleImageProtection(true, image, _window.Value);
    }

    /// <summary>
    /// Removes the <b>Image</b> and re-enables screen content exposure.
    /// </summary>
    public void DisableImageScreenProtection()
    {
        ImageProtectionManager.HandleImageProtection(false);
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
        ScreenRecordingProtectionManager.HandleScreenRecordingProtection(true, withColor, _window.Value);
    }

    /// <summary>
    /// Turn off the screen recording protection.
    /// </summary>
    public void DisableScreenRecordingProtection()
    {
        ScreenRecordingProtectionManager.HandleScreenRecordingProtection(false);
    }

    /// <summary>
    /// Prevent screen content from being exposed when taking a <b><c>screenshot</c></b>
    /// by placing a black screen.
    /// </summary>
    public void EnableScreenshotProtection()
    {
        ScreenshotProtectionManager.HandleScreenshotProtection(true, _window.Value);
    }

    /// <summary>
    /// Turn off the screenshot protection.
    /// </summary>
    public void DisableScreenshotProtection()
    {
        ScreenshotProtectionManager.HandleScreenshotProtection(false);
    }

    private void HandleScreenCaptureProtection(bool preventScreenshot, bool preventScreenRecording)
    {
        ScreenshotProtectionManager.HandleScreenshotProtection(preventScreenshot, _window.Value);

        ScreenRecordingProtectionManager.HandleScreenRecordingProtection(preventScreenRecording, string.Empty, _window.Value);
    }
}