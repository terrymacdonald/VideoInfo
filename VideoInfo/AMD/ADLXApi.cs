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
        private IntPtr _pSystemServices;
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

        private ADLXApi(IntPtr hDLL, IntPtr pSystemServices)
        {
            _hDLL = hDLL;
            _pSystemServices = pSystemServices;
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

            var api = new ADLXApi(hDLL, pSystem)
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

            var api = new ADLXApi(hDLL, pSystem)
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
        public AdlxInterfaceHandle GetSystemServicesHandle()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(_pSystemServices, addRef: true);
        }

        /// <summary>
        /// Get GPU tuning services wrapped in a SafeHandle.
        /// </summary>
        public AdlxInterfaceHandle GetGPUTuningServicesHandle()
        {
            var ptr = GetGPUTuningServicesInternal();
            return AdlxInterfaceHandle.From(ptr);
        }

        /// <summary>
        /// Get performance monitoring services wrapped in a SafeHandle.
        /// </summary>
        public AdlxInterfaceHandle GetPerformanceMonitoringServicesHandle()
        {
            var ptr = GetPerformanceMonitoringServicesInternal();
            return AdlxInterfaceHandle.From(ptr);
        }

        public AdlxInterfaceHandle GetPowerTuningServicesHandle()
        {
            var ptr = GetPowerTuningServicesInternal();
            return AdlxInterfaceHandle.From(ptr);
        }

        /// <summary>
        /// Enumerate GPUs and wrap them in SafeHandles for automatic release.
        /// </summary>
        public AdlxInterfaceHandle[] EnumerateGPUHandles()
        {
            var raw = EnumerateGPUsInternal();
            var handles = new AdlxInterfaceHandle[raw.Length];
            for (int i = 0; i < raw.Length; i++)
            {
                handles[i] = AdlxInterfaceHandle.From(raw[i]);
            }
            return handles;
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
                if (_terminateFn != null && _pSystemServices != IntPtr.Zero)
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
                if (_pSystemServices != IntPtr.Zero)
                {
                    try
                    {
                        ADLXHelpers.ReleaseInterface(_pSystemServices);
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

            _pSystemServices = IntPtr.Zero;
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

        private unsafe IntPtr GetGPUTuningServicesInternal()
        {
            ThrowIfDisposed();

            var systemVtbl = *(ADLXVTables.IADLXSystemVtbl**)_pSystemServices;
            var getGPUTuningServicesFn = (ADLXVTables.GetGPUTuningServicesFn)Marshal.GetDelegateForFunctionPointer(
                systemVtbl->GetGPUTuningServices, typeof(ADLXVTables.GetGPUTuningServicesFn));

            IntPtr pGPUTuningServices;
            var result = getGPUTuningServicesFn(_pSystemServices, &pGPUTuningServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU tuning services");
            }

            return pGPUTuningServices;
        }

        private unsafe IntPtr GetPowerTuningServicesInternal()
        {
            ThrowIfDisposed();

            var systemVtbl = *(ADLXVTables.IADLXSystemVtbl**)_pSystemServices;
            var getPowerTuningServicesFn = (ADLXVTables.GetPowerTuningServicesFn)Marshal.GetDelegateForFunctionPointer(
                systemVtbl->GetPowerTuningServices, typeof(ADLXVTables.GetPowerTuningServicesFn));

            IntPtr pPowerServices;
            var result = getPowerTuningServicesFn(_pSystemServices, &pPowerServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get power tuning services");

            return pPowerServices;
        }

        private unsafe IntPtr GetPerformanceMonitoringServicesInternal()
        {
            ThrowIfDisposed();

            var systemVtbl = *(ADLXVTables.IADLXSystemVtbl**)_pSystemServices;
            var getPerfMonServicesFn = (ADLXVTables.GetPerformanceMonitoringServicesFn)Marshal.GetDelegateForFunctionPointer(
                systemVtbl->GetPerformanceMonitoringServices, typeof(ADLXVTables.GetPerformanceMonitoringServicesFn));

            IntPtr pPerfMonServices;
            var result = getPerfMonServicesFn(_pSystemServices, &pPerfMonServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get performance monitoring services");
            }

            return pPerfMonServices;
        }

        private unsafe IntPtr[] EnumerateGPUsInternal()
        {
            ThrowIfDisposed();

            var systemVtbl = *(ADLXVTables.IADLXSystemVtbl**)_pSystemServices;
            var getGPUsFn = (ADLXVTables.GetGPUsFn)Marshal.GetDelegateForFunctionPointer(
                systemVtbl->GetGPUs, typeof(ADLXVTables.GetGPUsFn));

            IntPtr pGPUList;
            var result = getGPUsFn(_pSystemServices, &pGPUList);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU list");
            }

            if (pGPUList == IntPtr.Zero)
            {
                return Array.Empty<IntPtr>();
            }

            try
            {
                var listVtbl = *(ADLXVTables.IADLXGPUListVtbl**)pGPUList;
                var sizeFn = (ADLXVTables.SizeFn)Marshal.GetDelegateForFunctionPointer(
                    listVtbl->Size, typeof(ADLXVTables.SizeFn));
                var atFn = (ADLXVTables.AtFn)Marshal.GetDelegateForFunctionPointer(
                    listVtbl->At, typeof(ADLXVTables.AtFn));

                uint count = sizeFn(pGPUList);

                if (count == 0)
                {
                    return Array.Empty<IntPtr>();
                }

                var gpus = new IntPtr[count];
                for (uint i = 0; i < count; i++)
                {
                    IntPtr pGPU;
                    result = atFn(pGPUList, i, &pGPU);

                    if (result != ADLX_RESULT.ADLX_OK)
                    {
                        throw new ADLXException(result, $"Failed to get GPU at index {i}");
                    }

                    gpus[i] = pGPU;
                }

                return gpus;
            }
            finally
            {
                var listVtbl = *(ADLXVTables.IADLXListVtbl**)pGPUList;
                var releaseFn = (ADLXVTables.ReleaseFn)Marshal.GetDelegateForFunctionPointer(
                    listVtbl->Release, typeof(ADLXVTables.ReleaseFn));
                releaseFn(pGPUList);
            }
        }
    }
}
