using Plugin.Maui.ScreenSecurity;

namespace ScreenSecuritySample.Views;

public partial class SecuredPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public SecuredPage(IScreenSecurity screenSecurity)
	{
		InitializeComponent();

        _screenSecurity = screenSecurity;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

#if ANDROID
        _screenSecurity.EnableScreenSecurityProtection();
#endif
    }

    protected override void OnDisappearing()
    {
#if ANDROID
        _screenSecurity.DisableScreenSecurityProtection();
#endif

        base.OnDisappearing();
    }
}