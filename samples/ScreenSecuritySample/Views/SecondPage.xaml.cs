using Plugin.Maui.ScreenSecurity;

namespace ScreenSecuritySample.Views;

public partial class SecondPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public SecondPage(IScreenSecurity screenSecurity)
	{
		InitializeComponent();

        _screenSecurity = screenSecurity;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Deactivate the screen security protection.
        _screenSecurity.DeactivateScreenSecurityProtection();

        isEnabledLabel.Text = $"Screen protection enabled: {_screenSecurity.IsProtectionEnabled}";

        _screenSecurity.ScreenCaptured += OnScreenCaptured;
    }

    protected override void OnDisappearing()
    {
        _screenSecurity.ScreenCaptured -= OnScreenCaptured;

        base.OnDisappearing();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..", true);
    }

    private async void OnScreenCaptured(object sender, EventArgs e)
    {
        string title = "ScreenSecuritySample";
        string message = "Screenshot was taken.";

        await Shell.Current.DisplayAlert(title, message, "Ok");
    }
}