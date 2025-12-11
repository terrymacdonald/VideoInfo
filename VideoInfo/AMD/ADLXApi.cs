using System;
using System.Runtime.InteropServices;
namespace ADLXWrapper
{
    /// <summary>
    /// Exception thrown when an ADLX API call fails
    /// </summary>
    public class ADLXException : Exception
    {
        public ADLX_RESULT Result { get; }

        public ADLXException(ADLX_RESULT result, string? message = null)
            : base(message ?? $"ADLX API error: {result}")
        {
            Result = result;
        }

        /// <summary>
        /// Checks if this exception is due to ADLX not being initialized
        /// </summary>
        public bool IsNotInitializedError()
        {
            return Result == ADLX_RESULT.ADLX_TERMINATED;
        }

        /// <summary>
        /// Checks if this exception is due to already being initialized
        /// </summary>
        public bool IsAlreadyInitializedError()
        {
            return Result == ADLX_RESULT.ADLX_ALREADY_INITIALIZED;
        }

        /// <summary>
        /// Checks if this exception is due to GPU being inactive
        /// </summary>
        public bool IsGPUInactiveError()
        {
            return Result == ADLX_RESULT.ADLX_GPU_INACTIVE;
        }
    }

    /// <summary>
    /// Main ADLX API wrapper providing safe access to AMD ADLX Library
    /// Implements IDisposable for proper resource cleanup
    /// </summary>
    public sealed class ADLXApi : IDisposable
    {
        private IntPtr _hDLL;
        private ComPtr<IADLXSystem> _systemServices;
        private bool _disposed;

        // Function pointers loaded from DLL
#pragma warning disable CS0414 // Field assigned but never used - kept for potential future use
        private ADLXNative.ADLXQueryFullVersion_Fn? _queryFullVersionFn;
        private ADLXNative.ADLXQueryVersion_Fn? _queryVersionFn;
        private ADLXNative.ADLXInitialize_Fn? _initializeFn;
        private ADLXNative.ADLXInitializeWithCallerAdl_Fn? _initializeWithCallerAdlFn;
        private ADLXNative.ADLXTerminate_Fn? _terminateFn;
#pragma warning restore CS0414

        // Cached version info
        private ulong _fullVersion;
        private string? _version;

        private unsafe ADLXApi(IntPtr hDLL, IADLXSystem* pSystemServices)
        {
            _hDLL = hDLL;
            _systemServices = new ComPtr<IADLXSystem>(pSystemServices);
        }

        /// <summary>
        /// Initialize the ADLX API with default settings
        /// </summary>
        public static unsafe ADLXApi Initialize()
        {
            // Load ADLX DLL
            var hDLL = LoadADLXDll();
            
            // Get function pointers
            var queryFullVersionFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXQueryFullVersion_Fn>(
                hDLL, ADLXNative.GetQueryFullVersionFunctionName());
            var queryVersionFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXQueryVersion_Fn>(
                hDLL, ADLXNative.GetQueryVersionFunctionName());
            var initializeFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXInitialize_Fn>(
                hDLL, ADLXNative.GetInitializeFunctionName());
            var terminateFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXTerminate_Fn>(
                hDLL, ADLXNative.GetTerminateFunctionName());

            // Query version
            ulong fullVersion = 0;
            var result = queryFullVersionFn(&fullVersion);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                ADLXNative.FreeLibrary(hDLL);
                throw new ADLXException(result, "Failed to query ADLX version");
            }

            // Initialize ADLX
            IntPtr pSystem;
            result = initializeFn(fullVersion, &pSystem);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                ADLXNative.FreeLibrary(hDLL);
                throw new ADLXException(result, "Failed to initialize ADLX");
            }

            var api = new ADLXApi(hDLL, (IADLXSystem*)pSystem)
            {
                _queryFullVersionFn = queryFullVersionFn,
                _queryVersionFn = queryVersionFn,
                _initializeFn = initializeFn,
                _terminateFn = terminateFn,
                _fullVersion = fullVersion
            };

            // Cache version string
            byte* pVersion;
            result = queryVersionFn(&pVersion);
            if (result == ADLX_RESULT.ADLX_OK && pVersion != null)
            {
                api._version = Marshal.PtrToStringAnsi((IntPtr)pVersion);
            }

