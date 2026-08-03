namespace Plugin.Maui.ScreenSecurity;

internal partial class ScreenSecurityImplementation : IScreenSecurity
{
    public void ActivateScreenSecurityProtection()
    {
        throw new PlatformNotSupportedException();
    }

    public void ActivateScreenSecurityProtection(bool blurScreenProtection, bool preventScreenshot, bool preventScreenRecording)
    {
        throw new PlatformNotSupportedException();
    }

    public void ActivateScreenSecurityProtection(ScreenProtectionOptions screenProtectionOptions)
    {
        throw new PlatformNotSupportedException();
    }

    public void DeactivateScreenSecurityProtection()
    {
        throw new PlatformNotSupportedException();
    }

    public bool IsProtectionEnabled { get; private set; }

    public bool ThrowErrors { get; set; }

    public event EventHandler<EventArgs>? ScreenCaptured;
}