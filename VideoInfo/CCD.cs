﻿using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Display.Core;

namespace DisplayMagicianShared.Windows
{

    public enum WIN32STATUS : UInt32
    {
        ERROR_SUCCESS = 0,
        ERROR_ACCESS_DENIED = 5,
        ERROR_NOT_SUPPORTED = 50,
        ERROR_GEN_FAILURE = 31,
        ERROR_INVALID_PARAMETER = 87,
        ERROR_INSUFFICIENT_BUFFER = 122,
        ERROR_BAD_CONFIGURATION = 1610,
    }

    public enum DPI_AWARENESS_CONTEXT : Int32
    {
        DPI_AWARENESS_CONTEXT_UNDEFINED = 0,
        DPI_AWARENESS_CONTEXT_UNAWARE = -1, //' DPI unaware. This window does not scale for DPI changes and is always assumed to have a scale factor of 100% (96 DPI). It will be automatically scaled by the system on any other DPI setting.
        DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = -2, //' System DPI aware. This window does not scale for DPI changes. It will query for the DPI once and use that value for the lifetime of the process. If the DPI changes, the process will not adjust to the new DPI value. It will be automatically scaled up or down by the system when the DPI changes from the system value.
        DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = -3, // ' Per monitor DPI aware. This window checks for the DPI when it is created and adjusts the scale factor whenever the DPI changes. These processes are not automatically scaled by the system.
        DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = -4, //' Also known as Per Monitor v2. An advancement over the original per-monitor DPI awareness mode, which enables applications to access new DPI-related scaling behaviors on a per top-level window basis.
        DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED = -5, //' DPI unaware with improved quality of GDI-based content. This mode behaves similarly to DPI_AWARENESS_CONTEXT_UNAWARE, but also enables the system to automatically improve the rendering quality of text and other GDI-based primitives when the window is displayed on a high-DPI monitor.
    };

    public enum DISPLAYCONFIG_DEVICE_INFO_TYPE : Int32
    {
        // MS Private API (which seems to use negative numbers)
        // See https://github.com/lihas/windows-DPI-scaling-sample/blob/master/DPIHelper/DpiHelper.h from Sahil Singh
        DISPLAYCONFIG_DEVICE_INFO_GET_MONITOR_INTERNAL_INFO = DISPLAYCONFIG_DEVICE_INFO_GET_MONITOR_BRIGHTNESS_INFO, //alias for DISPLAYCONFIG_DEVICE_INFO_GET_MONITOR_BRIGHTNESS_INFO since it returns values other than brightness
        DISPLAYCONFIG_DEVICE_INFO_GET_MONITOR_UNIQUE_NAME = DISPLAYCONFIG_DEVICE_INFO_GET_MONITOR_BRIGHTNESS_INFO, //Another Alias since we are using the parameter mainly for getting the display unique name
        DISPLAYCONFIG_DEVICE_INFO_TERRY_TEST_MINUS_EIGHT = -8, //???
        DISPLAYCONFIG_DEVICE_INFO_GET_MONITOR_BRIGHTNESS_INFO = -7, //Get monitor brightness info
        DISPLAYCONFIG_DEVICE_INFO_TERRY_TEST_MINUS_SIX = -6, //???
        DISPLAYCONFIG_DEVICE_INFO_TERRY_TEST_MINUS_FIVE = -5, //???
        DISPLAYCONFIG_DEVICE_INFO_SET_DPI_SCALE = -4, // Set current dpi scaling value for a display
        DISPLAYCONFIG_DEVICE_INFO_GET_DPI_SCALE = -3, // Returns min, max, suggested, and currently applied DPI scaling values.
        DISPLAYCONFIG_DEVICE_INFO_TERRY_TEST_MINUS_TWO = -2, //???
        DISPLAYCONFIG_DEVICE_INFO_TERRY_TEST_MINUS_ONE = -1, //???

        // MS Public API
        Zero = 0,
        DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME = 1, // Specifies the source name of the display device. If the DisplayConfigGetDeviceInfo function is successful, DisplayConfigGetDeviceInfo returns the source name in the DISPLAYCONFIG_SOURCE_DEVICE_NAME structure.
        DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME = 2, // Specifies information about the monitor. If the DisplayConfigGetDeviceInfo function is successful, DisplayConfigGetDeviceInfo returns info about the monitor in the DISPLAYCONFIG_TARGET_DEVICE_NAME structure.
        DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE = 3, // Specifies information about the preferred mode of a monitor. If the DisplayConfigGetDeviceInfo function is successful, DisplayConfigGetDeviceInfo returns info about the preferred mode of a monitor in the DISPLAYCONFIG_TARGET_PREFERRED_MODE structure.
        DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME = 4, // Specifies the graphics adapter name. If the DisplayConfigGetDeviceInfo function is successful, DisplayConfigGetDeviceInfo returns the adapter name in the DISPLAYCONFIG_ADAPTER_NAME structure.
        DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE = 5, // Specifies how to set the monitor. If the DisplayConfigSetDeviceInfo function is successful, DisplayConfigSetDeviceInfo uses info in the DISPLAYCONFIG_SET_TARGET_PERSISTENCE structure to force the output in a boot-persistent manner.
        DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE = 6, // Specifies how to set the base output technology for a given target ID. If the DisplayConfigGetDeviceInfo function is successful, DisplayConfigGetDeviceInfo returns base output technology info in the DISPLAYCONFIG_TARGET_BASE_TYPE structure.
                                                            // Supported by WDDM 1.3 and later user-mode display drivers running on Windows 8.1 and later.
        DISPLAYCONFIG_DEVICE_INFO_GET_SUPPORT_VIRTUAL_RESOLUTION = 7, // Specifies the state of virtual mode support. If the DisplayConfigGetDeviceInfo function is successful, DisplayConfigGetDeviceInfo returns virtual mode support information in the DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION structure. Supported starting in Windows 10.
        DISPLAYCONFIG_DEVICE_INFO_SET_SUPPORT_VIRTUAL_RESOLUTION = 8, // Specifies how to set the state of virtual mode support. If the DisplayConfigSetDeviceInfo function is successful, DisplayConfigSetDeviceInfo uses info in the DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION structure to change the state of virtual mode support. Supported starting in Windows 10.
        DISPLAYCONFIG_DEVICE_INFO_GET_ADVANCED_COLOR_INFO = 9, // Specifies information about the state of the HDR Color for a display
        DISPLAYCONFIG_DEVICE_INFO_SET_ADVANCED_COLOR_STATE = 10, // Enables or disables the HDR Color for a display
        DISPLAYCONFIG_DEVICE_INFO_GET_SDR_WHITE_LEVEL = 11, // Specifies the current SDR white level for an HDR monitor. If the DisplayConfigGetDeviceInfo function is successful, DisplayConfigGetDeviceInfo return SDR white level info in the DISPLAYCONFIG_SDR_WHITE_LEVEL structure.
                                                            // Supported starting in Windows�10 Fall Creators Update (Version 1709).
        DISPLAYCONFIG_DEVICE_INFO_GET_MONITOR_SPECIALIZATION = 12,
        DISPLAYCONFIG_DEVICE_INFO_SET_MONITOR_SPECIALIZATION = 13,
        //DISPLAYCONFIG_DEVICE_INFO_FORCE_UINT32 = 0xFFFFFFFF // Only here to 
    }

    [Flags]
    public enum DISPLAYCONFIG_COLOR_ENCODING : UInt32
    {
        DISPLAYCONFIG_COLOR_ENCODING_RGB = 0,
        DISPLAYCONFIG_COLOR_ENCODING_YCBCR444 = 1,
        DISPLAYCONFIG_COLOR_ENCODING_YCBCR422 = 2,
        DISPLAYCONFIG_COLOR_ENCODING_YCBCR420 = 3,
        DISPLAYCONFIG_COLOR_ENCODING_INTENSITY = 4,
    }

    [Flags]
    public enum DISPLAYCONFIG_SCALING : UInt32
    {
        Zero = 0,
        DISPLAYCONFIG_SCALING_IDENTITY = 1,
        DISPLAYCONFIG_SCALING_CENTERED = 2,
        DISPLAYCONFIG_SCALING_STRETCHED = 3,
        DISPLAYCONFIG_SCALING_ASPECTRATIOCENTEREDMAX = 4,
        DISPLAYCONFIG_SCALING_CUSTOM = 5,
        DISPLAYCONFIG_SCALING_PREFERRED = 128,
        DISPLAYCONFIG_SCALING_FORCEUINT32 = 0xFFFFFFFF,
    }

