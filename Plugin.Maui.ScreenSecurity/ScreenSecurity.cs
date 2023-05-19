namespace Plugin.Maui.ScreenSecurity;

public static class ScreenSecurity
{
    /// <summary>
    /// Provides the default implementation for static usage of this API.
    /// </summary>
    static IScreenSecurity? defaultImplementation;

    public static IScreenSecurity? Default => defaultImplementation ??= new ScreenSecurityImplementation();

    internal static void SetDefault(IScreenSecurity? implementation) => defaultImplementation = implementation;
}