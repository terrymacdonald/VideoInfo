namespace IGCLWrapper
{
    public partial struct _ctl_power_optimization_caps_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_power_optimization_flags_t")]
        public uint SupportedFeatures;
    }
}
