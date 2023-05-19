using Android.Views;

namespace Plugin.Maui.ScreenSecurity;

partial class ScreenSecurityImplementation : IScreenSecurity
{
    /// <summary>
    /// Prevent screen content from being exposed when the app
    /// is sent to <b>Background</b> or the <b>Recents screen</b>.
    /// Also prevents app <b>screenshots</b> or <b>recording</b> to be taken.
    /// </summary>
    public void EnableScreenSecurityProtection()
    {
        try
        {
            var activity = Platform.CurrentActivity;

            activity?.Window?.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"EnableScreenSecurityProtection failed with Exception message: {ex.Message}");
            Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
            if (ex.InnerException != null)
                Console.WriteLine($"With InnerException: {ex.InnerException}");
        }
    }

    /// <summary>
    /// Re-enables screen content exposure.
    /// </summary>
    public void DisableScreenSecurityProtection()
    {
        try
        {
            var activity = Platform.CurrentActivity;

            activity?.Window?.ClearFlags(WindowManagerFlags.Secure);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DisableScreenSecurityProtection failed with Exception message: {ex.Message}");
            Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
            if (ex.InnerException != null)
                Console.WriteLine($"With InnerException: {ex.InnerException}");
        }
    }
}