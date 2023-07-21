using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Plugin.Maui.ScreenSecurity;

namespace ScreenSecuritySample.Views;

public partial class ScreenshotProtectionIOSPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public ScreenshotProtectionIOSPage(IScreenSecurity screenSecurity)
	{
		InitializeComponent();

        _screenSecurity = screenSecurity;
    }

    private async void EnableScreenshotProtection_Clicked(object sender, EventArgs e)
    {
#if IOS
        _screenSecurity.EnableScreenshotProtection();
#endif

        var toast = Toast.Make("Screenshot Protection Enabled.", ToastDuration.Short, 14);

        await toast.Show();
    }

    private async void DisableScreenshotProtection_Clicked(object sender, EventArgs e)
    {
#if IOS
        _screenSecurity.DisableScreenshotProtection();
#endif

        var toast = Toast.Make("Screenshot Protection Disabled.", ToastDuration.Short, 14);

        await toast.Show();
    }
}