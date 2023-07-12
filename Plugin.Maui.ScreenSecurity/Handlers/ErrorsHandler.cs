namespace Plugin.Maui.ScreenSecurity.Handlers;

internal static class ErrorsHandler
{
    internal static void HandleException(string methodName, Exception ex)
    {
        Console.WriteLine($"{methodName} failed with Exception message: {ex.Message}");
        Console.WriteLine($"Exception Stacktrace: {ex.StackTrace}");
        if (ex.InnerException != null)
            Console.WriteLine($"With InnerException: {ex.InnerException}");
    }
}