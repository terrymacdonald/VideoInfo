using ADLXWrapper;
using DisplayMagicianShared;
using DisplayMagicianShared.NVIDIA;
using DisplayMagicianShared.Windows;
using EDIDParser;
using IGCLWrapper;
using Microsoft.VisualBasic;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
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
using WinRT;
using static DisplayMagicianShared.NVIDIA.DisplayTopologyStatus;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DisplayMagicianShared.AMD
{

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
            SLSTargets = new List<ADL_SLS_TARGET>() { };
            NativeModes = new List<ADL_SLS_MODE>() { };
            NativeModeOffsets = new List<ADL_SLS_OFFSET>() { };
            BezelModes = new List<ADL_BEZEL_TRANSIENT_MODE>() { };
            TransientModes = new List<ADL_BEZEL_TRANSIENT_MODE>() { };
            SLSOffsets = new List<ADL_SLS_OFFSET>() { };
            BezelModePercent= 0;
        }

        public override bool Equals(object obj) => obj is AMD_SLS_CONFIG other && this.Equals(other);

        public bool Equals(AMD_SLSMAP_CONFIG other)
        => SLSMap == other.SLSMap &&
           SLSTargets.SequenceEqual(other.SLSTargets) &&
           NativeModes.SequenceEqual(other.NativeModes) &&
           NativeModeOffsets.SequenceEqual(other.NativeModeOffsets) &&
           BezelModes.SequenceEqual(other.BezelModes) &&
           TransientModes.SequenceEqual(other.TransientModes) &&
           SLSOffsets.SequenceEqual(other.SLSOffsets) &&
           BezelModePercent == other.BezelModePercent;

        public override int GetHashCode()
        {
            return (SLSMap, SLSTargets, NativeModes, NativeModeOffsets, BezelModes, TransientModes, SLSOffsets, BezelModePercent).GetHashCode();
        }
        public static bool operator ==(AMD_SLSMAP_CONFIG lhs, AMD_SLSMAP_CONFIG rhs) => lhs.Equals(rhs);

        public static bool operator !=(AMD_SLSMAP_CONFIG lhs, AMD_SLSMAP_CONFIG rhs) => !(lhs == rhs);
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

        public override bool Equals(object obj) => obj is AMD_SLS_CONFIG other && this.Equals(other);

        public bool Equals(AMD_SLS_CONFIG other)
        => IsSlsEnabled == other.IsSlsEnabled &&
           SLSMapConfigs.SequenceEqual(other.SLSMapConfigs) &&
           SLSEnabledDisplayTargets.SequenceEqual(other.SLSEnabledDisplayTargets);

        public override int GetHashCode()
        {
            return (IsSlsEnabled, SLSMapConfigs, SLSEnabledDisplayTargets).GetHashCode();
        }
        public static bool operator ==(AMD_SLS_CONFIG lhs, AMD_SLS_CONFIG rhs) => lhs.Equals(rhs);

        public static bool operator !=(AMD_SLS_CONFIG lhs, AMD_SLS_CONFIG rhs) => !(lhs == rhs);
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

    public struct AMD_3DLUT_INFO : IEquatable<AMD_3DLUT_INFO>
    {
        public bool IsSupportedSCE { get; set; }
        public bool IsSupportedSCEVividGaming { get; set; }
        public bool IsSupportedSCEDynamicContrast { get; set; }
        public bool IsSupportedUser3DLUT { get; set; }

        public bool IsCurrentSCEDisabled { get; set; }
        public bool IsCurrentSCEVividGaming { get; set; }
        public bool HasDynamicContrast { get; set; }

        // If using Dynamic Contrast mode:
        public int CurrentDynamicContrastValue { get; set; }
        public IntRange DynamicContrastRange { get; set; }  // min, max, step

        public AMD_3DLUT_INFO()
        {
            CurrentDynamicContrastValue = 0;
        }

        public AMD_3DLUT_INFO(bool isSupportedSCE, bool isSupportedSCEVividGaming, bool hasDynamicContrast,
                             bool isSupportedUser3DLUT, bool isCurrentSCEDisabled, bool isCurrentSCEVividGaming,
                             bool isCurrentSCEDynamicContrast, bool isUser3DLUTApplied, int currentDynamicContrastValue,
                             IntRange dynamicContrastRange, List<ushort> userLUTData, int userLUTNumPoints, LutMode userLUTMode)
        {
            IsSupportedSCE = isSupportedSCE;
            IsSupportedSCEVividGaming = isSupportedSCEVividGaming;
            HasDynamicContrast = hasDynamicContrast;
            IsSupportedUser3DLUT = isSupportedUser3DLUT;
            IsCurrentSCEDisabled = isCurrentSCEDisabled;
            IsCurrentSCEVividGaming = isCurrentSCEVividGaming;
            CurrentDynamicContrastValue = currentDynamicContrastValue;
            DynamicContrastRange = dynamicContrastRange;
        }

        public AMD_3DLUT_INFO(ThreeDLUTInfo threeDLUTInfo)
        {
            IsSupportedSCE = threeDLUTInfo.IsSceSupported;
            IsSupportedSCEVividGaming = threeDLUTInfo.IsSceVividGamingSupported;
            IsSupportedSCEDynamicContrast = threeDLUTInfo.IsSceDynamicContrastSupported;
            IsSupportedUser3DLUT = threeDLUTInfo.IsUser3DLutSupported;
            IsCurrentSCEDisabled = threeDLUTInfo.IsCurrentSceDisabled;
            IsCurrentSCEVividGaming = threeDLUTInfo.IsCurrentSceVividGaming;
            HasDynamicContrast = threeDLUTInfo.HasDynamicContrast;
            CurrentDynamicContrastValue = threeDLUTInfo.CurrentDynamicContrast;
            DynamicContrastRange = new IntRange
            {
                Min = threeDLUTInfo.DynamicContrastRange.minValue,
                Max = threeDLUTInfo.DynamicContrastRange.maxValue,
                Step = threeDLUTInfo.DynamicContrastRange.step
            };
        }

        public ThreeDLUTInfo ToThreeDLUTInfo()
        {
            return new ThreeDLUTInfo
            {
                IsSceSupported = IsSupportedSCE,
                IsSceVividGamingSupported = IsSupportedSCEVividGaming,
                IsSceDynamicContrastSupported = IsSupportedSCEDynamicContrast,
                IsUser3DLutSupported = IsSupportedUser3DLUT,
                IsCurrentSceDisabled = IsCurrentSCEDisabled,
                IsCurrentSceVividGaming = IsCurrentSCEVividGaming,
                HasDynamicContrast = HasDynamicContrast,
                CurrentDynamicContrast = CurrentDynamicContrastValue,
                DynamicContrastRange = new ADLX_IntRange
                {
                    minValue = DynamicContrastRange.Min,
                    maxValue = DynamicContrastRange.Max,
                    step = DynamicContrastRange.Step
                }
            };
        }

        public override bool Equals(object obj) => obj is AMD_3DLUT_INFO other && this.Equals(other);
        public bool Equals(AMD_3DLUT_INFO other)
        {
            if (IsSupportedSCE != other.IsSupportedSCE)
            {
                SharedLogger.logger.Trace($"AMD_3DLUT_INFO/Equals: The IsSupportedSCE values don't equal each other");
                return false;
            }
            if (IsSupportedSCEVividGaming != other.IsSupportedSCEVividGaming)
            {
                SharedLogger.logger.Trace($"AMD_3DLUT_INFO/Equals: The IsSupportedSCEVividGaming values don't equal each other");
                return false;
            }
            if (IsSupportedUser3DLUT != other.IsSupportedUser3DLUT)
            {
                SharedLogger.logger.Trace($"AMD_3DLUT_INFO/Equals: The IsSupportedUser3DLUT values don't equal each other");
                return false;
            }
            if (IsCurrentSCEDisabled != other.IsCurrentSCEDisabled)
            {
                SharedLogger.logger.Trace($"AMD_3DLUT_INFO/Equals: The IsCurrentSCEDisabled values don't equal each other");
                return false;
            }
            if (IsCurrentSCEVividGaming != other.IsCurrentSCEVividGaming)
            {
                SharedLogger.logger.Trace($"AMD_3DLUT_INFO/Equals: The IsCurrentSCEVividGaming values don't equal each other");
                return false;
            }
            if (CurrentDynamicContrastValue != other.CurrentDynamicContrastValue)
            {
                SharedLogger.logger.Trace($"AMD_3DLUT_INFO/Equals: The CurrentDynamicContrastValue values don't equal each other");
                return false;
            }
            if (!DynamicContrastRange.Equals(other.DynamicContrastRange))
            {
                SharedLogger.logger.Trace($"AMD_3DLUT_INFO/Equals: The DynamicContrastRange values don't equal each other");
                return false;
            }
            return true;
        }
        public override int GetHashCode() => (IsSupportedSCE, IsSupportedSCEVividGaming, IsSupportedSCEDynamicContrast, IsSupportedUser3DLUT, IsCurrentSCEDisabled, IsCurrentSCEVividGaming, CurrentDynamicContrastValue, DynamicContrastRange).GetHashCode();
    }

    /// Range helper
    public struct IntRange: IEquatable<IntRange>
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Step { get; set; }
        public override bool Equals(object obj) => obj is IntRange other && this.Equals(other);
        public bool Equals(IntRange other)
        {
            if (Min != other.Min)
            {
                SharedLogger.logger.Trace($"IntRange/Equals: The Min values don't equal each other");
                return false;
            }
            if (Max != other.Max)
            {
                SharedLogger.logger.Trace($"IntRange/Equals: The Max values don't equal each other");
                return false;
            }
            if (Step != other.Step)
            {
                SharedLogger.logger.Trace($"IntRange/Equals: The Step values don't equal each other");
                return false;
            }
            return true;
        }
        public override int GetHashCode() => (Min, Max, Step).GetHashCode();
    }

    public enum LutMode
    {
        None = 0,
        SDR = 1,
        HDR = 2,
        All = 3
    }

    public struct AMD_DISPLAY_RESOLUTION_INFO : IEquatable<AMD_DISPLAY_RESOLUTION_INFO>
    {
        public int ResWidth { get; init; }
        public int ResHeight { get; init; }
        public int RefreshRate { get; init; }

        public AMD_DISPLAY_RESOLUTION_INFO()
        {
            ResWidth = 0;
            ResHeight = 0;
            RefreshRate = 0;
        }

        public AMD_DISPLAY_RESOLUTION_INFO(int resWidth, int resHeight, int refreshRate)
        {
            ResWidth = resWidth;
            ResHeight = resHeight;
            RefreshRate = refreshRate;
        }
        public AMD_DISPLAY_RESOLUTION_INFO(DisplayResolutionInfo displayResInfo)
        {
            ResWidth = displayResInfo.ResWidth;
            ResHeight = displayResInfo.ResHeight;
            RefreshRate = displayResInfo.RefreshRate;
        }
        public DisplayResolutionInfo ToDisplayResolutionInfo()
        {
            return new DisplayResolutionInfo
            {
                ResWidth = ResWidth,
                ResHeight = ResHeight,
                RefreshRate = RefreshRate
            };
        }

        public override bool Equals(object obj) => obj is AMD_DISPLAY_RESOLUTION_INFO other && this.Equals(other);
        public bool Equals(AMD_DISPLAY_RESOLUTION_INFO other)
        {
            if (ResWidth != other.ResWidth)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_RESOLUTION_INFO/Equals: The ResWidth values don't equal each other");
                return false;
            }
            if (ResHeight != other.ResHeight)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_RESOLUTION_INFO/Equals: The ResHeight values don't equal each other");
                return false;
            }
            if (RefreshRate != other.RefreshRate)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_RESOLUTION_INFO/Equals: The RefreshRate values don't equal each other");
                return false;
            }
            return true;
        }
        public override int GetHashCode() => (ResWidth, ResHeight, RefreshRate).GetHashCode();
    }

    public struct AMD_CUSTOM_DISPLAY_RESOLUTION_INFO : IEquatable<AMD_CUSTOM_DISPLAY_RESOLUTION_INFO>
    {
        public bool IsSupported { get; init; }
        public IReadOnlyList<AMD_DISPLAY_RESOLUTION_INFO> Resolutions { get; init; }

        public AMD_CUSTOM_DISPLAY_RESOLUTION_INFO()
        {
            IsSupported = false;
            Resolutions = Array.Empty<AMD_DISPLAY_RESOLUTION_INFO>();
        }

        public AMD_CUSTOM_DISPLAY_RESOLUTION_INFO(bool isSupported, IReadOnlyList<DisplayResolutionInfo> resolutions)
        {
            IsSupported = isSupported;
            foreach (var res in resolutions)
            {
                Resolutions.Append(new AMD_DISPLAY_RESOLUTION_INFO(res));
            }
        }
        public AMD_CUSTOM_DISPLAY_RESOLUTION_INFO(CustomResolutionInfo customResInfo)
        {
            IsSupported = customResInfo.IsSupported;
            Resolutions = new List<AMD_DISPLAY_RESOLUTION_INFO>();
            foreach (var res in customResInfo.Resolutions)
            {
                Resolutions.Append(new AMD_DISPLAY_RESOLUTION_INFO(res));
            }
        }

        public CustomResolutionInfo ToCustomResolutionInfo()
        {
            return new CustomResolutionInfo
            {
                IsSupported = IsSupported,
                Resolutions = Resolutions.Select(res => res.ToDisplayResolutionInfo()).ToList()
            };
        }
        public override bool Equals(object obj) => obj is AMD_CUSTOM_DISPLAY_RESOLUTION_INFO other && this.Equals(other);
        public bool Equals(AMD_CUSTOM_DISPLAY_RESOLUTION_INFO other)
        {
            if (IsSupported != other.IsSupported)
            {
                SharedLogger.logger.Trace($"AMD_CUSTOM_DISPLAY_RESOLUTION_INFO/Equals: The IsSupported values don't equal each other");
                return false;
            }
            if (!Resolutions.SequenceEqual(other.Resolutions))
            {
                SharedLogger.logger.Trace($"AMD_CUSTOM_DISPLAY_RESOLUTION_INFO/Equals: The Resolutions values don't equal each other");
                return false;
            }
            return true;
        }
        public override int GetHashCode() => (IsSupported, Resolutions).GetHashCode();
    }

    public struct AMD_CONNECTIVITY_EXPERIENCE_INFO : IEquatable<AMD_CONNECTIVITY_EXPERIENCE_INFO>
    {
        public bool IsHdmiQualityDetectionSupported { get; init; }
        public bool IsHdmiQualityDetectionEnabled { get; init; }
        public bool IsDpLinkRateSupported { get; init; }
        public ADLX_DP_LINK_RATE DpLinkRate { get; init; }
        public bool IsRelativePreEmphasisSupported { get; init; }
        public int RelativePreEmphasis { get; init; }
        public bool IsRelativeVoltageSwingSupported { get; init; }
        public int RelativeVoltageSwing { get; init; }

        public AMD_CONNECTIVITY_EXPERIENCE_INFO()
        {
            IsHdmiQualityDetectionSupported = false;
            IsHdmiQualityDetectionEnabled = false;
            IsDpLinkRateSupported = false;
            DpLinkRate = ADLX_DP_LINK_RATE.DP_LINK_RATE_UNKNOWN;
            IsRelativePreEmphasisSupported = false;
            RelativePreEmphasis = 0;
            IsRelativeVoltageSwingSupported = false;
            RelativeVoltageSwing = 0;
        }

        public AMD_CONNECTIVITY_EXPERIENCE_INFO(bool isHdmiQualityDetectionSupported, bool isHdmiQualityDetectionEnabled,
                                               bool isDpLinkRateSupported, ADLX_DP_LINK_RATE dpLinkRate,
                                               bool isRelativePreEmphasisSupported, int relativePreEmphasis,
                                               bool isRelativeVoltageSwingSupported, int relativeVoltageSwing)
        {
            IsHdmiQualityDetectionSupported = isHdmiQualityDetectionSupported;
            IsHdmiQualityDetectionEnabled = isHdmiQualityDetectionEnabled;
            IsDpLinkRateSupported = isDpLinkRateSupported;
            DpLinkRate = dpLinkRate;
            IsRelativePreEmphasisSupported = isRelativePreEmphasisSupported;
            RelativePreEmphasis = relativePreEmphasis;
            IsRelativeVoltageSwingSupported = isRelativeVoltageSwingSupported;
            RelativeVoltageSwing = relativeVoltageSwing;
        }

        public AMD_CONNECTIVITY_EXPERIENCE_INFO(ConnectivityExperienceInfo ConnExp)
        {
            IsHdmiQualityDetectionSupported = ConnExp.IsHdmiQualityDetectionSupported;
            IsHdmiQualityDetectionEnabled = ConnExp.IsHdmiQualityDetectionEnabled;
            IsDpLinkRateSupported = ConnExp.IsDpLinkRateSupported;
            DpLinkRate = ConnExp.DpLinkRate;
            IsRelativePreEmphasisSupported = ConnExp.IsRelativePreEmphasisSupported;
            RelativePreEmphasis = ConnExp.RelativePreEmphasis;
            IsRelativeVoltageSwingSupported = ConnExp.IsRelativeVoltageSwingSupported;
            RelativeVoltageSwing = ConnExp.RelativeVoltageSwing;
        }

        public ConnectivityExperienceInfo ToConnectivityExperienceInfo()
        {
            return new ConnectivityExperienceInfo
            {
                IsHdmiQualityDetectionSupported = IsHdmiQualityDetectionSupported,
                IsHdmiQualityDetectionEnabled = IsHdmiQualityDetectionEnabled,
                IsDpLinkRateSupported = IsDpLinkRateSupported,
                DpLinkRate = DpLinkRate,
                IsRelativePreEmphasisSupported = IsRelativePreEmphasisSupported,
                RelativePreEmphasis = RelativePreEmphasis,
                IsRelativeVoltageSwingSupported = IsRelativeVoltageSwingSupported,
                RelativeVoltageSwing = RelativeVoltageSwing
            };
        }   

        public override bool Equals(object obj) => obj is AMD_CONNECTIVITY_EXPERIENCE_INFO other && this.Equals(other);
        public bool Equals(AMD_CONNECTIVITY_EXPERIENCE_INFO other)
        {
            if (IsHdmiQualityDetectionSupported != other.IsHdmiQualityDetectionSupported)
            {
                SharedLogger.logger.Trace($"AMD_CONNECTIVITY_EXPERIENCE_INFO/Equals: The IsHdmiQualityDetectionSupported values don't equal each other");
                return false;
            }
            if (IsHdmiQualityDetectionEnabled != other.IsHdmiQualityDetectionEnabled)
            {
                SharedLogger.logger.Trace($"AMD_CONNECTIVITY_EXPERIENCE_INFO/Equals: The IsHdmiQualityDetectionEnabled values don't equal each other");
                return false;
            }
            if (IsDpLinkRateSupported != other.IsDpLinkRateSupported)
            {
                SharedLogger.logger.Trace($"AMD_CONNECTIVITY_EXPERIENCE_INFO/Equals: The IsDpLinkRateSupported values don't equal each other");
                return false;
            }
            if (DpLinkRate != other.DpLinkRate)
            {
                SharedLogger.logger.Trace($"AMD_CONNECTIVITY_EXPERIENCE_INFO/Equals: The DpLinkRate values don't equal each other");
                return false;
            }
            if (IsRelativePreEmphasisSupported != other.IsRelativePreEmphasisSupported)
            {
                SharedLogger.logger.Trace($"AMD_CONNECTIVITY_EXPERIENCE_INFO/Equals: The IsRelativePreEmphasisSupported values don't equal each other");
                return false;
            }
            if (RelativePreEmphasis != other.RelativePreEmphasis)
            {
                SharedLogger.logger.Trace($"AMD_CONNECTIVITY_EXPERIENCE_INFO/Equals: The RelativePreEmphasis values don't equal each other");
                return false;
            }
            if (IsRelativeVoltageSwingSupported != other.IsRelativeVoltageSwingSupported)
            {
                SharedLogger.logger.Trace($"AMD_CONNECTIVITY_EXPERIENCE_INFO/Equals: The IsRelativeVoltageSwingSupported values don't equal each other");
                return false;
            }
            if (RelativeVoltageSwing != other.RelativeVoltageSwing)
            {
                SharedLogger.logger.Trace($"AMD_CONNECTIVITY_EXPERIENCE_INFO/Equals: The RelativeVoltageSwing values don't equal each other");
                return false;
            }
            return true;
        }
        public override int GetHashCode() => (IsHdmiQualityDetectionSupported, IsHdmiQualityDetectionEnabled, IsDpLinkRateSupported, DpLinkRate, 
        IsRelativePreEmphasisSupported, RelativePreEmphasis, IsRelativeVoltageSwingSupported, RelativeVoltageSwing).GetHashCode();
    }

    public struct AMD_GAMUT_COLOR_SPACE : IEquatable<AMD_GAMUT_COLOR_SPACE>
    {
        public int RedX { get; init; }
        public int RedY { get; init; }
        public int GreenX { get; init; }
        public int GreenY { get; init; }
        public int BlueX { get; init; }
        public int BlueY { get; init; }

        public AMD_GAMUT_COLOR_SPACE()
        {
            RedX = 0;
            RedY = 0;
            GreenX = 0;
            GreenY = 0;
            BlueX = 0;
            BlueY = 0;
        }

        public AMD_GAMUT_COLOR_SPACE(int redX, int redY, int greenX, int greenY, int blueX, int blueY)
        {
            RedX = redX;
            RedY = redY;
            GreenX = greenX;
            GreenY = greenY;
            BlueX = blueX;
            BlueY = blueY;
        }

        public AMD_GAMUT_COLOR_SPACE(ADLX_GamutColorSpace gamutColorSpace)
        {
            RedX = gamutColorSpace.red.x;
            RedY = gamutColorSpace.red.y;
            GreenX = gamutColorSpace.green.x;
            GreenY = gamutColorSpace.green.y;
            BlueX = gamutColorSpace.blue.x;
            BlueY = gamutColorSpace.blue.y;
        }

        public ADLX_GamutColorSpace ToGamutColorSpace()
        {
            return new ADLX_GamutColorSpace
            {
                red = new ADLX_Point { x = RedX, y = RedY },
                green = new ADLX_Point { x = GreenX, y = GreenY },
                blue = new ADLX_Point { x = BlueX, y = BlueY }
            };
        }

        public override bool Equals(object obj) => obj is AMD_GAMUT_COLOR_SPACE other && this.Equals(other);
        public bool Equals(AMD_GAMUT_COLOR_SPACE other)
        {
            if (RedX != other.RedX)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_COLOR_SPACE/Equals: The RedX values don't equal each other");
                return false;
            }
            if (RedY != other.RedY)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_COLOR_SPACE/Equals: The RedY values don't equal each other");
                return false;
            }
            if (GreenX != other.GreenX)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_COLOR_SPACE/Equals: The GreenX values don't equal each other");
                return false;
            }
            if (GreenY != other.GreenY)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_COLOR_SPACE/Equals: The GreenY values don't equal each other");
                return false;
            }
            if (BlueX != other.BlueX)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_COLOR_SPACE/Equals: The BlueX values don't equal each other");
                return false;
            }
            if (BlueY != other.BlueY)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_COLOR_SPACE/Equals: The BlueY values don't equal each other");
                return false;
            }
            return true;
        }
        public override int GetHashCode() => (RedX, RedY, GreenX, GreenY, BlueX, BlueY).GetHashCode();
    }

    public struct AMD_GAMUT_INFO : IEquatable<AMD_GAMUT_INFO>
    {
        public bool IsWhitePointSupported { get; init; }
        public bool IsGamutSupported { get; init; }
        public bool IsCurrent5000K { get; init; }
        public bool IsCurrent6500K { get; init; }
        public bool IsCurrent7500K { get; init; }
        public bool IsCurrent9300K { get; init; }
        public bool IsCurrentCustomWhitePoint { get; init; }
        public bool IsCurrent709 { get; init; }
        public bool IsCurrent601 { get; init; }
        public bool IsCurrentAdobe { get; init; }
        public bool IsCurrentCieRgb { get; init; }
        public bool IsCurrent2020 { get; init; }
        public bool IsCurrentCustomColorSpace { get; init; }
        public AMD_GAMUT_COLOR_SPACE CurrentGamutSpace { get; init; }
        public int WhitePointX { get; init; }
        public int WhitePointY { get; init; }
        public bool HasWhitePoint { get; init; }

        public AMD_GAMUT_INFO()
        {
            IsWhitePointSupported = false;
            IsGamutSupported = false;
            IsCurrent5000K = false;
            IsCurrent6500K = false;
            IsCurrent7500K = false;
            IsCurrent9300K = false;
            IsCurrentCustomWhitePoint = false;
            IsCurrent709 = false;
            IsCurrent601 = false;
            IsCurrentAdobe = false;
            IsCurrentCieRgb = false;
            IsCurrent2020 = false;
            IsCurrentCustomColorSpace = false;
            CurrentGamutSpace = new AMD_GAMUT_COLOR_SPACE();
            WhitePointX = 0;
            WhitePointY = 0;
            HasWhitePoint = false;
        }

        public AMD_GAMUT_INFO(bool isWhitePointSupported, bool isGamutSupported,
                             bool isCurrent5000K, bool isCurrent6500K, bool isCurrent7500K, bool isCurrent9300K,
                             bool isCurrentCustomWhitePoint, bool isCurrent709, bool isCurrent601,
                             bool isCurrentAdobe, bool isCurrentCieRgb, bool isCurrent2020,
                             bool isCurrentCustomColorSpace, ADLX_GamutColorSpace currentGamutSpace,
                             ADLX_Point whitePoint,  bool hasWhitePoint)
        {
            IsWhitePointSupported = isWhitePointSupported;
            IsGamutSupported = isGamutSupported;
            IsCurrent5000K = isCurrent5000K;
            IsCurrent6500K = isCurrent6500K;
            IsCurrent7500K = isCurrent7500K;
            IsCurrent9300K = isCurrent9300K;
            IsCurrentCustomWhitePoint = isCurrentCustomWhitePoint;
            IsCurrent709 = isCurrent709;
            IsCurrent601 = isCurrent601;
            IsCurrentAdobe = isCurrentAdobe;
            IsCurrentCieRgb = isCurrentCieRgb;
            IsCurrent2020 = isCurrent2020;
            IsCurrentCustomColorSpace = isCurrentCustomColorSpace;
            CurrentGamutSpace = new AMD_GAMUT_COLOR_SPACE(currentGamutSpace);
            WhitePointX = whitePoint.x;
            WhitePointY = whitePoint.y;
            HasWhitePoint = hasWhitePoint;
        }

        public AMD_GAMUT_INFO(GamutInfo gamutInfo)
        {
            IsWhitePointSupported = gamutInfo.IsWhitePointSupported;
            IsGamutSupported = gamutInfo.IsGamutSupported;
            IsCurrent5000K = gamutInfo.IsCurrent5000K;
            IsCurrent6500K = gamutInfo.IsCurrent6500K;
            IsCurrent7500K = gamutInfo.IsCurrent7500K;
            IsCurrent9300K = gamutInfo.IsCurrent9300K;
            IsCurrentCustomWhitePoint = gamutInfo.IsCurrentCustomWhitePoint;
            IsCurrent709 = gamutInfo.IsCurrent709;
            IsCurrent601 = gamutInfo.IsCurrent601;
            IsCurrentAdobe = gamutInfo.IsCurrentAdobe;
            IsCurrentCieRgb = gamutInfo.IsCurrentCieRgb;
            IsCurrent2020 = gamutInfo.IsCurrent2020;
            IsCurrentCustomColorSpace = gamutInfo.IsCurrentCustomColorSpace;
            CurrentGamutSpace = new AMD_GAMUT_COLOR_SPACE(gamutInfo.CurrentGamutSpace);
            WhitePointX = gamutInfo.WhitePoint.x;
            WhitePointY = gamutInfo.WhitePoint.y;
            HasWhitePoint = gamutInfo.HasWhitePoint;
        }

        public GamutInfo ToGamutInfo()
        {
            return new GamutInfo
            {
                IsWhitePointSupported = IsWhitePointSupported,
                IsGamutSupported = IsGamutSupported,
                IsCurrent5000K = IsCurrent5000K,
                IsCurrent6500K = IsCurrent6500K,
                IsCurrent7500K = IsCurrent7500K,
                IsCurrent9300K = IsCurrent9300K,
                IsCurrentCustomWhitePoint = IsCurrentCustomWhitePoint,
                IsCurrent709 = IsCurrent709,
                IsCurrent601 = IsCurrent601,
                IsCurrentAdobe = IsCurrentAdobe,
                IsCurrentCieRgb = IsCurrentCieRgb,
                IsCurrent2020 = IsCurrent2020,
                IsCurrentCustomColorSpace = IsCurrentCustomColorSpace,
                CurrentGamutSpace = CurrentGamutSpace.ToGamutColorSpace(),
                WhitePoint = new ADLX_Point { x = WhitePointX, y = WhitePointY },
                HasWhitePoint = HasWhitePoint
            };
        }

        public override bool Equals(object obj) => obj is AMD_GAMUT_INFO other && this.Equals(other);
        public bool Equals(AMD_GAMUT_INFO other)
        {
            if (IsWhitePointSupported != other.IsWhitePointSupported)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_INFO/Equals: The IsWhitePointSupported values don't equal each other");
                return false;
            }
            if (IsGamutSupported != other.IsGamutSupported)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_INFO/Equals: The IsGamutSupported values don't equal each other");
                return false;
            }
            if (IsCurrent5000K != other.IsCurrent5000K)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_INFO/Equals: The IsCurrent5000K values don't equal each other");
                return false;
            }
            if (IsCurrent6500K != other.IsCurrent6500K)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_INFO/Equals: The IsCurrent6500K values don't equal each other");
                return false;
            }
            if (IsCurrent7500K != other.IsCurrent7500K)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_INFO/Equals: The IsCurrent7500K values don't equal each other");
                return false;
            }
            if (IsCurrent9300K != other.IsCurrent9300K)
            {
                SharedLogger.logger.Trace($"AMD_GAMUT_INFO/Equals: The IsCurrent9300K values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode() => (IsWhitePointSupported, IsGamutSupported, IsCurrent5000K, IsCurrent6500K, IsCurrent7500K, IsCurrent9300K).GetHashCode();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AMD_DISPLAY_WITH_SETTINGS : IEquatable<AMD_DISPLAY_WITH_SETTINGS>
    {
        public ADLX_DISPLAY_CONNECTOR_TYPE ConnectorType;
        public ADLX_DISPLAY_TYPE Type;
        public string EDID;
        public uint ManufacturerID;
        public string Name;
        public int NativeResolutionWidth;
        public int NativeResolutionHeight;
        public uint PixelClock;
        public double RefreshRate;
        public ADLX_DISPLAY_SCAN_TYPE ScanType;
        public ulong UniqueID;
        public int GpuUniqueID;
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
        public bool IsSupportedFreeSync;
        public bool IsEnabledFreeSync;        
        public bool IsSupportedFreeSyncColorAccuracy;
        public bool IsEnabledFreeSyncColorAccuracy;        
        public bool IsSupportedDynamicRefreshRateControl;
        public bool IsEnabledDynamicRefreshRateControl;
        public bool IsSupportedDisplayBlanking;
        public bool IsEnabledDisplayBlanking;
        public bool IsSupportedGamma;
        public AMD_GAMUT_INFO GamutInfo;
        public bool IsSupportedGPUScaling;
        public bool IsEnabledGPUScaling;
        public bool IsSupportedIntegerScaling;
        public bool IsEnabledIntegerScaling;
        public bool IsSupportedPixelFormat;
        public ADLX_PIXEL_FORMAT CurrentPixelFormat = ADLX_PIXEL_FORMAT.FORMAT_UNKNOWN;
        public bool IsSupportedScalingMode;
        public ADLX_SCALE_MODE CurrentScalingMode = ADLX_SCALE_MODE.PRESERVE_ASPECT_RATIO;
        public bool IsSupportedVSR = false;
        public bool IsEnabledVSR = false;
        public bool IsSupportedHDCP = false;
        public bool IsEnabledHDCP = false;
        public bool IsSupportedVariBright = false;
        public bool IsEnabledVariBright = false;
        public VariBrightMode VariBrightMode = VariBrightMode.Unknown;
        public AMD_CONNECTIVITY_EXPERIENCE_INFO ConnectivityExperience;
        public bool IsSupported3DLUT = false;
        public AMD_3DLUT_INFO ThreeDLUTSettings { get; set; } = new AMD_3DLUT_INFO();
        // Custom resolution support / status
        public bool IsSupportedCustomResolution { get; set; }
        public bool IsCustomResolutionApplied { get; set; }

        // The details of the currently applied custom resolution (if any)
        public int CustomResWidth { get; set; }
        public int CustomResHeight { get; set; }
        public int CustomResRefreshRate { get; set; }
        public int CustomResTimingStandard { get; set; }

        // (Optional) store the list of all defined custom resolutions for that display
        public List<AMD_CUSTOM_DISPLAY_RESOLUTION_INFO> CustomResolutions { get; set; } = new List<AMD_CUSTOM_DISPLAY_RESOLUTION_INFO>();

        public AMD_DISPLAY_WITH_SETTINGS()
        {
            EDID = "";
            Name = "";
            ScanType = ADLX_DISPLAY_SCAN_TYPE.PROGRESSIVE;
            ConnectorType = ADLX_DISPLAY_CONNECTOR_TYPE.DISPLAY_CONTYPE_UNKNOWN;
            Type = ADLX_DISPLAY_TYPE.DISPLAY_TYPE_UNKOWN;
            ColorDepth = ADLX_COLOR_DEPTH.BPC_UNKNOWN;
            CurrentPixelFormat = ADLX_PIXEL_FORMAT.FORMAT_UNKNOWN;
            CurrentScalingMode = ADLX_SCALE_MODE.PRESERVE_ASPECT_RATIO;
            CustomResolutions = new List<AMD_CUSTOM_DISPLAY_RESOLUTION_INFO>();
            ThreeDLUTSettings = new AMD_3DLUT_INFO();
            GamutInfo = new AMD_GAMUT_INFO();
            VariBrightMode = VariBrightMode.Unknown;
        }

        public override bool Equals(object obj) => obj is AMD_DISPLAY_WITH_SETTINGS other && this.Equals(other);
        public bool Equals(AMD_DISPLAY_WITH_SETTINGS other)
        {
            if (ConnectorType != other.ConnectorType)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The ConnectorType values don't equal each other");
                return false;
            }
            if (Type != other.Type)
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
            if (NativeResolutionWidth != other.NativeResolutionWidth)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The NativeResolutionWidth values don't equal each other");
                return false;
            }
            if (NativeResolutionHeight != other.NativeResolutionHeight)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The NativeResolutionHeight values don't equal each other");
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
            if (IsSupportedColorDepth != other.IsSupportedColorDepth)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedColorDepth values don't equal each other");
                return false;
            }
            if (ColorDepth != other.ColorDepth)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The ColorDepth values don't equal each other");
                return false;
            }
            if (IsSupportedCustomColorBrightness != other.IsSupportedCustomColorBrightness)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedCustomColorBrightness values don't equal each other");
                return false;
            }
            if (CustomColorBrightness != other.CustomColorBrightness)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CustomColorBrightness values don't equal each other");
                return false;
            }
            if (IsSupportedCustomColorHue != other.IsSupportedCustomColorHue)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedCustomColorHue values don't equal each other");
                return false;
            }
            if (CustomColorHue != other.CustomColorHue)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CustomColorHue values don't equal each other");
                return false;
            }
            if (IsSupportedCustomColorSaturation != other.IsSupportedCustomColorSaturation)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedCustomColorSaturation values don't equal each other");
                return false;
            }
            if (CustomColorSaturation != other.CustomColorSaturation)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CustomColorSaturation values don't equal each other");
                return false;
            }
            if (IsSupportedCustomColorContrast != other.IsSupportedCustomColorContrast)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedCustomColorContrast values don't equal each other");
                return false;
            }
            if (CustomColorContrast != other.CustomColorContrast)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CustomColorContrast values don't equal each other");
                return false;
            }
            if (IsSupportedCustomColorTemperature != other.IsSupportedCustomColorTemperature)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedCustomColorTemperature values don't equal each other");
                return false;
            }
            if (CustomColorTemperature != other.CustomColorTemperature)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CustomColorTemperature values don't equal each other");
                return false;
            }
            if (IsSupportedGamma != other.IsSupportedGamma)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedGamma values don't equal each other");
                return false;
            }
            if (IsSupportedFreeSync != other.IsSupportedFreeSync)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedFreeSync values don't equal each other");
                return false;
            }
            if (IsEnabledFreeSync != other.IsEnabledFreeSync)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsEnabledFreeSync values don't equal each other");
                return false;
            }
            if (IsSupportedGPUScaling != other.IsSupportedGPUScaling)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedGPUScaling values don't equal each other");
                return false;
            }
            if (IsEnabledGPUScaling != other.IsEnabledGPUScaling)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsEnabledGPUScaling values don't equal each other");
                return false;
            }
            if (IsSupportedIntegerScaling != other.IsSupportedIntegerScaling)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedIntegerScaling values don't equal each other");
                return false;
            }
            if (IsEnabledIntegerScaling != other.IsEnabledIntegerScaling)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsEnabledIntegerScaling values don't equal each other");
                return false;
            }
            if (IsSupportedPixelFormat != other.IsSupportedPixelFormat)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedPixelFormat values don't equal each other");
                return false;
            }
            if (CurrentPixelFormat != other.CurrentPixelFormat)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CurrentPixelFormat values don't equal each other");
                return false;
            }
            if (IsSupportedScalingMode != other.IsSupportedScalingMode)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedScalingMode values don't equal each other");
                return false;
            }
            if (CurrentScalingMode != other.CurrentScalingMode)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CurrentScalingMode values don't equal each other");
                return false;
            }
            if (IsSupportedVSR != other.IsSupportedVSR)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedVSR values don't equal each other");
                return false;
            }
            if (IsEnabledVSR != other.IsEnabledVSR)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsEnabledVSR values don't equal each other");
                return false;
            }  
            if (IsSupportedCustomResolution != other.IsSupportedCustomResolution)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedCustomResolution values don't equal each other");
                return false;
            }
            if (IsCustomResolutionApplied != other.IsCustomResolutionApplied)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The IsCustomResolutionApplied values don't equal each other");
                return false;
            }
            if (CustomResWidth != other.CustomResWidth)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CustomResWidth values don't equal each other");
                return false;
            }
            if (CustomResHeight != other.CustomResHeight)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CustomResHeight values don't equal each other");
                return false;
            }
            if (CustomResRefreshRate != other.CustomResRefreshRate)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CustomResRefreshRate values don't equal each other");
                return false;
            }
            if (CustomResTimingStandard != other.CustomResTimingStandard)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CustomResTimingStandard values don't equal each other");
                return false;
            }
            if (!CustomResolutions.SequenceEqual(other.CustomResolutions))
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The CustomResolutions values don't equal each other");
                return false;
            }
            if (!ConnectivityExperience.Equals(other.ConnectivityExperience))
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The ConnectivityExperience values don't equal each other");
                return false;
            }
            if (!ThreeDLUTSettings.Equals(other.ThreeDLUTSettings))
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_WITH_SETTINGS/Equals: The ThreeDLUTSettings values don't equal each other");
                return false;
            }

            return true;
        }

        // Replace the GetHashCode method in AMD_DISPLAY_WITH_SETTINGS with the following:
        public override int GetHashCode()
        {
            return (ConnectorType, Type, EDID, ManufacturerID, Name, NativeResolutionWidth, NativeResolutionHeight, PixelClock, RefreshRate, ScanType, UniqueID,
                IsSupportedColorDepth, ColorDepth, IsSupportedCustomColorBrightness, CustomColorBrightness, IsSupportedCustomColorHue, CustomColorHue,
                IsSupportedCustomColorSaturation, CustomColorSaturation, IsSupportedCustomColorContrast, CustomColorContrast, IsSupportedCustomColorTemperature,
                CustomColorTemperature, IsSupportedGamma, IsSupportedFreeSync, IsEnabledFreeSync, IsSupportedGPUScaling, IsEnabledGPUScaling,
                IsSupportedIntegerScaling, IsEnabledIntegerScaling, IsSupportedPixelFormat, CurrentPixelFormat, IsSupportedScalingMode, CurrentScalingMode,
                IsSupportedVSR, IsEnabledVSR, IsSupportedCustomResolution, IsCustomResolutionApplied, CustomResWidth, CustomResHeight, CustomResRefreshRate
            ).GetHashCode();
        }
        public static bool operator ==(AMD_DISPLAY_WITH_SETTINGS lhs, AMD_DISPLAY_WITH_SETTINGS rhs) => lhs.Equals(rhs);

        public static bool operator !=(AMD_DISPLAY_WITH_SETTINGS lhs, AMD_DISPLAY_WITH_SETTINGS rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AMD_DISPLAY : IEquatable<AMD_DISPLAY>
    {
       public ADLX_DISPLAY_CONNECTOR_TYPE ConnectorType;
        public ADLX_DISPLAY_TYPE Type;
        public string EDID;
        public uint ManufacturerID;
        public string Name;
        public int NativeResolutionWidth;
        public int NativeResolutionHeight;
        public uint PixelClock;
        public double RefreshRate;
        public ADLX_DISPLAY_SCAN_TYPE ScanType;
        public ulong UniqueID;
        public int GpuUniqueID;

        public AMD_DISPLAY()
        {
            EDID = "";
            Name = "";
            ScanType = ADLX_DISPLAY_SCAN_TYPE.PROGRESSIVE;
            ConnectorType = ADLX_DISPLAY_CONNECTOR_TYPE.DISPLAY_CONTYPE_UNKNOWN;
            Type = ADLX_DISPLAY_TYPE.DISPLAY_TYPE_UNKOWN;
        }

        public override bool Equals(object obj) => obj is AMD_DISPLAY other && this.Equals(other);
        public bool Equals(AMD_DISPLAY other)
        {
            if (ConnectorType != other.ConnectorType)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The ConnectorType values don't equal each other");
                return false;
            }
            if (Type != other.Type)
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
            if (NativeResolutionWidth  != other.NativeResolutionWidth)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The NativeResolutionWidth values don't equal each other");
                return false;
            }
            if (NativeResolutionHeight != other.NativeResolutionHeight)
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY/Equals: The NativeResolutionHeight values don't equal each other");
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
            return (ConnectorType, Type, EDID, ManufacturerID, Name, NativeResolutionWidth, NativeResolutionHeight, PixelClock, RefreshRate, ScanType, UniqueID).GetHashCode();
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
        public Dictionary<ulong,AMD_DISPLAY_WITH_SETTINGS> Displays;
        public AMD_SLS_CONFIG Adl2SlsConfig;
        public List<string> DisplayIdentifiers;

        public AMD_DISPLAY_CONFIG()
        {
            IsInUse = false;
            IsCloned = false;
            IsEyefinity = false;
            Desktops = new List<AMD_DESKTOP>();
            EyefinityDesktop = new AMD_EYEFINITY_DESKTOP();
            Displays = new Dictionary<ulong,AMD_DISPLAY_WITH_SETTINGS>();
            Adl2SlsConfig = new AMD_SLS_CONFIG();
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
            if (!Adl2SlsConfig.Equals(other.Adl2SlsConfig))
            {
                SharedLogger.logger.Trace($"AMD_DISPLAY_CONFIG/Equals: The Adl2SlsConfig values don't equal each other");
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
            return (IsInUse, IsCloned, Desktops, IsEyefinity, EyefinityDesktop, Displays, Adl2SlsConfig, DisplayIdentifiers).GetHashCode();
        }

        public static bool operator ==(AMD_DISPLAY_CONFIG lhs, AMD_DISPLAY_CONFIG rhs) => lhs.Equals(rhs);

        public static bool operator !=(AMD_DISPLAY_CONFIG lhs, AMD_DISPLAY_CONFIG rhs) => !(lhs == rhs);
    }

    class AMDLibrary : IDisposable
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
        private static AMDLibrary _instance = new AMDLibrary();

        private bool _initialised = false;
        private bool _initialisedADL2 = false;

        // To detect redundant calls
        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        private IntPtr _adlContextHandle = IntPtr.Zero;
        private ADLXApiHelper _adlxHelper;
        //private ADLXHelper _adlxHelper;
        private ADLXSystemServicesHelper _adlxSystem;
        //private int _adlxHighestSupportedSystemVersion = 0; // Only the base SystemServices is supported in all versions of ADLX
        private AMD_DISPLAY_CONFIG? _activeDisplayConfig;
        public List<ADL_DISPLAY_CONNECTION_TYPE> SkippedColorConnectionTypes;
        public List<string> _allConnectedDisplayIdentifiers;
        //public IntPtr hADLXBindingModule = IntPtr.Zero;
        public IntPtr hADLXModule = IntPtr.Zero;
        public const string AMD_ADLX_BINDING_DLL = "ADLXWrapper.dll";
        public const string AMD_ADLX_DLL = "amdadlx64.dll";

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
                _initialised = false;
                // Check if there is AMD hardware installed
                SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Looking for AMD PCI hardware...");
                if (WinLibrary.IsPCIVideoCardVendorInstalled(PCIVendorIDs))
                {
                    SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: AMD hardware detected");
                }
                else
                {
                    SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: No AMD hardware detected");
                    return;
                }

                try
                {
                    // Attempt to load the AMD ADLX 64-bit DLL
                    SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Attempting to load the AMD ADLX DLL {AMD_ADLX_DLL} to confirm it's available for use by our ADLX Csharp Binding DLL");
                    hADLXModule = LoadLibrary(AMD_ADLX_DLL);
                    if (hADLXModule != IntPtr.Zero)
                    {
                        // Successfully loaded the ADLX DLL, which means it's installed!
                        SharedLogger.logger.Trace("AMDLibrary/AMDLibrary: We successfully loaded the AMD ADLX DLL which means the AMD Adrenalin driver software is installed.");
                    }
                    else
                    {
                        // LoadLibrary failed, DLL is not available
                        _initialised = false;
                        SharedLogger.logger.Error("AMDLibrary/AMDLibrary: Failed to load the AMD ADLX DLL. You need to download and install the AMD Adrenalin software from the AMD support website in order to fully support AMD hardware.");
                        return;
                    }

                    // Attempt to load the Custom ADLX Binding DLL
                    /*SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Attempting to load the AMD ADLXWrapper DLL {AMD_ADLX_BINDING_DLL} so we can access the AMD ADLX DLL from C#");
                    hADLXBindingModule = LoadLibrary(AMD_ADLX_BINDING_DLL);
                    if (hADLXBindingModule != IntPtr.Zero)
                    {
                        // Attempt to get the address of a non-existent function to verify the DLL is loaded
                        // IntPtr procAddress = GetProcAddress(hModule, "fakefunction");
                        // If GetProcAddress returns IntPtr.Zero, the function doesn't exist, which is expected
                        // The key point is that LoadLibrary succeeded, indicating the DLL is present
                        // Free the loaded library if we're exiting now, to avoid memory leaks
                        //FreeLibrary(hModule);

                        // Successfully loaded our custom ADLX Binding DLL, which means it's installed!
                        _initialised = true;
                        SharedLogger.logger.Trace("AMDLibrary/AMDLibrary: We successfully loaded our custom AMD ADLX Binding DLL! We can use the AMD ADLX API");
                    }
                    else
                    {
                        // LoadLibrary failed, DLL is not available
                        _initialised = false;
                        SharedLogger.logger.Error("AMDLibrary/AMDLibrary: Failed to load the AMD ADLX Binding DLL.");
                        return;
                    }*/
                }
                catch (Exception ex)
                {
                    _initialised = false;
                    SharedLogger.logger.Error(ex, "AMDLibrary/AMDLibrary: Exception while trying to load the AMD ADLX DLL or our AMD ADLX CSharp Binding DLL. You may need to install the AMD driver.");
                }

                SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Attempting to load the AMD ADL2 DLL {ADLImport.ATI_ADL_DLL} to use for setting AMD Eyefinity (it seems more reliable)");
                try
                {
                    _initialisedADL2 = false;
                    Marshal.PrelinkAll(typeof(ADLImport));
                    SharedLogger.logger.Trace("AMDLibrary/AMDLibrary: Successfully loaded the AMD ADL2 DLL.");
                    try
                    {
                        ADL_STATUS ADLRet;
                        ADLRet = ADLImport.ADL2_Main_Control_Create(ADLImport.ADL_Main_Memory_Alloc, ADLImport.ADL_TRUE, out _adlContextHandle);
                        if (ADLRet == ADL_STATUS.ADL_OK)
                        {
                            _initialisedADL2 = true;
                            SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: AMD ADL2 library was initialised successfully");
                        }
                        else
                        {
                            _initialisedADL2 = false;
                            SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Error intialising AMD ADL2 library. ADL2_Main_Control_Create() returned error code {ADLRet}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _initialisedADL2 = false;
                        SharedLogger.logger.Trace(ex, $"AMDLibrary/AMDLibrary: Exception intialising AMD ADL2 library. ADL2_Main_Control_Create() caused an exception.");
                    }
                }
                catch (Exception ex)
                {
                    _initialisedADL2 = false;
                    SharedLogger.logger.Error(ex, "AMDLibrary/AMDLibrary: Exception whie trying to load the AMD ADL DLL. You may need to install the AMD driver.");
                }

                // We set the environment variable as a workaround so that ADL2_Display_SLSMapConfigX2_Get works :(
                // This is a weird thing that AMD even set in their own code! WTF! Who programmed that as a feature?
                Environment.SetEnvironmentVariable("ADL_4KWORKAROUND_CANCEL", "TRUE");


                // Initialize ADLX with ADLXHelper
                //_adlxHelper = new ADLXHelper();
                SharedLogger.logger.Trace("AMDLibrary/AMDLibrary: Intialising AMD ADLX Helper interface");
                try
                {
                    _adlxHelper = ADLXApiHelper.Initialize();
                    try
                    {
                        // Get system services
                        SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Successfully intialised AMD ADLX Helper.");
                        SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Attemping to access AMD ADLX System Services.");
                        _adlxSystem = _adlxHelper.GetSystemServices();
                        _initialised = true;
                        //_adlxHighestSupportedSystemVersion = 0;
                        SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Successfully got AMD ADLX System Services.");
                        SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: AMD ADLX library was initialised successfully");
                    }
                    catch (Exception ex)
                    {
                        SharedLogger.logger.Trace(ex, $"AMDLibrary/AMDLibrary: Exception getting the ADLX System Services");
                        SharedLogger.logger.Trace(ex, $"AMDLibrary/AMDLibrary: Disposing the ADLXHelper to avoid memory leaks");
                        _adlxHelper.Dispose();
                        SharedLogger.logger.Trace(ex, $"AMDLibrary/AMDLibrary: Setting ADLXHelper to null");
                        _adlxHelper = null;
                        _initialised = false;
                        return;
                    }

                    SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Automatically getting the AMD Display Configuration");
                    _activeDisplayConfig = GetActiveConfig();
                    SharedLogger.logger.Trace($"AMDLibrary/AMDLibrary: Automatically getting the AMD Connected Display Identifiers");
                    _allConnectedDisplayIdentifiers = GetAllConnectedDisplayIdentifiers(out bool failure);

                }
                catch (Exception ex)
                {
                    SharedLogger.logger.Trace(ex, $"AMDLibrary/AMDLibrary: Exception intialising AMD ADLX Helper.");
                    _initialised = false;
                    return;
                }
            }
            catch (TypeInitializationException ex)
            {
                SharedLogger.logger.Info(ex, $"AMDLibrary/AMDLibrary: TypeInitializationException trying to load the AMD ADLX DLL {AMD_ADLX_BINDING_DLL}. This generally means you don't have the AMD ADLX driver installed.");
                _initialised = false;
                return;
            }
            catch (DllNotFoundException ex)
            {
                // If we get here then the AMD ADL DLL wasn't found. We can't continue to use it, so we log the error and exit
                SharedLogger.logger.Info(ex, $"AMDLibrary/AMDLibrary: DllNotFoundException trying to load the AMD ADLX DLL {AMD_ADLX_BINDING_DLL}. This generally means you don't have the AMD ADLX driver installed.");
                _initialised = false; 
                return;
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Info(ex, $"AMDLibrary/AMDLibrary: A general exception trying to load the AMD ADLX DLL {AMD_ADLX_BINDING_DLL}.");
                _initialised = false; 
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
                // If the ADLX library was initialised, then we need to free it up.
                if (_initialised)
                {
                    try
                    {
                        // Terminate ADLX
                        _adlxHelper.Dispose();
                        _adlxHelper = null;
                        SharedLogger.logger.Trace($"AMDLibrary/Dispose: AMD ADLX library was destroyed successfully");
                    }
                    catch (Exception ex)
                    {
                        SharedLogger.logger.Trace(ex, $"AMDLibrary/Dispose: Exception destroying AMD ADL2 library. _adlxHelper.Terminate() caused an exception.");
                    }

                }
            }

            // Dispose unmanaged resources
            if (_adlContextHandle != IntPtr.Zero)
            {

                SharedLogger.logger.Trace("AMDLibrary/Dispose: Destroying AMD ADL2 library interface");
                // If the ADL2 library was initialised, then we need to free it up.
                if (_initialisedADL2)
                {
                    try
                    {
                        ADLImport.ADL2_Main_Control_Destroy(_adlContextHandle);
                        _adlContextHandle = IntPtr.Zero;
                        SharedLogger.logger.Trace($"AMDLibrary/Dispose: AMD ADL2 library was destroyed successfully");
                    }
                    catch (Exception ex)
                    {
                        SharedLogger.logger.Trace(ex, $"AMDLibrary/Dispose: Exception destroying AMD ADL2 library. ADL2_Main_Control_Destroy() caused an exception.");
                    }

                }
            }

            /*if (hADLXBindingModule != IntPtr.Zero)
            {
                SharedLogger.logger.Trace("AMDLibrary/Dispose: Freeing the AMD ADLX Binding DLL");
                FreeLibrary(hADLXBindingModule);
                hADLXBindingModule = IntPtr.Zero;
            }*/

            if (hADLXModule != IntPtr.Zero)
            {
                SharedLogger.logger.Trace("AMDLibrary/Dispose: Freeing the AMD ADLX DLL");
                FreeLibrary(hADLXModule);
                hADLXModule = IntPtr.Zero;
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
            myDefaultConfig.IsInUse = false;
            //myDefaultConfig.Desktops = new List<AMD_DESKTOP>();
            //myDefaultConfig.EyefinityDesktop = new AMD_EYEFINITY_DESKTOP();
            //myDefaultConfig.Displays = new Dictionary<long, AMD_DISPLAY_WITH_SETTINGS>();   
            //myDefaultConfig.Adl2SlsConfig = new AMD_SLS_CONFIG();

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
                List<ADLXDesktop> desktopsList = _adlxSystem.EnumerateDesktops().ToList();
                if (desktopsList.Count == 0)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: No desktops found in ADLX system services");
                    return CreateDefaultConfig();
                }
                else
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: Successfully got the desktop list");
                    AMD_DESKTOP newDesktop = new AMD_DESKTOP();
                    // Iterate through the desktop list
                    foreach (var desktop in desktopsList)
                    {
                        var desktopDisplayList = desktop.EnumerateDisplaysForDesktop();
                        newDesktop.Displays = new List<AMD_DISPLAY>();

                        newDesktop.NumberOfDisplays = desktopDisplayList.Count;
                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: The number of displays that are part of this desktop is {newDesktop.NumberOfDisplays}");

                        if (newDesktop.NumberOfDisplays > 0)
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: The number of displays that are part of this desktop is > 0, so getting list of displays");
                            // Get the list of displays that are part of this desktop
                            foreach (var display in desktopDisplayList)
                            {
                                AMD_DISPLAY newDisplay = new AMD_DISPLAY();

                                // Get the display connection type
                                newDisplay.ConnectorType = display.ConnectorType;

                                // Get the display type
                                //newDisplay.DisplayType = display.DisplayType ;

                                // Get the EDID
                                newDisplay.EDID = display.Edid;

                                // Get the manufacturer ID
                                newDisplay.ManufacturerID = display.ManufacturerId;

                                // Get the display name
                                newDisplay.Name = display.Name;

                                // Get the native resolution
                                newDisplay.NativeResolutionWidth = display.NativeResolutionWidth;
                                newDisplay.NativeResolutionHeight = display.NativeResolutionHeight;

                                // Get the PixelClock
                                newDisplay.PixelClock = display.PixelClock;

                                // Get the refresh rate
                                newDisplay.RefreshRate = display.RefreshRate;

                                // Get the scan type
                                newDisplay.ScanType = display.ScanType;

                                // Get the Unique ID
                                newDisplay.UniqueID = display.UniqueId;

                                // Add the new display to the list of displays for this desktop
                                newDesktop.Displays.Add(newDisplay);                               
                            }
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/GetAMDDesktopConfig: The number of displays that are part of this desktop is 0, so not getting list of displays. Skipping.");
                        }

                        // Store the oreientation
                        newDesktop.Orientation = desktop.Orientation;

                        // Store the size and top left location of the desktop
                        newDesktop.SizeWidth = desktop.Width;
                        newDesktop.SizeHeight = desktop.Height;
                        newDesktop.TopLeftX = desktop.TopLeftX;
                        newDesktop.TopLeftY = desktop.TopLeftY;

                        // Store the desktop type
                        newDesktop.Type = desktop.Type;
/*
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
                            SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Cloned desktop detected");
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Single desktop detected");
                        }
*/
                        // Release desktop interface
                        desktop.Dispose();

                        // Save the Desktop to the main list
                        myDisplayConfig.Desktops.Add(newDesktop);
                    }
                }

                //-----------------------------------------------------------------------

                // Get the display services
                // This lets us interact with the various displays to get the settings from them
                
                SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Attempting to get the ADLX display services");
                List<ADLXDisplay> displaysList = _adlxSystem.EnumerateDisplays().ToList();
                if (displaysList.Count == 0)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: No displays found in ADLX system services");
                    return CreateDefaultConfig();
                }
                else
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Attempting to get the ADLX display list");
                    foreach (var display in displaysList)
                    {
                        // Create a new AMD_DISPLAY_WITH_SETTINGS to store things in
                        AMD_DISPLAY_WITH_SETTINGS newDisplay = new AMD_DISPLAY_WITH_SETTINGS();

                        newDisplay.ConnectorType = display.ConnectorType;
                        newDisplay.Type = display.Type;
                        newDisplay.EDID = display.Edid;
                        newDisplay.ManufacturerID = display.ManufacturerId;
                        newDisplay.Name = display.Name;
                        newDisplay.UniqueID = display.UniqueId;
                        newDisplay.NativeResolutionWidth = display.NativeResolutionWidth;
                        newDisplay.NativeResolutionHeight = display.NativeResolutionHeight;
                        newDisplay.PixelClock = display.PixelClock;
                        newDisplay.RefreshRate = display.RefreshRate;
                        newDisplay.ScanType = display.ScanType;

                        // Ok now start getting the various settings for this display

                        //------------------------------------
                        // GET THE COLOR DEPTH IF WE CAN
                        //------------------------------------
                        // Get the current color depth for this display
                        (newDisplay.IsSupportedColorDepth, newDisplay.ColorDepth) = display.GetColorDepthState();

                        //------------------------------------
                        // GET THE DISPLAY CUSTOM COLOR IF POSSIBLE
                        //------------------------------------
                        // Get the current custom color object for this display
                        var customColorInfo = display.GetCustomColor();
                        newDisplay.CustomColorBrightness = customColorInfo.Brightness;
                        newDisplay.CustomColorContrast = customColorInfo.Contrast;
                        newDisplay.CustomColorHue = customColorInfo.Hue;
                        newDisplay.CustomColorSaturation = customColorInfo.Saturation;
                        newDisplay.CustomColorTemperature = customColorInfo.Temperature;


                        // TODO: Fix getting and setting the gamma ramp as that is the bit that is breaking thi section
                        //       but for now we just disable this section.
                        // Now grab the display gamma settings if we can
                        newDisplay.IsSupportedGamma = false;
                        /*SWIGTYPE_p_p_adlx__IADLXDisplayGamma ppGamma = ADLX.new_displayGammaP_Ptr();
                        status = displayService.GetGamma(display, ppGamma);
                        if (status == ADLX_RESULT.ADLX_OK)
                        {
                            IADLXDisplayGamma gamma = ADLX.displayGammaP_Ptr_value(ppGamma);

                            // Create a RegammaCoeff object to hold the coefficients
                            ADLX_RegammaCoeff regammaCoeff = new ADLX_RegammaCoeff();
                            status = gamma.GetGammaCoefficient(regammaCoeff);
                            if (status == ADLX_RESULT.ADLX_OK)
                            {
                                // Store the gamma coefficients in the display settings
                                newDisplay.GammaCoefficientGamma = regammaCoeff.gamma;
                                newDisplay.GammaCoefficientA0 = regammaCoeff.coefficientA0;
                                newDisplay.GammaCoefficientA1 = regammaCoeff.coefficientA1;
                                newDisplay.GammaCoefficientA2 = regammaCoeff.coefficientA2;
                                newDisplay.GammaCoefficientA3 = regammaCoeff.coefficientA3;
                                SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got gamma coefficients for display.");
                            }
                            else
                            {
                                SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: Error getting gamma coefficients. gamma.GetGammaCoefficient() returned error code {status}");
                            }

                            // Get GammaRamp
                            ADLX_GammaRamp gammaRamp = new ADLX_GammaRamp();
                            status = gamma.GetGammaRamp(gammaRamp);
                            if (status == ADLX_RESULT.ADLX_OK)
                            {
                                // Store the gamma ramp in the display settings
                                newDisplay.GammaRamp = GammaRampExtensions.ToSerializable(gammaRamp);
                                SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got gamma ramp for display.");
                            }
                            else
                            {
                                SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: Error getting gamma ramp. gamma.GetGammaRamp() returned error code {status}");
                            }


                            // Release the gamma interface when done
                            gamma.Release();
                        }
                        else
                        {
                            SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: Error getting the display gamma object. displayService.GetGamma() returned error code {status}");
                        }*/

                        //------------------------------------
                        // GET THE FreeSync Settings IF WE CAN
                        //------------------------------------                                
                        // Check if FreeSync is supported and enabled
                        (newDisplay.IsSupportedFreeSync, newDisplay.IsEnabledFreeSync) = display.GetFreeSyncState();

                        //------------------------------------
                        // GET THE Display Gamut Settings IF WE CAN
                        //------------------------------------
                        // Get the current display gamut object for this display
                        // TODO: Gamut is all wrong - we are missing a lot of settings
                        //GamutInfo gamutInfo = display.GetGamut();
                        //newDisplay.Is = gamutInfo.IsGamutSupported;
                        /*    SWIGTYPE_p_p_adlx__IADLXDisplayGamut ppGamut = ADLX.new_displayGamutP_Ptr();
                        status = displayService.GetGamut(display, ppGamut);
                        if (status == ADLX_RESULT.ADLX_OK)
                        {
                            IADLXDisplayGamut gamut = ADLX.displayGamutP_Ptr_value(ppGamut);

                            // Check if Custom gamut color space is supported
                            SWIGTYPE_p_bool pIsSupportedCustomColorSpace = ADLX.new_boolP();
                            status = gamut.IsSupportedCustomColorSpace(pIsSupportedCustomColorSpace);
                            if (status == ADLX_RESULT.ADLX_OK)
                            {

                                // Get the gamut color space
                                ADLX_GamutColorSpace pGamutColorSpace = ADLX.new_adlx_gamutColorSpaceP();
                                status = gamut.GetGamutColorSpace(pGamutColorSpace);
                                if (status == ADLX_RESULT.ADLX_OK)
                                {
                                    ADLX_Point red = pGamutColorSpace.red;
                                    newDisplay.GamutColorSpaceRedX = red.x;
                                    newDisplay.GamutColorSpaceRedY = red.y;

                                    ADLX_Point green = pGamutColorSpace.green;
                                    newDisplay.GamutColorSpaceGreenX = green.x;
                                    newDisplay.GamutColorSpaceGreenY = green.y;

                                    ADLX_Point blue = pGamutColorSpace.blue;
                                    newDisplay.GamutColorSpaceBlueX = blue.x;
                                    newDisplay.GamutColorSpaceBlueY = blue.y;

                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got Display Gamut Color Space for this display!");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Error getting Display Gamut Color Space. gamma.GetGamutColorSpace() returned error code {status}");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Custom Display Gamut Color Space is NOT supported for this display.");
                            }


                            // Check if Custom gamut color space is supported
                            SWIGTYPE_p_bool pIsSupportedCustomWhitePoint = ADLX.new_boolP();
                            status = gamut.IsSupportedCustomWhitePoint(pIsSupportedCustomWhitePoint);
                            if (status == ADLX_RESULT.ADLX_OK)
                            {

                                // Get the gamut white point
                                ADLX_Point pGamutWhitePoint = new ADLX_Point();
                                status = gamut.GetWhitePoint(pGamutWhitePoint);
                                if (status == ADLX_RESULT.ADLX_OK)
                                {
                                    newDisplay.GamutWhitePointX = pGamutWhitePoint.x;
                                    newDisplay.GamutWhitePointY = pGamutWhitePoint.y;

                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got Display Gamut White Point for this display!");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Error getting Display Gamut White Point. gamma.GetWhitePoint() returned error code {status}");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Custom Display Gamut White Point is NOT supported for this display.");
                            }

                            //TODO: Test for all the supported whitepoints and store them
                            //TODO: Test for all the supported whitepoints and store them


                            // Release the gamut interface when done
                            gamut.Release();
                        }
                        else
                        {
                            SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: Error getting the display gamut object. systemServices.GetGamut() returned error code {status}");
                            //return false;
                        }*/

                        // Now get the Display GPU Scaling settings if we can
                        (newDisplay.IsSupportedGPUScaling,newDisplay.IsEnabledGPUScaling) = display.GetGpuScalingState();
                        
                        // Get the Integer Display Scaling settings if we can                            
                        (newDisplay.IsEnabledIntegerScaling, newDisplay.IsEnabledIntegerScaling) = display.GetIntegerScalingState();

                        // Get the Display Pixel Format settings if we can
                        (newDisplay.IsSupportedPixelFormat, newDisplay.CurrentPixelFormat) = display.GetPixelFormatState();
                        
                        // ----
                        // //// Get the current custom resolution set for this display if there is one
                        // foreach (var customResolution in display.EnumerateCustomResolutions())
                        // {
                        //     if (customResolution.IsCurrentAppliedResolution())
                        //     {
                        //         // Store the custom resolution in the display settings
                        //         newDisplay.CurrentCustomResolutionWidth = customResolution.Width;
                        //         newDisplay.CurrentCustomResolutionHeight = customResolution.Height;
                        //         newDisplay.CurrentCustomResolutionRefreshRate = customResolution.RefreshRate;
                        //         newDisplay.CurrentCustomResolutionTimingStandard = customResolution.TimingStandard;
                        //         SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got current custom resolution for display: {newDisplay.CurrentCustomResolutionWidth}x{newDisplay.CurrentCustomResolutionHeight} @ {newDisplay.CurrentCustomResolutionRefreshRate}Hz, Timing Standard: {newDisplay.CurrentCustomResolutionTimingStandard}");
                        //     }
                        // }
                        //SWIGTYPE_p_p_adlx__IADLXDisplayCustomResolution ppCustomResolution = ADLX.new_displayCustomResolutionP_Ptr();
                        //status = displayService.GetCustomResolution(display, ppCustomResolution);
                        //if (status == ADLX_RESULT.ADLX_OK)
                        //{
                        //    IADLXDisplayCustomResolution customResolution = ADLX.displayCustomResolutionP_Ptr_value(ppCustomResolution);

                        //    // Get the current custom resolution
                        //    SWIGTYPE_p_p_adlx__IADLXDisplayResolution ppDisplayResolution = ADLX.new_adlx_customResolutionP();

                        //    ADLX_CustomResolution adlxCurrentCustomResolution = ADLX.new_adlx_customResolutionP();
                        //    status = customResolution.GetCurrentAppliedResolution(adlxCurrentCustomResolution);
                        //    if (status == ADLX_RESULT.ADLX_OK)
                        //    {
                        //        // Store the custom resolution in the display settings
                        //        newDisplay.CurrentCustomResolutionWidth = adlxCustomResolution.width;
                        //        newDisplay.CurrentCustomResolutionHeight = adlxCustomResolution.height;
                        //        newDisplay.CurrentCustomResolutionRefreshRate = adlxCustomResolution.refreshRate;
                        //        newDisplay.CurrentCustomResolutionTimingStandard = adlxCustomResolution.timingStandard;
                        //        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Successfully got current custom resolution for display: {newDisplay.CurrentCustomResolutionWidth}x{newDisplay.CurrentCustomResolutionHeight} @ {newDisplay.CurrentCustomResolutionRefreshRate}Hz, Timing Standard: {newDisplay.CurrentCustomResolutionTimingStandard}");
                        //    }
                        //    else
                        //    {
                        //        SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: Error getting current custom resolution. customResolution.GetCurrentCustomResolution() returned error code {status}");
                        //    }
                        //    // Release the custom resolution interface when done
                        //    customResolution.Release();
                        //}

                        // Get the Display Scaling Mode if we can
                        (newDisplay.IsSupportedScalingMode, newDisplay.CurrentScalingMode) = display.GetScalingMode();
 
                        // Get the VSR settings if we can
                        (newDisplay.IsSupportedVSR, newDisplay.IsEnabledVSR) = display.GetVirtualSuperResolutionState();

                        // Get the HDCP settings if we can
                        (newDisplay.IsSupportedHDCP, newDisplay.IsEnabledHDCP) = display.GetHdcpState();

                        // Get VariBright settings
                        (newDisplay.IsSupportedVariBright, newDisplay.IsEnabledVariBright, newDisplay.VariBrightMode) = display.GetVariBrightState();
                        
                        // Get 3DLUT settings if we can
                        // TODO: This section needs the ADLXWRapper updated to get all the avilable settings
                        // and it needs the Display3DLUTSettings DTO updated to take all the settings too.
                        var threeDLUT = display.GetThreeDLut();
                        var newDisplayThreeDLUTSettings = new AMD_3DLUT_INFO();
                        newDisplayThreeDLUTSettings.IsSupportedSCE = threeDLUT.IsSceDynamicContrastSupported;
                        newDisplayThreeDLUTSettings.IsSupportedSCEVividGaming = threeDLUT.IsSceVividGamingSupported;
                        newDisplayThreeDLUTSettings.IsSupportedSCEDynamicContrast = threeDLUT.IsSceDynamicContrastSupported;
                        newDisplayThreeDLUTSettings.IsSupportedUser3DLUT = threeDLUT.IsUser3DLutSupported;
                        // newDisplayThreeDLUTSettings.IsCurrentSCEDisabled = threeDLUT.IsCurrentSceDisabled;
                        // newDisplayThreeDLUTSettings.IsCurrentSCEVividGaming = threeDLUT.IsCurrentSceVividGaming;
                        // newDisplayThreeDLUTSettings.IsCurrentSCEDynamicContrast = threeDLUT.IsCurrentSceDynamicContrast;
                        // if (threeDLUT.IsCurrentSceDynamicContrast)
                        // {
                        //     newDisplayThreeDLUTSettings.DynamicContrastRange = new IntRange
                        //     {
                        //         Min = threeDLUT.SceDynamicContrastRangeMin,
                        //         Max = threeDLUT.SceDynamicContrastRangeMax,
                        //         Step = threeDLUT.SceDynamicContrastRangeStep
                        //     };
                        //     newDisplayThreeDLUTSettings.CurrentDynamicContrastValue = threeDLUT.CurrentSceDynamicContrastValue;
                        // }

                        // Save the Display to the main dictionary of displays with the uniqueid as the key
                        myDisplayConfig.Displays.Add(newDisplay.UniqueID, newDisplay);
                    }

                }

                // Now we have everything we need, so we can build the display config!
                myDisplayConfig.IsInUse = true;

                // Get the display identifiers                
                myDisplayConfig.DisplayIdentifiers = GetCurrentDisplayIdentifiers(out bool failure);

                // Now try to get the AMD Eyefinity layout using ADL2 (the older standard) as it is more configurable
                if (_initialisedADL2)
                {
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Attempting to get the AMD Eyefinity layout using the ADL2 API");

                    // Get the Adapter info for ALL adapter and put it in the AdapterBuffer
                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: Running ADL2_Adapter_AdapterInfoX4_Get to get the information about all AMD Adapters.");
                    int numAdaptersInfo = 0;
                    IntPtr adapterInfoBuffer = IntPtr.Zero;
                    ADL_STATUS ADLRet = ADLImport.ADL2_Adapter_AdapterInfoX4_Get(_adlContextHandle, -1, out numAdaptersInfo, out adapterInfoBuffer);
                    if (ADLRet == ADL_STATUS.ADL_OK)
                    {

                        ADL_ADAPTER_INFOX2[] adapterArray = new ADL_ADAPTER_INFOX2[numAdaptersInfo];
                        if (numAdaptersInfo > 0)
                        {
                            IntPtr currentAdaptersInfoBuffer = adapterInfoBuffer;
                            for (int i = 0; i < numAdaptersInfo; i++)
                            {
                                // build a structure in the array slot
                                adapterArray[i] = new ADL_ADAPTER_INFOX2();
                                // fill the array slot structure with the data from the buffer
                                adapterArray[i] = (ADL_ADAPTER_INFOX2)Marshal.PtrToStructure(currentAdaptersInfoBuffer, typeof(ADL_ADAPTER_INFOX2));
                                // destroy the bit of memory we no longer need
                                //Marshal.DestroyStructure(currentAdaptersInfoBuffer, typeof(ADL_ADAPTER_INFOX2));
                                // advance the buffer forwards to the next object
                                currentAdaptersInfoBuffer = (IntPtr)((long)currentAdaptersInfoBuffer + Marshal.SizeOf(adapterArray[i]));
                            }
                            // Free the memory used by the buffer                        
                            Marshal.FreeCoTaskMem(adapterInfoBuffer);

                            SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: ADL2_Adapter_AdapterInfoX4_Get returned information about all AMD Adapters.");
                            // Now go through each adapter and get the information we need from it
                            for (int adapterIndex = 0; adapterIndex < numAdaptersInfo; adapterIndex++)
                            {
                                // Skip this adapter if it isn't active
                                ADL_ADAPTER_INFOX2 oneAdapter = adapterArray[adapterIndex]; // There is always just one as we asked for a specific one!
                                if (oneAdapter.Exist != ADLImport.ADL_TRUE)
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: AMD Adapter #{oneAdapter.AdapterIndex.ToString()} doesn't exist at present so skipping detection for this adapter.");
                                    continue;
                                }

                                // Only skip non-present displays if we want all displays information
                                if (oneAdapter.Present != ADLImport.ADL_TRUE)
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: AMD Adapter #{oneAdapter.AdapterIndex.ToString()} isn't enabled at present so skipping detection for this adapter.");
                                    continue;
                                }

                                // Check if the adapter is active
                                // Skip this adapter if it isn't active
                                int adapterActiveStatus = ADLImport.ADL_FALSE;
                                ADLRet = ADLImport.ADL2_Adapter_Active_Get(_adlContextHandle, adapterIndex, out adapterActiveStatus);
                                if (ADLRet == ADL_STATUS.ADL_OK)
                                {
                                    if (adapterActiveStatus == ADLImport.ADL_TRUE)
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/GetSomeDisplayIdentifiers: ADL2_Adapter_Active_Get returned ADL_TRUE - AMD Adapter #{adapterIndex} is active! We can continue.");
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"AMDLibrary/GetSomeDisplayIdentifiers: ADL2_Adapter_Active_Get returned ADL_FALSE - AMD Adapter #{adapterIndex} is NOT active, so skipping.");
                                        continue;
                                    }
                                }
                                else
                                {
                                    SharedLogger.logger.Warn($"AMDLibrary/GetSomeDisplayIdentifiers: WARNING - ADL2_Adapter_Active_Get returned ADL_STATUS {ADLRet} when trying to see if AMD Adapter #{adapterIndex} is active. Trying to skip this adapter so something at least works.");
                                    continue;
                                }

                                // Go grab the DisplayMaps and DisplayTargets as that is useful infor for creating screens
                                int numDisplayTargets = 0;
                                int numDisplayMaps = 0;
                                IntPtr displayTargetBuffer = IntPtr.Zero;
                                IntPtr displayMapBuffer = IntPtr.Zero;
                                ADLRet = ADLImport.ADL2_Display_DisplayMapConfig_Get(_adlContextHandle, adapterIndex, out numDisplayMaps, out displayMapBuffer, out numDisplayTargets, out displayTargetBuffer, ADLImport.ADL_DISPLAY_DISPLAYMAP_OPTION_GPUINFO);
                                if (ADLRet == ADL_STATUS.ADL_OK)
                                {
                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: ADL2_Display_DisplayMapConfig_Get returned information about all displaytargets connected to AMD adapter {adapterIndex}.");
                                }
                                else
                                {
                                    SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: ERROR - ADL2_Display_DisplayMapConfig_Get returned ADL_STATUS {ADLRet} when trying to get the display target info from AMD adapter {adapterIndex} in the computer.");
                                    throw new AMDLibraryException($"ADL2_Display_DisplayMapConfig_Get returned ADL_STATUS {ADLRet} when trying to get the display target info from AMD adapter {adapterIndex} in the computer");
                                }

                                ADL_DISPLAY_TARGET[] displayTargetArray = { };
                                if (numDisplayTargets > 0)
                                {
                                    // At this point we know there is at least one screen connected to an adapter
                                    myDisplayConfig.IsInUse = true;

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
                                    // Save the item                            
                                    //savedAdapterConfig.DisplayTargets = new ADL_DISPLAY_TARGET[numDisplayTargets];
                                    //myDisplayConfig.DisplayTargets = displayTargetArray.ToList<ADL_DISPLAY_TARGET>();
                                }
                                else
                                {
                                    // Free the memory used by the buffer                        
                                    Marshal.FreeCoTaskMem(displayTargetBuffer);
                                    // Return the default config as there are no display targets to get info from
                                    return myDisplayConfig;
                                }


                                // If there are more than 1 display targets then eyefinity is possible
                                if (numDisplayTargets > 1)
                                {
                                    // Check if SLS is enabled for this adapter!
                                    int matchingSLSMapIndex = -1;
                                    ADLRet = ADLImport.ADL2_Display_SLSMapIndex_Get(_adlContextHandle, oneAdapter.AdapterIndex, numDisplayTargets, displayTargetArray, out matchingSLSMapIndex);
                                    if (ADLRet == ADL_STATUS.ADL_OK && matchingSLSMapIndex != -1)
                                    {
                                        // We have a matching SLS index!
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: AMD Adapter #{oneAdapter.AdapterIndex.ToString()} has one or more SLS Maps that could be used with this display configuration! Eyefinity (SLS) could be enabled.");

                                        AMD_SLSMAP_CONFIG mySLSMapConfig = new AMD_SLSMAP_CONFIG();

                                        // We want to get the SLSMapConfig for this matching SLS Map to see if it is actually in use
                                        int numSLSTargets = 0;
                                        IntPtr slsTargetBuffer = IntPtr.Zero;
                                        int numNativeMode = 0;
                                        IntPtr nativeModeBuffer = IntPtr.Zero;
                                        int numNativeModeOffsets = 0;
                                        IntPtr nativeModeOffsetsBuffer = IntPtr.Zero;
                                        int numBezelMode = 0;
                                        IntPtr bezelModeBuffer = IntPtr.Zero;
                                        int numTransientMode = 0;
                                        IntPtr transientModeBuffer = IntPtr.Zero;
                                        int numSLSOffset = 0;
                                        IntPtr slsOffsetBuffer = IntPtr.Zero;
                                        ADL_SLS_MAP slsMap = new ADL_SLS_MAP();
                                        ADLRet = ADLImport.ADL2_Display_SLSMapConfigX2_Get(
                                                                                        _adlContextHandle,
                                                                                            oneAdapter.AdapterIndex,
                                                                                            matchingSLSMapIndex,
                                                                                            ref slsMap,
                                                                                            out numSLSTargets,
                                                                                            out slsTargetBuffer,
                                                                                            out numNativeMode,
                                                                                            out nativeModeBuffer,
                                                                                            out numNativeModeOffsets,
                                                                                            out nativeModeOffsetsBuffer,
                                                                                            out numBezelMode,
                                                                                            out bezelModeBuffer,
                                                                                            out numTransientMode,
                                                                                            out transientModeBuffer,
                                                                                            out numSLSOffset,
                                                                                            out slsOffsetBuffer,
                                                                                            ADLImport.ADL_DISPLAY_SLSGRID_CAP_OPTION_RELATIVETO_CURRENTANGLE);
                                        if (ADLRet == ADL_STATUS.ADL_OK)
                                        {
                                            SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: ADL2_Display_SLSMapConfigX2_Get returned information about the SLS Info connected to AMD adapter {adapterIndex}.");
                                        }
                                        else
                                        {
                                            SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: ERROR - ADL2_Display_SLSMapConfigX2_Get returned ADL_STATUS {ADLRet} when trying to get the SLS Info from AMD adapter {adapterIndex} in the computer.");
                                            continue;
                                        }

                                        // First check that the number of grid entries is equal to the number
                                        // of display targets associated with this adapter & SLS surface.
                                        if (numDisplayTargets != (slsMap.Grid.SLSGridColumn * slsMap.Grid.SLSGridRow))
                                        {
                                            //Number of display targets returned is not equal to the SLS grid size, so SLS can't be enabled fo this display
                                            //myDisplayConfig.SlsConfig.IsSlsEnabled = false; // This is already set to false at the start!
                                            break;
                                        }

                                        // Add the slsMap to the config we want to store
                                        mySLSMapConfig.SLSMap = slsMap;

                                        // Process the slsTargetBuffer
                                        ADL_SLS_TARGET[] slsTargetArray = new ADL_SLS_TARGET[numSLSTargets];
                                        if (numSLSTargets > 0)
                                        {
                                            IntPtr currentSLSTargetBuffer = slsTargetBuffer;
                                            for (int i = 0; i < numSLSTargets; i++)
                                            {
                                                // build a structure in the array slot
                                                slsTargetArray[i] = new ADL_SLS_TARGET();
                                                // fill the array slot structure with the data from the buffer
                                                slsTargetArray[i] = (ADL_SLS_TARGET)Marshal.PtrToStructure(currentSLSTargetBuffer, typeof(ADL_SLS_TARGET));
                                                // destroy the bit of memory we no longer need
                                                //Marshal.DestroyStructure(currentDisplayTargetBuffer, typeof(ADL_ADAPTER_INFOX2));
                                                // advance the buffer forwards to the next object
                                                currentSLSTargetBuffer = (IntPtr)((long)currentSLSTargetBuffer + Marshal.SizeOf(slsTargetArray[i]));
                                            }
                                            // Free the memory used by the buffer                        
                                            Marshal.FreeCoTaskMem(slsTargetBuffer);

                                            // Add the slsTarget to the config we want to store
                                            mySLSMapConfig.SLSTargets = slsTargetArray.ToList();

                                        }
                                        else
                                        {
                                            // Add the slsTarget to the config we want to store
                                            mySLSMapConfig.SLSTargets = new List<ADL_SLS_TARGET>();
                                        }

                                        // Process the nativeModeBuffer
                                        ADL_SLS_MODE[] nativeModeArray = new ADL_SLS_MODE[numNativeMode];
                                        if (numNativeMode > 0)
                                        {
                                            IntPtr currentNativeModeBuffer = nativeModeBuffer;
                                            for (int i = 0; i < numNativeMode; i++)
                                            {
                                                // build a structure in the array slot
                                                nativeModeArray[i] = new ADL_SLS_MODE();
                                                // fill the array slot structure with the data from the buffer
                                                nativeModeArray[i] = (ADL_SLS_MODE)Marshal.PtrToStructure(currentNativeModeBuffer, typeof(ADL_SLS_MODE));
                                                // destroy the bit of memory we no longer need
                                                //Marshal.DestroyStructure(currentDisplayTargetBuffer, typeof(ADL_ADAPTER_INFOX2));
                                                // advance the buffer forwards to the next object
                                                currentNativeModeBuffer = (IntPtr)((long)currentNativeModeBuffer + Marshal.SizeOf(nativeModeArray[i]));
                                            }
                                            // Free the memory used by the buffer                        
                                            Marshal.FreeCoTaskMem(nativeModeBuffer);

                                            // Add the nativeMode to the config we want to store
                                            mySLSMapConfig.NativeModes = nativeModeArray.ToList();

                                        }
                                        else
                                        {
                                            // Add the slsTarget to the config we want to store
                                            mySLSMapConfig.NativeModes = new List<ADL_SLS_MODE>();
                                        }

                                        // Process the nativeModeOffsetsBuffer
                                        ADL_SLS_OFFSET[] nativeModeOffsetArray = new ADL_SLS_OFFSET[numNativeModeOffsets];
                                        if (numNativeModeOffsets > 0)
                                        {
                                            IntPtr currentNativeModeOffsetsBuffer = nativeModeOffsetsBuffer;
                                            for (int i = 0; i < numNativeModeOffsets; i++)
                                            {
                                                // build a structure in the array slot
                                                nativeModeOffsetArray[i] = new ADL_SLS_OFFSET();
                                                // fill the array slot structure with the data from the buffer
                                                nativeModeOffsetArray[i] = (ADL_SLS_OFFSET)Marshal.PtrToStructure(currentNativeModeOffsetsBuffer, typeof(ADL_SLS_OFFSET));
                                                // destroy the bit of memory we no longer need
                                                //Marshal.DestroyStructure(currentDisplayTargetBuffer, typeof(ADL_ADAPTER_INFOX2));
                                                // advance the buffer forwards to the next object
                                                currentNativeModeOffsetsBuffer = (IntPtr)((long)currentNativeModeOffsetsBuffer + Marshal.SizeOf(nativeModeOffsetArray[i]));
                                            }
                                            // Free the memory used by the buffer                        
                                            Marshal.FreeCoTaskMem(nativeModeOffsetsBuffer);

                                            // Add the nativeModeOffsets to the config we want to store
                                            mySLSMapConfig.NativeModeOffsets = nativeModeOffsetArray.ToList();

                                        }
                                        else
                                        {
                                            // Add the empty list to the config we want to store
                                            mySLSMapConfig.NativeModeOffsets = new List<ADL_SLS_OFFSET>();
                                        }

                                        // Process the bezelModeBuffer
                                        ADL_BEZEL_TRANSIENT_MODE[] bezelModeArray = new ADL_BEZEL_TRANSIENT_MODE[numBezelMode];
                                        if (numBezelMode > 0)
                                        {
                                            IntPtr currentBezelModeBuffer = bezelModeBuffer;
                                            for (int i = 0; i < numBezelMode; i++)
                                            {
                                                // build a structure in the array slot
                                                bezelModeArray[i] = new ADL_BEZEL_TRANSIENT_MODE();
                                                // fill the array slot structure with the data from the buffer
                                                bezelModeArray[i] = (ADL_BEZEL_TRANSIENT_MODE)Marshal.PtrToStructure(currentBezelModeBuffer, typeof(ADL_BEZEL_TRANSIENT_MODE));
                                                // destroy the bit of memory we no longer need
                                                //Marshal.DestroyStructure(currentDisplayTargetBuffer, typeof(ADL_ADAPTER_INFOX2));
                                                // advance the buffer forwards to the next object
                                                currentBezelModeBuffer = (IntPtr)((long)currentBezelModeBuffer + Marshal.SizeOf(bezelModeArray[i]));
                                            }
                                            // Free the memory used by the buffer                        
                                            Marshal.FreeCoTaskMem(bezelModeBuffer);

                                            // Add the bezelModes to the config we want to store
                                            mySLSMapConfig.BezelModes = bezelModeArray.ToList();

                                        }
                                        else
                                        {
                                            // Add the slsTarget to the config we want to store
                                            mySLSMapConfig.BezelModes = new List<ADL_BEZEL_TRANSIENT_MODE>();
                                        }

                                        // Process the transientModeBuffer
                                        ADL_BEZEL_TRANSIENT_MODE[] transientModeArray = new ADL_BEZEL_TRANSIENT_MODE[numTransientMode];
                                        if (numTransientMode > 0)
                                        {
                                            IntPtr currentTransientModeBuffer = transientModeBuffer;
                                            for (int i = 0; i < numTransientMode; i++)
                                            {
                                                // build a structure in the array slot
                                                transientModeArray[i] = new ADL_BEZEL_TRANSIENT_MODE();
                                                // fill the array slot structure with the data from the buffer
                                                transientModeArray[i] = (ADL_BEZEL_TRANSIENT_MODE)Marshal.PtrToStructure(currentTransientModeBuffer, typeof(ADL_BEZEL_TRANSIENT_MODE));
                                                // destroy the bit of memory we no longer need
                                                //Marshal.DestroyStructure(currentDisplayTargetBuffer, typeof(ADL_ADAPTER_INFOX2));
                                                // advance the buffer forwards to the next object
                                                currentTransientModeBuffer = (IntPtr)((long)currentTransientModeBuffer + Marshal.SizeOf(transientModeArray[i]));
                                            }
                                            // Free the memory used by the buffer                        
                                            Marshal.FreeCoTaskMem(transientModeBuffer);

                                            // Add the transientModes to the config we want to store
                                            mySLSMapConfig.TransientModes = transientModeArray.ToList();
                                        }
                                        else
                                        {
                                            // Add the slsTarget to the config we want to store
                                            mySLSMapConfig.TransientModes = new List<ADL_BEZEL_TRANSIENT_MODE>();
                                        }

                                        // Process the slsOffsetBuffer
                                        ADL_SLS_OFFSET[] slsOffsetArray = new ADL_SLS_OFFSET[numSLSOffset];
                                        if (numSLSOffset > 0)
                                        {
                                            IntPtr currentSLSOffsetBuffer = slsOffsetBuffer;
                                            for (int i = 0; i < numSLSOffset; i++)
                                            {
                                                // build a structure in the array slot
                                                slsOffsetArray[i] = new ADL_SLS_OFFSET();
                                                // fill the array slot structure with the data from the buffer
                                                slsOffsetArray[i] = (ADL_SLS_OFFSET)Marshal.PtrToStructure(currentSLSOffsetBuffer, typeof(ADL_SLS_OFFSET));
                                                // destroy the bit of memory we no longer need
                                                //Marshal.DestroyStructure(currentDisplayTargetBuffer, typeof(ADL_ADAPTER_INFOX2));
                                                // advance the buffer forwards to the next object
                                                currentSLSOffsetBuffer = (IntPtr)((long)currentSLSOffsetBuffer + Marshal.SizeOf(slsOffsetArray[i]));
                                            }
                                            // Free the memory used by the buffer                        
                                            Marshal.FreeCoTaskMem(slsOffsetBuffer);

                                            // Add the slsOffsets to the config we want to store
                                            mySLSMapConfig.SLSOffsets = slsOffsetArray.ToList();

                                        }
                                        else
                                        {
                                            // Add the slsTarget to the config we want to store
                                            mySLSMapConfig.SLSOffsets = new List<ADL_SLS_OFFSET>();
                                        }

                                        // Now we try to calculate whether SLS is enabled
                                        // NFI why they don't just add a ADL2_Display_SLSMapConfig_GetState function to make this easy for ppl :(
                                        // NVIDIA make it easy, why can't you AMD?

                                        // Logic cribbed from https://github.com/elitak/amd-adl-sdk/blob/master/Sample/Eyefinity/ati_eyefinity.c
                                        // Go through each display Target
                                        foreach (var displayTarget in displayTargetArray)
                                        {
                                            // Get the current Display Modes for this adapter/display combination
                                            int numDisplayModes;
                                            IntPtr displayModeBuffer;
                                            ADLRet = ADLImport.ADL2_Display_Modes_Get(
                                                                                        _adlContextHandle,
                                                                                            oneAdapter.AdapterIndex,
                                                                                            displayTarget.DisplayID.DisplayLogicalIndex,
                                                                                            out numDisplayModes,
                                                                                            out displayModeBuffer);
                                            if (ADLRet == ADL_STATUS.ADL_OK)
                                            {
                                                SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: ADL2_Display_Modes_Get returned information about the display modes used by display #{displayTarget.DisplayID.DisplayLogicalAdapterIndex} connected to AMD adapter {adapterIndex}.");
                                            }
                                            else
                                            {
                                                SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: ERROR - ADL2_Display_Modes_Get returned ADL_STATUS {ADLRet} when trying to get the display modes from AMD adapter {adapterIndex} in the computer.");
                                                continue;
                                            }

                                            ADL_MODE[] displayModeArray = new ADL_MODE[numDisplayModes];
                                            if (numDisplayModes > 0)
                                            {
                                                IntPtr currentDisplayModeBuffer = displayModeBuffer;
                                                for (int i = 0; i < numDisplayModes; i++)
                                                {
                                                    // build a structure in the array slot
                                                    displayModeArray[i] = new ADL_MODE();
                                                    // fill the array slot structure with the data from the buffer
                                                    displayModeArray[i] = (ADL_MODE)Marshal.PtrToStructure(currentDisplayModeBuffer, typeof(ADL_MODE));
                                                    // destroy the bit of memory we no longer need
                                                    //Marshal.DestroyStructure(currentDisplayTargetBuffer, typeof(ADL_ADAPTER_INFOX2));
                                                    // advance the buffer forwards to the next object
                                                    currentDisplayModeBuffer = (IntPtr)((long)currentDisplayModeBuffer + Marshal.SizeOf(displayModeArray[i]));
                                                }
                                                // Free the memory used by the buffer                        
                                                Marshal.FreeCoTaskMem(displayModeBuffer);

                                                // Add the slsOffsets to the config we want to store
                                                //mySLSMapConfig.SLSOffsets = displayModeArray.ToList();

                                            }

                                            // If Eyefinity is enabled for this adapter, then the display mode of an
                                            // attached display target will match one of the SLS display modes reported by
                                            // ADL_Display_SLSMapConfig_Get(). The match will either be with "native" SLS 
                                            // modes (which are not bezel-compensated), or with "bezel" SLS modes which are.
                                            // 
                                            // So, simply compare current display mode against all the ones listed for the
                                            // SLS native or bezel-compensated modes: if there is a match, then the mode
                                            // currently used by this adapter is an Eyefinity/SLS mode, and Eyefinity is enabled.
                                            // First check the native SLS mode list
                                            // Process the slsOffsetBuffer
                                            bool isSlsEnabled = false;
                                            bool isBezelCompensatedDisplay = false;
                                            foreach (var displayMode in displayModeArray)
                                            {
                                                foreach (var nativeMode in nativeModeArray)
                                                {
                                                    if (nativeMode.DisplayMode.XRes == displayMode.XRes && nativeMode.DisplayMode.YRes == displayMode.YRes)
                                                    {
                                                        isSlsEnabled = true;
                                                        break;
                                                    }

                                                }

                                                // If no match was found, check the bezel-compensated SLS mode list
                                                if (!isSlsEnabled)
                                                {
                                                    foreach (var bezelMode in bezelModeArray)
                                                    {
                                                        if (bezelMode.DisplayMode.XRes == displayMode.XRes && bezelMode.DisplayMode.YRes == displayMode.YRes)
                                                        {
                                                            isSlsEnabled = true;
                                                            isBezelCompensatedDisplay = true;
                                                            break;
                                                        }
                                                    }
                                                }

                                                // Now we check which slot we need to put this display into
                                                if (isSlsEnabled)
                                                {
                                                    // SLS is enabled for this display
                                                    if (!myDisplayConfig.Adl2SlsConfig.SLSEnabledDisplayTargets.Contains(displayMode))
                                                    {
                                                        myDisplayConfig.Adl2SlsConfig.SLSEnabledDisplayTargets.Add(displayMode);
                                                    }
                                                    // we also update the main IsSLSEnabled so that it is indicated at the top level too

                                                    myDisplayConfig.Adl2SlsConfig.IsSlsEnabled = true;
                                                    SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: AMD Adapter #{oneAdapter.AdapterIndex.ToString()} has a matching SLS grid set! Eyefinity (SLS) is enabled. Setting IsSlsEnabled to true");

                                                }
                                            }

                                        }

                                        // Only Add the mySLSMapConfig to the displayConfig if SLS is enabled
                                        if (myDisplayConfig.Adl2SlsConfig.IsSlsEnabled)
                                        {
                                            myDisplayConfig.Adl2SlsConfig.SLSMapConfigs.Add(mySLSMapConfig);
                                        }

                                    }
                                    else
                                    {
                                        // If we get here then there there was no active SLSGrid, meaning Eyefinity is disabled!
                                        SharedLogger.logger.Trace($"AMDLibrary/GetAMDDisplayConfig: AMD Adapter #{oneAdapter.AdapterIndex.ToString()} has no active SLS grids set! Eyefinity (SLS) hasn't even been setup yet. Keeping the default IsSlsEnabled value of false.");
                                    }
                                }
                                else
                                {
                                    // If we get here then there are less than two displays connected. Eyefinity cannot be enabled in this case!
                                    SharedLogger.logger.Info($"AMDLibrary/GetAMDDisplayConfig: There are less than two displays connected to this adapter so Eyefinity cannot be enabled.");
                                }

                            }

                        }
                        else
                        {
                            // Free the memory used by the buffer                        
                            Marshal.FreeCoTaskMem(adapterInfoBuffer);
                            // Return the default config as there are no adapters to get info from
                        }
                    }
                    else
                    {
                        SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: ERROR - ADL2_Adapter_AdapterInfoX4_Get returned ADL_STATUS {ADLRet} when trying to get the adapter info about all AMD Adapters. Trying to skip this adapter so something at least works.");
                    }



                }
                else
                {
                    SharedLogger.logger.Error($"AMDLibrary/GetAMDDisplayConfig: ERROR - Tried to get the AMD Eyefinity layout using the older ADL2 AP but the AMD ADL2 library isn't initialised!");
                    myDisplayConfig.Adl2SlsConfig = new AMD_SLS_CONFIG();
                }

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

        public bool SetActiveConfig(AMD_DISPLAY_CONFIG displayConfig, bool useADLEyefinity, int delayInMs)
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

                return true;

                // SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Attempting to get the ADLX desktop services");
                // SWIGTYPE_p_p_adlx__IADLXDesktopServices d = ADLX.new_desktopSerP_Ptr();
                // status = _adlxSystem.GetDesktopsServices(d);
                // desktopService = ADLX.desktopSerP_Ptr_value(d);
                // if (status != ADLX_RESULT.ADLX_OK)
                // {
                //     SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Error getting the ADLX desktop services. systemServices.GetDesktopsServices() returned error code {status}");
                //     return false;
                // }
                // else
                // {
                //     // Get the list of Desktops we have (this is more for informational purposes)
                //     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Successfully got the desktop services");

                //     // If the display config needs an Eyefinity Desktop then lets create one.
                //     if (displayConfig.IsEyefinity)
                //     {
                //         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: New display layout requires an Eyefinity desktop");

                //         // Check if we are using the new ADLX or older ADL API to create the Eyefinity Desktop
                //         if (useADLEyefinity)
                //         {                          
                //             // If set then we are using the older ADL API to create the Eyefinity Desktop
                //             SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Using the older ADL API to create the Eyefinity Desktop.");
                           
                //             // Set the initial state of the ADL_STATUS
                //             ADL_STATUS ADLRet = 0;
                //             foreach (AMD_SLSMAP_CONFIG slsMapConfig in displayConfig.Adl2SlsConfig.SLSMapConfigs)
                //             {
                //                 // Attempt to turn on this SLS Map Config if it exists in the AMD Radeon driver config database
                //                 ADLRet = ADLImport.ADL2_Display_SLSMapConfig_SetState(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap.SLSMapIndex, ADLImport.ADL_TRUE);
                //                 if (ADLRet == ADL_STATUS.ADL_OK)
                //                 {
                //                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_SetState successfully set the SLSMAP with index {slsMapConfig.SLSMap.SLSMapIndex} to TRUE for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                //                 }
                //                 else
                //                 {
                //                     SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ERROR - ADL2_Display_SLSMapConfig_SetState returned ADL_STATUS {ADLRet} when trying to set the SLSMAP with index {slsMapConfig.SLSMap.SLSMapIndex} to TRUE for adapter {slsMapConfig.SLSMap.AdapterIndex}.");

                //                     // If we get an error with just tturning it on, then we need to actually try to created a new Eyefinity map and then enable it
                //                     // If we reach this stage, then the user has discarded the AMD Eyefinity mode in AMD due to a bad UI design, and we need to work around that slight issue.
                //                     // (BTW that's FAR to easy to do in the AMD Radeon GUI)
                //                     // NOTE: There is a slight issue with way of doing things. Although we create a much more robust way of working, we also will never ever actually use the Eyefinity config as saved.
                //                     //       Instead, we will always drop through to creating an Eyefinity config each time, the only saving grace being that the AMD Driver is smart enough to notice this and it will reuse the same SLSMapIndex number.
                //                     //       This at least means that we won't keep filling the AMD Driver up with additional EYefinity configs! It will instaed only add one more additional AMD Config if it works this way.

                //                     int supportedSLSLayoutImageMode;
                //                     int reasonForNotSupportSLS;
                //                     ADLRet = ADLImport.ADL2_Display_SLSMapConfig_Valid(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap, slsMapConfig.SLSTargets.Count, slsMapConfig.SLSTargets.ToArray(), out supportedSLSLayoutImageMode, out reasonForNotSupportSLS, ADLImport.ADL_DISPLAY_SLSMAPCONFIG_CREATE_OPTION_RELATIVETO_CURRENTANGLE);
                //                     if (ADLRet == ADL_STATUS.ADL_OK)
                //                     {
                //                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_Valid successfully validated a new SLSMAP config for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                //                     }
                //                     else
                //                     {
                //                         SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ERROR - ADL2_Display_SLSMapConfig_Valid returned ADL_STATUS {ADLRet} when trying to create a new SLSMAP for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                //                         return false;
                //                     }

                //                     // Create and apply the new SLSMap
                //                     int newSlsMapIndex;
                //                     ADLRet = ADLImport.ADL2_Display_SLSMapConfig_Create(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap, slsMapConfig.SLSTargets.Count, slsMapConfig.SLSTargets.ToArray(), slsMapConfig.BezelModePercent, out newSlsMapIndex, ADLImport.ADL_DISPLAY_SLSMAPCONFIG_CREATE_OPTION_RELATIVETO_CURRENTANGLE);
                //                     if (ADLRet == ADL_STATUS.ADL_OK)
                //                     {
                //                         if (newSlsMapIndex != -1)
                //                         {
                //                             SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_Create successfully created the new SLSMAP we just created with index {newSlsMapIndex} to TRUE for adapter {slsMapConfig.SLSMap.AdapterIndex}.");

                //                             // At this point we have created a new AMD Eyefinity Config
                //                         }
                //                         else
                //                         {
                //                             SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ERROR - ADL2_Display_SLSMapConfig_Create returned ADL_STATUS {ADLRet} but the returned SLSMapIndex was -1, which indicates that the new SLSMAP failed to create for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                //                         }
                //                     }
                //                     else
                //                     {
                //                         SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ERROR - ADL2_Display_SLSMapConfig_Create returned ADL_STATUS {ADLRet} when trying to create a new SLSMAP for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                //                         return false;
                //                     }

                //                     // Make the changes permanent
                //                     ADLRet = ADLImport.ADL2_Flush_Driver_Data(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex);
                //                     if (ADLRet == ADL_STATUS.ADL_OK)
                //                     {
                //                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: ADL2_Flush_Driver_Data successfully saved the adapter settings as permanent for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                //                     }
                //                     else
                //                     {
                //                         SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ADL2_Flush_Driver_Data failed to save the adapter settings as permanent for adapter {slsMapConfig.SLSMap.AdapterIndex}. ");
                //                         return false;
                //                     }
                //                 }

                //             }
                //         }
                //         else
                //         {
                //             // Otherwise we are using the newer ADLX API to create the Eyefinity Desktop
                //             SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Using the newer ADLX API to create the Eyefinity Desktop.");
                //             if (displayConfig.EyefinityDesktop.Equals(ActiveDisplayConfig.EyefinityDesktop))
                //             {
                //                 // If the Eyefinity Desktop is already set then we don't need to do anything
                //                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Eyefinity layout is exactly the same as the one we want, so skipping setting up the Eyefinity Desktop");
                //             }
                //             else
                //             {
                //                 // Otherwise we need to use the new ADLX API to create the Eyefinity Desktop
                //                 // Setup the EyefinityDesktop using the settings the driver stores internally
                //                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Attempting to get the ADLX EyefinityDesktop object");
                //                 // Get eyefinitydisplay list
                //                 SWIGTYPE_p_p_adlx__IADLXSimpleEyefinity ppSimpleEyefinity = ADLX.new_simpleEyefinityP_Ptr();
                //                 status = desktopService.GetSimpleEyefinity(ppSimpleEyefinity);
                //                 IADLXSimpleEyefinity simpleEyefinity = ADLX.simpleEyefinityP_Ptr_value(ppSimpleEyefinity);

                //                 if (status != ADLX_RESULT.ADLX_OK)
                //                 {
                //                     SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Error getting the ADLX SimpleEyefinity object. systemServices.GetSimpleEyefinity() returned error code {status}");
                //                     return false;
                //                 }
                //                 else
                //                 {
                //                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Successfully got the ADLX SimpleEyefinity object");
                //                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Attempting to create the ADLX Eyefinity Desktop");
                //                     SWIGTYPE_p_p_adlx__IADLXEyefinityDesktop ppEyefinityDesktop = ADLX.new_eyefinityDesktopP_Ptr();
                //                     status = simpleEyefinity.Create(ppEyefinityDesktop);
                //                     IADLXEyefinityDesktop eyefinityDesktop = ADLX.eyefinityDesktopP_Ptr_value(ppEyefinityDesktop);

                //                     if (status != ADLX_RESULT.ADLX_OK)
                //                     {
                //                         SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Error creating the ADLX Eyefinity Desktop. systemServices.GetSimpleEyefinity() returned error code {status}");
                //                         return false;
                //                     }
                //                     else
                //                     {
                //                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Successfully created the ADLX Eyefinity Desktop");
                //                         if (displayConfig.EyefinityDesktop.Equals(ActiveDisplayConfig.EyefinityDesktop))
                //                         {
                //                             SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: This new Eyefinity layout matches the desired configuraton");
                //                         }
                //                         else
                //                         {
                //                             SharedLogger.logger.Warn($"AMDLibrary/SetActiveConfig: This new Eyefinity layout is different from the one we originally saved with this desktop profile. If you have changed your Eyefinity Layout then you need to update this desktop profile!.");
                //                         }
                //                     }
                //                 }
                //                 // Release simpleEyefinity interface
                //                 simpleEyefinity.Release();
                //             }
                //         }
                //     }
                //     else
                //     {
                //         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: New display layout does NOT requires a Eyefinity desktop");

                //         if (ActiveDisplayConfig.IsEyefinity)
                //         {
                //             SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Eyefinity layout is currently in use but is NOT required, so we need to destroy the Eyefinity Desktop");

                //             // Check if we are using the new ADLX or older ADL API to destroy the Eyefinity Desktop
                //             if (useADLEyefinity)
                //             {
                //                 // If set then we are using the older ADL API to destroy the Eyefinity Desktop
                //                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Using the older ADL API to destroy the Eyefinity Desktop.");

                //                 // We need to disable the current Eyefinity (SLS) profile to turn it off
                //                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: SLS is enabled in the current display configuration, so we need to turn it off");
                //                 // Set the initial state of the ADL_STATUS
                //                 ADL_STATUS ADLRet = 0;

                //                 foreach (AMD_SLSMAP_CONFIG slsMapConfig in ActiveDisplayConfig.Adl2SlsConfig.SLSMapConfigs)
                //                 {
                //                     // Turn off this SLS Map Config
                //                     ADLRet = ADLImport.ADL2_Display_SLSMapConfig_SetState(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex, slsMapConfig.SLSMap.SLSMapIndex, ADLImport.ADL_FALSE);
                //                     if (ADLRet == ADL_STATUS.ADL_OK)
                //                     {
                //                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: ADL2_Display_SLSMapConfig_SetState successfully disabled the SLSMAP with index {slsMapConfig.SLSMap.SLSMapIndex} for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                //                     }
                //                     else
                //                     {
                //                         SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ERROR - ADL2_Display_SLSMapConfig_SetState returned ADL_STATUS {ADLRet} when trying to set the SLSMAP with index {slsMapConfig.SLSMap.SLSMapIndex} to FALSE for adapter {slsMapConfig.SLSMap.AdapterIndex}.");
                //                         return false;
                //                     }

                //                     // Make the changes permanent
                //                     ADLRet = ADLImport.ADL2_Flush_Driver_Data(_adlContextHandle, slsMapConfig.SLSMap.AdapterIndex);
                //                     if (ADLRet == ADL_STATUS.ADL_OK)
                //                     {
                //                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: ADL2_Flush_Driver_Data successfully saved the adapter settings as permanent for adapter {slsMapConfig.SLSMap.AdapterIndex} (after disable).");
                //                     }
                //                     else
                //                     {
                //                         SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: ADL2_Flush_Driver_Data failed to save the adapter settings as permanent for adapter {slsMapConfig.SLSMap.AdapterIndex} (after disable).");
                //                         return false;
                //                     }

                //                 }
                //             }
                //             else
                //             {
                //                 // Otherwise we are using the new ADLX API to destroy the Eyefinity Desktop
                //                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Using the newer ADLX API to destroy the Eyefinity Desktop.");

                //                 // Setup the EyefinityDesktop using the settings the driver stores internally
                //                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Attempting to get the ADLX EyefinityDesktop object");
                //                 // Get eyefinitydisplay list
                //                 SWIGTYPE_p_p_adlx__IADLXSimpleEyefinity ppSimpleEyefinity = ADLX.new_simpleEyefinityP_Ptr();
                //                 status = desktopService.GetSimpleEyefinity(ppSimpleEyefinity);
                //                 IADLXSimpleEyefinity simpleEyefinity = ADLX.simpleEyefinityP_Ptr_value(ppSimpleEyefinity);

                //                 if (status != ADLX_RESULT.ADLX_OK)
                //                 {
                //                     SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Error getting the ADLX SimpleEyefinity object. systemServices.GetSimpleEyefinity() returned error code {status}");
                //                     return false;
                //                 }
                //                 else
                //                 {
                //                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Successfully got the ADLX SimpleEyefinity object");
                //                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Attempting to destroy all the ADLX Eyefinity Desktops");
                //                     SWIGTYPE_p_p_adlx__IADLXEyefinityDesktop ppEyefinityDesktop = ADLX.new_eyefinityDesktopP_Ptr();
                //                     status = simpleEyefinity.DestroyAll();
                //                     if (status != ADLX_RESULT.ADLX_OK)
                //                     {
                //                         SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Error destroying all existing ADLX Eyefinity Desktops. systemServices.GetSimpleEyefinity() returned error code {status}");
                //                         return false;
                //                     }
                //                     else
                //                     {
                //                         SharedLogger.logger.Error($"AMDLibrary/SetActiveConfig: Successfully destroyed all existing ADLX Eyefinity Desktops. ");
                //                     }
                //                 }
                //                 // Release simpleEyefinity interface
                //                 simpleEyefinity.Release();

                //             }

                //         }
                //         else
                //         {
                //             SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfig: Eyefinity layout is not currently in use and is NOT required, so leaving things as they are.");
                //         }
                //     }                    
                // }

                // // Release desktop services interface
                // desktopService.Release();

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

                return true;

                // SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Attempting to get the ADLX display services");
                // SWIGTYPE_p_p_adlx__IADLXDisplayServices s = ADLX.new_displaySerP_Ptr();
                // status = _adlxSystem.GetDisplaysServices(s);
                // displayService = ADLX.displaySerP_Ptr_value(s);
                // if (status != ADLX_RESULT.ADLX_OK)
                // {
                //     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Error getting the ADLX display services. systemServices.GetDisplaysServices() returned error code {status}");
                //     return false;
                // }
                // else
                // {
                //     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully got the display services");
                //     // Get display list
                //     SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                //     status = displayService.GetDisplays(ppDisplayList);
                //     displayList = ADLX.displayListP_Ptr_value(ppDisplayList);
                //     if (status != ADLX_RESULT.ADLX_OK)
                //     {
                //         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Error getting the ADLX display list. systemServices.GetDisplays() returned error code {status}");
                //         return false;
                //     }
                //     else
                //     {
                //         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully got the display list");
                //         // Iterate through the display list and see if we need to change any settings
                //         uint it = displayList.Begin();
                //         for (; it != displayList.Size(); it++)
                //         {
                //             SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay = ADLX.new_displayP_Ptr();
                //             status = displayList.At(it, ppDisplay);
                //             IADLXDisplay display = ADLX.displayP_Ptr_value(ppDisplay);

                //             if (status == ADLX_RESULT.ADLX_OK)
                //             {
                //                 SWIGTYPE_p_size_t pID = ADLX.new_adlx_sizeP();
                //                 display.UniqueId(pID);
                //                 uint id = ADLX.adlx_sizeP_value(pID);

                //                 SWIGTYPE_p_p_char ppName = ADLX.new_charP_Ptr();
                //                 display.Name(ppName);
                //                 string name = ADLX.charP_Ptr_value(ppName);

                //                 // find the display settings that match this display
                //                 if (displayConfig.Displays.ContainsKey(id))
                //                 {
                //                     // We have a match, so lets set the display settings
                //                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Found a matching display with ID {id} in the display list");
                //                     // Get the display settings we stored
                //                     AMD_DISPLAY_WITH_SETTINGS displaySettingsWeStored = displayConfig.Displays[id];

                //                     //------------------------------------
                //                     // SET THE COLOR DEPTH IF NEEDED
                //                     //------------------------------------
                //                     if (displaySettingsWeStored.IsSupportedColorDepth)
                //                     {
                //                         SWIGTYPE_p_p_adlx__IADLXDisplayColorDepth ppColorDepth = ADLX.new_displayColorDepthP_Ptr();
                //                         status = displayService.GetColorDepth(display, ppColorDepth);
                //                         if (status == ADLX_RESULT.ADLX_OK)
                //                         {
                //                             IADLXDisplayColorDepth colorDepth = ADLX.displayColorDepthP_Ptr_value(ppColorDepth);
                                            
                //                             // Check if current hardware supports color depth
                //                             SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                //                             status = colorDepth.IsSupported(pIsSupported);
                //                             bool currentHardwareSupportsColorDepth = ADLX.boolP_value(pIsSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsColorDepth)
                //                             {
                //                                 // Get current color depth value
                //                                 SWIGTYPE_p_ADLX_COLOR_DEPTH pColorDepth = ADLX.new_adlx_colorDepthP();
                //                                 status = colorDepth.GetValue(pColorDepth);
                //                                 ADLX_COLOR_DEPTH colorDepthValue = ADLX.adlx_colorDepthP_value(pColorDepth);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && colorDepthValue != displaySettingsWeStored.ColorDepth)
                //                                 {
                //                                     status = colorDepth.SetValue(displaySettingsWeStored.ColorDepth);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set Color Depth to {displaySettingsWeStored.ColorDepth.ToString("G")}");
                //                                     }
                //                                     else
                //                                     {
                //                                         SharedLogger.logger.Error($"AMDLibrary/SetActiveConfigOverride: Error setting Color Depth, returned error code {status}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Color Depth already set to desired value {displaySettingsWeStored.ColorDepth.ToString("G")}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Color Depth not supported by current hardware, skipping");
                //                             }
                //                         }
                //                     }

                //                     //------------------------------------
                //                     // SET THE CUSTOM COLOR SETTINGS IF NEEDED
                //                     //------------------------------------
                //                     SWIGTYPE_p_p_adlx__IADLXDisplayCustomColor ppCustomColor = ADLX.new_displayCustomColorP_Ptr();
                //                     status = displayService.GetCustomColor(display, ppCustomColor);
                //                     if (status == ADLX_RESULT.ADLX_OK)
                //                     {
                //                         IADLXDisplayCustomColor customColor = ADLX.displayCustomColorP_Ptr_value(ppCustomColor);
                                        
                //                         // Set Brightness
                //                         if (displaySettingsWeStored.IsSupportedCustomColorBrightness)
                //                         {
                //                             // Check if current hardware supports brightness
                //                             SWIGTYPE_p_bool pIsBrightnessSupported = ADLX.new_boolP();
                //                             status = customColor.IsBrightnessSupported(pIsBrightnessSupported);
                //                             bool currentHardwareSupportsBrightness = ADLX.boolP_value(pIsBrightnessSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsBrightness)
                //                             {
                //                                 // Get current brightness value
                //                                 SWIGTYPE_p_int pCurrentBrightness = ADLX.new_adlx_intP();
                //                                 status = customColor.GetBrightness(pCurrentBrightness);
                //                                 int currentBrightnessValue = ADLX.adlx_intP_value(pCurrentBrightness);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && currentBrightnessValue != displaySettingsWeStored.CustomColorBrightness)
                //                                 {
                //                                     status = customColor.SetBrightness(displaySettingsWeStored.CustomColorBrightness);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set Custom Color Brightness to {displaySettingsWeStored.CustomColorBrightness}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Brightness already set to desired value {displaySettingsWeStored.CustomColorBrightness}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Brightness not supported by current hardware, skipping");
                //                             }
                //                         }

                //                         // Set Hue
                //                         if (displaySettingsWeStored.IsSupportedCustomColorHue)
                //                         {
                //                             // Check if current hardware supports hue
                //                             SWIGTYPE_p_bool pIsHueSupported = ADLX.new_boolP();
                //                             status = customColor.IsHueSupported(pIsHueSupported);
                //                             bool currentHardwareSupportsHue = ADLX.boolP_value(pIsHueSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsHue)
                //                             {
                //                                 // Get current hue value
                //                                 SWIGTYPE_p_int pCurrentHue = ADLX.new_adlx_intP();
                //                                 status = customColor.GetHue(pCurrentHue);
                //                                 int currentHueValue = ADLX.adlx_intP_value(pCurrentHue);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && currentHueValue != displaySettingsWeStored.CustomColorHue)
                //                                 {
                //                                     status = customColor.SetHue(displaySettingsWeStored.CustomColorHue);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set Custom Color Hue to {displaySettingsWeStored.CustomColorHue}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Hue already set to desired value {displaySettingsWeStored.CustomColorHue}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Hue not supported by current hardware, skipping");
                //                             }
                //                         }

                //                         // Set Saturation
                //                         if (displaySettingsWeStored.IsSupportedCustomColorSaturation)
                //                         {
                //                             // Check if current hardware supports saturation
                //                             SWIGTYPE_p_bool pIsSaturationSupported = ADLX.new_boolP();
                //                             status = customColor.IsSaturationSupported(pIsSaturationSupported);
                //                             bool currentHardwareSupportsSaturation = ADLX.boolP_value(pIsSaturationSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsSaturation)
                //                             {
                //                                 // Get current saturation value
                //                                 SWIGTYPE_p_int pCurrentSaturation = ADLX.new_adlx_intP();
                //                                 status = customColor.GetSaturation(pCurrentSaturation);
                //                                 int currentSaturationValue = ADLX.adlx_intP_value(pCurrentSaturation);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && currentSaturationValue != displaySettingsWeStored.CustomColorSaturation)
                //                                 {
                //                                     status = customColor.SetSaturation(displaySettingsWeStored.CustomColorSaturation);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set Custom Color Saturation to {displaySettingsWeStored.CustomColorSaturation}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Saturation already set to desired value {displaySettingsWeStored.CustomColorSaturation}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Saturation not supported by current hardware, skipping");
                //                             }
                //                         }

                //                         // Set Contrast
                //                         if (displaySettingsWeStored.IsSupportedCustomColorContrast)
                //                         {
                //                             // Check if current hardware supports contrast
                //                             SWIGTYPE_p_bool pIsContrastSupported = ADLX.new_boolP();
                //                             status = customColor.IsContrastSupported(pIsContrastSupported);
                //                             bool currentHardwareSupportsContrast = ADLX.boolP_value(pIsContrastSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsContrast)
                //                             {
                //                                 // Get current contrast value
                //                                 SWIGTYPE_p_int pCurrentContrast = ADLX.new_adlx_intP();
                //                                 status = customColor.GetContrast(pCurrentContrast);
                //                                 int currentContrastValue = ADLX.adlx_intP_value(pCurrentContrast);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && currentContrastValue != displaySettingsWeStored.CustomColorContrast)
                //                                 {
                //                                     status = customColor.SetContrast(displaySettingsWeStored.CustomColorContrast);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set Custom Color Contrast to {displaySettingsWeStored.CustomColorContrast}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Contrast already set to desired value {displaySettingsWeStored.CustomColorContrast}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Contrast not supported by current hardware, skipping");
                //                             }
                //                         }

                //                         // Set Temperature
                //                         if (displaySettingsWeStored.IsSupportedCustomColorTemperature)
                //                         {
                //                             // Check if current hardware supports temperature
                //                             SWIGTYPE_p_bool pIsTemperatureSupported = ADLX.new_boolP();
                //                             status = customColor.IsTemperatureSupported(pIsTemperatureSupported);
                //                             bool currentHardwareSupportsTemperature = ADLX.boolP_value(pIsTemperatureSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsTemperature)
                //                             {
                //                                 // Get current temperature value
                //                                 SWIGTYPE_p_int pCurrentTemperature = ADLX.new_adlx_intP();
                //                                 status = customColor.GetTemperature(pCurrentTemperature);
                //                                 int currentTemperatureValue = ADLX.adlx_intP_value(pCurrentTemperature);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && currentTemperatureValue != displaySettingsWeStored.CustomColorTemperature)
                //                                 {
                //                                     status = customColor.SetTemperature(displaySettingsWeStored.CustomColorTemperature);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set Custom Color Temperature to {displaySettingsWeStored.CustomColorTemperature}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Temperature already set to desired value {displaySettingsWeStored.CustomColorTemperature}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Custom Color Temperature not supported by current hardware, skipping");
                //                             }
                //                         }

                //                         customColor.Release();
                //                     }

                //                     //------------------------------------
                //                     // SET FREESYNC IF NEEDED
                //                     //------------------------------------
                //                     if (displaySettingsWeStored.IsSupportedFreeSync)
                //                     {
                //                         SWIGTYPE_p_p_adlx__IADLXDisplayFreeSync ppFreeSync = ADLX.new_displayFreeSyncP_Ptr();
                //                         status = displayService.GetFreeSync(display, ppFreeSync);
                //                         if (status == ADLX_RESULT.ADLX_OK)
                //                         {
                //                             IADLXDisplayFreeSync freeSync = ADLX.displayFreeSyncP_Ptr_value(ppFreeSync);
                                            
                //                             // Check if current hardware supports FreeSync
                //                             SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                //                             status = freeSync.IsSupported(pIsSupported);
                //                             bool currentHardwareSupportsFreeSyn = ADLX.boolP_value(pIsSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsFreeSyn)
                //                             {
                //                                 // Get current FreeSync enabled state
                //                                 SWIGTYPE_p_bool pIsEnabled = ADLX.new_boolP();
                //                                 status = freeSync.IsEnabled(pIsEnabled);
                //                                 bool isEnabled = ADLX.boolP_value(pIsEnabled);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && isEnabled != displaySettingsWeStored.IsEnabledFreeSync)
                //                                 {
                //                                     status = freeSync.SetEnabled(displaySettingsWeStored.IsEnabledFreeSync);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set FreeSync to {displaySettingsWeStored.IsEnabledFreeSync}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: FreeSync already set to desired value {displaySettingsWeStored.IsEnabledFreeSync}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: FreeSync not supported by current hardware, skipping");
                //                             }
                //                             freeSync.Release();
                //                         }
                //                     }

                //                     //------------------------------------
                //                     // SET GPU SCALING IF NEEDED
                //                     //------------------------------------
                //                     if (displaySettingsWeStored.IsSupportedGPUScaling)
                //                     {
                //                         SWIGTYPE_p_p_adlx__IADLXDisplayGPUScaling ppGPUScaling = ADLX.new_displayGPUScalingP_Ptr();
                //                         status = displayService.GetGPUScaling(display, ppGPUScaling);
                //                         if (status == ADLX_RESULT.ADLX_OK)
                //                         {
                //                             IADLXDisplayGPUScaling gpuScaling = ADLX.displayGPUScalingP_Ptr_value(ppGPUScaling);
                                            
                //                             // Check if current hardware supports GPU Scaling
                //                             SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                //                             status = gpuScaling.IsSupported(pIsSupported);
                //                             bool currentHardwareSupportsGPUScaling = ADLX.boolP_value(pIsSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsGPUScaling)
                //                             {
                //                                 // Get current GPU Scaling enabled state
                //                                 SWIGTYPE_p_bool pIsEnabled = ADLX.new_boolP();
                //                                 status = gpuScaling.IsEnabled(pIsEnabled);
                //                                 bool isEnabled = ADLX.boolP_value(pIsEnabled);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && isEnabled != displaySettingsWeStored.IsEnabledGPUScaling)
                //                                 {
                //                                     status = gpuScaling.SetEnabled(displaySettingsWeStored.IsEnabledGPUScaling);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set GPU Scaling to {displaySettingsWeStored.IsEnabledGPUScaling}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: GPU Scaling already set to desired value {displaySettingsWeStored.IsEnabledGPUScaling}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: GPU Scaling not supported by current hardware, skipping");
                //                             }
                //                             gpuScaling.Release();
                //                         }
                //                     }

                //                     //------------------------------------
                //                     // SET INTEGER SCALING IF NEEDED
                //                     //------------------------------------
                //                     if (displaySettingsWeStored.IsSupportedIntegerScaling)
                //                     {
                //                         SWIGTYPE_p_p_adlx__IADLXDisplayIntegerScaling ppIntegerScaling = ADLX.new_displayIntegerScalingP_Ptr();
                //                         status = displayService.GetIntegerScaling(display, ppIntegerScaling);
                //                         if (status == ADLX_RESULT.ADLX_OK)
                //                         {
                //                             IADLXDisplayIntegerScaling integerScaling = ADLX.displayIntegerScalingP_Ptr_value(ppIntegerScaling);
                                            
                //                             // Check if current hardware supports Integer Scaling
                //                             SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                //                             status = integerScaling.IsSupported(pIsSupported);
                //                             bool currentHardwareSupportsIntegerScaling = ADLX.boolP_value(pIsSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsIntegerScaling)
                //                             {
                //                                 // Get current Integer Scaling enabled state
                //                                 SWIGTYPE_p_bool pIsEnabled = ADLX.new_boolP();
                //                                 status = integerScaling.IsEnabled(pIsEnabled);
                //                                 bool isEnabled = ADLX.boolP_value(pIsEnabled);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && isEnabled != displaySettingsWeStored.IsEnabledIntegerScaling)
                //                                 {
                //                                     status = integerScaling.SetEnabled(displaySettingsWeStored.IsEnabledIntegerScaling);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set Integer Scaling to {displaySettingsWeStored.IsEnabledIntegerScaling}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Integer Scaling already set to desired value {displaySettingsWeStored.IsEnabledIntegerScaling}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Integer Scaling not supported by current hardware, skipping");
                //                             }
                //                             integerScaling.Release();
                //                         }
                //                     }

                //                     //------------------------------------
                //                     // SET PIXEL FORMAT IF NEEDED
                //                     //------------------------------------
                //                     if (displaySettingsWeStored.IsSupportedPixelFormat)
                //                     {
                //                         SWIGTYPE_p_p_adlx__IADLXDisplayPixelFormat ppPixelFormat = ADLX.new_displayPixelFormatP_Ptr();
                //                         status = displayService.GetPixelFormat(display, ppPixelFormat);
                //                         if (status == ADLX_RESULT.ADLX_OK)
                //                         {
                //                             IADLXDisplayPixelFormat pixelFormat = ADLX.displayPixelFormatP_Ptr_value(ppPixelFormat);
                                            
                //                             // Check if current hardware supports Pixel Format
                //                             SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                //                             status = pixelFormat.IsSupported(pIsSupported);
                //                             bool currentHardwareSupportsPixelFormat = ADLX.boolP_value(pIsSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsPixelFormat)
                //                             {
                //                                 // Get current pixel format value
                //                                 SWIGTYPE_p_ADLX_PIXEL_FORMAT pPixelFormat = ADLX.new_adlx_pixelFormatP();
                //                                 status = pixelFormat.GetValue(pPixelFormat);
                //                                 ADLX_PIXEL_FORMAT currentPixelFormat = ADLX.adlx_pixelFormatP_value(pPixelFormat);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && currentPixelFormat != displaySettingsWeStored.CurrentPixelFormat)
                //                                 {
                //                                     status = pixelFormat.SetValue(displaySettingsWeStored.CurrentPixelFormat);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set Pixel Format to {displaySettingsWeStored.CurrentPixelFormat.ToString("G")}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Pixel Format already set to desired value {displaySettingsWeStored.CurrentPixelFormat.ToString("G")}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Pixel Format not supported by current hardware, skipping");
                //                             }
                //                             pixelFormat.Release();
                //                         }
                //                     }

                //                     //------------------------------------
                //                     // SET SCALING MODE IF NEEDED
                //                     //------------------------------------
                //                     if (displaySettingsWeStored.IsSupportedScalingMode)
                //                     {
                //                         SWIGTYPE_p_p_adlx__IADLXDisplayScalingMode ppScalingMode = ADLX.new_displayScalingModeP_Ptr();
                //                         status = displayService.GetScalingMode(display, ppScalingMode);
                //                         if (status == ADLX_RESULT.ADLX_OK)
                //                         {
                //                             IADLXDisplayScalingMode scalingMode = ADLX.displayScalingModeP_Ptr_value(ppScalingMode);
                                            
                //                             // Check if current hardware supports Scaling Mode
                //                             SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                //                             status = scalingMode.IsSupported(pIsSupported);
                //                             bool currentHardwareSupportsScalingMode = ADLX.boolP_value(pIsSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsScalingMode)
                //                             {
                //                                 // Get current scaling mode value
                //                                 SWIGTYPE_p_ADLX_SCALE_MODE pCurrentMode = ADLX.new_adlx_scaleModeP();
                //                                 status = scalingMode.GetMode(pCurrentMode);
                //                                 ADLX_SCALE_MODE currentMode = ADLX.adlx_scaleModeP_value(pCurrentMode);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && currentMode != displaySettingsWeStored.CurrentScalingMode)
                //                                 {
                //                                     status = scalingMode.SetMode(displaySettingsWeStored.CurrentScalingMode);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set Scaling Mode to {displaySettingsWeStored.CurrentScalingMode.ToString("G")}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Scaling Mode already set to desired value {displaySettingsWeStored.CurrentScalingMode.ToString("G")}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Scaling Mode not supported by current hardware, skipping");
                //                             }
                //                             scalingMode.Release();
                //                         }
                //                     }

                //                     //------------------------------------
                //                     // SET VSR IF NEEDED
                //                     //------------------------------------
                //                     if (displaySettingsWeStored.IsSupportedVSR)
                //                     {
                //                         SWIGTYPE_p_p_adlx__IADLXDisplayVSR ppVSR = ADLX.new_displayVSRP_Ptr();
                //                         status = displayService.GetVirtualSuperResolution(display, ppVSR);
                //                         if (status == ADLX_RESULT.ADLX_OK)
                //                         {
                //                             IADLXDisplayVSR vsr = ADLX.displayVSRP_Ptr_value(ppVSR);
                                            
                //                             // Check if current hardware supports VSR
                //                             SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                //                             status = vsr.IsSupported(pIsSupported);
                //                             bool currentHardwareSupportsVSR = ADLX.boolP_value(pIsSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsVSR)
                //                             {
                //                                 // Get current VSR enabled state
                //                                 SWIGTYPE_p_bool pIsEnabled = ADLX.new_boolP();
                //                                 status = vsr.IsEnabled(pIsEnabled);
                //                                 bool isEnabled = ADLX.boolP_value(pIsEnabled);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && isEnabled != displaySettingsWeStored.IsEnabledVSR)
                //                                 {
                //                                     status = vsr.SetEnabled(displaySettingsWeStored.IsEnabledVSR);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set VSR to {displaySettingsWeStored.IsEnabledVSR}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: VSR already set to desired value {displaySettingsWeStored.IsEnabledVSR}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: VSR not supported by current hardware, skipping");
                //                             }
                //                             vsr.Release();
                //                         }
                //                     }

                //                     //------------------------------------
                //                     // SET HDCP IF NEEDED
                //                     //------------------------------------
                //                     if (displaySettingsWeStored.IsSupportedHDCP)
                //                     {
                //                         SWIGTYPE_p_p_adlx__IADLXDisplayHDCP ppHDCP = ADLX.new_displayHDCPP_Ptr();
                //                         status = displayService.GetHDCP(display, ppHDCP);
                //                         if (status == ADLX_RESULT.ADLX_OK)
                //                         {
                //                             IADLXDisplayHDCP hdcp = ADLX.displayHDCPP_Ptr_value(ppHDCP);
                                            
                //                             // Check if current hardware supports HDCP
                //                             SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                //                             status = hdcp.IsSupported(pIsSupported);
                //                             bool currentHardwareSupportsHDCP = ADLX.boolP_value(pIsSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsHDCP)
                //                             {
                //                                 // Get current HDCP enabled state
                //                                 SWIGTYPE_p_bool pIsEnabled = ADLX.new_boolP();
                //                                 status = hdcp.IsEnabled(pIsEnabled);
                //                                 bool isEnabled = ADLX.boolP_value(pIsEnabled);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && isEnabled != displaySettingsWeStored.IsEnabledHDCP)
                //                                 {
                //                                     status = hdcp.SetEnabled(displaySettingsWeStored.IsEnabledHDCP);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set HDCP to {displaySettingsWeStored.IsEnabledHDCP}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: HDCP already set to desired value {displaySettingsWeStored.IsEnabledHDCP}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: HDCP not supported by current hardware, skipping");
                //                             }
                //                             hdcp.Release();
                //                         }
                //                     }

                //                     //------------------------------------
                //                     // SET VARIBRIGHT IF NEEDED
                //                     //------------------------------------
                //                     if (displaySettingsWeStored.IsSupportedVariBright)
                //                     {
                //                         SWIGTYPE_p_p_adlx__IADLXDisplayVariBright ppVariBright = ADLX.new_displayVariBrightP_Ptr();
                //                         status = displayService.GetVariBright(display, ppVariBright);
                //                         if (status == ADLX_RESULT.ADLX_OK)
                //                         {
                //                             IADLXDisplayVariBright variBright = ADLX.displayVariBrightP_Ptr_value(ppVariBright);
                                            
                //                             // Check if current hardware supports VariBright
                //                             SWIGTYPE_p_bool pIsSupported = ADLX.new_boolP();
                //                             status = variBright.IsSupported(pIsSupported);
                //                             bool currentHardwareSupportsVariBright = ADLX.boolP_value(pIsSupported);

                //                             if (status == ADLX_RESULT.ADLX_OK && currentHardwareSupportsVariBright)
                //                             {
                //                                 // Get current VariBright enabled state
                //                                 SWIGTYPE_p_bool pIsEnabled = ADLX.new_boolP();
                //                                 status = variBright.IsEnabled(pIsEnabled);
                //                                 bool isEnabled = ADLX.boolP_value(pIsEnabled);

                //                                 // Only change if current value differs from wanted value
                //                                 if (status == ADLX_RESULT.ADLX_OK && isEnabled != displaySettingsWeStored.IsEnabledVariBright)
                //                                 {
                //                                     status = variBright.SetEnabled(displaySettingsWeStored.IsEnabledVariBright);
                //                                     if (status == ADLX_RESULT.ADLX_OK)
                //                                     {
                //                                         SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: Successfully set VariBright to {displaySettingsWeStored.IsEnabledVariBright}");
                //                                     }
                //                                 }
                //                                 else if (status == ADLX_RESULT.ADLX_OK)
                //                                 {
                //                                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: VariBright already set to desired value {displaySettingsWeStored.IsEnabledVariBright}, skipping");
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: VariBright not supported by current hardware, skipping");
                //                             }
                //                             variBright.Release();
                //                         }
                //                     }
                //                 }
                //                 else
                //                 {
                //                     SharedLogger.logger.Trace($"AMDLibrary/SetActiveConfigOverride: No display with UniqueID {id} found in the stored settings, so skipping.");
                //                 }

                //             }

                //             // Release display interface
                //             display.Release();
                //         }
                //     }
                //     // Release display list interface
                //     displayList.Release();
                // }

                // // Release display services interface
                // displayService.Release();

            }
            else
            {
                SharedLogger.logger.Error($"AMDLibrary/SetActiveConfigOverride: ERROR - Tried to run SetActiveConfigOverride but the AMD ADLX library isn't initialised!");
                throw new AMDLibraryException($"Tried to run SetActiveConfigOverride but the AMD ADLX library isn't initialised!");
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
                
                SharedLogger.logger.Trace($"AMDLibrary/GetCurrentDisplayIdentifiers: Attempting to get the ADLX desktop list");
                try
                {
                    // Get display list of the displays urrently in use
                    var displaysList =_adlxSystem.EnumerateDisplaysInUse();  
                    foreach (var display in displaysList)
                    {

                        // Get the GPU related to this display
                        var displaypGPU = display.GetGPU();

                        // Create an array of all the important display info we need to record
                        List<string> displayInfo = new List<string>();
                        displayInfo.Add("AMDADLX");
                        try
                        {
                            displayInfo.Add(displaypGPU.Name);
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting GPU Name from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(displaypGPU.UniqueId.ToString());
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting GPU Unique ID from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(displaypGPU.IsExternal.ToString());
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting GPU Is External from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(display.ConnectorType.ToString("G"));
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Connection Type for the display from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(display.Name);
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Name for the display from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(display.Type.ToString("G"));
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Display Type for the display from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(display.ManufacturerId.ToString());
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Manufacturer for the display from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(display.UniqueId.ToString());
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception getting ADLX Display Unique ID for the display from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        // Create a display identifier out of it
                        string displayIdentifier = System.String.Join("|", displayInfo);
                        // Add it to the list of display identifiers so we can return it
                        // but only add it if it doesn't already exist. Otherwise we get duplicates :/
                        if (!displayIdentifiers.Contains(displayIdentifier))
                        {
                            displayIdentifiers.Add(displayIdentifier);
                            SharedLogger.logger.Debug($"AMDLibrary/GetCurrentDisplayIdentifiers: DisplayIdentifier detected: {displayIdentifier}");
                        }                        

                    }
                }
                catch(Exception ex)
                {
                    SharedLogger.logger.Error(ex, $"AMDLibrary/GetCurrentDisplayIdentifiers: Exception trying to get the ADLX desktop services");
                    failure = true;
                    return new List<string>();
                }

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
                try
                {
                    var displaysList =_adlxSystem.EnumerateDisplays();  
                    foreach (var display in displaysList)
                    {

                        // Get the GPU related to this display
                        var displaypGPU = display.GetGPU();
                        // Create an array of all the important display info we need to record
                        List<string> displayInfo = new List<string>();
                        displayInfo.Add("AMDADLX");
                        try
                        {
                            displayInfo.Add(displaypGPU.Name);
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting GPU Name from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(displaypGPU.UniqueId.ToString());
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting GPU Unique ID from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(displaypGPU.IsExternal.ToString());
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting GPU Is External from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(display.ConnectorType.ToString("G"));
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Connection Type for the display from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(display.Name);
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Name for the display from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(display.Type.ToString("G"));
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Display Type for the display from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(display.ManufacturerId.ToString());
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Manufacturer for the display from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        try
                        {
                            displayInfo.Add(display.UniqueId.ToString());
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Warn(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception getting ADLX Display Unique ID for the display from video card. Substituting with a # instead");
                            displayInfo.Add("#");
                        }
                        // Create a display identifier out of it
                        string displayIdentifier = System.String.Join("|", displayInfo);
                        // Add it to the list of display identifiers so we can return it
                        // but only add it if it doesn't already exist. Otherwise we get duplicates :/
                        if (!displayIdentifiers.Contains(displayIdentifier))
                        {
                            displayIdentifiers.Add(displayIdentifier);
                            SharedLogger.logger.Debug($"AMDLibrary/GetAllConnectedDisplayIdentifiers: DisplayIdentifier detected: {displayIdentifier}");
                        }                        

                    }
                }
                catch(Exception ex)
                {
                    SharedLogger.logger.Error(ex, $"AMDLibrary/GetAllConnectedDisplayIdentifiers: Exception trying to get the ADLX desktop services");
                    failure = true;
                    return new List<string>();
                }

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