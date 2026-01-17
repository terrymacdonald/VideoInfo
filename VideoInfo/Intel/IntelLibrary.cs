using DisplayMagicianShared;
using DisplayMagicianShared.Windows;
using IGCLWrapper;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Windows.Graphics.Display;

namespace DisplayMagicianShared.Intel
{
    #region Data Structures

    // [StructLayout(LayoutKind.Sequential)]
    // public struct INTEL_DISPLAY : IEquatable<INTEL_DISPLAY>
    // {
    //     public string Name;
    //     public string DeviceID;
    //     public uint DisplayIndex;
    //     public uint AdapterIndex;

    //     public INTEL_DISPLAY()
    //     {
    //         Name = "";
    //         DeviceID = "";
    //         DisplayIndex = 0;
    //         AdapterIndex = 0;
    //     }

    //     public override bool Equals(object obj) => obj is INTEL_DISPLAY other && Equals(other);
        
    //     public bool Equals(INTEL_DISPLAY other)
    //     {
    //         if (Name != other.Name)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_DISPLAY/Equals: The Name values don't equal each other");
    //             return false;
    //         }
    //         if (DeviceID != other.DeviceID)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_DISPLAY/Equals: The DeviceID values don't equal each other");
    //             return false;
    //         }
    //         if (DisplayIndex != other.DisplayIndex)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_DISPLAY/Equals: The DisplayIndex values don't equal each other");
    //             return false;
    //         }
    //         if (AdapterIndex != other.AdapterIndex)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_DISPLAY/Equals: The AdapterIndex values don't equal each other");
    //             return false;
    //         }
    //         return true;
    //     }

    //     public override int GetHashCode()
    //     {
    //         return (Name, DeviceID, DisplayIndex, AdapterIndex).GetHashCode();
    //     }

    //     public static bool operator ==(INTEL_DISPLAY lhs, INTEL_DISPLAY rhs) => lhs.Equals(rhs);
    //     public static bool operator !=(INTEL_DISPLAY lhs, INTEL_DISPLAY rhs) => !(lhs == rhs);
    // }

    [StructLayout(LayoutKind.Sequential)]
    public struct INTEL_DISPLAY_WITH_SETTINGS : IEquatable<INTEL_DISPLAY_WITH_SETTINGS>
    {
        public string Name;
        public string DisplayDeviceID;
        public uint DisplayIndex;
        public uint AdapterIndex;
        
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

        // Display-related DTOs
        public DisplaySettingsDto DisplaySettings;
        public ScalingSettingsDto ScalingSettings;
        public SharpnessSettingsDto SharpnessSettings;
        public RetroScalingSettingsDto RetroScalingSettings;
        public DceArgsDto DynamicContrastEnhancement;
        public uint[] DynamicContrastEnhancementHistogram;
        public PowerOptimizationSettingsDto PowerOptimizationSettings;
        public LaceConfigDto LaceConfig;
        public SwPsrSettingsDto SoftwarePsrSettings;
        public GenlockArgsDto GenlockArgs;
        public IntelArcSyncMonitorParamsDto IntelArcSyncMonitorParams;
        public AdapterDisplayEncoderPropertiesDto AdapterDisplayEncoderProperties;

        // Display getter outputs
        public ctl_display_properties_t DisplayProperties;
        //public ctl_device_adapter_properties_t DeviceProperties;
        public string DeviceID;
        public ctl_display_timing_t DisplayTiming;
        public ctl_get_set_wire_format_config_t WireFormat;
        public ctl_get_brightness_t Brightness;
        public ctl_scaling_caps_t ScalingCaps;
        public ctl_sharpness_caps_t SharpnessCaps;
        public ctl_sharpness_filter_properties_t[] SharpnessFilterProperties;
        public ctl_retro_scaling_caps_t RetroScalingCaps;
        public ctl_power_optimization_caps_t PowerOptimizationCaps;
        public ctl_intel_arc_sync_profile_params_t IntelArcSyncProfile;
        public ctl_get_set_custom_mode_args_t CustomModeArgs;
        public ctl_custom_src_mode_t[] CustomModes;
        public ctl_lda_args_t LinkedDisplayAdaptersArgs;
        public IntPtr[] LinkedDisplayAdapters;
        public ctl_mux_properties_t MuxProperties;
        public IntPtr[] MuxDisplayOutputs;
        public ctl_vblank_ts_args_t VblankTimestamp;
        //public IntPtr ZeDeviceHandle;
        //public IntPtr ZeDriverHandle;
        public double RefreshRateHz;
        public uint ResolutionWidth;
        public uint ResolutionHeight;
        public bool IsActive;

        public INTEL_DISPLAY_WITH_SETTINGS()
        {
            Name = "";
            DisplayDeviceID = "";
            DisplayIndex = 0;
            AdapterIndex = 0;
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

            DisplaySettings = new DisplaySettingsDto();
            ScalingSettings = new ScalingSettingsDto();
            SharpnessSettings = new SharpnessSettingsDto();
            RetroScalingSettings = new RetroScalingSettingsDto();
            DynamicContrastEnhancement = new DceArgsDto();
            DynamicContrastEnhancementHistogram = Array.Empty<uint>();
            PowerOptimizationSettings = new PowerOptimizationSettingsDto();
            LaceConfig = new LaceConfigDto();
            SoftwarePsrSettings = new SwPsrSettingsDto();
            GenlockArgs = new GenlockArgsDto();
            IntelArcSyncMonitorParams = new IntelArcSyncMonitorParamsDto();
            AdapterDisplayEncoderProperties = new AdapterDisplayEncoderPropertiesDto();

            DisplayProperties = new ctl_display_properties_t();
            //DeviceProperties = new ctl_device_adapter_properties_t();
            DeviceID = "";
            DisplayTiming = new ctl_display_timing_t();
            WireFormat = new ctl_get_set_wire_format_config_t();
            Brightness = new ctl_get_brightness_t();
            ScalingCaps = new ctl_scaling_caps_t();
            SharpnessCaps = new ctl_sharpness_caps_t();
            SharpnessFilterProperties = Array.Empty<ctl_sharpness_filter_properties_t>();
            RetroScalingCaps = new ctl_retro_scaling_caps_t();
            PowerOptimizationCaps = new ctl_power_optimization_caps_t();
            IntelArcSyncProfile = new ctl_intel_arc_sync_profile_params_t();
            CustomModeArgs = new ctl_get_set_custom_mode_args_t();
            CustomModes = Array.Empty<ctl_custom_src_mode_t>();
            LinkedDisplayAdaptersArgs = new ctl_lda_args_t();
            LinkedDisplayAdapters = Array.Empty<IntPtr>();
            MuxProperties = new ctl_mux_properties_t();
            MuxDisplayOutputs = Array.Empty<IntPtr>();
            VblankTimestamp = new ctl_vblank_ts_args_t();
            //ZeDeviceHandle = IntPtr.Zero;
            //ZeDriverHandle = IntPtr.Zero;
            RefreshRateHz = 0.0;
            ResolutionWidth = 0;
            ResolutionHeight = 0;
            IsActive = false;
        }

        public override bool Equals(object obj) => obj is INTEL_DISPLAY_WITH_SETTINGS other && Equals(other);
        
