namespace IGCLWrapper
{
    public partial struct _ctl_power_optimization_dpst_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("uint8_t")]
        public byte MinLevel;

        [NativeTypeName("uint8_t")]
        public byte MaxLevel;

        [NativeTypeName("uint8_t")]
        public byte Level;

        [NativeTypeName("ctl_power_optimization_dpst_flags_t")]
        public uint SupportedFeatures;

        [NativeTypeName("ctl_power_optimization_dpst_flags_t")]
        public uint EnabledFeatures;
    }
}
