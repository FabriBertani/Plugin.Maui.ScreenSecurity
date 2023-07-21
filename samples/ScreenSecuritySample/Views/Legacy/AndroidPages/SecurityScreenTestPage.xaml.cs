using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Plugin.Maui.ScreenSecurity;

namespace ScreenSecuritySample.Views;

public partial class SecurityScreenTestPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public SecurityScreenTestPage(IScreenSecurity screenSecurity)
	{
		InitializeComponent();

        _screenSecurity = screenSecurity;
    }

    private async void ActivateScreenSecurity_Clicked(object sender, EventArgs e)
    {
#if ANDROID
        _screenSecurity.EnableScreenSecurityProtection();
#endif

        var toast = Toast.Make("Screen security enabled.", ToastDuration.Short, 14);

        await toast.Show();
    }

    private async void DeactivateScreenSecurity_Clicked(object sender, EventArgs e)
    {
#if ANDROID
        _screenSecurity.DisableScreenSecurityProtection();
#endif

        var toast = Toast.Make("Screen security disabled.", ToastDuration.Short, 14);

        await toast.Show();
    }
}