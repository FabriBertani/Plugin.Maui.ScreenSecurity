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
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..", true);
    }
}