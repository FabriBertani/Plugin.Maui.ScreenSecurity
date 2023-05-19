# Plugin.Maui.ScreenSecurity
[![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.ScreenSecurity.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.ScreenSecurity)

`Plugin.Maui.ScreenSecurity` provides a seamless solution for preventing content exposure, as well as blocking screenshots and recordings within your .NET MAUI application

## Platforms supported
|Platform|Version|
|-------------------|:------------------:|
|.Net MAUI Android|API 21+|
|.Net MAUI iOS|iOS 14+|

## Installation
`Plugin.Maui.ScreenSecurity` is available via NuGet, grab the latest package and install it on your solution:

    Install-Package Plugin.Maui.ScreenSecurity

In your `MauiProgram` class add the following using statement:

```csharp
using Plugin.Maui.ScreenSecurity;
```

Finally, add the default instance of the plugin as a singleton to inject it in your code late:

```csharp
builder.Services.AddSingleton<IScreenSecurity>(ScreenSecurity.Default);
```
## :warning:  WARNING  :warning:
It's important to acknowledge that preventing users from taking screenshots or recordings of your app can be a challenging task and achieving complete prevention may not be feasible. It's worth noting that no method can entirely eliminate the possibility of your screen being captured through another physical device or a potential breach in the OS.

:point_right: It's also important to consider the impact on user experience when implementing any of these methods and striking a balance with the security concerns of your app.

---

## API Usage
Call the injected interface in any page or viewmodel to gain access to the APIs.

Given that each platform handles this functionality internally in a distinct manner, we will utilize `processor directives` to access the specific implementation for each platform.

### **Android**
```csharp
/// <remarks>Supported for Android only.</remarks>
void EnableScreenSecurityProtection();
```
> This method provides enhanced protection for screen content by preventing exposure when the app is sent to the **Background** or displayed on the **Recents screen**. It also effectively prevents unauthorized **screenshots** or **recordings** from being captured.

If the method fails, a message will be displayed on the `Console` and no _Exepction_ will be thrown.

```csharp
/// <remarks>Supported for Android only.</remarks>
void DisableScreenSecurityProtection();
```

> This method desable protection and re-enables screen content exposure.

If the method fails, a message will be displayed on the `Console` and no _Exepction_ will be thrown.

#### **Example**

This method can also be attached to a single page by using it on the `OnAppearing` and `OnDisappearing` methods.

```csharp
protected override void OnAppearing()
{
    base.OnAppearing();

#if ANDROID
    _screenSecurity.EnableScreenSecurityProtection();
#endif
}

protected override void OnDisappearing()
{
#if ANDROID
    _screenSecurity.DisableScreenSecurityProtection();
#endif

    base.OnDisappearing();
}
```
---
### **iOS**
Preventing screenshots on iOS can be a complex task that may not always be effective unless we utilize paid software like [ScreenShieldKit](https://screenshieldkit.com/). However, there are various methods available to protect the content of our screen when sending the app to the Background or switching between apps. Additionally, some methods help to prevent the app screen from being recorded by the system or any external application.

#### **Blur Screen Protection**
```csharp
/// <param name="style">Choose between a Light or Dark theme for the Blur layer. Light by default.</param>
/// <remarks>Supported for iOS only.</remarks>
void EnableBlurScreenProtection(ThemeStyle style);
```
> This method ensures that your app screen content remains protected by applying a blur layer when the app is sent to the Background or displayed on the App Switcher.

To choose between a light or dark Blur, you can pass the `style` property as an argument to this method. By default, it uses a Light Blur.

If the method fails, a message will be displayed on the `Console` and no _Exepction_ will be thrown.

```csharp
/// <remarks>Supported for iOS only.</remarks>
void DisableBlurScreenProtection();
```
> This method removes the Blur layer, thereby re-enabling the exposure of screen content.

If the method fails, a message will be displayed on the `Console` and no _Exepction_ will be thrown.

#### **Example**

This method can also be attached to a single page by using it on the `OnAppearing` and `OnDisappearing` methods.

```csharp
private void ActivateBlurProtection_Clicked(object sender, EventArgs e)
{
#if IOS
    var theme = Application.Current.RequestedTheme;

    var style = theme == AppTheme.Dark ? ThemeStyle.Dark : ThemeStyle.Light;

    _screenSecurity.EnableBlurScreenProtection(style);
#endif
}

private void DeactivateBlurProtection_Clicked(object sender, EventArgs e)
{
#if IOS
    _screenSecurity.DisableBlurScreenProtection();
#endif
}
```

#### **Color Screen Protection**

```csharp
/// <param name="hexColor">Hexadecimal color as string in the form of #RGB, #RGBA, #RRGGBB or #RRGGBBAA.</param>
/// <remarks>Supported for iOS only.</remarks>
void EnableColorScreenProtection(string hexColor);
```

> This method will prevent screen content from being exposed by using a color layer when the app is sent to Background or the App Switcher.

The `hexColor` property represents a color in the form of a hexadecimal string and can be passed as an argument to customize the color layer. It supports formats such as `#RGB`, `#RGBA`, `#RRGGBB`, or `#RRGGBBAA`. By default, the color is set to white (`#FFFFFF`).

If the method fails, a message will be displayed on the `Console` and no _Exepction_ will be thrown.

```csharp
/// <remarks>Supported for iOS only.</remarks>
void DisableColorScreenProtection();
```
> This method removes the color layer, thereby re-enabling the exposure of screen content.

If the method fails, a message will be displayed on the `Console` and no _Exepction_ will be thrown.

#### **Example**

This method can also be attached to a single page by using it on the `OnAppearing` and `OnDisappearing` methods.

```csharp
private void ActivateColorProtection_Clicked(object sender, EventArgs e)
{
#if IOS
    _screenSecurity.EnableColorScreenProtection("#6C4675");
#endif
}

private void DeactivateColorProtection_Clicked(object sender, EventArgs e)
{
#if IOS
    _screenSecurity.DisableColorScreenProtection();
#endif
}
```

#### **Image Screen Protection**
To utilize this method, please follow these steps:
* Save the image you intend to use inside the `Resources\Images` folder.
* Ensure you refer to the [.Net MAUI Image documentation](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/image#load-a-local-image) for detailed instructions on how to accomplish this task effectively.

:warning: If your app does not recognize the image after setting the build action to `MauiImage`, consider changing the build action to `Embedded resource` to ensure proper functionality.

```csharp
/// <param name="image">Name with extension of the image to use.</param>
/// <remarks>Supported for iOS only.</remarks>
void EnableImageScreenProtection(string image);
```
> This method safeguards screen content from exposure by utilizing an Image when the app is sent to Background or displayed on the App Switcher.

The `image` property, which accepts a string argument, should contain the name of the image file along with its extension.

If the method fails, a message will be displayed on the `Console` and no _Exepction_ will be thrown.

```csharp
/// <remarks>Supported for iOS only.</remarks>
void DisableImageScreenProtection();
```
> This method removes the Image and re-enables screen content exposure.

If the method fails, a message will be displayed on the `Console` and no _Exepction_ will be thrown.

#### **Example**

This method can also be attached to a single page by using it on the `OnAppearing` and `OnDisappearing` methods.

```csharp
protected override void OnAppearing()
{
    base.OnAppearing();
#if IOS
    _screenSecurity.EnableImageScreenProtection("protection_bg.png");
#endif
}

protected override void OnDisappearing()
{
#if IOS
    _screenSecurity.DisableImageScreenProtection();
#endif
    base.OnDisappearing();
}
```

#### **Screen Recording Protection**

```csharp
/// <param name="withColor">Hexadecimal color as string in the form of #RGB, #RGBA, #RRGGBB or #RRGGBBAA. 
/// It can be mixed with withBlur param.</param>
/// <param name="withBlur">Set it to false to deactivate screen protection with Blur. It can be mixed with withColor param. True by default.</param>
/// <remarks>Supported for iOS only.</remarks>
void EnableScreenRecordingProtection(string withColor, bool withBlur);
```
> This method effectively prevents screen content from **being recorded** by the system or any external application, utilizing a default Blur layer.

The `withColor` property allows you to specify a color argument in the form of `#RGB`, `#RGBA`, `#RRGGBB`, or `#RRGGBBAA`. By default, this feature is disabled.

The `withBlur` property is used to enable a blur layer with a *Light* theme, which is active by default. If desired, you can set it to false to disable the blur layer.

:warning: If a screen recording has already **started before** calling this method, the screen content **will be exposed** in the recorded film.

If the method fails, a message will be displayed on the `Console` and no _Exepction_ will be thrown.

```csharp
/// <remarks>Supported for iOS only.</remarks>
void DisableScreenRecordingProtection();
```
> This method will turn off the screen recording protection.

If the method fails, a message will be displayed on the `Console` and no _Exepction_ will be thrown.

#### **Example**

This method can also be attached to a single page by using it on the `OnAppearing` and `OnDisappearing` methods.

```csharp
private async void ActivateRecordingProtection_Clicked(object sender, EventArgs e)
{
#if IOS
    _screenSecurity.EnableScreenRecordingProtection();
#endif
}

private async void DeactivateRecordingProtection_Clicked(object sender, EventArgs e)
{
#if IOS
    _screenSecurity.DisableScreenRecordingProtection();
#endif
}
```

## Usage

```csharp
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
#if ANDROID
        _screenSecurity.EnableScreenSecurityProtection();
#elif IOS
        _screenSecurity.EnableBlurScreenProtection(ThemeStyle.Light);
        _screenSecurity.EnableScreenRecordingProtection();
#endif
    }

    protected override void OnDisappearing()
    {
#if ANDROID
        _screenSecurity.DisableScreenSecurityProtection();
#elif IOS
        _screenSecurity.DisableBlurScreenProtection();
        _screenSecurity.DisableScreenRecordingProtection();
#endif
        base.OnDisappearing();
    }
}
```

## Sample
Please refer to the [ScreenSecuritySample](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/tree/main/samples/ScreenSecuritySample) for a comprehensive and detailed implementation of this plugin, providing you with a complete understanding of its usage.

## Contributions
Please feel free to open an [Issue](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/issues) if you encounter any bugs or submit a PR to contribute improvements or fixes. Your contributions are greatly appreciated.

## License
Plugin.Maui.ScreenSecurity is licensed under [MIT](https://github.com/FabriBertani/Plugin.Maui.ScreenSecurity/blob/main/LICENSE).