using System;

namespace IGCLWrapper
{
    /// <summary>
    /// Helper methods for IGCL structures and handles
    /// NOTE: These are static methods, not extensions, because C# doesn't allow extension methods on pointers
    /// </summary>
    public static class IGCLHelpers
    {
        /// <summary>
        /// Get adapter properties
        /// </summary>
        public static unsafe ctl_device_adapter_properties_t GetProperties(IntPtr hAdapter)
        {
            var props = new ctl_device_adapter_properties_t
            {
                Size = (uint)sizeof(ctl_device_adapter_properties_t),
                Version = (byte)1 // Adapter properties use version 1
            };

            var result = IGCL.ctlGetDeviceProperties((_ctl_device_adapter_handle_t*)hAdapter, &props);
            
            if (result != ctl_result_t.CTL_RESULT_SUCCESS)
            {
                throw new IGCLException(result, "Failed to get adapter properties");
            }

            return props;
        }

        /// <summary>
        /// Get display properties
        /// </summary>
        public static unsafe ctl_display_properties_t GetDisplayProperties(IntPtr hDisplay)
        {
            var props = new ctl_display_properties_t
            {
                Size = (uint)sizeof(ctl_display_properties_t),
                Version = (byte)0 // Display properties use version 0
            };

            var result = IGCL.ctlGetDisplayProperties((_ctl_display_output_handle_t*)hDisplay, &props);
            
            if (result != ctl_result_t.CTL_RESULT_SUCCESS)
            {
                throw new IGCLException(result, "Failed to get display properties");
            }

            return props;
        }

        /// <summary>
        /// Get display timing information
        /// </summary>
        public static unsafe ctl_display_timing_t GetTiming(IntPtr hDisplay)
        {
            var props = GetDisplayProperties(hDisplay);
            return props.Display_Timing_Info;
        }

        /// <summary>
        /// Check if a display is currently active
        /// </summary>
        public static unsafe bool IsActive(IntPtr hDisplay)
        {
            var props = GetDisplayProperties(hDisplay);
            return props.Display_Timing_Info.HActive > 0 && props.Display_Timing_Info.VActive > 0;
        }

        /// <summary>
        /// Get refresh rate in Hz
        /// </summary>
        public static unsafe double GetRefreshRate(IntPtr hDisplay)
        {
            var timing = GetTiming(hDisplay);
            return timing.RefreshRate / 1000.0; // Convert from mHz to Hz
        }

        /// <summary>
        /// Get display resolution as (width, height) tuple
        /// </summary>
        public static unsafe (uint width, uint height) GetResolution(IntPtr hDisplay)
        {
            var timing = GetTiming(hDisplay);
            return (timing.HActive, timing.VActive);
        }
    }

    /// <summary>
    /// Helper methods for structure initialization
    /// </summary>
    public static class IGCLStructHelper
    {
        /// <summary>
        /// Create a properly initialized ctl_init_args_t structure
        /// </summary>
        public static unsafe ctl_init_args_t CreateInitArgs()
        {
            return new ctl_init_args_t
            {
                Size = (uint)sizeof(ctl_init_args_t),
                Version = (byte)0,
                AppVersion = IGCLApi.GetImplVersion(),
                flags = (uint)ctl_init_flag_t.CTL_INIT_FLAG_USE_LEVEL_ZERO,
                SupportedVersion = IGCLApi.GetImplVersion(),
                ApplicationUID = default
            };
        }

        /// <summary>
        /// Create a properly initialized ctl_device_adapter_properties_t structure
        /// </summary>
        public static unsafe ctl_device_adapter_properties_t CreateAdapterProperties()
        {
            return new ctl_device_adapter_properties_t
            {
                Size = (uint)sizeof(ctl_device_adapter_properties_t),
                Version = (byte)1
            };
        }

        /// <summary>
        /// Create a properly initialized ctl_display_properties_t structure
        /// </summary>
        public static unsafe ctl_display_properties_t CreateDisplayProperties()
        {
            return new ctl_display_properties_t
            {
                Size = (uint)sizeof(ctl_display_properties_t),
                Version = (byte)0
            };
        }

        /// <summary>
        /// Create a properly initialized ctl_3d_feature_caps_t structure
        /// </summary>
        public static unsafe ctl_3d_feature_caps_t Create3DFeatureCaps()
        {
            return new ctl_3d_feature_caps_t
            {
                Size = (uint)sizeof(ctl_3d_feature_caps_t),
                Version = (byte)0
            };
        }

        /// <summary>
        /// Create a properly initialized ctl_power_telemetry_t structure
        /// </summary>
        public static unsafe ctl_power_telemetry_t CreatePowerTelemetry()
        {
            return new ctl_power_telemetry_t
            {
                Size = (uint)sizeof(ctl_power_telemetry_t),
                Version = (byte)0
            };
        }
    }
}