            return api;
        }

        /// <summary>
        /// Initialize ADLX with an existing ADL context (for applications migrating from ADL)
        /// </summary>
        public static unsafe ADLXApi InitializeWithCallerAdl(IntPtr adlContext, IntPtr adlMainMemoryFree)
        {
            if (adlContext == IntPtr.Zero || adlMainMemoryFree == IntPtr.Zero)
            {
                throw new ArgumentException("ADL context and memory free function must not be null");
            }

            // Load ADLX DLL
            var hDLL = LoadADLXDll();

            // Get function pointers
            var queryFullVersionFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXQueryFullVersion_Fn>(
                hDLL, ADLXNative.GetQueryFullVersionFunctionName());
            var queryVersionFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXQueryVersion_Fn>(
                hDLL, ADLXNative.GetQueryVersionFunctionName());
            var initializeWithCallerAdlFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXInitializeWithCallerAdl_Fn>(
                hDLL, ADLXNative.GetInitializeWithCallerAdlFunctionName());
            var terminateFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXTerminate_Fn>(
                hDLL, ADLXNative.GetTerminateFunctionName());

            // Query version
            ulong fullVersion = 0;
            var result = queryFullVersionFn(&fullVersion);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                ADLXNative.FreeLibrary(hDLL);
                throw new ADLXException(result, "Failed to query ADLX version");
            }

            // Initialize ADLX with ADL context
            IntPtr pSystem;
            IntPtr pAdlMapping;
            result = initializeWithCallerAdlFn(fullVersion, &pSystem, &pAdlMapping, adlContext, adlMainMemoryFree);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                ADLXNative.FreeLibrary(hDLL);
                throw new ADLXException(result, "Failed to initialize ADLX with ADL context");
            }

            var api = new ADLXApi(hDLL, (IADLXSystem*)pSystem)
            {
                _queryFullVersionFn = queryFullVersionFn,
                _queryVersionFn = queryVersionFn,
                _initializeWithCallerAdlFn = initializeWithCallerAdlFn,
                _terminateFn = terminateFn,
                _fullVersion = fullVersion
            };

            // Cache version string
            byte* pVersion;
            result = queryVersionFn(&pVersion);
            if (result == ADLX_RESULT.ADLX_OK && pVersion != null)
            {
                api._version = Marshal.PtrToStringAnsi((IntPtr)pVersion);
            }

            return api;
        }

        /// <summary>
        /// Get the ADLX full version number
        /// </summary>
        public ulong GetFullVersion()
        {
            ThrowIfDisposed();
            return _fullVersion;
        }

        /// <summary>
        /// Get the ADLX version string
        /// </summary>
        public string GetVersion()
        {
            ThrowIfDisposed();
            return _version ?? "Unknown";
        }

        /// <summary>
        /// Get the system services as a SafeHandle (auto-release even if not disposed manually).
        /// </summary>
        public unsafe IADLXSystem* GetSystemServices()
        {
            ThrowIfDisposed();
            // AddRef is handled by the ComPtr wrapper when it's created.
            // We return the raw pointer here for convenience, but its lifetime is managed by _systemServices.
            // Callers should not call Release() on this pointer.
            // If a caller needs to hold onto it, they should create their own ComPtr and call AddRef().
            return _systemServices.Get();
        }

        /// <summary>
        /// Enumerate GPU handles with automatic lifetime management.
        /// </summary>
        public unsafe AdlxInterfaceHandle[] EnumerateGPUHandles()
        {
            ThrowIfDisposed();

            IADLXGPUList* pGpuList = null;
            var system = _systemServices.Get();
            var result = system->GetGPUs(&pGpuList);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate GPUs");

            using var gpuList = new ComPtr<IADLXGPUList>(pGpuList);
            var count = gpuList.Get()->Size();
            var handles = new AdlxInterfaceHandle[count];

            for (uint i = 0; i < count; i++)
            {
                IADLXGPU* pGpu = null;
                gpuList.Get()->At(i, &pGpu);
                handles[i] = AdlxInterfaceHandle.From(pGpu, addRef: false); // At already AddRef's
            }

            return handles;
        }

        /// <summary>
        /// Get the GPU tuning services interface.
        /// </summary>
        public unsafe IADLXGPUTuningServices* GetGPUTuningServices()
        {
            ThrowIfDisposed();
            IADLXGPUTuningServices* pServices = null;
            var result = _systemServices.Get()->GetGPUTuningServices(&pServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU tuning services");
            return pServices;
        }

        /// <summary>
        /// Get the performance monitoring services as a disposable handle.
        /// </summary>
        public unsafe AdlxInterfaceHandle GetPerformanceMonitoringServicesHandle()
        {
            ThrowIfDisposed();
            IADLXPerformanceMonitoringServices* pServices = null;
            var result = _systemServices.Get()->GetPerformanceMonitoringServices(&pServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get performance monitoring services");
            return AdlxInterfaceHandle.From(pServices, addRef: false);
        }

        /// <summary>
        /// Get the system services as a disposable handle (AddRef'd).
        /// </summary>
        public unsafe AdlxInterfaceHandle GetSystemServicesHandle()
        {
            ThrowIfDisposed();
            var ptr = _systemServices.Get();
            ADLXHelpers.AddRefInterface((IntPtr)ptr);
            return AdlxInterfaceHandle.From(ptr, addRef: false);
        }

        /// <summary>
        /// Dispose pattern implementation
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            unsafe
            {
                // Terminate first (keeps system pointer valid while terminating)
                if (_terminateFn != null && _systemServices.Get() != null)
                {
                    try
                    {
                        _terminateFn();
                    }
                    catch
                    {
                        // Ignore errors during cleanup
                    }
                }

                // Release system interface if still present
                if (_systemServices.Get() != null)
                {
                    try
                    {
                        _systemServices.Dispose(); // This calls Release()
                    }
                    catch
                    {
                        // ignore release errors during cleanup
                    }
                }

                // Free DLL
                if (_hDLL != IntPtr.Zero)
                {
                    ADLXNative.FreeLibrary(_hDLL);
                    _hDLL = IntPtr.Zero;
                }
            }
            
            _systemServices = default; // Clear the ComPtr
            _queryFullVersionFn = null;
            _queryVersionFn = null;
            _initializeFn = null;
            _initializeWithCallerAdlFn = null;
            _terminateFn = null;

            _disposed = true;
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~ADLXApi()
        {
            Dispose(false);
        }

        /// <summary>
        /// Load the ADLX DLL dynamically
        /// </summary>
        private static IntPtr LoadADLXDll()
        {
            var dllName = ADLXNative.GetDllName();
            
            var hDLL = ADLXNative.LoadLibraryEx(
                dllName,
                IntPtr.Zero,
                ADLXNative.LOAD_LIBRARY_SEARCH_USER_DIRS |
                ADLXNative.LOAD_LIBRARY_SEARCH_APPLICATION_DIR |
                ADLXNative.LOAD_LIBRARY_SEARCH_DEFAULT_DIRS |
                ADLXNative.LOAD_LIBRARY_SEARCH_SYSTEM32);

            if (hDLL == IntPtr.Zero)
            {
                var error = Marshal.GetLastWin32Error();
                throw new DllNotFoundException(
                    $"Failed to load ADLX DLL '{dllName}'. Error code: {error}. " +
                    "Make sure AMD GPU drivers are installed.");
            }

            return hDLL;
        }

        /// <summary>
        /// Throw if object has been disposed
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ADLXApi));
            }
        }

        /// <summary>
        /// Check if the ADLX DLL is available in the DLL search path
        /// </summary>
        /// <param name="errorMessage">Details about why the DLL could not be loaded</param>
        /// <returns>True if DLL can be loaded, false otherwise</returns>
        public static bool IsADLXDllAvailable(out string errorMessage)
        {
            var dllName = ADLXNative.GetDllName();
            
            IntPtr handle = ADLXNative.LoadLibraryEx(
                dllName,
                IntPtr.Zero,
                ADLXNative.LOAD_LIBRARY_SEARCH_USER_DIRS |
                ADLXNative.LOAD_LIBRARY_SEARCH_APPLICATION_DIR |
                ADLXNative.LOAD_LIBRARY_SEARCH_DEFAULT_DIRS |
                ADLXNative.LOAD_LIBRARY_SEARCH_SYSTEM32);

            if (handle == IntPtr.Zero)
            {
                var error = Marshal.GetLastWin32Error();
                errorMessage = $"ADLX SDK DLL '{dllName}' not found in DLL search path (Error: {error})";
                return false;
            }

            ADLXNative.FreeLibrary(handle);
            errorMessage = string.Empty;
            return true;
        }

    }
}