        public bool Equals(INTEL_DISPLAY_WITH_SETTINGS other)
        {
            if (Name != other.Name)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The Name values don't equal each other");
                return false;
            }
            if (DisplayDeviceID != other.DisplayDeviceID)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The DisplayDeviceID values don't equal each other");
                return false;
            }
            if (DisplayIndex != other.DisplayIndex)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The DisplayIndex values don't equal each other");
                return false;
            }
            if (AdapterIndex != other.AdapterIndex)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The AdapterIndex values don't equal each other");
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
            if (!EqualityComparer<DisplaySettingsDto>.Default.Equals(DisplaySettings, other.DisplaySettings))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The DisplaySettings values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ScalingSettingsDto>.Default.Equals(ScalingSettings, other.ScalingSettings))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The ScalingSettings values don't equal each other");
                return false;
            }
            if (!EqualityComparer<SharpnessSettingsDto>.Default.Equals(SharpnessSettings, other.SharpnessSettings))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The SharpnessSettings values don't equal each other");
                return false;
            }
            if (!EqualityComparer<RetroScalingSettingsDto>.Default.Equals(RetroScalingSettings, other.RetroScalingSettings))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The RetroScalingSettings values don't equal each other");
                return false;
            }
            if (!EqualityComparer<DceArgsDto>.Default.Equals(DynamicContrastEnhancement, other.DynamicContrastEnhancement))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The DynamicContrastEnhancement values don't equal each other");
                return false;
            }
            if (!DynamicContrastEnhancementHistogram.SequenceEqual(other.DynamicContrastEnhancementHistogram))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The DynamicContrastEnhancementHistogram values don't equal each other");
                return false;
            }
            if (!EqualityComparer<PowerOptimizationSettingsDto>.Default.Equals(PowerOptimizationSettings, other.PowerOptimizationSettings))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The PowerOptimizationSettings values don't equal each other");
                return false;
            }
            if (!EqualityComparer<LaceConfigDto>.Default.Equals(LaceConfig, other.LaceConfig))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The LaceConfig values don't equal each other");
                return false;
            }
            if (!EqualityComparer<SwPsrSettingsDto>.Default.Equals(SoftwarePsrSettings, other.SoftwarePsrSettings))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The SoftwarePsrSettings values don't equal each other");
                return false;
            }
            if (!EqualityComparer<GenlockArgsDto>.Default.Equals(GenlockArgs, other.GenlockArgs))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The GenlockArgs values don't equal each other");
                return false;
            }
            if (!EqualityComparer<IntelArcSyncMonitorParamsDto>.Default.Equals(IntelArcSyncMonitorParams, other.IntelArcSyncMonitorParams))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IntelArcSyncMonitorParams values don't equal each other");
                return false;
            }
            if (!EqualityComparer<AdapterDisplayEncoderPropertiesDto>.Default.Equals(AdapterDisplayEncoderProperties, other.AdapterDisplayEncoderProperties))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The AdapterDisplayEncoderProperties values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_display_properties_t>.Default.Equals(DisplayProperties, other.DisplayProperties))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The DisplayProperties values don't equal each other");
                return false;
            }
            // if (!EqualityComparer<ctl_device_adapter_properties_t>.Default.Equals(DeviceProperties, other.DeviceProperties))
            // {
            //     SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The DeviceProperties values don't equal each other");
            //     return false;
            // }
            if (DeviceID != other.DeviceID)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The DeviceID values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_display_timing_t>.Default.Equals(DisplayTiming, other.DisplayTiming))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The DisplayTiming values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_get_set_wire_format_config_t>.Default.Equals(WireFormat, other.WireFormat))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The WireFormat values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_get_brightness_t>.Default.Equals(Brightness, other.Brightness))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The Brightness values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_scaling_caps_t>.Default.Equals(ScalingCaps, other.ScalingCaps))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The ScalingCaps values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_sharpness_caps_t>.Default.Equals(SharpnessCaps, other.SharpnessCaps))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The SharpnessCaps values don't equal each other");
                return false;
            }
            if (!SharpnessFilterProperties.SequenceEqual(other.SharpnessFilterProperties))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The SharpnessFilterProperties values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_retro_scaling_caps_t>.Default.Equals(RetroScalingCaps, other.RetroScalingCaps))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The RetroScalingCaps values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_power_optimization_caps_t>.Default.Equals(PowerOptimizationCaps, other.PowerOptimizationCaps))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The PowerOptimizationCaps values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_intel_arc_sync_profile_params_t>.Default.Equals(IntelArcSyncProfile, other.IntelArcSyncProfile))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IntelArcSyncProfile values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_get_set_custom_mode_args_t>.Default.Equals(CustomModeArgs, other.CustomModeArgs))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The CustomModeArgs values don't equal each other");
                return false;
            }
            if (!CustomModes.SequenceEqual(other.CustomModes))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The CustomModes values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_lda_args_t>.Default.Equals(LinkedDisplayAdaptersArgs, other.LinkedDisplayAdaptersArgs))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The LinkedDisplayAdaptersArgs values don't equal each other");
                return false;
            }
            if (!LinkedDisplayAdapters.SequenceEqual(other.LinkedDisplayAdapters))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The LinkedDisplayAdapters values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_mux_properties_t>.Default.Equals(MuxProperties, other.MuxProperties))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The MuxProperties values don't equal each other");
                return false;
            }
            if (!MuxDisplayOutputs.SequenceEqual(other.MuxDisplayOutputs))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The MuxDisplayOutputs values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_vblank_ts_args_t>.Default.Equals(VblankTimestamp, other.VblankTimestamp))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The VblankTimestamp values don't equal each other");
                return false;
            }
            // if (ZeDeviceHandle != other.ZeDeviceHandle)
            // {
            //     SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The ZeDeviceHandle values don't equal each other");
            //     return false;
            // }
            // if (ZeDriverHandle != other.ZeDriverHandle)
            // {
            //     SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The ZeDriverHandle values don't equal each other");
            //     return false;
            // }
            if (Math.Abs(RefreshRateHz - other.RefreshRateHz) > 0.001)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The RefreshRateHz values don't equal each other");
                return false;
            }
            if (ResolutionWidth != other.ResolutionWidth)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The ResolutionWidth values don't equal each other");
                return false;
            }
            if (ResolutionHeight != other.ResolutionHeight)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The ResolutionHeight values don't equal each other");
                return false;
            }
            if (IsActive != other.IsActive)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsActive values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {

            return (Name, DisplayDeviceID, DisplayIndex, AdapterIndex, IsSupportedIntegerScaling, IsEnabledIntegerScaling, IntegerScalingType, IsSupportedGPUScaling, IsEnabledGPUScaling, ScalingType, IsSupportedImageSharpening, IsEnabledImageSharpening, SharpeningFilterType, SharpeningIntensity, 
                DisplaySettings, ScalingSettings, SharpnessSettings, RetroScalingSettings, DynamicContrastEnhancement, DynamicContrastEnhancementHistogram, PowerOptimizationSettings, LaceConfig, SoftwarePsrSettings, GenlockArgs, IntelArcSyncMonitorParams, AdapterDisplayEncoderProperties, 
                DisplayProperties, /*DeviceProperties,*/ DeviceID, DisplayTiming, WireFormat, Brightness, ScalingCaps, SharpnessCaps, SharpnessFilterProperties, RetroScalingCaps, PowerOptimizationCaps, IntelArcSyncProfile, CustomModeArgs, CustomModes, LinkedDisplayAdaptersArgs, LinkedDisplayAdapters, MuxProperties, MuxDisplayOutputs, 
                VblankTimestamp, /*ZeDeviceHandle, ZeDriverHandle,*/ RefreshRateHz, ResolutionWidth, ResolutionHeight, IsActive).GetHashCode();
        }

        public static bool operator ==(INTEL_DISPLAY_WITH_SETTINGS lhs, INTEL_DISPLAY_WITH_SETTINGS rhs) => lhs.Equals(rhs);
        public static bool operator !=(INTEL_DISPLAY_WITH_SETTINGS lhs, INTEL_DISPLAY_WITH_SETTINGS rhs) => !(lhs == rhs);
    }

    // [StructLayout(LayoutKind.Sequential)]
    
    // public struct INTEL_COMBINED_DISPLAY_CHILD
    // {
    //     public string DisplayId;      // stable ID: EDID hash, PnP ID, IGCL display UID
    //     public uint Width;
    //     public uint Height;
    //     public int OffsetX;
    //     public int OffsetY;
    //     public uint Rotation;

    //     public INTEL_COMBINED_DISPLAY_CHILD()
    //     {
    //         DisplayId = "";
    //         Width = 0;
    //         Height = 0;
    //         OffsetX = 0;
    //         OffsetY = 0;
    //         Rotation = 0;
    //     }

    //     public override bool Equals(object obj) => obj is INTEL_COMBINED_DISPLAY_CHILD other && Equals(other);
        
    //     public bool Equals(INTEL_COMBINED_DISPLAY_CHILD other)
    //     {   
    //         if (DisplayId != other.DisplayId)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY_CHILD/Equals: The DisplayId values don't equal each other");
    //             return false;
    //         }
    //         if (Width != other.Width)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY_CHILD/Equals: The Width values don't equal each other");
    //             return false;
    //         }
    //         if (Height != other.Height)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY_CHILD/Equals: The Height values don't equal each other");
    //             return false;
    //         }
    //         if (OffsetX != other.OffsetX)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY_CHILD/Equals: The OffsetX values don't equal each other");
    //             return false;
    //         }
    //         if (OffsetY != other.OffsetY)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY_CHILD/Equals: The OffsetY values don't equal each other");
    //             return false;
    //         }
    //         if (Rotation != other.Rotation)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY_CHILD/Equals: The Rotation values don't equal each other");
    //             return false;
    //         }
    //         return true;
    //     }

    //     public override int GetHashCode()
    //     {
    //         return (DisplayId, Width, Height, OffsetX, OffsetY, Rotation).GetHashCode();
    //     }

    //     public static bool operator ==(INTEL_COMBINED_DISPLAY_CHILD lhs, INTEL_COMBINED_DISPLAY_CHILD rhs) => lhs.Equals(rhs);
    //     public static bool operator !=(INTEL_COMBINED_DISPLAY_CHILD lhs, INTEL_COMBINED_DISPLAY_CHILD rhs) => !(lhs == rhs);

    // }

    // [StructLayout(LayoutKind.Sequential)]
    // public struct INTEL_COMBINED_DISPLAY : IEquatable<INTEL_COMBINED_DISPLAY>
    // {
    //     public bool IsCombinedDisplay;
    //     public uint NumOutputs;
    //     public uint CombinedDesktopWidth;
    //     public uint CombinedDesktopHeight;
    //     public List<INTEL_COMBINED_DISPLAY_CHILD> ChildDisplayHandles;  // Display handles that are part of the combined display

    //     public INTEL_COMBINED_DISPLAY()
    //     {
    //         IsCombinedDisplay = false;
    //         NumOutputs = 0;
    //         CombinedDesktopWidth = 0;
    //         CombinedDesktopHeight = 0;
    //         ChildDisplayHandles = new List<INTEL_COMBINED_DISPLAY_CHILD>();
    //     }

    //     public override bool Equals(object obj) => obj is INTEL_COMBINED_DISPLAY other && Equals(other);
        
    //     public bool Equals(INTEL_COMBINED_DISPLAY other)
    //     {
    //         if (IsCombinedDisplay != other.IsCombinedDisplay)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY/Equals: The IsCombinedDisplay values don't equal each other");
    //             return false;
    //         }
    //         if (NumOutputs != other.NumOutputs)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY/Equals: The NumOutputs values don't equal each other");
    //             return false;
    //         }
    //         if (CombinedDesktopWidth != other.CombinedDesktopWidth)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY/Equals: The CombinedDesktopWidth values don't equal each other");
    //             return false;
    //         }
    //         if (CombinedDesktopHeight != other.CombinedDesktopHeight)
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY/Equals: The CombinedDesktopHeight values don't equal each other");
    //             return false;
    //         }
    //         if (!ChildDisplayHandles.SequenceEqual(other.ChildDisplayHandles))
    //         {
    //             SharedLogger.logger.Trace($"INTEL_COMBINED_DISPLAY/Equals: The ChildDisplayHandles values don't equal each other");
    //             return false;
    //         }
    //         return true;
    //     }

    //     public override int GetHashCode()
    //     {
    //         return (IsCombinedDisplay, NumOutputs, CombinedDesktopWidth, CombinedDesktopHeight, ChildDisplayHandles).GetHashCode();
    //     }

    //     public static bool operator ==(INTEL_COMBINED_DISPLAY lhs, INTEL_COMBINED_DISPLAY rhs) => lhs.Equals(rhs);
    //     public static bool operator !=(INTEL_COMBINED_DISPLAY lhs, INTEL_COMBINED_DISPLAY rhs) => !(lhs == rhs);
    // }

    [StructLayout(LayoutKind.Sequential)]
    public struct INTEL_ADAPTER : IEquatable<INTEL_ADAPTER>
    {
        public string AdapterID;
        public string Name;
        public uint AdapterIndex;
        public ctl_device_adapter_properties_t AdapterProperties;
        public bool IsCombinedDisplay;
        //public INTEL_COMBINED_DISPLAY CombinedDisplay;
        public CombinedDisplayArgsDto CombinedDisplay;

        public INTEL_ADAPTER()
        {
            AdapterID = "";
            Name = "";
            AdapterIndex = 0;
            AdapterProperties = new ctl_device_adapter_properties_t();
            IsCombinedDisplay = false;
            //CombinedDisplay = new INTEL_COMBINED_DISPLAY();
            CombinedDisplay = new CombinedDisplayArgsDto();
            CombinedDisplay.ChildInfos = Array.Empty<CombinedDisplayChildInfoDto>();
            CombinedDisplay.IsSupported = false;
        }
        public override bool Equals(object obj) => obj is INTEL_ADAPTER other && Equals(other);
        
        public  bool Equals(INTEL_ADAPTER other)
        {
            if (AdapterID != other.AdapterID)
            {
                SharedLogger.logger.Trace($"INTEL_ADAPTER/Equals: The AdapterID values don't equal each other");
                return false;
            }
            if (Name != other.Name)
            {
                SharedLogger.logger.Trace($"INTEL_ADAPTER/Equals: The Name values don't equal each other");
                return false;
            }   
            if (AdapterIndex != other.AdapterIndex)
            {
                SharedLogger.logger.Trace($"INTEL_ADAPTER/Equals: The AdapterIndex values don't equal each other");
                return false;
            }
            if (!EqualityComparer<ctl_device_adapter_properties_t>.Default.Equals(AdapterProperties, other.AdapterProperties))
            {
                SharedLogger.logger.Trace($"INTEL_ADAPTER/Equals: The AdapterProperties values don't equal each other");
                return false;
            }
            if (IsCombinedDisplay != other.IsCombinedDisplay)
            {
                SharedLogger.logger.Trace($"INTEL_ADAPTER/Equals: The IsCombinedDisplay values don't equal each other");
                return false;
            }
            if (!CombinedDisplay.Equals(other.CombinedDisplay))
            {
                SharedLogger.logger.Trace($"INTEL_ADAPTER/Equals: The CombinedDisplay values don't equal each other");
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (AdapterID, Name, AdapterIndex, AdapterProperties, IsCombinedDisplay, CombinedDisplay).GetHashCode();
        }

        public static bool operator ==(INTEL_ADAPTER lhs, INTEL_ADAPTER rhs) => lhs.Equals(rhs);
        public static bool operator !=(INTEL_ADAPTER lhs, INTEL_ADAPTER rhs) => !(lhs == rhs);
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct INTEL_DISPLAY_CONFIG : IEquatable<INTEL_DISPLAY_CONFIG>
    {
        public bool IsInUse;
        public bool CombinedDisplayIsInUse;
        public Dictionary<string, INTEL_ADAPTER> PhysicalAdapters;  // Key is adapter ID
        public Dictionary<string, INTEL_DISPLAY_WITH_SETTINGS> Displays;  // Key is display ID
        public List<string> DisplayIdentifiers;

        public INTEL_DISPLAY_CONFIG()
        {
            IsInUse = false;
            CombinedDisplayIsInUse = false;
            PhysicalAdapters = new Dictionary<string, INTEL_ADAPTER>();
            Displays = new Dictionary<string, INTEL_DISPLAY_WITH_SETTINGS>();
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
            if (CombinedDisplayIsInUse != other.CombinedDisplayIsInUse)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The CombinedDisplayIsInUse values don't equal each other");
                return false;
            }            
            if (!PhysicalAdapters.SequenceEqual(other.PhysicalAdapters))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_CONFIG/Equals: The PhysicalAdapters values don't equal each other");
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
            return (IsInUse, CombinedDisplayIsInUse, PhysicalAdapters, Displays, DisplayIdentifiers).GetHashCode();
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
        private IGCLApiHelper _igclApiHelper;
        
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
            _allConnectedDisplayIdentifiers = new List<string>();
            
            try
            {
                _initialised = false;
                
                // Check if there is Intel hardware installed
                SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Looking for Intel PCI hardware...");
                if (!WinLibrary.IsPCIVideoCardVendorInstalled(PCIVendorIDs))
                {
                    SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: No Intel GPU hardware detected.");
                    return;
                }
                else
                {
                    SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Intel GPU hardware detected");
                }

                // Confirm the IGCL DLL is available before attempting to initialise
                if (!IGCLApiHelper.IsIGCLDllAvailable(out string dllError))
                {
                    _initialised = false;
                    SharedLogger.logger.Error($"IntelLibrary/IntelLibrary: Failed to load the Intel IGCL DLL. {dllError}");
                    return;
                }

                SharedLogger.logger.Trace("IntelLibrary/IntelLibrary: Intialising Intel IGCL facade helper");

                _igclApiHelper = IGCLApiHelper.Initialize();
                if (_igclApiHelper == null)
                {
                    _initialised = false;
                    SharedLogger.logger.Error("IntelLibrary/IntelLibrary: Failed to initialise Intel IGCL helper.");
                    return;
                }
                _initialised = true;
                SharedLogger.logger.Trace("IntelLibrary/IntelLibrary: Successfully initialised Intel IGCL helper.");
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

            SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Automatically getting the Intel Display Configuration");
            _activeDisplayConfig = GetActiveConfig();
            SharedLogger.logger.Trace($"IntelLibrary/IntelLibrary: Automatically getting the Intel Connected Display Identifiers");
            _allConnectedDisplayIdentifiers = GetAllConnectedDisplayIdentifiers(out bool failure);
        
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

            // Free unmanaged resources (unmanaged objects) and override finalizer.
            if (_igclApiHelper != null)
            {
                _igclApiHelper.Dispose();
                _igclApiHelper = null;
            }


            if (hIGCLModule != IntPtr.Zero)
            {
                SharedLogger.logger.Trace("IntelLibrary/Dispose: Freeing the Intel IGCL DLL");
                FreeLibrary(hIGCLModule);
                hIGCLModule = IntPtr.Zero;
            }

            _initialised = false;
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

        public static void Shutdown()
        {
            if (_instance == null)
            {
                return;
            }

            _instance.Dispose();
            _instance = null;
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

            if (_initialised && _igclApiHelper != null)
            {
                
                // Enumerate the Intel GPUs adapters in the sytem
                var adapters = _igclApiHelper.EnumerateAdapters();
                int adapterTotalCount = adapters.Count;
                int adapterNum = 0;

                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Found {adapterTotalCount} Intel GPU adapter(s)");


                // Go through each adapter
                foreach (var adapter in adapters)
                {
                    adapterNum++;
                    // Get adapter properties
                    var adapterProperties = adapter.GetProperties();
                    var adapterDeviceID = $"{adapter.Name}|{adapterProperties.pci_device_id.ToString("G")}|{adapterProperties.pci_subsys_id.ToString("G")}";

                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Processing Intel GPU adapter {adapterNum}({adapterProperties.name}), PCI Device ID: 0x{adapterProperties.pci_device_id:X4}, device type {adapterProperties.device_type} ({adapterNum}/{adapterTotalCount}");                    


                    // Create adapter storage struct
                    INTEL_ADAPTER newAdapter = new INTEL_ADAPTER();
                    newAdapter.AdapterID = adapterDeviceID;
                    newAdapter.Name = adapter.Name;
                    newAdapter.AdapterProperties = adapterProperties;

                    // Add adapter to config
                    myDisplayConfig.PhysicalAdapters.Add(adapterDeviceID, newAdapter);

                    //------------------------------------
                    // CHECK FOR COMBINED DISPLAY CONFIGURATION PER ADAPTER
                    //------------------------------------
                    

                    try
                    {
                        var combinedDisplay = adapter.GetCombinedDisplay();
                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got Combined Display settings for adapter {adapterNum}");

                        if (combinedDisplay.IsSupported)
                        {
                            if (combinedDisplay.NumOutputs > 1)
                            {
                                newAdapter.IsCombinedDisplay = true;
                                myDisplayConfig.CombinedDisplayIsInUse = true;
                                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Adapter {adapterNum} has a Combined Display with {combinedDisplay.NumOutputs} outputs, {combinedDisplay.CombinedDesktopWidth}x{combinedDisplay.CombinedDesktopHeight}");
                            }
                            else
                            {
                                newAdapter.IsCombinedDisplay = false;
                                SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Adapter {adapterNum} does not currently have a Combined Display.");
                            }                            
                            newAdapter.CombinedDisplay= combinedDisplay;
                        }                        
                        else
                        {
                            // This adapter doesn't support Combined Displays, so we force things to false
                            newAdapter.IsCombinedDisplay = false;
                            newAdapter.CombinedDisplay = new CombinedDisplayArgsDto();
                            newAdapter.CombinedDisplay.IsSupported = false;
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Adapter {adapterNum} does not support a Combined Display.");
                        }
                    }
                    catch (Exception ex)
                    {
                        SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting Combined Display settings for adapter {adapterNum}.");
                    }


                    // Enumerate displays for this adapter
                    var displays = adapter.EnumerateDisplayOutputs();
                    int displayTotalCount = displays.Count;
                    int displayCount = 0;
                    
                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Found {displayTotalCount} display(s) on adapter {adapterNum}");

                    foreach (var display in displays)
                    {

                        ctl_display_properties_t displayProperties;
                        try
                        {
                            displayProperties = display.GetProperties();
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting display properties for display {display.Name} on adapter {adapterNum}. Skipping display.");
                            continue;
                        }

                        string logDisplayId = $"{display.Name}|{displayProperties.Os_display_encoder_handle.WindowsDisplayEncoderID}";

                        // Skip inactive displays (inactive displays are not part of the current desktop so are not in use now)
                        if (((uint)displayProperties.DisplayConfigFlags & (uint)ctl_display_config_flag_t.CTL_DISPLAY_CONFIG_FLAG_DISPLAY_ACTIVE) != (uint)ctl_display_config_flag_t.CTL_DISPLAY_CONFIG_FLAG_DISPLAY_ACTIVE)
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Skipping inactive display {logDisplayId} on Adapter {adapterNum}");
                            continue;
                        }

                        displayCount++;
                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Processing display {displayCount}/{displayTotalCount} on adapter {adapterNum}");

                        // Create display with settings
                        INTEL_DISPLAY_WITH_SETTINGS newDisplay = new INTEL_DISPLAY_WITH_SETTINGS();
                        
                        // Set basic info
                        newDisplay.Name = display.Name;                                    
                        newDisplay.DisplayProperties = displayProperties;
                        // make up a adapter DeviceID that includes the PCI device and subsystem IDs that we can match on.
                        newDisplay.DeviceID = adapterDeviceID;
                        
                        // Get display settings
                        try
                        {
                            newDisplay.DisplaySettings = display.GetDisplaySettings();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got display settings for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting display settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET DISPLAY TIMING / RESOLUTION / REFRESH / ACTIVE STATE
                        //------------------------------------
                        try
                        {
                            newDisplay.DisplayTiming = display.GetTiming();
                            (newDisplay.ResolutionWidth, newDisplay.ResolutionHeight) = display.GetResolution();
                            newDisplay.RefreshRateHz = display.GetRefreshRateHz();
                            newDisplay.IsActive = display.IsActive();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got timing/resolution/refresh/active for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting timing/resolution/refresh/active for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET DISPLAY ENCODER PROPERTIES
                        //------------------------------------
                        try
                        {
                            newDisplay.AdapterDisplayEncoderProperties = display.GetAdapterDisplayEncoderProperties();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got adapter display encoder properties for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting adapter display encoder properties for display {logDisplayId} on adapter {adapterNum}.");
                        }
                        
                        //------------------------------------
                        // GET INTEGER SCALING (RETRO SCALING) SETTINGS
                        //------------------------------------
                        try
                        {
                            newDisplay.RetroScalingCaps = display.GetSupportedRetroScalingCapability();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got retro scaling caps for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                            newDisplay.RetroScalingSettings = display.GetRetroScalingSettings();
                            newDisplay.IsSupportedIntegerScaling = newDisplay.RetroScalingCaps.SupportedRetroScaling == 1 ? true : false;
                            newDisplay.IsEnabledIntegerScaling = newDisplay.RetroScalingSettings.Enable;
                            newDisplay.IntegerScalingType = (ctl_retro_scaling_type_flag_t)newDisplay.RetroScalingSettings.RetroScalingType;
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Integer scaling settings for display {logDisplayId}: Supported={newDisplay.RetroScalingCaps.SupportedRetroScaling}, Enabled={newDisplay.RetroScalingSettings.Enable}, Type={newDisplay.RetroScalingSettings.RetroScalingType}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting retro scaling settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET GPU SCALING SETTINGS
                        //------------------------------------
                        try
                        {
                            newDisplay.ScalingCaps = display.GetSupportedScalingCapability();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got scaling caps for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                            newDisplay.ScalingSettings = display.GetCurrentScaling();
                            newDisplay.IsSupportedGPUScaling = newDisplay.ScalingCaps.SupportedScaling == 1 ? true : false;
                            newDisplay.IsEnabledGPUScaling = newDisplay.ScalingSettings.Enable;
                            newDisplay.ScalingType = (ctl_scaling_type_flag_t)newDisplay.ScalingSettings.ScalingType;
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: GPU scaling settings for display {logDisplayId}: Supported={newDisplay.ScalingCaps.SupportedScaling}, Enabled={newDisplay.ScalingSettings.Enable}, Type={newDisplay.ScalingSettings.ScalingType}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting scaling settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET IMAGE SHARPENING SETTINGS
                        //------------------------------------

                        try
                        {
                            (newDisplay.SharpnessCaps,newDisplay.SharpnessFilterProperties) = display.GetSharpnessCaps();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got sharpness caps for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                            newDisplay.SharpnessSettings = display.GetCurrentSharpness();
                            newDisplay.IsSupportedImageSharpening = newDisplay.SharpnessCaps.SupportedFilterFlags != 0;
                            newDisplay.IsEnabledImageSharpening = newDisplay.SharpnessSettings.Enable;
                            newDisplay.SharpeningFilterType = (ctl_sharpness_filter_type_flag_t)newDisplay.SharpnessSettings.FilterType;
                            newDisplay.SharpeningIntensity = newDisplay.SharpnessSettings.Intensity;
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Image sharpening settings for display {logDisplayId}: Enabled={newDisplay.SharpnessSettings.Enable}, FilterType={newDisplay.SharpnessSettings.FilterType}, Intensity={newDisplay.SharpnessSettings.Intensity}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting image sharpening settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET POWER OPTIMIZATION SETTINGS
                        //------------------------------------
                        try
                        {
                            newDisplay.PowerOptimizationCaps = display.GetPowerOptimizationCaps();
                            newDisplay.PowerOptimizationSettings = display.GetPowerOptimizationSetting(newDisplay.PowerOptimizationSettings);
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got power optimization settings for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting power optimization settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET BRIGHTNESS SETTINGS
                        //------------------------------------
                        try
                        {
                            newDisplay.Brightness = display.GetBrightnessSetting();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got brightness settings for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting brightness settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET LACE CONFIG
                        //------------------------------------
                        try
                        {
                            newDisplay.LaceConfig = display.GetLACEConfig();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got LACE config for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting LACE config for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET SOFTWARE PSR SETTINGS
                        //------------------------------------
                        try
                        {
                            newDisplay.SoftwarePsrSettings = display.SoftwarePSR(newDisplay.SoftwarePsrSettings);
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got Software PSR settings for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting Software PSR settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET INTEL ARC SYNC SETTINGS
                        //------------------------------------
                        try
                        {
                            newDisplay.IntelArcSyncMonitorParams = display.GetIntelArcSyncInfoForMonitor();
                            newDisplay.IntelArcSyncProfile = display.GetIntelArcSyncProfile();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got Intel Arc Sync settings for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting Intel Arc Sync settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET WIRE FORMAT SETTINGS
                        //------------------------------------
                        try
                        {
                            newDisplay.WireFormat = display.GetWireFormat();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got wire format settings for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting wire format settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET DYNAMIC CONTRAST ENHANCEMENT SETTINGS
                        //------------------------------------
                        try
                        {
                            (newDisplay.DynamicContrastEnhancement, newDisplay.DynamicContrastEnhancementHistogram) = display.GetDynamicContrastEnhancement();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got dynamic contrast enhancement settings for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting dynamic contrast enhancement settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET CUSTOM MODES
                        //------------------------------------
                        try
                        {
                            (newDisplay.CustomModeArgs, newDisplay.CustomModes) = display.GetCustomModes();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got custom modes for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting custom modes for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET VBLANK TIMESTAMP
                        //------------------------------------
                        try
                        {
                            newDisplay.VblankTimestamp = display.GetVblankTimestamp();
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got vblank timestamp for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting vblank timestamp for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        //------------------------------------
                        // GET MUX PROPERTIES
                        //------------------------------------
                        try
                        {
                            var muxHandles = display.EnumerateMuxDevices();
                            if (muxHandles != null && muxHandles.Length > 0)
                            {
                                (newDisplay.MuxProperties, newDisplay.MuxDisplayOutputs) = display.GetMuxProperties(muxHandles[0]);
                                if (muxHandles.Length > 1)
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Multiple mux devices detected ({muxHandles.Length}); storing properties for the first one only.");
                                }
                            }
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got mux properties for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting mux properties for display {logDisplayId} on adapter {adapterNum}.");
                        }


                        // 3. Create a unique Hardware PCI ID + Target ID
                        // Format: VEN_8086&DEV_XXXX&REV_XX-PORT_X
                        
                        newDisplay.DisplayDeviceID = $"VEN_{adapterProperties.pci_vendor_id:X4}&DEV_{adapterProperties.pci_device_id:X4}&REV_{adapterProperties.rev_id:X2}-PORT_{displayProperties.Os_display_encoder_handle.WindowsDisplayEncoderID}";

                        // Add display to configuration
                        myDisplayConfig.Displays.Add(newDisplay.DisplayDeviceID, newDisplay);
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

        private IntPtr TryGetApiHandlePointer(IGCLApiHelper helper)
        {
            if (helper == null)
            {
                return IntPtr.Zero;
            }

            try
            {
                PropertyInfo handleProperty = helper.GetType().GetProperty("ApiHandle") ?? helper.GetType().GetProperty("Handle");
                if (handleProperty != null && handleProperty.GetValue(helper) is SafeHandle safeHandle)
                {
                    return safeHandle.DangerousGetHandle();
                }
            }
            catch (Exception ex)
            {
                SharedLogger.logger.Trace(ex, "IntelLibrary/TryGetApiHandlePointer: Failed to extract IGCL API handle from helper.");
            }

            return IntPtr.Zero;
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

            return stringToReturn;

            // if (_initialised && _igclApiHandle != null)
            // {
            //     ctl_result_t status = ctl_result_t.CTL_RESULT_SUCCESS;

            //     // Enumerate Intel adapters
            //     SWIGTYPE_p_unsigned_int pAdapterCount = IGCL.new_igcl_uint32P();
            //     IGCL.igcl_uint32P_assign(pAdapterCount, 0);
                
            //     status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, null);
            //     uint adapterCount = IGCL.igcl_uint32P_value(pAdapterCount);
                
            //     if (status == ctl_result_t.CTL_RESULT_SUCCESS && adapterCount > 0)
            //     {
            //         stringToReturn += $"Found {adapterCount} Intel adapter(s)\n\n";

            //         SWIGTYPE_p_p__ctl_device_adapter_handle_t ppAdapters = IGCL.new_deviceAdapterHandleP();
            //         status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, ppAdapters);
                    
            //         if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //         {
            //             IntPtr adaptersPtr = IGCL.deviceAdapterHandleP_value(ppAdapters);

            //             for (uint adapterIdx = 0; adapterIdx < adapterCount; adapterIdx++)
            //             {
            //                 IntPtr hAdapter = Marshal.ReadIntPtr(adaptersPtr, (int)(adapterIdx * IntPtr.Size));

            //                 ctl_device_adapter_properties_t adapterProps = IGCL.new_adapterPropertiesP();
            //                 status = IGCL.IGCL_GetAdapterProperties(hAdapter, adapterProps);
                            
            //                 if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                 {
            //                     stringToReturn += $"Adapter #{adapterIdx}\n";
            //                     stringToReturn += $"  Name: {adapterProps.name}\n";
            //                     stringToReturn += $"  PCI Vendor ID: 0x{adapterProps.pci_vendor_id:X4}\n";
            //                     stringToReturn += $"  PCI Device ID: 0x{adapterProps.pci_device_id:X4}\n";
            //                     stringToReturn += $"  Driver Version: {adapterProps.driver_version}\n";
            //                     stringToReturn += $"  Device Type: {adapterProps.device_type}\n";
            //                     stringToReturn += $"  Graphics Properties: {adapterProps.graphics_adapter_properties}\n\n";
            //                 }
            //             }
            //         }
            //     }
            // }

            // stringToReturn += $"\n\n";

            // return stringToReturn;
        }

        public bool SetActiveConfig(INTEL_DISPLAY_CONFIG displayConfig, int delayInMs)
        {
            return true;

            // if (_initialised && _igclApiHandle != null)
            // {
            //     ctl_result_t status = ctl_result_t.CTL_RESULT_SUCCESS;

            //     SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Managing Intel Combined Display configuration");

            //     // Enumerate Intel adapters
            //     SWIGTYPE_p_unsigned_int pAdapterCount = IGCL.new_igcl_uint32P();
            //     IGCL.igcl_uint32P_assign(pAdapterCount, 0);
                
            //     status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, null);
            //     uint adapterCount = IGCL.igcl_uint32P_value(pAdapterCount);
                
            //     if (status != ctl_result_t.CTL_RESULT_SUCCESS || adapterCount == 0)
            //     {
            //         SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: No Intel adapters found or error getting adapter count. Status: {status}");
            //         return false;
            //     }

            //     SWIGTYPE_p_p__ctl_device_adapter_handle_t ppAdapters = IGCL.new_deviceAdapterHandleP();
            //     status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, ppAdapters);
                
            //     if (status != ctl_result_t.CTL_RESULT_SUCCESS)
            //     {
            //         SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error enumerating Intel adapters. Status: {status}");
            //         return false;
            //     }

            //     IntPtr adaptersPtr = IGCL.deviceAdapterHandleP_value(ppAdapters);

            //     // Iterate through adapters
            //     for (uint adapterIdx = 0; adapterIdx < adapterCount; adapterIdx++)
            //     {
            //         IntPtr hAdapter = Marshal.ReadIntPtr(adaptersPtr, (int)(adapterIdx * IntPtr.Size));

            //         // If the display config needs a Combined Display then let's create one
            //         if (displayConfig.IsCombinedDisplay)
            //         {
            //             SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: New display layout requires a Combined Display");

            //             // Check if the Combined Display is already set exactly as we want it
            //             if (displayConfig.CombinedDisplay.Equals(ActiveDisplayConfig.CombinedDisplay))
            //             {
            //                 SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Combined Display layout is exactly the same as the one we want, so skipping setting up the Combined Display");
            //             }
            //             else
            //             {
            //                 SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Attempting to create the Intel Combined Display");
                            
            //                 ctl_combined_display_args_t combinedDisplayArgs = new ctl_combined_display_args_t();
            //                 combinedDisplayArgs.OpType = ctl_combined_display_optype_t.CTL_COMBINED_DISPLAY_OPTYPE_ENABLE;
            //                 combinedDisplayArgs.NumOutputs = (byte)displayConfig.CombinedDisplay.NumOutputs;
            //                 combinedDisplayArgs.CombinedDesktopWidth = displayConfig.CombinedDisplay.CombinedDesktopWidth;
            //                 combinedDisplayArgs.CombinedDesktopHeight = displayConfig.CombinedDisplay.CombinedDesktopHeight;
                            
            //                 // Note: In a full implementation, you would need to populate pChildInfo with the
            //                 // display handles and layout information from displayConfig.CombinedDisplay.ChildDisplayHandles
                            
            //                 status = IGCL.ctlGetSetCombinedDisplay(hAdapter, combinedDisplayArgs);
                            
            //                 if (status != ctl_result_t.CTL_RESULT_SUCCESS)
            //                 {
            //                     SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error creating the Intel Combined Display. ctlGetSetCombinedDisplay() returned error code {status}");
            //                     return false;
            //                 }
            //                 else
            //                 {
            //                     SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Successfully created the Intel Combined Display");
                                
            //                     // Verify the created display matches what we want
            //                     ctl_combined_display_args_t queryArgs = new ctl_combined_display_args_t();
            //                     queryArgs.OpType = ctl_combined_display_optype_t.CTL_COMBINED_DISPLAY_OPTYPE_QUERY_CONFIG;
                                
            //                     status = IGCL.ctlGetSetCombinedDisplay(hAdapter, queryArgs);
                                
            //                     if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                     {
            //                         if (queryArgs.NumOutputs == displayConfig.CombinedDisplay.NumOutputs &&
            //                             queryArgs.CombinedDesktopWidth == displayConfig.CombinedDisplay.CombinedDesktopWidth &&
            //                             queryArgs.CombinedDesktopHeight == displayConfig.CombinedDisplay.CombinedDesktopHeight)
            //                         {
            //                             SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: This new Combined Display layout matches the desired configuration.");
            //                         }
            //                         else
            //                         {
            //                             SharedLogger.logger.Warn($"IntelLibrary/SetActiveConfig: This new Combined Display layout is different from the one originally saved. You may need to update this desktop profile.");
            //                         }
            //                     }
            //                 }
            //             }
            //         }
            //         else
            //         {
            //             SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: New display layout does NOT require a Combined Display");

            //             if (ActiveDisplayConfig.IsCombinedDisplay)
            //             {
            //                 SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Combined Display layout is currently in use but is NOT required, so we need to destroy the Combined Display");

            //                 ctl_combined_display_args_t combinedDisplayArgs = new ctl_combined_display_args_t();
            //                 combinedDisplayArgs.OpType = ctl_combined_display_optype_t.CTL_COMBINED_DISPLAY_OPTYPE_DISABLE;
                            
            //                 status = IGCL.ctlGetSetCombinedDisplay(hAdapter, combinedDisplayArgs);
                            
            //                 if (status != ctl_result_t.CTL_RESULT_SUCCESS)
            //                 {
            //                     SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: Error destroying the Intel Combined Display. ctlGetSetCombinedDisplay() returned error code {status}");
            //                     return false;
            //                 }
            //                 else
            //                 {
            //                     SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Successfully destroyed the Intel Combined Display.");
            //                 }
            //             }
            //             else
            //             {
            //                 SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Combined Display layout is not currently in use and is NOT required, so leaving things as they are.");
            //             }
            //         }
            //     }
            // }
            // else
            // {
            //     SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: ERROR - Tried to run SetActiveConfig but the Intel IGCL library isn't initialised!");
            //     throw new IntelLibraryException($"Tried to run SetActiveConfig but the Intel IGCL library isn't initialised!");
            // }

            // return true;
        }

        public bool SetActiveConfigOverride(INTEL_DISPLAY_CONFIG displayConfig, int delayInMs)
        {
            return true;

            // if (_initialised && _igclApiHandle != null)
            // {
            //     ctl_result_t status = ctl_result_t.CTL_RESULT_SUCCESS;

            //     SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Applying display settings stored in the display configuration");

            //     // Enumerate Intel adapters
            //     SWIGTYPE_p_unsigned_int pAdapterCount = IGCL.new_igcl_uint32P();
            //     IGCL.igcl_uint32P_assign(pAdapterCount, 0);
                
            //     status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, null);
            //     uint adapterCount = IGCL.igcl_uint32P_value(pAdapterCount);
                
            //     if (status != ctl_result_t.CTL_RESULT_SUCCESS || adapterCount == 0)
            //     {
            //         SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: No Intel adapters found or error getting adapter count. Status: {status}");
            //         return false;
            //     }

            //     SWIGTYPE_p_p__ctl_device_adapter_handle_t ppAdapters = IGCL.new_deviceAdapterHandleP();
            //     status = IGCL.IGCL_EnumerateAdapters(_igclApiHandle, pAdapterCount, ppAdapters);
                
            //     if (status != ctl_result_t.CTL_RESULT_SUCCESS)
            //     {
            //         SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: Error enumerating Intel adapters. Status: {status}");
            //         return false;
            //     }

            //     IntPtr adaptersPtr = IGCL.deviceAdapterHandleP_value(ppAdapters);

            //     // Iterate through adapters
            //     for (uint adapterIdx = 0; adapterIdx < adapterCount; adapterIdx++)
            //     {
            //         IntPtr hAdapter = Marshal.ReadIntPtr(adaptersPtr, (int)(adapterIdx * IntPtr.Size));

            //         // Enumerate displays for this adapter
            //         SWIGTYPE_p_unsigned_int pDisplayCount = IGCL.new_igcl_uint32P();
            //         IGCL.igcl_uint32P_assign(pDisplayCount, 0);
                    
            //         status = IGCL.IGCL_EnumerateDisplays(hAdapter, pDisplayCount, null);
            //         uint displayCount = IGCL.igcl_uint32P_value(pDisplayCount);
                    
            //         if (status != ctl_result_t.CTL_RESULT_SUCCESS || displayCount == 0)
            //         {
            //             continue;
            //         }

            //         SWIGTYPE_p_p__ctl_display_output_handle_t ppDisplays = IGCL.new_displayOutputHandleP();
            //         status = IGCL.IGCL_EnumerateDisplays(hAdapter, pDisplayCount, ppDisplays);
                    
            //         if (status != ctl_result_t.CTL_RESULT_SUCCESS)
            //         {
            //             continue;
            //         }

            //         IntPtr displaysPtr = IGCL.displayOutputHandleP_value(ppDisplays);

            //         // Iterate through displays
            //         for (uint displayIdx = 0; displayIdx < displayCount; displayIdx++)
            //         {
            //             IntPtr hDisplay = Marshal.ReadIntPtr(displaysPtr, (int)(displayIdx * IntPtr.Size));

            //             // Find the stored settings for this display
            //             if (!displayConfig.Displays.ContainsKey(hDisplay))
            //             {
            //                 SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: No stored settings found for display handle {hDisplay}, skipping");
            //                 continue;
            //             }

            //             INTEL_DISPLAY_WITH_SETTINGS displaySettingsWeStored = displayConfig.Displays[hDisplay];
            //             SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Applying settings for display: {displaySettingsWeStored.Display.Name}");

            //             //------------------------------------
            //             // SET INTEGER SCALING IF NEEDED
            //             //------------------------------------
            //             if (displaySettingsWeStored.IsSupportedIntegerScaling)
            //             {
            //                 ctl_retro_scaling_caps_t retroScalingCaps = new ctl_retro_scaling_caps_t();
            //                 status = IGCL.ctlGetSupportedRetroScalingCapability(hAdapter, retroScalingCaps);
                            
            //                 if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                 {
            //                     ctl_retro_scaling_settings_t retroScalingSettings = new ctl_retro_scaling_settings_t();
            //                     retroScalingSettings.Get = true;
            //                     status = IGCL.ctlGetSetRetroScaling(hAdapter, retroScalingSettings);
                                
            //                     if (status == ctl_result_t.CTL_RESULT_SUCCESS && 
            //                         (retroScalingSettings.Enable != displaySettingsWeStored.IsEnabledIntegerScaling ||
            //                          (uint)retroScalingSettings.RetroScalingType != (uint)displaySettingsWeStored.IntegerScalingType))
            //                     {
            //                         retroScalingSettings.Get = false;
            //                         retroScalingSettings.Enable = displaySettingsWeStored.IsEnabledIntegerScaling;
            //                         retroScalingSettings.RetroScalingType = (uint)displaySettingsWeStored.IntegerScalingType;
                                    
            //                         status = IGCL.ctlGetSetRetroScaling(hAdapter, retroScalingSettings);
            //                         if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                         {
            //                             SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Integer Scaling to Enabled={displaySettingsWeStored.IsEnabledIntegerScaling}, Type={displaySettingsWeStored.IntegerScalingType}");
            //                         }
            //                     }
            //                     else if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                     {
            //                         SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Integer Scaling already set to desired values, skipping");
            //                     }
            //                 }
            //                 else
            //                 {
            //                     SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Integer Scaling not supported by current hardware, skipping");
            //                 }
            //             }

            //             //------------------------------------
            //             // SET GPU SCALING IF NEEDED
            //             //------------------------------------
            //             if (displaySettingsWeStored.IsSupportedGPUScaling)
            //             {
            //                 ctl_scaling_caps_t scalingCaps = new ctl_scaling_caps_t();
            //                 status = IGCL.ctlGetSupportedScalingCapability(hDisplay, scalingCaps);
                            
            //                 if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                 {
            //                     ctl_scaling_settings_t scalingSettings = new ctl_scaling_settings_t();
            //                     status = IGCL.ctlGetCurrentScaling(hDisplay, scalingSettings);
                                
            //                     if (status == ctl_result_t.CTL_RESULT_SUCCESS &&
            //                         (scalingSettings.Enable != displaySettingsWeStored.IsEnabledGPUScaling ||
            //                          (uint)scalingSettings.ScalingType != (uint)displaySettingsWeStored.ScalingType))
            //                     {
            //                         scalingSettings.Enable = displaySettingsWeStored.IsEnabledGPUScaling;
            //                         scalingSettings.ScalingType = (uint)displaySettingsWeStored.ScalingType;
                                    
            //                         status = IGCL.ctlSetCurrentScaling(hDisplay, scalingSettings);
            //                         if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                         {
            //                             SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set GPU Scaling to Enabled={displaySettingsWeStored.IsEnabledGPUScaling}, Type={displaySettingsWeStored.ScalingType}");
            //                         }
            //                     }
            //                     else if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                     {
            //                         SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: GPU Scaling already set to desired values, skipping");
            //                     }
            //                 }
            //                 else
            //                 {
            //                     SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: GPU Scaling not supported by current hardware, skipping");
            //                 }
            //             }

            //             //------------------------------------
            //             // SET IMAGE SHARPENING IF NEEDED
            //             //------------------------------------
            //             if (displaySettingsWeStored.IsSupportedImageSharpening)
            //             {
            //                 ctl_sharpness_caps_t sharpnessCaps = new ctl_sharpness_caps_t();
            //                 status = IGCL.ctlGetSharpnessCaps(hDisplay, sharpnessCaps);
                            
            //                 if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                 {
            //                     ctl_sharpness_settings_t sharpnessSettings = new ctl_sharpness_settings_t();
            //                     status = IGCL.ctlGetCurrentSharpness(hDisplay, sharpnessSettings);
                                
            //                     if (status == ctl_result_t.CTL_RESULT_SUCCESS &&
            //                         (sharpnessSettings.Enable != displaySettingsWeStored.IsEnabledImageSharpening ||
            //                          (uint)sharpnessSettings.FilterType != (uint)displaySettingsWeStored.SharpeningFilterType ||
            //                          Math.Abs(sharpnessSettings.Intensity - displaySettingsWeStored.SharpeningIntensity) > 0.001f))
            //                     {
            //                         sharpnessSettings.Enable = displaySettingsWeStored.IsEnabledImageSharpening;
            //                         sharpnessSettings.FilterType = (uint)displaySettingsWeStored.SharpeningFilterType;
            //                         sharpnessSettings.Intensity = displaySettingsWeStored.SharpeningIntensity;
                                    
            //                         status = IGCL.ctlSetCurrentSharpness(hDisplay, sharpnessSettings);
            //                         if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                         {
            //                             SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Image Sharpening to Enabled={displaySettingsWeStored.IsEnabledImageSharpening}, Intensity={displaySettingsWeStored.SharpeningIntensity}");
            //                         }
            //                     }
            //                     else if (status == ctl_result_t.CTL_RESULT_SUCCESS)
            //                     {
            //                         SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Image Sharpening already set to desired values, skipping");
            //                     }
            //                 }
            //                 else
            //                 {
            //                     SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Image Sharpening not supported by current hardware, skipping");
            //                 }
            //             }
            //         }
            //     }
            // }
            // else
            // {
            //     SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: ERROR - Tried to run SetActiveConfigOverride but the Intel IGCL library isn't initialised!");
            //     throw new IntelLibraryException($"Tried to run SetActiveConfigOverride but the Intel IGCL library isn't initialised!");
            // }

            // return true;
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

            if (_initialised && _igclApiHelper != null)
            {
                
                // Enumerate the Intel GPUs adapters in the sytem
                var adapters = _igclApiHelper.EnumerateAdapters();
                int adapterTotalCount = adapters.Count;
                int adapterNum = 0;

                SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Found {adapterTotalCount} Intel GPU adapter(s)");


                // Go through each adapter
                foreach (var adapter in adapters)
                {
                    var displays = adapter.EnumerateDisplayOutputs();
                    int displayTotalCount = displays.Count;
                    int displayNum = 0;

                    var adapterDeviceProperties = adapter.GetDeviceProperties();

                    foreach (var display in displays)
                    {
                        displayNum++;
                        var displayProperties = display.GetProperties();

                        // Skip inactive displays (inactive displays are not part of the current desktop so are not in use now)
                        if (((uint)displayProperties.DisplayConfigFlags & (uint)ctl_display_config_flag_t.CTL_DISPLAY_CONFIG_FLAG_DISPLAY_ACTIVE) != (uint)ctl_display_config_flag_t.CTL_DISPLAY_CONFIG_FLAG_DISPLAY_ACTIVE)
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Skipping inactive display Index {displayNum} on Adapter {adapterNum}");
                            continue;
                        }

                        var displayDeviceProperties = display.GetProperties();

                        // Create display identifier: IntelIGCL|AdapterName|DisplayIndex|AdapterIndex
                        List<string> displayInfo = new List<string>
                        {
                            "IntelIGCL",
                            adapter.Name,
                            adapterDeviceProperties.pci_device_id.ToString("G"),
                            adapterDeviceProperties.pci_subsys_id.ToString("G"),
                            display.Name,
                            displayProperties.Type.ToString(),
                            displayProperties.Os_display_encoder_handle.WindowsDisplayEncoderID.ToString()
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

            List<string> displayIdentifiers = new List<string>();
            failure = false;

            if (_initialised && _igclApiHelper != null)
            {
                
                // Enumerate the Intel GPUs adapters in the sytem
                var adapters = _igclApiHelper.EnumerateAdapters();
                int adapterTotalCount = adapters.Count;
                int adapterNum = 0;

                SharedLogger.logger.Trace($"IntelLibrary/GetAllConnectedDisplayIdentifiers: Found {adapterTotalCount} Intel GPU adapter(s)");


                // Go through each adapter
                foreach (var adapter in adapters)
                {
                    var displays = adapter.EnumerateDisplayOutputs();
                    int displayTotalCount = displays.Count;
                    int displayNum = 0;

                    var adapterDeviceProperties = adapter.GetDeviceProperties();

                    foreach (var display in displays)
                    {
                        displayNum++;
                        var displayProperties = display.GetProperties();

                        // Skip displays unless they are attached and windows knows about them (these displays could be attached to the desktop if we wanted to use them)
                        if (((uint)displayProperties.DisplayConfigFlags & (uint)ctl_display_config_flag_t.CTL_DISPLAY_CONFIG_FLAG_DISPLAY_ATTACHED) != (uint)ctl_display_config_flag_t.CTL_DISPLAY_CONFIG_FLAG_DISPLAY_ATTACHED)

                        {
                            SharedLogger.logger.Trace($"IntelLibrary/GetCurrentDisplayIdentifiers: Skipping inactive display Index {displayNum} on Adapter {adapterNum}");
                            continue;
                        }                        

                        var displayDeviceProperties = display.GetProperties();

                        // Create display identifier: IntelIGCL|AdapterName|DisplayIndex|AdapterIndex
                        List<string> displayInfo = new List<string>
                        {
                            "IntelIGCL",
                            adapter.Name,
                            adapterDeviceProperties.pci_device_id.ToString("G"),
                            adapterDeviceProperties.pci_subsys_id.ToString("G"),
                            display.Name,
                            displayProperties.Type.ToString(),
                            displayProperties.Os_display_encoder_handle.WindowsDisplayEncoderID.ToString()
                        };
                        
                        string displayIdentifier = String.Join("|", displayInfo);
                        if (!displayIdentifiers.Contains(displayIdentifier))
                        {
                            displayIdentifiers.Add(displayIdentifier);
                            SharedLogger.logger.Debug($"IntelLibrary/GetAllConnectedDisplayIdentifiers: DisplayIdentifier detected: {displayIdentifier}");
                        }
                    }
                }
            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/GetCurrentDisplaGetAllConnectedDisplayIdentifiersyIdentifiers: ERROR - Tried to get Displays but the Intel IGCL library isn't initialised!");
                throw new IntelLibraryException($"Tried to get Displays but the Intel IGCL library isn't initialised!");
            }

            // Sort the display identifiers
            displayIdentifiers.Sort();

            return displayIdentifiers;
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

