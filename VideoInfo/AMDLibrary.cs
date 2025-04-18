﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using DisplayMagicianShared;
using System.ComponentModel;
using DisplayMagicianShared.Windows;
using System.Threading;
using DisplayMagicianShared.NVIDIA;
using static System.Net.Mime.MediaTypeNames;
using EDIDParser;
using Windows.Devices.PointOfService;
using Windows.Graphics;
using System.Security.Cryptography;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace DisplayMagicianShared.AMD
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AMD_DESKTOP : IEquatable<AMD_DESKTOP>
    {
        public long NumberOfDisplays;
        public List<AMD_DISPLAY> Displays;
        public ADLX_ORIENTATION Orientation;
        public int SizeWidth;
        public int SizeHeight;
        public int TopLeftX;
        public int TopLeftY;
        public ADLX_DESKTOP_TYPE Type;

        public AMD_DESKTOP()
        {
            Displays = new List<AMD_DISPLAY>();
            Type = ADLX_DESKTOP_TYPE.DESKTOP_SINGLE;
        }

        public override bool Equals(object obj) => obj is AMD_DESKTOP other && this.Equals(other);
        public bool Equals(AMD_DESKTOP other)
        {
            if (NumberOfDisplays != other.NumberOfDisplays)
            {
                SharedLogger.logger.Trace($"AMD_DESKTOP/Equals: The NumberOfDisplays values don't equal each other");
                return false;
            }
            if (!Displays.SequenceEqual(other.Displays))
            {
                SharedLogger.logger.Trace($"AMD_DESKTOP/Equals: The SequenceEqual values don't equal each other");
                return false;
            }
            if (Orientation != other.Orientation)
            {
                SharedLogger.logger.Trace($"AMD_DESKTOP/Equals: The Orientation values don't equal each other");
                return false;
            }
            if (SizeWidth != other.SizeWidth)
            {
                SharedLogger.logger.Trace($"AMD_DESKTOP/Equals: The SizeWidth values don't equal each other");
                return false;
            }
            if (SizeHeight != other.SizeHeight)
            {
                SharedLogger.logger.Trace($"AMD_DESKTOP/Equals: The SizeHeight values don't equal each other");
                return false;
            }
            if (TopLeftX != other.TopLeftX)
            {
                SharedLogger.logger.Trace($"AMD_DESKTOP/Equals: The TopLeftX values don't equal each other");
                return false;
            }
            if (TopLeftY != other.TopLeftY)
            {
                SharedLogger.logger.Trace($"AMD_DESKTOP/Equals: The TopLeftY values don't equal each other");
                return false;
            }
            if (Type != other.Type)
            {
                SharedLogger.logger.Trace($"AMD_DESKTOP/Equals: The Type values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (NumberOfDisplays, Displays, Orientation, SizeWidth, SizeHeight, TopLeftX, TopLeftY, Type).GetHashCode();
        }
        public static bool operator ==(AMD_DESKTOP lhs, AMD_DESKTOP rhs) => lhs.Equals(rhs);

        public static bool operator !=(AMD_DESKTOP lhs, AMD_DESKTOP rhs) => !(lhs == rhs);
    }

    /* [StructLayout(LayoutKind.Sequential)]
     public struct EYEFINITY_GRID_NODE : IEquatable<EYEFINITY_GRID_NODE>
     {
         public long Row;
         public long Column;
         public ADLX_ORIENTATION DisplayOrientation;
         public int DisplayWidth;
         public int DisplayHeight;
         public int DisplayTopLeftX;
         public int DisplayTopLeftY;
         public long DisplayUniqueId;

         public EYEFINITY_GRID_NODE()
         {
             DisplayOrientation = ADLX_ORIENTATION.ORIENTATION_LANDSCAPE;
         }

         public override bool Equals(object obj) => obj is EYEFINITY_GRID_NODE other && this.Equals(other);
         public bool Equals(EYEFINITY_GRID_NODE other)
         {
             if (Row != other.Row)
             {
                 SharedLogger.logger.Trace($"EYEFINITY_GRID_NODE/Equals: The Row values don't equal each other");
                 return false;
             }
             if (Column != other.Column)
             {
                 SharedLogger.logger.Trace($"EYEFINITY_GRID_NODE/Equals: The Column values don't equal each other");
                 return false;
             }
             if (DisplayOrientation != other.DisplayOrientation)
             {
                 SharedLogger.logger.Trace($"EYEFINITY_GRID_NODE/Equals: The DisplayOrientation values don't equal each other");
                 return false;
             }
             if (DisplayWidth != other.DisplayWidth)
             {
                 SharedLogger.logger.Trace($"EYEFINITY_GRID_NODE/Equals: The DisplayWidth values don't equal each other");
                 return false;
             }
             if (DisplayHeight != other.DisplayHeight)
             {
                 SharedLogger.logger.Trace($"EYEFINITY_GRID_NODE/Equals: The DisplayHeight values don't equal each other");
                 return false;
             }
             if(DisplayTopLeftX != other.DisplayTopLeftX)
             {
                 SharedLogger.logger.Trace($"EYEFINITY_GRID_NODE/Equals: The DisplayTopLeftX values don't equal each other");
                 return false;
             }
             if (DisplayTopLeftY != other.DisplayTopLeftY)
             {
                 SharedLogger.logger.Trace($"EYEFINITY_GRID_NODE/Equals: The DisplayTopLeftY values don't equal each other");
                 return false;
             }
             if (DisplayUniqueId != other.DisplayUniqueId)
             {
                 SharedLogger.logger.Trace($"EYEFINITY_GRID_NODE/Equals: The DisplayUniqueId values don't equal each other");
                 return false;
             }
             return true;
         }

         public override int GetHashCode()
         {
             return (Row, Column, DisplayOrientation, DisplayWidth, DisplayHeight, DisplayTopLeftX, DisplayTopLeftY, DisplayUniqueId).GetHashCode();
         }
         public static bool operator ==(EYEFINITY_GRID_NODE lhs, EYEFINITY_GRID_NODE rhs) => lhs.Equals(rhs);

         public static bool operator !=(EYEFINITY_GRID_NODE lhs, EYEFINITY_GRID_NODE rhs) => !(lhs == rhs);
     }*/

    [StructLayout(LayoutKind.Sequential)]
    public struct AMD_EYEFINITY_DESKTOP : IEquatable<AMD_EYEFINITY_DESKTOP>
    {
        public long Rows;
        public long Columns;
        public ADLX_ORIENTATION Orientation;
        public int SizeWidth;
        public int SizeHeight;
        public int TopLeftX;
        public int TopLeftY;
        //public EYEFINITY_GRID_NODE[][] Grid;

        public AMD_EYEFINITY_DESKTOP()
        {
            Orientation = ADLX_ORIENTATION.ORIENTATION_LANDSCAPE;
            //Grid = Array.Empty<EYEFINITY_GRID_NODE[]>();
        }

        public override bool Equals(object obj) => obj is AMD_EYEFINITY_DESKTOP other && this.Equals(other);
        public bool Equals(AMD_EYEFINITY_DESKTOP other)
        {
            if (Rows != other.Rows)
            {
                SharedLogger.logger.Trace($"AMD_EYEFINITY_DESKTOP/Rows: The Rows values don't equal each other");
                return false;
            }
            if (Columns != other.Columns)
            {
                SharedLogger.logger.Trace($"AMD_EYEFINITY_DESKTOP/Equals: The Columns values don't equal each other");
                return false;
            }
            if (Orientation != other.Orientation)
            {
                SharedLogger.logger.Trace($"AMD_EYEFINITY_DESKTOP/Equals: The Orientation values don't equal each other");
                return false;
            }
            if (SizeWidth != other.SizeWidth)
            {
                SharedLogger.logger.Trace($"AMD_EYEFINITY_DESKTOP/Equals: The SizeWidth values don't equal each other");
                return false;
            }
            if (SizeHeight != other.SizeHeight)
            {
                SharedLogger.logger.Trace($"AMD_EYEFINITY_DESKTOP/Equals: The SizeHeight values don't equal each other");
                return false;
            }
            if (TopLeftX != other.TopLeftX)
            {
                SharedLogger.logger.Trace($"AMD_EYEFINITY_DESKTOP/Equals: The TopLeftX values don't equal each other");
                return false;
            }
            if (TopLeftY != other.TopLeftY)
            {
                SharedLogger.logger.Trace($"AMD_EYEFINITY_DESKTOP/Equals: The TopLeftY values don't equal each other");
                return false;
            }
            /*if (Grid.SequenceEqual(other.Grid))
            {
                SharedLogger.logger.Trace($"AMD_EYEFINITY_DESKTOP/Equals: The Grid values don't equal each other");
                return false;
            }*/
            return true;
        }

        public override int GetHashCode()
        {
            return (Rows, Columns, Orientation, SizeWidth, SizeHeight, TopLeftX, TopLeftY).GetHashCode();
        }
        public static bool operator ==(AMD_EYEFINITY_DESKTOP lhs, AMD_EYEFINITY_DESKTOP rhs) => lhs.Equals(rhs);

        public static bool operator !=(AMD_EYEFINITY_DESKTOP lhs, AMD_EYEFINITY_DESKTOP rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AMD_DISPLAY_WITH_SETTINGS : IEquatable<AMD_DISPLAY_WITH_SETTINGS>
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

        public AMD_DISPLAY_WITH_SETTINGS()
        {
            EDID = "";
            Name = "";
            ScanType = ADLX_DISPLAY_SCAN_TYPE.PROGRESSIVE;
            ConnectorType = ADLX_DISPLAY_CONNECTOR_TYPE.DISPLAY_CONTYPE_UNKNOWN;
            DisplayType = ADLX_DISPLAY_TYPE.DISPLAY_TYPE_UNKOWN;
            ColorDepth = ADLX_COLOR_DEPTH.BPC_UNKNOWN;
        }

        public override bool Equals(object obj) => obj is AMD_DISPLAY_WITH_SETTINGS other && this.Equals(other);
        public bool Equals(AMD_DISPLAY_WITH_SETTINGS other)
        {
            if (ConnectorType != other.ConnectorType)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The ConnectorType values don't equal each other");
                return false;
            }
            if (DisplayType != other.DisplayType)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The DisplayType values don't equal each other");
                return false;
            }
            if (EDID != other.EDID)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The EDID values don't equal each other");
                return false;
            }
            if (ManufacturerID != other.ManufacturerID)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The ManufacturerID values don't equal each other");
                return false;
            }
            if (Name != other.Name)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The Name values don't equal each other");
                return false;
            }
            if (MaxHResolution != other.MaxHResolution)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The MaxHResolution values don't equal each other");
                return false;
            }
            if (MaxVResolution != other.MaxVResolution)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The MaxVResolution values don't equal each other");
                return false;
            }
            if (PixelClock != other.PixelClock)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The PixelClock values don't equal each other");
                return false;
            }
            if (RefreshRate != other.RefreshRate)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The RefreshRate values don't equal each other");
                return false;
            }
            if (ScanType != other.ScanType)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The ScanType values don't equal each other");
                return false;
            }
            if (UniqueID != other.UniqueID)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The UniqueID values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (ConnectorType, DisplayType, EDID, ManufacturerID, Name, MaxHResolution, MaxVResolution, PixelClock, RefreshRate, ScanType, UniqueID).GetHashCode();
        }
        public static bool operator ==(AMD_DISPLAY_WITH_SETTINGS lhs, AMD_DISPLAY_WITH_SETTINGS rhs) => lhs.Equals(rhs);

        public static bool operator !=(AMD_DISPLAY_WITH_SETTINGS lhs, AMD_DISPLAY_WITH_SETTINGS rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AMD_DISPLAY : IEquatable<AMD_DISPLAY>
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

        public AMD_DISPLAY()
        {
            EDID = "";
            Name = "";
            ScanType = ADLX_DISPLAY_SCAN_TYPE.PROGRESSIVE;
            ConnectorType = ADLX_DISPLAY_CONNECTOR_TYPE.DISPLAY_CONTYPE_UNKNOWN;
            DisplayType = ADLX_DISPLAY_TYPE.DISPLAY_TYPE_UNKOWN;
        }

        public override bool Equals(object obj) => obj is AMD_DISPLAY other && this.Equals(other);
        public bool Equals(AMD_DISPLAY other)
        {
            if (ConnectorType != other.ConnectorType)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The ConnectorType values don't equal each other");
                return false;
            }
            if (DisplayType != other.DisplayType)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The DisplayType values don't equal each other");
                return false;
            }
            if (EDID != other.EDID)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The EDID values don't equal each other");
                return false;
            }
            if (ManufacturerID != other.ManufacturerID)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The ManufacturerID values don't equal each other");
                return false;
            }
            if (Name != other.Name)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The Name values don't equal each other");
                return false;
            }
            if (MaxHResolution != other.MaxHResolution)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The MaxHResolution values don't equal each other");
                return false;
            }
            if (MaxVResolution != other.MaxVResolution)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The MaxVResolution values don't equal each other");
                return false;
            }
            if (PixelClock != other.PixelClock)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The PixelClock values don't equal each other");
                return false;
            }
            if (RefreshRate != other.RefreshRate)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The RefreshRate values don't equal each other");
                return false;
            }
            if (ScanType != other.ScanType)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The ScanType values don't equal each other");
                return false;
            }
            if (UniqueID != other.UniqueID)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The UniqueID values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (ConnectorType, DisplayType, EDID, ManufacturerID, Name, MaxHResolution, MaxVResolution, PixelClock, RefreshRate, ScanType, UniqueID).GetHashCode();
        }
        public static bool operator ==(AMD_DISPLAY lhs, AMD_DISPLAY rhs) => lhs.Equals(rhs);

        public static bool operator !=(AMD_DISPLAY lhs, AMD_DISPLAY rhs) => !(lhs == rhs);
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct AMD_DISPLAY_CONFIG : IEquatable<AMD_DISPLAY_CONFIG>
    {
        public bool IsInUse;
        public bool IsCloned;
        public bool IsEyefinity;
        public List<AMD_DESKTOP> Desktops;
        public AMD_EYEFINITY_DESKTOP EyefinityDesktop;
        public Dictionary<long,AMD_DISPLAY_WITH_SETTINGS> Displays;
        public List<string> DisplayIdentifiers;

        public AMD_DISPLAY_CONFIG()
        {
            IsInUse = false;
            IsCloned = false;
            IsEyefinity = false;
            Desktops = new List<AMD_DESKTOP>();
            EyefinityDesktop = new AMD_EYEFINITY_DESKTOP();
            Displays = new Dictionary<long,AMD_DISPLAY_WITH_SETTINGS>();
            DisplayIdentifiers = new List<string>();
        }

        public override bool Equals(object obj) => obj is AMD_DISPLAY_CONFIG other && this.Equals(other);
        public bool Equals(AMD_DISPLAY_CONFIG other)
        {
            if (IsInUse != other.IsInUse)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_CONFIG/Equals: The IsInUse values don't equal each other");
                return false;
            }
            if (IsCloned != other.IsCloned)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_CONFIG/Equals: The IsCloned values don't equal each other");
                return false;
            }
            if (!Desktops.SequenceEqual(other.Desktops))
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_CONFIG/Equals: The Desktops values don't equal each other");
                return false;
            }
            if (IsEyefinity != other.IsEyefinity)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_CONFIG/Equals: The IsEyefinity values don't equal each other");
                return false;
            }
            if (!EyefinityDesktop.Equals(other.EyefinityDesktop))
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_CONFIG/Equals: The EyefinityDesktop values don't equal each other");
                return false;
            }
            if (!Displays.SequenceEqual(other.Displays))
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_CONFIG/Equals: The Displays values don't equal each other");
                return false;
            }            
            if (!DisplayIdentifiers.SequenceEqual(other.DisplayIdentifiers))
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_CONFIG/Equals: The DisplayIdentifiers values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (IsInUse, IsCloned, Desktops, IsEyefinity, EyefinityDesktop, Displays, DisplayIdentifiers).GetHashCode();
        }

        public static bool operator ==(AMD_DISPLAY_CONFIG lhs, AMD_DISPLAY_CONFIG rhs) => lhs.Equals(rhs);

        public static bool operator !=(AMD_DISPLAY_CONFIG lhs, AMD_DISPLAY_CONFIG rhs) => !(lhs == rhs);
    }

    class AMDLibrary : IDisposable
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization
        private static AMDLibrary _instance = new AMDLibrary();

        private bool _initialised = false;

        // To detect redundant calls
        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        //private IntPtr _adlContextHandle = IntPtr.Zero;
        private ADLXHelper _adlxHelper;
        private IADLXSystem _adlxSystem;
        private AMD_DISPLAY_CONFIG? _activeDisplayConfig;
        public List<ADL_DISPLAY_CONNECTION_TYPE> SkippedColorConnectionTypes;
        public List<string> _allConnectedDisplayIdentifiers;
        public const string AMD_ADLX_BINDING_DLL = "ADLXCSharpBind.dll";

        static AMDLibrary() { }
        public AMDLibrary()
        {
            // Populate the list of ConnectionTypes we want to skip as they don't support querying
            SkippedColorConnectionTypes = new List<ADL_DISPLAY_CONNECTION_TYPE> {
                ADL_DISPLAY_CONNECTION_TYPE.Composite,
                ADL_DISPLAY_CONNECTION_TYPE.DVI_D,
                ADL_DISPLAY_CONNECTION_TYPE.DVI_I,
                ADL_DISPLAY_CONNECTION_TYPE.RCA_3Component,
                ADL_DISPLAY_CONNECTION_TYPE.SVideo,
                ADL_DISPLAY_CONNECTION_TYPE.VGA
            };

            _activeDisplayConfig = CreateDefaultConfig();
            try
            {
                SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Attempting to load the AMD ADLX Binding DLL {AMD_ADLX_BINDING_DLL}");
                // Attempt to prelink all of the ADLX functions
                LoadLibrary(AMD_ADLX_BINDING_DLL);
                //Marshal.PrelinkAll(typeof(ADLImport));

                SharedLogger.logger.Trace("AMDLibrary/AMDLibrary: Intialising AMD ADLX library interface");
                // Second parameter is 1 so that we only the get connected adapters in use now

                // We set the environment variable as a workaround so that ADL2_Display_SLSMapConfigX2_Get works :(
                // This is a weird thing that AMD even set in their own code! WTF! Who programmed that as a feature?
                Environment.SetEnvironmentVariable("ADL_4KWORKAROUND_CANCEL", "TRUE");

                // Initialize ADLX with ADLXHelper
                _adlxHelper = new ADLXHelper();
                ADLX_RESULT status = _adlxHelper.Initialize();
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Equals($"AMDLibrary/AMDLibrary: Error intialising AMD ADLX library. ADLXHelper.Initialize() returned error code {status}");
                }
                else
                {                   
                    try
                    {
                        // Get system services
                        SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Attempting to get the ADLX system services");
                        _adlxSystem = _adlxHelper.GetSystemServices();
                        _initialised = true;
                        SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: AMD ADLX library was initialised successfully");
                    }
                    catch (Exception ex)
                    {
                        SharedLogger.logger.Trace(ex, $"AMDLibrary/AMDLibrary: Exception getting the ADLX system services");
                        SharedLogger.logger.Trace(ex, $"AMDLibrary/AMDLibrary: Terminating the ADLXHelper to avoid memory leaks");
                        _adlxHelper.Terminate();
                        SharedLogger.logger.Trace(ex, $"AMDLibrary/AMDLibrary: Setting ADLXHelper to null");
                        _adlxHelper = null;
                    }

                    SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Automatically getting the AMD Display Configuration");
                    _activeDisplayConfig = GetActiveConfig();
                    SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Automatically getting the AMD Connected Display Identifiers");
                    _allConnectedDisplayIdentifiers = GetAllConnectedDisplayIdentifiers(out bool failure);
                }

            }
            catch (TypeInitializationException ex)
            {
                SharedLogger.logger.Info(ex, $"AMDLibrary/AMDLibrary: TypeInitializationException trying to load the AMD ADLX DLL {AMD_ADLX_BINDING_DLL}. This generally means you don't have the AMD ADLX driver installed.");
                return;
            }
            catch (DllNotFoundException ex)
            {
                // If we get here then the AMD ADL DLL wasn't found. We can't continue to use it, so we log the error and exit
                SharedLogger.logger.Info(ex, $"AMDLibrary/AMDLibrary: DllNotFoundException trying to load the AMD ADLX DLL {AMD_ADLX_BINDING_DLL}. This generally means you don't have the AMD ADLX driver installed.");
                return;
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Info(ex, $"AMDLibrary/AMDLibrary: A general exception trying to load the AMD ADLX DLL {AMD_ADLX_BINDING_DLL}.");
                return;
            }
        }

        ~AMDLibrary()
        {
            SharedLogger.logger.Trace("AMDLibrary/~AMDLibrary: Destroying AMDX Library");
            Dispose(false);
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

            // Dispose unmanaged resources
            if (_adlxHelper != null)
            {

                SharedLogger.logger.Trace("AMDLibrary/Dispose: Destroying AMD ADLX library interface");
                // If the ADL2 library was initialised, then we need to free it up.
                if (_initialised)
                {
                    try
                    {
                        // Terminate ADLX
                        ADLX_RESULT status = _adlxHelper.Terminate();
                        _adlxHelper = null;
                        if (status != ADLX_RESULT.ADLX_OK)
                        {
                            SharedLogger.logger.Error($"AMDLibrary/Dispose: Error destroying AMD ADLX library. _adlxHelper.Terminate() returned error code {status}");
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/Dispose: AMD ADLX library was destroyed successfully");
                        }
                    }
                    catch (Exception ex)
                    {
                        SharedLogger.logger.Trace(ex, $"AMDLibrary/Dispose: Exception destroying AMD ADL2 library. _adlxHelper.Terminate() caused an exception.");
                    }

                }
            }

            _disposed = true;
        }

        public static void KeepVideoCardOn()
        {
            LoadLibrary("AMDExportsDLL.dll");
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
                // A list of all the matching PCI Vendor IDs are per https://www.pcilookup.com/?ven=amd&dev=&action=submit
                return new List<string>() { "1002" };
            }
        }

        public AMD_DISPLAY_CONFIG ActiveDisplayConfig
        {
            get
            {
                if(_activeDisplayConfig == null)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/ActiveDisplayConfig: ActiveDisplayConfig is null, so creating a new one");
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

        public static AMDLibrary GetLibrary()
        {
            if (_instance == null)
            {
                _instance = new AMDLibrary();
            }

            return _instance;
        }

        public AMD_DISPLAY_CONFIG CreateDefaultConfig()
        {
            AMD_DISPLAY_CONFIG myDefaultConfig = new AMD_DISPLAY_CONFIG();

            // Fill in the minimal amount we need to avoid null references
            // so that we won't break json.net when we save a default config

            // THIS IS ALL TAKEN CARE OF IN THE STRUCT CONSTRUCTORS NOW \o/ yay!
            /*myDefaultConfig.IsInUse = false;
            myDefaultConfig.IsCloned = false;
            myDefaultConfig.IsEyefinity = false;
            myDefaultConfig.Desktops = new List<AMD_DESKTOP>();
            myDefaultConfig.Displays = new List<AMD_DISPLAY_WITH_SETTINGS>();
            myDefaultConfig.DisplayIdentifiers = new List<string>();
            myDefaultConfig.EyefinityDesktop = new AMD_EYEFINITY_DESKTOP();
            myDefaultConfig.EyefinityDesktop.Grid = Array.Empty<EYEFINITY_GRID_NODE[]>();*/

            return myDefaultConfig;
        }

        public bool UpdateActiveConfig()
        {
            SharedLogger.logger.Trace($"AMDLibrary/UpdateActiveConfig: Updating the currently active config");
            try
            {
                _activeDisplayConfig = GetActiveConfig();
                _allConnectedDisplayIdentifiers = GetAllConnectedDisplayIdentifiers(out bool failure);
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Trace(ex, $"AMDLibrary/UpdateActiveConfig: Exception updating the currently active config");
                return false;
            }

            return true;
        }

        public AMD_DISPLAY_CONFIG GetActiveConfig()
        {
            SharedLogger.logger.Trace($"AMDLibrary/GetActiveConfig: Getting the currently active config");
            bool allDisplays = true;
            return GetAMDDisplayConfig(allDisplays);
        }

        private AMD_DISPLAY_CONFIG GetAMDDisplayConfig(bool allDisplays = false)
        {
            // Creat empty config struct so we know there are no nulls in there to break the json serializer
            AMD_DISPLAY_CONFIG myDisplayConfig = CreateDefaultConfig();

            if (_initialised)
            {
                ADLX_RESULT status = ADLX_RESULT.ADLX_OK;
                // Get the desktop services
                // This is how we get and iterate through the various desktops. 
                // - A single desktop is associated with one display.
                // - A duplicate desktop is associated with two or more displays.
                // - An AMD Eyefinity desktop is associated with two or more displays.
                IADLXDesktopServices desktopService;
                IADLXDesktopList desktopList;

                bool isEyefinityEnabled = false;
                bool isCloned = false;
                List<AMD_DESKTOP> desktopsToStore = new List<AMD_DESKTOP>();
                List<AMD_DISPLAY_WITH_SETTINGS> displaysToStore = new List<AMD_DISPLAY_WITH_SETTINGS>();
                AMD_EYEFINITY_DESKTOP eyefinityDesktopToStore = new AMD_EYEFINITY_DESKTOP();

                SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Attempting to get the ADLX desktop services");
                SWIGTYPE_p_p_adlx__IADLXDesktopServices d = ADLX.new_desktopSerP_Ptr();
                status = _adlxSystem.GetDesktopsServices(d);
                desktopService = ADLX.desktopSerP_Ptr_value(d);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Error getting the ADLX desktop services. systemServices.GetDesktopsServices() returned error code {status}");
                    return CreateDefaultConfig(); ;
                }
                else
                {

                    // Get the list of Desktops we have (this is more for informational purposes)

                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got the desktop services");
                    // Get the display services
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Attempting to get the ADLX desktop list");
                    // Get display list
                    SWIGTYPE_p_p_adlx__IADLXDesktopList ppDesktopList = ADLX.new_desktopListP_Ptr();
                    status = desktopService.GetDesktops(ppDesktopList);
                    desktopList = ADLX.desktopListP_Ptr_value(ppDesktopList);

                    if (status != ADLX_RESULT.ADLX_OK)
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                        return CreateDefaultConfig(); ;
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: Successfully got the desktop list");
                        // Iterate through the desktop list
                        uint it = desktopList.Begin();
                        for (; it != desktopList.Size(); it++)
                        {
                            SWIGTYPE_p_p_adlx__IADLXDesktop ppDesktop = ADLX.new_desktopP_Ptr();
                            status = desktopList.At(it, ppDesktop);
                            IADLXDesktop desktop = ADLX.desktopP_Ptr_value(ppDesktop);

                            if (status == ADLX_RESULT.ADLX_OK)
                            {
                                AMD_DESKTOP newDesktop = new AMD_DESKTOP();
                                newDesktop.Displays = new List<AMD_DISPLAY>();

                                SWIGTYPE_p_unsigned_int pNumDisplays = ADLX.new_adlx_uintP();
                                desktop.GetNumberOfDisplays(pNumDisplays);
                                newDesktop.NumberOfDisplays = ADLX.adlx_uintP_value(pNumDisplays);
                                SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: The number of displays that are part of this desktop is {newDesktop.NumberOfDisplays}");

                                if (newDesktop.NumberOfDisplays > 0)
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: The number of displays that are part of this desktop is > 0, so getting list of displays");
                                    // Get the list of displays that are part of this desktop
                                    SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                                    status = desktop.GetDisplays(ppDisplayList);
                                    IADLXDisplayList desktopDisplayList = ADLX.displayListP_Ptr_value(ppDisplayList);
                                    if (status != ADLX_RESULT.ADLX_OK)
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                                        return CreateDefaultConfig(); ;
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: Successfully got the display list");
                                        // Iterate through the display list
                                        uint itDisplay = desktopDisplayList.Begin();
                                        for (; itDisplay != desktopDisplayList.Size(); itDisplay++)
                                        {
                                            SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay = ADLX.new_displayP_Ptr();
                                            status = desktopDisplayList.At(itDisplay, ppDisplay);
                                            IADLXDisplay display = ADLX.displayP_Ptr_value(ppDisplay);
                                            if (status == ADLX_RESULT.ADLX_OK)
                                            {
                                                // Create a new AMD_DISPLAY to store things in
                                                AMD_DISPLAY newDisplay = new AMD_DISPLAY();

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
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: The number of displays that are part of this desktop is 0, so not getting list of displays. Skipping.");
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
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Eyefinity desktop detected");
                                    // Get the eyefinity desktop

                                    // 1. Allocate a void** via SWIG
                                    SWIGTYPE_p_p_void ppVoid = ADLX.new_voidP_Ptr();

                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Getting a pointer to the Eyefinity desktop object");
                                    // 2. Call QueryInterface with the IID for IADLXEyefinityDesktop to get the interface
                                    status = desktop.QueryInterface(
                                        IADLXEyefinityDesktop.IID(),
                                        ppVoid
                                    );

                                    if (status != ADLX_RESULT.ADLX_OK)
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                                        return CreateDefaultConfig(); ;
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Converting pointer to the Eyefinity desktop object to an IntPtr");

                                        // Extract the raw IntPtr from the void** for the IADLXEyefinityDesktop
                                        IntPtr rawPtr = ADLX.voidP_Ptr_value(ppVoid);

                                        // Wrap it in the managed proxy
                                        //    (Constructor args may vary based on SWIG config)
                                        IADLXEyefinityDesktop eyefinityDesktop = new IADLXEyefinityDesktop(rawPtr, true);

                                        // Use the EyefinityDesktop object to get the Eyefinity layout
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Getting the rows and columns of the diaplay grid for the Eyefinity desktop");
                                        SWIGTYPE_p_unsigned_int pRow = ADLX.new_adlx_uintP();
                                        ADLX.adlx_uintP_assign(pRow, 0);
                                        SWIGTYPE_p_unsigned_int pCol = ADLX.new_adlx_uintP();
                                        ADLX.adlx_uintP_assign(pCol, 0);
                                        eyefinityDesktop.GridSize(pRow, pCol);
                                        myDisplayConfig.EyefinityDesktop.Rows = ADLX.adlx_uintP_value(pRow);
                                        myDisplayConfig.EyefinityDesktop.Columns = ADLX.adlx_uintP_value(pCol);

                                        /*for (uint row=1; row<gridRows; row++)
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
                                        }*/

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
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Cloned desktop detected");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Single desktop detected");
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
                desktopService.Release();

                //-----------------------------------------------------------------------

                // Get the display services
                // This lets us interact witth the various displays
                IADLXDisplayServices displayService;
                IADLXDisplayList displayList;

                SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Attempting to get the ADLX display services");
                SWIGTYPE_p_p_adlx__IADLXDisplayServices s = ADLX.new_displaySerP_Ptr();
                status = _adlxSystem.GetDisplaysServices(s);
                displayService = ADLX.displaySerP_Ptr_value(s);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Error getting the ADLX display services. systemServices.GetDisplaysServices() returned error code {status}");
                    return CreateDefaultConfig(); ;
                }
                else
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got the display services");
                    // Get the display services
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Attempting to get the ADLX display list");
                    // Get display list
                    SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                    status = displayService.GetDisplays(ppDisplayList);
                    displayList = ADLX.displayListP_Ptr_value(ppDisplayList);
                    if (status != ADLX_RESULT.ADLX_OK)
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                        return CreateDefaultConfig();
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got the display list");
                        // Iterate through the display list
                        uint it = displayList.Begin();
                        for (; it != displayList.Size(); it++)
                        {
                            SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay = ADLX.new_displayP_Ptr();
                            status = displayList.At(it, ppDisplay);
                            IADLXDisplay display = ADLX.displayP_Ptr_value(ppDisplay);

                            if (status == ADLX_RESULT.ADLX_OK)
                            {
                                // Create a new AMD_DISPLAY_WITH_SETTINGS to store things in
                                AMD_DISPLAY_WITH_SETTINGS newDisplay = new AMD_DISPLAY_WITH_SETTINGS();

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
                                    SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: Error getting the display color depth object. systemServices.GetColorDepth() returned error code {status}");
                                    //return false;
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got the display color depth object");
                                    // Check if the color depth is the same as the one we stored
                                    IADLXDisplayColorDepth colorDepth = ADLX.displayColorDepthP_Ptr_value(ppColorDepth);
                                    // Check if the color depth is supported
                                    SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                                    status = colorDepth.IsSupported(pIsSupported);
                                    if (status == ADLX_RESULT.ADLX_OK)
                                    {
                                        newDisplay.IsSupportedColorDepth = ADLX.boolP_value(pIsSupported);
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Color Depth can be set for this display");
                                        
                                        // Get the current color depth for this display
                                        SWIGTYPE_p_ADLX_COLOR_DEPTH pColorDepth = ADLX.new_adlx_colorDepthP();
                                        status = colorDepth.GetValue(pColorDepth);

                                        if (status != ADLX_RESULT.ADLX_OK)
                                        {
                                            SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: Error getting the display color depth. systemServices.GetColorDepth() returned error code {status}");
                                            //return false;
                                        }
                                        else
                                        {
                                            newDisplay.ColorDepth = ADLX.adlx_colorDepthP_value(pColorDepth);
                                            SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got the display color depth for this display: {newDisplay.ColorDepth}");
                                        }
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Color Depth is NOT supported for this display so skipping setting it");
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
                                    SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: Error getting the display custom color object. systemServices.GetCustomColor() returned error code {status}");
                                    //return false;
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got the display custom color object");
                                    IADLXDisplayCustomColor customColor = ADLX.displayCustomColorP_Ptr_value(ppCustomColor);
                                    // Check if the custom color brightness is supported
                                    SWIGTYPE_p_bool pIsBrightnessSupported = ADLX.new_boolP();
                                    status = customColor.IsBrightnessSupported(pIsBrightnessSupported);
                                    if (status == ADLX_RESULT.ADLX_OK)
                                    {
                                        newDisplay.IsSupportedCustomColorBrightness = ADLX.boolP_value(pIsBrightnessSupported);
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Custom Color Brightness can be set for this display!");
                                        // Get the current color brightness for this display
                                        SWIGTYPE_p_int pCurrentBrightness = ADLX.new_adlx_intP();
                                        status = customColor.GetBrightness(pCurrentBrightness);
                                        if (status != ADLX_RESULT.ADLX_OK)
                                        {
                                            SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: Error getting the display custom color brightness. systemServices.GetCustomColor() returned error code {status}");
                                            //return false;
                                        }
                                        else
                                        {
                                            newDisplay.CustomColorBrightness = ADLX.adlx_intP_value(pCurrentBrightness);
                                            SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got the display custom color brightness for this display: {newDisplay.CustomColorBrightness}");
                                        }
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Custom Color Brightness is NOT supported for this display.");
                                    }
                                }
                                SharedLogger.logger.Warn($"AMDLibrary/GetAMDDisplayConfig: Found the display settings for this UniqueID but it has a different name");


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


                // Now we have everything we need, so we can build the display config!
                myDisplayConfig.IsInUse = true;

                // Get the display identifiers                
                myDisplayConfig.DisplayIdentifiers = GetCurrentDisplayIdentifiers(out bool failure);

            }
            else
            {
                SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: ERROR - Tried to run GetAMDDisplayConfig but the AMD ADL library isn't initialised!");
                return CreateDefaultConfig();
            }
            
            // Return the configuration
            return myDisplayConfig;
        }


        public string PrintActiveConfig()
        {
            string stringToReturn = "";

            // Get the current config
            AMD_DISPLAY_CONFIG displayConfig = ActiveDisplayConfig;

            stringToReturn += $"****** AMD VIDEO CARDS *******\n";


            /*if (_initialised)
            {
                // Get the number of AMD adapters that the OS knows about
                int numAdapters = 0;
                ADL_STATUS ADLRet = ADLImport.ADL2_Adapter_NumberOfAdapters_Get(_adlContextHandle, out numAdapters);
                if (ADLRet == ADL_STATUS.ADL_OK)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: ADL2_Adapter_NumberOfAdapters_Get returned the number of AMD Adapters the OS knows about ({numAdapters}).");
                }
                else
                {
                    SharedLogger.logger.Error($"AMDLibrary/PrintActiveConfig: ERROR - ADL2_Adapter_NumberOfAdapters_Get returned ADL_STATUS {ADLRet} when trying to get number of AMD adapters in the computer.");
                }

                // Figure out primary adapter
                int primaryAdapterIndex = 0;
                ADLRet = ADLImport.ADL2_Adapter_Primary_Get(_adlContextHandle, out primaryAdapterIndex);
                if (ADLRet == ADL_STATUS.ADL_OK)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: The primary adapter has index {primaryAdapterIndex}.");
                }
                else
                {
                    SharedLogger.logger.Error($"AMDLibrary/PrintActiveConfig: ERROR - ADL2_Adapter_Primary_Get returned ADL_STATUS {ADLRet} when trying to get the primary adapter info from all the AMD adapters in the computer.");
                }

                // Now go through each adapter and get the information we need from it
                for (int adapterIndex = 0; adapterIndex < numAdapters; adapterIndex++)
                {
                    // Skip this adapter if it isn't active
                    int adapterActiveStatus = ADLImport.ADL_FALSE;
                    ADLRet = ADLImport.ADL2_Adapter_Active_Get(_adlContextHandle, adapterIndex, out adapterActiveStatus);
                    if (ADLRet == ADL_STATUS.ADL_OK)
                    {
                        if (adapterActiveStatus == ADLImport.ADL_TRUE)
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: ADL2_Adapter_Active_Get returned ADL_TRUE - AMD Adapter #{adapterIndex} is active! We can continue.");
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: ADL2_Adapter_Active_Get returned ADL_FALSE - AMD Adapter #{adapterIndex} is NOT active, so skipping.");
                            continue;
                        }
                    }
                    else
                    {
                        SharedLogger.logger.Warn($"AMDLibrary/PrintActiveConfig: WARNING - ADL2_Adapter_Active_Get returned ADL_STATUS {ADLRet} when trying to see if AMD Adapter #{adapterIndex} is active. Trying to skip this adapter so something at least works.");
                        continue;
                    }

                    // Get the Adapter info for this adapter and put it in the AdapterBuffer
                    SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: Running ADL2_Adapter_AdapterInfoX4_Get to get the information about AMD Adapter #{adapterIndex}.");
                    int numAdaptersInfo = 0;
                    IntPtr adapterInfoBuffer = IntPtr.Zero;
                    ADLRet = ADLImport.ADL2_Adapter_AdapterInfoX4_Get(_adlContextHandle, adapterIndex, out numAdaptersInfo, out adapterInfoBuffer);
                    if (ADLRet == ADL_STATUS.ADL_OK)
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: ADL2_Adapter_AdapterInfoX4_Get returned information about AMD Adapter #{adapterIndex}.");
                    }
                    else
                    {
                        SharedLogger.logger.Error($"AMDLibrary/PrintActiveConfig: ERROR - ADL2_Adapter_AdapterInfoX4_Get returned ADL_STATUS {ADLRet} when trying to get the adapter info from AMD Adapter #{adapterIndex}. Trying to skip this adapter so something at least works.");
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

                    SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: Converted ADL2_Adapter_AdapterInfoX4_Get memory buffer into a {adapterArray.Length} long array about AMD Adapter #{adapterIndex}.");

                    //AMD_ADAPTER_CONFIG savedAdapterConfig = new AMD_ADAPTER_CONFIG();
                    ADL_ADAPTER_INFOX2 oneAdapter = adapterArray[0];
                    if (oneAdapter.Exist != 1)
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: AMD Adapter #{oneAdapter.AdapterIndex.ToString()} doesn't exist at present so skipping detection for this adapter.");
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
                ADLRet = ADLImport.ADL2_Display_DisplayMapConfig_Get(_adlContextHandle, -1, out numDisplayMaps, out displayMapBuffer, out numDisplayTargets, out displayTargetBuffer, ADLImport.ADL_DISPLAY_DISPLAYMAP_OPTION_GPUINFO);
                if (ADLRet == ADL_STATUS.ADL_OK)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: ADL2_Display_DisplayMapConfig_Get returned information about all displaytargets connected to all AMD adapters.");

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
                        ADLRet = ADLImport.ADL2_Display_DisplayInfo_Get(_adlContextHandle, displayTarget.DisplayID.DisplayLogicalAdapterIndex, out numDisplays, out displayInfoBuffer, forceDetect);
                        if (ADLRet == ADL_STATUS.ADL_OK)
                        {
                            if (displayTarget.DisplayID.DisplayLogicalAdapterIndex == -1)
                            {
                                SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: ADL2_Display_DisplayInfo_Get returned information about all displaytargets connected to all AMD adapters.");
                                continue;
                            }
                            SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: ADL2_Display_DisplayInfo_Get returned information about all displaytargets connected to all AMD adapters.");
                        }
                        else if (ADLRet == ADL_STATUS.ADL_ERR_NULL_POINTER || ADLRet == ADL_STATUS.ADL_ERR_NOT_SUPPORTED)
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: ADL2_Display_DisplayInfo_Get returned ADL_ERR_NULL_POINTER so skipping getting display info from all AMD adapters.");
                            continue;
                        }
                        else
                        {
                            SharedLogger.logger.Error($"AMDLibrary/PrintActiveConfig: ERROR - ADL2_Display_DisplayInfo_Get returned ADL_STATUS {ADLRet} when trying to get the display target info from all AMD adapters in the computer.");
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

                            stringToReturn += $"\n****** AMD DISPLAY INFO *******\n";
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
                            ADLRet = ADLImport.ADL2_Display_DDCInfo2_Get(_adlContextHandle, displayInfoItem.DisplayID.DisplayLogicalAdapterIndex, displayInfoItem.DisplayID.DisplayLogicalIndex, out ddcInfo);
                            if (ADLRet == ADL_STATUS.ADL_OK)
                            {
                                SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: ADL2_Display_DDCInfo2_Get returned information about DDC Information for display {displayInfoItem.DisplayID.DisplayLogicalIndex} connected to AMD adapter {displayInfoItem.DisplayID.DisplayLogicalAdapterIndex}.");
                                if (ddcInfo.SupportsDDC == 1)
                                {
                                    // The display supports DDC and returned some data!
                                    SharedLogger.logger.Trace($"AMDLibrary/PrintActiveConfig: ADL2_Display_DDCInfo2_Get returned information about DDC Information for display {displayInfoItem.DisplayID.DisplayLogicalIndex} connected to AMD adapter {displayInfoItem.DisplayID.DisplayLogicalAdapterIndex}.");
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

                stringToReturn += $"\n****** AMD EYEFINITY (SLS) *******\n";
                if (displayConfig.SlsConfig.IsSlsEnabled)
                {
                    stringToReturn += $"AMD Eyefinity is Enabled\n";
                    if (displayConfig.SlsConfig.SLSMapConfigs.Count > 1)
                    {
                        stringToReturn += $"There are {displayConfig.SlsConfig.SLSMapConfigs.Count} AMD Eyefinity (SLS) configurations in use.\n";
                    }
                    if (displayConfig.SlsConfig.SLSMapConfigs.Count == 1)
                    {
                        stringToReturn += $"There is 1 AMD Eyefinity (SLS) configurations in use.\n";
                    }
                    else
                    {
                        stringToReturn += $"There are no AMD Eyefinity (SLS) configurations in use.\n";
                    }

                    int count = 0;
                    foreach (var slsMap in displayConfig.SlsConfig.SLSMapConfigs)
                    {
                        stringToReturn += $"NOTE: This Eyefinity (SLS) screen will be treated as a single display by Windows.\n";
                        stringToReturn += $"The AMD Eyefinity (SLS) Grid Topology #{count} is {slsMap.SLSMap.Grid.SLSGridColumn} Columns x {slsMap.SLSMap.Grid.SLSGridRow} Rows\n";
                        stringToReturn += $"The AMD Eyefinity (SLS) Grid Topology #{count} involves {slsMap.SLSMap.NumSLSTarget} Displays\n";
                    }

                }
                else
                {
                    stringToReturn += $"AMD Eyefinity (SLS) is Disabled\n";
                }

            }
            else
            {
                SharedLogger.logger.Error($"AMDLibrary/PrintActiveConfig: ERROR - Tried to run GetSomeDisplayIdentifiers but the AMD ADL library isn't initialised!");
                throw new AMDLibraryException($"Tried to run PrintActiveConfig but the AMD ADL library isn't initialised!");
            }*/



            stringToReturn += $"\n\n";
            // Now we also get the Windows CCD Library info, and add it to the above
            stringToReturn += WinLibrary.GetLibrary().PrintActiveConfig();

            return stringToReturn;
        }

        public bool SetActiveConfig(AMD_DISPLAY_CONFIG displayConfig, int delayInMs)
        {

            if (_initialised)
            {
                ADLX_RESULT status = ADLX_RESULT.ADLX_OK;
                // Get the desktop services
                // This is how we control the various desktops. 
                // - A single desktop is associated with one display.
                // - A duplicate desktop is associated with two or more displays.
                // - An AMD Eyefinity desktop is associated with two or more displays.
                IADLXDesktopServices desktopService;
                IADLXDesktopList desktopList;

                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Attempting to get the ADLX desktop services");
                SWIGTYPE_p_p_adlx__IADLXDesktopServices d = ADLX.new_desktopSerP_Ptr();
                status = _adlxSystem.GetDesktopsServices(d);
                desktopService = ADLX.desktopSerP_Ptr_value(d);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Error getting the ADLX desktop services. systemServices.GetDesktopsServices() returned error code {status}");
                    return false;
                }
                else
                {
                    // Get the list of Desktops we have (this is more for informational purposes)
                    SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Successfully got the desktop services");

                    // If the display config needs an Eyefinity Desktop then lets create one.
                    if (displayConfig.IsEyefinity)
                    {
                        if (displayConfig.EyefinityDesktop.Equals(ActiveDisplayConfig.EyefinityDesktop))
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Eyefinity layout is exactly the same as the one we want, so skipping setting up the Eyefinity Desktop");
                        }
                        else
                        {
                            // Setup the EyefinityDesktop using the settings the driver stores internally
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Attempting to get the ADLX EyefinityDesktop object");
                            // Get eyefinitydisplay list
                            SWIGTYPE_p_p_adlx__IADLXSimpleEyefinity ppSimpleEyefinity = ADLX.new_simpleEyefinityP_Ptr();
                            status = desktopService.GetSimpleEyefinity(ppSimpleEyefinity);
                            IADLXSimpleEyefinity simpleEyefinity = ADLX.simpleEyefinityP_Ptr_value(ppSimpleEyefinity);

                            if (status != ADLX_RESULT.ADLX_OK)
                            {
                                SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Error getting the ADLX SimpleEyefinity object. systemServices.GetSimpleEyefinity() returned error code {status}");
                                return false;
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Successfully got the ADLX SimpleEyefinity object");
                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Attempting to create the ADLX Eyefinity Desktop");
                                SWIGTYPE_p_p_adlx__IADLXEyefinityDesktop ppEyefinityDesktop = ADLX.new_eyefinityDesktopP_Ptr();
                                status = simpleEyefinity.Create(ppEyefinityDesktop);
                                IADLXEyefinityDesktop eyefinityDesktop = ADLX.eyefinityDesktopP_Ptr_value(ppEyefinityDesktop);

                                if (status != ADLX_RESULT.ADLX_OK)
                                {
                                    SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Error creating the ADLX Eyefinity Desktop. systemServices.GetSimpleEyefinity() returned error code {status}");
                                    return false;
                                }
                                else
                                {
                                    if (displayConfig.EyefinityDesktop.Equals(ActiveDisplayConfig.EyefinityDesktop))
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: This new Eyefinity layout is exactly the same as the one we want! Our job is done.");
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Warn($"AMDLibrary/SetActiveConfig: This new Eyefinity layout is different from the one we originally saved with this desktop profile. If you have changed your Eyefinity Layout then you need to update this desktop profile!.");
                                    }
                                }
                            }
                            // Release simpleEyefinity interface
                            simpleEyefinity.Release();
                        }
                    }
                    else
                    {
                        if (ActiveDisplayConfig.IsEyefinity)
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Eyefinity layout is currently in use but is NOT required, so we need to destroy the Eyefinity Desktop");

                            // Setup the EyefinityDesktop using the settings the driver stores internally
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Attempting to get the ADLX EyefinityDesktop object");
                            // Get eyefinitydisplay list
                            SWIGTYPE_p_p_adlx__IADLXSimpleEyefinity ppSimpleEyefinity = ADLX.new_simpleEyefinityP_Ptr();
                            status = desktopService.GetSimpleEyefinity(ppSimpleEyefinity);
                            IADLXSimpleEyefinity simpleEyefinity = ADLX.simpleEyefinityP_Ptr_value(ppSimpleEyefinity);

                            if (status != ADLX_RESULT.ADLX_OK)
                            {
                                SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Error getting the ADLX SimpleEyefinity object. systemServices.GetSimpleEyefinity() returned error code {status}");
                                return false;
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Successfully got the ADLX SimpleEyefinity object");
                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Attempting to destroy all the ADLX Eyefinity Desktops");
                                SWIGTYPE_p_p_adlx__IADLXEyefinityDesktop ppEyefinityDesktop = ADLX.new_eyefinityDesktopP_Ptr();
                                status = simpleEyefinity.DestroyAll();
                                if (status != ADLX_RESULT.ADLX_OK)
                                {
                                    SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Error destroying all existing ADLX Eyefinity Desktops. systemServices.GetSimpleEyefinity() returned error code {status}");
                                    return false;
                                }
                                else
                                {
                                    SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Successfully destroyed all existing ADLX Eyefinity Desktops. ");
                                }
                            }
                            // Release simpleEyefinity interface
                            simpleEyefinity.Release();

                        }
                        else
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Eyefinity layout is not currently in use and is NOT required, so leaving things as they are.");
                        }
                    }                    
                }

                // Release desktop services interface
                desktopService.Release();

            }
            else
            {
                SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ERROR - Tried to run SetActiveConfig but the AMD ADLX library isn't initialised!");
                throw new AMDLibraryException($"Tried to run SetActiveConfig but the AMD ADLX library isn't initialised!");
            }

            return true;
        }


        public bool SetActiveConfigOverride(AMD_DISPLAY_CONFIG displayConfig, int delayInMs)
        {
            if (_initialised)
            {
                ADLX_RESULT status = ADLX_RESULT.ADLX_OK;                

                // Get the display services
                // This lets us interact witth the various displays individually
                IADLXDisplayServices displayService;
                IADLXDisplayList displayList;

                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Attempting to get the ADLX display services");
                SWIGTYPE_p_p_adlx__IADLXDisplayServices s = ADLX.new_displaySerP_Ptr();
                status = _adlxSystem.GetDisplaysServices(s);
                displayService = ADLX.displaySerP_Ptr_value(s);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Error getting the ADLX display services. systemServices.GetDisplaysServices() returned error code {status}");
                    return false;
                }
                else
                {
                    SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully got the display services");
                    // Get the display services
                    SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Attempting to get the ADLX display list");
                    // Get display list
                    SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                    status = displayService.GetDisplays(ppDisplayList);
                    displayList = ADLX.displayListP_Ptr_value(ppDisplayList);
                    if (status != ADLX_RESULT.ADLX_OK)
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                        return false;
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully got the display list");
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
                                    SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Found a matching display with ID {id} in the display list");
                                    // Get the display settings we stored
                                    AMD_DISPLAY_WITH_SETTINGS displaySettingsWeStored = displayConfig.Displays[id];

                                    //------------------------------------
                                    // SET THE COLOR DEPTH IF NEEDED
                                    //------------------------------------
                                    // Get the current color depth for this display
                                    SWIGTYPE_p_p_adlx__IADLXDisplayColorDepth ppColorDepth = ADLX.new_displayColorDepthP_Ptr();
                                    status = displayService.GetColorDepth(display, ppColorDepth);
                                    if (status != ADLX_RESULT.ADLX_OK)
                                    {
                                        SharedLogger.logger.Error($"AMDLibrary/SetActiveConfigOverride: Error getting the display color depth object. systemServices.GetColorDepth() returned error code {status}");
                                        //return false;
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully got the display color depth object");
                                        // Check if the color depth is the same as the one we stored
                                        IADLXDisplayColorDepth colorDepth = ADLX.displayColorDepthP_Ptr_value(ppColorDepth);
                                        // Check if the color depth is supported
                                        SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                                        status = colorDepth.IsSupported(pIsSupported);
                                        bool colorDepthIsSupported = ADLX.boolP_value(pIsSupported);
                                        if (status == ADLX_RESULT.ADLX_OK && colorDepthIsSupported)
                                        {
                                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Color Depth can be set for this display!");
                                            // Get the current color depth for this display
                                            SWIGTYPE_p_ADLX_COLOR_DEPTH pColorDepth = ADLX.new_adlx_colorDepthP();
                                            status = colorDepth.GetValue(pColorDepth);
                                            ADLX_COLOR_DEPTH colorDepthValue = ADLX.adlx_colorDepthP_value(pColorDepth);

                                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Checking if Color Depth needs to be changed for this display");
                                            if (colorDepthValue != displaySettingsWeStored.ColorDepth)
                                            {
                                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Color Depth does need to be changed for this display so attempting to change it");
                                                // Set the color depth to the one we stored before
                                                status = colorDepth.SetValue(displaySettingsWeStored.ColorDepth);
                                                if (status != ADLX_RESULT.ADLX_OK)
                                                {
                                                    SharedLogger.logger.Error($"AMDLibrary/SetActiveConfigOverride: Error setting the display color depth. systemServices.SetColorDepth() returned error code {status}");
                                                    //return false;
                                                }
                                                else
                                                {
                                                    SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set the display color depth to {displaySettingsWeStored.ColorDepth.ToString("G")}");
                                                }
                                            }
                                            else
                                            {
                                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Color Depth does NOT need to be changed for this display as it is already set to {displaySettingsWeStored.ColorDepth.ToString("G")}");
                                            }
                                        }
                                        else
                                        {
                                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Color Depth is NOT supported for this display so skipping setting it");
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
                                        SharedLogger.logger.Error($"AMDLibrary/SetActiveConfigOverride: Error getting the display custom color object. systemServices.GetCustomColor() returned error code {status}");
                                        //return false;
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully got the display custom color object");
                                        // Check if the custom color is the same as the one we stored
                                        IADLXDisplayCustomColor customColor = ADLX.displayCustomColorP_Ptr_value(ppCustomColor);
                                        // Check if the custom color brightness is supported
                                        SWIGTYPE_p_bool pIsBrightnessSupported = ADLX.new_boolP();
                                        status = customColor.IsBrightnessSupported(pIsBrightnessSupported);
                                        bool brightnessIsSupported = ADLX.boolP_value(pIsBrightnessSupported);
                                        if (status == ADLX_RESULT.ADLX_OK && brightnessIsSupported)
                                        {
                                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Brightness can be set for this display!");
                                            // Get the current color brightness for this display
                                            SWIGTYPE_p_int pCurrentBrightness = ADLX.new_adlx_intP();
                                            status = customColor.GetBrightness(pCurrentBrightness);
                                            int currentBrightnessValue = ADLX.adlx_intP_value(pCurrentBrightness);

                                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Checking if Custom Color Brightness needs to be changed for this display");
                                            if (currentBrightnessValue != displaySettingsWeStored.CustomColorBrightness)
                                            {
                                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Brightness does need to be changed for this display so attempting to change it");
                                                // Set the color depth to the one we stored before
                                                status = customColor.SetBrightness(displaySettingsWeStored.CustomColorBrightness);
                                                if (status != ADLX_RESULT.ADLX_OK)
                                                {
                                                    SharedLogger.logger.Error($"AMDLibrary/SetActiveConfigOverride: Error setting the display Custom Color Brightness. systemServices.CustomColorBrightness() returned error code {status}");
                                                    //return false;
                                                }
                                                else
                                                {
                                                    SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set the display Custom Color Brightness to {displaySettingsWeStored.CustomColorBrightness.ToString("G")}");
                                                }
                                            }
                                            else
                                            {
                                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Brightness does NOT need to be changed for this display as it is already set to {displaySettingsWeStored.CustomColorBrightness.ToString("G")}");
                                            }
                                        }
                                        else
                                        {
                                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Brightness is NOT supported for this display.");
                                        }
                                    }
                                    SharedLogger.logger.Warn($"AMDLibrary/SetActiveConfigOverride: Found the display settings for this UniqueID but it has a different name");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: No display with UniqueID {id} found in the stored settings, so skipping.");
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
                displayService.Release();

            }
            else
            {
                SharedLogger.logger.Error($"AMDLibrary/SetActiveConfigOverride: ERROR - Tried to run SetActiveConfig but the AMD ADLX library isn't initialised!");
                throw new AMDLibraryException($"Tried to run SetActiveConfig but the AMD ADLX library isn't initialised!");
            }

            return true;
        }



        public bool IsActiveConfig(AMD_DISPLAY_CONFIG displayConfig)
        {

            // Check whether the display config is in use now
            SharedLogger.logger.Trace($"AMDLibrary/IsActiveConfig: Checking whether the display configuration is already being used.");
            if (displayConfig.Equals(_activeDisplayConfig))
            {
                SharedLogger.logger.Trace($"AMDLibrary/IsActiveConfig: The display configuration is already being used (supplied displayConfig Equals currentWindowsDisplayConfig)");
                return true;
            }
            else
            {
                SharedLogger.logger.Trace($"AMDLibrary/IsActiveConfig: The display configuration is NOT currently in use (supplied displayConfig Equals currentWindowsDisplayConfig)");
                return false;
            }

        }

        public bool IsValidConfig(AMD_DISPLAY_CONFIG displayConfig)
        {
            // We want to check the AMD Eyefinity (SLS) config is valid
            SharedLogger.logger.Trace($"AMDLibrary/IsValidConfig: Testing whether the display configuration is valid");
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

        public bool IsPossibleConfig(AMD_DISPLAY_CONFIG displayConfig)
        {
            // We want to check the AMD profile can be used now
            SharedLogger.logger.Trace($"AMDLibrary/IsPossibleConfig: Testing whether the AMD display configuration is possible to be used now");

            // If both display identifiers are 0 then no displays were connected via AMD and we should just return true.
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
                SharedLogger.logger.Trace($"AMDLibrary/IsPossibleConfig: Success! The AMD display configuration is possible to be used now");
                return true;
            }
            else
            {
                SharedLogger.logger.Trace($"AMDLibrary/IsPossibleConfig: Uh oh! The AMDdisplay configuration is possible cannot be used now");
                return false;
            }
        }

        public List<string> GetCurrentDisplayIdentifiers(out bool failure)
        {
            SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Getting the current display identifiers for the displays in use now");

            List<string> displayIdentifiers = new List<string>();
            failure = false;

            if (_initialised)
            {
                ADLX_RESULT status = ADLX_RESULT.ADLX_OK;

                // Get the desktop services
                // This is how we get and iterate through the various desktops. 
                // - A single desktop is associated with one display.
                // - A duplicate desktop is associated with two or more displays.
                // - An AMD Eyefinity desktop is associated with two or more displays.
                IADLXDesktopServices desktopService;
                IADLXDesktopList desktopList;

                SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Attempting to get the ADLX desktop services");
                SWIGTYPE_p_p_adlx__IADLXDesktopServices d = ADLX.new_desktopSerP_Ptr();
                status = _adlxSystem.GetDesktopsServices(d);
                desktopService = ADLX.desktopSerP_Ptr_value(d);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Error getting the ADLX desktop services. systemServices.GetDesktopsServices() returned error code {status}");
                    failure = true;
                }
                else
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Successfully got the desktop services");
                    // Get the display services
                    SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Attempting to get the ADLX desktop list");
                    // Get display list
                    SWIGTYPE_p_p_adlx__IADLXDesktopList ppDesktopList = ADLX.new_desktopListP_Ptr();
                    status = desktopService.GetDesktops(ppDesktopList);
                    desktopList = ADLX.desktopListP_Ptr_value(ppDesktopList);

                    if (status != ADLX_RESULT.ADLX_OK)
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                        failure = true;
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Successfully got the desktop list");
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
                                    SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Error getting the ADLX display list for this desktop. desktop.GetDisplays() returned error code {status}");
                                    failure = true;
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Successfully got the display list for this desktop");
                                    // Iterate through the display list
                                    uint displayIt = desktopDisplayList.Begin();
                                    for (; displayIt != desktopDisplayList.Size(); displayIt++)
                                    {
                                        SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay = ADLX.new_displayP_Ptr();
                                        status = desktopDisplayList.At(displayIt, ppDisplay);
                                        IADLXDisplay display = ADLX.displayP_Ptr_value(ppDisplay);
                                        if (status != ADLX_RESULT.ADLX_OK)
                                        {
                                            SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Error getting the ADLX display name. desktop.GetDisplays() returned error code {status}");
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
                                            displayInfo.Add("AMDADLX");
                                            try
                                            {
                                                displayInfo.Add(gpuName);
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting GPU Name from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(gpuUniqueId.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting GPU Unique ID from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(gpuIsExternal.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting GPU Is External from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(connect.ToString("G"));
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Connection Type for the display from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(name);
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Name for the display from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(disType.ToString("G"));
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Display Type for the display from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(mid.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Manufacturer for the display from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            try
                                            {
                                                displayInfo.Add(uniqueId.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Display Unique ID for the display from video card. Substituting with a # instead");
                                                displayInfo.Add("#");
                                            }
                                            // Create a display identifier out of it
                                            string displayIdentifier = String.Join("|", displayInfo);
                                            // Add it to the list of display identifiers so we can return it
                                            // but only add it if it doesn't already exist. Otherwise we get duplicates :/
                                            if (!displayIdentifiers.Contains(displayIdentifier))
                                            {
                                                displayIdentifiers.Add(displayIdentifier);
                                                SharedLogger.logger.Debug($"AMDLibrary/GetCurrentDisplayIdentifiers: DisplayIdentifier detected: {displayIdentifier}");
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
                desktopService.Release();

            }
            else
            {
                SharedLogger.logger.Error($"AMDLibrary/GetSomeDisplayIdentifiers: ERROR - Tried to get Displays but the AMD ADLX library isn't initialised!");
                throw new AMDLibraryException($"Tried to get Displays but the AMD ADLX library isn't initialised!");
            }

            // Sort the display identifiers
            displayIdentifiers.Sort();

            return displayIdentifiers;
        }

        public List<string> GetAllConnectedDisplayIdentifiers(out bool failure)
        {
            SharedLogger.logger.Trace($"AMDLibrary/GetAllConnectedDisplayIdentifiers: Getting all the display identifiers that can possibly be used");

            List<string> displayIdentifiers = new List<string>();
            failure = false;

            if (_initialised)
            {
                ADLX_RESULT status = ADLX_RESULT.ADLX_OK;

                // Get the display services
                // This lets us interact witth the various displays
                IADLXDisplayServices displayService;
                IADLXDisplayList displayList;

                SharedLogger.logger.Trace($"AMDLibrary/GetAllConnectedDisplayIdentifiers: Attempting to get the ADLX display services");
                SWIGTYPE_p_p_adlx__IADLXDisplayServices s = ADLX.new_displaySerP_Ptr();
                status = _adlxSystem.GetDisplaysServices(s);
                displayService = ADLX.displaySerP_Ptr_value(s);
                if (status != ADLX_RESULT.ADLX_OK)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetAllConnectedDisplayIdentifiers: Error getting the ADLX display services. systemServices.GetDisplaysServices() returned error code {status}");
                    failure = true;
                }
                else
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetAllConnectedDisplayIdentifiers: Successfully got the display services");
                    // Get the display services
                    SharedLogger.logger.Trace($"AMDLibrary/GetAllConnectedDisplayIdentifiers: Attempting to get the ADLX display list");
                    // Get display list
                    SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                    status = displayService.GetDisplays(ppDisplayList);
                    displayList = ADLX.displayListP_Ptr_value(ppDisplayList);
                    if (status != ADLX_RESULT.ADLX_OK)
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/GetAllConnectedDisplayIdentifiers: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                        failure = true;
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/GetAllConnectedDisplayIdentifiers: Successfully got the display list");
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

                                /*SWIGTYPE_p_p_char ppGpuVendorId = ADLX.new_charP_Ptr();
                                gpu.VendorId(ppGpuVendorId);
                                string gpuVendorId = ADLX.charP_Ptr_value(ppGpuVendorId);*/

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

                                /*SWIGTYPE_p_p_char ppEDIE = ADLX.new_charP_Ptr();
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
                                ADLX_DISPLAY_SCAN_TYPE scanType = ADLX.disScanTypeP_value(pScanType);*/

                                SWIGTYPE_p_size_t pID = ADLX.new_adlx_sizeP();
                                display.UniqueId(pID);
                                uint uniqueId = ADLX.adlx_sizeP_value(pID);

/*                                Console.WriteLine(String.Format("\nThe display [{0}]:", it));
                                Console.WriteLine(String.Format("\tName: {0}", name));
                                Console.WriteLine(String.Format("\tType: {0}", disType));
                                Console.WriteLine(String.Format("\tConnector type: {0}", connect));
                                Console.WriteLine(String.Format("\tManufacturer id: {0}", mid));*/
                                //Console.WriteLine(String.Format("\tEDID: {0}", edid));
                                /*Console.WriteLine(String.Format("\tResolution:  h: {0}  v: {1}", h, v));
                                Console.WriteLine(String.Format("\tRefresh rate: {0}", refRate));
                                Console.WriteLine(String.Format("\tPixel clock: {0}", pixClock));
                                Console.WriteLine(String.Format("\tScan type: {0}", scanType));
                                Console.WriteLine(String.Format("\tUnique id: {0}", id));*/

                                // Create an array of all the important display info we need to record
                                List<string> displayInfo = new List<string>();
                                displayInfo.Add("AMDADLX");
                                try
                                {
                                    displayInfo.Add(gpuName);
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting GPU Name from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(gpuUniqueId.ToString());
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting GPU Unique ID from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(gpuIsExternal.ToString());
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting GPU Is External from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(connect.ToString("G"));
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Connection Type for the display from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(name);
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Name for the display from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(disType.ToString("G"));
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Display Type for the display from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(mid.ToString());
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Manufacturer for the display from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                try
                                {
                                    displayInfo.Add(uniqueId.ToString());
                                }
                                catch (Exception ex)
                                {
                                    SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Display Unique ID for the display from video card. Substituting with a # instead");
                                    displayInfo.Add("#");
                                }
                                // Create a display identifier out of it
                                string displayIdentifier = String.Join("|", displayInfo);
                                // Add it to the list of display identifiers so we can return it
                                // but only add it if it doesn't already exist. Otherwise we get duplicates :/
                                if (!displayIdentifiers.Contains(displayIdentifier))
                                {
                                    displayIdentifiers.Add(displayIdentifier);
                                    SharedLogger.logger.Debug($"AMDLibrary/GetAllConnectedDisplayIdentifiers: DisplayIdentifier detected: {displayIdentifier}");
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
                displayService.Release();

            }
            else
            {
                SharedLogger.logger.Error($"AMDLibrary/GetSomeDisplayIdentifiers: ERROR - Tried to get Displays but the AMD ADLX library isn't initialised!");
                throw new AMDLibraryException($"Tried to get Displays but the AMD ADLX library isn't initialised!");
            }

            // Sort the display identifiers
            displayIdentifiers.Sort();

            return displayIdentifiers;
        }


    }

    [global::System.Serializable]
    public class AMDLibraryException : Exception
    {
        public AMDLibraryException() { }
        public AMDLibraryException(string message) : base(message) { }
        public AMDLibraryException(string message, Exception inner) : base(message, inner) { }
    }
}