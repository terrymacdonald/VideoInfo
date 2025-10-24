using DisplayMagicianShared;
using DisplayMagicianShared.Windows;
using IGCLWrapper; // SWIG-generated bindings
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace DisplayMagicianShared.Intel
{
    #region Data Structures

    [StructLayout(LayoutKind.Sequential)]
    public struct INTEL_DISPLAY : IEquatable<INTEL_DISPLAY>
    {
        public string Name;
        public string DeviceID;
        public uint DisplayIndex;
        public uint AdapterIndex;

        public INTEL_DISPLAY()
        {
            Name = "";
            DeviceID = "";
            DisplayIndex = 0;
            AdapterIndex = 0;
        }

        public override bool Equals(object obj) => obj is INTEL_DISPLAY other && Equals(other);
        
        public bool Equals(INTEL_DISPLAY other)
        {
            if (Name != other.Name)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY/Equals: The Name values don't equal each other");
                return false;
            }
            if (DeviceID != other.DeviceID)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY/Equals: The DeviceID values don't equal each other");
                return false;
            }
            if (DisplayIndex != other.DisplayIndex)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY/Equals: The DisplayIndex values don't equal each other");
                return false;
            }
            if (AdapterIndex != other.AdapterIndex)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY/Equals: The AdapterIndex values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Name, DeviceID, DisplayIndex, AdapterIndex).GetHashCode();
        }

        public static bool operator ==(INTEL_DISPLAY lhs, INTEL_DISPLAY rhs) => lhs.Equals(rhs);
        public static bool operator !=(INTEL_DISPLAY lhs, INTEL_DISPLAY rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INTEL_DISPLAY_WITH_SETTINGS : IEquatable<INTEL_DISPLAY_WITH_SETTINGS>
    {
        public INTEL_DISPLAY Display;
        
        // Integer Scaling (Retro Scaling)
        public bool IsSupportedIntegerScaling;
        public bool IsEnabledIntegerScaling;
        public ctl_retro_scaling_type_flag_t IntegerScalingType;
        
        // GPU Scaling
        public bool IsSupportedGPUScaling;
        public bool IsEnabledGPUScaling;
        public ctl_scaling_type_flag_t ScalingType;
        
        // Image Sharpening
        public bool IsSupportedImageSharpening;
        public bool IsEnabledImageSharpening;
        public ctl_sharpness_filter_type_flag_t SharpeningFilterType;
        public float SharpeningIntensity;

        public INTEL_DISPLAY_WITH_SETTINGS()
        {
            Display = new INTEL_DISPLAY();
            IsSupportedIntegerScaling = false;
            IsEnabledIntegerScaling = false;
            IntegerScalingType = ctl_retro_scaling_type_flag_t.CTL_RETRO_SCALING_TYPE_FLAG_INTEGER;
            IsSupportedGPUScaling = false;
            IsEnabledGPUScaling = false;
            ScalingType = ctl_scaling_type_flag_t.CTL_SCALING_TYPE_FLAG_IDENTITY;
            IsSupportedImageSharpening = false;
            IsEnabledImageSharpening = false;
            SharpeningFilterType = ctl_sharpness_filter_type_flag_t.CTL_SHARPNESS_FILTER_TYPE_FLAG_NON_ADAPTIVE;
            SharpeningIntensity = 0.0f;
        }

        public override bool Equals(object obj) => obj is INTEL_DISPLAY_WITH_SETTINGS other && Equals(other);
        
        public bool Equals(INTEL_DISPLAY_WITH_SETTINGS other)
        {
            if (!Display.Equals(other.Display))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The Display values don't equal each other");
                return false;
            }
            if (IsSupportedIntegerScaling != other.IsSupportedIntegerScaling)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedIntegerScaling values don't equal each other");
                return false;
            }
            if (IsEnabledIntegerScaling != other.IsEnabledIntegerScaling)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsEnabledIntegerScaling values don't equal each other");
                return false;
            }
            if (IntegerScalingType != other.IntegerScalingType)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IntegerScalingType values don't equal each other");
                return false;
            }
            if (IsSupportedGPUScaling != other.IsSupportedGPUScaling)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedGPUScaling values don't equal each other");
                return false;
            }
            if (IsEnabledGPUScaling != other.IsEnabledGPUScaling)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsEnabledGPUScaling values don't equal each other");
                return false;
            }
            if (ScalingType != other.ScalingType)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The ScalingType values don't equal each other");
                return false;
            }
            if (IsSupportedImageSharpening != other.IsSupportedImageSharpening)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedImageSharpening values don't equal each other");
                return false;
            }
            if (IsEnabledImageSharpening != other.IsEnabledImageSharpening)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsEnabledImageSharpening values don't equal each other");
                return false;
            }
            if (SharpeningFilterType != other.SharpeningFilterType)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The SharpeningFilterType values don't equal each other");
                return false;
            }
            if (Math.Abs(SharpeningIntensity - other.SharpeningIntensity) > 0.001f)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The SharpeningIntensity values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Display, IsSupportedIntegerScaling, IsEnabledIntegerScaling, IntegerScalingType, 
                    IsSupportedGPUScaling, IsEnabledGPUScaling, ScalingType,
                    IsSupportedImageSharpening, IsEnabledImageSharpening, SharpeningFilterType, SharpeningIntensity).GetHashCode();
        }

        public static bool operator ==(INTEL_DISPLAY_WITH_SETTINGS lhs, INTEL_DISPLAY_WITH_SETTINGS rhs) => lhs.Equals(rhs);
        public static bool operator !=(INTEL_DISPLAY_WITH_SETTINGS lhs, INTEL_DISPLAY_WITH_SETTINGS rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INTEL_COMBINED_DISPLAY : IEquatable<INTEL_COMBINED_DISPLAY>
    {
        public bool IsCombinedDisplay;
        public uint NumOutputs;
        public uint CombinedDesktopWidth;
        public uint CombinedDesktopHeight;
        public List<IntPtr> ChildDisplayHandles;  // Display handles that are part of the combined display

        public INTEL_COMBINED_DISPLAY()
        {
            IsCombinedDisplay = false;
            NumOutputs = 0;
            CombinedDesktopWidth = 0;
            CombinedDesktopHeight = 0;
            ChildDisplayHandles = new List<IntPtr>();
        }

        public override bool Equals(object obj) => obj is INTEL_COMBINED_DISPLAY other && Equals(other);
        
        public bool Equals(INTEL_COMBINED_DISPLAY other)
        {
            if (IsCombinedDisplay != other.IsCombinedDisplay)
            {
                SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY/Equals: The IsCombinedDisplay values don't equal each other");
                return false;
            }
            if (NumOutputs != other.NumOutputs)
            {
                SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY/Equals: The NumOutputs values don't equal each other");
                return false;
            }
            if (CombinedDesktopWidth != other.CombinedDesktopWidth)
            {
                SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY/Equals: The CombinedDesktopWidth values don't equal each other");
                return false;
            }
            if (CombinedDesktopHeight != other.CombinedDesktopHeight)
            {
                SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY/Equals: The CombinedDesktopHeight values don't equal each other");
                return false;
            }
            if (!ChildDisplayHandles.SequenceEqual(other.ChildDisplayHandles))
            {
                SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY/Equals: The ChildDisplayHandles values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (IsCombinedDisplay, NumOutputs, CombinedDesktopWidth, CombinedDesktopHeight, ChildDisplayHandles).GetHashCode();
        }

        public static bool operator ==(INTEL_COMBINED_DISPLAY lhs, INTEL_COMBINED_DISPLAY rhs) => lhs.Equals(rhs);
        public static bool operator !=(INTEL_COMBINED_DISPLAY lhs, INTEL_COMBINED_DISPLAY rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INTEL_DISPLAY_CONFIG : IEquatable<INTEL_DISPLAY_CONFIG>
    {
        public bool IsInUse;
        public bool IsCombinedDisplay;
        public INTEL_COMBINED_DISPLAY CombinedDisplay;
        public Dictionary<IntPtr, INTEL_DISPLAY_WITH_SETTINGS> Displays;  // Key is display handle
        public List<string> DisplayIdentifiers;

        public INTEL_DISPLAY_CONFIG()
        {
            IsInUse = false;
            IsCombinedDisplay = false;
            CombinedDisplay = new INTEL_COMBINED_DISPLAY();
            Displays = new Dictionary<IntPtr, INTEL_DISPLAY_WITH_SETTINGS>();
            DisplayIdentifiers = new List<string>();
        }

        public override bool Equals(object obj) => obj is INTEL_DISPLAY_CONFIG other && Equals(other);
        
        public bool Equals(INTEL_DISPLAY_CONFIG other)
        {
            if (IsInUse != other.IsInUse)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The IsInUse values don't equal each other");
                return false;
            }
            if (IsCombinedDisplay != other.IsCombinedDisplay)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The IsCombinedDisplay values don't equal each other");
                return false;
            }
            if (!CombinedDisplay.Equals(other.CombinedDisplay))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The CombinedDisplay values don't equal each other");
                return false;
            }
            if (!Displays.SequenceEqual(other.Displays))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The Displays values don't equal each other");
                return false;
            }            
            if (!DisplayIdentifiers.SequenceEqual(other.DisplayIdentifiers))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The DisplayIdentifiers values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (IsInUse, IsCombinedDisplay, CombinedDisplay, Displays, DisplayIdentifiers).GetHashCode();
        }

        public static bool operator ==(INTEL_DISPLAY_CONFIG lhs, INTEL_DISPLAY_CONFIG rhs) => lhs.Equals(rhs);
        public static bool operator !=(INTEL_DISPLAY_CONFIG lhs, INTEL_DISPLAY_CONFIG rhs) => !(lhs == rhs);
    }

    #endregion

    public class IntelLibrary : IDisposable
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization
        private static IntelLibrary _instance = new IntelLibrary();

        private bool _initialised = false;
        
        // To detect redundant calls
        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        
        // IGCL API Handle
        private SWIGTYPE_p__ctl_api_handle_t _igclApiHandle;
        
        private INTEL_DISPLAY_CONFIG? _activeDisplayConfig;
        public List<string> _allConnectedDisplayIdentifiers;
        public IntPtr hIGCLModule = IntPtr.Zero;
        public const string Intel_IGCL_DLL = "ControlLib.dll";
        public IntPtr hIGCLBindingModule = IntPtr.Zero;
        public const string INTEL_IGCL_BINDING_DLL = "IGCLWrapper.dll";

        const uint IGCL_IMPL_MAJOR = 1;
        const uint IGCL_IMPL_MINOR = 1;
        const uint IGCL_VERSION = (IGCL_IMPL_MAJOR << 16) | (IGCL_IMPL_MINOR & 0x0000FFFF);

        static IntelLibrary() { }
        
        public IntelLibrary()
        {
            _activeDisplayConfig = CreateDefaultConfig();
            try
            {
                _initialised = false;
                
                // Check if there is Intel hardware installed
                SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Looking for Intel PCI hardware...");
                if (WinLibrary.IsPCIVideoCardVendorInstalled(PCIVendorIDs))
                {
                    SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Intel hardware detected");
                }
                else
                {
                    SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: No Intel hardware detected");
                    return;
                }

                try {
                    // Attempt to load the Intel IGCL 64-bit DLL
                    
                    string system32Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System),"DriverStore","FileRepository");

                    Console.WriteLine($"Searching for {Intel_IGCL_DLL} in {system32Path} and its subdirectories...");

                    // Find all files matching the DLL name.
                    string[] foundDlls = FindAllFiles(system32Path, Intel_IGCL_DLL);

                    if (foundDlls.Length == 0)
                    {
                        Console.WriteLine($"{Intel_IGCL_DLL} not found in System32 or its subdirectories.");
                        return;
                    }

                    // Find the newest version among the found DLLs.
                    string newestDllPath = GetNewestDllPath(foundDlls);
                    if (newestDllPath == null)
                    {
                        Console.WriteLine("Could not determine the newest DLL version.");
                        return;
                    }

                    Console.WriteLine($"Found newest version of {Intel_IGCL_DLL} at: {newestDllPath}");

                    hIGCLModule = LoadLibrary(newestDllPath);
                    //hIGCLModule = LoadLibrary(intelDriverPath);
                    if (hIGCLModule != IntPtr.Zero)
                    {
                        SharedLogger.logger.Trace("IntelLibrary/IntelLibrary: We successfully loaded the Intel IGCL DLL which means the Intel Graphics driver software is installed.");
                    }
                    else
                    {
                        // LoadLibrary failed, DLL is not available
                        _initialised = false;
                        SharedLogger.logger.Error("IntelLibrary/IntelLibrary: Failed to load the Intel IGCL DLL. You need to download and install the Intel Graphics Driver software from the Intel support website in order to fully support Intel hardware.");
                        return;
                    }

                    // Attempt to load the Custom ADLX Binding DLL
                    SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Attempting to load the Intel IGCL CSharp Binding DLL {INTEL_IGCL_BINDING_DLL} so we can access the Intel IGCL DLL from C#");
                    hIGCLBindingModule = LoadLibrary(INTEL_IGCL_BINDING_DLL);
                    if (hIGCLBindingModule != IntPtr.Zero)
                    {

                        // Successfully loaded our custom ADLX Binding DLL, which means it's installed!
                        _initialised = true;
                        SharedLogger.logger.Trace("IntelLibrary/IntelLibrary: We successfully loaded our custom Intel IGCL CSharp Binding DLL! We can use the Intel IGCL API");
                    }
                    else
                    {
                        // LoadLibrary failed, DLL is not available
                        _initialised = false;
                        SharedLogger.logger.Error("IntelLibrary/IntelLibrary: Failed to load the Intel IGCL CSharp Binding DLL.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    _initialised = false;
                    SharedLogger.logger.Error(ex, "IntelLibrary/IntelLibrary: Exception while trying to load the Intel IGCL DLL or Intel IGCL CSharp Binding DLL. You may need to install the Intel Graphics driver.");
                }

                // Initialize ADLX with ADLXHelper
                //_adlxHelper = new ADLXHelper();
                SharedLogger.logger.Trace("IntelLibrary/IntelLibrary: Intialising Intel IGCL Helper interface");

                // Create a pointer to hold the API handle
                var ppApiHandle = IGCL.new_apiHandleP();

                /* CtlInitArgs.AppVersion = CTL_MAKE_VERSION(CTL_IMPL_MAJOR_VERSION, CTL_IMPL_MINOR_VERSION);
                CtlInitArgs.flags = 0;
                CtlInitArgs.Size = sizeof(CtlInitArgs);
                CtlInitArgs.Version = 0;*/

                ctl_init_args_t ctl_Init_Args = new ctl_init_args_t();
                ctl_Init_Args.Version = 1;
                ctl_Init_Args.flags = 0;
                ctl_Init_Args.AppVersion = (uint)IGCL.CTL_MakeVersion((uint)IGCL.CTL_IMPL_MAJOR_VERSION, (uint)IGCL.CTL_IMPL_MINOR_VERSION);

                
                //ctl_Init_Args.Size = (uint)Marshal.SizeOf(typeof(ctl_init_args_t)); // or the alias type
                //ctl_Init_Args.Version = (byte)1.1;  // or 0 if header says so
                //ctl_Init_Args.flags = (uint)ctl_init_flag_t.CTL_INIT_FLAG_USE_LEVEL_ZERO; // or 0 if no special flags
                // If there’s an ApplicationUID field:
                //ctl_Init_Args.ApplicationUID = new ctl_application_id_t();
                // zero it out if necessary
                // Initialize IGCL with default settings
                //ctl_result_t status = IGCL.IGCL_InitDefault(apiHandlePtr);
                ctl_result_t status = IGCL.ctlInit(ctl_Init_Args, ppApiHandle);
                if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                {
                    if (status == ctl_result_t.CTL_RESULT_ERROR_UNSUPPORTED_VERSION)
                    {
                        SharedLogger.logger.Error($"IntelLibrary/IntelLibrary: Error intialising Intel IGCL library. This version of the IGCL API is not supported on your PC. IGCL is supported on Alderlake-P and later CPUs and select GPUs.");
                    }
                    else if (status == ctl_result_t.CTL_RESULT_ERROR_PLATFORM_NOT_SUPPORTED)
                    {
                        SharedLogger.logger.Error($"IntelLibrary/IntelLibrary: Error intialising Intel IGCL library. The IGCL API Platform is not supported on your PC. IGCL is supported on Alderlake-P and later CPUs and select GPUs.");
                    }
                    else
                    {
                        SharedLogger.logger.Error($"IntelLibrary/IntelLibrary: Error intialising Intel IGCL library. IGCL.ctlInit() returned error code {status.ToString("G")}");
                    }
                    _initialised = false;
                    return;
                }
                else
                {
                    // Get the actual API handle from the pointer
                    _igclApiHandle = IGCL.apiHandleP_value(ppApiHandle);
                    Console.WriteLine("IGCL initialized successfully");
                    SharedLogger.logger.Error($"IntelLibrary/IntelLibrary: Successfully intialised Intel IGCL library!");
                    _initialised = true;
                }
            }
            catch (TypeInitializationException ex)
            {
                SharedLogger.logger.Info(ex, $"IntelLibrary/IntelLibrary: TypeInitializationException trying to load the Intel IGCL DLL {Intel_IGCL_DLL}. This generally means you don't have the Intel IGCL driver installed.");
                _initialised = false;
                return;
            }
            catch (DllNotFoundException ex)
            {
                // If we get here then the Intel IGCL DLL wasn't found. We can't continue to use it, so we log the error and exit
                SharedLogger.logger.Info(ex, $"IntelLibrary/IntelLibrary: DllNotFoundException trying to load the Intel IGCL DLL {Intel_IGCL_DLL}. This generally means you don't have the Intel IGCL driver installed.");
                _initialised = false;
                return;
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Info(ex, $"IntelLibrary/IntelLibrary: A general exception trying to load the Intel IGCL DLL {Intel_IGCL_DLL}.");
                _initialised = false;
                return;
            }


            /*SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Attempting to initialise the Intel IGCL API");
                try
                {
                    // Initialize IGCL - using the helper pointer functions
                    SWIGTYPE_p_p__ctl_api_handle_t ppApiHandle = IGCL.new_deviceAdapterHandleP();
                    ctl_result_t result = IGCL.IGCL_InitDefault(ppApiHandle);
                    
                    if (result == ctl_result_t.CTL_RESULT_SUCCESS)
                    {
                        // Extract the actual API handle from the pointer-to-pointer
                        IntPtr apiHandlePtr = IGCL.deviceAdapterHandleP_value(ppApiHandle);
                        _igclApiHandle = new SWIGTYPE_p__ctl_api_handle_t(apiHandlePtr, false);
                        _initialised = true;
                        SharedLogger.logger.Trace("IntelLibrary/IntelLibrary: We successfully initialised the Intel IGCL API which means that the Intel Graphics driver software is installed and working.");
                    }
                    else
                    {
                        _initialised = false;
                        SharedLogger.logger.Error($"IntelLibrary/IntelLibrary: Failed to initialise the Intel IGCL API. IGCL_InitDefault() returned error code {result}");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    _initialised = false;
                    SharedLogger.logger.Error(ex, "IntelLibrary/IntelLibrary: Exception while trying to initialise the Intel IGCL API. You may need to install the Intel Graphics driver.");
                    return;
                }

                SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Automatically getting the Intel Display Configuration");
                _activeDisplayConfig = GetActiveConfig();
                SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Automatically getting the Intel Connected Display Identifiers");
                _allConnectedDisplayIdentifiers = GetAllConnectedDisplayIdentifiers(out bool failure);

            }
            catch (Exception ex)
            {
                SharedLogger.logger.Info(ex, $"IntelLibrary/IntelLibrary: A general exception trying to load the Intel IGCL DLL {Intel_IGCL_DLL}.");
                _initialised = false; 
                return;
            }*/
        }

        ~IntelLibrary()
        {
            SharedLogger.logger.Trace("IntelLibrary/~IntelLibrary: Destroying IGCL Library");
            Dispose(true);
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
            }

            if (_initialised && _igclApiHandle != null)
            {
                // Close the IGCL API to avoid memory leaks
                SharedLogger.logger.Trace("IntelLibrary/Dispose: Closing the Intel IGCL API");
                IGCL.IGCL_Close(_igclApiHandle);
                _igclApiHandle = null;
            }

            if (hIGCLModule != IntPtr.Zero)
            {
                SharedLogger.logger.Trace("IntelLibrary/Dispose: Freeing the Intel IGCL DLL");
                FreeLibrary(hIGCLModule);
                hIGCLModule = IntPtr.Zero;
            }

            _disposed = true;
        }

        public static void KeepVideoCardOn()
        {
            // Not needed for Intel
        }

        public bool IsInstalled
        {
            get
            {
                return _initialised;
            }
        }

        public List<string> PCIVendorIDs
        {
            get
            {
                // Intel PCI Vendor ID
                return new List<string>() { "8086" };
            }
        }

        public INTEL_DISPLAY_CONFIG ActiveDisplayConfig
        {
            get
            {
                if(_activeDisplayConfig == null)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/ActiveDisplayConfig: ActiveDisplayConfig is null, so creating a new one");
                    _activeDisplayConfig = CreateDefaultConfig();
                }
                return _activeDisplayConfig.Value;
            }
            set
            {
                _activeDisplayConfig = value;
            }
        }

        public List<string> CurrentDisplayIdentifiers
        {
            get
            {
                return _activeDisplayConfig.Value.DisplayIdentifiers;
            }
        }

        public static IntelLibrary GetLibrary()
        {
            if (_instance == null)
            {
                _instance = new IntelLibrary();
            }

            return _instance;
        }

        public INTEL_DISPLAY_CONFIG CreateDefaultConfig()
        {
            INTEL_DISPLAY_CONFIG myDefaultConfig = new INTEL_DISPLAY_CONFIG
            {
                IsInUse = false
            };

            return myDefaultConfig;
        }

        public bool UpdateActiveConfig()
        {
            SharedLogger.logger.Trace($"IntelLibrary/UpdateActiveConfig: Updating the currently active config");
            try
            {
                _activeDisplayConfig = GetActiveConfig();
                _allConnectedDisplayIdentifiers = GetAllConnectedDisplayIdentifiers(out bool failure);
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Trace(ex, $"IntelLibrary/UpdateActiveConfig: Exception updating the currently active config");
                return false;
            }

            return true;
        }

        public INTEL_DISPLAY_CONFIG GetActiveConfig()
        {
            SharedLogger.logger.Trace($"IntelLibrary/GetActiveConfig: Getting the currently active config");
            bool allDisplays = true;
            return GetIntelDisplayConfig(allDisplays);
        }

        private INTEL_DISPLAY_CONFIG GetIntelDisplayConfig(bool allDisplays = false)
        {
            // Create empty config struct so we know there are no nulls in there to break the json serializer
            INTEL_DISPLAY_CONFIG myDisplayConfig = CreateDefaultConfig();

            if (_initialised && _igclApiHandle != null)
            {
                ctl_result_t status = ctl_result_t.CTL_RESULT_SUCCESS;

                // Enumerate Intel adapters
                SWIGTYPE_p_unsigned_int pAdapterCount = IGCL.new_igcl_uint32P();
                IGCL.igcl_uint32P_assign(pAdapterCount, 0);
                
                // First call to get count
                status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, null);
                uint adapterCount = IGCL.igcl_uint32P_value(pAdapterCount);
                
                if (status != ctl_result_t.CTL_RESULT_SUCCESS || adapterCount == 0)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: No Intel adapters found or error getting adapter count. Status: {status}");
                    return myDisplayConfig;
                }

                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Found {adapterCount} Intel adapter(s)");

                // Allocate array for adapter handles
                SWIGTYPE_p_p__ctl_device_adapter_handle_t ppAdapters = IGCL.new_deviceAdapterHandleP();
                
                // Second call to get actual adapters
                status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, ppAdapters);
                
                if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                {
                    SharedLogger.logger.Error($"IntelLibrary/GetIntelDisplayConfig: Error enumerating Intel adapters. Status: {status}");
                    return myDisplayConfig;
                }

                IntPtr adaptersPtr = IGCL.deviceAdapterHandleP_value(ppAdapters);

                // Iterate through adapters
                for (uint adapterIdx = 0; adapterIdx < adapterCount; adapterIdx++)
                {
                    // Get adapter handle at this index
                    IntPtr hAdapter = Marshal.ReadIntPtr(adaptersPtr, (int)(adapterIdx * IntPtr.Size));

                    // Get adapter properties
                    ctl_device_adapter_properties_t adapterProps = IGCL.new_adapterPropertiesP();
                    status = IGCL.IGCL_GetAdapterProperties(hAdapter, adapterProps);
                    
                    if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                    {
                        SharedLogger.logger.Warn($"IntelLibrary/GetIntelDisplayConfig: Failed to get properties for adapter {adapterIdx}");
                        continue;
                    }

                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Processing adapter {adapterIdx}: {adapterProps.name}");

                    //------------------------------------
                    // CHECK FOR COMBINED DISPLAY CONFIGURATION
                    //------------------------------------
                    ctl_combined_display_args_t combinedDisplayArgs = new ctl_combined_display_args_t();
                    combinedDisplayArgs.OpType = ctl_combined_display_optype_t.CTL_COMBINED_DISPLAY_OPTYPE_QUERY_CONFIG;
                    combinedDisplayArgs.IsSupported = false;
                    
                    status = IGCL.ctlGetSetCombinedDisplay(hAdapter, combinedDisplayArgs);
                    
                    if (status == ctl_result_t.CTL_RESULT_SUCCESS && combinedDisplayArgs.IsSupported && combinedDisplayArgs.NumOutputs > 1)
                    {
                        myDisplayConfig.IsCombinedDisplay = true;
                        myDisplayConfig.CombinedDisplay.IsCombinedDisplay = true;
                        myDisplayConfig.CombinedDisplay.NumOutputs = combinedDisplayArgs.NumOutputs;
                        myDisplayConfig.CombinedDisplay.CombinedDesktopWidth = combinedDisplayArgs.CombinedDesktopWidth;
                        myDisplayConfig.CombinedDisplay.CombinedDesktopHeight = combinedDisplayArgs.CombinedDesktopHeight;
                        
                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Combined Display detected: {combinedDisplayArgs.NumOutputs} outputs, {combinedDisplayArgs.CombinedDesktopWidth}x{combinedDisplayArgs.CombinedDesktopHeight}");
                        
                        // Store child display handles if available
                        // Note: The actual child handles would need to be extracted from pChildInfo
                        // This is a simplified version - full implementation would iterate through child info
                    }
                    else
                    {
                        myDisplayConfig.IsCombinedDisplay = false;
                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: No Combined Display detected for adapter {adapterIdx}");
                    }

                    // Enumerate displays for this adapter
                    SWIGTYPE_p_unsigned_int pDisplayCount = IGCL.new_igcl_uint32P();
                    IGCL.igcl_uint32P_assign(pDisplayCount, 0);
                    
                    // First call to get display count
                    status = IGCL.IGCL_EnumerateDisplays(hAdapter, pDisplayCount, null);
                    uint displayCount = IGCL.igcl_uint32P_value(pDisplayCount);
                    
                    if (status != ctl_result_t.CTL_RESULT_SUCCESS || displayCount == 0)
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: No displays found for adapter {adapterIdx} or error. Status: {status}");
                        continue;
                    }

                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Found {displayCount} display(s) on adapter {adapterIdx}");

                    // Allocate array for display handles
                    SWIGTYPE_p_p__ctl_display_output_handle_t ppDisplays = IGCL.new_displayOutputHandleP();
                    
                    // Second call to get actual displays
                    status = IGCL.IGCL_EnumerateDisplays(hAdapter, pDisplayCount, ppDisplays);
                    
                    if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                    {
                        SharedLogger.logger.Error($"IntelLibrary/GetIntelDisplayConfig: Error enumerating displays for adapter {adapterIdx}. Status: {status}");
                        continue;
                    }

                    IntPtr displaysPtr = IGCL.displayOutputHandleP_value(ppDisplays);

                    // Iterate through displays
                    for (uint displayIdx = 0; displayIdx < displayCount; displayIdx++)
                    {
                        // Get display handle at this index
                        IntPtr hDisplay = Marshal.ReadIntPtr(displaysPtr, (int)(displayIdx * IntPtr.Size));

                        // Get display properties
                        ctl_display_properties_t displayProps = IGCL.new_displayPropertiesP();
                        status = IGCL.IGCL_GetDisplayProperties(hDisplay, displayProps);
                        
                        if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                        {
                            SharedLogger.logger.Warn($"IntelLibrary/GetIntelDisplayConfig: Failed to get properties for display {displayIdx} on adapter {adapterIdx}");
                            continue;
                        }

                        // Create display with settings
                        INTEL_DISPLAY_WITH_SETTINGS displayWithSettings = new INTEL_DISPLAY_WITH_SETTINGS();
                        
                        // Get display name - using display index as identifier since name isn't directly available
                        string displayName = $"Intel Display {displayIdx} on Adapter {adapterIdx}";
                        string deviceId = $"IntelDisplay_{adapterIdx}_{displayIdx}";
                        
                        displayWithSettings.Display = new INTEL_DISPLAY
                        {
                            Name = displayName,
                            DeviceID = deviceId,
                            DisplayIndex = displayIdx,
                            AdapterIndex = adapterIdx
                        };

                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Processing display {displayIdx}: {displayName}");

                        //------------------------------------
                        // GET INTEGER SCALING (RETRO SCALING) SETTINGS
                        //------------------------------------
                        ctl_retro_scaling_caps_t retroScalingCaps = new ctl_retro_scaling_caps_t();
                        status = IGCL.ctlGetSupportedRetroScalingCapability(hAdapter, retroScalingCaps);
                        
                        if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                        {
                            displayWithSettings.IsSupportedIntegerScaling = true;
                            
                            ctl_retro_scaling_settings_t retroScalingSettings = new ctl_retro_scaling_settings_t();
                            retroScalingSettings.Get = true;
                            status = IGCL.ctlGetSetRetroScaling(hAdapter, retroScalingSettings);
                            
                            if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                            {
                                displayWithSettings.IsEnabledIntegerScaling = retroScalingSettings.Enable;
                                displayWithSettings.IntegerScalingType = (ctl_retro_scaling_type_flag_t)retroScalingSettings.RetroScalingType;
                                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Integer Scaling: Enabled={retroScalingSettings.Enable}, Type={retroScalingSettings.RetroScalingType}");
                            }
                        }

                        //------------------------------------
                        // GET GPU SCALING SETTINGS
                        //------------------------------------
                        ctl_scaling_caps_t scalingCaps = new ctl_scaling_caps_t();
                        status = IGCL.ctlGetSupportedScalingCapability(hDisplay, scalingCaps);
                        
                        if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                        {
                            displayWithSettings.IsSupportedGPUScaling = true;
                            
                            ctl_scaling_settings_t scalingSettings = new ctl_scaling_settings_t();
                            status = IGCL.ctlGetCurrentScaling(hDisplay, scalingSettings);
                            
                            if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                            {
                                displayWithSettings.IsEnabledGPUScaling = scalingSettings.Enable;
                                displayWithSettings.ScalingType = (ctl_scaling_type_flag_t)scalingSettings.ScalingType;
                                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: GPU Scaling: Enabled={scalingSettings.Enable}, Type={scalingSettings.ScalingType}");
                            }
                        }

                        //------------------------------------
                        // GET IMAGE SHARPENING SETTINGS
                        //------------------------------------
                        ctl_sharpness_caps_t sharpnessCaps = new ctl_sharpness_caps_t();
                        status = IGCL.ctlGetSharpnessCaps(hDisplay, sharpnessCaps);
                        
                        if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                        {
                            displayWithSettings.IsSupportedImageSharpening = true;
                            
                            ctl_sharpness_settings_t sharpnessSettings = new ctl_sharpness_settings_t();
                            status = IGCL.ctlGetCurrentSharpness(hDisplay, sharpnessSettings);
                            
                            if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                            {
                                displayWithSettings.IsEnabledImageSharpening = sharpnessSettings.Enable;
                                displayWithSettings.SharpeningFilterType = (ctl_sharpness_filter_type_flag_t)sharpnessSettings.FilterType;
                                displayWithSettings.SharpeningIntensity = sharpnessSettings.Intensity;
                                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Image Sharpening: Enabled={sharpnessSettings.Enable}, Intensity={sharpnessSettings.Intensity}");
                            }
                        }

                        // Add display to configuration
                        myDisplayConfig.Displays.Add(hDisplay, displayWithSettings);
                    }
                }

                // Get display identifiers
                myDisplayConfig.DisplayIdentifiers = GetCurrentDisplayIdentifiers(out bool failure);
                myDisplayConfig.IsInUse = true;
            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/GetIntelDisplayConfig: ERROR - Tried to run GetIntelDisplayConfig but the Intel IGCL library isn't initialised!");
                return CreateDefaultConfig();
            }
            
            // Return the configuration
            return myDisplayConfig;
        }

        /// <summary>
        /// Searches for all instances of a file recursively within a specified directory.
        /// </summary>
        private string[] FindAllFiles(string startPath, string fileName)
        {
            try
            {
                return Directory.GetFiles(startPath, fileName, SearchOption.AllDirectories);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is DirectoryNotFoundException)
            {
                // Ignore access errors in protected directories.
                Console.WriteLine($"Access error during search: {ex.Message}");
            }

            return new string[0];
        }

        /// <summary>
        /// Compares the file version information of multiple DLLs and returns the path to the newest one.
        /// </summary>
        private string GetNewestDllPath(string[] dllPaths)
        {
            Version newestVersion = new Version(0, 0);
            string newestPath = null;

            foreach (string path in dllPaths)
            {
                try
                {
                    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);
                    Version fileVersion = new Version(versionInfo.FileVersion);

                    if (fileVersion > newestVersion)
                    {
                        newestVersion = fileVersion;
                        newestPath = path;
                    }
                }
                catch (Exception ex)
                {
                    // This can happen if the file is locked or corrupt.
                    Console.WriteLine($"Could not get version info for {path}. Error: {ex.Message}");
                }
            }

            return newestPath;
        }

        public string PrintActiveConfig()
        {
            string stringToReturn = "";

            // Get the current config
            INTEL_DISPLAY_CONFIG displayConfig = ActiveDisplayConfig;

            stringToReturn += $"****** INTEL VIDEO CARDS *******\n";

            if (_initialised && _igclApiHandle != null)
            {
                ctl_result_t status = ctl_result_t.CTL_RESULT_SUCCESS;

                // Enumerate Intel adapters
                SWIGTYPE_p_unsigned_int pAdapterCount = IGCL.new_igcl_uint32P();
                IGCL.igcl_uint32P_assign(pAdapterCount, 0);
                
                status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, null);
                uint adapterCount = IGCL.igcl_uint32P_value(pAdapterCount);
                
                if (status == ctl_result_t.CTL_RESULT_SUCCESS && adapterCount > 0)
                {
                    stringToReturn += $"Found {adapterCount} Intel adapter(s)\n\n";

                    SWIGTYPE_p_p__ctl_device_adapter_handle_t ppAdapters = IGCL.new_deviceAdapterHandleP();
                    status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, ppAdapters);
                    
                    if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                    {
                        IntPtr adaptersPtr = IGCL.deviceAdapterHandleP_value(ppAdapters);

                        for (uint adapterIdx = 0; adapterIdx < adapterCount; adapterIdx++)
                        {
                            IntPtr hAdapter = Marshal.ReadIntPtr(adaptersPtr, (int)(adapterIdx * IntPtr.Size));

                            ctl_device_adapter_properties_t adapterProps = IGCL.new_adapterPropertiesP();
                            status = IGCL.IGCL_GetAdapterProperties(hAdapter, adapterProps);
                            
                            if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                            {
                                stringToReturn += $"Adapter #{adapterIdx}\n";
                                stringToReturn += $"  Name: {adapterProps.name}\n";
                                stringToReturn += $"  PCI Vendor ID: 0x{adapterProps.pci_vendor_id:X4}\n";
                                stringToReturn += $"  PCI Device ID: 0x{adapterProps.pci_device_id:X4}\n";
                                stringToReturn += $"  Driver Version: {adapterProps.driver_version}\n";
                                stringToReturn += $"  Device Type: {adapterProps.device_type}\n";
                                stringToReturn += $"  Graphics Properties: {adapterProps.graphics_adapter_properties}\n\n";
                            }
                        }
                    }
                }
            }

            stringToReturn += $"\n\n";
            // Now we also get the Windows CCD Library info, and add it to the above
            stringToReturn += WinLibrary.GetLibrary().PrintActiveConfig();

            return stringToReturn;
        }

        public bool SetActiveConfig(INTEL_DISPLAY_CONFIG displayConfig, int delayInMs)
        {
            if (_initialised && _igclApiHandle != null)
            {
                ctl_result_t status = ctl_result_t.CTL_RESULT_SUCCESS;

                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Managing Intel Combined Display configuration");

                // Enumerate Intel adapters
                SWIGTYPE_p_unsigned_int pAdapterCount = IGCL.new_igcl_uint32P();
                IGCL.igcl_uint32P_assign(pAdapterCount, 0);
                
                status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, null);
                uint adapterCount = IGCL.igcl_uint32P_value(pAdapterCount);
                
                if (status != ctl_result_t.CTL_RESULT_SUCCESS || adapterCount == 0)
                {
                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: No Intel adapters found or error getting adapter count. Status: {status}");
                    return false;
                }

                SWIGTYPE_p_p__ctl_device_adapter_handle_t ppAdapters = IGCL.new_deviceAdapterHandleP();
                status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, ppAdapters);
                
                if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                {
                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error enumerating Intel adapters. Status: {status}");
                    return false;
                }

                IntPtr adaptersPtr = IGCL.deviceAdapterHandleP_value(ppAdapters);

                // Iterate through adapters
                for (uint adapterIdx = 0; adapterIdx < adapterCount; adapterIdx++)
                {
                    IntPtr hAdapter = Marshal.ReadIntPtr(adaptersPtr, (int)(adapterIdx * IntPtr.Size));

                    // If the display config needs a Combined Display then let's create one
                    if (displayConfig.IsCombinedDisplay)
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: New display layout requires a Combined Display");

                        // Check if the Combined Display is already set exactly as we want it
                        if (displayConfig.CombinedDisplay.Equals(ActiveDisplayConfig.CombinedDisplay))
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Combined Display layout is exactly the same as the one we want, so skipping setting up the Combined Display");
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Attempting to create the Intel Combined Display");
                            
                            ctl_combined_display_args_t combinedDisplayArgs = new ctl_combined_display_args_t();
                            combinedDisplayArgs.OpType = ctl_combined_display_optype_t.CTL_COMBINED_DISPLAY_OPTYPE_ENABLE;
                            combinedDisplayArgs.NumOutputs = (byte)displayConfig.CombinedDisplay.NumOutputs;
                            combinedDisplayArgs.CombinedDesktopWidth = displayConfig.CombinedDisplay.CombinedDesktopWidth;
                            combinedDisplayArgs.CombinedDesktopHeight = displayConfig.CombinedDisplay.CombinedDesktopHeight;
                            
                            // Note: In a full implementation, you would need to populate pChildInfo with the
                            // display handles and layout information from displayConfig.CombinedDisplay.ChildDisplayHandles
                            
                            status = IGCL.ctlGetSetCombinedDisplay(hAdapter, combinedDisplayArgs);
                            
                            if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                            {
                                SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error creating the Intel Combined Display. ctlGetSetCombinedDisplay() returned error code {status}");
                                return false;
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Successfully created the Intel Combined Display");
                                
                                // Verify the created display matches what we want
                                ctl_combined_display_args_t queryArgs = new ctl_combined_display_args_t();
                                queryArgs.OpType = ctl_combined_display_optype_t.CTL_COMBINED_DISPLAY_OPTYPE_QUERY_CONFIG;
                                
                                status = IGCL.ctlGetSetCombinedDisplay(hAdapter, queryArgs);
                                
                                if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                                {
                                    if (queryArgs.NumOutputs == displayConfig.CombinedDisplay.NumOutputs &&
                                        queryArgs.CombinedDesktopWidth == displayConfig.CombinedDisplay.CombinedDesktopWidth &&
                                        queryArgs.CombinedDesktopHeight == displayConfig.CombinedDisplay.CombinedDesktopHeight)
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: This new Combined Display layout matches the desired configuration.");
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Warn($"IntelLibrary/SetActiveConfig: This new Combined Display layout is different from the one originally saved. You may need to update this desktop profile.");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: New display layout does NOT require a Combined Display");

                        if (ActiveDisplayConfig.IsCombinedDisplay)
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Combined Display layout is currently in use but is NOT required, so we need to destroy the Combined Display");

                            ctl_combined_display_args_t combinedDisplayArgs = new ctl_combined_display_args_t();
                            combinedDisplayArgs.OpType = ctl_combined_display_optype_t.CTL_COMBINED_DISPLAY_OPTYPE_DISABLE;
                            
                            status = IGCL.ctlGetSetCombinedDisplay(hAdapter, combinedDisplayArgs);
                            
                            if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                            {
                                SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error destroying the Intel Combined Display. ctlGetSetCombinedDisplay() returned error code {status}");
                                return false;
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Successfully destroyed the Intel Combined Display.");
                            }
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Combined Display layout is not currently in use and is NOT required, so leaving things as they are.");
                        }
                    }
                }
            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: ERROR - Tried to run SetActiveConfig but the Intel IGCL library isn't initialised!");
                throw new IntelLibraryException($"Tried to run SetActiveConfig but the Intel IGCL library isn't initialised!");
            }

            return true;
        }

        public bool SetActiveConfigOverride(INTEL_DISPLAY_CONFIG displayConfig, int delayInMs)
        {
            if (_initialised && _igclApiHandle != null)
            {
                ctl_result_t status = ctl_result_t.CTL_RESULT_SUCCESS;

                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Applying display settings stored in the display configuration");

                // Enumerate Intel adapters
                SWIGTYPE_p_unsigned_int pAdapterCount = IGCL.new_igcl_uint32P();
                IGCL.igcl_uint32P_assign(pAdapterCount, 0);
                
                status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, null);
                uint adapterCount = IGCL.igcl_uint32P_value(pAdapterCount);
                
                if (status != ctl_result_t.CTL_RESULT_SUCCESS || adapterCount == 0)
                {
                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: No Intel adapters found or error getting adapter count. Status: {status}");
                    return false;
                }

                SWIGTYPE_p_p__ctl_device_adapter_handle_t ppAdapters = IGCL.new_deviceAdapterHandleP();
                status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, ppAdapters);
                
                if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                {
                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: Error enumerating Intel adapters. Status: {status}");
                    return false;
                }

                IntPtr adaptersPtr = IGCL.deviceAdapterHandleP_value(ppAdapters);

                // Iterate through adapters
                for (uint adapterIdx = 0; adapterIdx < adapterCount; adapterIdx++)
                {
                    IntPtr hAdapter = Marshal.ReadIntPtr(adaptersPtr, (int)(adapterIdx * IntPtr.Size));

                    // Enumerate displays for this adapter
                    SWIGTYPE_p_unsigned_int pDisplayCount = IGCL.new_igcl_uint32P();
                    IGCL.igcl_uint32P_assign(pDisplayCount, 0);
                    
                    status = IGCL.IGCL_EnumerateDisplays(hAdapter, pDisplayCount, null);
                    uint displayCount = IGCL.igcl_uint32P_value(pDisplayCount);
                    
                    if (status != ctl_result_t.CTL_RESULT_SUCCESS || displayCount == 0)
                    {
                        continue;
                    }

                    SWIGTYPE_p_p__ctl_display_output_handle_t ppDisplays = IGCL.new_displayOutputHandleP();
                    status = IGCL.IGCL_EnumerateDisplays(hAdapter, pDisplayCount, ppDisplays);
                    
                    if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                    {
                        continue;
                    }

                    IntPtr displaysPtr = IGCL.displayOutputHandleP_value(ppDisplays);

                    // Iterate through displays
                    for (uint displayIdx = 0; displayIdx < displayCount; displayIdx++)
                    {
                        IntPtr hDisplay = Marshal.ReadIntPtr(displaysPtr, (int)(displayIdx * IntPtr.Size));

                        // Find the stored settings for this display
                        if (!displayConfig.Displays.ContainsKey(hDisplay))
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: No stored settings found for display handle {hDisplay}, skipping");
                            continue;
                        }

                        INTEL_DISPLAY_WITH_SETTINGS displaySettingsWeStored = displayConfig.Displays[hDisplay];
                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Applying settings for display: {displaySettingsWeStored.Display.Name}");

                        //------------------------------------
                        // SET INTEGER SCALING IF NEEDED
                        //------------------------------------
                        if (displaySettingsWeStored.IsSupportedIntegerScaling)
                        {
                            ctl_retro_scaling_caps_t retroScalingCaps = new ctl_retro_scaling_caps_t();
                            status = IGCL.ctlGetSupportedRetroScalingCapability(hAdapter, retroScalingCaps);
                            
                            if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                            {
                                ctl_retro_scaling_settings_t retroScalingSettings = new ctl_retro_scaling_settings_t();
                                retroScalingSettings.Get = true;
                                status = IGCL.ctlGetSetRetroScaling(hAdapter, retroScalingSettings);
                                
                                if (status == ctl_result_t.CTL_RESULT_SUCCESS && 
                                    (retroScalingSettings.Enable != displaySettingsWeStored.IsEnabledIntegerScaling ||
                                     (uint)retroScalingSettings.RetroScalingType != (uint)displaySettingsWeStored.IntegerScalingType))
                                {
                                    retroScalingSettings.Get = false;
                                    retroScalingSettings.Enable = displaySettingsWeStored.IsEnabledIntegerScaling;
                                    retroScalingSettings.RetroScalingType = (uint)displaySettingsWeStored.IntegerScalingType;
                                    
                                    status = IGCL.ctlGetSetRetroScaling(hAdapter, retroScalingSettings);
                                    if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Integer Scaling to Enabled={displaySettingsWeStored.IsEnabledIntegerScaling}, Type={displaySettingsWeStored.IntegerScalingType}");
                                    }
                                }
                                else if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Integer Scaling already set to desired values, skipping");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Integer Scaling not supported by current hardware, skipping");
                            }
                        }

                        //------------------------------------
                        // SET GPU SCALING IF NEEDED
                        //------------------------------------
                        if (displaySettingsWeStored.IsSupportedGPUScaling)
                        {
                            ctl_scaling_caps_t scalingCaps = new ctl_scaling_caps_t();
                            status = IGCL.ctlGetSupportedScalingCapability(hDisplay, scalingCaps);
                            
                            if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                            {
                                ctl_scaling_settings_t scalingSettings = new ctl_scaling_settings_t();
                                status = IGCL.ctlGetCurrentScaling(hDisplay, scalingSettings);
                                
                                if (status == ctl_result_t.CTL_RESULT_SUCCESS &&
                                    (scalingSettings.Enable != displaySettingsWeStored.IsEnabledGPUScaling ||
                                     (uint)scalingSettings.ScalingType != (uint)displaySettingsWeStored.ScalingType))
                                {
                                    scalingSettings.Enable = displaySettingsWeStored.IsEnabledGPUScaling;
                                    scalingSettings.ScalingType = (uint)displaySettingsWeStored.ScalingType;
                                    
                                    status = IGCL.ctlSetCurrentScaling(hDisplay, scalingSettings);
                                    if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set GPU Scaling to Enabled={displaySettingsWeStored.IsEnabledGPUScaling}, Type={displaySettingsWeStored.ScalingType}");
                                    }
                                }
                                else if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: GPU Scaling already set to desired values, skipping");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: GPU Scaling not supported by current hardware, skipping");
                            }
                        }

                        //------------------------------------
                        // SET IMAGE SHARPENING IF NEEDED
                        //------------------------------------
                        if (displaySettingsWeStored.IsSupportedImageSharpening)
                        {
                            ctl_sharpness_caps_t sharpnessCaps = new ctl_sharpness_caps_t();
                            status = IGCL.ctlGetSharpnessCaps(hDisplay, sharpnessCaps);
                            
                            if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                            {
                                ctl_sharpness_settings_t sharpnessSettings = new ctl_sharpness_settings_t();
                                status = IGCL.ctlGetCurrentSharpness(hDisplay, sharpnessSettings);
                                
                                if (status == ctl_result_t.CTL_RESULT_SUCCESS &&
                                    (sharpnessSettings.Enable != displaySettingsWeStored.IsEnabledImageSharpening ||
                                     (uint)sharpnessSettings.FilterType != (uint)displaySettingsWeStored.SharpeningFilterType ||
                                     Math.Abs(sharpnessSettings.Intensity - displaySettingsWeStored.SharpeningIntensity) > 0.001f))
                                {
                                    sharpnessSettings.Enable = displaySettingsWeStored.IsEnabledImageSharpening;
                                    sharpnessSettings.FilterType = (uint)displaySettingsWeStored.SharpeningFilterType;
                                    sharpnessSettings.Intensity = displaySettingsWeStored.SharpeningIntensity;
                                    
                                    status = IGCL.ctlSetCurrentSharpness(hDisplay, sharpnessSettings);
                                    if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Image Sharpening to Enabled={displaySettingsWeStored.IsEnabledImageSharpening}, Intensity={displaySettingsWeStored.SharpeningIntensity}");
                                    }
                                }
                                else if (status == ctl_result_t.CTL_RESULT_SUCCESS)
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Image Sharpening already set to desired values, skipping");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Image Sharpening not supported by current hardware, skipping");
                            }
                        }
                    }
                }
            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: ERROR - Tried to run SetActiveConfigOverride but the Intel IGCL library isn't initialised!");
                throw new IntelLibraryException($"Tried to run SetActiveConfigOverride but the Intel IGCL library isn't initialised!");
            }

            return true;
        }

        public bool IsActiveConfig(INTEL_DISPLAY_CONFIG displayConfig)
        {
            SharedLogger.logger.Trace($"IntelLibrary/IsActiveConfig: Checking whether the display configuration is already being used.");
            if (displayConfig.Equals(_activeDisplayConfig))
            {
                SharedLogger.logger.Trace($"IntelLibrary/IsActiveConfig: The display configuration is already being used");
                return true;
            }
            else
            {
                SharedLogger.logger.Trace($"IntelLibrary/IsActiveConfig: The display configuration is NOT currently in use");
                return false;
            }
        }

        public bool IsValidConfig(INTEL_DISPLAY_CONFIG displayConfig)
        {
            SharedLogger.logger.Trace($"IntelLibrary/IsValidConfig: Testing whether the display configuration is valid");
            if (displayConfig.IsInUse)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPossibleConfig(INTEL_DISPLAY_CONFIG displayConfig)
        {
            SharedLogger.logger.Trace($"IntelLibrary/IsPossibleConfig: Testing whether the Intel display configuration is possible to be used now");

            // If both display identifiers are 0 then no displays were connected via Intel and we should just return true.
            if (displayConfig.DisplayIdentifiers.Count == 0 && _allConnectedDisplayIdentifiers.Count == 0)
            {
                return true;
            }
            // but if only allconnected count is 0 then we have a problem
            else if (_allConnectedDisplayIdentifiers.Count == 0)
            {
                return false;
            }

            // Check that we have all the displayConfig DisplayIdentifiers we need available now            
            if (displayConfig.DisplayIdentifiers.All(value => _allConnectedDisplayIdentifiers.Contains(value)))
            {
                SharedLogger.logger.Trace($"IntelLibrary/IsPossibleConfig: Success! The Intel display configuration is possible to be used now");
                return true;
            }
            else
            {
                SharedLogger.logger.Trace($"IntelLibrary/IsPossibleConfig: Uh oh! The Intel display configuration cannot be used now");
                return false;
            }
        }

        public List<string> GetCurrentDisplayIdentifiers(out bool failure)
        {
            SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Getting the current display identifiers for the displays in use now");

            List<string> displayIdentifiers = new List<string>();
            failure = false;

            if (_initialised && _igclApiHandle != null)
            {
                ctl_result_t status = ctl_result_t.CTL_RESULT_SUCCESS;

                // Enumerate Intel adapters
                SWIGTYPE_p_unsigned_int pAdapterCount = IGCL.new_igcl_uint32P();
                IGCL.igcl_uint32P_assign(pAdapterCount, 0);
                
                status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, null);
                uint adapterCount = IGCL.igcl_uint32P_value(pAdapterCount);
                
                if (status != ctl_result_t.CTL_RESULT_SUCCESS || adapterCount == 0)
                {
                    failure = true;
                    return displayIdentifiers;
                }

                SWIGTYPE_p_p__ctl_device_adapter_handle_t ppAdapters = IGCL.new_deviceAdapterHandleP();
                status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, ppAdapters);
                
                if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                {
                    failure = true;
                    return displayIdentifiers;
                }

                IntPtr adaptersPtr = IGCL.deviceAdapterHandleP_value(ppAdapters);

                for (uint adapterIdx = 0; adapterIdx < adapterCount; adapterIdx++)
                {
                    IntPtr hAdapter = Marshal.ReadIntPtr(adaptersPtr, (int)(adapterIdx * IntPtr.Size));

                    ctl_device_adapter_properties_t adapterProps = IGCL.new_adapterPropertiesP();
                    status = IGCL.IGCL_GetAdapterProperties(hAdapter, adapterProps);
                    
                    if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                    {
                        continue;
                    }

                    // Enumerate displays for this adapter
                    SWIGTYPE_p_unsigned_int pDisplayCount = IGCL.new_igcl_uint32P();
                    IGCL.igcl_uint32P_assign(pDisplayCount, 0);
                    
                    status = IGCL.IGCL_EnumerateDisplays(hAdapter, pDisplayCount, null);
                    uint displayCount = IGCL.igcl_uint32P_value(pDisplayCount);
                    
                    if (status != ctl_result_t.CTL_RESULT_SUCCESS || displayCount == 0)
                    {
                        continue;
                    }

                    SWIGTYPE_p_p__ctl_display_output_handle_t ppDisplays = IGCL.new_displayOutputHandleP();
                    status = IGCL.IGCL_EnumerateDisplays(hAdapter, pDisplayCount, ppDisplays);
                    
                    if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                    {
                        continue;
                    }

                    IntPtr displaysPtr = IGCL.displayOutputHandleP_value(ppDisplays);

                    for (uint displayIdx = 0; displayIdx < displayCount; displayIdx++)
                    {
                        IntPtr hDisplay = Marshal.ReadIntPtr(displaysPtr, (int)(displayIdx * IntPtr.Size));

                        ctl_display_properties_t displayProps = IGCL.new_displayPropertiesP();
                        status = IGCL.IGCL_GetDisplayProperties(hDisplay, displayProps);
                        
                        if (status != ctl_result_t.CTL_RESULT_SUCCESS)
                        {
                            continue;
                        }

                        // Create display identifier: IntelIGCL|AdapterName|DisplayIndex|AdapterIndex
                        List<string> displayInfo = new List<string>
                        {
                            "IntelIGCL",
                            adapterProps.name,
                            displayIdx.ToString(),
                            adapterIdx.ToString()
                        };
                        
                        string displayIdentifier = String.Join("|", displayInfo);
                        if (!displayIdentifiers.Contains(displayIdentifier))
                        {
                            displayIdentifiers.Add(displayIdentifier);
                            SharedLogger.logger.Debug($"IntelLibrary/GetCurrentDisplayIdentifiers: DisplayIdentifier detected: {displayIdentifier}");
                        }
                    }
                }
            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/GetCurrentDisplayIdentifiers: ERROR - Tried to get Displays but the Intel IGCL library isn't initialised!");
                throw new IntelLibraryException($"Tried to get Displays but the Intel IGCL library isn't initialised!");
            }

            // Sort the display identifiers
            displayIdentifiers.Sort();

            return displayIdentifiers;
        }

        public List<string> GetAllConnectedDisplayIdentifiers(out bool failure)
        {
            SharedLogger.logger.Trace($"IntelLibrary/GetAllConnectedDisplayIdentifiers: Getting all the display identifiers that can possibly be used");
            
            // For Intel, all connected displays are the same as current displays
            // since Intel doesn't have the concept of "dormant" displays like AMD Eyefinity
            return GetCurrentDisplayIdentifiers(out failure);
        }
    }

    [Serializable]
    public class IntelLibraryException : Exception
    {
        public IntelLibraryException() { }
        public IntelLibraryException(string message) : base(message) { }
        public IntelLibraryException(string message, Exception inner) : base(message, inner) { }
    }
}
