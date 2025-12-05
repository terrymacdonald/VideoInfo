namespace IGCLWrapper
{
    public partial struct ctl_init_args_t
    {
        [NativeTypeName("uint32_t")]
        public uint Size;

        [NativeTypeName("uint8_t")]
        public byte Version;

        [NativeTypeName("ctl_version_info_t")]
        public uint AppVersion;

        [NativeTypeName("ctl_init_flags_t")]
        public uint flags;

        [NativeTypeName("ctl_version_info_t")]
        public uint SupportedVersion;

        public ctl_application_id_t ApplicationUID;
    }
}
