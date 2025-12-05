using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct ctl_power_telemetry_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_oc_telemetry_item_t timeStamp;

        public ctl_oc_telemetry_item_t gpuEnergyCounter;

        public ctl_oc_telemetry_item_t gpuVoltage;

        public ctl_oc_telemetry_item_t gpuCurrentClockFrequency;

        public ctl_oc_telemetry_item_t gpuCurrentTemperature;

        public ctl_oc_telemetry_item_t globalActivityCounter;

        public ctl_oc_telemetry_item_t renderComputeActivityCounter;

        public ctl_oc_telemetry_item_t mediaActivityCounter;

        [NativeTypeName("bool")]
        public byte gpuPowerLimited;

        [NativeTypeName("bool")]
        public byte gpuTemperatureLimited;

        [NativeTypeName("bool")]
        public byte gpuCurrentLimited;

        [NativeTypeName("bool")]
        public byte gpuVoltageLimited;

        [NativeTypeName("bool")]
        public byte gpuUtilizationLimited;

        public ctl_oc_telemetry_item_t vramEnergyCounter;

        public ctl_oc_telemetry_item_t vramVoltage;

        public ctl_oc_telemetry_item_t vramCurrentClockFrequency;

        public ctl_oc_telemetry_item_t vramCurrentEffectiveFrequency;

        public ctl_oc_telemetry_item_t vramReadBandwidthCounter;

        public ctl_oc_telemetry_item_t vramWriteBandwidthCounter;

        public ctl_oc_telemetry_item_t vramCurrentTemperature;

        [NativeTypeName("bool")]
        public byte vramPowerLimited;

        [NativeTypeName("bool")]
        public byte vramTemperatureLimited;

        [NativeTypeName("bool")]
        public byte vramCurrentLimited;

        [NativeTypeName("bool")]
        public byte vramVoltageLimited;

        [NativeTypeName("bool")]
        public byte vramUtilizationLimited;

        public ctl_oc_telemetry_item_t totalCardEnergyCounter;

        [NativeTypeName("ctl_psu_info_t[5]")]
        public _psu_e__FixedBuffer psu;

        [NativeTypeName("ctl_oc_telemetry_item_t[5]")]
        public _fanSpeed_e__FixedBuffer fanSpeed;

        public ctl_oc_telemetry_item_t gpuVrTemp;

        public ctl_oc_telemetry_item_t vramVrTemp;

        public ctl_oc_telemetry_item_t saVrTemp;

        public ctl_oc_telemetry_item_t gpuEffectiveClock;

        public ctl_oc_telemetry_item_t gpuOverVoltagePercent;

        public ctl_oc_telemetry_item_t gpuPowerPercent;

        public ctl_oc_telemetry_item_t gpuTemperaturePercent;

        public ctl_oc_telemetry_item_t vramReadBandwidth;

        public ctl_oc_telemetry_item_t vramWriteBandwidth;

        [InlineArray(5)]
        public partial struct _psu_e__FixedBuffer
        {
            public ctl_psu_info_t e0;
        }

        [InlineArray(5)]
        public partial struct _fanSpeed_e__FixedBuffer
        {
            public ctl_oc_telemetry_item_t e0;
        }
    }
}
