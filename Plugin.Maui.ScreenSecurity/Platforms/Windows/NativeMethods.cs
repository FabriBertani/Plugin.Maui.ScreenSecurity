using Plugin.Maui.ScreenSecurity.Handlers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Plugin.Maui.ScreenSecurity.Platforms.Windows;

internal partial class NativeMethods
{
    private const int WM_KEYDOWN = 0x0100;
    // Virtual code for Print Screen (PrtSc) key
    private const int VK_SNAPSHOT = 0x2C;

    public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    public static LowLevelKeyboardProc _proc = HookCallback;
    public static IntPtr _hookID = IntPtr.Zero;

    [LibraryImport("user32.dll", SetLastError = true)]
    public static partial uint SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);

    [LibraryImport("user32.dll", EntryPoint = "SetWindowsHookExW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool UnhookWindowsHookEx(IntPtr hhk);

    [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
    private static partial IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial IntPtr GetModuleHandle(string lpModuleName);

    public static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(13, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            if (vkCode == VK_SNAPSHOT)
                ScreenCaptureEventHandler.RaiseScreenCaptured();
        }

        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }
}