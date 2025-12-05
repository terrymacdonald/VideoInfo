namespace IGCLWrapper
{
    public partial struct ctl_sw_psr_settings_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte Set;

        [NativeTypeName("bool")]
        public byte Supported;

        [NativeTypeName("bool")]
        public byte Enable;
    }
}
