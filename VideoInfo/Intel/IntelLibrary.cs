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
using System.Threading;
using EDIDParser;
using Windows.Graphics.Display;

namespace DisplayMagicianShared.Intel
{
    #region Data Structures

    [StructLayout(LayoutKind.Sequential)]
    public struct INTEL_DISPLAY_WITH_SETTINGS : IEquatable<INTEL_DISPLAY_WITH_SETTINGS>
    {
        public string Name;
        public string DisplayDeviceID;
        public uint DisplayIndex;
        public uint AdapterIndex;

        public byte[] Edid; // EDID data for the display
        
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
        public bool IsSupportedDisplaySettings;
        public DisplaySettingsDto DisplaySettings;
        public ScalingSettingsDto ScalingSettings;
        public SharpnessSettingsDto SharpnessSettings;
        public RetroScalingSettingsDto RetroScalingSettings;
        public DceArgsDto DynamicContrastEnhancement;
        public bool IsSupportedDynamicContrastEnhancement;
        public uint[] DynamicContrastEnhancementHistogram;
        public bool IsSupportedPowerOptimization;
        public bool IsEnabledPowerOptimization;
        public PowerOptimizationSettingsDto PowerOptimizationSettings;
        public bool IsEnabledLaceConfig;
        public LaceConfigDto LaceConfig;

        public bool IsEnabledSoftwarePsrSettings;
        public SwPsrSettingsDto SoftwarePsrSettings;
        public GenlockArgsDto GenlockArgs;
        public IntelArcSyncMonitorParamsDto IntelArcSyncMonitorParams;
        public bool IsSupportedIntelArcSync;
        public AdapterDisplayEncoderPropertiesDto AdapterDisplayEncoderProperties;

        // Display getter outputs
        public DisplayPropertiesDto DisplayProperties;
        //public ctl_device_adapter_properties_t DeviceProperties;
        public string DeviceID;
        public ctl_display_timing_t DisplayTiming;
        public bool IsSupportedWireFormat;
        public WireFormatConfigDto WireFormat;
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
            Edid = Array.Empty<byte>();
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

            IsSupportedDisplaySettings = false;
            DisplaySettings = new DisplaySettingsDto();
            ScalingSettings = new ScalingSettingsDto();
            SharpnessSettings = new SharpnessSettingsDto();
            RetroScalingSettings = new RetroScalingSettingsDto();
            DynamicContrastEnhancement = new DceArgsDto();
            IsSupportedDynamicContrastEnhancement = false;
            DynamicContrastEnhancementHistogram = Array.Empty<uint>();
            IsSupportedPowerOptimization = false;
            IsEnabledPowerOptimization = false;
            PowerOptimizationSettings = new PowerOptimizationSettingsDto();
            IsEnabledLaceConfig = false;
            LaceConfig = new LaceConfigDto();
            IsEnabledSoftwarePsrSettings = false;
            SoftwarePsrSettings = new SwPsrSettingsDto();
            GenlockArgs = new GenlockArgsDto();
            IntelArcSyncMonitorParams = new IntelArcSyncMonitorParamsDto();
            IsSupportedIntelArcSync = false;
            AdapterDisplayEncoderProperties = new AdapterDisplayEncoderPropertiesDto();

            DisplayProperties = new DisplayPropertiesDto();
            //DeviceProperties = new ctl_device_adapter_properties_t();
            DeviceID = "";
            DisplayTiming = new ctl_display_timing_t();
            IsSupportedWireFormat = false;
            WireFormat = new WireFormatConfigDto();
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

        internal static bool AreDisplaySettingsEqual(DisplaySettingsDto left, DisplaySettingsDto right)
        {
            return left.Size == right.Size &&
                left.Version == right.Version &&
                left.Set == right.Set &&
                left.SupportedFlags == right.SupportedFlags &&
                left.ControllableFlags == right.ControllableFlags &&
                left.ValidFlags == right.ValidFlags &&
                left.LowLatency == right.LowLatency &&
                left.SourceTm == right.SourceTm &&
                left.ContentType == right.ContentType &&
                left.QuantizationRange == right.QuantizationRange &&
                left.SupportedPictureAr == right.SupportedPictureAr &&
                left.PictureAr == right.PictureAr &&
                left.AudioSettings == right.AudioSettings;
        }

