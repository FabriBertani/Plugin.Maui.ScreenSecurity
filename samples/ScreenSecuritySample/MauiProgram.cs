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

        // Register Android Pages
        builder.Services.AddScoped<SecuredPage>();
        builder.Services.AddScoped<SecurityScreenTestPage>();

        // Register iOS Pages
        builder.Services.AddScoped<BlurProtectionPage>();
        builder.Services.AddScoped<ColorProtectionPage>();
        builder.Services.AddScoped<ImageProtectionPage>();
        builder.Services.AddScoped<RecordingProtectionPage>();
        builder.Services.AddScoped<ScreenshotProtectionIOSPage>();

        // Register Windows Pages
        builder.Services.AddScoped<ScreenshotProtectionPage>();

        // Register Main Page
        builder.Services.AddScoped<MainPage>();

        // Register plugin as Singleton
        builder.Services.AddSingleton<IScreenSecurity>(ScreenSecurity.Default);

        return builder.Build();
    }
}