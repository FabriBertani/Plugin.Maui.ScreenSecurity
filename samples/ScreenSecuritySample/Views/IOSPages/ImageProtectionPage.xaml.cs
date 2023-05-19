using CommunityToolkit.Maui.Core;
using Plugin.Maui.ScreenSecurity;

namespace ScreenSecuritySample.Views;

public partial class ImageProtectionPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public ImageProtectionPage(IScreenSecurity screenSecurity)
	{
		InitializeComponent();

        _screenSecurity = screenSecurity;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

#if IOS
        _screenSecurity.EnableImageScreenProtection("protection_bg.png");
#endif
    }

    protected override void OnDisappearing()
    {
#if IOS
        _screenSecurity.DisableImageScreenProtection();
#endif

        base.OnDisappearing();
    }
}