        internal static int GetDisplaySettingsHash(DisplaySettingsDto settings)
        {
            var hash = new HashCode();
            hash.Add(settings.Size);
            hash.Add(settings.Version);
            hash.Add(settings.Set);
            hash.Add(settings.SupportedFlags);
            hash.Add(settings.ControllableFlags);
            hash.Add(settings.ValidFlags);
            hash.Add(settings.LowLatency);
            hash.Add(settings.SourceTm);
            hash.Add(settings.ContentType);
            hash.Add(settings.QuantizationRange);
            hash.Add(settings.SupportedPictureAr);
            hash.Add(settings.PictureAr);
            hash.Add(settings.AudioSettings);
            return hash.ToHashCode();
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
            if (Edid.Length != other.Edid.Length || !Edid.SequenceEqual(other.Edid))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The Edid values don't equal each other");
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
            if (IsSupportedDisplaySettings != other.IsSupportedDisplaySettings)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedDisplaySettings values don't equal each other");
                return false;
            }
            if (!AreDisplaySettingsEqual(DisplaySettings, other.DisplaySettings))
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
            if (IsSupportedDynamicContrastEnhancement != other.IsSupportedDynamicContrastEnhancement)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedDynamicContrastEnhancement values don't equal each other");
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
            if (IsSupportedPowerOptimization != other.IsSupportedPowerOptimization)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedPowerOptimization values don't equal each other");
                return false;
            }
            if (IsEnabledPowerOptimization != other.IsEnabledPowerOptimization)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsEnabledPowerOptimization values don't equal each other");
                return false;
            }
            if (!EqualityComparer<PowerOptimizationSettingsDto>.Default.Equals(PowerOptimizationSettings, other.PowerOptimizationSettings))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The PowerOptimizationSettings values don't equal each other");
                return false;
            }
            if (IsEnabledLaceConfig != other.IsEnabledLaceConfig)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedLaceConfig values don't equal each other");
                return false;
            }
            if (!EqualityComparer<LaceConfigDto>.Default.Equals(LaceConfig, other.LaceConfig))
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The LaceConfig values don't equal each other");
                return false;
            }
            if (IsEnabledSoftwarePsrSettings != other.IsEnabledSoftwarePsrSettings)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsEnabledSoftwarePsrSettings values don't equal each other");
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
            if (IsSupportedIntelArcSync != other.IsSupportedIntelArcSync)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedIntelArcSync values don't equal each other");
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
            if (!DisplayProperties.Equals(other.DisplayProperties))
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
            if (IsSupportedWireFormat != other.IsSupportedWireFormat)
            {
                SharedLogger.logger.Trace($"INTEL_DISPLAY_WITH_SETTINGS/Equals: The IsSupportedWireFormat values don't equal each other");
                return false;
            }
            if (!WireFormat.Equals(other.WireFormat))
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
                IsSupportedDisplaySettings, GetDisplaySettingsHash(DisplaySettings), ScalingSettings, SharpnessSettings, RetroScalingSettings, IsSupportedDynamicContrastEnhancement, DynamicContrastEnhancement, DynamicContrastEnhancementHistogram, PowerOptimizationSettings, LaceConfig, SoftwarePsrSettings, GenlockArgs, IsSupportedIntelArcSync, IntelArcSyncMonitorParams, AdapterDisplayEncoderProperties, 
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
        public DeviceAdapterPropertiesDto AdapterProperties;
        public bool CombinedDisplayIsSupported;
        public bool IsCombinedDisplay;
        //public INTEL_COMBINED_DISPLAY CombinedDisplay;
        public CombinedDisplayArgsDto CombinedDisplay;

        public INTEL_ADAPTER()
        {
            AdapterID = "";
            Name = "";
            AdapterIndex = 0;
            AdapterProperties = new DeviceAdapterPropertiesDto();
            CombinedDisplayIsSupported = false;
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
            if (!AdapterProperties.Equals(other.AdapterProperties))
            {
                SharedLogger.logger.Trace($"INTEL_ADAPTER/Equals: The AdapterProperties values don't equal each other");
                return false;
            }
            if (CombinedDisplayIsSupported != other.CombinedDisplayIsSupported)
            {
                SharedLogger.logger.Trace($"INTEL_ADAPTER/Equals: The CombinedDisplayIsSupported values don't equal each other");
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
            return (AdapterID, Name, AdapterIndex, AdapterProperties, CombinedDisplayIsSupported, IsCombinedDisplay, CombinedDisplay).GetHashCode();
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
                    var adapterDeviceID = $"{adapter.Name}|{adapterProperties.PciDeviceId.ToString("G")}|{adapterProperties.PciSubsysId.ToString("G")}";

                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Processing Intel GPU adapter {adapterNum}({adapterProperties.Name}), PCI Device ID: 0x{adapterProperties.PciDeviceId:X4}, device type {adapterProperties.DeviceType} ({adapterNum}/{adapterTotalCount}");                    


                    // Create adapter storage struct
                    INTEL_ADAPTER newAdapter = new INTEL_ADAPTER();
                    newAdapter.AdapterID = adapterDeviceID;
                    newAdapter.Name = adapter.Name;
                    newAdapter.AdapterProperties = adapterProperties;                    

                    //------------------------------------
                    // CHECK FOR COMBINED DISPLAY CONFIGURATION PER ADAPTER
                    //------------------------------------
                    

                    try
                    {
                        var combinedDisplay = adapter.GetCombinedDisplay();
                        SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got Combined Display settings for adapter {adapterNum}");

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
                        newAdapter.CombinedDisplayIsSupported = true;                    
                    }
                    catch (IGCLException ex)
                    {
                         if (ex.Result == ctl_result_t.CTL_RESULT_ERROR_UNSUPPORTED_FEATURE ||
                                               ex.Result == ctl_result_t.CTL_RESULT_ERROR_UNSUPPORTED_VERSION ||
                                               ex.Result == ctl_result_t.CTL_RESULT_ERROR_INVALID_OPERATION_TYPE ||
                                               ex.Result == ctl_result_t.CTL_RESULT_ERROR_INVALID_ARGUMENT)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: IGCLException: Combined Display not supported for adapter {adapterNum}. Skipping adapter.");
                            newAdapter.CombinedDisplayIsSupported = false;                            
                        }
                        else
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: IGCLException getting Combined Display settings for adapter {adapterNum}.");                        
                        }
                    }
                    catch (Exception ex)
                    {
                        SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting Combined Display settings for adapter {adapterNum}.");
                    }

                    // Add adapter to config
                    myDisplayConfig.PhysicalAdapters.Add(adapterDeviceID, newAdapter);

                    // Enumerate displays for this adapter
                    var displays = adapter.EnumerateDisplayOutputs();
                    int displayTotalCount = displays.Count;
                    int displayCount = 0;
                    
                    SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Found {displayTotalCount} display(s) on adapter {adapterNum}");

                    foreach (var display in displays)
                    {

                        displayCount++;

                        DisplayPropertiesDto displayProperties;
                        try
                        {
                            displayProperties = display.GetProperties();
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting display properties for display {display.Name} on adapter {adapterNum}. Skipping display.");
                            continue;
                        }

                        string logDisplayId = $"{display.Name}|{displayProperties.OsDisplayEncoderHandle.WindowsDisplayEncoderID}";

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
                        newDisplay.DisplayProperties = displayProperties;
                        // make up a adapter DeviceID that includes the PCI device and subsystem IDs that we can match on.
                        newDisplay.DeviceID = adapterDeviceID;
                        
                        // Get display settings
                        try
                        {
                            newDisplay.DisplaySettings = display.GetDisplaySettings();
                            newDisplay.IsSupportedDisplaySettings = Convert.ToUInt64((object)newDisplay.DisplaySettings.ValidFlags) != 0 ||
                                Convert.ToUInt64((object)newDisplay.DisplaySettings.ControllableFlags) != 0;
                            SharedLogger.logger.Trace($"IntelLibrary/GetIntelDisplayConfig: Successfully got display settings for display {logDisplayId} ({displayCount}/{displayTotalCount}) on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting display settings for display {logDisplayId} on adapter {adapterNum}.");
                        }

                        // Make a display name that works across reboots
                        try
                        {
                            
                            var manufacturerCode = "";
                            var productCode = "";
                            try {
                                var edidBytes = display.GetPanelEdidData();
                                var edid = new EDID(edidBytes);
                                newDisplay.Edid = edidBytes;
                                manufacturerCode = edid.ManufacturerCode.ToString();
                                productCode = edid.ProductCode.ToString();
                                SharedLogger.logger.Trace($"IntelLibrary/GetAllConnectedDisplayIdentifiers: Successfully got EDID for display Index {displayCount} on Adapter {adapterNum}");                            
                            }
                            catch (Exception ex)
                            {
                                SharedLogger.logger.Error($"IntelLibrary/GetAllConnectedDisplayIdentifiers: ERROR - Failed to get EDID for display Index {displayCount} on Adapter {adapterNum}: {ex.Message}");
                            }
                            newDisplay.Name = $"Display_{manufacturerCode}_{productCode}_{displayProperties.OsDisplayEncoderHandle.WindowsDisplayEncoderID}";
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/GetIntelDisplayConfig: Exception getting display settings for display {logDisplayId} on adapter {adapterNum}.");
                            // Backup Display name if EDID lookup fails
                            newDisplay.Name = $"Display_{logDisplayId}_{displayProperties.OsDisplayEncoderHandle.WindowsDisplayEncoderID}";
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
                            uint retroScalingMask =
                                (uint)(ctl_retro_scaling_type_flag_t.CTL_RETRO_SCALING_TYPE_FLAG_INTEGER |
                                    ctl_retro_scaling_type_flag_t.CTL_RETRO_SCALING_TYPE_FLAG_NEAREST_NEIGHBOUR);

                            newDisplay.IsSupportedIntegerScaling = (newDisplay.RetroScalingCaps.SupportedRetroScaling & retroScalingMask) != 0;
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
                            uint gpuScalingMask =
                                (uint)(ctl_scaling_type_flag_t.CTL_SCALING_TYPE_FLAG_CENTERED |
                                    ctl_scaling_type_flag_t.CTL_SCALING_TYPE_FLAG_STRETCHED |
                                    ctl_scaling_type_flag_t.CTL_SCALING_TYPE_FLAG_ASPECT_RATIO_CENTERED_MAX |
                                    ctl_scaling_type_flag_t.CTL_SCALING_TYPE_FLAG_CUSTOM);

                            newDisplay.IsSupportedGPUScaling = (newDisplay.ScalingCaps.SupportedScaling & gpuScalingMask) != 0;
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
                            newDisplay.IsSupportedPowerOptimization = newDisplay.PowerOptimizationCaps.SupportedFeatures != 0;
                            newDisplay.IsEnabledPowerOptimization = newDisplay.PowerOptimizationSettings.Enable;
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
                            newDisplay.IsEnabledLaceConfig = newDisplay.LaceConfig.Enabled;
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
                            newDisplay.IsEnabledSoftwarePsrSettings = newDisplay.SoftwarePsrSettings.Enable;
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
                            newDisplay.IsSupportedIntelArcSync = newDisplay.IntelArcSyncMonitorParams.IsIntelArcSyncSupported;
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
                            bool wireFormatSupported = false;
                            var supportedWireFormats = newDisplay.WireFormat.SupportedWireFormat;

                            if (supportedWireFormats != null)
                            {
                                foreach (var supportedWireFormat in supportedWireFormats)
                                {
                                    if (supportedWireFormat.ColorDepth != 0)
                                    {
                                        wireFormatSupported = true;
                                        break;
                                    }
                                }
                            }

                            newDisplay.IsSupportedWireFormat = wireFormatSupported;
                            
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
                            newDisplay.IsSupportedDynamicContrastEnhancement = newDisplay.DynamicContrastEnhancement.IsSupported;
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
                        
                        newDisplay.DisplayDeviceID = $"VEN_{adapterProperties.PciVendorId:X4}&DEV_{adapterProperties.PciDeviceId:X4}&REV_{adapterProperties.RevId:X2}-PORT_{displayProperties.OsDisplayEncoderHandle.WindowsDisplayEncoderID}";

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
            if (_initialised && _igclApiHelper != null)
            {
                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Managing Intel Combined Display configuration");

                var adapters = _igclApiHelper.EnumerateAdapters();
                if (adapters == null || adapters.Count == 0)
                {
                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: No Intel adapters found or error getting adapter count.");
                    return false;
                }

                int adapterNum = 0;
                foreach (var adapter in adapters)
                {
                    adapterNum++;

                    var adapterProperties = adapter.GetProperties();
                    string adapterDeviceID = $"{adapter.Name}|{adapterProperties.PciDeviceId.ToString("G")}|{adapterProperties.PciSubsysId.ToString("G")}";

                    if (!displayConfig.PhysicalAdapters.TryGetValue(adapterDeviceID, out INTEL_ADAPTER desiredAdapter))
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: No stored settings found for adapter {adapterDeviceID}, skipping");
                        continue;
                    }

                    CombinedDisplayArgsDto currentCombinedDisplay;
                    try
                    {
                        currentCombinedDisplay = adapter.GetCombinedDisplay();
                    }
                    catch (Exception ex)
                    {
                        SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfig: Exception getting Combined Display settings for adapter {adapterNum}.");
                        continue;
                    }


                    bool currentCombinedDisplayInUse = currentCombinedDisplay.NumOutputs > 1;
                    bool desiredCombinedDisplayInUse = desiredAdapter.IsCombinedDisplay;

                    if (desiredCombinedDisplayInUse)
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: New display layout requires a Combined Display on adapter {adapterNum}");

                        if (currentCombinedDisplay.Equals(desiredAdapter.CombinedDisplay))
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Combined Display layout matches desired configuration, skipping");
                            continue;
                        }

                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Attempting to create the Intel Combined Display on adapter {adapterNum}");

                        if (currentCombinedDisplayInUse)
                        {
                            CombinedDisplayArgsDto disableCombinedDisplayArgs = currentCombinedDisplay;
                            disableCombinedDisplayArgs.OpType = ctl_combined_display_optype_t.CTL_COMBINED_DISPLAY_OPTYPE_DISABLE;

                            try
                            {
                                adapter.SetCombinedDisplay(disableCombinedDisplayArgs);
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Disabled the current Intel Combined Display on adapter {adapterNum} before creating a new one");
                                Thread.Sleep(delayInMs); // Wait a moment to ensure the disable takes effect
                            }
                            catch (Exception ex)
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfig: Error disabling the current Intel Combined Display on adapter {adapterNum}");
                                return false;
                            }
                        }

                        CombinedDisplayArgsDto combinedDisplayArgs = desiredAdapter.CombinedDisplay;
                        combinedDisplayArgs.OpType = ctl_combined_display_optype_t.CTL_COMBINED_DISPLAY_OPTYPE_ENABLE;

                        if (combinedDisplayArgs.ChildInfos == null || combinedDisplayArgs.ChildInfos.Length == 0)
                        {
                            SharedLogger.logger.Error($"IntelLibrary/SetActiveConfig: The desired combined display on adapter {adapterNum} is missing ChildInfos.");
                            return false;
                        }

                        try
                        {
                            adapter.SetCombinedDisplay(combinedDisplayArgs);
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Successfully created the Intel Combined Display on adapter {adapterNum}");
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfig: Error creating the Intel Combined Display on adapter {adapterNum}");
                            return false;
                        }

                        try
                        {
                            var updatedCombinedDisplay = adapter.GetCombinedDisplay();
                            if (updatedCombinedDisplay.NumOutputs == desiredAdapter.CombinedDisplay.NumOutputs &&
                                updatedCombinedDisplay.CombinedDesktopWidth == desiredAdapter.CombinedDisplay.CombinedDesktopWidth &&
                                updatedCombinedDisplay.CombinedDesktopHeight == desiredAdapter.CombinedDisplay.CombinedDesktopHeight)
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: This new Combined Display layout matches the desired configuration.");
                            }
                            else
                            {
                                SharedLogger.logger.Warn($"IntelLibrary/SetActiveConfig: This new Combined Display layout is different from the one originally saved. You may need to update this desktop profile.");
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfig: Exception verifying Combined Display settings for adapter {adapterNum}.");
                        }
                    }
                    else
                    {
                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Combined Display in use but new display layout does NOT require a Combined Display on adapter {adapterNum}");

                        if (currentCombinedDisplayInUse)
                        {
                            CombinedDisplayArgsDto disableCombinedDisplayArgs = currentCombinedDisplay;
                            disableCombinedDisplayArgs.OpType = ctl_combined_display_optype_t.CTL_COMBINED_DISPLAY_OPTYPE_DISABLE;

                            try
                            {
                                adapter.SetCombinedDisplay(disableCombinedDisplayArgs);
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfig: Disabled the current Intel Combined Display on adapter {adapterNum} as it is not needed in the new display layout");
                            }
                            catch (Exception ex)
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfig: Error disabling the current Intel Combined Display on adapter {adapterNum}");
                                return false;
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
            if (_initialised && _igclApiHelper != null)
            {
                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Applying display settings stored in the display configuration");
                var currentDisplayConfig = GetIntelDisplayConfig();

                var adapters = _igclApiHelper.EnumerateAdapters();
                if (adapters == null || adapters.Count == 0)
                {
                    SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: No Intel adapters found or error getting adapter count.");
                    return false;
                }

                bool success = true;
                int adapterNum = 0;
                bool IsUnsupportedResult(ctl_result_t result)
                {
                    return result == ctl_result_t.CTL_RESULT_ERROR_UNSUPPORTED_FEATURE ||
                        result == ctl_result_t.CTL_RESULT_ERROR_UNSUPPORTED_VERSION ||
                        result == ctl_result_t.CTL_RESULT_ERROR_INVALID_OPERATION_TYPE ||
                        result == ctl_result_t.CTL_RESULT_ERROR_INVALID_ARGUMENT;
                }

                foreach (var adapter in adapters)
                {
                    adapterNum++;
                    DeviceAdapterPropertiesDto adapterProperties;

                    try
                    {
                        adapterProperties = adapter.GetProperties();
                    }
                    catch (Exception ex)
                    {
                        SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Exception getting adapter properties for adapter {adapterNum}.");
                        success = false;
                        continue;
                    }

                    var displays = adapter.EnumerateDisplayOutputs();
                    if (displays == null || displays.Count == 0)
                    {
                        continue;
                    }

                    int displayNum = 0;
                    foreach (var display in displays)
                    {
                        displayNum++;
                        DisplayPropertiesDto displayProperties;

                        try
                        {
                            displayProperties = display.GetProperties();
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Exception getting display properties for display {display.Name} on adapter {adapterNum}.");
                            success = false;
                            continue;
                        }

                        string logDisplayId = $"{display.Name}|{displayProperties.OsDisplayEncoderHandle.WindowsDisplayEncoderID}";

                        if (((uint)displayProperties.DisplayConfigFlags & (uint)ctl_display_config_flag_t.CTL_DISPLAY_CONFIG_FLAG_DISPLAY_ACTIVE) != (uint)ctl_display_config_flag_t.CTL_DISPLAY_CONFIG_FLAG_DISPLAY_ACTIVE)
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Skipping inactive display {logDisplayId} on adapter {adapterNum}");
                            continue;
                        }
                    
                        string displayDeviceId = $"VEN_{adapterProperties.PciVendorId:X4}&DEV_{adapterProperties.PciDeviceId:X4}&REV_{adapterProperties.RevId:X2}-PORT_{displayProperties.OsDisplayEncoderHandle.WindowsDisplayEncoderID}";

                        if (!displayConfig.Displays.TryGetValue(displayDeviceId, out INTEL_DISPLAY_WITH_SETTINGS storedSettings))
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: No stored settings found for display {logDisplayId} (ID {displayDeviceId}), skipping");
                            continue;
                        }
                        else
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Found stored settings for display {logDisplayId} (ID {displayDeviceId})");
                        }

                        if (!currentDisplayConfig.Displays.TryGetValue(displayDeviceId, out INTEL_DISPLAY_WITH_SETTINGS currentSettings))
                        {
                            SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: No current settings found for display {logDisplayId} (ID {displayDeviceId}), skipping");
                            continue;
                        }

                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Applying settings for display {logDisplayId}");

                        //------------------------------------
                        // SET INTEGER SCALING IF NEEDED
                        //------------------------------------
                        if (storedSettings.IsSupportedIntegerScaling)
                        {
                            try
                            {
                                if (currentSettings.IsSupportedIntegerScaling)
                                {
                                    var retroScalingSettings = currentSettings.RetroScalingSettings;
                                    if (retroScalingSettings.Enable != storedSettings.IsEnabledIntegerScaling ||
                                        (uint)retroScalingSettings.RetroScalingType != (uint)storedSettings.IntegerScalingType)
                                    {
                                        retroScalingSettings.Get = false;
                                        retroScalingSettings.Enable = storedSettings.IsEnabledIntegerScaling;
                                        retroScalingSettings.RetroScalingType = (uint)storedSettings.IntegerScalingType;
                                        display.SetRetroScalingSettings(retroScalingSettings);
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Integer Scaling to Enabled={storedSettings.IsEnabledIntegerScaling}, Type={storedSettings.IntegerScalingType}");
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Integer Scaling already set to desired values, skipping");
                                    }
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Integer Scaling not supported by current hardware, skipping");
                                }
                            }
                            catch (Exception ex)
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Integer Scaling for display {logDisplayId}");
                                success = false;
                            }
                        }

                        //------------------------------------
                        // SET GPU SCALING IF NEEDED
                        //------------------------------------
                        if (storedSettings.IsSupportedGPUScaling)
                        {
                            try
                            {
                                if (currentSettings.IsSupportedGPUScaling)
                                {
                                    var scalingSettings = currentSettings.ScalingSettings;
                                    if (scalingSettings.Enable != storedSettings.IsEnabledGPUScaling ||
                                        (uint)scalingSettings.ScalingType != (uint)storedSettings.ScalingType)
                                    {
                                        scalingSettings.Enable = storedSettings.IsEnabledGPUScaling;
                                        scalingSettings.ScalingType = (uint)storedSettings.ScalingType;
                                        display.SetCurrentScaling(scalingSettings);
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set GPU Scaling to Enabled={storedSettings.IsEnabledGPUScaling}, Type={storedSettings.ScalingType}");
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: GPU Scaling already set to desired values, skipping");
                                    }
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: GPU Scaling not supported by current hardware, skipping");
                                }
                            }
                            catch (Exception ex)
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying GPU Scaling for display {logDisplayId}");
                                success = false;
                            }
                        }

                        //------------------------------------
                        // SET IMAGE SHARPENING IF NEEDED
                        //------------------------------------
                        if (storedSettings.IsSupportedImageSharpening)
                        {
                            try
                            {
                                if (currentSettings.IsSupportedImageSharpening)
                                {
                                    var sharpnessSettings = currentSettings.SharpnessSettings;
                                    if (sharpnessSettings.Enable != storedSettings.IsEnabledImageSharpening ||
                                        (uint)sharpnessSettings.FilterType != (uint)storedSettings.SharpeningFilterType ||
                                        Math.Abs(sharpnessSettings.Intensity - storedSettings.SharpeningIntensity) > 0.001f)
                                    {
                                        sharpnessSettings.Enable = storedSettings.IsEnabledImageSharpening;
                                        sharpnessSettings.FilterType = (uint)storedSettings.SharpeningFilterType;
                                        sharpnessSettings.Intensity = storedSettings.SharpeningIntensity;
                                        display.SetCurrentSharpness(sharpnessSettings);
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Image Sharpening to Enabled={storedSettings.IsEnabledImageSharpening}, Intensity={storedSettings.SharpeningIntensity}");
                                    }
                                    else
                                    {
                                        SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Image Sharpening already set to desired values, skipping");
                                    }
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Image Sharpening not supported by current hardware, skipping");
                                }
                            }
                            catch (Exception ex)
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Image Sharpening for display {logDisplayId}");
                                success = false;
                            }
                        }

                        //------------------------------------
                        // SET DISPLAY SETTINGS IF NEEDED
                        //------------------------------------
                        try
                        {
                            var currentDisplaySettings = currentSettings.DisplaySettings;
                            if (currentSettings.IsSupportedDisplaySettings)
                            {
                                if (!INTEL_DISPLAY_WITH_SETTINGS.AreDisplaySettingsEqual(currentDisplaySettings, storedSettings.DisplaySettings))
                                {
                                    var desiredDisplaySettings = storedSettings.DisplaySettings;
                                    desiredDisplaySettings.Set = true;
                                    display.SetDisplaySettings(desiredDisplaySettings);
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set DisplaySettings for display {logDisplayId}");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: DisplaySettings already set to desired values, skipping");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: DisplaySettings not supported by current hardware, skipping");
                            }
                        }
                        catch (IGCLException ex)
                        {
                            if (IsUnsupportedResult(ex.Result))
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: DisplaySettings not supported by current hardware, skipping");
                            }
                            else
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying DisplaySettings for display {logDisplayId}");
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying DisplaySettings for display {logDisplayId}");
                            success = false;
                        }

                        //------------------------------------
                        // SET WIRE FORMAT IF NEEDED
                        //------------------------------------
                        try
                        {
                            var currentWireFormat = currentSettings.WireFormat;
                            if (currentSettings.IsSupportedWireFormat)
                            {
                                if (!EqualityComparer<ctl_wire_format_t>.Default.Equals(currentWireFormat.WireFormat, storedSettings.WireFormat.WireFormat))
                                {
                                    var desiredWireFormat = storedSettings.WireFormat;
                                    desiredWireFormat.Operation = ctl_wire_format_operation_type_t.CTL_WIRE_FORMAT_OPERATION_TYPE_SET;
                                    display.SetWireFormat(desiredWireFormat);
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set WireFormat for display {logDisplayId}");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: WireFormat already set to desired values, skipping");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: WireFormat not supported by current hardware, skipping");
                            }
                        }
                        catch (IGCLException ex)
                        {
                            if (IsUnsupportedResult(ex.Result))
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: WireFormat not supported by current hardware, skipping");
                            }
                            else
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying WireFormat for display {logDisplayId}");
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying WireFormat for display {logDisplayId}");
                            success = false;
                        }

                        //------------------------------------
                        // SET BRIGHTNESS IF NEEDED
                        //------------------------------------
                        try
                        {
                            var currentBrightness = currentSettings.Brightness;
                            if (currentBrightness.TargetBrightness != storedSettings.Brightness.TargetBrightness)
                            {
                                var brightnessToSet = IGCLDisplayHelper.CreateSetBrightness();
                                brightnessToSet.TargetBrightness = storedSettings.Brightness.TargetBrightness;
                                brightnessToSet.SmoothTransitionTimeInMs = 0;
                                display.SetBrightnessSetting(brightnessToSet);
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Brightness to {storedSettings.Brightness.TargetBrightness} for display {logDisplayId}");
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Brightness already set to desired values, skipping");
                            }
                        }
                        catch (IGCLException ex)
                        {
                            if (IsUnsupportedResult(ex.Result))
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Brightness not supported by current hardware, skipping");
                            }
                            else
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Brightness for display {logDisplayId}");
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Brightness for display {logDisplayId}");
                            success = false;
                        }

                        //------------------------------------
                        // SET POWER OPTIMIZATION IF NEEDED
                        //------------------------------------
                        try
                        {
                            if (currentSettings.IsSupportedPowerOptimization)
                            {
                                var currentPowerOptimization = currentSettings.PowerOptimizationSettings;
                                if (!EqualityComparer<PowerOptimizationSettingsDto>.Default.Equals(currentPowerOptimization, storedSettings.PowerOptimizationSettings))
                                {
                                    display.SetPowerOptimizationSetting(storedSettings.PowerOptimizationSettings);
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set PowerOptimization settings for display {logDisplayId}");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: PowerOptimization settings already set to desired values, skipping");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: PowerOptimization not supported by current hardware, skipping");
                            }
                        }
                        catch (IGCLException ex)
                        {
                            if (IsUnsupportedResult(ex.Result))
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: PowerOptimization not supported by current hardware, skipping");
                            }
                            else
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying PowerOptimization settings for display {logDisplayId}");
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying PowerOptimization settings for display {logDisplayId}");
                            success = false;
                        }

                        //------------------------------------
                        // SET LACE CONFIG IF NEEDED
                        //------------------------------------
                        try
                        {
                            var currentLaceConfig = currentSettings.LaceConfig;
                            if (!EqualityComparer<LaceConfigDto>.Default.Equals(currentLaceConfig, storedSettings.LaceConfig))
                            {
                                display.SetLACEConfig(storedSettings.LaceConfig);
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set LACE config for display {logDisplayId}");
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: LACE config already set to desired values, skipping");
                            }
                        }
                        catch (IGCLException ex)
                        {
                            if (IsUnsupportedResult(ex.Result))
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: LACE not supported by current hardware, skipping");
                            }
                            else
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying LACE config for display {logDisplayId}");
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying LACE config for display {logDisplayId}");
                            success = false;
                        }

                        //------------------------------------
                        // SET SOFTWARE PSR IF NEEDED
                        //------------------------------------
                        try
                        {
                            var currentPsrSettings = currentSettings.SoftwarePsrSettings;
                            if (currentPsrSettings.Supported)
                            {
                                if (!EqualityComparer<SwPsrSettingsDto>.Default.Equals(currentPsrSettings, storedSettings.SoftwarePsrSettings))
                                {
                                    var desiredPsrSettings = storedSettings.SoftwarePsrSettings;
                                    desiredPsrSettings.Set = true;
                                    display.SoftwarePSR(desiredPsrSettings);
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Software PSR settings for display {logDisplayId}");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Software PSR settings already set to desired values, skipping");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Software PSR not supported by current hardware, skipping");
                            }
                        }
                        catch (IGCLException ex)
                        {
                            if (IsUnsupportedResult(ex.Result))
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Software PSR not supported by current hardware, skipping");
                            }
                            else
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Software PSR settings for display {logDisplayId}");
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Software PSR settings for display {logDisplayId}");
                            success = false;
                        }

                        //------------------------------------
                        // SET DYNAMIC CONTRAST ENHANCEMENT IF NEEDED
                        //------------------------------------
                        try
                        {
                            var currentDceArgs = currentSettings.DynamicContrastEnhancement;
                            if (currentSettings.IsSupportedDynamicContrastEnhancement)
                            {
                                bool dceDifferent =
                                    currentDceArgs.Enable != storedSettings.DynamicContrastEnhancement.Enable ||
                                    currentDceArgs.TargetBrightnessPercent != storedSettings.DynamicContrastEnhancement.TargetBrightnessPercent ||
                                    currentDceArgs.PhaseinSpeedMultiplier != storedSettings.DynamicContrastEnhancement.PhaseinSpeedMultiplier ||
                                    currentDceArgs.NumBins != storedSettings.DynamicContrastEnhancement.NumBins;

                                if (dceDifferent)
                                {
                                    var desiredDceArgs = storedSettings.DynamicContrastEnhancement;
                                    desiredDceArgs.Set = true;
                                    var desiredHistogram = storedSettings.DynamicContrastEnhancementHistogram ?? Array.Empty<uint>();
                                    display.SetDynamicContrastEnhancement(desiredDceArgs, desiredHistogram);
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Dynamic Contrast Enhancement for display {logDisplayId}");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Dynamic Contrast Enhancement already set to desired values, skipping");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Dynamic Contrast Enhancement not supported by current hardware, skipping");
                            }
                        }
                        catch (IGCLException ex)
                        {
                            if (IsUnsupportedResult(ex.Result))
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Dynamic Contrast Enhancement not supported by current hardware, skipping");
                            }
                            else
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Dynamic Contrast Enhancement for display {logDisplayId}");
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Dynamic Contrast Enhancement for display {logDisplayId}");
                            success = false;
                        }

                        //------------------------------------
                        // SET INTEL ARC SYNC PROFILE IF NEEDED
                        //------------------------------------
                        try
                        {
                            if (currentSettings.IsSupportedIntelArcSync)
                            {
                                var currentArcSyncProfile = currentSettings.IntelArcSyncProfile;
                                if (!EqualityComparer<ctl_intel_arc_sync_profile_params_t>.Default.Equals(currentArcSyncProfile, storedSettings.IntelArcSyncProfile))
                                {
                                    display.SetIntelArcSyncProfile(storedSettings.IntelArcSyncProfile);
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Intel Arc Sync profile for display {logDisplayId}");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Intel Arc Sync profile already set to desired values, skipping");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Intel Arc Sync not supported by current hardware, skipping");
                            }
                        }
                        catch (IGCLException ex)
                        {
                            if (IsUnsupportedResult(ex.Result))
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Intel Arc Sync not supported by current hardware, skipping");
                            }
                            else
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Intel Arc Sync profile for display {logDisplayId}");
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Intel Arc Sync profile for display {logDisplayId}");
                            success = false;
                        }

                        //------------------------------------
                        // SET CUSTOM MODES IF NEEDED
                        //------------------------------------
                        try
                        {
                            if (storedSettings.CustomModes != null && storedSettings.CustomModes.Length > 0)
                            {
                                var currentCustomModes = currentSettings.CustomModes;
                                bool customModesDifferent = currentCustomModes == null ||
                                    currentCustomModes.Length != storedSettings.CustomModes.Length ||
                                    !currentCustomModes.SequenceEqual(storedSettings.CustomModes);

                                if (customModesDifferent)
                                {
                                    var desiredCustomModeArgs = storedSettings.CustomModeArgs;
                                    desiredCustomModeArgs.CustomModeOpType = ctl_custom_mode_operation_types_t.CTL_CUSTOM_MODE_OPERATION_TYPES_ADD_CUSTOM_SOURCE_MODE;
                                    desiredCustomModeArgs.NumOfModes = (uint)storedSettings.CustomModes.Length;
                                    display.SetCustomModes(desiredCustomModeArgs, storedSettings.CustomModes);
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Successfully set Custom Modes for display {logDisplayId}");
                                }
                                else
                                {
                                    SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Custom Modes already set to desired values, skipping");
                                }
                            }
                            else
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: No Custom Modes stored for display {logDisplayId}, skipping");
                            }
                        }
                        catch (IGCLException ex)
                        {
                            if (IsUnsupportedResult(ex.Result))
                            {
                                SharedLogger.logger.Trace($"IntelLibrary/SetActiveConfigOverride: Custom Modes not supported by current hardware, skipping");
                            }
                            else
                            {
                                SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Custom Modes for display {logDisplayId}");
                                success = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error(ex, $"IntelLibrary/SetActiveConfigOverride: Error applying Custom Modes for display {logDisplayId}");
                            success = false;
                        }

                        if (delayInMs > 0)
                        {
                            Thread.Sleep(delayInMs);
                        }
                    }
                }

                return success;
            }
            else
            {
                SharedLogger.logger.Error($"IntelLibrary/SetActiveConfigOverride: ERROR - Tried to run SetActiveConfigOverride but the Intel IGCL library isn't initialised!");
                throw new IntelLibraryException($"Tried to run SetActiveConfigOverride but the Intel IGCL library isn't initialised!");
            }
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

            // IGCL does not support identifying the displays that are part of a combined display. We have no way of knowing this,
            // so we will just assume that if any of the display identifiers in the config are part of a combined display,
            // then they are available now. This is not ideal, but there is no other way to do this with IGCL.
            // TODO: Find a way to use the Windows IDs to identify combined displays in IGCL.
            if (displayConfig.CombinedDisplayIsInUse || _activeDisplayConfig.Value.CombinedDisplayIsInUse)
            {
                return true;
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

                        var manufacturerCode = "";
                        var productCode = "";
                        try {
                            var edidBytes = display.GetPanelEdidData();
                            var edid = new EDID(edidBytes);
                            manufacturerCode = edid.ManufacturerCode.ToString();
                            productCode = edid.ProductCode.ToString();
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error($"IntelLibrary/GetAllConnectedDisplayIdentifiers: ERROR - Failed to get EDID for display Index {displayNum} on Adapter {adapterNum}: {ex.Message}");
                        }
                        List<string> displayInfo = new List<string>
                        {
                            "IntelIGCL",
                            adapter.Name,
                            adapterDeviceProperties.PciDeviceId.ToString("G"),
                            adapterDeviceProperties.PciSubsysId.ToString("G"),
                            displayProperties.Type.ToString(),
                            displayProperties.AttachedDisplayMuxType.ToString(),
                            displayProperties.ProtocolConverterOutput.ToString(),
                            displayProperties.ProtocolConverterType.ToString(),
                            manufacturerCode,
                            productCode,
                            displayProperties.OsDisplayEncoderHandle.WindowsDisplayEncoderID.ToString("G")                          
                        };
                        
                        string displayIdentifier = String.Join("#", displayInfo);
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

                        var manufacturerCode = "";
                        var productCode = "";

                        try {
                            var edidBytes = display.GetPanelEdidData();
                            var edid = new EDID(edidBytes);
                            manufacturerCode = edid.ManufacturerCode.ToString();
                            productCode = edid.ProductCode.ToString();
                        }
                        catch (Exception ex)
                        {
                            SharedLogger.logger.Error($"IntelLibrary/GetAllConnectedDisplayIdentifiers: ERROR - Failed to get EDID for display Index {displayNum} on Adapter {adapterNum}: {ex.Message}");
                        }
                        List<string> displayInfo = new List<string>
                        {
                            "IntelIGCL",
                            adapter.Name,
                            adapterDeviceProperties.PciDeviceId.ToString("G"),
                            adapterDeviceProperties.PciSubsysId.ToString("G"),
                            displayProperties.Type.ToString(),
                            displayProperties.AttachedDisplayMuxType.ToString(),
                            displayProperties.ProtocolConverterOutput.ToString(),
                            displayProperties.ProtocolConverterType.ToString(),
                            manufacturerCode,
                            productCode,
                            displayProperties.OsDisplayEncoderHandle.WindowsDisplayEncoderID.ToString("G")                          
                        };
                        
                        string displayIdentifier = String.Join("#", displayInfo);
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

