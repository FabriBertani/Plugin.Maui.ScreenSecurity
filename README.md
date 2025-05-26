# Plugin.Maui.ScreenSecurity
[![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.ScreenSecurity.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.ScreenSecurity) ![NuGet Downloads](https://img.shields.io/nuget/dt/Plugin.Maui.ScreenSecurity)
 [![Buy Me a Coffee](https://img.shields.io/badge/support-buy%20me%20a%20coffee-FFDD00)](https://buymeacoffee.com/fabribertani)

`Plugin.Maui.ScreenSecurity` provides a seamless solution for preventing content exposure, as well as blocking screenshots and recordings within your .NET MAUI application

## Platforms supported
|Platform|Version|
|-------------------|:------------------:|
|.Net MAUI Android|API 21+|
|.Net MAUI iOS|iOS 14+|
|Windows|10.0.17763+|

## Version 1.2.1

### What's new?
- Removed .Net7 support. :warning:
- Added .Net9 support for all platforms.
- Code improvements were applied.
- Fixed an issue where blur protection was not being disabled.

Click [here](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/releases/tag/v1.2.1) to see the full Changelog!

## Installation
`Plugin.Maui.ScreenSecurity` is available via NuGet, grab the latest package and install it on your solution:

    Install-Package Plugin.Maui.ScreenSecurity

Initialize the plugin in your `MauiProgram` class:

```csharp
using Plugin.Maui.ScreenSecurity;

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

    return builder.Build();
}
```

### Android

In your `Android.manifest` file (Platforms/Android) add the following permission:

```xml
<uses-permission android:name="android.permission.DETECT_SCREEN_CAPTURE" />
```

### Using Plugin.Maui.ScreenSecurity

Finally, add the default instance of the plugin as a singleton to inject it in your code late:

```csharp
builder.Services.AddSingleton<IScreenSecurity>(ScreenSecurity.Default);
```
## :warning:  WARNING  :warning:
It's important to acknowledge that preventing users from taking screenshots or recordings of your app can be a challenging task and achieving complete prevention may not be feasible. It's worth noting that no method can entirely eliminate the possibility of your screen being captured through another physical device or a potential breach in the OS.

:point_right: It's also important to consider the impact on user experience when implementing any of these methods and striking a balance with the security concerns of your app.

---

## API Usage
> If you are still using version 1.0.0, please refer to the [Legacy docs](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/wiki/Legacy) for the previous version.

The new unified API includes two methods: `ActivateScreenSecurityProtection()` and `DeactivateScreenSecurityProtection()`, with optional parameters applicable only to the iOS platform. It also provides two properties: `IsProtectionEnabled`, which checks if protection is active, and the `ScreenCaptured` event handler, which notifies when a screenshot is taken or the screen is recorded.

```csharp
void ActivateScreenSecurityProtection();
```
When you activate this protection, your app's content will be safeguarded when it's sent to the Recents screen or the App Switcher. This helps ensure that sensitive information won't be exposed.

### Behavior by platform:
- **Android**: provides enhanced protection for screen content by preventing exposure when the app is sent to the _Background_ or displayed on the _Recents_ screen. It also effectively prevents unauthorized screenshots or recordings from being captured.
- **Windows**: Prevents screenshots and recordings by obscuring the screen of the app.
- **iOS**: By default, it apply a blur layer when the app is sent to the _Background_ or displayed on the _App Switcher_. Also enables the screenshot and screen recording protection.

```csharp
void ActivateScreenSecurityProtection(bool blurScreenProtection, bool preventScreenshot, bool preventScreenRecording);
```
This method is similar to the previous method, but with parameters to change the default values in iOS:

- **`blurScreenProtection`**: Enable/disable screen blurring to prevent content visibility in the background. **True** by default.
- **`preventScreenshot`**: Decide whether users can take screenshots of your app. **True** by default.
- **`preventScreenRecording`**: Control whether users can record the screen while using your app. **True** by default.

```csharp
void ActivateScreenSecurityProtection(ScreenProtectionOptions screenProtectionOptions);
```
This method is similar to the original method, but takes a `ScreenProtectionOptions` parameter. This allows you to further customize the screen protection by specifying either a _Color_ or an _Image_, along with the the screenshot and screen recording protection for iOS devices.

**Note**: If you set both _Color_ and _Image_, it will only apply the one you declared first.

`ScreenProtectionOptions` properties:
- **`Color`**: Represents a color in the form of a hexadecimal string and can be passed as an argument to customize the color layer. It supports formats such as `#RGB`, `#RGBA`, `#RRGGBB`, or `#RRGGBBAA`. **Empty** by default.
- **`Image`**: The name of the image file along with its extension. In order to utilize this property, please follow these steps:
    - Save the image you intend to use inside the `Resources\Images` folder.
    - Ensure you refer to the [.Net MAUI Image documentation](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/image#load-a-local-image) for detailed instructions on how to accomplish this task effectively.
    - :warning: If your app does not recognize the image after setting the build action to `MauiImage`, consider changing the build action to `Embedded resource` to ensure proper functionality.
- **`PreventScreenshot`**: Decide whether users can take screenshots of your app. **True** by default.
- **`PreventScreenRecording`**: Control whether users can record the screen while using your app. **True** by default.

```csharp
void DeactivateScreenSecurityProtection();
```
This method deactivates all screen security protection.

```csharp
bool IsProtectionEnabled { get; }
```
This bool checks if screen protection is enabled.

```csharp
event EventHandler<EventArgs>? ScreenCaptured;
```
The event handler is triggered when the screen is captured, either through a screenshot or recording on Android and iOS, **but only for screenshots on Windows**.

## Usage Example

```csharp
public partial class MainPage : ContentPage
{
    private readonly IScreenSecurity _screenSecurity;

    public MainPage(IScreenSecurity screenSecurity)
    {
        InitializeComponent();

        _screenSecurity = screenSecurity;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Check if screen security protection is not enabled
        if (!_screenSecurity.IsProtectionEnabled)
        {
            // Activate the screen security protection with default settings
            _screenSecurity.ActivateScreenSecurityProtection();
        }

        // Attach to the ScreenCaptured event handler
        _screenSecurity.ScreenCaptured += OnScreenCaptured;

        /*
        // For changing iOS options, follow one of the next examples:

        // Example 1: Customize with a specific color
        var screenProtectionOptions = new ScreenProtectionOptions
        {
            HexColor = "#6C4675",
            PreventScreenshot = true,
            PreventScreenRecording = false
        };

        // Example 2: Customize with an image
        var screenProtectionOptions = new ScreenProtectionOptions
        {
            Image = "protection_bg.png"
            PreventScreenshot = false,
            PreventScreenRecording = true
        };

        _screenSecurity.ActivateScreenSecurityProtection(screenProtectionOptions);
        */
    }

    protected override void OnDisappearing()
    {
        _screenSecurity.DeactivateScreenSecurityProtection();

        // Detach from the ScreenCaptured event handler
        _screenSecurity.ScreenCaptured -= OnScreenCaptured;

        base.OnDisappearing();
    }

    private async void OnScreenCaptured(object sender, EventArgs e)
    {
        string title = "ScreenSecuritySample";
        string message = "Screen was captured by screenshot or recording.";

        await Shell.Current.DisplayAlert(title, message, "Ok");
    }
}
```

## Sample
Refer to the [samples folder](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/tree/main/samples) for a detailed implementation of this plugin for both Maui and Blazor, which will give you a complete understanding of its usage.

## Contributions
Please feel free to open an [Issue](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/issues) if you encounter any bugs or submit a PR to contribute improvements or fixes. Your contributions are greatly appreciated.

## License
The Plugin.Maui.ScreenSecurity is licensed under [MIT](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/blob/main/LICENSE) license.

## Contributors

* **[Goran Karacic](https://github.com/Gogzs)** for the iOS 17 fix.
* **[fabien367](https://github.com/fabien367)** for the iOS leak fix.
