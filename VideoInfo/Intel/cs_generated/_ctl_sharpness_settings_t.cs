namespace IGCLWrapper
{
    public partial struct _ctl_sharpness_settings_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("bool")]
        public byte Enable;

        [NativeTypeName("ctl_sharpness_filter_type_flags_t")]
        public uint FilterType;

        public float Intensity;
    }
}
