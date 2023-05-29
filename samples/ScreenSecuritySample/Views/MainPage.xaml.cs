namespace ScreenSecuritySample.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OpenSecurityScreenTestPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("security_screen_test");
    }

    private async void OpenSecuredPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("secured");
    }

    private async void OpenBlurProtectionPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("blur_protection");
    }

    private async void OpenColorProtectionPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("color_protection");
    }

    private async void OpenImageProtectionPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("image_protection");
    }

    private async void OpenRecordingProtectionPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("recording_protection");
    }

    private async void OpenScreenshotProtectionPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("screenshot_protection");
    }
}