    [Flags]
    public enum DISPLAYCONFIG_ROTATION : UInt32
    {
        Zero = 0,
        DISPLAYCONFIG_ROTATION_IDENTITY = 1,
        DISPLAYCONFIG_ROTATION_ROTATE90 = 2,
        DISPLAYCONFIG_ROTATION_ROTATE180 = 3,
        DISPLAYCONFIG_ROTATION_ROTATE270 = 4,
        DISPLAYCONFIG_ROTATION_FORCEUINT32 = 0xFFFFFFFF,
    }

    [Flags]
    public enum DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY : UInt32
    {
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_OTHER = 4294967295, // - 1
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15 = 0,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO = 1,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO = 2,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO = 3,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI = 4,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI = 5,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS = 6,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN = 8,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI = 9,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL = 10,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED = 11,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL = 12,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED = 13,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE = 14,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST = 15,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INDIRECT_WIRED = 16,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INDIRECT_VIRTUAL = 17,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_public = 0x80000000,
        DISPLAYCONFIG_OUTPUT_TECHNOLOGY_FORCEUINT32 = 0xFFFFFFFF,
    }

    [Flags]
    public enum DISPLAYCONFIG_TOPOLOGY_ID : UInt32
    {
        Zero = 0x0,
        DISPLAYCONFIG_TOPOLOGY_INTERNAL = 0x00000001,
        DISPLAYCONFIG_TOPOLOGY_CLONE = 0x00000002,
        DISPLAYCONFIG_TOPOLOGY_EXTEND = 0x00000004,
        DISPLAYCONFIG_TOPOLOGY_EXTERNAL = 0x00000008,
        DISPLAYCONFIG_TOPOLOGY_FORCEUINT32 = 0xFFFFFFFF,
    }

    [Flags]
    public enum DISPLAYCONFIG_PATH_FLAGS : UInt32
    {
        Zero = 0x0,
        DISPLAYCONFIG_PATH_ACTIVE = 0x00000001,
        DISPLAYCONFIG_PATH_PREFERRED_UNSCALED = 0x00000004,
        DISPLAYCONFIG_PATH_SUPPORT_VIRTUAL_MODE = 0x00000008,
    }

    [Flags]
    public enum DISPLAYCONFIG_SOURCE_FLAGS : UInt32
    {
        Zero = 0x0,
        DISPLAYCONFIG_SOURCE_IN_USE = 0x00000001,
    }

    [Flags]
    public enum DISPLAYCONFIG_TARGET_FLAGS : UInt32
    {
        Zero = 0x0,
        DISPLAYCONFIG_TARGET_IN_USE = 0x00000001,
        DISPLAYCONFIG_TARGET_FORCIBLE = 0x00000002,
        DISPLAYCONFIG_TARGET_FORCED_AVAILABILITY_BOOT = 0x00000004,
        DISPLAYCONFIG_TARGET_FORCED_AVAILABILITY_PATH = 0x00000008,
        DISPLAYCONFIG_TARGET_FORCED_AVAILABILITY_SYSTEM = 0x00000010,
        DISPLAYCONFIG_TARGET_IS_HMD = 0x00000020,
    }

    [Flags]
    public enum QDC : UInt32
    {
        Zero = 0x0,
        // Get all paths
        QDC_ALL_PATHS = 0x00000001,
        // Get only the active paths currently in use
        QDC_ONLY_ACTIVE_PATHS = 0x00000002,
        // Get the currently active paths as stored in the display database
        QDC_DATABASE_CURRENT = 0x00000004,
        // This flag should be bitwise OR'ed with other flags to indicate that the caller is aware of virtual mode support.  Supported starting in Windows 10.
        QDC_VIRTUAL_MODE_AWARE = 0x00000010,
        // This flag should be bitwise OR'ed with QDC_ONLY_ACTIVE_PATHS to indicate that the caller would like to include head-mounted displays (HMDs) in the list of active paths. See Remarks for more information. 
        // Supported starting in Windows 10 1703 Creators Update.
        QDC_INCLUDE_HMD = 0x00000020,
        // This flag should be bitwise OR'ed with other flags to indicate that the caller is aware of virtual refresh rate support.
        // Supported starting in Windows 11.
        QDC_VIRTUAL_REFRESH_RATE_AWARE = 0x00000040,
    }

    [Flags]
    public enum SDC : UInt32
    {
        Zero = 0x0,
        SDC_TOPOLOGY_public = 0x00000001,
        SDC_TOPOLOGY_CLONE = 0x00000002,
        SDC_TOPOLOGY_EXTEND = 0x00000004,
        SDC_TOPOLOGY_EXTERNAL = 0x00000008,
        SDC_TOPOLOGY_SUPPLIED = 0x00000010,
        SDC_USE_DATABASE_CURRENT = (SDC_TOPOLOGY_public | SDC_TOPOLOGY_CLONE | SDC_TOPOLOGY_EXTEND | SDC_TOPOLOGY_EXTERNAL),
        SDC_USE_SUPPLIED_DISPLAY_CONFIG = 0x00000020,
        SDC_VALIDATE = 0x00000040,
        SDC_APPLY = 0x00000080,
        SDC_NO_OPTIMIZATION = 0x00000100,
        SDC_SAVE_TO_DATABASE = 0x00000200,
        SDC_ALLOW_CHANGES = 0x00000400,
        SDC_PATH_PERSIST_IF_REQUIRED = 0x00000800,
        SDC_FORCE_MODE_ENUMERATION = 0x00001000,
        SDC_ALLOW_PATH_ORDER_CHANGES = 0x00002000,
        SDC_VIRTUAL_MODE_AWARE = 0x00008000,
        SDC_VIRTUAL_REFRESH_RATE_AWARE = 0x00020000,

        // Special common combinations (only set in this library)
        TEST_IF_VALID_DISPLAYCONFIG = (SDC_VALIDATE | SDC_USE_SUPPLIED_DISPLAY_CONFIG),
        TEST_IF_VALID_DISPLAYCONFIG_WITH_TWEAKS = (SDC_VALIDATE | SDC_USE_SUPPLIED_DISPLAY_CONFIG | SDC_ALLOW_CHANGES),
        SET_DISPLAYCONFIG_AND_SAVE = (SDC_APPLY | SDC_USE_SUPPLIED_DISPLAY_CONFIG | SDC_SAVE_TO_DATABASE),
        SET_DISPLAYCONFIG_WITH_TWEAKS_AND_SAVE = (SDC_APPLY | SDC_USE_SUPPLIED_DISPLAY_CONFIG | SDC_ALLOW_CHANGES | SDC_SAVE_TO_DATABASE),
        DISPLAYMAGICIAN_SET = (SDC_APPLY | SDC_USE_SUPPLIED_DISPLAY_CONFIG | SDC_ALLOW_CHANGES | SDC_SAVE_TO_DATABASE),
        DISPLAYMAGICIAN_VALIDATE = (SDC_VALIDATE | SDC_USE_SUPPLIED_DISPLAY_CONFIG | SDC_ALLOW_CHANGES | SDC_SAVE_TO_DATABASE),
        //DISPLAYMAGICIAN_SET = (SDC_APPLY | SDC_TOPOLOGY_SUPPLIED | SDC_ALLOW_CHANGES | SDC_ALLOW_PATH_ORDER_CHANGES ),
        //DISPLAYMAGICIAN_VALIDATE = (SDC_VALIDATE | SDC_TOPOLOGY_SUPPLIED | SDC_ALLOW_CHANGES | SDC_ALLOW_PATH_ORDER_CHANGES ),

