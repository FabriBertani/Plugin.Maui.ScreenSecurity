using Plugin.Maui.ScreenSecurity;
using ScreenSecuritySample.Views;

namespace ScreenSecuritySample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseScreenSecurity();

        // Register Main Page
        builder.Services.AddScoped<MainPage>();
        builder.Services.AddScoped<SecondPage>();

        return builder.Build();
    }
}