using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Plugin.Maui.ScreenSecurity;
#if IOS
using Plugin.Maui.ScreenSecurity.Platforms.iOS;
#endif

namespace ScreenSecuritySample.Views;

public partial class BlurProtectionPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public BlurProtectionPage(IScreenSecurity screenSecurity)
	{
		InitializeComponent();

        _screenSecurity = screenSecurity;
    }

    private async void ActivateBlurProtection_Clicked(object sender, EventArgs e)
    {
#if IOS
        var theme = Application.Current.RequestedTheme;

        var style = theme == AppTheme.Dark
            ? ThemeStyle.Dark
            : ThemeStyle.Light;

        _screenSecurity.EnableBlurScreenProtection(style);
#endif

        var toast = Toast.Make("Blur Protection Enabled.", ToastDuration.Short, 14);

        await toast.Show();
    }

    private async void DeactivateBlurProtection_Clicked(object sender, EventArgs e)
    {
#if IOS
        _screenSecurity.DisableBlurScreenProtection();
#endif

        var toast = Toast.Make("Blur Protection Disabled.", ToastDuration.Short, 14);

        await toast.Show();
    }
}