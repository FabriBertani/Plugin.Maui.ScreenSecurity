using Android.OS;
using Java.Lang;
using Java.Util.Concurrent;
using Plugin.Maui.ScreenSecurity.Handlers;
using static Android.App.Activity;
using Object = Java.Lang.Object;

namespace Plugin.Maui.ScreenSecurity.Platforms.Android;

#if NET8_0_OR_GREATER

internal class CustomScreenCaptureCallback : Object, IScreenCaptureCallback
{
    public void OnScreenCaptured()
    {
        ScreenCaptureEventHandler.RaiseScreenCaptured();
    }
}

internal class MainThreadExecutor : Object, IExecutor
{
    private readonly Handler _handler = new(Looper.MainLooper);

    public void Execute(IRunnable? command)
    {
        _handler.Post(command);
    }
}

#endif