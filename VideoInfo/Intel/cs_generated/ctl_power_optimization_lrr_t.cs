namespace IGCLWrapper
{
    public partial struct ctl_power_optimization_lrr_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_power_optimization_lrr_flags_t")]
        public uint SupportedLRRTypes;

        [NativeTypeName("ctl_power_optimization_lrr_flags_t")]
        public uint CurrentLRRTypes;

        [NativeTypeName("bool")]
        public byte bRequirePSRDisable;

        [NativeTypeName("uint16_t")]
        public ushort LowRR;
    }
}
