using DisplayMagicianShared;
using DisplayMagicianShared.NVIDIA;
using DisplayMagicianShared.Windows;
using EDIDParser;
using Microsoft.VisualBasic;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Devices.PointOfService;
using Windows.Graphics;
using static DisplayMagicianShared.Intel.IGCLImport;
using static DisplayMagicianShared.NVIDIA.DisplayTopologyStatus;
using static System.Net.Mime.MediaTypeNames;

namespace DisplayMagicianShared.Intel
{

    [StructLayout(LayoutKind.Sequential)]
    public struct INTEL_DISPLAY_WITH_SETTINGS : IEquatable<INTEL_DISPLAY_WITH_SETTINGS>
    {
        public ADLX_DISPLAY_CONNECTOR_TYPE ConnectorType;
        public ADLX_DISPLAY_TYPE DisplayType;
        public string EDID;
        public long ManufacturerID;
        public string Name;
        public int MaxHResolution;
        public int MaxVResolution;
        public long PixelClock;
        public double RefreshRate;
        public ADLX_DISPLAY_SCAN_TYPE ScanType;
        public long UniqueID;
        public bool IsSupportedColorDepth;
        public ADLX_COLOR_DEPTH ColorDepth;
        public bool IsSupportedCustomColorBrightness;
        public int CustomColorBrightness;

        public INTEL_DISPLAY_WITH_SETTINGS()
        {
            EDID = "";
            Name = "";
            ScanType = ADLX_DISPLAY_SCAN_TYPE.PROGRESSIVE;
            ConnectorType = ADLX_DISPLAY_CONNECTOR_TYPE.DISPLAY_CONTYPE_UNKNOWN;
            DisplayType = ADLX_DISPLAY_TYPE.DISPLAY_TYPE_UNKOWN;
            ColorDepth = ADLX_COLOR_DEPTH.BPC_UNKNOWN;
        }

