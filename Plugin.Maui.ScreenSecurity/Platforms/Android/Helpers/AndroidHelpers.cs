using Android.OS;
using Java.Lang;
using Java.Util.Concurrent;
using Plugin.Maui.ScreenSecurity.Handlers;
using static Android.App.Activity;
using Object = Java.Lang.Object;

namespace Plugin.Maui.ScreenSecurity.Platforms.Android;

internal class CustomScreenCaptureCallback : Object, IScreenCaptureCallback
{
    public void OnScreenCaptured()
    {
        ScreenCaptureEventHandler.RaiseScreenCaptured();
    }
}

internal class MainThreadExecutor : Object, IExecutor
{
    private readonly Handler _handler = new(Looper.MainLooper ?? throw new InvalidOperationException("MainLooper is null"));

    public void Execute(IRunnable? command)
    {
        if (command is not null)
            _handler.Post(command);
    }
}