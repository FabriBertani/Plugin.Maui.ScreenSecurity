using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Plugin.Maui.ScreenSecurity;

namespace ScreenSecuritySample.Views;

public partial class ScreenshotProtectionPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public ScreenshotProtectionPage(IScreenSecurity screenSecurity)
	{
		InitializeComponent();

        _screenSecurity = screenSecurity;
    }

    private async void ActivateScreenshotProtection_Clicked(object sender, EventArgs e)
    {
#if WINDOWS
        _screenSecurity.EnableScreenshotProtection();
#endif

        var toast = Toast.Make("Screenshot Protection Enabled.", ToastDuration.Short, 14);

        await toast.Show();
    }

    private async void DeactivateScreenshotProtection_Clicked(object sender, EventArgs e)
    {
#if WINDOWS
        _screenSecurity.DisableScreenshotProtection();
#endif

        var toast = Toast.Make("Screenshot Protection Disabled.", ToastDuration.Short, 14);

        await toast.Show();
    }
}