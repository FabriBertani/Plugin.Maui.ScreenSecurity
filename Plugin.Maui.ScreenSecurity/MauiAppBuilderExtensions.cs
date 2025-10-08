#if ANDROID
using Android.OS;
using Plugin.Maui.ScreenSecurity.Platforms.Android;
#elif WINDOWS
using Plugin.Maui.ScreenSecurity.Platforms.Windows;
#endif
using Microsoft.Maui.LifecycleEvents;
using Plugin.Maui.ScreenSecurity.Handlers;

namespace Plugin.Maui.ScreenSecurity;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseScreenSecurity(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(life =>
        {
#if ANDROID34_0_OR_GREATER
            life.AddAndroid(android =>
            {
                if (!OperatingSystem.IsAndroidVersionAtLeast(34))
                    return;

                CustomScreenCaptureCallback customScreenCaptureCallback = new();
                MainThreadExecutor mainThreadExecutor = new();

                android.OnStart(activity =>
                {
                    activity.RegisterScreenCaptureCallback(mainThreadExecutor, customScreenCaptureCallback);
                });

                android.OnStop(activity =>
                {
                    activity.UnregisterScreenCaptureCallback(customScreenCaptureCallback);
                });

                android.OnDestroy(activity => { });
            });
#elif WINDOWS
            life.AddWindows(windows =>
            {
                windows.OnLaunched((app, args) =>
                {
                    var mainWindow = Application.Current?.Windows[0];
                    var nativeWindow = mainWindow?.Handler?.PlatformView as MauiWinUIWindow;

                    NativeMethods.HookID = NativeMethods.SetHook(NativeMethods.Proc);

                    if (nativeWindow is not null)
                    {
                        nativeWindow.Content.KeyDown += (sender, e) =>
                        {
                            if (e.Key == Windows.System.VirtualKey.Snapshot)
                            {
                                ScreenCaptureEventHandler.RaiseScreenCaptured();
                            }
                        };
                    }
                });

                windows.OnClosed((app, args) =>
                {
                    NativeMethods.UnhookWindowsHookEx(NativeMethods.HookID);
                });
            });
#endif
        });

        builder.Services.AddSingleton<IScreenSecurity>(ScreenSecurity.Default);

        return builder;
    }
}