        SET_DISPLAYCONFIG_BUT_NOT_SAVE = (SDC_APPLY | SDC_USE_SUPPLIED_DISPLAY_CONFIG),
        TEST_IF_CLONE_VALID = (SDC_VALIDATE | SDC_TOPOLOGY_CLONE),
        SET_CLONE_TOPOLOGY = (SDC_APPLY | SDC_TOPOLOGY_CLONE),
        SET_CLONE_TOPOLOGY_WITH_PATH_PERSISTENCE = (SDC_APPLY | SDC_TOPOLOGY_CLONE | SDC_PATH_PERSIST_IF_REQUIRED),
        RESET_DISPLAYCONFIG_TO_LAST_SAVED = (SDC_APPLY | SDC_USE_DATABASE_CURRENT),
        SET_DISPLAYCONFIG_USING_PATHS_ONLY_AND_SAVE = (SDC_APPLY | SDC_TOPOLOGY_SUPPLIED | SDC_ALLOW_PATH_ORDER_CHANGES),
    }

    [Flags]
    public enum DISPLAYCONFIG_SCANLINE_ORDERING : UInt32
    {
        DISPLAYCONFIG_SCANLINE_ORDERING_UNSPECIFIED = 0,
        DISPLAYCONFIG_SCANLINE_ORDERING_PROGRESSIVE = 1,
        DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED = 2,
        DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_UPPERFIELDFIRST = DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED,
        DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_LOWERFIELDFIRST = 3,
        DISPLAYCONFIG_SCANLINE_ORDERING_FORCEUINT32 = 0xFFFFFFFF,
    }

    [Flags]
    public enum DISPLAYCONFIG_PIXELFORMAT : UInt32
    {
        Zero = 0x0,
        DISPLAYCONFIG_PIXELFORMAT_8BPP = 1,
        DISPLAYCONFIG_PIXELFORMAT_16BPP = 2,
        DISPLAYCONFIG_PIXELFORMAT_24BPP = 3,
        DISPLAYCONFIG_PIXELFORMAT_32BPP = 4,
        DISPLAYCONFIG_PIXELFORMAT_NONGDI = 5,
        DISPLAYCONFIG_PIXELFORMAT_FORCEUINT32 = 0xFFFFFFFF,
    }

    [Flags]
    public enum DISPLAYCONFIG_MODE_INFO_TYPE : UInt32
    {
        Zero = 0x0,
        DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE = 1,
        DISPLAYCONFIG_MODE_INFO_TYPE_TARGET = 2,
        DISPLAYCONFIG_MODE_INFO_TYPE_DESKTOP_IMAGE = 3,
        DISPLAYCONFIG_MODE_INFO_TYPE_FORCEUINT32 = 0xFFFFFFFF,
    }

    [Flags]
    public enum D3D_VIDEO_SIGNAL_STANDARD : UInt32
    {
        Uninitialized = 0,
        VesaDmt = 1,
        VesaGtf = 2,
        VesaCvt = 3,
        Ibm = 4,
        Apple = 5,
        NtscM = 6,
        NtscJ = 7,
        Ntsc443 = 8,
        PalB = 9,
        PalB1 = 10,
        PalG = 11,
        PalH = 12,
        PalI = 13,
        PalD = 14,
        PalN = 15,
        PalNc = 16,
        SecamB = 17,
        SecNVIDIA = 18,
        SecamG = 19,
        SecamH = 20,
        SecamK = 21,
        SecamK1 = 22,
        SecamL = 23,
        SecamL1 = 24,
        Eia861 = 25,
        Eia861A = 26,
        Eia861B = 27,
        PalK = 28,
        PalK1 = 29,
        PalL = 30,
        PalM = 31,
        Other = 255
    }


/*
* OS reports DPI scaling values in relative terms, and not absolute terms.
* eg. if current DPI value is 250%, and recommended value is 200%, then
* OS will give us integer 2 for DPI scaling value (starting from recommended
* DPI scaling move 2 steps to the right in this list).
* values observed (and extrapolated) from system settings app (immersive control panel).
*/
    public enum DPI_VALUES: UInt32
    { 
        DPI_100 = 100,
        DPI_125 = 125,
        DPI_150 = 150,
        DPI_175 = 175,
        DPI_200 = 200,
        DPI_225 = 225,
        DPI_250 = 250,
        DPI_300 = 300,
        DPI_350 = 350,
        DPI_400 = 400,
        DPI_450 = 450,
        DPI_500 = 500 
    };

    /*
   * struct DPIScalingInfo
   * @brief DPI info about a source
   * mininum :     minumum DPI scaling in terms of percentage supported by source. Will always be 100%.
   * maximum :     maximum DPI scaling in terms of percentage supported by source. eg. 100%, 150%, etc.
   * current :     currently applied DPI scaling value
   * recommended : DPI scaling value reommended by OS. OS takes resolution, physical size, and expected viewing distance
   *               into account while calculating this, however exact formula is not known, hence must be retrieved from OS
   *               For a system in which user has not explicitly changed DPI, current should eqaul recommended.
   * bInitDone :   If true, it means that the members of the struct contain values, as fetched from OS, and not the default
   *               ones given while object creation.
   */
    public struct DPIScalingInfo
    {
        public UInt32 Minimum;
        public UInt32 Maximum;
        public UInt32 Current;
        public UInt32 Recommended;

        public DPIScalingInfo()
        {
            Minimum = 100;
            Maximum = 100;
            Current = 100;
            Recommended = 100;
        }

        public override bool Equals(object obj) => obj is DPIScalingInfo other && this.Equals(other);
        public bool Equals(DPIScalingInfo other)
        {
            if (!Minimum.Equals(other.Minimum))
            {
                SharedLogger.logger.Trace($"DPIScalingInfo/Equals: The Minimum values don't equal each other");
                return false;
            }
            if (!Maximum.Equals(other.Maximum))
            {
                SharedLogger.logger.Trace($"DPIScalingInfo/Equals: The Maximum values don't equal each other");
                return false;
            }
            if (!Current.Equals(other.Current))
            {
                SharedLogger.logger.Trace($"DPIScalingInfo/Equals: The Current values don't equal each other");
                return false;
            }
            if (!Recommended.Equals(other.Recommended))
            {
                SharedLogger.logger.Trace($"DPIScalingInfo/Equals: The Recommended values don't equal each other");
                return false;
            }
            return true;
        }
        
        public override int GetHashCode()
        {
            return (Minimum, Maximum, Current, Recommended).GetHashCode();
        }

        public static bool operator ==(DPIScalingInfo lhs, DPIScalingInfo rhs) => lhs.Equals(rhs);

        public static bool operator !=(DPIScalingInfo lhs, DPIScalingInfo rhs) => !(lhs == rhs);
    };

    /*
    * struct DISPLAYCONFIG_SOURCE_DPI_SCALE_GET
    * @brief used to fetch min, max, suggested, and currently applied DPI scaling values.
    * All values are relative to the recommended DPI scaling value
    * Note that DPI scaling is a property of the source, and not of target.
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_SOURCE_DPI_SCALE_GET
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        /*
        * @brief min value of DPI scaling is always 100, minScaleRel gives no. of steps down from recommended scaling
        * eg. if minScaleRel is -3 => 100 is 3 steps down from recommended scaling => recommended scaling is 175%
        */
        public Int32 MinScaleRel;

        /*
        * @brief currently applied DPI scaling value wrt the recommended value. eg. if recommended value is 175%,
        * => if curScaleRel == 0 the current scaling is 175%, if curScaleRel == -1, then current scale is 150%
        */
        public Int32 CurrrentScaleRel;

