using CommunityToolkit.Maui;
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
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register Main Page
        builder.Services.AddScoped<MainPage>();

        // Register plugin as Singleton
        builder.Services.AddSingleton<IScreenSecurity>(ScreenSecurity.Default);

        return builder.Build();
    }
}