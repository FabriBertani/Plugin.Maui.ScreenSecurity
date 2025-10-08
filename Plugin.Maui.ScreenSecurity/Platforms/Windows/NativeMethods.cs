using Plugin.Maui.ScreenSecurity.Handlers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Plugin.Maui.ScreenSecurity.Platforms.Windows;

internal partial class NativeMethods
{
#if NET9_0_OR_GREATER
    private static readonly Lock _hookLock = new();
#else
    private static readonly object _hookLock = new();
#endif

    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    // Virtual code for Print Screen (PrtSc) key
    private const int VK_SNAPSHOT = 0x2C;

    private static int _hookRefCount;

    public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    public static readonly LowLevelKeyboardProc Proc = HookCallback;
    public static IntPtr HookID = IntPtr.Zero;

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

    [StructLayout(LayoutKind.Sequential)]
    private struct KBDLLHOOKSTRUCT
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    public static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
#if NET9_0_OR_GREATER
        lock (_hookLock)
#else
        lock (_hookLock)
#endif
        {
            _hookRefCount++;

            if (HookID != IntPtr.Zero)
                return HookID;

            using Process curProcess = Process.GetCurrentProcess();
            using ProcessModule? curModule = curProcess.MainModule;

            if (curModule is null)
            {
                _hookRefCount--;

                return IntPtr.Zero;
            }

            HookID = SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);

            if (HookID == IntPtr.Zero)
            {
                int err = Marshal.GetLastWin32Error();

                System.Diagnostics.Trace.TraceError($"SetWindowsHookEx failed. Win32Error={err}");

                _hookRefCount = 0;
            }

            return HookID;
        }
    }

    public static void Unhook()
    {
#if NET9_0_OR_GREATER
        lock (_hookLock)
#else
        lock (_hookLock)
#endif
        {
            if (_hookRefCount > 0)
                _hookRefCount--;

            if (_hookRefCount == 0 && HookID != IntPtr.Zero)
            {
                if (!UnhookWindowsHookEx(HookID))
                {
                    int err = Marshal.GetLastWin32Error();

                    System.Diagnostics.Trace.TraceError($"UnhookWindowsHookEx failed. Win32Error={err}");
                }

                HookID = IntPtr.Zero;
            }
        }
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        try
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (vkCode == VK_SNAPSHOT)
                    ScreenCaptureEventHandler.RaiseScreenCaptured();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError($"WINDOWS - Exception in HookCallback: {ex}");
        }

        return CallNextHookEx(HookID, nCode, wParam, lParam);
    }
}