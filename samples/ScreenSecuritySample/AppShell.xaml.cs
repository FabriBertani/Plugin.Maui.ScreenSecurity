using ScreenSecuritySample.Views;

namespace ScreenSecuritySample;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        RegisterRoutes();
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute("unprotected_page", typeof(SecondPage));
    }
}