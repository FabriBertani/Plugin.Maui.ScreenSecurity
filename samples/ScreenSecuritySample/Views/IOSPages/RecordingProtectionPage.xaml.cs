using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Plugin.Maui.ScreenSecurity;

namespace ScreenSecuritySample.Views;

public partial class RecordingProtectionPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public RecordingProtectionPage(IScreenSecurity screenSecurity)
	{
		InitializeComponent();

        _screenSecurity = screenSecurity;
    }

    private async void ActivateRecordingProtection_Clicked(object sender, EventArgs e)
    {
#if IOS
        _screenSecurity.EnableScreenRecordingProtection("#606E8C");
#endif

        var toast = Toast.Make("Recording Protection Enabled.", ToastDuration.Short, 14);

        await toast.Show();
    }

    private async void DeactivateRecordingProtection_Clicked(object sender, EventArgs e)
    {
#if IOS
        _screenSecurity.DisableScreenRecordingProtection();
#endif

        var toast = Toast.Make("Recording Protection Disabled.", ToastDuration.Short, 14);

        await toast.Show();
    }
}