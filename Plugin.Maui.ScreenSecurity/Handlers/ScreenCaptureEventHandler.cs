namespace Plugin.Maui.ScreenSecurity.Handlers;

internal static class ScreenCaptureEventHandler
{
    internal static event EventHandler<EventArgs>? ScreenCaptured;

    internal static void RaiseScreenCaptured()
    {
        ScreenCaptured?.Invoke(null, EventArgs.Empty);
    }
}