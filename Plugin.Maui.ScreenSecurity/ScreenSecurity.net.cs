﻿namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity
{
    public void ActivateScreenSecurityProtection()
    {
        throw new NotImplementedException();
    }

    public void ActivateScreenSecurityProtection(bool blurScreenProtection, bool preventScreenshot, bool preventScreenRecording)
    {
        throw new NotImplementedException();
    }

    public void ActivateScreenSecurityProtection(ScreenProtectionOptions screenProtectionOptions)
    {
        throw new NotImplementedException();
    }

    public void DeactivateScreenSecurityProtection()
    {
        throw new NotImplementedException();
    }

    public bool IsProtectionEnabled { get; private set; }

    public event EventHandler<EventArgs>? ScreenCaptured;
}