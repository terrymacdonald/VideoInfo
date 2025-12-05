namespace IGCLWrapper
{
    public partial struct ctl_power_limits_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        public ctl_power_sustained_limit_t sustainedPowerLimit;

        public ctl_power_burst_limit_t burstPowerLimit;

        public ctl_power_peak_limit_t peakPowerLimits;
    }
}
