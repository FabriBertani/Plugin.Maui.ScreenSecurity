namespace ScreenSecuritySample.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OpenSecurityScreenTestPage_Clicked(object sender, EventArgs e)
    {
        Navigate("security_screen_test");
    }

    private void OpenSecuredPage_Clicked(object sender, EventArgs e)
    {
        Navigate("secured");
    }

    private void OpenBlurProtectionPage_Clicked(object sender, EventArgs e)
    {
        Navigate("blur_protection");        
    }

    private void OpenColorProtectionPage_Clicked(object sender, EventArgs e)
    {
        Navigate("color_protection");
    }

    private void OpenImageProtectionPage_Clicked(object sender, EventArgs e)
    {
        Navigate("image_protection");
    }

    private void OpenRecordingProtectionPage_Clicked(object sender, EventArgs e)
    {
        Navigate("recording_protection");
    }

    private void OpenScreenshotProtectionPage_Clicked(object sender, EventArgs e)
    {
        Navigate("screenshot_protection");
    }

    private void OpenIOSScreenshotProtectionPage_Clicked(object sender, EventArgs e)
    {
        Navigate("ios_screenshot_protection");
    }

    private static void Navigate(string to)
    {
        MainThread.InvokeOnMainThreadAsync(async () =>
            await Shell.Current.GoToAsync(to)
        );
    }
}