        public override bool Equals(object obj) => obj is INTEL_DISPLAY_WITH_SETTINGS other && this.Equals(other);
        public bool Equals(INTEL_DISPLAY_WITH_SETTINGS other)
        {
            if (ConnectorType != other.ConnectorType)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The ConnectorType values don't equal each other");
                return false;
            }
            if (DisplayType != other.DisplayType)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The DisplayType values don't equal each other");
                return false;
            }
            if (EDID != other.EDID)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The EDID values don't equal each other");
                return false;
            }
            if (ManufacturerID != other.ManufacturerID)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The ManufacturerID values don't equal each other");
                return false;
            }
            if (Name != other.Name)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The Name values don't equal each other");
                return false;
            }
            if (MaxHResolution != other.MaxHResolution)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The MaxHResolution values don't equal each other");
                return false;
            }
            if (MaxVResolution != other.MaxVResolution)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The MaxVResolution values don't equal each other");
                return false;
            }
            if (PixelClock != other.PixelClock)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The PixelClock values don't equal each other");
                return false;
            }
            if (RefreshRate != other.RefreshRate)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The RefreshRate values don't equal each other");
                return false;
            }
            if (ScanType != other.ScanType)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The ScanType values don't equal each other");
                return false;
            }
            if (UniqueID != other.UniqueID)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The UniqueID values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (ConnectorType, DisplayType, EDID, ManufacturerID, Name, MaxHResolution, MaxVResolution, PixelClock, RefreshRate, ScanType, UniqueID).GetHashCode();
        }
        public static bool operator ==(INTEL_DISPLAY_WITH_SETTINGS lhs, INTEL_DISPLAY_WITH_SETTINGS rhs) => lhs.Equals(rhs);

        public static bool operator !=(INTEL_DISPLAY_WITH_SETTINGS lhs, INTEL_DISPLAY_WITH_SETTINGS rhs) => !(lhs == rhs);
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct INTEL_DISPLAY_CONFIG : IEquatable<INTEL_DISPLAY_CONFIG>
    {
        public bool IsInUse;
        public bool IsCloned;
        public bool IsEyefinity;
        //public List<Intel_DESKTOP> Desktops;
        //public Intel_EYEFINITY_DESKTOP EyefinityDesktop;
        //public Dictionary<long,INTEL_DISPLAY_WITH_SETTINGS> Displays;
        //public Intel_SLS_CONFIG Adl2SlsConfig;
        public List<string> DisplayIdentifiers;

        public INTEL_DISPLAY_CONFIG()
        {
            IsInUse = false;
            IsCloned = false;
            //IsEyefinity = false;
            //Desktops = new List<Intel_DESKTOP>();
            //EyefinityDesktop = new Intel_EYEFINITY_DESKTOP();
            //Displays = new Dictionary<long,INTEL_DISPLAY_WITH_SETTINGS>();
            //Adl2SlsConfig = new Intel_SLS_CONFIG();
            DisplayIdentifiers = new List<string>();
        }

        public override bool Equals(object obj) => obj is INTEL_DISPLAY_CONFIG other && this.Equals(other);
        public bool Equals(INTEL_DISPLAY_CONFIG other)
        {
            if (IsInUse != other.IsInUse)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The IsInUse values don't equal each other");
                return false;
            }
            if (IsCloned != other.IsCloned)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The IsCloned values don't equal each other");
                return false;
            }
            /*if (!Desktops.SequenceEqual(other.Desktops))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The Desktops values don't equal each other");
                return false;
            }
            if (IsEyefinity != other.IsEyefinity)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The IsEyefinity values don't equal each other");
                return false;
            }
            if (!EyefinityDesktop.Equals(other.EyefinityDesktop))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The EyefinityDesktop values don't equal each other");
                return false;
            }
            if (!Displays.SequenceEqual(other.Displays))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The Displays values don't equal each other");
                return false;
            }*/            
            if (!DisplayIdentifiers.SequenceEqual(other.DisplayIdentifiers))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The DisplayIdentifiers values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            //return (IsInUse, IsCloned, Desktops, IsEyefinity, EyefinityDesktop, Displays, DisplayIdentifiers).GetHashCode();
            return (IsInUse, IsCloned, DisplayIdentifiers).GetHashCode();
        }

        public static bool operator ==(INTEL_DISPLAY_CONFIG lhs, INTEL_DISPLAY_CONFIG rhs) => lhs.Equals(rhs);

        public static bool operator !=(INTEL_DISPLAY_CONFIG lhs, INTEL_DISPLAY_CONFIG rhs) => !(lhs == rhs);
    }

    class IntelLibrary : IDisposable
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
        private INTEL_DISPLAY_CONFIG? _activeDisplayConfig;
        public List<string> _allConnectedDisplayIdentifiers;
        public const string Intel_ADLX_BINDING_DLL = "ADLXCSharpBind.dll";
        public const string Intel_ADLX_DLL = "Inteladlx64.dll";

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

                SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Attempting to initiliase the Intel IGCL API");
                try
                {
                    bool result = IGCLImport.Initialize();
                    if ( result)
                    {
                        // IGCL API Initialised correctly
                        _initialised = false;
                        SharedLogger.logger.Trace("IntelLibrary/IntelLibrary: We successfully initialised the Intel IGCL API which means that the Intel Graphics driver software is installed.");
                    }
                    else
                    {
                        // IGCL API failed to initialise
                        _initialised = false;
                        SharedLogger.logger.Error("IntelLibrary/IntelLibrary: Failed to access the Intel IGCL API. You need to download and install the INtel Graphics Driver software from the Intel support website in order to fully support Intel hardware.");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    _initialised = false;
                    SharedLogger.logger.Error(ex, "IntelLibrary/IntelLibrary: Exception whie trying to load the Intel ADLX DLL. You may need to install the Intel driver.");
                }

                SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Automatically getting the Intel Display Configuration");
                _activeDisplayConfig = GetActiveConfig();
                SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Automatically getting the Intel Connected Display Identifiers");
                _allConnectedDisplayIdentifiers = GetAllConnectedDisplayIdentifiers(out bool failure);

            }
            catch (Exception ex)
            {
                SharedLogger.logger.Info(ex, $"IntelLibrary/IntelLibrary: A general exception trying to load the Intel ADLX DLL {Intel_ADLX_BINDING_DLL}.");
                _initialised = false; 
                return;
            }
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

            if (_initialised)
            {
                // Terminate the IGCL API to avoid memory leaks
                IGCLImport.Terminate();
            }

            _disposed = true;
        }

        public static void KeepVideoCardOn()
        {
            // TODO: Fix this for Intel
            //LoadLibrary("IntelExportsDLL.dll");
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
                // A list of all the matching PCI Vendor IDs are per https://www.pcilookup.com/?ven=Intel&dev=&action=submit
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
            INTEL_DISPLAY_CONFIG myDefaultConfig = new INTEL_DISPLAY_CONFIG();

            // Fill in the minimal amount we need to avoid null references
            // so that we won't break json.net when we save a default config

            // THIS IS ALL TAKEN CARE OF IN THE STRUCT CONSTRUCTORS NOW \o/ yay!
            myDefaultConfig.IsInUse = false;

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
            // Creat empty config struct so we know there are no nulls in there to break the json serializer
            INTEL_DISPLAY_CONFIG myDisplayConfig = CreateDefaultConfig();

            if (_initialised)
            {
                IGCLStatus status = IGCLStatus.NO_ERROR;
                // Get the desktop services
                // This is how we get and iterate through the various desktops. 
                // - A single desktop is associated with one display.
                // - A duplicate desktop is associated with two or more displays.
                // - An Intel Eyefinity desktop is associated with two or more displays.

                // Iterate through the desktops and displays to get the display information
                List<ctl_device_adapter_properties_t> adapters;
                IGCLImport.GetDevices(out adapters);

                if (adapters == null || adapters.Count == 0)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: No Intel devices found, returning empty config");
                    return myDisplayConfig;
                }

                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Found {adapters.Count} Intel devices, proceeding to get display configuration");

                foreach (var adapter in adapters)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Found Intel device {adapter.name}");
                    myDisplayConfig.DisplayIdentifiers.Add(adapter.name);
                }
                /*IADLXDesktopServices desktopService;
                IADLXDesktopList desktopList;

                bool isEyefinityEnabled = false;
                bool isCloned = false;
                List<Intel_DESKTOP> desktopsToStore = new List<Intel_DESKTOP>();
                List<INTEL_DISPLAY_WITH_SETTINGS> displaysToStore = new List<INTEL_DISPLAY_WITH_SETTINGS>();
                Intel_EYEFINITY_DESKTOP eyefinityDesktopToStore = new Intel_EYEFINITY_DESKTOP();

                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Attempting to get the ADLX desktop services");
                SWIGTYPE_p_p_adlx__IADLXDesktopServices d = ADLX.new_desktopSerP_Ptr();
                status = _adlxSystem.GetDesktopsServices(d);
                desktopService = ADLX.desktopSerP_Ptr_value(d);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Error getting the ADLX desktop services. systemServices.GetDesktopsServices() returned error code {status}");
                    return myDisplayConfig; ;
                }
                else
                {

                    // Get the list of Desktops we have (this is more for informational purposes)

                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got the desktop services");
                    // Get the display services
                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Attempting to get the ADLX desktop list");
                    // Get display list
                    SWIGTYPE_p_p_adlx__IADLXDesktopList ppDesktopList = ADLX.new_desktopListP_Ptr();
                    status = desktopService.GetDesktops(ppDesktopList);
                    desktopList = ADLX.desktopListP_Ptr_value(ppDesktopList);

                    if (status != ADLX_RESULT.ADLX_OK)
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                        return CreateDefaultConfig(); ;
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDesktopConfig: Successfully got the desktop list");
                        // Iterate through the desktop list
                        uint it = desktopList.Begin();
                        for (; it != desktopList.Size(); it++)
                        {
                            SWIGTYPE_p_p_adlx__IADLXDesktop ppDesktop = ADLX.new_desktopP_Ptr();
                            status = desktopList.At(it, ppDesktop);
                            IADLXDesktop desktop = ADLX.desktopP_Ptr_value(ppDesktop);

                            if (status == ADLX_RESULT.ADLX_OK)
                            {
                                Intel_DESKTOP newDesktop = new Intel_DESKTOP();
                                newDesktop.Displays = new List<Intel_DISPLAY>();

                                SWIGTYPE_p_unsigned_int pNumDisplays = ADLX.new_adlx_uintP();
                                desktop.GetNumberOfDisplays(pNumDisplays);
                                newDesktop.NumberOfDisplays = ADLX.adlx_uintP_value(pNumDisplays);
                                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDesktopConfig: The number of displays that are part of this desktop is {newDesktop.NumberOfDisplays}");

                                if (newDesktop.NumberOfDisplays > 0)
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDesktopConfig: The number of displays that are part of this desktop is > 0, so getting list of displays");
                                    // Get the list of displays that are part of this desktop
                                    SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                                    status = desktop.GetDisplays(ppDisplayList);
                                    IADLXDisplayList desktopDisplayList = ADLX.displayListP_Ptr_value(ppDisplayList);
                                    if (status != ADLX_RESULT.ADLX_OK)
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDesktopConfig: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                                        return CreateDefaultConfig(); ;
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDesktopConfig: Successfully got the display list");
                                        // Iterate through the display list
                                        uint itDisplay = desktopDisplayList.Begin();
                                        for (; itDisplay != desktopDisplayList.Size(); itDisplay++)
                                        {
                                            SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay = ADLX.new_displayP_Ptr();
                                            status = desktopDisplayList.At(itDisplay, ppDisplay);
                                            IADLXDisplay display = ADLX.displayP_Ptr_value(ppDisplay);
                                            if (status == ADLX_RESULT.ADLX_OK)
                                            {
                                                // Create a new Intel_DISPLAY to store things in
                                                Intel_DISPLAY newDisplay = new Intel_DISPLAY();

                                                // Get the display connection type
                                                SWIGTYPE_p_ADLX_DISPLAY_CONNECTOR_TYPE pConnect = ADLX.new_adlx_displayConnectTypeP();
                                                display.ConnectorType(pConnect);
                                                newDisplay.ConnectorType = ADLX.adlx_displayConnectTypeP_value(pConnect);

                                                // Get the display type
                                                SWIGTYPE_p_ADLX_DISPLAY_TYPE pDisType = ADLX.new_adlx_displayTypeP();
                                                display.DisplayType(pDisType);
                                                newDisplay.DisplayType = ADLX.adlx_displayTypeP_value(pDisType);

                                                // Get the EDID
                                                SWIGTYPE_p_p_char ppEDID = ADLX.new_charP_Ptr();
                                                display.EDID(ppEDID);
                                                String edid = ADLX.charP_Ptr_value(ppEDID);

                                                // Get the manufacturer ID
                                                SWIGTYPE_p_unsigned_int pMID = ADLX.new_adlx_uintP();
                                                display.ManufacturerID(pMID);
                                                newDisplay.ManufacturerID = ADLX.adlx_uintP_value(pMID);

                                                // Get the display name
                                                SWIGTYPE_p_p_char ppName = ADLX.new_charP_Ptr();
                                                display.Name(ppName);
                                                String name = ADLX.charP_Ptr_value(ppName);
                                                newDisplay.Name = name;

                                                // Get the native resolution
                                                SWIGTYPE_p_int pMaxHRes = ADLX.new_adlx_intP();
                                                SWIGTYPE_p_int pMaxVRes = ADLX.new_adlx_intP();
                                                display.NativeResolution(pMaxHRes, pMaxVRes);
                                                newDisplay.MaxHResolution = ADLX.adlx_intP_value(pMaxHRes);
                                                newDisplay.MaxVResolution = ADLX.adlx_intP_value(pMaxVRes);

                                                // Get the PixelClock
                                                SWIGTYPE_p_unsigned_int pPixelClock = ADLX.new_adlx_uintP();
                                                display.PixelClock(pPixelClock);
                                                newDisplay.PixelClock = ADLX.adlx_uintP_value(pPixelClock);
                                                // Get the refresh rate
                                                SWIGTYPE_p_double pRefreshRate = ADLX.new_doubleP();
                                                display.RefreshRate(pRefreshRate);
                                                newDisplay.RefreshRate = ADLX.doubleP_value(pRefreshRate);

                                                // Get the scan type
                                                SWIGTYPE_p_ADLX_DISPLAY_SCAN_TYPE pScanType = ADLX.new_adlx_displayScanTypeP();
                                                display.ScanType(pScanType);
                                                newDisplay.ScanType = ADLX.adlx_displayScanTypeP_value(pScanType);

                                                // Get the Unique ID
                                                SWIGTYPE_p_size_t pUID = ADLX.new_adlx_sizeP();
                                                display.UniqueId(pUID);
                                                newDisplay.UniqueID = ADLX.adlx_sizeP_value(pUID);

                                                SWIGTYPE_p_size_t pID = ADLX.new_adlx_sizeP();
                                                display.UniqueId(pID);
                                                uint id = ADLX.adlx_sizeP_value(pID);

                                                // Add the new display to the list of displays for this desktop
                                                newDesktop.Displays.Add(newDisplay);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDesktopConfig: The number of displays that are part of this desktop is 0, so not getting list of displays. Skipping.");
                                }

                                SWIGTYPE_p_ADLX_ORIENTATION pOrientation = ADLX.new_adlx_orientationP();
                                desktop.Orientation(pOrientation);
                                newDesktop.Orientation = ADLX.adlx_orientationP_value(pOrientation);

                                SWIGTYPE_p_int pWidth = ADLX.new_adlx_intP();
                                SWIGTYPE_p_int pHeight = ADLX.new_adlx_intP();
                                desktop.Size(pWidth, pHeight);
                                newDesktop.SizeWidth = ADLX.adlx_intP_value(pWidth);
                                newDesktop.SizeHeight = ADLX.adlx_intP_value(pHeight);

                                ADLX_Point pLocationTopLeft = ADLX.new_adlx_pointP();
                                desktop.TopLeft(pLocationTopLeft);
                                ADLX_Point locationTopLeft = ADLX.adlx_pointP_value(pLocationTopLeft);
                                newDesktop.TopLeftX = locationTopLeft.x;
                                newDesktop.TopLeftY = locationTopLeft.y;

                                SWIGTYPE_p_ADLX_DESKTOP_TYPE pDesktopType = ADLX.new_adlx_desktopTypeP();
                                desktop.Type(pDesktopType);
                                newDesktop.Type = ADLX.adlx_desktopTypeP_value(pDesktopType);

                                // The the desktop is an eyefinity desktop then set the eyefinity enabled flag
                                // and also process the EyefinityDesktop layout
                                if (newDesktop.Type == ADLX_DESKTOP_TYPE.DESKTOP_EYEFINITY)
                                {
                                    isEyefinityEnabled = true;
                                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Eyefinity desktop detected");
                                    // Get the eyefinity desktop

                                    // 1. Allocate a void** via SWIG
                                    SWIGTYPE_p_p_void ppVoid = ADLX.new_voidP_Ptr();

                                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Getting a pointer to the Eyefinity desktop object");
                                    // 2. Call QueryInterface with the IID for IADLXEyefinityDesktop to get the interface
                                    status = desktop.QueryInterface(
                                        IADLXEyefinityDesktop.IID(),
                                        ppVoid
                                    );

                                    if (status != ADLX_RESULT.ADLX_OK)
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDesktopConfig: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                                        return CreateDefaultConfig(); ;
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Converting pointer to the Eyefinity desktop object to an IntPtr");

                                        // Extract the raw IntPtr from the void** for the IADLXEyefinityDesktop
                                        IntPtr rawPtr = ADLX.voidP_Ptr_value(ppVoid);

                                        // Wrap it in the managed proxy
                                        //    (Constructor args may vary based on SWIG config)
                                        IADLXEyefinityDesktop eyefinityDesktop = new IADLXEyefinityDesktop(rawPtr, true);

                                        // Use the EyefinityDesktop object to get the Eyefinity layout
                                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Getting the rows and columns of the diaplay grid for the Eyefinity desktop");
                                        SWIGTYPE_p_unsigned_int pRow = ADLX.new_adlx_uintP();
                                        ADLX.adlx_uintP_assign(pRow, 0);
                                        SWIGTYPE_p_unsigned_int pCol = ADLX.new_adlx_uintP();
                                        ADLX.adlx_uintP_assign(pCol, 0);
                                        eyefinityDesktop.GridSize(pRow, pCol);
                                        myDisplayConfig.EyefinityDesktop.Rows = ADLX.adlx_uintP_value(pRow);
                                        myDisplayConfig.EyefinityDesktop.Columns = ADLX.adlx_uintP_value(pCol);

                                        *//*for (uint row=1; row<gridRows; row++)
                                        {
                                            for (uint col = 1; col < gridCols; col++)
                                            {
                                                // Get the eyefinity desktop orientation
                                                SWIGTYPE_p_ADLX_ORIENTATION pEyefinityDisplayOrientation = ADLX.new_orientationP();
                                                eyefinityDesktop.DisplayOrientation(row, col, pEyefinityDisplayOrientation);
                                                ADLX_ORIENTATION eyefinityOrientation = ADLX.orientationP_value(pEyefinityDisplayOrientation);

                                                // Get the display size
                                                SWIGTYPE_p_int pEyefinityDisplayWidth= ADLX.new_intP();
                                                SWIGTYPE_p_int pEyefinityDisplayHeight = ADLX.new_intP();
                                                eyefinityDesktop.DisplaySize(row,col, pEyefinityDisplayWidth, pEyefinityDisplayHeight);
                                                int eyefinityDisplayWidth = ADLX.intP_value(pEyefinityDisplayWidth);
                                                int eyefinityDisplayHeight = ADLX.intP_value(pEyefinityDisplayHeight);

                                                // Get the display location
                                                ADLX_Point pLocation = ADLX.new_pointP();
                                                eyefinityDesktop.DisplayTopLeft(row, col, pLocation);
                                                ADLX_Point location = ADLX.pointP_value(pLocation);

                                            }
                                        }*//*

                                        // Copy over the desktop level sizes so that we can match things easier in the future
                                        myDisplayConfig.EyefinityDesktop.Orientation = newDesktop.Orientation;
                                        myDisplayConfig.EyefinityDesktop.TopLeftX = newDesktop.TopLeftX;
                                        myDisplayConfig.EyefinityDesktop.TopLeftY = newDesktop.TopLeftY;
                                        myDisplayConfig.EyefinityDesktop.SizeWidth = newDesktop.SizeWidth;
                                        myDisplayConfig.EyefinityDesktop.SizeHeight = newDesktop.SizeHeight;

                                        // 7. Release when done
                                        eyefinityDesktop.Release();
                                        ADLX.delete_voidP_Ptr(ppVoid);
                                    }
                                }
                                else if (newDesktop.Type == ADLX_DESKTOP_TYPE.DESKTOP_DUPLCATE)
                                {
                                    isCloned = true;
                                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Cloned desktop detected");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Single desktop detected");
                                }

                                // Release desktop interface
                                desktop.Release();

                                // Save the Desktop to the main list
                                myDisplayConfig.Desktops.Add(newDesktop);
                            }
                        }
                    }
                    // Release desktop list interface
                    desktopList.Release();
                                      
                }

                // Release desktop services interface
                desktopService.Release();*/

                //-----------------------------------------------------------------------

                // Get the display services
                // This lets us interact witth the various displays
                /*IADLXDisplayServices displayService;
                IADLXDisplayList displayList;

                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Attempting to get the ADLX display services");
                SWIGTYPE_p_p_adlx__IADLXDisplayServices s = ADLX.new_displaySerP_Ptr();
                status = _adlxSystem.GetDisplaysServices(s);
                displayService = ADLX.displaySerP_Ptr_value(s);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Error getting the ADLX display services. systemServices.GetDisplaysServices() returned error code {status}");
                    return CreateDefaultConfig(); ;
                }
                else
                {
                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got the display services");
                    // Get the display services
                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Attempting to get the ADLX display list");
                    // Get display list
                    SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                    status = displayService.GetDisplays(ppDisplayList);
                    displayList = ADLX.displayListP_Ptr_value(ppDisplayList);
                    if (status != ADLX_RESULT.ADLX_OK)
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                        return CreateDefaultConfig();
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got the display list");
                        // Iterate through the display list
                        uint it = displayList.Begin();
                        for (; it != displayList.Size(); it++)
                        {
                            SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay = ADLX.new_displayP_Ptr();
                            status = displayList.At(it, ppDisplay);
                            IADLXDisplay display = ADLX.displayP_Ptr_value(ppDisplay);

                            if (status == ADLX_RESULT.ADLX_OK)
                            {
                                // Create a new INTEL_DISPLAY_WITH_SETTINGS to store things in
                                INTEL_DISPLAY_WITH_SETTINGS newDisplay = new INTEL_DISPLAY_WITH_SETTINGS();

                                // Get the display connection type
                                SWIGTYPE_p_ADLX_DISPLAY_CONNECTOR_TYPE pConnect = ADLX.new_adlx_displayConnectTypeP();
                                display.ConnectorType(pConnect);
                                newDisplay.ConnectorType = ADLX.adlx_displayConnectTypeP_value(pConnect);

                                // Get the display type
                                SWIGTYPE_p_ADLX_DISPLAY_TYPE pDisType = ADLX.new_adlx_displayTypeP();
                                display.DisplayType(pDisType);
                                newDisplay.DisplayType = ADLX.adlx_displayTypeP_value(pDisType);

                                // Get the EDID
                                SWIGTYPE_p_p_char ppEDID = ADLX.new_charP_Ptr();
                                display.EDID(ppEDID);
                                String edid = ADLX.charP_Ptr_value(ppEDID);

                                // Get the manufacturer ID
                                SWIGTYPE_p_unsigned_int pMID = ADLX.new_adlx_uintP();
                                display.ManufacturerID(pMID);
                                newDisplay.ManufacturerID = ADLX.adlx_uintP_value(pMID);

                                // Get the display name
                                SWIGTYPE_p_p_char ppName = ADLX.new_charP_Ptr();
                                display.Name(ppName);
                                String name = ADLX.charP_Ptr_value(ppName);
                                newDisplay.Name = name;

                                // Get the native resolution
                                SWIGTYPE_p_int pMaxHRes = ADLX.new_adlx_intP();
                                SWIGTYPE_p_int pMaxVRes = ADLX.new_adlx_intP();
                                display.NativeResolution(pMaxHRes, pMaxVRes);
                                newDisplay.MaxHResolution = ADLX.adlx_intP_value(pMaxHRes);
                                newDisplay.MaxVResolution = ADLX.adlx_intP_value(pMaxVRes);

                                // Get the PixelClock
                                SWIGTYPE_p_unsigned_int pPixelClock = ADLX.new_adlx_uintP();
                                display.PixelClock(pPixelClock);
                                newDisplay.PixelClock = ADLX.adlx_uintP_value(pPixelClock);
                                // Get the refresh rate
                                SWIGTYPE_p_double pRefreshRate = ADLX.new_doubleP();
                                display.RefreshRate(pRefreshRate);
                                newDisplay.RefreshRate = ADLX.doubleP_value(pRefreshRate);

                                // Get the scan type
                                SWIGTYPE_p_ADLX_DISPLAY_SCAN_TYPE pScanType = ADLX.new_adlx_displayScanTypeP();
                                display.ScanType(pScanType);
                                newDisplay.ScanType = ADLX.adlx_displayScanTypeP_value(pScanType);

                                // Get the Unique ID
                                SWIGTYPE_p_size_t pUID = ADLX.new_adlx_sizeP();
                                display.UniqueId(pUID);
                                newDisplay.UniqueID = ADLX.adlx_sizeP_value(pUID);

                                // Ok now start getting the various settings for this display

                                //------------------------------------
                                // GET THE COLOR DEPTH IF WE CAN
                                //------------------------------------
                                // Get the current color depth for this display
                                SWIGTYPE_p_p_adlx__IADLXDisplayColorDepth ppColorDepth = ADLX.new_displayColorDepthP_Ptr();
                                status = displayService.GetColorDepth(display, ppColorDepth);
                                if (status != ADLX_RESULT.ADLX_OK)
                                {
                                    SharedLogger.logger.Error($"IntelLibrary/GetIntelDisplayConfig: Error getting the display color depth object. systemServices.GetColorDepth() returned error code {status}");
                                    //return false;
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got the display color depth object");
                                    // Check if the color depth is the same as the one we stored
                                    IADLXDisplayColorDepth colorDepth = ADLX.displayColorDepthP_Ptr_value(ppColorDepth);
                                    // Check if the color depth is supported
                                    SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                                    status = colorDepth.IsSupported(pIsSupported);
                                    if (status == ADLX_RESULT.ADLX_OK)
                                    {
                                        newDisplay.IsSupportedColorDepth = ADLX.boolP_value(pIsSupported);
                                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Color Depth can be set for this display");
                                        
                                        // Get the current color depth for this display
                                        SWIGTYPE_p_ADLX_COLOR_DEPTH pColorDepth = ADLX.new_adlx_colorDepthP();
                                        status = colorDepth.GetValue(pColorDepth);

                                        if (status != ADLX_RESULT.ADLX_OK)
                                        {
                                            SharedLogger.logger.Error($"IntelLibrary/GetIntelDisplayConfig: Error getting the display color depth. systemServices.GetColorDepth() returned error code {status}");
                                            //return false;
                                        }
                                        else
                                        {
                                            newDisplay.ColorDepth = ADLX.adlx_colorDepthP_value(pColorDepth);
                                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got the display color depth for this display: {newDisplay.ColorDepth}");
                                        }
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Color Depth is NOT supported for this display so skipping setting it");
                                    }
                                }

                                //------------------------------------
                                // GET THE DISPLAY CUSTOM COLOR IF POSSIBLE
                                //------------------------------------
                                // Get the current custom color object for this display
                                SWIGTYPE_p_p_adlx__IADLXDisplayCustomColor ppCustomColor = ADLX.new_displayCustomColorP_Ptr();
                                status = displayService.GetCustomColor(display, ppCustomColor);
                                if (status != ADLX_RESULT.ADLX_OK)
                                {
                                    SharedLogger.logger.Error($"IntelLibrary/GetIntelDisplayConfig: Error getting the display custom color object. systemServices.GetCustomColor() returned error code {status}");
                                    //return false;
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got the display custom color object");
                                    IADLXDisplayCustomColor customColor = ADLX.displayCustomColorP_Ptr_value(ppCustomColor);
                                    // Check if the custom color brightness is supported
                                    SWIGTYPE_p_bool pIsBrightnessSupported = ADLX.new_boolP();
                                    status = customColor.IsBrightnessSupported(pIsBrightnessSupported);
                                    if (status == ADLX_RESULT.ADLX_OK)
                                    {
                                        newDisplay.IsSupportedCustomColorBrightness = ADLX.boolP_value(pIsBrightnessSupported);
                                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Custom Color Brightness can be set for this display!");
                                        // Get the current color brightness for this display
                                        SWIGTYPE_p_int pCurrentBrightness = ADLX.new_adlx_intP();
                                        status = customColor.GetBrightness(pCurrentBrightness);
                                        if (status != ADLX_RESULT.ADLX_OK)
                                        {
                                            SharedLogger.logger.Error($"IntelLibrary/GetIntelDisplayConfig: Error getting the display custom color brightness. systemServices.GetCustomColor() returned error code {status}");
                                            //return false;
                                        }
                                        else
                                        {
                                            newDisplay.CustomColorBrightness = ADLX.adlx_intP_value(pCurrentBrightness);
                                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got the display custom color brightness for this display: {newDisplay.CustomColorBrightness}");
                                        }
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Custom Color Brightness is NOT supported for this display.");
                                    }
                                }
                                SharedLogger.logger.Warn($"IntelLibrary/GetIntelDisplayConfig: Found the display settings for this UniqueID but it has a different name");


                                // Save the Display to the main dictionary of displays with the uniqueid as the key
                                myDisplayConfig.Displays.Add(newDisplay.UniqueID, newDisplay);
                            }
                        }
                    }
                    // Release display list interface
                    displayList.Release();
                }

                // Release display services interface
                displayService.Release();
*/

                // Now we have everything we need, so we can build the display config!
                myDisplayConfig.IsInUse = true;

                // Get the display identifiers                
                myDisplayConfig.DisplayIdentifiers = GetCurrentDisplayIdentifiers(out bool failure);

            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/GetIntelDisplayConfig: ERROR - Tried to run GetIntelDisplayConfig but the Intel ADL library isn't initialised!");
                return CreateDefaultConfig();
            }
            
            // Return the configuration
            return myDisplayConfig;
        }


        public string PrintActiveConfig()
        {
            string stringToReturn = "";

            // Get the current config
            INTEL_DISPLAY_CONFIG displayConfig = ActiveDisplayConfig;

            stringToReturn += $"****** INTEL VIDEO CARDS *******\n";


            /*if (_initialised)
            {
                // Get the number of Intel adapters that the OS knows about
                int numAdapters = 0;
                ADL_STATUS ADLRet = IGCLImport.ADL2_Adapter_NumberOfAdapters_Get(_adlContextHandle, out numAdapters);
                if (ADLRet == ADL_STATUS.ADL_OK)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: ADL2_Adapter_NumberOfAdapters_Get returned the number of Intel Adapters the OS knows about ({numAdapters}).");
                }
                else
                {
                    SharedLogger.logger.Error($"IntelLibrary/PrintActiveConfig: ERROR - ADL2_Adapter_NumberOfAdapters_Get returned ADL_STATUS {ADLRet} when trying to get number of Intel adapters in the computer.");
                }

                // Figure out primary adapter
                int primaryAdapterIndex = 0;
                ADLRet = IGCLImport.ADL2_Adapter_Primary_Get(_adlContextHandle, out primaryAdapterIndex);
                if (ADLRet == ADL_STATUS.ADL_OK)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: The primary adapter has index {primaryAdapterIndex}.");
                }
                else
                {
                    SharedLogger.logger.Error($"IntelLibrary/PrintActiveConfig: ERROR - ADL2_Adapter_Primary_Get returned ADL_STATUS {ADLRet} when trying to get the primary adapter info from all the Intel adapters in the computer.");
                }

                // Now go through each adapter and get the information we need from it
                for (int adapterIndex = 0; adapterIndex < numAdapters; adapterIndex++)
                {
                    // Skip this adapter if it isn't active
                    int adapterActiveStatus = IGCLImport.ADL_FALSE;
                    ADLRet = IGCLImport.ADL2_Adapter_Active_Get(_adlContextHandle, adapterIndex, out adapterActiveStatus);
                    if (ADLRet == ADL_STATUS.ADL_OK)
                    {
                        if (adapterActiveStatus == IGCLImport.ADL_TRUE)
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: ADL2_Adapter_Active_Get returned ADL_TRUE - Intel Adapter #{adapterIndex} is active! We can continue.");
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: ADL2_Adapter_Active_Get returned ADL_FALSE - Intel Adapter #{adapterIndex} is NOT active, so skipping.");
                            continue;
                        }
                    }
                    else
                    {
                        SharedLogger.logger.Warn($"IntelLibrary/PrintActiveConfig: WARNING - ADL2_Adapter_Active_Get returned ADL_STATUS {ADLRet} when trying to see if Intel Adapter #{adapterIndex} is active. Trying to skip this adapter so something at least works.");
                        continue;
                    }

                    // Get the Adapter info for this adapter and put it in the AdapterBuffer
                    SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: Running ADL2_Adapter_AdapterInfoX4_Get to get the information about Intel Adapter #{adapterIndex}.");
                    int numAdaptersInfo = 0;
                    IntPtr adapterInfoBuffer = IntPtr.Zero;
                    ADLRet = IGCLImport.ADL2_Adapter_AdapterInfoX4_Get(_adlContextHandle, adapterIndex, out numAdaptersInfo, out adapterInfoBuffer);
                    if (ADLRet == ADL_STATUS.ADL_OK)
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: ADL2_Adapter_AdapterInfoX4_Get returned information about Intel Adapter #{adapterIndex}.");
                    }
                    else
                    {
                        SharedLogger.logger.Error($"IntelLibrary/PrintActiveConfig: ERROR - ADL2_Adapter_AdapterInfoX4_Get returned ADL_STATUS {ADLRet} when trying to get the adapter info from Intel Adapter #{adapterIndex}. Trying to skip this adapter so something at least works.");
                        continue;
                    }

                    ADL_ADAPTER_INFOX2[] adapterArray = new ADL_ADAPTER_INFOX2[numAdaptersInfo];
                    if (numAdaptersInfo > 0)
                    {
                        IntPtr currentDisplayTargetBuffer = adapterInfoBuffer;
                        for (int i = 0; i < numAdaptersInfo; i++)
                        {
                            // build a structure in the array slot
                            adapterArray[i] = new ADL_ADAPTER_INFOX2();
                            // fill the array slot structure with the data from the buffer
                            adapterArray[i] = (ADL_ADAPTER_INFOX2)Marshal.PtrToStructure(currentDisplayTargetBuffer, typeof(ADL_ADAPTER_INFOX2));
                            // destroy the bit of memory we no longer need
                            //Marshal.DestroyStructure(currentDisplayTargetBuffer, typeof(ADL_ADAPTER_INFOX2));
                            // advance the buffer forwards to the next object
                            currentDisplayTargetBuffer = (IntPtr)((long)currentDisplayTargetBuffer + Marshal.SizeOf(adapterArray[i]));
                        }
                        // Free the memory used by the buffer                        
                        Marshal.FreeCoTaskMem(adapterInfoBuffer);
                    }

                    SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: Converted ADL2_Adapter_AdapterInfoX4_Get memory buffer into a {adapterArray.Length} long array about Intel Adapter #{adapterIndex}.");

                    //Intel_ADAPTER_CONFIG savedAdapterConfig = new Intel_ADAPTER_CONFIG();
                    ADL_ADAPTER_INFOX2 oneAdapter = adapterArray[0];
                    if (oneAdapter.Exist != 1)
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: Intel Adapter #{oneAdapter.AdapterIndex.ToString()} doesn't exist at present so skipping detection for this adapter.");
                        continue;
                    }

                    // Print out what we need
                    stringToReturn += $"Adapter #{adapterIndex}\n";
                    stringToReturn += $"Adapter Exists: {oneAdapter.Exist}\n";
                    stringToReturn += $"Adapter Present: {oneAdapter.Present}\n";
                    stringToReturn += $"Adapter Name: {oneAdapter.AdapterName}\n";
                    stringToReturn += $"Adapter Display Name: {oneAdapter.DisplayName}\n";
                    stringToReturn += $"Adapter Driver Path: {oneAdapter.DriverPath}\n";
                    stringToReturn += $"Adapter Driver Path Extension: {oneAdapter.DriverPathExt}\n";
                    stringToReturn += $"Adapter UDID: {oneAdapter.UDID}\n";
                    stringToReturn += $"Adapter Vendor ID: {oneAdapter.VendorID}\n";
                    stringToReturn += $"Adapter PNP String: {oneAdapter.PNPString}\n";
                    stringToReturn += $"Adapter PCI Device Number: {oneAdapter.DeviceNumber}\n";
                    stringToReturn += $"Adapter PCI Bus Number: {oneAdapter.BusNumber}\n";
                    stringToReturn += $"Adapter Windows OS Display Index: {oneAdapter.OSDisplayIndex}\n";
                    stringToReturn += $"Adapter Display Connected: {oneAdapter.DisplayConnectedSet}\n";
                    stringToReturn += $"Adapter Display Mapped in Windows: {oneAdapter.DisplayMappedSet}\n";
                    stringToReturn += $"Adapter Is Forcibly Enabled: {oneAdapter.ForcibleSet}\n";
                    stringToReturn += $"Adapter GetLock is Set: {oneAdapter.GenLockSet}\n";
                    stringToReturn += $"Adapter LDA Display is Set: {oneAdapter.LDADisplaySet}\n";
                    stringToReturn += $"Adapter Display Configuration is stretched horizontally across two displays: {oneAdapter.Manner2HStretchSet}\n";
                    stringToReturn += $"Adapter Display Configuration is stretched vertically across two displays: {oneAdapter.Manner2VStretchSet}\n";
                    stringToReturn += $"Adapter Display Configuration is a clone of another display: {oneAdapter.MannerCloneSet}\n";
                    stringToReturn += $"Adapter Display Configuration is an extension of another display: {oneAdapter.MannerExtendedSet}\n";
                    stringToReturn += $"Adapter Display Configuration is an N Strech across 1 GPU: {oneAdapter.MannerNStretch1GPUSet}\n";
                    stringToReturn += $"Adapter Display Configuration is an N Strech across more than one GPU: {oneAdapter.MannerNStretchNGPUSet}\n";
                    stringToReturn += $"Adapter Display Configuration is a single display: {oneAdapter.MannerSingleSet}\n";
                    stringToReturn += $"Adapter timing override: {oneAdapter.ModeTimingOverrideSet}\n";
                    stringToReturn += $"Adapter has MultiVPU set: {oneAdapter.MultiVPUSet}\n";
                    stringToReturn += $"Adapter has non-local set (it is a remote display): {oneAdapter.NonLocalSet}\n";
                    stringToReturn += $"Adapter is a Show Type Projector: {oneAdapter.ShowTypeProjectorSet}\n\n";

                }

                // Now we still try to get the information from each display we need to print 
                int numDisplayTargets = 0;
                int numDisplayMaps = 0;
                IntPtr displayTargetBuffer = IntPtr.Zero;
                IntPtr displayMapBuffer = IntPtr.Zero;
                ADLRet = IGCLImport.ADL2_Display_DisplayMapConfig_Get(_adlContextHandle, -1, out numDisplayMaps, out displayMapBuffer, out numDisplayTargets, out displayTargetBuffer, IGCLImport.ADL_DISPLAY_DISPLAYMAP_OPTION_GPUINFO);
                if (ADLRet == ADL_STATUS.ADL_OK)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: ADL2_Display_DisplayMapConfig_Get returned information about all displaytargets connected to all Intel adapters.");

                    // Free the memory used by the buffer to avoid heap corruption
                    Marshal.FreeCoTaskMem(displayMapBuffer);

                    ADL_DISPLAY_TARGET[] displayTargetArray = { };
                    if (numDisplayTargets > 0)
                    {
                        IntPtr currentDisplayTargetBuffer = displayTargetBuffer;
                        //displayTargetArray = new ADL_DISPLAY_TARGET[numDisplayTargets];
                        displayTargetArray = new ADL_DISPLAY_TARGET[numDisplayTargets];
                        for (int i = 0; i < numDisplayTargets; i++)
                        {
                            // build a structure in the array slot
                            displayTargetArray[i] = new ADL_DISPLAY_TARGET();
                            //displayTargetArray[i] = new ADL_DISPLAY_TARGET();
                            // fill the array slot structure with the data from the buffer
                            displayTargetArray[i] = (ADL_DISPLAY_TARGET)Marshal.PtrToStructure(currentDisplayTargetBuffer, typeof(ADL_DISPLAY_TARGET));
                            //displayTargetArray[i] = (ADL_DISPLAY_TARGET)Marshal.PtrToStructure(currentDisplayTargetBuffer, typeof(ADL_DISPLAY_TARGET));
                            // destroy the bit of memory we no longer need
                            Marshal.DestroyStructure(currentDisplayTargetBuffer, typeof(ADL_DISPLAY_TARGET));
                            // advance the buffer forwards to the next object
                            currentDisplayTargetBuffer = (IntPtr)((long)currentDisplayTargetBuffer + Marshal.SizeOf(displayTargetArray[i]));
                            //currentDisplayTargetBuffer = (IntPtr)((long)currentDisplayTargetBuffer + Marshal.SizeOf(displayTargetArray[i]));

                        }
                        // Free the memory used by the buffer                        
                        Marshal.FreeCoTaskMem(displayTargetBuffer);
                    }

                    foreach (var displayTarget in displayTargetArray)
                    {
                        int forceDetect = 0;
                        int numDisplays;
                        IntPtr displayInfoBuffer;
                        ADLRet = IGCLImport.ADL2_Display_DisplayInfo_Get(_adlContextHandle, displayTarget.DisplayID.DisplayLogicalAdapterIndex, out numDisplays, out displayInfoBuffer, forceDetect);
                        if (ADLRet == ADL_STATUS.ADL_OK)
                        {
                            if (displayTarget.DisplayID.DisplayLogicalAdapterIndex == -1)
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: ADL2_Display_DisplayInfo_Get returned information about all displaytargets connected to all Intel adapters.");
                                continue;
                            }
                            SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: ADL2_Display_DisplayInfo_Get returned information about all displaytargets connected to all Intel adapters.");
                        }
                        else if (ADLRet == ADL_STATUS.ADL_ERR_NULL_POINTER || ADLRet == ADL_STATUS.ADL_ERR_NOT_SUPPORTED)
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: ADL2_Display_DisplayInfo_Get returned ADL_ERR_NULL_POINTER so skipping getting display info from all Intel adapters.");
                            continue;
                        }
                        else
                        {
                            SharedLogger.logger.Error($"IntelLibrary/PrintActiveConfig: ERROR - ADL2_Display_DisplayInfo_Get returned ADL_STATUS {ADLRet} when trying to get the display target info from all Intel adapters in the computer.");
                        }

                        ADL_DISPLAY_INFO[] displayInfoArray = { };
                        if (numDisplays > 0)
                        {
                            IntPtr currentDisplayInfoBuffer = displayInfoBuffer;
                            displayInfoArray = new ADL_DISPLAY_INFO[numDisplays];
                            for (int i = 0; i < numDisplays; i++)
                            {
                                // build a structure in the array slot
                                displayInfoArray[i] = new ADL_DISPLAY_INFO();
                                // fill the array slot structure with the data from the buffer
                                displayInfoArray[i] = (ADL_DISPLAY_INFO)Marshal.PtrToStructure(currentDisplayInfoBuffer, typeof(ADL_DISPLAY_INFO));
                                // destroy the bit of memory we no longer need
                                Marshal.DestroyStructure(currentDisplayInfoBuffer, typeof(ADL_DISPLAY_INFO));
                                // advance the buffer forwards to the next object
                                currentDisplayInfoBuffer = (IntPtr)((long)currentDisplayInfoBuffer + Marshal.SizeOf(displayInfoArray[i]));
                                //currentDisplayTargetBuffer = (IntPtr)((long)currentDisplayTargetBuffer + Marshal.SizeOf(displayTargetArray[i]));

                            }
                            // Free the memory used by the buffer                        
                            Marshal.FreeCoTaskMem(displayInfoBuffer);
                        }

                        // Now we need to get all the displays connected to this adapter so that we can get their HDR state
                        foreach (var displayInfoItem in displayInfoArray)
                        {

                            // Ignore the display if it isn't connected (note: we still need to see if it's actively mapped to windows!)
                            if (!displayInfoItem.DisplayConnectedSet)
                            {
                                continue;
                            }

                            // If the display is not mapped in windows then we only want to skip this display if all alldisplays is false
                            if (!displayInfoItem.DisplayMappedSet)
                            {
                                continue;
                            }

                            stringToReturn += $"\n****** Intel DISPLAY INFO *******\n";
                            stringToReturn += $"Display #{displayInfoItem.DisplayID.DisplayLogicalIndex}\n";
                            stringToReturn += $"Display connected via Adapter #{displayInfoItem.DisplayID.DisplayLogicalAdapterIndex}\n";
                            stringToReturn += $"Display Name: {displayInfoItem.DisplayName}\n";
                            stringToReturn += $"Display Manufacturer Name: {displayInfoItem.DisplayManufacturerName}\n";
                            stringToReturn += $"Display Type: {displayInfoItem.DisplayType.ToString("G")}\n";
                            stringToReturn += $"Display connector: {displayInfoItem.DisplayConnector.ToString("G")}\n";
                            stringToReturn += $"Display controller index: {displayInfoItem.DisplayControllerIndex}\n";
                            stringToReturn += $"Display Connected: {displayInfoItem.DisplayConnectedSet}\n";
                            stringToReturn += $"Display Mapped in Windows: {displayInfoItem.DisplayMappedSet}\n";
                            stringToReturn += $"Display Is Forcibly Enabled: {displayInfoItem.ForcibleSet}\n";
                            stringToReturn += $"Display GetLock is Set: {displayInfoItem.GenLockSet}\n";
                            stringToReturn += $"LDA Display is Set: {displayInfoItem.LDADisplaySet}\n";
                            stringToReturn += $"Display Configuration is stretched horizontally across two displays: {displayInfoItem.Manner2HStretchSet}\n";
                            stringToReturn += $"Display Configuration is stretched vertically across two displays: {displayInfoItem.Manner2VStretchSet}\n";
                            stringToReturn += $"Display Configuration is a clone of another display: {displayInfoItem.MannerCloneSet}\n";
                            stringToReturn += $"Display Configuration is an extension of another display: {displayInfoItem.MannerExtendedSet}\n";
                            stringToReturn += $"Display Configuration is an N Strech across 1 GPU: {displayInfoItem.MannerNStretch1GPUSet}\n";
                            stringToReturn += $"Display Configuration is an N Strech across more than one GPU: {displayInfoItem.MannerNStretchNGPUSet}\n";
                            stringToReturn += $"Display Configuration is a single display: {displayInfoItem.MannerSingleSet}\n";
                            stringToReturn += $"Display timing override: {displayInfoItem.ModeTimingOverrideSet}\n";
                            stringToReturn += $"Display has MultiVPU set: {displayInfoItem.MultiVPUSet}\n";
                            stringToReturn += $"Display has non-local set (it is a remote display): {displayInfoItem.NonLocalSet}\n";
                            stringToReturn += $"Display is a Show Type Projector: {displayInfoItem.ShowTypeProjectorSet}\n\n";

                            // Get some more Display Info (if we can!)
                            ADL_DDC_INFO2 ddcInfo;
                            ADLRet = IGCLImport.ADL2_Display_DDCInfo2_Get(_adlContextHandle, displayInfoItem.DisplayID.DisplayLogicalAdapterIndex, displayInfoItem.DisplayID.DisplayLogicalIndex, out ddcInfo);
                            if (ADLRet == ADL_STATUS.ADL_OK)
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: ADL2_Display_DDCInfo2_Get returned information about DDC Information for display {displayInfoItem.DisplayID.DisplayLogicalIndex} connected to Intel adapter {displayInfoItem.DisplayID.DisplayLogicalAdapterIndex}.");
                                if (ddcInfo.SupportsDDC == 1)
                                {
                                    // The display supports DDC and returned some data!
                                    SharedLogger.logger.Trace($"IntelLibrary/PrintActiveConfig: ADL2_Display_DDCInfo2_Get returned information about DDC Information for display {displayInfoItem.DisplayID.DisplayLogicalIndex} connected to Intel adapter {displayInfoItem.DisplayID.DisplayLogicalAdapterIndex}.");
                                    stringToReturn += $"DDC Information: \n";
                                    stringToReturn += $"- Display Name: {ddcInfo.DisplayName}\n";
                                    stringToReturn += $"- Display Manufacturer ID: {ddcInfo.ManufacturerID}\n";
                                    stringToReturn += $"- Display Product ID: {ddcInfo.ProductID}\n";
                                    stringToReturn += $"- Display Serial ID: {ddcInfo.SerialID}\n";
                                    stringToReturn += $"- Display FreeSync Flags: {ddcInfo.FreesyncFlags}\n";
                                    stringToReturn += $"- Display FreeSync HDR Supported: {ddcInfo.FreeSyncHDRSupported}\n";
                                    stringToReturn += $"- Display FreeSync HDR Backlight Supported: {ddcInfo.FreeSyncHDRBacklightSupported}\n";
                                    stringToReturn += $"- Display FreeSync HDR Local Dimming Supported: {ddcInfo.FreeSyncHDRLocalDimmingSupported}\n";
                                    stringToReturn += $"- Display is Digital Device: {ddcInfo.IsDigitalDevice}\n";
                                    stringToReturn += $"- Display is HDMI Audio Device: {ddcInfo.IsHDMIAudioDevice}\n";
                                    stringToReturn += $"- Display is Projector Device: {ddcInfo.IsProjectorDevice}\n";
                                    stringToReturn += $"- Display Supported Colourspace: {ddcInfo.SupportedColorSpace}\n";
                                    stringToReturn += $"- Display Supported HDR: {ddcInfo.SupportedHDR}\n";
                                    stringToReturn += $"- Display Supported Transfer Function: {ddcInfo.SupportedTransferFunction}\n";
                                    stringToReturn += $"- Display Supports AI: {ddcInfo.SupportsAI}\n";
                                    stringToReturn += $"- Display Supports DDC: {ddcInfo.SupportsDDC}\n";
                                    stringToReturn += $"- Display Supports DolbyVision: {ddcInfo.DolbyVisionSupported}\n";
                                    stringToReturn += $"- Display Supports CEA861_3: {ddcInfo.CEA861_3Supported}\n";
                                    stringToReturn += $"- Display Supports sxvYCC601: {ddcInfo.SupportsxvYCC601}\n";
                                    stringToReturn += $"- Display Supports sxvYCC709: {ddcInfo.SupportsxvYCC709}\n";
                                    stringToReturn += $"- Display Average Luminance Data: {ddcInfo.AvgLuminanceData}\n";
                                    stringToReturn += $"- Display Diffuse Screen Reflectance: {ddcInfo.DiffuseScreenReflectance}\n";
                                    stringToReturn += $"- Display Specular Screen Reflectance: {ddcInfo.SpecularScreenReflectance}\n";
                                    stringToReturn += $"- Display Max Backlight Min Luminance: {ddcInfo.MaxBacklightMinLuminanceData}\n";
                                    stringToReturn += $"- Display Max Backlight Max Luminance: {ddcInfo.MaxBacklightMaxLuminanceData}\n";
                                    stringToReturn += $"- Display Min Luminance: {ddcInfo.MinLuminanceData}\n";
                                    stringToReturn += $"- Display Max Luminance: {ddcInfo.MaxLuminanceData}\n";
                                    stringToReturn += $"- Display Min Backlight Min Luminance: {ddcInfo.MinBacklightMinLuminanceData}\n";
                                    stringToReturn += $"- Display Min Backlight Max Luminance: {ddcInfo.MinBacklightMaxLuminanceData}\n";
                                    stringToReturn += $"- Display Min Luminance No Dimming: {ddcInfo.MinLuminanceNoDimmingData}\n";
                                    stringToReturn += $"- Display Native Chromacity Red X: {ddcInfo.NativeDisplayChromaticityRedX}\n";
                                    stringToReturn += $"- Display Native Chromacity Red Y: {ddcInfo.NativeDisplayChromaticityRedY}\n";
                                    stringToReturn += $"- Display Native Chromacity Green X: {ddcInfo.NativeDisplayChromaticityGreenX}\n";
                                    stringToReturn += $"- Display Native Chromacity Green Y: {ddcInfo.NativeDisplayChromaticityGreenY}\n";
                                    stringToReturn += $"- Display Native Chromacity Blue X: {ddcInfo.NativeDisplayChromaticityBlueX}\n";
                                    stringToReturn += $"- Display Native Chromacity Blue Y: {ddcInfo.NativeDisplayChromaticityBlueY}\n";
                                    stringToReturn += $"- Display Native Chromacity White X: {ddcInfo.NativeDisplayChromaticityWhiteX}\n";
                                    stringToReturn += $"- Display Native Chromacity White Y: {ddcInfo.NativeDisplayChromaticityWhiteY}\n";
                                    stringToReturn += $"- Display Packed Pixel Supported: {ddcInfo.PackedPixelSupported}\n";
                                    stringToReturn += $"- Display Panel Pixel Format: {ddcInfo.PanelPixelFormat}\n";
                                    stringToReturn += $"- Display Pixel Format Limited Range: {ddcInfo.PixelFormatLimitedRange}\n";
                                    stringToReturn += $"- Display PTMCx: {ddcInfo.PTMCx}\n";
                                    stringToReturn += $"- Display PTMCy: {ddcInfo.PTMCy}\n";
                                    stringToReturn += $"- Display PTM Refresh Rate: {ddcInfo.PTMRefreshRate}\n";

                                    stringToReturn += $"- Display Serial ID: {ddcInfo.SerialID}\n";
                                }

                            }

                        }
                    }

                }

                stringToReturn += $"\n****** Intel EYEFINITY (SLS) *******\n";
                if (displayConfig.SlsConfig.IsSlsEnabled)
                {
                    stringToReturn += $"Intel Eyefinity is Enabled\n";
                    if (displayConfig.SlsConfig.SLSMapConfigs.Count > 1)
                    {
                        stringToReturn += $"There are {displayConfig.SlsConfig.SLSMapConfigs.Count} Intel Eyefinity (SLS) configurations in use.\n";
                    }
                    if (displayConfig.SlsConfig.SLSMapConfigs.Count == 1)
                    {
                        stringToReturn += $"There is 1 Intel Eyefinity (SLS) configurations in use.\n";
                    }
                    else
                    {
                        stringToReturn += $"There are no Intel Eyefinity (SLS) configurations in use.\n";
                    }

                    int count = 0;
                    foreach (var slsMap in displayConfig.SlsConfig.SLSMapConfigs)
                    {
                        stringToReturn += $"NOTE: This Eyefinity (SLS) screen will be treated as a single display by Windows.\n";
                        stringToReturn += $"The Intel Eyefinity (SLS) Grid Topology #{count} is {slsMap.SLSMap.Grid.SLSGridColumn} Columns x {slsMap.SLSMap.Grid.SLSGridRow} Rows\n";
                        stringToReturn += $"The Intel Eyefinity (SLS) Grid Topology #{count} involves {slsMap.SLSMap.NumSLSTarget} Displays\n";
                    }

                }
                else
                {
                    stringToReturn += $"Intel Eyefinity (SLS) is Disabled\n";
                }

            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/PrintActiveConfig: ERROR - Tried to run GetSomeDisplayIdentifiers but the Intel ADL library isn't initialised!");
                throw new IntelLibraryException($"Tried to run PrintActiveConfig but the Intel ADL library isn't initialised!");
            }*/



            stringToReturn += $"\n\n";
            // Now we also get the Windows CCD Library info, and add it to the above
            stringToReturn += WinLibrary.GetLibrary().PrintActiveConfig();

            return stringToReturn;
        }

        public bool SetActiveConfig(INTEL_DISPLAY_CONFIG displayConfig, int delayInMs)
        {

            if (_initialised)
            {
                IGCLStatus status = IGCLStatus.NO_ERROR;
                
                /*
                IADLXDesktopServices desktopService;
                IADLXDesktopList desktopList;

                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Attempting to get the ADLX desktop services");
                SWIGTYPE_p_p_adlx__IADLXDesktopServices d = ADLX.new_desktopSerP_Ptr();
                status = _adlxSystem.GetDesktopsServices(d);
                desktopService = ADLX.desktopSerP_Ptr_value(d);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error getting the ADLX desktop services. systemServices.GetDesktopsServices() returned error code {status}");
                    return false;
                }
                else
                {
                    // Get the list of Desktops we have (this is more for informational purposes)
                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Successfully got the desktop services");

                    // If the display config needs an Eyefinity Desktop then lets create one.
                    if (displayConfig.IsEyefinity)
                    {
                        // Check if we are using the new ADLX or older ADL API to create the Eyefinity Desktop
                        if (useADLEyefinity)
                        {                          
                            // If set then we are using the older ADL API to create the Eyefinity Desktop
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Using the older ADL API to create the Eyefinity Desktop.");
                            // Set the initial state of the ADL_STATUS
                            ADL_STATUS ADLRet = 0;
                            
                            foreach (Intel_SLSMAP_CONFIG slsMapConfig in displayConfig.Adl2SlsConfig.SLSMapConfigs)
                            {
                                // Attempt to turn on this SLS Map Config if it exists in the Intel Radeon driver config database
                                ADLRet = IGCLImport.ADL2_Display_SLSMapConfig_SetState(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap.SLSMapIndex, IGCLImport.ADL_TRUE);
                                if (ADLRet == ADL_STATUS.ADL_OK)
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_SetState successfully set the SLSMAP with index {slsMapConfig.SLSMap.SLSMapIndex} to TRUE for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                }
                                else
                                {
                                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: ERROR - ADL2_Display_SLSMapConfig_SetState returned ADL_STATUS {ADLRet} when trying to set the SLSMAP with index {slsMapConfig.SLSMap.SLSMapIndex} to TRUE for adapter {slsMapConfig.SLSMap.AdapterIndex}.");

                                    // If we get an error with just tturning it on, then we need to actually try to created a new Eyefinity map and then enable it
                                    // If we reach this stage, then the user has discarded the Intel Eyefinity mode in Intel due to a bad UI design, and we need to work around that slight issue.
                                    // (BTW that's FAR to easy to do in the Intel Radeon GUI)
                                    // NOTE: There is a slight issue with way of doing things. Although we create a much more robust way of working, we also will never ever actually use the Eyefinity config as saved.
                                    //       Instead, we will always drop through to creating an Eyefinity config each time, the only saving grace being that the Intel Driver is smart enough to notice this and it will reuse the same SLSMapIndex number.
                                    //       This at least means that we won't keep filling the Intel Driver up with additional EYefinity configs! It will instaed only add one more additional Intel Config if it works this way.

                                    int supportedSLSLayoutImageMode;
                                    int reasonForNotSupportSLS;
                                    ADLRet = IGCLImport.ADL2_Display_SLSMapConfig_Valid(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap, slsMapConfig.SLSTargets.Count, slsMapConfig.SLSTargets.ToArray(), out supportedSLSLayoutImageMode, out reasonForNotSupportSLS, IGCLImport.ADL_DISPLAY_SLSMAPCONFIG_CREATE_OPTION_RELATIVETO_CURRENTANGLE);
                                    if (ADLRet == ADL_STATUS.ADL_OK)
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_Valid successfully validated a new SLSMAP config for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: ERROR - ADL2_Display_SLSMapConfig_Valid returned ADL_STATUS {ADLRet} when trying to create a new SLSMAP for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                        return false;
                                    }

                                    // Create and apply the new SLSMap
                                    int newSlsMapIndex;
                                    ADLRet = IGCLImport.ADL2_Display_SLSMapConfig_Create(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap, slsMapConfig.SLSTargets.Count, slsMapConfig.SLSTargets.ToArray(), slsMapConfig.BezelModePercent, out newSlsMapIndex, IGCLImport.ADL_DISPLAY_SLSMAPCONFIG_CREATE_OPTION_RELATIVETO_CURRENTANGLE);
                                    if (ADLRet == ADL_STATUS.ADL_OK)
                                    {
                                        if (newSlsMapIndex != -1)
                                        {
                                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_Create successfully created the new SLSMAP we just created with index {newSlsMapIndex} to TRUE for adapter {slsMapConfig.SLSMap.AdapterIndex}.");

                                            // At this point we have created a new Intel Eyefinity Config
                                        }
                                        else
                                        {
                                            SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: ERROR - ADL2_Display_SLSMapConfig_Create returned ADL_STATUS {ADLRet} but the returned SLSMapIndex was -1, which indicates that the new SLSMAP failed to create for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                        }
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: ERROR - ADL2_Display_SLSMapConfig_Create returned ADL_STATUS {ADLRet} when trying to create a new SLSMAP for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                        return false;
                                    }

                                }

                            }
                        }
                        else
                        {
                            // Otherwise we are using the newer ADLX API to create the Eyefinity Desktop
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Using the newer ADLX API to create the Eyefinity Desktop.");
                            if (displayConfig.EyefinityDesktop.Equals(ActiveDisplayConfig.EyefinityDesktop))
                            {
                                // If the Eyefinity Desktop is already set then we don't need to do anything
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Eyefinity layout is exactly the same as the one we want, so skipping setting up the Eyefinity Desktop");
                            }
                            else
                            {
                                // Otherwise we need to use the new ADLX API to create the Eyefinity Desktop
                                // Setup the EyefinityDesktop using the settings the driver stores internally
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Attempting to get the ADLX EyefinityDesktop object");
                                // Get eyefinitydisplay list
                                SWIGTYPE_p_p_adlx__IADLXSimpleEyefinity ppSimpleEyefinity = ADLX.new_simpleEyefinityP_Ptr();
                                status = desktopService.GetSimpleEyefinity(ppSimpleEyefinity);
                                IADLXSimpleEyefinity simpleEyefinity = ADLX.simpleEyefinityP_Ptr_value(ppSimpleEyefinity);

                                if (status != ADLX_RESULT.ADLX_OK)
                                {
                                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error getting the ADLX SimpleEyefinity object. systemServices.GetSimpleEyefinity() returned error code {status}");
                                    return false;
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Successfully got the ADLX SimpleEyefinity object");
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Attempting to create the ADLX Eyefinity Desktop");
                                    SWIGTYPE_p_p_adlx__IADLXEyefinityDesktop ppEyefinityDesktop = ADLX.new_eyefinityDesktopP_Ptr();
                                    status = simpleEyefinity.Create(ppEyefinityDesktop);
                                    IADLXEyefinityDesktop eyefinityDesktop = ADLX.eyefinityDesktopP_Ptr_value(ppEyefinityDesktop);

                                    if (status != ADLX_RESULT.ADLX_OK)
                                    {
                                        SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error creating the ADLX Eyefinity Desktop. systemServices.GetSimpleEyefinity() returned error code {status}");
                                        return false;
                                    }
                                    else
                                    {
                                        if (displayConfig.EyefinityDesktop.Equals(ActiveDisplayConfig.EyefinityDesktop))
                                        {
                                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: This new Eyefinity layout is exactly the same as the one we want! Our job is done.");
                                        }
                                        else
                                        {
                                            SharedLogger.logger.Warn($"IntelLibrary/SetActiveConfig: This new Eyefinity layout is different from the one we originally saved with this desktop profile. If you have changed your Eyefinity Layout then you need to update this desktop profile!.");
                                        }
                                    }
                                }
                                // Release simpleEyefinity interface
                                simpleEyefinity.Release();
                            }
                        }
                    }
                    else
                    {
                        if (ActiveDisplayConfig.IsEyefinity)
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Eyefinity layout is currently in use but is NOT required, so we need to destroy the Eyefinity Desktop");

                            // Check if we are using the new ADLX or older ADL API to destroy the Eyefinity Desktop
                            if (useADLEyefinity)
                            {
                                // If set then we are using the older ADL API to destroy the Eyefinity Desktop
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Using the older ADL API to destroy the Eyefinity Desktop.");

                                // We need to disable the current Eyefinity (SLS) profile to turn it off
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: SLS is enabled in the current display configuration, so we need to turn it off");
                                // Set the initial state of the ADL_STATUS
                                ADL_STATUS ADLRet = 0;

                                foreach (Intel_SLSMAP_CONFIG slsMapConfig in ActiveDisplayConfig.Adl2SlsConfig.SLSMapConfigs)
                                {
                                    // Turn off this SLS Map Config
                                    ADLRet = IGCLImport.ADL2_Display_SLSMapConfig_SetState(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap.SLSMapIndex, IGCLImport.ADL_FALSE);
                                    if (ADLRet == ADL_STATUS.ADL_OK)
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_SetState successfully disabled the SLSMAP with index {slsMapConfig.SLSMap.SLSMapIndex} for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: ERROR - ADL2_Display_SLSMapConfig_SetState returned ADL_STATUS {ADLRet} when trying to set the SLSMAP with index {slsMapConfig.SLSMap.SLSMapIndex} to FALSE for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                        return false;
                                    }

                                }
                            }
                            else
                            {
                                // Otherwise we are using the new ADLX API to destroy the Eyefinity Desktop
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Using the newer ADLX API to destroy the Eyefinity Desktop.");

                                // Setup the EyefinityDesktop using the settings the driver stores internally
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Attempting to get the ADLX EyefinityDesktop object");
                                // Get eyefinitydisplay list
                                SWIGTYPE_p_p_adlx__IADLXSimpleEyefinity ppSimpleEyefinity = ADLX.new_simpleEyefinityP_Ptr();
                                status = desktopService.GetSimpleEyefinity(ppSimpleEyefinity);
                                IADLXSimpleEyefinity simpleEyefinity = ADLX.simpleEyefinityP_Ptr_value(ppSimpleEyefinity);

                                if (status != ADLX_RESULT.ADLX_OK)
                                {
                                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error getting the ADLX SimpleEyefinity object. systemServices.GetSimpleEyefinity() returned error code {status}");
                                    return false;
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Successfully got the ADLX SimpleEyefinity object");
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Attempting to destroy all the ADLX Eyefinity Desktops");
                                    SWIGTYPE_p_p_adlx__IADLXEyefinityDesktop ppEyefinityDesktop = ADLX.new_eyefinityDesktopP_Ptr();
                                    status = simpleEyefinity.DestroyAll();
                                    if (status != ADLX_RESULT.ADLX_OK)
                                    {
                                        SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error destroying all existing ADLX Eyefinity Desktops. systemServices.GetSimpleEyefinity() returned error code {status}");
                                        return false;
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Successfully destroyed all existing ADLX Eyefinity Desktops. ");
                                    }
                                }
                                // Release simpleEyefinity interface
                                simpleEyefinity.Release();

                            }

                        }
                        else
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Eyefinity layout is not currently in use and is NOT required, so leaving things as they are.");
                        }
                    }                    
                }

                // Release desktop services interface
                desktopService.Release();
                */

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
            if (_initialised)
            {
                IGCLStatus status = IGCLStatus.NO_ERROR;

                /*
                // Get the display services
                // This lets us interact witth the various displays individually
                IADLXDisplayServices displayService;
                IADLXDisplayList displayList;

                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Attempting to get the ADLX display services");
                SWIGTYPE_p_p_adlx__IADLXDisplayServices s = ADLX.new_displaySerP_Ptr();
                status = _adlxSystem.GetDisplaysServices(s);
                displayService = ADLX.displaySerP_Ptr_value(s);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Error getting the ADLX display services. systemServices.GetDisplaysServices() returned error code {status}");
                    return false;
                }
                else
                {
                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully got the display services");
                    // Get the display services
                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Attempting to get the ADLX display list");
                    // Get display list
                    SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                    status = displayService.GetDisplays(ppDisplayList);
                    displayList = ADLX.displayListP_Ptr_value(ppDisplayList);
                    if (status != ADLX_RESULT.ADLX_OK)
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                        return false;
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully got the display list");
                        // Iterate through the display list and see if we need to change any settings
                        uint it = displayList.Begin();
                        for (; it != displayList.Size(); it++)
                        {
                            SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay = ADLX.new_displayP_Ptr();
                            status = displayList.At(it, ppDisplay);
                            IADLXDisplay display = ADLX.displayP_Ptr_value(ppDisplay);

                            if (status == ADLX_RESULT.ADLX_OK)
                            {
                                SWIGTYPE_p_size_t pID = ADLX.new_adlx_sizeP();
                                display.UniqueId(pID);
                                uint id = ADLX.adlx_sizeP_value(pID);

                                SWIGTYPE_p_p_char ppName = ADLX.new_charP_Ptr();
                                display.Name(ppName);
                                string name = ADLX.charP_Ptr_value(ppName);

                                // find the display settings that match this display
                                if (displayConfig.Displays.ContainsKey(id))
                                {
                                    // We have a match, so lets set the display settings
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Found a matching display with ID {id} in the display list");
                                    // Get the display settings we stored
                                    INTEL_DISPLAY_WITH_SETTINGS displaySettingsWeStored = displayConfig.Displays[id];

                                    //------------------------------------
                                    // SET THE COLOR DEPTH IF NEEDED
                                    //------------------------------------
                                    // Get the current color depth for this display
                                    SWIGTYPE_p_p_adlx__IADLXDisplayColorDepth ppColorDepth = ADLX.new_displayColorDepthP_Ptr();
                                    status = displayService.GetColorDepth(display, ppColorDepth);
                                    if (status != ADLX_RESULT.ADLX_OK)
                                    {
                                        SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: Error getting the display color depth object. systemServices.GetColorDepth() returned error code {status}");
                                        //return false;
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully got the display color depth object");
                                        // Check if the color depth is the same as the one we stored
                                        IADLXDisplayColorDepth colorDepth = ADLX.displayColorDepthP_Ptr_value(ppColorDepth);
                                        // Check if the color depth is supported
                                        SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                                        status = colorDepth.IsSupported(pIsSupported);
                                        bool colorDepthIsSupported = ADLX.boolP_value(pIsSupported);
                                        if (status == ADLX_RESULT.ADLX_OK && colorDepthIsSupported)
                                        {
                                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Color Depth can be set for this display!");
                                            // Get the current color depth for this display
                                            SWIGTYPE_p_ADLX_COLOR_DEPTH pColorDepth = ADLX.new_adlx_colorDepthP();
                                            status = colorDepth.GetValue(pColorDepth);
                                            ADLX_COLOR_DEPTH colorDepthValue = ADLX.adlx_colorDepthP_value(pColorDepth);

                                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Checking if Color Depth needs to be changed for this display");
                                            if (colorDepthValue != displaySettingsWeStored.ColorDepth)
                                            {
                                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Color Depth does need to be changed for this display so attempting to change it");
                                                // Set the color depth to the one we stored before
                                                status = colorDepth.SetValue(displaySettingsWeStored.ColorDepth);
                                                if (status != ADLX_RESULT.ADLX_OK)
                                                {
                                                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: Error setting the display color depth. systemServices.SetColorDepth() returned error code {status}");
                                                    //return false;
                                                }
                                                else
                                                {
                                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set the display color depth to {displaySettingsWeStored.ColorDepth.ToString("G")}");
                                                }
                                            }
                                            else
                                            {
                                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Color Depth does NOT need to be changed for this display as it is already set to {displaySettingsWeStored.ColorDepth.ToString("G")}");
                                            }
                                        }
                                        else
                                        {
                                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Color Depth is NOT supported for this display so skipping setting it");
                                        }
                                    }

                                    //------------------------------------
                                    // SET THE DISPLAY CUSTOM COLOR IF NEEDED
                                    //------------------------------------
                                    // Get the current custom color object for this display
                                    SWIGTYPE_p_p_adlx__IADLXDisplayCustomColor ppCustomColor = ADLX.new_displayCustomColorP_Ptr();
                                    status = displayService.GetCustomColor(display, ppCustomColor);
                                    if (status != ADLX_RESULT.ADLX_OK)
                                    {
                                        SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: Error getting the display custom color object. systemServices.GetCustomColor() returned error code {status}");
                                        //return false;
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully got the display custom color object");
                                        // Check if the custom color is the same as the one we stored
                                        IADLXDisplayCustomColor customColor = ADLX.displayCustomColorP_Ptr_value(ppCustomColor);
                                        // Check if the custom color brightness is supported
                                        SWIGTYPE_p_bool pIsBrightnessSupported = ADLX.new_boolP();
                                        status = customColor.IsBrightnessSupported(pIsBrightnessSupported);
                                        bool brightnessIsSupported = ADLX.boolP_value(pIsBrightnessSupported);
                                        if (status == ADLX_RESULT.ADLX_OK && brightnessIsSupported)
                                        {
                                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Custom Color Brightness can be set for this display!");
                                            // Get the current color brightness for this display
                                            SWIGTYPE_p_int pCurrentBrightness = ADLX.new_adlx_intP();
                                            status = customColor.GetBrightness(pCurrentBrightness);
                                            int currentBrightnessValue = ADLX.adlx_intP_value(pCurrentBrightness);

                                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Checking if Custom Color Brightness needs to be changed for this display");
                                            if (currentBrightnessValue != displaySettingsWeStored.CustomColorBrightness)
                                            {
                                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Custom Color Brightness does need to be changed for this display so attempting to change it");
                                                // Set the color depth to the one we stored before
                                                status = customColor.SetBrightness(displaySettingsWeStored.CustomColorBrightness);
                                                if (status != ADLX_RESULT.ADLX_OK)
                                                {
                                                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: Error setting the display Custom Color Brightness. systemServices.CustomColorBrightness() returned error code {status}");
                                                    //return false;
                                                }
                                                else
                                                {
                                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set the display Custom Color Brightness to {displaySettingsWeStored.CustomColorBrightness.ToString("G")}");
                                                }
                                            }
                                            else
                                            {
                                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Custom Color Brightness does NOT need to be changed for this display as it is already set to {displaySettingsWeStored.CustomColorBrightness.ToString("G")}");
                                            }
                                        }
                                        else
                                        {
                                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Custom Color Brightness is NOT supported for this display.");
                                        }
                                    }
                                    SharedLogger.logger.Warn($"IntelLibrary/SetActiveConfigOverride: Found the display settings for this UniqueID but it has a different name");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: No display with UniqueID {id} found in the stored settings, so skipping.");
                                }

                            }

                            // Release display interface
                            display.Release();
                        }
                    }
                    // Release display list interface
                    displayList.Release();
                }

                // Release display services interface
                displayService.Release();*/

            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: ERROR - Tried to run SetActiveConfig but the INtel IGCL library isn't initialised!");
                throw new IntelLibraryException($"Tried to run SetActiveConfig but the Intel IGCL library isn't initialised!");
            }

            return true;
        }



        public bool IsActiveConfig(INTEL_DISPLAY_CONFIG displayConfig)
        {

            // Check whether the display config is in use now
            SharedLogger.logger.Trace($"IntelLibrary/IsActiveConfig: Checking whether the display configuration is already being used.");
            if (displayConfig.Equals(_activeDisplayConfig))
            {
                SharedLogger.logger.Trace($"IntelLibrary/IsActiveConfig: The display configuration is already being used (supplied displayConfig Equals currentWindowsDisplayConfig)");
                return true;
            }
            else
            {
                SharedLogger.logger.Trace($"IntelLibrary/IsActiveConfig: The display configuration is NOT currently in use (supplied displayConfig Equals currentWindowsDisplayConfig)");
                return false;
            }

        }

        public bool IsValidConfig(INTEL_DISPLAY_CONFIG displayConfig)
        {
            // We want to check the Intel Eyefinity (SLS) config is valid
            SharedLogger.logger.Trace($"IntelLibrary/IsValidConfig: Testing whether the display configuration is valid");
            // 
            if (displayConfig.IsInUse && displayConfig.IsEyefinity)
            {
                // At the moment we just assume the config is true so we try to use it
                return true;
            }
            else
            {
                // Its not a Mosaic topology, so we just let it pass, as it's windows settings that matter.
                return true;
            }
        }

        public bool IsPossibleConfig(INTEL_DISPLAY_CONFIG displayConfig)
        {
            // We want to check the Intel profile can be used now
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

            // Otherwise we need to actuially check through things
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

            if (_initialised)
            {
                IGCLStatus status = IGCLStatus.NO_ERROR;

                /*// Get the desktop services
                // This is how we get and iterate through the various desktops. 
                // - A single desktop is associated with one display.
                // - A duplicate desktop is associated with two or more displays.
                // - An Intel Eyefinity desktop is associated with two or more displays.
                IADLXDesktopServices desktopService;
                IADLXDesktopList desktopList;

                SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Attempting to get the ADLX desktop services");
                SWIGTYPE_p_p_adlx__IADLXDesktopServices d = ADLX.new_desktopSerP_Ptr();
                status = _adlxSystem.GetDesktopsServices(d);
                desktopService = ADLX.desktopSerP_Ptr_value(d);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Error getting the ADLX desktop services. systemServices.GetDesktopsServices() returned error code {status}");
                    failure = true;
                }
                else
                {
                    SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Successfully got the desktop services");
                    // Get the display services
                    SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Attempting to get the ADLX desktop list");
                    // Get display list
                    SWIGTYPE_p_p_adlx__IADLXDesktopList ppDesktopList = ADLX.new_desktopListP_Ptr();
                    status = desktopService.GetDesktops(ppDesktopList);
                    desktopList = ADLX.desktopListP_Ptr_value(ppDesktopList);

                    if (status != ADLX_RESULT.ADLX_OK)
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                        failure = true;
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Successfully got the desktop list");
                        // Iterate through the desktop list
                        uint it = desktopList.Begin();
                        for (; it != desktopList.Size(); it++)
                        {
                            SWIGTYPE_p_p_adlx__IADLXDesktop ppDesktop = ADLX.new_desktopP_Ptr();
                            status = desktopList.At(it, ppDesktop);
                            IADLXDesktop desktop = ADLX.desktopP_Ptr_value(ppDesktop);

                            if (status == ADLX_RESULT.ADLX_OK)
                            {

                                SWIGTYPE_p_unsigned_int pNumDisplays = ADLX.new_adlx_uintP();
                                desktop.GetNumberOfDisplays(pNumDisplays);
                                long numDisplays = ADLX.adlx_uintP_value(pNumDisplays);

                                SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                                desktop.GetDisplays(ppDisplayList);
                                IADLXDisplayList desktopDisplayList = ADLX.displayListP_Ptr_value(ppDisplayList);

                                if (status != ADLX_RESULT.ADLX_OK)
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Error getting the ADLX display list for this desktop. desktop.GetDisplays() returned error code {status}");
                                    failure = true;
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Successfully got the display list for this desktop");
                                    // Iterate through the display list
                                    uint displayIt = desktopDisplayList.Begin();
                                    for (; displayIt != desktopDisplayList.Size(); displayIt++)
                                    {
                                        SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay = ADLX.new_displayP_Ptr();
                                        status = desktopDisplayList.At(displayIt, ppDisplay);
                                        IADLXDisplay display = ADLX.displayP_Ptr_value(ppDisplay);
                                        if (status != ADLX_RESULT.ADLX_OK)
                                        {
                                            SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Error getting the ADLX display name. desktop.GetDisplays() returned error code {status}");
                                            failure = true;
                                        }
                                        else
                                        {

                                            // Get the GPU related to this display
                                            SWIGTYPE_p_p_adlx__IADLXGPU ppGPU = ADLX.new_gpuP_Ptr();
                                            display.GetGPU(ppGPU);
                                            IADLXGPU gpu = ADLX.gpuP_Ptr_value(ppGPU);

                                            SWIGTYPE_p_p_char ppGpuName = ADLX.new_charP_Ptr();
                                            gpu.Name(ppGpuName);
                                            string gpuName = ADLX.charP_Ptr_value(ppGpuName);

                                            SWIGTYPE_p_int ppGpuUniqueId = ADLX.new_adlx_intP();
                                            gpu.UniqueId(ppGpuUniqueId);
                                            int gpuUniqueId = ADLX.adlx_intP_value(ppGpuUniqueId);

                                            SWIGTYPE_p_bool ppGpuIsExternal = ADLX.new_boolP();
                                            gpu.IsExternal(ppGpuIsExternal);
                                            bool gpuIsExternal = ADLX.boolP_value(ppGpuIsExternal);

                                            // Release the memory we allocated for the GPU
                                            gpu.Release();

                                            SWIGTYPE_p_p_char ppName = ADLX.new_charP_Ptr();
                                            display.Name(ppName);
                                            String name = ADLX.charP_Ptr_value(ppName);

                                            SWIGTYPE_p_ADLX_DISPLAY_TYPE pDisType = ADLX.new_adlx_displayTypeP();
                                            display.DisplayType(pDisType);
                                            ADLX_DISPLAY_TYPE disType = ADLX.adlx_displayTypeP_value(pDisType);

                                            SWIGTYPE_p_unsigned_int pMID = ADLX.new_adlx_uintP();
                                            display.ManufacturerID(pMID);
                                            long mid = ADLX.adlx_uintP_value(pMID);

                                            SWIGTYPE_p_ADLX_DISPLAY_CONNECTOR_TYPE pConnect = ADLX.new_adlx_displayConnectTypeP();
                                            display.ConnectorType(pConnect);
                                            ADLX_DISPLAY_CONNECTOR_TYPE connect = ADLX.adlx_displayConnectTypeP_value(pConnect);

                                            SWIGTYPE_p_size_t pID = ADLX.new_adlx_sizeP();
                                            display.UniqueId(pID);
                                            uint uniqueId = ADLX.adlx_sizeP_value(pID);

                                            // Create an array of all the important display info we need to record
                                            List<string> displayInfo = new List<string>();
                                            displayInfo.Add("IntelADLX");
                                            try
                                            {
                                                displayInfo.Add(gpuName);
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"IntelLibrary/GetCurrentDisplayIdentifiers: Exception getting GPU Name from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(gpuUniqueId.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"IntelLibrary/GetCurrentDisplayIdentifiers: Exception getting GPU Unique ID from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(gpuIsExternal.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"IntelLibrary/GetCurrentDisplayIdentifiers: Exception getting GPU Is External from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(connect.ToString("G"));
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"IntelLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Connection Type for the display from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(name);
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"IntelLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Name for the display from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(disType.ToString("G"));
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"IntelLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Display Type for the display from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(mid.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"IntelLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Manufacturer for the display from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(uniqueId.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"IntelLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Display Unique ID for the display from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            // Create a display identifier out of it
                                            string displayIdentifier = String.Join("|", displayInfo);
                                            // Add it to the list of display identifiers so we can return it
                                            // but only add it if it doesn't already exist. Otherwise we get duplicates :/
                                            if (!displayIdentifiers.Contains(displayIdentifier))
                                            {
                                                displayIdentifiers.Add(displayIdentifier);
                                                SharedLogger.logger.Debug($"IntelLibrary/GetCurrentDisplayIdentifiers: DisplayIdentifier detected: {displayIdentifier}");
                                            }
                                            // Release display interface
                                            display.Release();

                                        }
                                    }
                                }
                                
                                // Release desktop interface
                                desktop.Release();
                            }
                        }
                    }
                    // Release desktop list interface
                    desktopList.Release();
                }


                // Release desktop services interface
                desktopService.Release();*/

            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/GetSomeDisplayIdentifiers: ERROR - Tried to get Displays but the Intel IGCL library isn't initialised!");
                throw new IntelLibraryException($"Tried to get Displays but the Intel IGCL library isn't initialised!");
            }

            // Sort the display identifiers
            displayIdentifiers.Sort();

            return displayIdentifiers;
        }

        public List<string> GetAllConnectedDisplayIdentifiers(out bool failure)
        {
            SharedLogger.logger.Trace($"IntelLibrary/GetAllConnectedDisplayIdentifiers: Getting all the display identifiers that can possibly be used");

            List<string> displayIdentifiers = new List<string>();
            failure = false;

            if (_initialised)
            {
                IGCLStatus status = IGCLStatus.NO_ERROR;

                /*// Get the display services
                // This lets us interact witth the various displays
                IADLXDisplayServices displayService;
                IADLXDisplayList displayList;

                SharedLogger.logger.Trace($"IntelLibrary/GetAllConnectedDisplayIdentifiers: Attempting to get the ADLX display services");
                SWIGTYPE_p_p_adlx__IADLXDisplayServices s = ADLX.new_displaySerP_Ptr();
                status = _adlxSystem.GetDisplaysServices(s);
                displayService = ADLX.displaySerP_Ptr_value(s);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Trace($"IntelLibrary/GetAllConnectedDisplayIdentifiers: Error getting the ADLX display services. systemServices.GetDisplaysServices() returned error code {status}");
                    failure = true;
                }
                else
                {
                    SharedLogger.logger.Trace($"IntelLibrary/GetAllConnectedDisplayIdentifiers: Successfully got the display services");
                    // Get the display services
                    SharedLogger.logger.Trace($"IntelLibrary/GetAllConnectedDisplayIdentifiers: Attempting to get the ADLX display list");
                    // Get display list
                    SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                    status = displayService.GetDisplays(ppDisplayList);
                    displayList = ADLX.displayListP_Ptr_value(ppDisplayList);
                    if (status != ADLX_RESULT.ADLX_OK)
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/GetAllConnectedDisplayIdentifiers: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                        failure = true;
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/GetAllConnectedDisplayIdentifiers: Successfully got the display list");
                        // Iterate through the display list
                        uint it = displayList.Begin();
                        for (; it != displayList.Size(); it++)
                        {
                            SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay = ADLX.new_displayP_Ptr();
                            status = displayList.At(it, ppDisplay);
                            IADLXDisplay display = ADLX.displayP_Ptr_value(ppDisplay);

                            if (status == ADLX_RESULT.ADLX_OK)
                            {
                                // Get the GPU related to this display
                                SWIGTYPE_p_p_adlx__IADLXGPU ppGPU = ADLX.new_gpuP_Ptr();
                                display.GetGPU(ppGPU);
                                IADLXGPU gpu = ADLX.gpuP_Ptr_value(ppGPU);

                                SWIGTYPE_p_p_char ppGpuName = ADLX.new_charP_Ptr();
                                gpu.Name(ppGpuName);
                                string gpuName = ADLX.charP_Ptr_value(ppGpuName);

                                SWIGTYPE_p_int ppGpuUniqueId = ADLX.new_adlx_intP();
                                gpu.UniqueId(ppGpuUniqueId);
                                int gpuUniqueId = ADLX.adlx_intP_value(ppGpuUniqueId);

                                SWIGTYPE_p_bool ppGpuIsExternal = ADLX.new_boolP();
                                gpu.IsExternal(ppGpuIsExternal);
                                bool gpuIsExternal = ADLX.boolP_value(ppGpuIsExternal);

                                *//*SWIGTYPE_p_p_char ppGpuVendorId = ADLX.new_charP_Ptr();
                                gpu.VendorId(ppGpuVendorId);
                                string gpuVendorId = ADLX.charP_Ptr_value(ppGpuVendorId);*//*

                                // Release the memory we allocated for the GPU
                                gpu.Release();

                                SWIGTYPE_p_p_char ppName = ADLX.new_charP_Ptr();
                                display.Name(ppName);
                                String name = ADLX.charP_Ptr_value(ppName);

                                SWIGTYPE_p_ADLX_DISPLAY_TYPE pDisType = ADLX.new_adlx_displayTypeP();
                                display.DisplayType(pDisType);
                                ADLX_DISPLAY_TYPE disType = ADLX.adlx_displayTypeP_value(pDisType);

                                SWIGTYPE_p_unsigned_int pMID = ADLX.new_adlx_uintP();
                                display.ManufacturerID(pMID);
                                long mid = ADLX.adlx_uintP_value(pMID);

                                SWIGTYPE_p_ADLX_DISPLAY_CONNECTOR_TYPE pConnect = ADLX.new_adlx_displayConnectTypeP();
                                display.ConnectorType(pConnect);
                                ADLX_DISPLAY_CONNECTOR_TYPE connect = ADLX.adlx_displayConnectTypeP_value(pConnect);

                                *//*SWIGTYPE_p_p_char ppEDIE = ADLX.new_charP_Ptr();
                                display.EDID(ppEDIE);
                                String edid = ADLX.charP_Ptr_value(ppEDIE);

                                SWIGTYPE_p_int pH = ADLX.new_intP();
                                SWIGTYPE_p_int pV = ADLX.new_intP();
                                display.NativeResolution(pH, pV);
                                int h = ADLX.intP_value(pH);
                                int v = ADLX.intP_value(pV);

                                SWIGTYPE_p_double pRefRate = ADLX.new_doubleP();
                                display.RefreshRate(pRefRate);
                                double refRate = ADLX.doubleP_value(pRefRate);

                                SWIGTYPE_p_unsigned_int pPixClock = ADLX.new_uintP();
                                display.PixelClock(pPixClock);
                                long pixClock = ADLX.uintP_value(pPixClock);

                                SWIGTYPE_p_ADLX_DISPLAY_SCAN_TYPE pScanType = ADLX.new_disScanTypeP();
                                display.ScanType(pScanType);
                                ADLX_DISPLAY_SCAN_TYPE scanType = ADLX.disScanTypeP_value(pScanType);*//*

                                SWIGTYPE_p_size_t pID = ADLX.new_adlx_sizeP();
                                display.UniqueId(pID);
                                uint uniqueId = ADLX.adlx_sizeP_value(pID);

*//*                                Console.WriteLine(String.Format("\nThe display [{0}]:", it));
                                Console.WriteLine(String.Format("\tName: {0}", name));
                                Console.WriteLine(String.Format("\tType: {0}", disType));
                                Console.WriteLine(String.Format("\tConnector type: {0}", connect));
                                Console.WriteLine(String.Format("\tManufacturer id: {0}", mid));*//*
                                //Console.WriteLine(String.Format("\tEDID: {0}", edid));
                                *//*Console.WriteLine(String.Format("\tResolution:  h: {0}  v: {1}", h, v));
                                Console.WriteLine(String.Format("\tRefresh rate: {0}", refRate));
                                Console.WriteLine(String.Format("\tPixel clock: {0}", pixClock));
                                Console.WriteLine(String.Format("\tScan type: {0}", scanType));
                                Console.WriteLine(String.Format("\tUnique id: {0}", id));*//*

                                // Create an array of all the important display info we need to record
                                List<string> displayInfo = new List<string>();
                                displayInfo.Add("IntelADLX");
                                try
                                {
                                    displayInfo.Add(gpuName);
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"IntelLibrary/GetAllConnectedDisplayIdentifiers: Exception getting GPU Name from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(gpuUniqueId.ToString());
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"IntelLibrary/GetAllConnectedDisplayIdentifiers: Exception getting GPU Unique ID from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(gpuIsExternal.ToString());
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"IntelLibrary/GetAllConnectedDisplayIdentifiers: Exception getting GPU Is External from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(connect.ToString("G"));
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"IntelLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Connection Type for the display from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(name);
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"IntelLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Name for the display from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(disType.ToString("G"));
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"IntelLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Display Type for the display from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(mid.ToString());
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"IntelLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Manufacturer for the display from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(uniqueId.ToString());
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"IntelLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Display Unique ID for the display from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                // Create a display identifier out of it
                                string displayIdentifier = String.Join("|", displayInfo);
                                // Add it to the list of display identifiers so we can return it
                                // but only add it if it doesn't already exist. Otherwise we get duplicates :/
                                if (!displayIdentifiers.Contains(displayIdentifier))
                                {
                                    displayIdentifiers.Add(displayIdentifier);
                                    SharedLogger.logger.Debug($"IntelLibrary/GetAllConnectedDisplayIdentifiers: DisplayIdentifier detected: {displayIdentifier}");
                                }
                                // Release display interface
                                display.Release();
                            }
                        }
                    }
                    // Release display list interface
                    displayList.Release();
                }

                // Release display services interface
                displayService.Release();*/

            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/GetSomeDisplayIdentifiers: ERROR - Tried to get Displays but the Intel IGCL library isn't initialised!");
                throw new IntelLibraryException($"Tried to get Displays but the Intel IGCL library isn't initialised!");
            }

            // Sort the display identifiers
            displayIdentifiers.Sort();

            return displayIdentifiers;
        }


    }

    [global::System.Serializable]
    public class IntelLibraryException : Exception
    {
        public IntelLibraryException() { }
        public IntelLibraryException(string message) : base(message) { }
        public IntelLibraryException(string message, Exception inner) : base(message, inner) { }
    }
}