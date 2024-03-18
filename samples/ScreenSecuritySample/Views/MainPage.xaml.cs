using Plugin.Maui.ScreenSecurity;

namespace ScreenSecuritySample.Views;

public partial class MainPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public MainPage(IScreenSecurity screenSecurity)
    {
        InitializeComponent();

        _screenSecurity = screenSecurity;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Activate the screen security protection with default settings
        _screenSecurity.ActivateScreenSecurityProtection();

        /*
        // For changing iOS options, follow one of the next examples:

        // Example 1: Customize with a specific color
        var screenProtectionOptions = new ScreenProtectionOptions
        {
            HexColor = "#6C4675",
            PreventScreenshot = true,
            PreventScreenRecording = false
        };

        // Example 2: Customize with an image
        var screenProtectionOptions = new ScreenProtectionOptions
        {
            Image = "protection_bg.png"
            PreventScreenshot = false,
            PreventScreenRecording = true
        };

        _screenSecurity.ActivateScreenSecurityProtection(screenProtectionOptions);
        */
    }

    protected override void OnDisappearing()
    {
        _screenSecurity.DeactivateScreenSecurityProtection();

        base.OnDisappearing();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("unprotected_page", true);
    }
}