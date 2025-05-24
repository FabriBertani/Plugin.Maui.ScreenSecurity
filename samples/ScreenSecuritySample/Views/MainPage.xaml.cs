using Plugin.Maui.ScreenSecurity;

namespace ScreenSecuritySample.Views;

public partial class MainPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    private readonly ScreenProtectionOptions _hexProtection = new()
    {
        HexColor = "#6C4675",
        PreventScreenshot = true,
        PreventScreenRecording = false
    };

    private readonly ScreenProtectionOptions _imageProtection = new()
    {
        Image = "protection_bg.png",
        PreventScreenshot = true,
        PreventScreenRecording = true
    };

    private bool _hexProtectionEnabled = false;
    private bool _imageProtectionEnabled = false;

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
        _screenSecurity.ActivateScreenSecurityProtection(_hexProtection);
        _hexProtectionEnabled = true;

        // Example 2: Customize with an image
        _screenSecurity.ActivateScreenSecurityProtection(_imageProtection);
        _imageProtectionEnabled = true;
        */

        CheckStatusButton();

        isEnabledLabel.Text = $"Screen protection enabled: {_screenSecurity.IsProtectionEnabled}";
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

    private void ActivationBtn_Clicked(object sender, EventArgs e)
    {
        if (!_screenSecurity.IsProtectionEnabled)
        {
            var protectionType = (_hexProtectionEnabled, _imageProtectionEnabled) switch
            {
                (true, false) => _hexProtection,
                (false, true) => _imageProtection,
                _ => null
            };

            if (protectionType is not null)
                _screenSecurity.ActivateScreenSecurityProtection(protectionType);
            else
                _screenSecurity.ActivateScreenSecurityProtection();
        }
        else
            _screenSecurity.DeactivateScreenSecurityProtection();

        CheckStatusButton();

        isEnabledLabel.Text = $"Screen protection enabled: {_screenSecurity.IsProtectionEnabled}";
    }

    private void CheckStatusButton()
    {
        activationBtn.Text = _screenSecurity.IsProtectionEnabled
            ? "Deactivate Screen Protection"
            : "Activate Screen Protection";
    }
}