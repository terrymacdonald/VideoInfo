using ADLXWrapper;
using DisplayMagicianShared.Windows;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DisplayMagicianShared.AMD
{
    #region Data Transfer Objects
    [StructLayout(LayoutKind.Sequential)]
    public struct AMD_SLSMAP_CONFIG : IEquatable<AMD_SLSMAP_CONFIG>
    {
        public ADL_SLS_MAP SLSMap;
        public List<ADL_SLS_TARGET> SLSTargets;
        public List<ADL_SLS_MODE> NativeModes;
        public List<ADL_SLS_OFFSET> NativeModeOffsets;
        public List<ADL_BEZEL_TRANSIENT_MODE> BezelModes;
        public List<ADL_BEZEL_TRANSIENT_MODE> TransientModes;
        public List<ADL_SLS_OFFSET> SLSOffsets;
        public int BezelModePercent;

        public AMD_SLSMAP_CONFIG()
        {
            SLSMap = new ADL_SLS_MAP();
            SLSTargets = new List<ADL_SLS_TARGET>();
            NativeModes = new List<ADL_SLS_MODE>();
            NativeModeOffsets = new List<ADL_SLS_OFFSET>();
            BezelModes = new List<ADL_BEZEL_TRANSIENT_MODE>();
            TransientModes = new List<ADL_BEZEL_TRANSIENT_MODE>();
            SLSOffsets = new List<ADL_SLS_OFFSET>();
            BezelModePercent = 0;
        }

        public override bool Equals(object obj) => obj is AMD_SLSMAP_CONFIG other && Equals(other);
        public bool Equals(AMD_SLSMAP_CONFIG other) =>
            SLSMap.Equals(other.SLSMap) &&
            SLSTargets.SequenceEqual(other.SLSTargets) &&
            NativeModes.SequenceEqual(other.NativeModes) &&
            NativeModeOffsets.SequenceEqual(other.NativeModeOffsets) &&
            BezelModes.SequenceEqual(other.BezelModes) &&
            TransientModes.SequenceEqual(other.TransientModes) &&
            SLSOffsets.SequenceEqual(other.SLSOffsets) &&
            BezelModePercent == other.BezelModePercent;
        public override int GetHashCode() => (SLSMap, SLSTargets, NativeModes, NativeModeOffsets, BezelModes, TransientModes, SLSOffsets, BezelModePercent).GetHashCode();
        public static bool operator ==(AMD_SLSMAP_CONFIG lhs, AMD_SLSMAP_CONFIG rhs) => lhs.Equals(rhs);
        public static bool operator !=(AMD_SLSMAP_CONFIG lhs, AMD_SLSMAP_CONFIG rhs) => !lhs.Equals(rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AMD_SLS_CONFIG : IEquatable<AMD_SLS_CONFIG>
    {
        public bool IsSlsEnabled;
        public List<AMD_SLSMAP_CONFIG> SLSMapConfigs;
        public List<ADL_MODE> SLSEnabledDisplayTargets;

        public AMD_SLS_CONFIG()
        {
            IsSlsEnabled = false;
            SLSMapConfigs = new List<AMD_SLSMAP_CONFIG>();
            SLSEnabledDisplayTargets = new List<ADL_MODE>();
        }

        public override bool Equals(object obj) => obj is AMD_SLS_CONFIG other && Equals(other);
        public bool Equals(AMD_SLS_CONFIG other) =>
            IsSlsEnabled == other.IsSlsEnabled &&
            SLSMapConfigs.SequenceEqual(other.SLSMapConfigs) &&
            SLSEnabledDisplayTargets.SequenceEqual(other.SLSEnabledDisplayTargets);
        public override int GetHashCode() => (IsSlsEnabled, SLSMapConfigs, SLSEnabledDisplayTargets).GetHashCode();
        public static bool operator ==(AMD_SLS_CONFIG lhs, AMD_SLS_CONFIG rhs) => lhs.Equals(rhs);
        public static bool operator !=(AMD_SLS_CONFIG lhs, AMD_SLS_CONFIG rhs) => !lhs.Equals(rhs);
    }

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

        public override bool Equals(object obj) => obj is AMD_DESKTOP other && Equals(other);
        public bool Equals(AMD_DESKTOP other) =>
            NumberOfDisplays == other.NumberOfDisplays &&
            Displays.SequenceEqual(other.Displays) &&
            Orientation == other.Orientation &&
            SizeWidth == other.SizeWidth &&
            SizeHeight == other.SizeHeight &&
            TopLeftX == other.TopLeftX &&
            TopLeftY == other.TopLeftY &&
            Type == other.Type;
        public override int GetHashCode() => (NumberOfDisplays, Displays, Orientation, SizeWidth, SizeHeight, TopLeftX, TopLeftY, Type).GetHashCode();
        public static bool operator ==(AMD_DESKTOP lhs, AMD_DESKTOP rhs) => lhs.Equals(rhs);
        public static bool operator !=(AMD_DESKTOP lhs, AMD_DESKTOP rhs) => !lhs.Equals(rhs);
    }

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

        public AMD_EYEFINITY_DESKTOP()
        {
            Orientation = ADLX_ORIENTATION.ORIENTATION_LANDSCAPE;
            Rows = 0;
            Columns = 0;
            SizeWidth = 0;
            SizeHeight = 0;
            TopLeftX = 0;
            TopLeftY = 0;
        }

        public override bool Equals(object obj) => obj is AMD_EYEFINITY_DESKTOP other && Equals(other);
        public bool Equals(AMD_EYEFINITY_DESKTOP other) =>
            Rows == other.Rows &&
            Columns == other.Columns &&
            Orientation == other.Orientation &&
            SizeWidth == other.SizeWidth &&
            SizeHeight == other.SizeHeight &&
            TopLeftX == other.TopLeftX &&
            TopLeftY == other.TopLeftY;
        public override int GetHashCode() => (Rows, Columns, Orientation, SizeWidth, SizeHeight, TopLeftX, TopLeftY).GetHashCode();
        public static bool operator ==(AMD_EYEFINITY_DESKTOP lhs, AMD_EYEFINITY_DESKTOP rhs) => lhs.Equals(rhs);
        public static bool operator !=(AMD_EYEFINITY_DESKTOP lhs, AMD_EYEFINITY_DESKTOP rhs) => !lhs.Equals(rhs);
    }

    public struct Display3DLUTSettings
    {
        public bool IsSupportedSCE { get; set; }
        public bool IsSupportedSCEVividGaming { get; set; }
        public bool IsSupportedSCEDynamicContrast { get; set; }
        public bool IsSupportedUser3DLUT { get; set; }

        public bool IsCurrentSCEDisabled { get; set; }
        public bool IsCurrentSCEVividGaming { get; set; }
        public bool IsCurrentSCEDynamicContrast { get; set; }
        public bool IsUser3DLUTApplied { get; set; }

        public int CurrentDynamicContrastValue { get; set; }
        public IntRange DynamicContrastRange { get; set; }
        public List<ushort> UserLUTData { get; set; }
        public int UserLUTNumPoints { get; set; }
        public LutMode UserLUTMode { get; set; }

        public Display3DLUTSettings()
        {
            CurrentDynamicContrastValue = 0;
            DynamicContrastRange = default;
            UserLUTData = new List<ushort>();
            UserLUTNumPoints = 0;
            UserLUTMode = LutMode.None;
            IsSupportedSCE = false;
            IsSupportedSCEVividGaming = false;
            IsSupportedSCEDynamicContrast = false;
            IsSupportedUser3DLUT = false;
            IsCurrentSCEDisabled = false;
            IsCurrentSCEVividGaming = false;
            IsCurrentSCEDynamicContrast = false;
            IsUser3DLUTApplied = false;
        }
    }

    public struct IntRange
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Step { get; set; }
    }

    public enum LutMode
    {
        None = 0,
        SDR = 1,
        HDR = 2,
        All = 3
    }

    public class CustomDisplayResolutionInfo
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int RefreshRate { get; set; }
        public int TimingStandard { get; set; }
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
        public bool IsSupportedCustomColorHue;
        public int CustomColorHue;
        public bool IsSupportedCustomColorSaturation;
        public int CustomColorSaturation;
        public bool IsSupportedCustomColorContrast;
        public int CustomColorContrast;
        public bool IsSupportedCustomColorTemperature;
        public int CustomColorTemperature;
        public bool IsSupportedGamma;
        public int GammaCoefficientGamma;
        public int GammaCoefficientA0;
        public int GammaCoefficientA1;
        public int GammaCoefficientA2;
        public int GammaCoefficientA3;
        public List<double> GammaRampRed;
        public List<double> GammaRampGreen;
        public List<double> GammaRampBlue;
        public bool IsSupportedFreeSync;
        public bool IsEnabledFreeSync;
        public int GamutColorSpaceRedX;
        public int GamutColorSpaceRedY;
        public int GamutColorSpaceGreenX;
        public int GamutColorSpaceGreenY;
        public int GamutColorSpaceBlueX;
        public int GamutColorSpaceBlueY;
        public int GamutWhitePointX;
        public int GamutWhitePointY;
        public bool IsSupportedGPUScaling;
        public bool IsEnabledGPUScaling;
        public bool IsSupportedIntegerScaling;
        public bool IsEnabledIntegerScaling;
        public bool IsSupportedPixelFormat;
        public ADLX_PIXEL_FORMAT CurrentPixelFormat;
        public bool IsSupportedScalingMode;
        public ADLX_SCALE_MODE CurrentScalingMode;
        public bool IsSupportedVSR;
        public bool IsEnabledVSR;
        public bool IsSupportedHDCP;
        public bool IsEnabledHDCP;
        public bool IsSupportedVariBright;
        public bool IsEnabledVariBright;
        public bool IsSupported3DLUT;
        public Display3DLUTSettings ThreeDLUTSettings { get; set; }
        public bool IsSupportedCustomResolution { get; set; }
        public bool IsCustomResolutionApplied { get; set; }
        public int CustomResWidth { get; set; }
        public int CustomResHeight { get; set; }
        public int CustomResRefreshRate { get; set; }
        public int CustomResTimingStandard { get; set; }
        public List<CustomDisplayResolutionInfo> CustomResolutions { get; set; }

        public AMD_DISPLAY_WITH_SETTINGS()
        {
            EDID = string.Empty;
            Name = string.Empty;
            ConnectorType = ADLX_DISPLAY_CONNECTOR_TYPE.DISPLAY_CONTYPE_UNKNOWN;
            DisplayType = ADLX_DISPLAY_TYPE.DISPLAY_TYPE_UNKOWN;
            ScanType = ADLX_DISPLAY_SCAN_TYPE.PROGRESSIVE;
            ColorDepth = ADLX_COLOR_DEPTH.BPC_UNKNOWN;
            CurrentPixelFormat = ADLX_PIXEL_FORMAT.FORMAT_UNKNOWN;
            CurrentScalingMode = ADLX_SCALE_MODE.PRESERVE_ASPECT_RATIO;
            GammaRampRed = new List<double>();
            GammaRampGreen = new List<double>();
            GammaRampBlue = new List<double>();
            ThreeDLUTSettings = new Display3DLUTSettings();
            CustomResolutions = new List<CustomDisplayResolutionInfo>();
        }

        public override bool Equals(object obj) => obj is AMD_DISPLAY_WITH_SETTINGS other && Equals(other);
        public bool Equals(AMD_DISPLAY_WITH_SETTINGS other) =>
            ConnectorType == other.ConnectorType &&
            DisplayType == other.DisplayType &&
            EDID == other.EDID &&
            ManufacturerID == other.ManufacturerID &&
            Name == other.Name &&
            MaxHResolution == other.MaxHResolution &&
            MaxVResolution == other.MaxVResolution &&
            PixelClock == other.PixelClock &&
            RefreshRate.Equals(other.RefreshRate) &&
            ScanType == other.ScanType &&
            UniqueID == other.UniqueID &&
            IsSupportedColorDepth == other.IsSupportedColorDepth &&
            ColorDepth == other.ColorDepth &&
            IsSupportedCustomColorBrightness == other.IsSupportedCustomColorBrightness &&
            CustomColorBrightness == other.CustomColorBrightness &&
            IsSupportedCustomColorHue == other.IsSupportedCustomColorHue &&
            CustomColorHue == other.CustomColorHue &&
            IsSupportedCustomColorSaturation == other.IsSupportedCustomColorSaturation &&
            CustomColorSaturation == other.CustomColorSaturation &&
            IsSupportedCustomColorContrast == other.IsSupportedCustomColorContrast &&
            CustomColorContrast == other.CustomColorContrast &&
            IsSupportedCustomColorTemperature == other.IsSupportedCustomColorTemperature &&
            CustomColorTemperature == other.CustomColorTemperature &&
            IsSupportedGamma == other.IsSupportedGamma &&
            GammaCoefficientGamma == other.GammaCoefficientGamma &&
            GammaCoefficientA0 == other.GammaCoefficientA0 &&
            GammaCoefficientA1 == other.GammaCoefficientA1 &&
            GammaCoefficientA2 == other.GammaCoefficientA2 &&
            GammaCoefficientA3 == other.GammaCoefficientA3 &&
            GammaRampRed.SequenceEqual(other.GammaRampRed) &&
            GammaRampGreen.SequenceEqual(other.GammaRampGreen) &&
            GammaRampBlue.SequenceEqual(other.GammaRampBlue) &&
            IsSupportedFreeSync == other.IsSupportedFreeSync &&
            IsEnabledFreeSync == other.IsEnabledFreeSync &&
            GamutColorSpaceRedX == other.GamutColorSpaceRedX &&
            GamutColorSpaceRedY == other.GamutColorSpaceRedY &&
            GamutColorSpaceGreenX == other.GamutColorSpaceGreenX &&
            GamutColorSpaceGreenY == other.GamutColorSpaceGreenY &&
            GamutColorSpaceBlueX == other.GamutColorSpaceBlueX &&
            GamutColorSpaceBlueY == other.GamutColorSpaceBlueY &&
            GamutWhitePointX == other.GamutWhitePointX &&
            GamutWhitePointY == other.GamutWhitePointY &&
            IsSupportedGPUScaling == other.IsSupportedGPUScaling &&
            IsEnabledGPUScaling == other.IsEnabledGPUScaling &&
            IsSupportedIntegerScaling == other.IsSupportedIntegerScaling &&
            IsEnabledIntegerScaling == other.IsEnabledIntegerScaling &&
            IsSupportedPixelFormat == other.IsSupportedPixelFormat &&
            CurrentPixelFormat == other.CurrentPixelFormat &&
            IsSupportedScalingMode == other.IsSupportedScalingMode &&
            CurrentScalingMode == other.CurrentScalingMode &&
            IsSupportedVSR == other.IsSupportedVSR &&
            IsEnabledVSR == other.IsEnabledVSR &&
            IsSupportedHDCP == other.IsSupportedHDCP &&
            IsEnabledHDCP == other.IsEnabledHDCP &&
            IsSupportedVariBright == other.IsSupportedVariBright &&
            IsEnabledVariBright == other.IsEnabledVariBright &&
            IsSupported3DLUT == other.IsSupported3DLUT &&
            IsSupportedCustomResolution == other.IsSupportedCustomResolution &&
            IsCustomResolutionApplied == other.IsCustomResolutionApplied &&
            CustomResWidth == other.CustomResWidth &&
            CustomResHeight == other.CustomResHeight &&
            CustomResRefreshRate == other.CustomResRefreshRate &&
            CustomResTimingStandard == other.CustomResTimingStandard &&
            CustomResolutions.SequenceEqual(other.CustomResolutions);

        public override int GetHashCode() =>
            (ConnectorType, DisplayType, EDID, ManufacturerID, Name, MaxHResolution, MaxVResolution, PixelClock, RefreshRate, ScanType, UniqueID,
             IsSupportedColorDepth, ColorDepth, IsSupportedCustomColorBrightness, CustomColorBrightness, IsSupportedCustomColorHue, CustomColorHue,
             IsSupportedCustomColorSaturation, CustomColorSaturation, IsSupportedCustomColorContrast, CustomColorContrast, IsSupportedCustomColorTemperature,
             CustomColorTemperature, IsSupportedGamma, GammaCoefficientGamma, GammaCoefficientA0, GammaCoefficientA1, GammaCoefficientA2, GammaCoefficientA3,
             GammaRampRed, GammaRampGreen, GammaRampBlue, IsSupportedFreeSync, IsEnabledFreeSync, GamutColorSpaceRedX, GamutColorSpaceRedY,
             GamutColorSpaceGreenX, GamutColorSpaceGreenY, GamutColorSpaceBlueX, GamutColorSpaceBlueY, GamutWhitePointX, GamutWhitePointY,
             IsSupportedGPUScaling, IsEnabledGPUScaling, IsSupportedIntegerScaling, IsEnabledIntegerScaling, IsSupportedPixelFormat, CurrentPixelFormat,
             IsSupportedScalingMode, CurrentScalingMode, IsSupportedVSR, IsEnabledVSR, IsSupportedHDCP, IsEnabledHDCP, IsSupportedVariBright, IsEnabledVariBright,
             IsSupported3DLUT, IsSupportedCustomResolution, IsCustomResolutionApplied, CustomResWidth, CustomResHeight, CustomResRefreshRate, CustomResTimingStandard,
             CustomResolutions).GetHashCode();

        public static bool operator ==(AMD_DISPLAY_WITH_SETTINGS lhs, AMD_DISPLAY_WITH_SETTINGS rhs) => lhs.Equals(rhs);
        public static bool operator !=(AMD_DISPLAY_WITH_SETTINGS lhs, AMD_DISPLAY_WITH_SETTINGS rhs) => !lhs.Equals(rhs);
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
            EDID = string.Empty;
            Name = string.Empty;
            ScanType = ADLX_DISPLAY_SCAN_TYPE.PROGRESSIVE;
            ConnectorType = ADLX_DISPLAY_CONNECTOR_TYPE.DISPLAY_CONTYPE_UNKNOWN;
            DisplayType = ADLX_DISPLAY_TYPE.DISPLAY_TYPE_UNKOWN;
        }

        public override bool Equals(object obj) => obj is AMD_DISPLAY other && Equals(other);
        public bool Equals(AMD_DISPLAY other) =>
            ConnectorType == other.ConnectorType &&
            DisplayType == other.DisplayType &&
            EDID == other.EDID &&
            ManufacturerID == other.ManufacturerID &&
            Name == other.Name &&
            MaxHResolution == other.MaxHResolution &&
            MaxVResolution == other.MaxVResolution &&
            PixelClock == other.PixelClock &&
            RefreshRate.Equals(other.RefreshRate) &&
            ScanType == other.ScanType &&
            UniqueID == other.UniqueID;

        public override int GetHashCode() =>
            (ConnectorType, DisplayType, EDID, ManufacturerID, Name, MaxHResolution, MaxVResolution, PixelClock, RefreshRate, ScanType, UniqueID).GetHashCode();

        public static bool operator ==(AMD_DISPLAY lhs, AMD_DISPLAY rhs) => lhs.Equals(rhs);
        public static bool operator !=(AMD_DISPLAY lhs, AMD_DISPLAY rhs) => !lhs.Equals(rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AMD_DISPLAY_CONFIG : IEquatable<AMD_DISPLAY_CONFIG>
    {
        public bool IsInUse;
        public bool IsCloned;
        public bool IsEyefinity;
        public List<AMD_DESKTOP> Desktops;
        public AMD_EYEFINITY_DESKTOP EyefinityDesktop;
        public Dictionary<long, AMD_DISPLAY_WITH_SETTINGS> Displays;
        public AMD_SLS_CONFIG Adl2SlsConfig;
        public List<string> DisplayIdentifiers;

        public AMD_DISPLAY_CONFIG()
        {
            IsInUse = false;
            IsCloned = false;
            IsEyefinity = false;
            Desktops = new List<AMD_DESKTOP>();
            EyefinityDesktop = new AMD_EYEFINITY_DESKTOP();
            Displays = new Dictionary<long, AMD_DISPLAY_WITH_SETTINGS>();
            Adl2SlsConfig = new AMD_SLS_CONFIG();
            DisplayIdentifiers = new List<string>();
        }

        public override bool Equals(object obj) => obj is AMD_DISPLAY_CONFIG other && Equals(other);
        public bool Equals(AMD_DISPLAY_CONFIG other) =>
            IsInUse == other.IsInUse &&
            IsCloned == other.IsCloned &&
            IsEyefinity == other.IsEyefinity &&
            Desktops.SequenceEqual(other.Desktops) &&
            EyefinityDesktop.Equals(other.EyefinityDesktop) &&
            Displays.SequenceEqual(other.Displays) &&
            Adl2SlsConfig.Equals(other.Adl2SlsConfig) &&
            DisplayIdentifiers.SequenceEqual(other.DisplayIdentifiers);

        public override int GetHashCode() =>
            (IsInUse, IsCloned, IsEyefinity, Desktops, EyefinityDesktop, Displays, Adl2SlsConfig, DisplayIdentifiers).GetHashCode();

        public static bool operator ==(AMD_DISPLAY_CONFIG lhs, AMD_DISPLAY_CONFIG rhs) => lhs.Equals(rhs);
        public static bool operator !=(AMD_DISPLAY_CONFIG lhs, AMD_DISPLAY_CONFIG rhs) => !lhs.Equals(rhs);
    }
    #endregion

    public class AMDLibraryException : Exception
    {
        public AMDLibraryException() { }
        public AMDLibraryException(string message) : base(message) { }
        public AMDLibraryException(string message, Exception inner) : base(message, inner) { }
    }

    public class AMDLibrary : IDisposable
    {
        private static AMDLibrary _instance = new AMDLibrary();

        private bool _initialised = false;
        private bool _initialisedADL2 = false;
        private bool _disposed = false;

        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        private IntPtr _adlContextHandle = IntPtr.Zero;
        private ADLXApi _adlxApi;
        private AdlxInterfaceHandle _adlxSystemHandle;
        private AMD_DISPLAY_CONFIG? _activeDisplayConfig;
        public List<ADL_DISPLAY_CONNECTION_TYPE> SkippedColorConnectionTypes;
        public List<string> _allConnectedDisplayIdentifiers;
        public IntPtr hADLXBindingModule = IntPtr.Zero;
        public IntPtr hADLXModule = IntPtr.Zero;
        public const string AMD_ADLX_BINDING_DLL = "ADLXWrapper.dll";
        public const string AMD_ADLX_DLL = "amdadlx64.dll";

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        public AMDLibrary()
        {
            SkippedColorConnectionTypes = new List<ADL_DISPLAY_CONNECTION_TYPE>
            {
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
                _initialised = false;
                if (!WinLibrary.IsPCIVideoCardVendorInstalled(PCIVendorIDs))
                {
                    SharedLogger.logger.Trace("AMDLibrary/AMDLibrary: No AMD hardware detected");
                    return;
                }

                try
                {
                    SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Attempting to load {AMD_ADLX_DLL}");
                    hADLXModule = LoadLibrary(AMD_ADLX_DLL);
                    if (hADLXModule == IntPtr.Zero)
                    {
                        SharedLogger.logger.Error($"AMDLibrary/AMDLibrary: Failed to load {AMD_ADLX_DLL}");
                        return;
                    }

                    SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Attempting to load {AMD_ADLX_BINDING_DLL}");
                    hADLXBindingModule = LoadLibrary(AMD_ADLX_BINDING_DLL);
                    if (hADLXBindingModule == IntPtr.Zero)
                    {
                        SharedLogger.logger.Error($"AMDLibrary/AMDLibrary: Failed to load {AMD_ADLX_BINDING_DLL}");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Error(ex, "AMDLibrary/AMDLibrary: Exception loading ADLX DLLs");
                    return;
                }

                try
                {
                    _initialisedADL2 = false;
                    Marshal.PrelinkAll(typeof(ADLImport));
                    ADL_STATUS ADLRet = ADLImport.ADL2_Main_Control_Create(ADLImport.ADL_Main_Memory_Alloc, ADLImport.ADL_TRUE, out _adlContextHandle);
                    if (ADLRet == ADL_STATUS.ADL_OK)
                    {
                        _initialisedADL2 = true;
                        SharedLogger.logger.Trace("AMDLibrary/AMDLibrary: AMD ADL2 initialised successfully");
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Error initialising AMD ADL2 library. ADL2_Main_Control_Create() returned error code {ADLRet}");
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Error(ex, "AMDLibrary/AMDLibrary: Exception initialising AMD ADL2 library.");
                }

                Environment.SetEnvironmentVariable("ADL_4KWORKAROUND_CANCEL", "TRUE");

                try
                {
                    SharedLogger.logger.Trace("AMDLibrary/AMDLibrary: Initialising AMD ADLX via ADLXWrapper");
                    _adlxApi = ADLXApi.Initialize();
                    _adlxSystemHandle = _adlxApi.GetSystemServicesHandle();
                    _initialised = true;
                }
                catch (ADLXException ex)
                {
                    _initialised = false;
                    SharedLogger.logger.Error(ex, "AMDLibrary/AMDLibrary: Error initialising AMD ADLX library via ADLXWrapper");
                    return;
                }
                catch (Exception ex)
                {
                    _initialised = false;
                    SharedLogger.logger.Error(ex, "AMDLibrary/AMDLibrary: Unexpected error initialising AMD ADLX library via ADLXWrapper");
                    return;
                }

                _activeDisplayConfig = GetActiveConfig();
                _allConnectedDisplayIdentifiers = GetAllConnectedDisplayIdentifiers(out bool _);
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Error(ex, "AMDLibrary/AMDLibrary: General exception during construction");
                _initialised = false;
            }
        }

        ~AMDLibrary()
        {
            Dispose(false);
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _safeHandle?.Dispose();
            }

            if (_adlxSystemHandle != null)
            {
                try { _adlxSystemHandle.Dispose(); } catch (Exception ex) { SharedLogger.logger.Trace(ex, "AMDLibrary/Dispose: Exception disposing ADLX system handle"); }
                _adlxSystemHandle = null;
            }

            if (_adlxApi != null)
            {
                try { _adlxApi.Dispose(); } catch (Exception ex) { SharedLogger.logger.Trace(ex, "AMDLibrary/Dispose: Exception disposing ADLX API"); }
                _adlxApi = null;
            }

            if (_adlContextHandle != IntPtr.Zero && _initialisedADL2)
            {
                try
                {
                    ADLImport.ADL2_Main_Control_Destroy(_adlContextHandle);
                    _adlContextHandle = IntPtr.Zero;
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, "AMDLibrary/Dispose: Exception destroying AMD ADL2 library");
                }
            }

            if (hADLXBindingModule != IntPtr.Zero)
            {
                try { FreeLibrary(hADLXBindingModule); } catch { }
                hADLXBindingModule = IntPtr.Zero;
            }

            if (hADLXModule != IntPtr.Zero)
            {
                try { FreeLibrary(hADLXModule); } catch { }
                hADLXModule = IntPtr.Zero;
            }

            _disposed = true;
        }

        public static void KeepVideoCardOn() => LoadLibrary("AMDExportsDLL.dll");

        public bool IsInstalled => _initialised;

        public List<string> PCIVendorIDs => new List<string> { "1002" };

        public AMD_DISPLAY_CONFIG ActiveDisplayConfig
        {
            get
            {
                if (_activeDisplayConfig == null)
                {
                    _activeDisplayConfig = CreateDefaultConfig();
                }
                return _activeDisplayConfig.Value;
            }
            set { _activeDisplayConfig = value; }
        }

        public List<string> CurrentDisplayIdentifiers => _activeDisplayConfig?.Value.DisplayIdentifiers ?? new List<string>();

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
            return new AMD_DISPLAY_CONFIG();
        }

        public bool UpdateActiveConfig()
        {
            try
            {
                _activeDisplayConfig = GetActiveConfig();
                _allConnectedDisplayIdentifiers = GetAllConnectedDisplayIdentifiers(out bool _);
                return true;
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Trace(ex, "AMDLibrary/UpdateActiveConfig: Exception updating active config");
                return false;
            }
        }

        public AMD_DISPLAY_CONFIG GetActiveConfig()
        {
            return GetAMDDisplayConfig(true);
        }

        private AMD_DISPLAY_CONFIG GetAMDDisplayConfig(bool allDisplays = false)
        {
            AMD_DISPLAY_CONFIG config = CreateDefaultConfig();

            if (!_initialised || _adlxApi is null || _adlxSystemHandle is null)
            {
                SharedLogger.logger.Error("AMDLibrary/GetAMDDisplayConfig: ADLX not initialised");
                return config;
            }

            var systemPtr = _adlxSystemHandle.DangerousGetHandle();

            try
            {
                // Desktops
                var desktops = GetAdlxDesktops(systemPtr, out AMD_EYEFINITY_DESKTOP eyefinityDesktop, out bool isCloned, out bool isEyefinity);
                config.Desktops = desktops;
                config.EyefinityDesktop = eyefinityDesktop;
                config.IsCloned = isCloned;
                config.IsEyefinity = isEyefinity;

                // Displays with settings
                var displays = GetAdlxDisplaysWithSettings(systemPtr);
                foreach (var d in displays)
                {
                    config.Displays[d.UniqueID] = d;
                }

                if (desktops.Count > 0 || displays.Count > 0)
                {
                    config.IsInUse = true;
                }

                config.DisplayIdentifiers = GetCurrentDisplayIdentifiers(out bool _);
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Error(ex, "AMDLibrary/GetAMDDisplayConfig: Failed to gather ADLX configuration");
            }

            return config;
        }

        public List<string> GetCurrentDisplayIdentifiers(out bool failure)
        {
            return GetAllConnectedDisplayIdentifiers(out failure);
        }

        public List<string> GetAllConnectedDisplayIdentifiers(out bool failure)
        {
            failure = false;
            var identifiers = new List<string>();

            if (!_initialised || _adlxApi is null || _adlxSystemHandle is null)
            {
                SharedLogger.logger.Error("AMDLibrary/GetAllConnectedDisplayIdentifiers: ADLX not initialised");
                failure = true;
                return identifiers;
            }

            var systemPtr = _adlxSystemHandle.DangerousGetHandle();
            try
            {
                var displayHandles = ADLXDisplayHelpers.EnumerateAllDisplayHandles(systemPtr);
                foreach (var handle in displayHandles)
                {
                    using (handle)
                    {
                        var displayPtr = handle.DangerousGetHandle();
                        string gpuName = "#";
                        string gpuUniqueId = "#";
                        string gpuIsExternal = "#";

                        try
                        {
                            var gpuPtr = ADLXDisplayHelpers.GetDisplayGPU(displayPtr);
                            if (gpuPtr != IntPtr.Zero)
                            {
                                gpuName = ADLXHelpers.GetGPUName(gpuPtr);
                                gpuUniqueId = ADLXHelpers.GetGPUUniqueId(gpuPtr).ToString();
                                gpuIsExternal = ADLXHelpers.IsGPUExternal(gpuPtr).ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Trace(ex, "AMDLibrary/GetAllConnectedDisplayIdentifiers: GPU info not available");
                        }

                        var parts = new List<string> { "AMDADLX" };
                        parts.Add(string.IsNullOrWhiteSpace(gpuName) ? "#" : gpuName);
                        parts.Add(string.IsNullOrWhiteSpace(gpuUniqueId) ? "#" : gpuUniqueId);
                        parts.Add(string.IsNullOrWhiteSpace(gpuIsExternal) ? "#" : gpuIsExternal);

                        try { parts.Add(ADLXDisplayHelpers.GetDisplayConnectorType(displayPtr).ToString("G")); } catch { parts.Add("#"); }
                        try { parts.Add(ADLXDisplayHelpers.GetDisplayName(displayPtr)); } catch { parts.Add("#"); }
                        try { parts.Add(ADLXDisplayHelpers.GetDisplayType(displayPtr).ToString("G")); } catch { parts.Add("#"); }
                        try { parts.Add(ADLXDisplayHelpers.GetDisplayManufacturerID(displayPtr).ToString()); } catch { parts.Add("#"); }
                        try { parts.Add(ADLXDisplayHelpers.GetDisplayUniqueId(displayPtr).ToString()); } catch { parts.Add("#"); }

                        var identifier = string.Join("|", parts);
                        if (!identifiers.Contains(identifier))
                        {
                            identifiers.Add(identifier);
                            SharedLogger.logger.Debug($"AMDLibrary/GetAllConnectedDisplayIdentifiers: {identifier}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                failure = true;
                SharedLogger.logger.Error(ex, "AMDLibrary/GetAllConnectedDisplayIdentifiers: Unexpected error");
            }

            identifiers.Sort();
            return identifiers;
        }

        public bool SetActiveConfig(AMD_DISPLAY_CONFIG displayConfig, bool useADLEyefinity, int delayInMs)
        {
            if (!_initialised || _adlxSystemHandle is null)
            {
                SharedLogger.logger.Error("AMDLibrary/SetActiveConfig: ERROR - Tried to run SetActiveConfig but the AMD ADLX library isn't initialised!");
                throw new AMDLibraryException("Tried to run SetActiveConfig but the AMD ADLX library isn't initialised!");
            }

            var systemPtr = _adlxSystemHandle.DangerousGetHandle();

            try
            {
                using var desktopServices = ADLXDesktopHelpers.GetDesktopServicesHandle(systemPtr);

                if (displayConfig.IsEyefinity)
                {
                    SharedLogger.logger.Trace("AMDLibrary/SetActiveConfig: New display layout requires an Eyefinity desktop");

                    if (useADLEyefinity)
                    {
                        SharedLogger.logger.Trace("AMDLibrary/SetActiveConfig: Using the older ADL API to create the Eyefinity Desktop.");

                        foreach (AMD_SLSMAP_CONFIG slsMapConfig in displayConfig.Adl2SlsConfig.SLSMapConfigs)
                        {
                            var ret = ADLImport.ADL2_Display_SLSMapConfig_SetState(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap.SLSMapIndex, ADLImport.ADL_TRUE);
                            if (ret == ADL_STATUS.ADL_OK)
                            {
                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_SetState successfully set the SLSMAP with index {slsMapConfig.SLSMap.SLSMapIndex} to TRUE for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                continue;
                            }

                            SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_SetState returned ADL_STATUS {ret} when trying to enable SLSMAP {slsMapConfig.SLSMap.SLSMapIndex} for adapter {slsMapConfig.SLSMap.AdapterIndex}. Attempting to create a new SLS map.");

                            int supportedMode;
                            int reasonNotSupported;
                            ret = ADLImport.ADL2_Display_SLSMapConfig_Valid(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap, slsMapConfig.SLSTargets.Count, slsMapConfig.SLSTargets.ToArray(), out supportedMode, out reasonNotSupported, ADLImport.ADL_DISPLAY_SLSMAPCONFIG_CREATE_OPTION_RELATIVETO_CURRENTANGLE);
                            if (ret != ADL_STATUS.ADL_OK)
                            {
                                SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_Valid returned ADL_STATUS {ret} for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                return false;
                            }

                            int newSlsMapIndex;
                            ret = ADLImport.ADL2_Display_SLSMapConfig_Create(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap, slsMapConfig.SLSTargets.Count, slsMapConfig.SLSTargets.ToArray(), slsMapConfig.BezelModePercent, out newSlsMapIndex, ADLImport.ADL_DISPLAY_SLSMAPCONFIG_CREATE_OPTION_RELATIVETO_CURRENTANGLE);
                            if (ret == ADL_STATUS.ADL_OK && newSlsMapIndex != -1)
                            {
                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_Create successfully created SLSMAP {newSlsMapIndex} for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                            }
                            else
                            {
                                SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_Create returned ADL_STATUS {ret} for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                return false;
                            }

                            ret = ADLImport.ADL2_Flush_Driver_Data(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex);
                            if (ret == ADL_STATUS.ADL_OK)
                            {
                                SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: ADL2_Flush_Driver_Data successfully saved settings for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                            }
                            else
                            {
                                SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ADL2_Flush_Driver_Data failed for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        SharedLogger.logger.Trace("AMDLibrary/SetActiveConfig: Using the newer ADLX API to create the Eyefinity Desktop.");

                        if (displayConfig.EyefinityDesktop.Equals(ActiveDisplayConfig.EyefinityDesktop))
                        {
                            SharedLogger.logger.Trace("AMDLibrary/SetActiveConfig: Eyefinity layout already matches desired configuration, skipping create.");
                        }
                        else
                        {
                            using var simpleEyefinity = ADLXDesktopHelpers.GetSimpleEyefinityHandle(desktopServices.DangerousGetHandle());
                            if (!ADLXDesktopHelpers.IsSimpleEyefinitySupported(simpleEyefinity.DangerousGetHandle()))
                            {
                                SharedLogger.logger.Error("AMDLibrary/SetActiveConfig: Eyefinity not supported on this system.");
                                return false;
                            }

                            using var eyefinityDesktop = ADLXDesktopHelpers.CreateEyefinityDesktop(simpleEyefinity.DangerousGetHandle());
                            SharedLogger.logger.Trace("AMDLibrary/SetActiveConfig: Created ADLX Eyefinity Desktop.");
                        }
                    }
                }
                else
                {
                    SharedLogger.logger.Trace("AMDLibrary/SetActiveConfig: New display layout does NOT require an Eyefinity desktop");

                    if (ActiveDisplayConfig.IsEyefinity)
                    {
                        SharedLogger.logger.Trace("AMDLibrary/SetActiveConfig: Eyefinity layout is currently in use but is NOT required, destroying Eyefinity Desktop.");

                        if (useADLEyefinity)
                        {
                            foreach (AMD_SLSMAP_CONFIG slsMapConfig in ActiveDisplayConfig.Adl2SlsConfig.SLSMapConfigs)
                            {
                                var ret = ADLImport.ADL2_Display_SLSMapConfig_SetState(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap.SLSMapIndex, ADLImport.ADL_FALSE);
                                if (ret == ADL_STATUS.ADL_OK)
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Disabled SLSMAP {slsMapConfig.SLSMap.SLSMapIndex} for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                                }
                                else
                                {
                                    SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Failed to disable SLSMAP {slsMapConfig.SLSMap.SLSMapIndex} for adapter {slsMapConfig.SLSMap.AdapterIndex}, ADL_STATUS {ret}.");
                                    return false;
                                }

                                ret = ADLImport.ADL2_Flush_Driver_Data(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex);
                                if (ret == ADL_STATUS.ADL_OK)
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Flushed adapter {slsMapConfig.SLSMap.AdapterIndex} after disable.");
                                }
                                else
                                {
                                    SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Failed to flush adapter {slsMapConfig.SLSMap.AdapterIndex} after disable, ADL_STATUS {ret}.");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            using var simpleEyefinity = ADLXDesktopHelpers.GetSimpleEyefinityHandle(desktopServices.DangerousGetHandle());
                            try
                            {
                                ADLXDesktopHelpers.DestroyAllEyefinityDesktops(simpleEyefinity.DangerousGetHandle());
                                SharedLogger.logger.Trace("AMDLibrary/SetActiveConfig: Destroyed all ADLX Eyefinity Desktops.");
                            }
                            catch (Exception ex)
                            {
                                SharedLogger.logger.Error(ex, "AMDLibrary/SetActiveConfig: Error destroying ADLX Eyefinity desktops.");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        SharedLogger.logger.Trace("AMDLibrary/SetActiveConfig: Eyefinity not in use and not required, no action needed.");
                    }
                }
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Error(ex, "AMDLibrary/SetActiveConfig: Unexpected error");
                return false;
            }

            return true;
        }

        public bool SetActiveConfigOverride(AMD_DISPLAY_CONFIG displayConfig, int delayInMs)
        {
            if (!_initialised || _adlxSystemHandle is null)
            {
                SharedLogger.logger.Error("AMDLibrary/SetActiveConfigOverride: ERROR - Tried to run SetActiveConfigOverride but the AMD ADLX library isn't initialised!");
                throw new AMDLibraryException("Tried to run SetActiveConfigOverride but the AMD ADLX library isn't initialised!");
            }

            var systemPtr = _adlxSystemHandle.DangerousGetHandle();
            try
            {
                var displayHandles = ADLXDisplayHelpers.EnumerateAllDisplayHandles(systemPtr);
                using var displayServices = ADLXDisplayHelpers.GetDisplayServicesHandle(systemPtr);

                foreach (var handle in displayHandles)
                {
                    using (handle)
                    {
                        var pDisplay = handle.DangerousGetHandle();
                        long uniqueId;
                        try { uniqueId = ADLXDisplayHelpers.GetDisplayUniqueId(pDisplay); }
                        catch { continue; }

                        if (!displayConfig.Displays.TryGetValue(uniqueId, out var storedSettings))
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: No display with UniqueID {uniqueId} found in the stored settings, skipping.");
                            continue;
                        }

                        ApplyDisplaySettings(displayServices.DangerousGetHandle(), pDisplay, storedSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Error(ex, "AMDLibrary/SetActiveConfigOverride: Unexpected error");
                return false;
            }

            return true;
        }

        public bool IsActiveConfig(AMD_DISPLAY_CONFIG displayConfig)
        {
            return ActiveDisplayConfig.Equals(displayConfig);
        }

        public bool IsValidConfig(AMD_DISPLAY_CONFIG displayConfig)
        {
            return true; // placeholder; validation will be added later
        }

        public bool IsPossibleConfig(AMD_DISPLAY_CONFIG displayConfig)
        {
            return true; // placeholder; feasibility checks will be added later
        }

        #region ADLX Helper Methods (read-only)
        private void ApplyDisplaySettings(IntPtr displayServicesPtr, IntPtr displayPtr, AMD_DISPLAY_WITH_SETTINGS stored)
        {
            // Color depth
            if (stored.IsSupportedColorDepth)
            {
                try
                {
                    using var handle = ADLXDisplaySettingsHelpers.GetColorDepthHandle(displayServicesPtr, displayPtr);
                    var (supported, current) = ADLXDisplaySettingsHelpers.GetColorDepthState(handle.DangerousGetHandle());
                    if (supported && current != stored.ColorDepth)
                    {
                        ADLXDisplaySettingsHelpers.SetColorDepth(handle.DangerousGetHandle(), stored.ColorDepth);
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set Color Depth to {stored.ColorDepth:G}");
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: Color Depth not applied");
                }
            }

            // Custom color
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetCustomColorHandle(displayServicesPtr, displayPtr);

                if (stored.IsSupportedCustomColorBrightness)
                {
                    try
                    {
                        var (supported, current, _) = ADLXDisplaySettingsHelpers.GetCustomColorBrightness(handle.DangerousGetHandle());
                        if (supported && current != stored.CustomColorBrightness)
                        {
                            ADLXDisplaySettingsHelpers.SetCustomColorBrightness(handle.DangerousGetHandle(), stored.CustomColorBrightness);
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set Custom Color Brightness to {stored.CustomColorBrightness}");
                        }
                    }
                    catch (Exception ex) { SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: Custom Color Brightness not applied"); }
                }

                if (stored.IsSupportedCustomColorHue)
                {
                    try
                    {
                        var (supported, current, _) = ADLXDisplaySettingsHelpers.GetCustomColorHue(handle.DangerousGetHandle());
                        if (supported && current != stored.CustomColorHue)
                        {
                            ADLXDisplaySettingsHelpers.SetCustomColorHue(handle.DangerousGetHandle(), stored.CustomColorHue);
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set Custom Color Hue to {stored.CustomColorHue}");
                        }
                    }
                    catch (Exception ex) { SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: Custom Color Hue not applied"); }
                }

                if (stored.IsSupportedCustomColorSaturation)
                {
                    try
                    {
                        var (supported, current, _) = ADLXDisplaySettingsHelpers.GetCustomColorSaturation(handle.DangerousGetHandle());
                        if (supported && current != stored.CustomColorSaturation)
                        {
                            ADLXDisplaySettingsHelpers.SetCustomColorSaturation(handle.DangerousGetHandle(), stored.CustomColorSaturation);
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set Custom Color Saturation to {stored.CustomColorSaturation}");
                        }
                    }
                    catch (Exception ex) { SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: Custom Color Saturation not applied"); }
                }

                if (stored.IsSupportedCustomColorContrast)
                {
                    try
                    {
                        var (supported, current, _) = ADLXDisplaySettingsHelpers.GetCustomColorContrast(handle.DangerousGetHandle());
                        if (supported && current != stored.CustomColorContrast)
                        {
                            ADLXDisplaySettingsHelpers.SetCustomColorContrast(handle.DangerousGetHandle(), stored.CustomColorContrast);
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set Custom Color Contrast to {stored.CustomColorContrast}");
                        }
                    }
                    catch (Exception ex) { SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: Custom Color Contrast not applied"); }
                }

                if (stored.IsSupportedCustomColorTemperature)
                {
                    try
                    {
                        var (supported, current, _) = ADLXDisplaySettingsHelpers.GetCustomColorTemperature(handle.DangerousGetHandle());
                        if (supported && current != stored.CustomColorTemperature)
                        {
                            ADLXDisplaySettingsHelpers.SetCustomColorTemperature(handle.DangerousGetHandle(), stored.CustomColorTemperature);
                            SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set Custom Color Temperature to {stored.CustomColorTemperature}");
                        }
                    }
                    catch (Exception ex) { SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: Custom Color Temperature not applied"); }
                }
            }
            catch { }

            // FreeSync
            if (stored.IsSupportedFreeSync)
            {
                try
                {
                    using var handle = ADLXDisplaySettingsHelpers.GetFreeSyncHandle(displayServicesPtr, displayPtr);
                    var (supported, enabled) = ADLXDisplaySettingsHelpers.GetFreeSyncState(handle.DangerousGetHandle());
                    if (supported && enabled != stored.IsEnabledFreeSync)
                    {
                        ADLXDisplaySettingsHelpers.SetFreeSyncEnabled(handle.DangerousGetHandle(), stored.IsEnabledFreeSync);
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set FreeSync to {stored.IsEnabledFreeSync}");
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: FreeSync not applied");
                }
            }

            // GPU Scaling
            if (stored.IsSupportedGPUScaling)
            {
                try
                {
                    using var handle = ADLXDisplaySettingsHelpers.GetGPUScalingHandle(displayServicesPtr, displayPtr);
                    var (supported, enabled) = ADLXDisplaySettingsHelpers.GetGPUScalingState(handle.DangerousGetHandle());
                    if (supported && enabled != stored.IsEnabledGPUScaling)
                    {
                        ADLXDisplaySettingsHelpers.SetGPUScalingEnabled(handle.DangerousGetHandle(), stored.IsEnabledGPUScaling);
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set GPU Scaling to {stored.IsEnabledGPUScaling}");
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: GPU Scaling not applied");
                }
            }

            // Integer Scaling
            if (stored.IsSupportedIntegerScaling)
            {
                try
                {
                    using var handle = ADLXDisplaySettingsHelpers.GetIntegerScalingHandle(displayServicesPtr, displayPtr);
                    var (supported, enabled) = ADLXDisplaySettingsHelpers.GetIntegerScalingState(handle.DangerousGetHandle());
                    if (supported && enabled != stored.IsEnabledIntegerScaling)
                    {
                        ADLXDisplaySettingsHelpers.SetIntegerScalingEnabled(handle.DangerousGetHandle(), stored.IsEnabledIntegerScaling);
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set Integer Scaling to {stored.IsEnabledIntegerScaling}");
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: Integer Scaling not applied");
                }
            }

            // Pixel Format
            if (stored.IsSupportedPixelFormat)
            {
                try
                {
                    using var handle = ADLXDisplaySettingsHelpers.GetPixelFormatHandle(displayServicesPtr, displayPtr);
                    var (supported, current) = ADLXDisplaySettingsHelpers.GetPixelFormatState(handle.DangerousGetHandle());
                    if (supported && current != stored.CurrentPixelFormat)
                    {
                        ADLXDisplaySettingsHelpers.SetPixelFormat(handle.DangerousGetHandle(), stored.CurrentPixelFormat);
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set Pixel Format to {stored.CurrentPixelFormat:G}");
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: Pixel Format not applied");
                }
            }

            // Scaling Mode
            if (stored.IsSupportedScalingMode)
            {
                try
                {
                    using var handle = ADLXDisplaySettingsHelpers.GetScalingModeHandle(displayServicesPtr, displayPtr);
                    var (supported, current) = ADLXDisplaySettingsHelpers.GetScalingModeState(handle.DangerousGetHandle());
                    if (supported && current != stored.CurrentScalingMode)
                    {
                        ADLXDisplaySettingsHelpers.SetScalingMode(handle.DangerousGetHandle(), stored.CurrentScalingMode);
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set Scaling Mode to {stored.CurrentScalingMode:G}");
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: Scaling Mode not applied");
                }
            }

            // VSR
            if (stored.IsSupportedVSR)
            {
                try
                {
                    using var handle = ADLXDisplaySettingsHelpers.GetVSRHandle(displayServicesPtr, displayPtr);
                    var (supported, enabled) = ADLXDisplaySettingsHelpers.GetVirtualSuperResolutionState(handle.DangerousGetHandle());
                    if (supported && enabled != stored.IsEnabledVSR)
                    {
                        ADLXDisplaySettingsHelpers.SetVirtualSuperResolutionEnabled(handle.DangerousGetHandle(), stored.IsEnabledVSR);
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set VSR to {stored.IsEnabledVSR}");
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: VSR not applied");
                }
            }

            // HDCP
            if (stored.IsSupportedHDCP)
            {
                try
                {
                    using var handle = ADLXDisplaySettingsHelpers.GetHDCPHandle(displayServicesPtr, displayPtr);
                    var (supported, enabled) = ADLXDisplaySettingsHelpers.GetHDCPState(handle.DangerousGetHandle());
                    if (supported && enabled != stored.IsEnabledHDCP)
                    {
                        ADLXDisplaySettingsHelpers.SetHDCPEnabled(handle.DangerousGetHandle(), stored.IsEnabledHDCP);
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set HDCP to {stored.IsEnabledHDCP}");
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: HDCP not applied");
                }
            }

            // VariBright
            if (stored.IsSupportedVariBright)
            {
                try
                {
                    using var handle = ADLXDisplaySettingsHelpers.GetVariBrightHandle(displayServicesPtr, displayPtr);
                    var (supported, enabled, _) = ADLXDisplaySettingsHelpers.GetVariBrightState(handle.DangerousGetHandle());
                    if (supported && enabled != stored.IsEnabledVariBright)
                    {
                        ADLXDisplaySettingsHelpers.SetVariBright(handle.DangerousGetHandle(), stored.IsEnabledVariBright, ADLXDisplaySettingsHelpers.VariBrightMode.Unknown);
                        SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Set VariBright to {stored.IsEnabledVariBright}");
                    }
                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, "AMDLibrary/SetActiveConfigOverride: VariBright not applied");
                }
            }
        }

        private List<AMD_DESKTOP> GetAdlxDesktops(IntPtr systemPtr, out AMD_EYEFINITY_DESKTOP eyefinityDesktop, out bool isCloned, out bool isEyefinity)
        {
            eyefinityDesktop = new AMD_EYEFINITY_DESKTOP();
            isCloned = false;
            isEyefinity = false;

            var desktops = new List<AMD_DESKTOP>();
            try
            {
                using var desktopServices = ADLXDesktopHelpers.GetDesktopServicesHandle(systemPtr);
                var desktopHandles = ADLXDesktopHelpers.EnumerateAllDesktopHandles(desktopServices.DangerousGetHandle());
                foreach (var desktopHandle in desktopHandles)
                {
                    using (desktopHandle)
                    {
                        var desktop = BuildDesktop(desktopHandle.DangerousGetHandle(), ref eyefinityDesktop, ref isEyefinity, ref isCloned);
                        desktops.Add(desktop);
                    }
                }
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Trace(ex, "AMDLibrary/GetAdlxDesktops: Failed to enumerate desktops");
            }
            return desktops;
        }

        private List<AMD_DISPLAY_WITH_SETTINGS> GetAdlxDisplaysWithSettings(IntPtr systemPtr)
        {
            var displays = new List<AMD_DISPLAY_WITH_SETTINGS>();
            try
            {
                var displayHandles = ADLXDisplayHelpers.EnumerateAllDisplayHandles(systemPtr);
                using var displayServices = ADLXDisplayHelpers.GetDisplayServicesHandle(systemPtr);
                foreach (var handle in displayHandles)
                {
                    using (handle)
                    {
                        var d = BuildDisplayWithSettings(handle.DangerousGetHandle(), displayServices.DangerousGetHandle());
                        displays.Add(d);
                    }
                }
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Trace(ex, "AMDLibrary/GetAdlxDisplaysWithSettings: Failed to enumerate displays");
            }
            return displays;
        }

        private AMD_DESKTOP BuildDesktop(IntPtr desktopPtr, ref AMD_EYEFINITY_DESKTOP eyefinityDesktop, ref bool isEyefinity, ref bool isCloned)
        {
            AMD_DESKTOP desktop = new AMD_DESKTOP { Displays = new List<AMD_DISPLAY>() };
            try
            {
                desktop.Type = ADLXDesktopHelpers.GetDesktopType(desktopPtr);
                var size = ADLXDesktopHelpers.GetDesktopSize(desktopPtr);
                desktop.SizeWidth = size.Width;
                desktop.SizeHeight = size.Height;
                var topLeft = ADLXDesktopHelpers.GetDesktopTopLeft(desktopPtr);
                desktop.TopLeftX = topLeft.X;
                desktop.TopLeftY = topLeft.Y;
                desktop.Orientation = ADLXDesktopHelpers.GetDesktopOrientation(desktopPtr);

                var displayPtrs = GetDesktopDisplays(desktopPtr);
                desktop.NumberOfDisplays = displayPtrs.LongLength;
                foreach (var pDisplay in displayPtrs)
                {
                    try
                    {
                        var basic = BuildBasicDisplay(pDisplay);
                        desktop.Displays.Add(basic);
                    }
                    catch (Exception ex)
                    {
                        SharedLogger.logger.Trace(ex, "AMDLibrary/BuildDesktop: Failed to build basic display");
                    }
                    finally
                    {
                        ADLXHelpers.ReleaseInterface(pDisplay);
                    }
                }

                if (desktop.Type == ADLX_DESKTOP_TYPE.DESKTOP_EYEFINITY)
                {
                    isEyefinity = true;
                    eyefinityDesktop = BuildEyefinityDesktop(desktopPtr);
                }
                else if (desktop.Type == ADLX_DESKTOP_TYPE.DESKTOP_DUPLCATE)
                {
                    isCloned = true;
                }
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Trace(ex, "AMDLibrary/BuildDesktop: Error reading desktop");
            }

            return desktop;
        }

        private AMD_EYEFINITY_DESKTOP BuildEyefinityDesktop(IntPtr desktopPtr)
        {
            AMD_EYEFINITY_DESKTOP result = new AMD_EYEFINITY_DESKTOP();
            try
            {
                if (ADLXHelpers.TryQueryInterface(desktopPtr, "IADLXEyefinityDesktop", out var eyefinityPtr) && eyefinityPtr != IntPtr.Zero)
                {
                    try
                    {
                        var grid = ADLXDesktopHelpers.GetEyefinityGridSize(eyefinityPtr);
                        result.Rows = grid.rows;
                        result.Columns = grid.cols;
                    }
                    finally
                    {
                        ADLXHelpers.ReleaseInterface(eyefinityPtr);
                    }
                }

                try
                {
                    var size = ADLXDesktopHelpers.GetDesktopSize(desktopPtr);
                    var topLeft = ADLXDesktopHelpers.GetDesktopTopLeft(desktopPtr);
                    result.SizeWidth = size.Width;
                    result.SizeHeight = size.Height;
                    result.TopLeftX = topLeft.X;
                    result.TopLeftY = topLeft.Y;
                    result.Orientation = ADLXDesktopHelpers.GetDesktopOrientation(desktopPtr);
                }
                catch { }
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Trace(ex, "AMDLibrary/BuildEyefinityDesktop: Error reading Eyefinity desktop");
            }
            return result;
        }

        private unsafe IntPtr[] GetDesktopDisplays(IntPtr desktopPtr)
        {
            var vtbl = *(ADLXVTables.IADLXDesktopVtbl**)desktopPtr;
            var getDisplaysFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.DesktopGetDisplaysFn>(vtbl->GetDisplays);
            IntPtr listPtr;
            var r = getDisplaysFn(desktopPtr, &listPtr);
            if (r != ADLX_RESULT.ADLX_OK || listPtr == IntPtr.Zero)
            {
                return Array.Empty<IntPtr>();
            }
            try
            {
                return ADLXListHelpers.ListToArray(listPtr);
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(listPtr);
            }
        }

        private AMD_DISPLAY BuildBasicDisplay(IntPtr displayPtr)
        {
            AMD_DISPLAY d = new AMD_DISPLAY();
            try
            {
                d.Name = ADLXDisplayHelpers.GetDisplayName(displayPtr);
                d.EDID = ADLXDisplayHelpers.GetDisplayEdid(displayPtr);
                d.ConnectorType = ADLXDisplayHelpers.GetDisplayConnectorType(displayPtr);
                d.DisplayType = ADLXDisplayHelpers.GetDisplayType(displayPtr);
                d.ManufacturerID = ADLXDisplayHelpers.GetDisplayManufacturerID(displayPtr);
                var res = ADLXDisplayHelpers.GetDisplayNativeResolution(displayPtr);
                d.MaxHResolution = res.width;
                d.MaxVResolution = res.height;
                d.PixelClock = ADLXDisplayHelpers.GetDisplayPixelClock(displayPtr);
                d.RefreshRate = ADLXDisplayHelpers.GetDisplayRefreshRate(displayPtr);
                d.ScanType = ADLXDisplayHelpers.GetDisplayScanType(displayPtr);
                d.UniqueID = ADLXDisplayHelpers.GetDisplayUniqueId(displayPtr);
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Trace(ex, "AMDLibrary/BuildBasicDisplay: Error reading display");
            }
            return d;
        }

        private AMD_DISPLAY_WITH_SETTINGS BuildDisplayWithSettings(IntPtr displayPtr, IntPtr displayServicesPtr)
        {
            var baseDisplay = BuildBasicDisplay(displayPtr);
            AMD_DISPLAY_WITH_SETTINGS d = new AMD_DISPLAY_WITH_SETTINGS
            {
                ConnectorType = baseDisplay.ConnectorType,
                DisplayType = baseDisplay.DisplayType,
                EDID = baseDisplay.EDID,
                ManufacturerID = baseDisplay.ManufacturerID,
                Name = baseDisplay.Name,
                MaxHResolution = baseDisplay.MaxHResolution,
                MaxVResolution = baseDisplay.MaxVResolution,
                PixelClock = baseDisplay.PixelClock,
                RefreshRate = baseDisplay.RefreshRate,
                ScanType = baseDisplay.ScanType,
                UniqueID = baseDisplay.UniqueID
            };

            // Color depth
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetColorDepthHandle(displayServicesPtr, displayPtr);
                var (supported, current) = ADLXDisplaySettingsHelpers.GetColorDepthState(handle);
                d.IsSupportedColorDepth = supported;
                if (supported) d.ColorDepth = current;
            }
            catch { }

            // Custom color
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetCustomColorHandle(displayServicesPtr, displayPtr);
                var (hueSupported, hue, _) = ADLXDisplaySettingsHelpers.GetCustomColorHue(handle);
                d.IsSupportedCustomColorHue = hueSupported; d.CustomColorHue = hue;
                var (satSupported, sat, _) = ADLXDisplaySettingsHelpers.GetCustomColorSaturation(handle);
                d.IsSupportedCustomColorSaturation = satSupported; d.CustomColorSaturation = sat;
                var (brightSupported, bright, _) = ADLXDisplaySettingsHelpers.GetCustomColorBrightness(handle);
                d.IsSupportedCustomColorBrightness = brightSupported; d.CustomColorBrightness = bright;
                var (contrastSupported, contrast, _) = ADLXDisplaySettingsHelpers.GetCustomColorContrast(handle);
                d.IsSupportedCustomColorContrast = contrastSupported; d.CustomColorContrast = contrast;
                var (tempSupported, temp, _) = ADLXDisplaySettingsHelpers.GetCustomColorTemperature(handle);
                d.IsSupportedCustomColorTemperature = tempSupported; d.CustomColorTemperature = temp;
            }
            catch { }

            // FreeSync
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetFreeSyncHandle(displayServicesPtr, displayPtr);
                var (supported, enabled) = ADLXDisplaySettingsHelpers.GetFreeSyncState(handle);
                d.IsSupportedFreeSync = supported;
                d.IsEnabledFreeSync = enabled;
            }
            catch { }

            // GPU Scaling
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetGPUScalingHandle(displayServicesPtr, displayPtr);
                var (supported, enabled) = ADLXDisplaySettingsHelpers.GetGPUScalingState(handle);
                d.IsSupportedGPUScaling = supported;
                d.IsEnabledGPUScaling = enabled;
            }
            catch { }

            // Integer Scaling
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetIntegerScalingHandle(displayServicesPtr, displayPtr);
                var (supported, enabled) = ADLXDisplaySettingsHelpers.GetIntegerScalingState(handle);
                d.IsSupportedIntegerScaling = supported;
                d.IsEnabledIntegerScaling = enabled;
            }
            catch { }

            // Pixel Format
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetPixelFormatHandle(displayServicesPtr, displayPtr);
                var (supported, current) = ADLXDisplaySettingsHelpers.GetPixelFormatState(handle);
                d.IsSupportedPixelFormat = supported;
                if (supported) d.CurrentPixelFormat = current;
            }
            catch { }

            // Scaling Mode
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetScalingModeHandle(displayServicesPtr, displayPtr);
                var (supported, current) = ADLXDisplaySettingsHelpers.GetScalingModeState(handle);
                d.IsSupportedScalingMode = supported;
                if (supported) d.CurrentScalingMode = current;
            }
            catch { }

            // VSR
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetVSRHandle(displayServicesPtr, displayPtr);
                var (supported, enabled) = ADLXDisplaySettingsHelpers.GetVSRState(handle);
                d.IsSupportedVSR = supported;
                d.IsEnabledVSR = enabled;
            }
            catch { }

            // HDCP
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetHDCPHandle(displayServicesPtr, displayPtr);
                var (supported, enabled) = ADLXDisplaySettingsHelpers.GetHDCPState(handle);
                d.IsSupportedHDCP = supported;
                d.IsEnabledHDCP = enabled;
            }
            catch { }

            // VariBright
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetVariBrightHandle(displayServicesPtr, displayPtr);
                var (supported, enabled) = ADLXDisplaySettingsHelpers.GetVariBrightState(handle);
                d.IsSupportedVariBright = supported;
                d.IsEnabledVariBright = enabled;
            }
            catch { }

            // Gamut
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.GetGamutHandle(displayServicesPtr, displayPtr);
                var (gamut, _, _, _, _, _, _, _) = ADLXDisplaySettingsHelpers.GetGamutState(handle);
                d.GamutColorSpaceRedX = gamut.red.x;
                d.GamutColorSpaceRedY = gamut.red.y;
                d.GamutColorSpaceGreenX = gamut.green.x;
                d.GamutColorSpaceGreenY = gamut.green.y;
                d.GamutColorSpaceBlueX = gamut.blue.x;
                d.GamutColorSpaceBlueY = gamut.blue.y;
            }
            catch { }

            // 3DLUT (basic flags only)
            try
            {
                using var handle = ADLXDisplaySettingsHelpers.Get3DLUTHandle(displayServicesPtr, displayPtr);
                var (sceSupported, vividSupported, currentDisabled, currentVivid) = ADLXDisplaySettingsHelpers.Get3DLUTState(handle);
                d.IsSupported3DLUT = sceSupported || vividSupported;
                d.ThreeDLUTSettings.IsSupportedSCE = sceSupported;
                d.ThreeDLUTSettings.IsSupportedSCEVividGaming = vividSupported;
                d.ThreeDLUTSettings.IsCurrentSCEDisabled = currentDisabled;
                d.ThreeDLUTSettings.IsCurrentSCEVividGaming = currentVivid;
            }
            catch { }

            return d;
        }

        #endregion
    }
}
