using System;

namespace ADLXWrapper
{
    // C# equivalents for function-like macros that ClangSharp skips
    public static partial class ADLX
    {
        // Matches ADLX_SUCCEEDED macro: OK or already enabled/initialized are treated as success.
        public static bool ADLX_SUCCEEDED(ADLX_RESULT result) =>
            result == ADLX_RESULT.ADLX_OK ||
            result == ADLX_RESULT.ADLX_ALREADY_ENABLED ||
            result == ADLX_RESULT.ADLX_ALREADY_INITIALIZED;

        // Matches ADLX_FAILED macro: anything not covered by ADLX_SUCCEEDED.
        public static bool ADLX_FAILED(ADLX_RESULT result) => !ADLX_SUCCEEDED(result);

        public static ulong ADLX_MAKE_FULL_VER(uint major, uint minor, uint patch, uint build) =>
            ((ulong)major << 48) | ((ulong)minor << 32) | ((ulong)patch << 16) | build;
    }
}
