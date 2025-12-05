namespace IGCLWrapper
{
    public unsafe partial struct ctl_sharpness_caps_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_sharpness_filter_type_flags_t")]
        public uint SupportedFilterFlags;

        [NativeTypeName("uint8_t")]
        public byte NumFilterTypes;

        public ctl_sharpness_filter_properties_t* pFilterProperty;
    }
}
