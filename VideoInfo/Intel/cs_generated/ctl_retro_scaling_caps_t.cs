namespace IGCLWrapper
{
    public partial struct ctl_retro_scaling_caps_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_retro_scaling_type_flags_t")]
        public uint SupportedRetroScaling;
    }
}
