using ScreenSecuritySample.Views;

namespace ScreenSecuritySample;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        RegisterRoutes();
    }

    private static void RegisterRoutes()
    {
        // Android
        Routing.RegisterRoute("security_screen_test", typeof(SecurityScreenTestPage));
        Routing.RegisterRoute("secured", typeof(SecuredPage));

        // iOS
        Routing.RegisterRoute("blur_protection", typeof(BlurProtectionPage));
        Routing.RegisterRoute("color_protection", typeof(ColorProtectionPage));
        Routing.RegisterRoute("image_protection", typeof(ImageProtectionPage));
        Routing.RegisterRoute("recording_protection", typeof(RecordingProtectionPage));
    }
}