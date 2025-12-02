using System.Runtime.CompilerServices;

namespace IGCLWrapper
{
    public partial struct _ctl_power_telemetry_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t timeStamp;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t gpuEnergyCounter;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t gpuVoltage;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t gpuCurrentClockFrequency;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t gpuCurrentTemperature;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t globalActivityCounter;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t renderComputeActivityCounter;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t mediaActivityCounter;

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

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t vramEnergyCounter;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t vramVoltage;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t vramCurrentClockFrequency;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t vramCurrentEffectiveFrequency;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t vramReadBandwidthCounter;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t vramWriteBandwidthCounter;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t vramCurrentTemperature;

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

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t totalCardEnergyCounter;

        [NativeTypeName("ctl_psu_info_t[5]")]
        public _psu_e__FixedBuffer psu;

        [NativeTypeName("ctl_oc_telemetry_item_t[5]")]
        public _fanSpeed_e__FixedBuffer fanSpeed;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t gpuVrTemp;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t vramVrTemp;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t saVrTemp;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t gpuEffectiveClock;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t gpuOverVoltagePercent;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t gpuPowerPercent;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t gpuTemperaturePercent;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t vramReadBandwidth;

        [NativeTypeName("ctl_oc_telemetry_item_t")]
        public _ctl_oc_telemetry_item_t vramWriteBandwidth;

        [InlineArray(5)]
        public partial struct _psu_e__FixedBuffer
        {
            public _ctl_psu_info_t e0;
        }

        [InlineArray(5)]
        public partial struct _fanSpeed_e__FixedBuffer
        {
            public _ctl_oc_telemetry_item_t e0;
        }
    }
}
