namespace Plugin.Maui.ScreenSecurity;

public static class ScreenSecurity
{
    /// <summary>
    /// Provides the default implementation for static usage of this API.
    /// </summary>
    private static IScreenSecurity? _defaultImplementation;

    public static IScreenSecurity Default => _defaultImplementation ??= new ScreenSecurityImplementation();

    internal static void SetDefault(IScreenSecurity? implementation) => _defaultImplementation = implementation;
}