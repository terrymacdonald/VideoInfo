using System;
using System.Runtime.InteropServices;

namespace IGCLWrapper
{
    /// <summary>
    /// Manual P/Invoke declarations for IGCL DLL entry points.
    /// These functions are exported from igcl.dll and must be loaded dynamically.
    /// </summary>
    internal static unsafe class IGCLNative
    {
        // DLL name constants
        private const string IGCL_DLL_NAME_64 = "ControlLib.dll";
        private const string IGCL_DLL_NAME_32 = "ControlLib32.dll";

        // Get appropriate DLL name based on platform
        public static string GetDllName()
        {
            return Environment.Is64BitProcess ? IGCL_DLL_NAME_64 : IGCL_DLL_NAME_32;
        }

        // Windows API functions for dynamic loading
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibraryEx(
            string lpFileName,
            IntPtr hFile,
            uint dwFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        // LoadLibraryEx flags
        public const uint LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400;
        public const uint LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200;
        public const uint LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000;
        public const uint LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800;

        // IGCL version constant
        public const ulong IGCL_FULL_VERSION = 0; // Will be updated from actual version

        // Helper to load function pointer from DLL
        public static T GetFunctionPointer<T>(IntPtr hModule, string functionName) where T : Delegate
        {
            IntPtr functionPtr = GetProcAddress(hModule, functionName);
            if (functionPtr == IntPtr.Zero)
            {
                throw new EntryPointNotFoundException($"Function '{functionName}' not found in ADLX DLL");
            }
            return Marshal.GetDelegateForFunctionPointer<T>(functionPtr);
        }

    }
}
