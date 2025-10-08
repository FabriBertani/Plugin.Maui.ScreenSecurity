using System.Runtime.ExceptionServices;

namespace Plugin.Maui.ScreenSecurity.Handlers;

internal static class ErrorsHandler
{
    internal static void HandleException(string methodName, bool throwErrors, Exception ex)
    {
        System.Diagnostics.Trace.WriteLine($"{methodName} failed with Exception message: {ex.Message}");
        System.Diagnostics.Trace.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
        if (ex.InnerException is not null)
            System.Diagnostics.Trace.WriteLine($"With InnerException: {ex.InnerException}");

        if (throwErrors)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();

            // Keep an explicit return so the compiler knows this code path doesn't continue.
            return;
        }
    }
}