        /*
        * @brief maximum supported DPI scaling wrt recommended value
        */
        public Int32 MaxScaleRel;
    };

    /*
    * struct DISPLAYCONFIG_SOURCE_DPI_SCALE_SET
    * @brief set DPI scaling value of a source
    * Note that DPI scaling is a property of the source, and not of target.
    */
    public struct DISPLAYCONFIG_SOURCE_DPI_SCALE_SET
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        /*
        * @brief The value we want to set. The value should be relative to the recommended DPI scaling value of source.
        * eg. if scaleRel == 1, and recommended value is 175% => we are trying to set 200% scaling for the source
        */
        public Int32 ScaleRel;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_DEVICE_INFO_HEADER : IEquatable<DISPLAYCONFIG_DEVICE_INFO_HEADER>
    {
        public DISPLAYCONFIG_DEVICE_INFO_TYPE Type;
        public uint Size;
        public LUID AdapterId;
        public uint Id;

        public DISPLAYCONFIG_DEVICE_INFO_HEADER()
        {
            Type = DISPLAYCONFIG_DEVICE_INFO_TYPE.Zero;
            Size = (uint)Marshal.SizeOf(typeof(DISPLAYCONFIG_DEVICE_INFO_HEADER));
            AdapterId = new LUID();
            Id = 0;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_DEVICE_INFO_HEADER other && this.Equals(other);

        public bool Equals(DISPLAYCONFIG_DEVICE_INFO_HEADER other)
        {
            if(Type != other.Type)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_DEVICE_INFO_HEADER/Equals: The Type values don't equal each other");
                return false;
            }
            if(Size != other.Size)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_DEVICE_INFO_HEADER/Equals: The Size values don't equal each other");
                return false;
            }
            if(Id != other.Id)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_DEVICE_INFO_HEADER/Equals: The Id values don't equal each other");
                return false;
            }
            return true;    
        }

        public override int GetHashCode()
        {
            return (Type, Size, AdapterId, Id).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_DEVICE_INFO_HEADER lhs, DISPLAYCONFIG_DEVICE_INFO_HEADER rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_DEVICE_INFO_HEADER lhs, DISPLAYCONFIG_DEVICE_INFO_HEADER rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO : IEquatable<DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO>
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        //[MarshalAs(UnmanagedType.U4)]
        public uint Value;
        public DISPLAYCONFIG_COLOR_ENCODING ColorEncoding;
        //[MarshalAs(UnmanagedType.U4)]
        public uint BitsPerColorChannel;

        public bool AdvancedColorSupported => (Value & 0x1) == 0x1;
        public bool AdvancedColorEnabled => (Value & 0x2) == 0x2;
        public bool WideColorEnforced => (Value & 0x4) == 0x4;
        public bool AdvancedColorForceDisabled => (Value & 0x8) == 0x8;

        public DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO()
        {
            Header = new DISPLAYCONFIG_DEVICE_INFO_HEADER();
            Value = 0;
            ColorEncoding = DISPLAYCONFIG_COLOR_ENCODING.DISPLAYCONFIG_COLOR_ENCODING_RGB;
            BitsPerColorChannel = 0;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO other && this.Equals(other);

        public bool Equals(DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO other)
        {
            if (!Header.Equals(other.Header))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO/Equals: The Header values don't equal each other");
                return false;
            }
            if(Value != other.Value)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO/Equals: The Value values don't equal each other");
                return false;
            }
            if (!ColorEncoding.Equals(other.ColorEncoding))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO/Equals: The ColorEncoding values don't equal each other");
                return false;
            }
            if(BitsPerColorChannel != other.BitsPerColorChannel)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO/Equals: The BitsPerColorChannel values don't equal each other");
                return false;
            }
            return true;
        }
            

        public override int GetHashCode()
        {
            return (Header, Value, ColorEncoding, BitsPerColorChannel).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO lhs, DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO lhs, DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINTL : IEquatable<POINTL>
    {
        public int X;
        public int Y;

        public POINTL()
        {
            X = 0;
            Y = 0;
        }

        public override bool Equals(object obj) => obj is POINTL other && this.Equals(other);
        public bool Equals(POINTL other)
        {
            if (X != other.X)
            {
                SharedLogger.logger.Trace($"POINTL/Equals: The X values don't equal each other");
                return false;
            }
            if (Y != other.Y)
            {
                SharedLogger.logger.Trace($"POINTL/Equals: The Y values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }

        public static bool operator ==(POINTL lhs, POINTL rhs) => lhs.Equals(rhs);

        public static bool operator !=(POINTL lhs, POINTL rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LUID : IEquatable<LUID>
    {
        public uint LowPart;
        public uint HighPart;

        public ulong Value => ((ulong)HighPart << 32) | LowPart;

        public LUID()
        {
            LowPart = 0;
            HighPart = 0;
        }

        public override bool Equals(object obj) => obj is LUID other && this.Equals(other);
        public bool Equals(LUID other)
        {
            if(LowPart != other.LowPart)
            {
                SharedLogger.logger.Trace($"LUID/Equals: The LowPart values don't equal each other");
                return false;
            }
            if(HighPart != other.HighPart)
            {
                SharedLogger.logger.Trace($"LUID/Equals: The HighPart values don't equal each other");
                return false;
            }
            return true;    
        }

        public override int GetHashCode()
        {
            return (LowPart, HighPart).GetHashCode();
        }

        public static bool operator ==(LUID lhs, LUID rhs) => lhs.Equals(rhs);

        public static bool operator !=(LUID lhs, LUID rhs) => !(lhs == rhs);

        public override string ToString() => Value.ToString();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_SOURCE_MODE : IEquatable<DISPLAYCONFIG_SOURCE_MODE>
    {
        public uint Width;
        public uint Height;
        public DISPLAYCONFIG_PIXELFORMAT PixelFormat;
        public POINTL Position;

        public DISPLAYCONFIG_SOURCE_MODE()
        {
            Width = 0;
            Height = 0;
            PixelFormat = DISPLAYCONFIG_PIXELFORMAT.DISPLAYCONFIG_PIXELFORMAT_32BPP;
            Position = new POINTL();
        }


        public override bool Equals(object obj) => obj is DISPLAYCONFIG_SOURCE_MODE other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_SOURCE_MODE other)
        {
            if(Width != other.Width)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SOURCE_MODE/Equals: The Width values don't equal each other");
                return false;
            }
            if(Height != other.Height)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SOURCE_MODE/Equals: The Height values don't equal each other");
                return false;
            }
            if (!PixelFormat.Equals(other.PixelFormat))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SOURCE_MODE/Equals: The PixelFormat values don't equal each other");
                return false;
            }
            if (!Position.Equals(other.Position))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SOURCE_MODE/Equals: The Position values don't equal each other");
                return false;
            }
            return true;
        }
            
        public override int GetHashCode()
        {
            return (Width, Height, PixelFormat, Position).GetHashCode();
        }
        public static bool operator ==(DISPLAYCONFIG_SOURCE_MODE lhs, DISPLAYCONFIG_SOURCE_MODE rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_SOURCE_MODE lhs, DISPLAYCONFIG_SOURCE_MODE rhs) => !(lhs == rhs);

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_RATIONAL : IEquatable<DISPLAYCONFIG_RATIONAL>
    {
        public uint Numerator;
        public uint Denominator;

        public DISPLAYCONFIG_RATIONAL()
        {
            Numerator = 0;
            Denominator = 0;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_RATIONAL other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_RATIONAL other)
        {
            if(Numerator != other.Numerator)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_RATIONAL/Equals: The Numerator values don't equal each other");
                return false;
            }
            if(Denominator != other.Denominator)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_RATIONAL/Equals: The Denominator values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Numerator, Denominator).GetHashCode();
        }
        public static bool operator ==(DISPLAYCONFIG_RATIONAL lhs, DISPLAYCONFIG_RATIONAL rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_RATIONAL lhs, DISPLAYCONFIG_RATIONAL rhs) => !(lhs == rhs);

        public override string ToString() => Numerator + " / " + Denominator;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_2DREGION : IEquatable<DISPLAYCONFIG_2DREGION>
    {
        public uint Cx;
        public uint Cy;

        public DISPLAYCONFIG_2DREGION()
        {
            Cx = 0;
            Cy = 0;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_2DREGION other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_2DREGION other)
        {
            if(Cx != other.Cx)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_2DREGION/Equals: The Cx values don't equal each other");
                return false;
            }
            if(Cy != other.Cy)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_2DREGION/Equals: The Cy values don't equal each other");
                return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            return (Cx, Cy).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_2DREGION lhs, DISPLAYCONFIG_2DREGION rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_2DREGION lhs, DISPLAYCONFIG_2DREGION rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_DESKTOP_IMAGE_INFO : IEquatable<DISPLAYCONFIG_DESKTOP_IMAGE_INFO>
    {
        public POINTL PathSourceSize;
        public RECTL DesktopImageRegion;
        public RECTL DesktopImageClip;

        public DISPLAYCONFIG_DESKTOP_IMAGE_INFO()
        {
            PathSourceSize = new POINTL();
            DesktopImageRegion = new RECTL();
            DesktopImageClip = new RECTL();
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_DESKTOP_IMAGE_INFO other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_DESKTOP_IMAGE_INFO other)
        {
            if (!PathSourceSize.Equals(other.PathSourceSize))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_DESKTOP_IMAGE_INFO/Equals: The PathSourceSize values don't equal each other");
                return false;
            }
            if (!DesktopImageRegion.Equals(other.DesktopImageRegion))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_DESKTOP_IMAGE_INFO/Equals: The DesktopImageRegion values don't equal each other");
                return false;
            }
            if (!DesktopImageClip.Equals(other.DesktopImageClip))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_DESKTOP_IMAGE_INFO/Equals: The DesktopImageClip values don't equal each other");
                return false;
            }
            return true;    
        }

        public override int GetHashCode()
        {
            return (PathSourceSize, DesktopImageRegion, DesktopImageClip).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_DESKTOP_IMAGE_INFO lhs, DISPLAYCONFIG_DESKTOP_IMAGE_INFO rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_DESKTOP_IMAGE_INFO lhs, DISPLAYCONFIG_DESKTOP_IMAGE_INFO rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_VIDEO_SIGNAL_INFO : IEquatable<DISPLAYCONFIG_VIDEO_SIGNAL_INFO>
    {
        public ulong PixelRate;
        public DISPLAYCONFIG_RATIONAL HSyncFreq;
        public DISPLAYCONFIG_RATIONAL VSyncFreq;
        public DISPLAYCONFIG_2DREGION ActiveSize;
        public DISPLAYCONFIG_2DREGION TotalSize;
        public D3D_VIDEO_SIGNAL_STANDARD VideoStandard;
        public DISPLAYCONFIG_SCANLINE_ORDERING ScanLineOrdering;

        public DISPLAYCONFIG_VIDEO_SIGNAL_INFO()
        {
            PixelRate = 0;
            HSyncFreq = new DISPLAYCONFIG_RATIONAL();
            VSyncFreq = new DISPLAYCONFIG_RATIONAL();
            ActiveSize = new DISPLAYCONFIG_2DREGION();
            TotalSize = new DISPLAYCONFIG_2DREGION();
            VideoStandard = D3D_VIDEO_SIGNAL_STANDARD.Uninitialized;
            ScanLineOrdering = DISPLAYCONFIG_SCANLINE_ORDERING.DISPLAYCONFIG_SCANLINE_ORDERING_UNSPECIFIED;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_VIDEO_SIGNAL_INFO other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_VIDEO_SIGNAL_INFO other)
        {
            if(PixelRate != other.PixelRate)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_VIDEO_SIGNAL_INFO/Equals: The PixelRate values don't equal each other");
                return false;
            }
            if (!HSyncFreq.Equals(other.HSyncFreq))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_VIDEO_SIGNAL_INFO/Equals: The HSyncFreq values don't equal each other");
                return false;
            }
            if (!VSyncFreq.Equals(other.VSyncFreq))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_VIDEO_SIGNAL_INFO/Equals: The VSyncFreq values don't equal each other");
                return false;
            }
            if (!ActiveSize.Equals(other.ActiveSize))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_VIDEO_SIGNAL_INFO/Equals: The ActiveSize values don't equal each other");
                return false;
            }
            if (!TotalSize.Equals(other.TotalSize))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_VIDEO_SIGNAL_INFO/Equals: The TotalSize values don't equal each other");
                return false;
            }
            if(VideoStandard != other.VideoStandard)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_VIDEO_SIGNAL_INFO/Equals: The VideoStandard values don't equal each other");
                return false;
            }
            if (!ScanLineOrdering.Equals(other.ScanLineOrdering))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_VIDEO_SIGNAL_INFO/Equals: The ScanLineOrdering values don't equal each other");
                return false;
            }
            return true;
        }            

        public override int GetHashCode()
        {
            return (PixelRate, HSyncFreq, VSyncFreq, ActiveSize, TotalSize, VideoStandard, ScanLineOrdering).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_VIDEO_SIGNAL_INFO lhs, DISPLAYCONFIG_VIDEO_SIGNAL_INFO rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_VIDEO_SIGNAL_INFO lhs, DISPLAYCONFIG_VIDEO_SIGNAL_INFO rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_TARGET_MODE : IEquatable<DISPLAYCONFIG_TARGET_MODE>
    {
        public DISPLAYCONFIG_VIDEO_SIGNAL_INFO TargetVideoSignalInfo;

        public DISPLAYCONFIG_TARGET_MODE()
        {
            TargetVideoSignalInfo = new DISPLAYCONFIG_VIDEO_SIGNAL_INFO();
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_TARGET_MODE other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_TARGET_MODE other)
        {
            if (!TargetVideoSignalInfo.Equals(other.TargetVideoSignalInfo))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_MODE/Equals: The TargetVideoSignalInfo values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (TargetVideoSignalInfo).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_TARGET_MODE lhs, DISPLAYCONFIG_TARGET_MODE rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_TARGET_MODE lhs, DISPLAYCONFIG_TARGET_MODE rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct DISPLAYCONFIG_PATH_SOURCE_INFO : IEquatable<DISPLAYCONFIG_PATH_SOURCE_INFO>
    {
        [FieldOffset(0)]
        public LUID AdapterId;
        [FieldOffset(8)]
        public uint Id;
        [FieldOffset(12)]
        public uint ModeInfoIdx;
        [FieldOffset(12)]
        public ushort cloneGroupId;
        [FieldOffset(14)]
        public ushort sourceModeInfoIdx;
        [FieldOffset(16)]
        public DISPLAYCONFIG_SOURCE_FLAGS StatusFlags;

        public DISPLAYCONFIG_PATH_SOURCE_INFO()
        {
            AdapterId = new LUID();
            Id = 0;
            ModeInfoIdx = 0;
            cloneGroupId = 0;
            sourceModeInfoIdx = 0;
            StatusFlags = DISPLAYCONFIG_SOURCE_FLAGS.Zero;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_PATH_SOURCE_INFO other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_PATH_SOURCE_INFO other)
        {
            // AdapterId.Equals(other.AdapterId) && // Removed the AdapterId from the Equals, as it changes after a reboot.
            //Id == other.Id &&  // Removed the ID from the list as the Display ID it maps to will change after a switch from surround to non-surround profile
            if (ModeInfoIdx != other.ModeInfoIdx)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_SOURCE_INFO/Equals: The ModeInfoIdx values don't equal each other");
                return false;
            }
            if (!StatusFlags.Equals(other.StatusFlags))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_SOURCE_INFO/Equals: The StatusFlags values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            //return (AdapterId, Id, ModeInfoIdx, StatusFlags).GetHashCode();
            return (ModeInfoIdx, Id, StatusFlags).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_PATH_SOURCE_INFO lhs, DISPLAYCONFIG_PATH_SOURCE_INFO rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_PATH_SOURCE_INFO lhs, DISPLAYCONFIG_PATH_SOURCE_INFO rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_PATH_TARGET_INFO : IEquatable<DISPLAYCONFIG_PATH_TARGET_INFO>
    {
        public LUID AdapterId;
        public uint Id;
        public uint ModeInfoIdx;
        public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY OutputTechnology;
        public DISPLAYCONFIG_ROTATION Rotation;
        public DISPLAYCONFIG_SCALING Scaling;
        public DISPLAYCONFIG_RATIONAL RefreshRate;
        public DISPLAYCONFIG_SCANLINE_ORDERING ScanLineOrdering;
        public bool TargetAvailable;
        public uint StatusFlags;

        public bool TargetInUse => (StatusFlags & 0x1) == 0x1;
        public bool TargetForcible => (StatusFlags & 0x2) == 0x2;
        public bool ForcedAvailabilityBoot => (StatusFlags & 0x4) == 0x4;
        public bool ForcedAvailabilityPath => (StatusFlags & 0x8) == 0x8;
        public bool ForcedAvailabilitySystem => (StatusFlags & 0x10) == 0x10;
        public bool IsHMD => (StatusFlags & 0x20) == 0x20;

        public DISPLAYCONFIG_PATH_TARGET_INFO()
        {
            AdapterId = new LUID();
            Id = 0;
            ModeInfoIdx = 0;
            OutputTechnology = DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15;
            Rotation = DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_IDENTITY;
            Scaling = DISPLAYCONFIG_SCALING.DISPLAYCONFIG_SCALING_STRETCHED;
            RefreshRate = new DISPLAYCONFIG_RATIONAL();
            ScanLineOrdering = DISPLAYCONFIG_SCANLINE_ORDERING.DISPLAYCONFIG_SCANLINE_ORDERING_UNSPECIFIED;
            TargetAvailable = false;
            StatusFlags = 0;
        }

        /* DISPLAYCONFIG_TARGET_IN_USE = 0x00000001,
        DISPLAYCONFIG_TARGET_FORCIBLE = 0x00000002,
        DISPLAYCONFIG_TARGET_FORCED_AVAILABILITY_BOOT = 0x00000004,
        DISPLAYCONFIG_TARGET_FORCED_AVAILABILITY_PATH = 0x00000008,
        DISPLAYCONFIG_TARGET_FORCED_AVAILABILITY_SYSTEM = 0x00000010,
        DISPLAYCONFIG_TARGET_IS_HMD = 0x00000020,*/
        public override bool Equals(object obj) => obj is DISPLAYCONFIG_PATH_TARGET_INFO other && this.Equals(other);

        public bool Equals(DISPLAYCONFIG_PATH_TARGET_INFO other)
        {
            // AdapterId.Equals(other.AdapterId) && // Removed the AdapterId from the Equals, as it changes after a reboot.
            // Id == other.Id &&  // Removed the ID from the list as the Display ID it maps to will change after a switch from surround to non-surround profile
            if (ModeInfoIdx != other.ModeInfoIdx)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_TARGET_INFO/Equals: The ModeInfoIdx values don't equal each other");
                return false;
            }
            if (!OutputTechnology.Equals(other.OutputTechnology))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_TARGET_INFO/Equals: The OutputTechnology values don't equal each other");
                return false;
            }
            if (!Rotation.Equals(other.Rotation))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_TARGET_INFO/Equals: The Rotation values don't equal each other");
                return false;
            }
            if (!Scaling.Equals(other.Scaling))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_TARGET_INFO/Equals: The Scaling values don't equal each other");
                return false;
            }
            if (!RefreshRate.Equals(other.RefreshRate))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_TARGET_INFO/Equals: The RefreshRate values don't equal each other");
                return false;
            }
            if (!ScanLineOrdering.Equals(other.ScanLineOrdering))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_TARGET_INFO/Equals: The ScanLineOrdering values don't equal each other");
                return false;
            }
            if (TargetAvailable != other.TargetAvailable)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_TARGET_INFO/Equals: The TargetAvailable values don't equal each other");
                return false;
            }
            if (!StatusFlags.Equals(StatusFlags))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_TARGET_INFO/Equals: The StatusFlags values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (AdapterId, Id, ModeInfoIdx, OutputTechnology, Rotation, Scaling, RefreshRate, ScanLineOrdering, TargetAvailable, StatusFlags).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_PATH_TARGET_INFO lhs, DISPLAYCONFIG_PATH_TARGET_INFO rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_PATH_TARGET_INFO lhs, DISPLAYCONFIG_PATH_TARGET_INFO rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_PATH_INFO : IEquatable<DISPLAYCONFIG_PATH_INFO>
    {
        public DISPLAYCONFIG_PATH_SOURCE_INFO SourceInfo;
        public DISPLAYCONFIG_PATH_TARGET_INFO TargetInfo;
        public DISPLAYCONFIG_PATH_FLAGS Flags;

        public DISPLAYCONFIG_PATH_INFO()
        {
            SourceInfo = new DISPLAYCONFIG_PATH_SOURCE_INFO();
            TargetInfo = new DISPLAYCONFIG_PATH_TARGET_INFO();
            Flags = DISPLAYCONFIG_PATH_FLAGS.Zero;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_PATH_INFO other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_PATH_INFO other)
        {
            if (!SourceInfo.Equals(other.SourceInfo))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_INFO/Equals: The SourceInfo values don't equal each other");
                return false;
            }
            if (!TargetInfo.Equals(other.TargetInfo))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_INFO/Equals: The TargetInfo values don't equal each other");
                return false;
            }
            if (!Flags.Equals(other.Flags))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_PATH_INFO/Equals: The Flags values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (SourceInfo, TargetInfo, Flags).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_PATH_INFO lhs, DISPLAYCONFIG_PATH_INFO rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_PATH_INFO lhs, DISPLAYCONFIG_PATH_INFO rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct DISPLAYCONFIG_MODE_INFO : IEquatable<DISPLAYCONFIG_MODE_INFO>
    {
        [FieldOffset((0))]
        public DISPLAYCONFIG_MODE_INFO_TYPE InfoType;

        [FieldOffset(4)]
        public uint Id;

        [FieldOffset(8)]
        public LUID AdapterId;

        // These 3 fields are all a C union in wingdi.dll
        [FieldOffset(16)]
        public DISPLAYCONFIG_TARGET_MODE TargetMode;

        [FieldOffset(16)]
        public DISPLAYCONFIG_SOURCE_MODE SourceMode;

        [FieldOffset(16)]
        public DISPLAYCONFIG_DESKTOP_IMAGE_INFO DesktopImageInfo;

        public DISPLAYCONFIG_MODE_INFO()
        {
            InfoType = DISPLAYCONFIG_MODE_INFO_TYPE.Zero;
            Id = 0;
            AdapterId = new LUID();
            TargetMode = new DISPLAYCONFIG_TARGET_MODE();
            SourceMode = new DISPLAYCONFIG_SOURCE_MODE();
            DesktopImageInfo = new DISPLAYCONFIG_DESKTOP_IMAGE_INFO();
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_MODE_INFO other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_MODE_INFO other)
        {
            if (InfoType != other.InfoType)
                return false;

            // This happens when it is a target mode info block
            if (InfoType == DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_TARGET &&
                Id == other.Id && // Disabling this check as as the Display ID it maps to will change after a switch from clone to non-clone profile, ruining the equality match
                TargetMode.Equals(other.TargetMode))
                return true;

            // This happens when it is a source mode info block
            if (InfoType == DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE &&
                //Id == other.Id && // Disabling this check as as the Display ID it maps to will change after a switch from surround to non-surround profile, ruining the equality match
                // Only seems to be a problem with the DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE options weirdly enough!
                SourceMode.Equals(other.SourceMode))
                return true;

            // This happens when it is a desktop image mode info block
            if (InfoType == DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_DESKTOP_IMAGE &&
                Id == other.Id &&  // Disabling this check as as the Display ID it maps to will change after a switch from clone to non-clone profile, ruining the equality match
                DesktopImageInfo.Equals(other.DesktopImageInfo))
                return true;

            // This happens when it is a clone - there is an extra entry with all zeros in it!
            if (InfoType == DISPLAYCONFIG_MODE_INFO_TYPE.Zero &&
                //Id == other.Id && // Disabling this check as as the Display ID it maps to will change after a switch from clone to non-clone profile, ruining the equality match
                DesktopImageInfo.Equals(other.DesktopImageInfo) &&
                TargetMode.Equals(other.TargetMode) &&
                SourceMode.Equals(other.SourceMode))
                return true;

            SharedLogger.logger.Trace($"DISPLAYCONFIG_MODE_INFO/Equals: The Flags values don't equal each other");
            return false;
        }


        public override int GetHashCode()
        {
            if (InfoType == DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_TARGET)
                return (InfoType, Id, TargetMode).GetHashCode();
            //return (InfoType, TargetMode).GetHashCode();

            if (InfoType == DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
                //return (InfoType, Id, SourceMode).GetHashCode();
                return (InfoType, SourceMode).GetHashCode();


            if (InfoType == DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_DESKTOP_IMAGE)
                return (InfoType, Id, DesktopImageInfo).GetHashCode();
            //return (InfoType, DesktopImageInfo).GetHashCode();

            // otherwise we return everything
            return (InfoType, Id, TargetMode, SourceMode, DesktopImageInfo).GetHashCode();
            //return (InfoType, TargetMode, SourceMode, DesktopImageInfo).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_MODE_INFO lhs, DISPLAYCONFIG_MODE_INFO rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_MODE_INFO lhs, DISPLAYCONFIG_MODE_INFO rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DISPLAYCONFIG_SOURCE_DEVICE_NAME : IEquatable<DISPLAYCONFIG_SOURCE_DEVICE_NAME>
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string ViewGdiDeviceName;

        public DISPLAYCONFIG_SOURCE_DEVICE_NAME()
        {
            Header = new DISPLAYCONFIG_DEVICE_INFO_HEADER();
            ViewGdiDeviceName = string.Empty;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_SOURCE_DEVICE_NAME other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_SOURCE_DEVICE_NAME other)
        {
            if(!Header.Equals(other.Header))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SOURCE_DEVICE_NAME/Equals: The Header values don't equal each other");
                return false;
            }
            if(ViewGdiDeviceName != other.ViewGdiDeviceName)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SOURCE_DEVICE_NAME/Equals: The ViewGdiDeviceName values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Header, ViewGdiDeviceName).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_SOURCE_DEVICE_NAME lhs, DISPLAYCONFIG_SOURCE_DEVICE_NAME rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_SOURCE_DEVICE_NAME lhs, DISPLAYCONFIG_SOURCE_DEVICE_NAME rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS : IEquatable<DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS>
    {
        public uint Value;

        public DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS()
        {
            Value = 0;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS other)
        {
            if(Value != other.Value)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS/Equals: The Value values don't equal each other");
                return false;
            }
            return true;
        }
            

        public bool FriendlyNameFromEdid => (Value & 0x1) == 0x1; // Might be this broken?
        public bool FriendlyNameForced => (Value & 0x2) == 0x2;
        public bool EdidIdsValid => (Value & 0x4) == 0x4;

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS lhs, DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS lhs, DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DISPLAYCONFIG_TARGET_DEVICE_NAME : IEquatable<DISPLAYCONFIG_TARGET_DEVICE_NAME>
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        public DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS Flags;
        public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY OutputTechnology;
        public ushort EdidManufactureId;
        public ushort EdidProductCodeId;
        public uint ConnectorInstance;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string MonitorFriendlyDeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string MonitorDevicePath;

        public DISPLAYCONFIG_TARGET_DEVICE_NAME()
        {
            Header = new DISPLAYCONFIG_DEVICE_INFO_HEADER();
            Flags = new DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS();
            OutputTechnology = DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15;
            EdidManufactureId = 0;
            EdidProductCodeId = 0;
            ConnectorInstance = 0;
            MonitorFriendlyDeviceName = string.Empty;
            MonitorDevicePath = string.Empty;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_TARGET_DEVICE_NAME other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_TARGET_DEVICE_NAME other)
        {
            if(!Header.Equals(other.Header))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_DEVICE_NAME/Equals: The Header values don't equal each other");
                return false;
            }
            if (!Flags.Equals(other.Flags))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_DEVICE_NAME/Equals: The Flags values don't equal each other");
                return false;
            }
            if(!OutputTechnology.Equals(other.OutputTechnology)) 
            { 
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_DEVICE_NAME/Equals: The OutputTechnology values don't equal each other"); 
                return false; 
            }
            if(EdidManufactureId != other.EdidManufactureId)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_DEVICE_NAME/Equals: The EdidManufactureId values don't equal each other");
                return false;
            }
            if(EdidProductCodeId != other.EdidProductCodeId)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_DEVICE_NAME/Equals: The EdidProductCodeId values don't equal each other");
                return false;
            }
            if(ConnectorInstance != other.ConnectorInstance)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_DEVICE_NAME/Equals: The ConnectorInstance values don't equal each other");
                return false;
            }
            if(MonitorFriendlyDeviceName != other.MonitorFriendlyDeviceName)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_DEVICE_NAME/Equals: The MonitorFriendlyDeviceName values don't equal each other");
                return false;
            }
            if(MonitorDevicePath != other.MonitorDevicePath)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_DEVICE_NAME/Equals: The MonitorDevicePath values don't equal each other");
                return false;
            }
            return true;
        }               

        public override int GetHashCode()
        {
            return (Header, Flags, OutputTechnology, EdidManufactureId, EdidProductCodeId, ConnectorInstance, MonitorFriendlyDeviceName, MonitorDevicePath).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_TARGET_DEVICE_NAME lhs, DISPLAYCONFIG_TARGET_DEVICE_NAME rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_TARGET_DEVICE_NAME lhs, DISPLAYCONFIG_TARGET_DEVICE_NAME rhs) => !(lhs == rhs);
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_TARGET_PREFERRED_MODE : IEquatable<DISPLAYCONFIG_TARGET_PREFERRED_MODE>
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        public uint Width;
        public uint Height;
        public DISPLAYCONFIG_TARGET_MODE TargetMode;

        public DISPLAYCONFIG_TARGET_PREFERRED_MODE()
        {
            Header = new DISPLAYCONFIG_DEVICE_INFO_HEADER();
            Width = 0;
            Height = 0;
            TargetMode = new DISPLAYCONFIG_TARGET_MODE();
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_TARGET_PREFERRED_MODE other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_TARGET_PREFERRED_MODE other)
        {
            if (!Header.Equals(other.Header))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_PREFERRED_MODE/Equals: The Header values don't equal each other");
                return false;
            }
            if (Width != other.Width)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_PREFERRED_MODE/Equals: The Width values don't equal each other");
                return false;
            }
            if (Height != other.Height)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_PREFERRED_MODE/Equals: The Height values don't equal each other");
                return false;
            }
            if (!TargetMode.Equals(other.TargetMode))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_PREFERRED_MODE/Equals: The TargetMode values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Header, Width, Height, TargetMode).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_TARGET_PREFERRED_MODE lhs, DISPLAYCONFIG_TARGET_PREFERRED_MODE rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_TARGET_PREFERRED_MODE lhs, DISPLAYCONFIG_TARGET_PREFERRED_MODE rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DISPLAYCONFIG_ADAPTER_NAME : IEquatable<DISPLAYCONFIG_ADAPTER_NAME>
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string AdapterDevicePath;

        public DISPLAYCONFIG_ADAPTER_NAME()
        {
            Header = new DISPLAYCONFIG_DEVICE_INFO_HEADER();
            AdapterDevicePath = string.Empty;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_ADAPTER_NAME other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_ADAPTER_NAME other)
        {
            if (!Header.Equals(other.Header))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_ADAPTER_NAME/Equals: The Header values don't equal each other");
                return false;
            }
            if (AdapterDevicePath != other.AdapterDevicePath)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_ADAPTER_NAME/Equals: The AdapterDevicePath values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Header, AdapterDevicePath).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_ADAPTER_NAME lhs, DISPLAYCONFIG_ADAPTER_NAME rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_ADAPTER_NAME lhs, DISPLAYCONFIG_ADAPTER_NAME rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION : IEquatable<DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION>
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        public uint Value;

        public DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION()
        {
            Header = new DISPLAYCONFIG_DEVICE_INFO_HEADER();
            Value = 0;
        }

        public bool IsMonitorVirtualResolutionDisabled
        {
            get => (Value & 0x1) == 0x1;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION other)
        {
            if (!Header.Equals(other.Header))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION/Equals: The Header values don't equal each other");
                return false;
            }
            if (Value != other.Value)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION/Equals: The Value values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Header, Value).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION lhs, DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION lhs, DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION rhs) => !(lhs == rhs);

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_SET_TARGET_PERSISTENCE : IEquatable<DISPLAYCONFIG_SET_TARGET_PERSISTENCE>
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        public uint Value;

        public DISPLAYCONFIG_SET_TARGET_PERSISTENCE()
        {
            Header = new DISPLAYCONFIG_DEVICE_INFO_HEADER();
            Value = 0;
        }

        public bool IsBootPersistenceOn
        {
            get => (Value & 0x1) == 0x1;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_SET_TARGET_PERSISTENCE other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_SET_TARGET_PERSISTENCE other)
        {
            if (!Header.Equals(other.Header))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SET_TARGET_PERSISTENCE/Equals: The Header values don't equal each other");
                return false;
            }
            if (Value != other.Value)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SET_TARGET_PERSISTENCE/Equals: The Value values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Header, Value).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_SET_TARGET_PERSISTENCE lhs, DISPLAYCONFIG_SET_TARGET_PERSISTENCE rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_SET_TARGET_PERSISTENCE lhs, DISPLAYCONFIG_SET_TARGET_PERSISTENCE rhs) => !(lhs == rhs);

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_TARGET_BASE_TYPE : IEquatable<DISPLAYCONFIG_TARGET_BASE_TYPE>
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        //[MarshalAs(UnmanagedType.U4)]
        public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY BaseOutputTechnology;

        public DISPLAYCONFIG_TARGET_BASE_TYPE()
        {
            Header = new DISPLAYCONFIG_DEVICE_INFO_HEADER();
            BaseOutputTechnology = DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_TARGET_BASE_TYPE other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_TARGET_BASE_TYPE other)
        {
            if (!Header.Equals(other.Header))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_BASE_TYPE/Equals: The Header values don't equal each other");
                return false;
            }
            if (BaseOutputTechnology != other.BaseOutputTechnology)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_TARGET_BASE_TYPE/Equals: The BaseOutputTechnology values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Header, BaseOutputTechnology).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_TARGET_BASE_TYPE lhs, DISPLAYCONFIG_TARGET_BASE_TYPE rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_TARGET_BASE_TYPE lhs, DISPLAYCONFIG_TARGET_BASE_TYPE rhs) => !(lhs == rhs);
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE : IEquatable<DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE>
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        public uint Value;

        public DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE()
        {
            Header = new DISPLAYCONFIG_DEVICE_INFO_HEADER();
            Value = 0;
        }

        public bool EnableAdvancedColor
        {
            get => (Value & 0x1) == 0x1;
            set
            {
                if (value)
                {
                    Value = 0x1;
                }
                else
                {
                    Value = 0x0;
                }
            }
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE other)
        {
            if (!Header.Equals(other.Header))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE/Equals: The Header values don't equal each other");
                return false;
            }
            if (Value != other.Value)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE/Equals: The Value values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Header, Value).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE lhs, DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE lhs, DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAYCONFIG_SDR_WHITE_LEVEL : IEquatable<DISPLAYCONFIG_SDR_WHITE_LEVEL>
    {
        public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;
        // SDRWhiteLevel represents a multiplier for standard SDR white
        // peak value i.e. 80 nits represented as fixed point.
        // To get value in nits use the following conversion
        // SDRWhiteLevel in nits = (SDRWhiteLevel / 1000 ) * 80
        // NOTE! Weirdly this is supposed to be a ulong, but there is an error in Win10 64-bit
        // where it actually returns a uint! So had to engineer in a bug :(
        public uint SDRWhiteLevel;

        public DISPLAYCONFIG_SDR_WHITE_LEVEL()
        {
            Header = new DISPLAYCONFIG_DEVICE_INFO_HEADER();
            SDRWhiteLevel = 0;
        }

        public override bool Equals(object obj) => obj is DISPLAYCONFIG_SDR_WHITE_LEVEL other && this.Equals(other);
        public bool Equals(DISPLAYCONFIG_SDR_WHITE_LEVEL other)
        {
            if (!Header.Equals(other.Header))
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SDR_WHITE_LEVEL/Equals: The Header values don't equal each other");
                return false;
            }
            if (SDRWhiteLevel != other.SDRWhiteLevel)
            {
                SharedLogger.logger.Trace($"DISPLAYCONFIG_SDR_WHITE_LEVEL/Equals: The SDRWhiteLevel values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Header, SDRWhiteLevel).GetHashCode();
        }

        public static bool operator ==(DISPLAYCONFIG_SDR_WHITE_LEVEL lhs, DISPLAYCONFIG_SDR_WHITE_LEVEL rhs) => lhs.Equals(rhs);

        public static bool operator !=(DISPLAYCONFIG_SDR_WHITE_LEVEL lhs, DISPLAYCONFIG_SDR_WHITE_LEVEL rhs) => !(lhs == rhs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECTL : IEquatable<RECTL>
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECTL()
        {
            Left = 0;
            Top = 0;
            Right = 0;
            Bottom = 0;
        }

        public RECTL(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public static RECTL FromXYWH(int x, int y, int width, int height)
        {
            return new RECTL(x, y, x + width, y + height);
        }

        public override bool Equals(object obj) => obj is RECTL other && this.Equals(other);
        public bool Equals(RECTL other)
        {
            if (Left != other.Left)
            {
                SharedLogger.logger.Trace($"RECTL/Equals: The Left values don't equal each other");
                return false;
            }
            if (Top != other.Top)
            {
                SharedLogger.logger.Trace($"RECTL/Equals: The Top values don't equal each other");
                return false;
            }
            if (Right != other.Right)
            {
                SharedLogger.logger.Trace($"RECTL/Equals: The Right values don't equal each other");
                return false;
            }
            if (Bottom != other.Bottom)
            {
                SharedLogger.logger.Trace($"RECTL/Equals: The Bottom values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (Left, Top, Right, Bottom).GetHashCode();
        }

        public static bool operator ==(RECTL lhs, RECTL rhs) => lhs.Equals(rhs);

        public static bool operator !=(RECTL lhs, RECTL rhs) => !(lhs == rhs);
    }


    class CCDImport
    {
        // Set some useful constants
        public const SDC SDC_CCD_TEST_IF_VALID = (SDC.SDC_VALIDATE | SDC.SDC_USE_SUPPLIED_DISPLAY_CONFIG);
        public const uint DISPLAYCONFIG_PATH_MODE_IDX_INVALID = 0xffffffff;
        public static readonly UInt32[] DPI_VALUE_LIST = { 100, 125, 150, 175, 200, 225, 250, 300, 350, 400, 450, 500 };


        // GetDisplayConfigBufferSizes
        [DllImport("user32")]
        public static extern WIN32STATUS GetDisplayConfigBufferSizes(QDC flags, out int numPathArrayElements, out int numModeInfoArrayElements);

        // QueryDisplayConfig
        [DllImport("user32")]
        public static extern WIN32STATUS QueryDisplayConfig(QDC flags, ref int numPathArrayElements, [In, Out] DISPLAYCONFIG_PATH_INFO[] pathArray, ref int numModeInfoArrayElements, [In, Out] DISPLAYCONFIG_MODE_INFO[] modeInfoArray, out DISPLAYCONFIG_TOPOLOGY_ID currentTopologyId);

        [DllImport("user32")]
        public static extern WIN32STATUS QueryDisplayConfig(QDC flags, ref int numPathArrayElements, [In, Out] DISPLAYCONFIG_PATH_INFO[] pathArray, ref int numModeInfoArrayElements, [In, Out] DISPLAYCONFIG_MODE_INFO[] modeInfoArray, IntPtr currentTopologyId);

        // DisplayConfigGetDeviceInfo
        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_SOURCE_DEVICE_NAME requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_TARGET_DEVICE_NAME requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_TARGET_PREFERRED_MODE requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_ADAPTER_NAME requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_SET_TARGET_PERSISTENCE requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_TARGET_BASE_TYPE requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION requestPacket);

        /*[DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_SET_SUPPORT_VIRTUAL_RESOLUTION requestPacket);
*/
        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_SDR_WHITE_LEVEL requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_SOURCE_DPI_SCALE_GET requestPacket);


        // DisplayConfigSetDeviceInfo
        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigSetDeviceInfo(ref DISPLAYCONFIG_SET_TARGET_PERSISTENCE requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigSetDeviceInfo(ref DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE requestPacket);

        [DllImport("user32")]
        public static extern WIN32STATUS DisplayConfigSetDeviceInfo(ref DISPLAYCONFIG_SOURCE_DPI_SCALE_SET requestPacket);


        // Have disabled the DisplayConfigSetDeviceInfo options except for SET_TARGET_PERSISTENCE, as per the note
        // from https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-displayconfigsetdeviceinfo
        // "DisplayConfigSetDeviceInfo can currently only be used to start and stop boot persisted force projection on an analog target."
        /*[DllImport("user32")]
        public static extern int DisplayConfigSetDeviceInfo( ref DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION targetSupportVirtualResolution );*/

        // SetDisplayConfig
        [DllImport("user32")]
        public static extern WIN32STATUS SetDisplayConfig([In] uint numPathArrayElements, [In, Optional] DISPLAYCONFIG_PATH_INFO[] pathArray, [In] uint numModeInfoArrayElements, [In, Optional] DISPLAYCONFIG_MODE_INFO[] modeInfoArray, [In] SDC flags);

        [DllImport("user32")]
        public static extern WIN32STATUS SetDisplayConfig([In] uint numPathArrayElements, [In,Optional] IntPtr pathArray, [In] uint numModeInfoArrayElements, [In,Optional] IntPtr modeInfoArray, [In] SDC flags);

        [DllImport("user32")]
        public static extern bool SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT value);



    }
}