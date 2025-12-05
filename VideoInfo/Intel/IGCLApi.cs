using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace IGCLWrapper
{
    /// <summary>
    /// Exception thrown when an IGCL API call fails
    /// </summary>
    public class IGCLException : Exception
    {
        public ctl_result_t Result { get; }

        public IGCLException(ctl_result_t result, string? message = null)
            : base(message ?? $"IGCL API error: {result}")
        {
            Result = result;
        }

        /// <summary>
        /// Checks if this exception is due to no display being attached/connected
        /// </summary>
        public bool IsNoDisplayError()
        {
            return Result == ctl_result_t.CTL_RESULT_ERROR_DISPLAY_NOT_ATTACHED ||
                   Result == ctl_result_t.CTL_RESULT_ERROR_DISPLAY_NOT_ACTIVE;
        }
    }

    /// <summary>
    /// Main IGCL API wrapper providing safe access to Intel Graphics Control Library
    /// </summary>
    public sealed class IGCLApi : IDisposable
    {
        private IGCLApiHandle? _hApi;
        private bool _disposed;

        private IGCLApi(IGCLApiHandle hApi)
        {
            _hApi = hApi;
        }

        ~IGCLApi()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initialize the IGCL API with default settings
        /// </summary>
        public static IGCLApi Initialize()
        {
            unsafe
            {
                // Create initialization arguments
                var initArgs = new ctl_init_args_t
                {
                    Size = (uint)sizeof(ctl_init_args_t),
                    Version = (byte)0,
                    AppVersion = GetImplVersion(),
                    flags = (uint)ctl_init_flag_t.CTL_INIT_FLAG_USE_LEVEL_ZERO,
                    SupportedVersion = GetImplVersion()
                };

                // Initialize the API
                _ctl_api_handle_t* hApi;
                var result = IGCL.ctlInit(&initArgs, &hApi);
                
                if (result != ctl_result_t.CTL_RESULT_SUCCESS)
                {
                    throw new IGCLException(result, $"Failed to initialize IGCL API");
                }

                return new IGCLApi(new IGCLApiHandle((IntPtr)hApi));
            }
        }

        /// <summary>
        /// Enumerate all GPU adapters in the system
        /// </summary>
        public unsafe IntPtr[] EnumerateAdapters()
        {
            return WithApiHandle(handle =>
            {
                var apiHandle = (_ctl_api_handle_t*)handle;

                // Get adapter count
                uint adapterCount = 0;
                var result = IGCL.ctlEnumerateDevices(apiHandle, &adapterCount, null);

                if (result != ctl_result_t.CTL_RESULT_SUCCESS)
                {
                    throw new IGCLException(result, "Failed to get adapter count");
                }

                if (adapterCount == 0)
                {
                    return Array.Empty<IntPtr>();
                }

                // Get adapters
                var adapters = new _ctl_device_adapter_handle_t*[adapterCount];
                fixed (_ctl_device_adapter_handle_t** pAdapters = adapters)
                {
                    result = IGCL.ctlEnumerateDevices(apiHandle, &adapterCount, pAdapters);

                    if (result != ctl_result_t.CTL_RESULT_SUCCESS)
                    {
                        throw new IGCLException(result, "Failed to enumerate adapters");
                    }
                }

                // adapterCount may change between calls; clamp to buffer length
                var actualCount = (int)Math.Min(adapterCount, (uint)adapters.Length);

                // Convert to IntPtr array for easier downstream use
                var intPtrAdapters = new IntPtr[actualCount];
                for (int i = 0; i < actualCount; i++)
                {
                    intPtrAdapters[i] = (IntPtr)adapters[i];
                }

                return intPtrAdapters;
            });
        }

        /// <summary>
        /// Enumerate display outputs for a given adapter
        /// </summary>
        public unsafe IntPtr[] EnumerateDisplays(IntPtr hAdapter)
        {
            ThrowIfDisposed();

            // Get display count
            uint displayCount = 0;
            var result = IGCL.ctlEnumerateDisplayOutputs((_ctl_device_adapter_handle_t*)hAdapter, &displayCount, null);

            if (result != ctl_result_t.CTL_RESULT_SUCCESS)
            {
                throw new IGCLException(result, "Failed to get display count");
            }

            if (displayCount == 0)
            {
                return Array.Empty<IntPtr>();
            }

            // Get displays
            var displays = new _ctl_display_output_handle_t*[displayCount];
            fixed (_ctl_display_output_handle_t** pDisplays = displays)
            {
                result = IGCL.ctlEnumerateDisplayOutputs((_ctl_device_adapter_handle_t*)hAdapter, &displayCount, pDisplays);

                if (result != ctl_result_t.CTL_RESULT_SUCCESS)
                {
                    throw new IGCLException(result, "Failed to enumerate displays");
                }
            }

            var actualCount = (int)Math.Min(displayCount, (uint)displays.Length);

            // Convert to IntPtr array for easier downstream use
            var intPtrDisplays = new IntPtr[actualCount];
            for (int i = 0; i < actualCount; i++)
            {
                intPtrDisplays[i] = (IntPtr)displays[i];
            }

            return intPtrDisplays;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private unsafe void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (_hApi != null)
            {
                _hApi.Dispose();
                _hApi = null;
            }

            _disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(IGCLApi));
        }

        private unsafe _ctl_api_handle_t* GetApiHandle()
        {
            ThrowIfDisposed();
            return (_ctl_api_handle_t*)_hApi!.DangerousGetHandle();
        }

        #region Helper Methods for Version Macros

        /// <summary>
        /// Create a version number from major and minor components
        /// </summary>
        public static uint MakeVersion(uint major, uint minor)
        {
            return (major << 16) | (minor & 0x0000ffff);
        }

        /// <summary>
        /// Extract major version from version number
        /// </summary>
        public static uint GetMajorVersion(uint version)
        {
            return version >> 16;
        }

        /// <summary>
        /// Extract minor version from version number
        /// </summary>
        public static uint GetMinorVersion(uint version)
        {
            return version & 0x0000ffff;
        }

        /// <summary>
        /// Get the IGCL implementation version
        /// </summary>
        public static uint GetImplVersion()
        {
            return (uint)IGCL.CTL_IMPL_VERSION;
        }

        #endregion

        /// <summary>
        /// Check if the IGCL DLL is available in the DLL search path
        /// </summary>
        /// <param name="errorMessage">Details about why the DLL could not be loaded</param>
        /// <returns>True if DLL can be loaded, false otherwise</returns>
        public static bool IsIGCLDllAvailable(out string errorMessage)
        {
            var dllName = IGCLNative.GetDllName();

            IntPtr handle = IGCLNative.LoadLibraryEx(
                dllName,
                IntPtr.Zero,
                IGCLNative.LOAD_LIBRARY_SEARCH_USER_DIRS |
                IGCLNative.LOAD_LIBRARY_SEARCH_APPLICATION_DIR |
                IGCLNative.LOAD_LIBRARY_SEARCH_DEFAULT_DIRS |
                IGCLNative.LOAD_LIBRARY_SEARCH_SYSTEM32);

            if (handle == IntPtr.Zero)
            {
                var error = Marshal.GetLastWin32Error();
                errorMessage = $"IGCL SDK DLL '{dllName}' not found in DLL search path (Error: {error})";
                return false;
            }

            IGCLNative.FreeLibrary(handle);
            errorMessage = string.Empty;
            return true;
        }

        private unsafe T WithApiHandle<T>(Func<IntPtr, T> action)
        {
            ThrowIfDisposed();
            bool addRef = false;
            try
            {
                _hApi!.DangerousAddRef(ref addRef);
                return action(_hApi.DangerousGetHandle());
            }
            finally
            {
                if (addRef)
                {
                    _hApi!.DangerousRelease();
                }
            }
        }
    }
}
