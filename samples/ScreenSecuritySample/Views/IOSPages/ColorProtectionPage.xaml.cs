using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Plugin.Maui.ScreenSecurity;

namespace ScreenSecuritySample.Views;

public partial class ColorProtectionPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public ColorProtectionPage(IScreenSecurity screenSecurity)
	{
		InitializeComponent();
        
        _screenSecurity = screenSecurity;
    }

    private async void ActivateColorProtection_Clicked(object sender, EventArgs e)
    {
#if IOS
        var random = new Random();
        var color = string.Format("#{0:X6}", random.Next(0x1000000));

        _screenSecurity.EnableColorScreenProtection(color);
#endif

        var toast = Toast.Make("Color Protection Enabled.", ToastDuration.Short, 14);

        await toast.Show();
    }

    private async void DeactivateColorProtection_Clicked(object sender, EventArgs e)
    {
#if IOS
        _screenSecurity.DisableColorScreenProtection();
#endif

        var toast = Toast.Make("Color Protection Disabled.", ToastDuration.Short, 14);

        await toast.Show();
    }
}