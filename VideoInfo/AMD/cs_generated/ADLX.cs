using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public static unsafe partial class ADLX
{
    [DllImport("amdadlx64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?adlx_atomic_inc@@YAJPEAJ@Z", ExactSpelling = true)]
    [return: NativeTypeName("adlx_long")]
    public static extern int adlx_atomic_inc([NativeTypeName("adlx_long *")] int* x);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?adlx_atomic_dec@@YAJPEAJ@Z", ExactSpelling = true)]
    [return: NativeTypeName("adlx_long")]
    public static extern int adlx_atomic_dec([NativeTypeName("adlx_long *")] int* x);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?adlx_load_library@@YAPEAXPEBD@Z", ExactSpelling = true)]
    [return: NativeTypeName("adlx_handle")]
    public static extern void* adlx_load_library([NativeTypeName("const TCHAR *")] sbyte* filename);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?adlx_free_library@@YAHPEAX@Z", ExactSpelling = true)]
    public static extern int adlx_free_library([NativeTypeName("adlx_handle")] void* module);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?adlx_get_proc_address@@YAPEAXPEAXPEBD@Z", ExactSpelling = true)]
    public static extern void* adlx_get_proc_address([NativeTypeName("adlx_handle")] void* module, [NativeTypeName("const char *")] sbyte* procName);

    [NativeTypeName("#define ADLX_VER_MAJOR 1")]
    public const int ADLX_VER_MAJOR = 1;

    [NativeTypeName("#define ADLX_VER_MINOR 4")]
    public const int ADLX_VER_MINOR = 4;

    [NativeTypeName("#define ADLX_VER_RELEASE 0")]
    public const int ADLX_VER_RELEASE = 0;

    [NativeTypeName("#define ADLX_VER_BUILD_NUM 110")]
    public const int ADLX_VER_BUILD_NUM = 110;

    [NativeTypeName("#define ADLX_FULL_VERSION ADLX_MAKE_FULL_VER(ADLX_VER_MAJOR, ADLX_VER_MINOR, ADLX_VER_RELEASE, ADLX_VER_BUILD_NUM)")]
    public const ulong ADLX_FULL_VERSION = (((ulong)(1) << 48) | ((ulong)(4) << 32) | ((ulong)(0) << 16) | (ulong)(110));

    [NativeTypeName("#define ADLX_VERSION_STR ADLX_VER_MAJOR")]
    public const int ADLX_VERSION_STR = 1;

    [NativeTypeName("#define MAX_USER_3DLUT_NUM_POINTS 17")]
    public const int MAX_USER_3DLUT_NUM_POINTS = 17;

    [NativeTypeName("#define ADLX_DLL_NAMEW L\"amdadlx64.dll\"")]
    public const string ADLX_DLL_NAMEW = "amdadlx64.dll";

    [NativeTypeName("#define ADLX_DLL_NAMEA \"amdadlx64.dll\"")]
    public static ReadOnlySpan<byte> ADLX_DLL_NAMEA => "amdadlx64.dll"u8;

    [NativeTypeName("#define ADLX_DLL_NAME ADLX_DLL_NAMEA")]
    public static ReadOnlySpan<byte> ADLX_DLL_NAME => "amdadlx64.dll"u8;

    [NativeTypeName("#define ADLX_QUERY_FULL_VERSION_FUNCTION_NAME \"ADLXQueryFullVersion\"")]
    public static ReadOnlySpan<byte> ADLX_QUERY_FULL_VERSION_FUNCTION_NAME => "ADLXQueryFullVersion"u8;

    [NativeTypeName("#define ADLX_QUERY_VERSION_FUNCTION_NAME \"ADLXQueryVersion\"")]
    public static ReadOnlySpan<byte> ADLX_QUERY_VERSION_FUNCTION_NAME => "ADLXQueryVersion"u8;

    [NativeTypeName("#define ADLX_INIT_FUNCTION_NAME \"ADLXInitialize\"")]
    public static ReadOnlySpan<byte> ADLX_INIT_FUNCTION_NAME => "ADLXInitialize"u8;

    [NativeTypeName("#define ADLX_INIT_WITH_INCOMPATIBLE_DRIVER_FUNCTION_NAME \"ADLXInitializeWithIncompatibleDriver\"")]
    public static ReadOnlySpan<byte> ADLX_INIT_WITH_INCOMPATIBLE_DRIVER_FUNCTION_NAME => "ADLXInitializeWithIncompatibleDriver"u8;

    [NativeTypeName("#define ADLX_INIT_WITH_CALLER_ADL_FUNCTION_NAME \"ADLXInitializeWithCallerAdl\"")]
    public static ReadOnlySpan<byte> ADLX_INIT_WITH_CALLER_ADL_FUNCTION_NAME => "ADLXInitializeWithCallerAdl"u8;

    [NativeTypeName("#define ADLX_TERMINATE_FUNCTION_NAME \"ADLXTerminate\"")]
    public static ReadOnlySpan<byte> ADLX_TERMINATE_FUNCTION_NAME => "ADLXTerminate"u8;
}
