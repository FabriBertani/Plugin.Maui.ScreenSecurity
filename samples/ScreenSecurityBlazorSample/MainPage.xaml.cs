using Plugin.Maui.ScreenSecurity;

namespace ScreenSecurityBlazorSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Uncomment this to enable screen protection on the entire app.
        // Note: If you open the Unprotected Page, protection will be disabled.
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    ScreenSecurity.Default?.ActivateScreenSecurityProtection();
        //}
    }
}
