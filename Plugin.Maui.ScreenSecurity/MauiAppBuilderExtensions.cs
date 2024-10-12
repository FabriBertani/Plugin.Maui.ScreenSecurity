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
                if (Build.VERSION.SdkInt >= BuildVersionCodes.UpsideDownCake)
                {
                    CustomScreenCaptureCallback _customScreenCaptureCallback = new();
                    MainThreadExecutor _mainThreadExecutor = new();

                    android.OnStart(activity =>
                    {
                        activity.RegisterScreenCaptureCallback(_mainThreadExecutor, _customScreenCaptureCallback);
                    });

                    android.OnStop(activity =>
                    {
                        activity.UnregisterScreenCaptureCallback(_customScreenCaptureCallback);
                    });
                }
            });
#elif WINDOWS
            life.AddWindows(windows =>
            {
                windows.OnLaunched((app, args) =>
                {
                    var mainWindow = Application.Current?.Windows[0];
                    var nativeWindow = mainWindow?.Handler?.PlatformView as MauiWinUIWindow;

                    NativeMethods._hookID = NativeMethods.SetHook(NativeMethods._proc);

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
                    NativeMethods.UnhookWindowsHookEx(NativeMethods._hookID);
                });
            });
#endif
        });

        return builder;
    }
}