namespace IGCLWrapper
{
    public partial struct _ctl_power_limits_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_power_sustained_limit_t")]
        public _ctl_power_sustained_limit_t sustainedPowerLimit;

        [NativeTypeName("ctl_power_burst_limit_t")]
        public _ctl_power_burst_limit_t burstPowerLimit;

        [NativeTypeName("ctl_power_peak_limit_t")]
        public _ctl_power_peak_limit_t peakPowerLimits;
    }
}
