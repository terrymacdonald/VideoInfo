using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    /// <summary>
    /// Manual P/Invoke declarations for ADLX DLL entry points.
    /// These functions are exported from amdadlx64.dll and must be loaded dynamically.
    /// </summary>
    internal static unsafe class ADLXNative
    {
        // DLL name constants
        private const string ADLX_DLL_NAME_64 = "amdadlx64.dll";
        private const string ADLX_DLL_NAME_32 = "amdadlx32.dll";

        // Function name constants (exported from DLL)
        private const string ADLX_QUERY_FULL_VERSION_FUNCTION_NAME = "ADLXQueryFullVersion";
        private const string ADLX_QUERY_VERSION_FUNCTION_NAME = "ADLXQueryVersion";
        private const string ADLX_INIT_FUNCTION_NAME = "ADLXInitialize";
        private const string ADLX_INIT_WITH_CALLER_ADL_FUNCTION_NAME = "ADLXInitializeWithCallerAdl";
        private const string ADLX_INIT_WITH_INCOMPATIBLE_DRIVER_FUNCTION_NAME = "ADLXInitializeWithIncompatibleDriver";
        private const string ADLX_TERMINATE_FUNCTION_NAME = "ADLXTerminate";

        // Get appropriate DLL name based on platform
        public static string GetDllName()
        {
            return Environment.Is64BitProcess ? ADLX_DLL_NAME_64 : ADLX_DLL_NAME_32;
        }

        // Function pointer delegates matching ADLX C API
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ADLX_RESULT ADLXQueryFullVersion_Fn(ulong* fullVersion);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ADLX_RESULT ADLXQueryVersion_Fn(byte** version);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ADLX_RESULT ADLXInitialize_Fn(ulong version, IntPtr* ppSystem);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ADLX_RESULT ADLXInitializeWithCallerAdl_Fn(
            ulong version,
            IntPtr* ppSystem,
            IntPtr* ppAdlMapping,
            IntPtr adlContext,
            IntPtr adlMainMemoryFree);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ADLX_RESULT ADLXTerminate_Fn();

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

        // ADLX version constant
        public const ulong ADLX_FULL_VERSION = 0; // Will be updated from actual version

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

        // Function name getters
        public static string GetQueryFullVersionFunctionName() => ADLX_QUERY_FULL_VERSION_FUNCTION_NAME;
        public static string GetQueryVersionFunctionName() => ADLX_QUERY_VERSION_FUNCTION_NAME;
        public static string GetInitializeFunctionName() => ADLX_INIT_FUNCTION_NAME;
        public static string GetInitializeWithCallerAdlFunctionName() => ADLX_INIT_WITH_CALLER_ADL_FUNCTION_NAME;
        public static string GetInitializeWithIncompatibleDriverFunctionName() => ADLX_INIT_WITH_INCOMPATIBLE_DRIVER_FUNCTION_NAME;
        public static string GetTerminateFunctionName() => ADLX_TERMINATE_FUNCTION_NAME;
    }

    /// <summary>
    /// ADLX result codes (from ADLXDefines.h)
    /// Will be replaced/augmented by ClangSharp generated enum
    /// </summary>
    //public enum ADLX_RESULT
    //{
    //    ADLX_OK = 0,
    //    ADLX_ALREADY_ENABLED = 1,
    //    ADLX_ALREADY_INITIALIZED = 2,
    //    ADLX_FAIL = -1,
    //    ADLX_INVALID_ARGS = -2,
    //    ADLX_BAD_VER = -3,
    //    ADLX_UNKNOWN_INTERFACE = -4,
    //    ADLX_TERMINATED = -5,
    //    ADLX_ADL_INIT_ERROR = -6,
    //    ADLX_NOT_FOUND = -7,
    //    ADLX_INVALID_OBJECT = -8,
    //    ADLX_ORPHAN_OBJECTS = -9,
    //    ADLX_NOT_SUPPORTED = -10,
    //    ADLX_PENDING_OPERATION = -11,
    //    ADLX_GPU_INACTIVE = -12
    //}
}
