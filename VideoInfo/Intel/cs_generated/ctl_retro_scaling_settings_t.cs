namespace IGCLWrapper
{
    public partial struct ctl_retro_scaling_settings_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte Get;

        [NativeTypeName("bool")]
        public byte Enable;

        [NativeTypeName("ctl_retro_scaling_type_flags_t")]
        public uint RetroScalingType;
    }
}
